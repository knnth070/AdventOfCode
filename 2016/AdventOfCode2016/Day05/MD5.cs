using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day5
{
    public static class MD5
    {
        public static string GetMD5Hash(string input)
        {
            var md5 = System.Security.Cryptography.MD5.Create();

            var output = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            var sb = new StringBuilder();

            for (int j = 0; j < output.Length; j++)
            {
                sb.Append(output[j].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
