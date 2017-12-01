using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day13
{
    public class Day13
    {
        private int seed;

        public Day13(int seed)
        {
            this.seed = seed;
        }

        public int FindShortestPath(int goalX, int goalY)
        {
            var queue = new Queue<Tuple<int, int, int>>();
            var visited = new List<string>();

            queue.Enqueue(Tuple.Create(1, 1, 0));

            while (queue.Count > 0)
            {
                int x, y, distance;

                var current = queue.Dequeue();
                x = current.Item1;
                y = current.Item2;
                distance = current.Item3;

                if (x == goalX && y == goalY)
                    return distance;

                if (visited.Contains($"{x}, {y}"))
                    continue;

                visited.Add($"{x}, {y}");
                if (IsOpenSpace(x, y - 1))
                    queue.Enqueue(Tuple.Create(x, y - 1, distance + 1));
                if (IsOpenSpace(x, y + 1))
                    queue.Enqueue(Tuple.Create(x, y + 1, distance + 1));
                if (IsOpenSpace(x - 1, y))
                    queue.Enqueue(Tuple.Create(x - 1, y, distance + 1));
                if (IsOpenSpace(x + 1, y))
                    queue.Enqueue(Tuple.Create(x + 1, y, distance + 1));
            }

            return 0;
        }

        public int FindNumberOfLocations(int maxDistance)
        {
            var queue = new Queue<Tuple<int, int, int>>();
            var visited = new List<string>();

            queue.Enqueue(Tuple.Create(1, 1, 0));

            while (queue.Count > 0)
            {
                int x, y, distance;

                var current = queue.Dequeue();
                x = current.Item1;
                y = current.Item2;
                distance = current.Item3;

                if (distance > maxDistance)
                    return visited.Count;

                if (visited.Contains($"{x}, {y}"))
                    continue;

                visited.Add($"{x}, {y}");
                if (IsOpenSpace(x, y - 1))
                    queue.Enqueue(Tuple.Create(x, y - 1, distance + 1));
                if (IsOpenSpace(x, y + 1))
                    queue.Enqueue(Tuple.Create(x, y + 1, distance + 1));
                if (IsOpenSpace(x - 1, y))
                    queue.Enqueue(Tuple.Create(x - 1, y, distance + 1));
                if (IsOpenSpace(x + 1, y))
                    queue.Enqueue(Tuple.Create(x + 1, y, distance + 1));
            }

            return 0;
        }

        public bool IsOpenSpace(int x, int y)
        {
            if (x < 0 || y < 0) return false;

            var sum = x * x + 3 * x + 2 * x * y + y + y * y;
            sum += seed;

            return Convert.ToString(sum, 2)
                    .ToCharArray()
                    .Where(c => c == '1')
                    .Count() % 2 == 0;
        }
    }
}
