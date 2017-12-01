using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day19
{
    class Elf
    {
        public int Number { get; set; }
        public Elf Previous { get; set; }
        public Elf Next { get; set; }
    }

    public class Day19
    {
        public int RemainingElfPuzzle1(int numberOfElves)
        {
            // Josephus problem
            var HighestPowerOfTwo = Math.Floor(Math.Log(numberOfElves) / Math.Log(2));
            return (numberOfElves - (int)Math.Pow(2, HighestPowerOfTwo)) * 2 + 1;
        }

        public int SlowRemainingElfPuzzle2(int numberOfElves)
        {
            var first = new Elf { Number = 1 };
            var current = first;

            for (int n = 2; n <= numberOfElves; n++)
            {
                var elf = new Elf { Number = n, Previous = current };
                current.Next = elf;
                current = elf;
            }
            current.Next = first;
            first.Previous = current;

            var remaining = numberOfElves;
            Elf lastElfStanding = null;

            for (var item = first; remaining > 1; item = item.Next)
            {
                Elf toRemove = item;

                for (int i = 0; i < Math.Floor((double) remaining / 2); i++)
                    toRemove = toRemove.Next;

                toRemove.Previous.Next = toRemove.Next;
                toRemove.Next.Previous = toRemove.Previous;
                remaining--;
                lastElfStanding = item;
            }

            return lastElfStanding.Number;
        }

        public int FastRemainingElfPuzzle2(int numberOfElves)
        {
            var exponent = Math.Floor(Math.Log(numberOfElves) / Math.Log(3));
            var highestPowerOfThree = (int)Math.Pow(3, exponent);
            if (numberOfElves == highestPowerOfThree) return numberOfElves;
            if (numberOfElves <= 2 * highestPowerOfThree) return numberOfElves - highestPowerOfThree;
            return 2 * (numberOfElves - 2 * highestPowerOfThree) + highestPowerOfThree;
        }
    }
}
