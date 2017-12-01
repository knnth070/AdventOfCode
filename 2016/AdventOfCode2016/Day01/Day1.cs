using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AdventOfCode2016.Day1
{
    public class Day1
    {
        private List<Point> history;
        private Orientation orientation;
        private Point currentPosition;
        private List<string> steps;

        public Day1(string path)
        {
            history = new List<Point>();
            orientation = Orientation.North;
            currentPosition = new Point(0, 0);

            steps = path
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.ToUpper().Trim())
                    .ToList();
        }

        public int GetShortestPathLength(bool stopAtFirstCrossing = false)
        {
            bool foundCrossing = false;

            foreach (var item in steps)
            {
                switch (item.ToCharArray()[0])
                {
                    case 'L':
                        orientation = orientation.TurnLeft();
                        break;
                    case 'R':
                        orientation = orientation.TurnRight();
                        break;
                    default:
                        break;
                }

                int distance = int.Parse(item.Substring(1));

                for (int i = 0; i < distance && !(foundCrossing && stopAtFirstCrossing); i++)
                {
                    switch (orientation)
                    {
                        case Orientation.North:
                            currentPosition.Offset(0, 1);
                            break;
                        case Orientation.East:
                            currentPosition.Offset(1, 0);
                            break;
                        case Orientation.South:
                            currentPosition.Offset(0, -1);
                            break;
                        case Orientation.West:
                            currentPosition.Offset(-1, 0);
                            break;
                    }

                    if (history.Any(p => p.X == currentPosition.X && p.Y == currentPosition.Y))
                    {
                        foundCrossing = true;
                    }

                    history.Add(new Point(currentPosition.X, currentPosition.Y));
                }

                if (foundCrossing && stopAtFirstCrossing)
                {
                    break;
                }

            }

            return Math.Abs(currentPosition.X) + Math.Abs(currentPosition.Y);
        }
    }
}
