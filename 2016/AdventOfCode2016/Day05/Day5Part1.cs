using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day5
{
    public class Day5Part1
    {
        private string doorId;

        public Day5Part1(string doorId)
        {
            this.doorId = doorId;
        }

        public string GetPassword(int numCharacters, int index = 0)
        {
            var password = new StringBuilder();

            for (int i = index; numCharacters > 0; i++)
            {
                var output = MD5.GetMD5Hash($"{doorId}{i}");

                if (output.StartsWith("00000"))
                {
                    numCharacters--;
                    password.Append(output.Substring(5, 1));
                }
            }

            return password.ToString();
        }
    }
}
