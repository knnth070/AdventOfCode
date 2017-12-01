using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day2
{
    public class Day2Part1
    {
        private string instructions;

        public Day2Part1(string instructions)
        {
            this.instructions = instructions;
        }

        public string GetBathroomCode()
        {
            int position = 5;
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
                            position = MoveUp(position);
                            break;
                        case 'L':
                            position = MoveLeft(position);
                            break;
                        case 'R':
                            position = MoveRight(position);
                            break;
                        case 'D':
                            position = MoveDown(position);
                            break;
                    }
                }
                result.Append(position.ToString());
            }

            return result.ToString();
        }

        private int MoveUp(int position)
        {
            if (position > 3)
                position -= 3;

            return position;
        }

        private int MoveLeft(int position)
        {
            if (position != 1 && position != 4 && position != 7)
                position--;

            return position;
        }

        private int MoveRight(int position)
        {
            if (position != 3 && position != 6 && position != 9)
                position++;

            return position;
        }

        private int MoveDown(int position)
        {
            if (position < 7)
                position += 3;

            return position;
        }
    }
}
