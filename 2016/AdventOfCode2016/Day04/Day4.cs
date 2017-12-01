using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day4
{
    public class Day4
    {
        private string name;

        public Day4(string name)
        {
            this.name = name;
        }

        public string CalculateChecksum()
        {
            var checksum = name
                            .ToCharArray()
                            .Where(x => char.IsLetter(x))
                            .GroupBy(x => x)
                            .OrderByDescending(x => x.Count())
                            .ThenBy(x => x.Key)
                            .Select(x => x.Key)
                            .Take(5)
                            .ToArray();

            return new string(checksum);
        }

        public string GetNamePart()
        {
            return name.Substring(0, name.IndexOf(GetSectorId().ToString()) - 1);
        }

        public int GetSectorId()
        {
            var sectorId = name
                            .ToCharArray()
                            .Where(x => char.IsNumber(x))
                            .ToArray();

            return int.Parse(new string(sectorId));
        }

        public string Decrypt()
        {
            var namePart = GetNamePart();
            var caesar = GetSectorId() % 26;

            var decrypted = namePart
                            .ToCharArray()
                            .Select(x => char.IsLetter(x) ? (char) (((x - 'a' + caesar) % 26) + 'a') : x)
                            .ToArray();

            return new string(decrypted).Replace('-', ' ');
        }

        public string GetSuggestedChecksum()
        {
            var openingBracketPosition = name.IndexOf('[');
            var closingBracketPosition = name.IndexOf(']');

            return name.Substring(openingBracketPosition + 1,
                                    closingBracketPosition - openingBracketPosition - 1);

        }

        public bool IsRealRoom()
        {
            return GetSuggestedChecksum().Equals(CalculateChecksum());
        }
    }
}
