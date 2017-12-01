using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day5
{
    public class Day5Part2
    {
        private readonly string doorId;
        private readonly int length;

        public Day5Part2(string doorId, int length)
        {
            this.doorId = doorId;
            this.length = length;
        }

        public string GetPassword(int charactersToFill, int index = 0)
        {
            var password = new string('_', length).ToCharArray();

            for (int i = index; charactersToFill > 0; i++)
            {
                var output = MD5.GetMD5Hash($"{doorId}{i}");

                if (output.StartsWith("00000"))
                {
                    var positionChar = output.Substring(5, 1);
                    if (char.IsDigit(positionChar[0]))
                    {
                        var position = int.Parse(positionChar);
                        var value = output.Substring(6, 1);

                        if (position < length && password[position] == '_')
                        {
                            charactersToFill--;
                            password[position] = value[0];
                        }
                    }
                }
            }

            return new string(password);
        }
    }
}
