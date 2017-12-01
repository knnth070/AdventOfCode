using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day22
{
    internal class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int Used { get; set; }
        public int Avail { get; set; }

        public Node(string line)
        {
            var tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            X = Convert.ToInt32(tokens[0].Split('-')[1].Substring(1));
            Y = Convert.ToInt32(tokens[0].Split('-')[2].Substring(1));

            Size = Convert.ToInt32(tokens[1].Substring(0, tokens[1].Length - 1));
            Used = Convert.ToInt32(tokens[2].Substring(0, tokens[2].Length - 1));
            Avail = Convert.ToInt32(tokens[3].Substring(0, tokens[3].Length - 1));
        }
    }

    public class Day22
    {
        private List<Node> nodes;

        public Day22(string[] input)
        {
            nodes = new List<Node>();

            foreach (var item in input.Skip(2))
                nodes.Add(new Node(item));
        }

        public int ViablePairs()
        {
            int viable = 0;

            foreach (var Anode in nodes)
                foreach (var Bnode in nodes)
                    if (Anode.Used != 0 &&
                        !(Anode.X == Bnode.X && Anode.Y == Bnode.Y) &&
                        Anode.Used <= Bnode.Avail)
                        viable++;

            return viable;
        }

        public void PrintMaze()
        {
            var maxX = nodes.OrderByDescending(n => n.X).First().X;
            var maxY = nodes.OrderByDescending(n => n.Y).First().Y;

            for (int y = 0; y <= maxY; y++)
            {
                foreach (var node in nodes.Where(n => n.Y == y).OrderBy(n => n.X))
                {
                    var pct = node.Used * 100 / node.Size;
                    string str = "_";
                    if (pct > 0 && pct <= 90) str = ".";
                    if (pct > 90) str = "#";
                    if (node.X == maxX && node.Y == 0) str = "G";

                    Console.Write($"{str}");
                }
                Console.WriteLine();
            }
        }
    }
}
