using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day17
{
    public class Day17
    {
        private string passcode;

        public Day17(string passcode)
        {
            this.passcode = passcode;
        }

        public bool UpIsOpen(string path)
        {
            var position = Coordinates(path);

            if (position.Item2 == 0) return false;

            return Day5.MD5.GetMD5Hash(passcode + path)[0] >= 'b';
        }

        public bool DownIsOpen(string path)
        {
            var position = Coordinates(path);

            if (position.Item2 == 3) return false;

            return Day5.MD5.GetMD5Hash(passcode + path)[1] >= 'b';
        }

        public bool LeftIsOpen(string path)
        {
            var position = Coordinates(path);

            if (position.Item1 == 0) return false;

            return Day5.MD5.GetMD5Hash(passcode + path)[2] >= 'b';
        }

        public bool RightIsOpen(string path)
        {
            var position = Coordinates(path);

            if (position.Item1 == 3) return false;

            return Day5.MD5.GetMD5Hash(passcode + path)[3] >= 'b';
        }

        private Tuple<int,int> Coordinates(string path)
        {
            int x = 0, y = 0;

            foreach(var item in path)
            {
                switch (item)
                {
                    case 'U':
                        y--;
                        break;
                    case 'D':
                        y++;
                        break;
                    case 'L':
                        x--;
                        break;
                    case 'R':
                        x++;
                        break;
                }
            }

            return Tuple.Create(x, y);
        }

        public string ShortestPath(int x, int y)
        {
            var queue = new Queue<string>();

            queue.Enqueue("");

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (Coordinates(current).Equals(Tuple.Create(x, y)))
                    return current;

                foreach (var item in PossibleMoves(current))
                    queue.Enqueue(current + item);
            }

            return "";
        }

        public int LongestPath(int x, int y)
        {
            var queue = new Queue<string>();
            string longest = "";

            queue.Enqueue("");

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (Coordinates(current).Equals(Tuple.Create(x, y)))
                {
                    if (current.Length > longest.Length)
                        longest = current;
                    continue;
                }

                foreach (var item in PossibleMoves(current))
                    queue.Enqueue(current + item);
            }

            return longest.Length;
        }

        private IEnumerable<string> PossibleMoves(string path)
        {
            if (UpIsOpen(path)) yield return "U";
            if (DownIsOpen(path)) yield return "D";
            if (LeftIsOpen(path)) yield return "L";
            if (RightIsOpen(path)) yield return "R";
        }
    }
}
