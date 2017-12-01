using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day3
{
    public class Day3
    {
        public bool IsValidTriangle(string triangle)
        {
            var sides = triangle
                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => int.Parse(x))
                        .ToArray();

            return sides[0] + sides[1] > sides[2] &&
                    sides[0] + sides[2] > sides[1] &&
                    sides[1] + sides[2] > sides[0]; 
        }
    }
}
