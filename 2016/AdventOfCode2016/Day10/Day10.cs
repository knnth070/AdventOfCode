using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day10
{
    internal struct DestinationValuePair
    {
        public string destination;
        public int value;
    }

    public class Day10
    {
        private List<DestinationValuePair> destinations;
        private List<string> instructions;

        public Day10(string[] lines)
        {
            destinations = new List<DestinationValuePair>();
            instructions = new List<string>();

            foreach (var line in lines)
            {
                if (line.StartsWith("value"))
                {
                    var tokens = line.Split(' ');
                    var value = int.Parse(tokens[1]);
                    destinations.Add(new DestinationValuePair { destination = tokens[4] + tokens[5], value = value });
                }
                else
                {
                    instructions.Add(line);
                }
            }
        }

        public bool DestinationHasValue(string destination, int value)
        {
            return destinations.Any(x => x.destination == destination && x.value == value);
        }

        public void Process()
        {
            string source;
            var unprocessed = new List<string>();

            do
            {
                unprocessed.Clear();

                foreach (var line in instructions)
                {
                    var tokens = line.Split(' ');
                    source = tokens[0] + tokens[1];

                    if (NumberOfChips(source) != 2)
                    {
                        unprocessed.Add(line);
                        continue;
                    }
                    var lower = MoveLowerChip(source, tokens[5] + tokens[6]);
                    var upper = MoveUpperChip(source, tokens[10] + tokens[11]);

                    if (lower == 17 && upper == 61) Console.WriteLine($"{source}");
                }

                instructions = unprocessed.ToList();
            } while (unprocessed.Count > 0);

            var result = destinations
                            .Where(x => x.destination.StartsWith("output"))
                            .OrderBy(x => int.Parse(x.destination.Substring(6)))
                            .Take(3)
                            .Select(x => x.value)
                            .Aggregate(1, (a, b) => a * b);

            Console.WriteLine(result);
        }

        private int NumberOfChips(string source)
        {
            return destinations.Where(x => x.destination == source).Count();
        }

        private int MoveLowerChip(string source, string destination)
        {
            var item = destinations
                        .Where(x => x.destination == source)
                        .OrderBy(x => x.value)
                        .FirstOrDefault();
            if (item.Equals(default(DestinationValuePair))) return 0;

            destinations.RemoveAll(x => x.destination == item.destination && x.value == item.value);
            destinations.Add(new DestinationValuePair { destination = destination, value = item.value });

            return item.value;
        }

        private int MoveUpperChip(string source, string destination)
        {
            var item = destinations
                        .Where(x => x.destination == source)
                        .OrderByDescending(x => x.value)
                        .FirstOrDefault();
            if (item.Equals(default(DestinationValuePair))) return 0;

            destinations.RemoveAll(x => x.destination == item.destination && x.value == item.value);
            destinations.Add(new DestinationValuePair { destination = destination, value = item.value });

            return item.value;
        }
    }
}
