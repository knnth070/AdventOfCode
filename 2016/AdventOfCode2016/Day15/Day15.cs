using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day15
{
    public struct Disc
    {
        public int Positions { get; set; }
        public int Current { get; set; }
    }

    public class Day15
    {
        private Disc[] discs;
        private int time;

        public Day15(int time, params Disc[] discs)
        {
            this.discs = discs;
            this.time = time;

            for (int i = 0; i < time; i++)
                Tick();
        }

        public bool CapsuleFallsThrough()
        {
            for (int i = 0; i < discs.Length; i++)
            {
                Tick();
                if (discs[i].Current != 0)
                    return false;
            }

            return true;
        }

        public int FindTimeSlot()
        {
            var size = discs.Aggregate(1, (a, b) => a * b.Positions);
            var solutions = new int[size + 1];

            for (int s = 1; s < size; s++) solutions[s] = s;

            for (int i = 0; i < discs.Length; i++)
            {
                for (int j = 1; j <= size; j++)
                {
                    if ((j + discs[i].Current + (i + 1)) % discs[i].Positions != 0)
                    {
                        solutions[j] = 0;
                    }
                }
            }

            return solutions.FirstOrDefault(s => s != 0);
        }

        private void Tick()
        {
            time++;
            for (int i = 0; i < discs.Length; i++)
                discs[i].Current = (discs[i].Current + 1) % discs[i].Positions;
        }
    }
}
