using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day1
{
    internal enum Orientation { North, East, South, West };

    internal static class OrientationExtensions
    {
        public static Orientation TurnLeft(this Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.North:
                    return Orientation.West;
                case Orientation.East:
                    return Orientation.North;
                case Orientation.South:
                    return Orientation.East;
                case Orientation.West:
                    return Orientation.South;
            }

            throw new InvalidOperationException("orientation");
        }

        public static Orientation TurnRight(this Orientation orientation)
        {
            return orientation.TurnLeft().TurnLeft().TurnLeft();
        }

    }

}
