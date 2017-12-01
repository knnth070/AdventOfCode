using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AdventOfCode2016.Day2
{
    public class Day2Part2
    {
        private string instructions;

        private string[] keypad =
            {
                "  1  ",
                " 234 ",
                "56789",
                " ABC ",
                "  D  "
            };

        public Day2Part2(string instructions)
        {
            this.instructions = instructions;
        }

        public string GetBathroomCode()
        {
            var position = new Point(0, 2);
            var result = new StringBuilder();

            var lines = instructions
                        .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                foreach (var item in line.ToCharArray())
                {
                    switch (item)
                    {
                        case 'U':
                            position = Translate(position, 0, -1);
                            break;
                        case 'L':
                            position = Translate(position, -1, 0);
                            break;
                        case 'R':
                            position = Translate(position, 1, 0);
                            break;
                        case 'D':
                            position = Translate(position, 0, 1);
                            break;
                    }
                }
                result.Append(keypad[position.Y].Substring(position.X, 1));
            }

            return result.ToString();
        }

        private Point Translate(Point position, int dx, int dy)
        {
            var newPosition = new Point(position.X + dx, position.Y + dy);

            return IsValidPosition(newPosition) ? newPosition : position;
        }

        private bool IsValidPosition(Point p)
        {
            if (p.X < 0 || p.Y < 0 || p.Y >= keypad.Length || p.X >= keypad[p.Y].Length)
            {
                return false;
            }

            return !keypad[p.Y].Substring(p.X, 1).Equals(" ");
        }
    }
}