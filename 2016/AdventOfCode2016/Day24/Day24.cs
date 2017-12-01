using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day24
{
    public class Day24
    {
        struct Position : IEquatable<Position>
        {
            public int X { get; set; }
            public int Y { get; set; }

            public bool Equals(Position other)
            {
                return X == other.X && Y == other.Y;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Position))
                    return false;

                return Equals((Position)obj);
            }

            public static bool operator ==(Position a, Position b)
            {
                return a.Equals(b);
            }

            public static bool operator !=(Position a, Position b)
            {
                return !a.Equals(b);
            }

            public override int GetHashCode()
            {
                return X.GetHashCode() << 5 + Y.GetHashCode();
            }
        }

        struct State
        {
            public int Cost { get; set; }
            public Position Current { get; set; }
        }

        struct Path
        {
            public int From { get; set; }
            public int To { get; set; }
            public int Cost { get; set; }
        }

        private string[] maze;
        private HashSet<Position> waypoints;
        private Position start;
        private List<Path> paths;

        public Day24(string[] input)
        {
            this.maze = input;
            waypoints = new HashSet<Position>();
        }

        private void CollectWaypoints()
        {
            for (int row = 0; row < maze.Length; row++)
                for (int column = 0; column < maze[row].Length; column++)
                    if (char.IsNumber(maze[row][column]))
                    {
                        if (maze[row][column] == '0')
                            start = new Position { X = column, Y = row };

                        waypoints.Add(new Position { X = column, Y = row });
                    }
        }

        private void CalculatePathCosts()
        {
            paths = new List<Path>();
            foreach (var from in waypoints)
                foreach (var to in waypoints.Where(w => w != from))
                {
                    var fromWaypoint = maze[from.Y][from.X] - '0';
                    var toWaypoint = maze[to.Y][to.X] - '0';
                    if (paths.Any(p => p.From == fromWaypoint && p.To == toWaypoint ||
                                        p.From == toWaypoint && p.To == fromWaypoint))
                        continue;

                    paths.Add(new Path
                    {
                        From = fromWaypoint,
                        To = toWaypoint,
                        Cost = FindShortestPath(from, to)
                    });
                }
        }

        public int FindShortestPath(bool cycle = false)
        {
            int shortest = Int32.MaxValue;

            CollectWaypoints();
            CalculatePathCosts();

            foreach (var path in GetPermutations(Enumerable.Range(1, waypoints.Count - 1)))
            {
                int acc = 0;
                int last = 0;

                foreach(var vertex in path)
                {
                    acc += GetPathLength(last, vertex);
                    last = vertex;
                }

                if (cycle)
                    acc += GetPathLength(last, 0);

                shortest = Math.Min(shortest, acc);
            }

            return shortest;
        }

        private int GetPathLength(int from, int to)
        {
            if (paths.Any(p => p.From == from && p.To == to))
                return paths.First(p => p.From == from && p.To == to).Cost;
            else
                return paths.First(p => p.From == to && p.To == from).Cost;
        }


        private IEnumerable<IEnumerable<int>> GetPermutations(IEnumerable<int> numbers)
        {
            if (numbers.Count() == 0)
                yield return new List<int>();

            foreach (var num in numbers)
            {
                var list = new List<int> { num };
                foreach (var item in GetPermutations(numbers.Where(n => n != num)))
                    yield return list.Concat(item);
            }
        }

        private int FindShortestPath(Position from, Position to)
        {
            var queue = new Queue<State>();
            var visited = new HashSet<Position>();

            queue.Enqueue(new State { Cost = 0, Current = from });

            while (queue.Count > 0)
            {
                var state = queue.Dequeue();

                if (state.Current == to)
                    return state.Cost;

                foreach (var newState in FindNextStates(state))
                    if (visited.Add(newState.Current))
                        queue.Enqueue(newState);
            }

            return 0;
        }

        private IEnumerable<State> FindNextStates(State state)
        {
            if (IsValidPosition(state.Current.X - 1, state.Current.Y))
                yield return new State { Current = new Position { X = state.Current.X - 1, Y = state.Current.Y }, Cost = state.Cost + 1 };
            if (IsValidPosition(state.Current.X + 1, state.Current.Y))
                yield return new State { Current = new Position { X = state.Current.X + 1, Y = state.Current.Y }, Cost = state.Cost + 1 };
            if (IsValidPosition(state.Current.X, state.Current.Y - 1))
                yield return new State { Current = new Position { X = state.Current.X, Y = state.Current.Y - 1 }, Cost = state.Cost + 1 };
            if (IsValidPosition(state.Current.X, state.Current.Y + 1))
                yield return new State { Current = new Position { X = state.Current.X, Y = state.Current.Y + 1 }, Cost = state.Cost + 1 };
        }

        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && y >= 0 &&
                x < maze[0].Length && y < maze.Length &&
                maze[y][x] != '#';
        }
    }
}
