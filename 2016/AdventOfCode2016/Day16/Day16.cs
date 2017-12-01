using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day16
{
    public class Day16
    {
        private string input;

        public Day16(string input)
        {
            this.input = input;
        }

        public string Double(string value)
        {
            return value + "0" + ReverseInverse(value);
        }

        private string ReverseInverse(string value)
        {
            var sb = new StringBuilder();

            for (int i = value.Length - 1; i >= 0; i--)
            {
                if (value[i] == '0')
                    sb.Append("1");
                else
                    sb.Append("0");
            }

            return sb.ToString();
        }

        public string FillToLength(int length)
        {
            string output = input;

            while (output.Length < length)
                output = Double(output);

            return output.Substring(0, length);
        }

        public string Checksum(string value)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < value.Length; i += 2)
                if (value[i] == value[i + 1])
                    sb.Append("1");
                else
                    sb.Append("0");

            if (sb.Length % 2 == 0)
                return Checksum(sb.ToString());
            else
                return sb.ToString();
        }
    }
}
