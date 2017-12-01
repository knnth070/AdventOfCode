using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day20
{
    internal class Range
    {
        public uint Begin { get; set; }
        public uint End { get; set; }

        public Range(string range)
        {
            var r = range.Split('-');
            Begin = Convert.ToUInt32(r[0]);
            End = Convert.ToUInt32(r[1]);
        }
    }

    internal class RangeComparer : IComparer<Range>
    {
        public int Compare(Range x, Range y)
        {
            if (x.Begin < y.Begin) return -1;
            if (x.Begin == y.Begin) return 0;
            return 1;
        }
    }

    public class Day20
    {
        private uint max;
        private List<Range> blacklist;

        public Day20(uint max, string[] input)
        {
            this.max = max;
            blacklist = new List<Range>();

            foreach (var item in input)
                blacklist.Add(new Range(item));

            blacklist.Sort(new RangeComparer());

            for (int i = 0; i < blacklist.Count - 1; i++)
            {
                if (blacklist[i + 1].Begin - 1 <= blacklist[i].End)
                {
                    blacklist[i].End = Math.Max(blacklist[i].End, blacklist[i + 1].End);
                    blacklist.RemoveAt(i + 1);
                    i--;
                }
            }
        }

        public uint GetFirstValidAddress()
        {
            return blacklist.First().End + 1;
        }

        public uint NumberOfValidAddresses()
        {
            uint count = 0;
            uint lastEnd = 0;

            foreach (var item in blacklist)
            {
                if (item.Begin != lastEnd)
                    count += item.Begin - lastEnd - 1;
                lastEnd = item.End;
            }

            count += max - lastEnd;

            return count;
        }
    }
}
