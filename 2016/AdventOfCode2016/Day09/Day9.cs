using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day9
{
    public class Day9
    {
        public long Decompress(string input, bool recurse = false)
        {
            var pattern = @"\([0-9]+x[0-9]+\)";
            int offset = 0;
            long length = input.Length;

            var match = Regex.Match(input.Substring(offset), pattern);
            while (match.Success && offset < input.Length)
            {
                var parameters = match.Value.Substring(1, match.Length - 2).Split('x');
                var size = int.Parse(parameters[0]);
                var repetitions = int.Parse(parameters[1]);

                var startOfSequence = match.Index + match.Length + offset;
                var sequenceToRepeat = input.Substring(startOfSequence, size);

                var sb = new StringBuilder();
                for (int i = 0; i < repetitions; i++)
                    sb.Append(sequenceToRepeat);
                var result = sb.ToString();

                length -= match.Length + size;
                length += size * repetitions;

                if (recurse)
                {
                    length -= result.Length;
                    length += Decompress(result, true);
                }

                offset += match.Index + match.Length + size;
                if (offset < input.Length)
                    match = Regex.Match(input.Substring(offset), pattern);
            }

            return length;
        }
    }
}
