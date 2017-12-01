using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day11
{
    public class Day11
    {
        struct Pair
        {
            public int GeneratorFloor { get; private set; }
            public int MicrochipFloor { get; private set; }

            public Pair(int generatorFloor, int microchipFloor)
            {
                this.GeneratorFloor = generatorFloor;
                this.MicrochipFloor = microchipFloor;
            }

            public override string ToString()
            {
                return $"{GeneratorFloor}, {MicrochipFloor}";
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Pair)) return false;

                var other = (Pair) obj;

                return GeneratorFloor == other.GeneratorFloor &&
                    MicrochipFloor == other.MicrochipFloor;
            }

            public override int GetHashCode()
            {
                return GeneratorFloor << 5 + MicrochipFloor;
            }
        }

        struct State
        {
            public int Elevator { get; set; }
            public Pair[] Pairs { get; set; }

            public override string ToString()
            {
                var sb = new StringBuilder();

                sb.Append($"{Elevator}");
                foreach (var p in Pairs.OrderBy(x => x.GeneratorFloor).ThenBy(x => x.MicrochipFloor))
                    sb.Append($"|{p}");
                return sb.ToString();
            }

            public override bool Equals(object obj)
            {
                return ToString() == obj.ToString();
            }

            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }
        }

        private State start, goal;

        public Day11()
        {
            start = new State
            {
                Elevator = 1,
                Pairs = new Pair[7]
                    {
                        new Pair(1, 1),
                        new Pair(2, 3),
                        new Pair(2, 3),
                        new Pair(2, 3),
                        new Pair(1, 1),
                        new Pair(1, 1),
                        new Pair(2, 3)
                    }
            };

            goal = new State
            {
                Elevator = 4,
                Pairs = new Pair[7]
                    {
                        new Pair(4, 4),
                        new Pair(4, 4),
                        new Pair(4, 4),
                        new Pair(4, 4),
                        new Pair(4, 4),
                        new Pair(4, 4),
                        new Pair(4, 4)
                    }
            };
        }

        public int Process()
        {
            var queue = new Queue<Tuple<State, int>>();
            var visited = new List<string>();

            queue.Enqueue(Tuple.Create(start, 0));

            while (queue.Count() > 0)
            {
                var current = queue.Dequeue();

                if (current.Item1.Equals(goal))
                    return current.Item2;

                foreach (var item in NextStates(current.Item1))
                {
                    if (visited.Contains(item.ToString())) continue;
                    visited.Add(item.ToString());
                    queue.Enqueue(Tuple.Create(item, current.Item2 + 1));
                }
            }

            Console.WriteLine("Queue empty, terminating.");
            return 0;
        }

        private bool IsValidState(State state)
        {
            for (int i = 0; i < state.Pairs.Length; i++)
                if (state.Pairs[i].GeneratorFloor != state.Pairs[i].MicrochipFloor &&
                    state.Pairs.Any(x => x.GeneratorFloor == state.Pairs[i].MicrochipFloor))
                    return false;

            return true;
        }

        private IEnumerable<State> NextStates(State current)
        {
            if (current.Elevator < 4)
            {
                // first item up
                foreach (var item in MoveItems(current, current.Elevator, current.Elevator + 1))
                {
                    var tmp = item;
                    tmp.Elevator = current.Elevator + 1;
                    if (IsValidState(tmp)) yield return tmp;

                    // second item up
                    foreach (var secondItem in MoveItems(item, current.Elevator, current.Elevator + 1))
                    {
                        var tmp2 = secondItem;
                        tmp2.Elevator = current.Elevator + 1;
                        if (IsValidState(tmp2)) yield return tmp2;
                    }

                }
            }

            if (current.Elevator > 1)
            {
                // first item down
                foreach (var item in MoveItems(current, current.Elevator, current.Elevator - 1))
                {
                    var tmp = item;
                    tmp.Elevator = current.Elevator - 1;
                    if (IsValidState(tmp)) yield return tmp;

                    // second item down
                    foreach (var secondItem in MoveItems(item, current.Elevator, current.Elevator - 1))
                    {
                        var tmp2 = secondItem;
                        tmp2.Elevator = current.Elevator - 1;
                        if (IsValidState(tmp2)) yield return tmp2;
                    }
                }

            }
        }

        private IEnumerable<State> MoveItems(State state, int oldFloor, int newFloor)
        {
            State newState;

            for (int i = 0; i < state.Pairs.Length; i++)
            {
                if (state.Pairs[i].GeneratorFloor == oldFloor)
                {
                    newState = state;
                    newState.Pairs = (Pair[])state.Pairs.Clone();
                    newState.Pairs[i] = new Pair(newFloor, state.Pairs[i].MicrochipFloor);
                    yield return newState;
                }

                if (state.Pairs[i].MicrochipFloor == oldFloor)
                {
                    newState = state;
                    newState.Pairs = (Pair[])state.Pairs.Clone();
                    newState.Pairs[i] = new Pair(state.Pairs[i].GeneratorFloor, newFloor);
                    yield return newState;
                }
            }
        }
    }
}
