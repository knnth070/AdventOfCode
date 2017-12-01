using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day18
{
    public class Day18
    {
        private string initialRow;

        public Day18(string initialRow)
        {
            this.initialRow = initialRow;
        }

        public IEnumerable<string> GenerateRows(int count)
        {
            string lastRow = initialRow;

            yield return initialRow;

            for (int i = 0; i < count - 1; i++)
            {
                var sb = new StringBuilder();
                for (int j = 0; j < lastRow.Length; j++)
                {
                    char left = '.', right = '.';

                    if (j > 0) left = lastRow[j - 1];
                    if (j < lastRow.Length - 1) right = lastRow[j + 1];

                    if (left != right)
                        sb.Append('^');
                    else
                        sb.Append('.');
                }
                lastRow = sb.ToString();
                yield return lastRow;
            }
        }

        public int CountSafeTiles(int rows)
        {
            return GenerateRows(rows)
                .Aggregate(0, (a, b) => a + b.ToCharArray()
                                            .Where(x => x == '.')
                                            .Count());
        }
    }
}
