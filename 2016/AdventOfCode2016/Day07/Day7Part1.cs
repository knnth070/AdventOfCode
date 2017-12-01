using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day7
{
    public class Day7Part1
    {
        public bool IPAddressIsAbba(string address)
        {
            var parts = address.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);

            return parts.Any(x => AddressPartIsAbba(x)) && !AddressPartInBracketsIsAbba(address);
        }

        private bool AddressPartIsAbba(string part)
        {
            for (int i = 0; i < part.Length - 3; i++)
            {
                if (part[i] == part[i + 3] &&
                    part[i + 1] == part[i + 2] &&
                    part[i] != part[i + 1])
                    return true;
            }

            return false;
        }

        private bool AddressPartInBracketsIsAbba(string address)
        {
            int openingBracket, closingBracket;

            openingBracket = address.IndexOf('[');

            while (openingBracket >= 0)
            {
                closingBracket = address.IndexOf(']', openingBracket);

                string part = address.Substring(openingBracket + 1, closingBracket - openingBracket - 1);

                if (AddressPartIsAbba(part))
                {
                    return true;
                }

                openingBracket = address.IndexOf('[', closingBracket);
            }

            return false;
        }
    }
}
