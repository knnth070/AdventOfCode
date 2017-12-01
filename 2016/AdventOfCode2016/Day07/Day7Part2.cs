using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2016.Day7
{
    public class Day7Part2
    {
        private readonly string address;

        public List<string> AddressParts { get; set; }

        public Day7Part2(string address)
        {
            this.address = address;

            AddressParts = new List<string>();

            AddressParts.AddRange(Split());
        }

        public bool SupportsSsl()
        {
            var babList = AddressParts
                            .Where(x => !x.StartsWith("["))
                            .GetAbaList()
                            .Select(x => x.BabFromAba());

            return babList.Any(bab =>
                            AddressParts
                            .Where(x => x.StartsWith("["))
                            .Any(x => x.Contains(bab))
                            );
        }

        private IEnumerable<string> Split()
        {
            int openingBracket, closingBracket;


            if (string.IsNullOrWhiteSpace(address))
            {
                yield break;
            }

            openingBracket = address.IndexOf('[');

            if (openingBracket > 0)
            {
                yield return address.Substring(0, openingBracket);
            }
            else
            {
                yield return address;
            }

            while (openingBracket >= 0)
            {
                closingBracket = address.IndexOf(']', openingBracket);

                yield return address.Substring(openingBracket, closingBracket - openingBracket + 1);

                openingBracket = address.IndexOf('[', closingBracket);

                if (openingBracket > 0)
                {
                    yield return address.Substring(closingBracket + 1, openingBracket - closingBracket - 1);
                }
                else if (closingBracket + 1 < address.Length)
                {
                    yield return address.Substring(closingBracket + 1, address.Length - closingBracket - 1);
                }
            }
        }
    }

    internal static class AbaExtensions
    {
        internal static IEnumerable<string> GetAbaList(this IEnumerable<string> addressPart)
        {
            foreach (var item in addressPart)
            {
                foreach(var aba in GetAbaListFromPart(item))
                {
                    yield return aba;
                }
            }
        }

        private static IEnumerable<string> GetAbaListFromPart(string part)
        {
            for (int i = 0; i < part.Length - 2; i++)
            {
                if (part[i] == part[i + 2] &&
                    part[i] != part[i + 1])
                {
                    yield return part.Substring(i, 3);
                }
            }
        }

        internal static string BabFromAba(this string aba)
        {
            return new string(new char[] { aba[1], aba[0], aba[1] });
        }
    }
}