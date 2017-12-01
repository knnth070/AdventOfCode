using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day6
{
    public enum RepetitionCode { MostFrequent, LeastFrequent };

    public class Day6
    {
        private readonly string[] sequence;

        public Day6(string sequence)
        {
            this.sequence = sequence.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string GetRepeatedLetters(RepetitionCode repetitionCode)
        {
            var sb = new StringBuilder();
            var length = sequence[0].Length;

            for (int i = 0; i < length; i++)
            {
                var repeatedLetter = sequence
                                    .Select(c => c[i])
                                    .GroupBy(c => c)
                                    .LetterOrder(repetitionCode)
                                    .Select(c => c.Key)
                                    .First()
                                    .ToString();

                sb.Append(repeatedLetter);
            }

            return sb.ToString();
        }

    }

    internal static class LetterOrderExtension
    {
        public static IOrderedEnumerable<IGrouping<char, char>> LetterOrder(this IEnumerable<IGrouping<char, char>> grouping, RepetitionCode repetitionCode)
        {
            IOrderedEnumerable<IGrouping<char, char>> orderedLetters = null;

            switch (repetitionCode)
            {
                case RepetitionCode.MostFrequent:
                    orderedLetters = grouping.OrderByDescending(c => c.Count());
                    break;
                case RepetitionCode.LeastFrequent:
                    orderedLetters = grouping.OrderBy(c => c.Count());
                    break;
            }

            return orderedLetters;
        }

    }
}
