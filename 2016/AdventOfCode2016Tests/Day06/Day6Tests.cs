using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day6;

namespace AdventOfCode2016Tests.Day6
{
    public class Day6Tests
    {
        [Theory]
        [InlineData("ba\nca\naa\nbb\nca\nbd", "ba")]
        [InlineData("eedadn\ndrvtee\neandsr\nraavrd\natevrs\ntsrnev\nsdttsa\nrasrtv\nnssdts\nntnada\nsvetve\ntesnvt\nvntsnd\nvrdear\ndvrsen\nenarar", "easter")]
        public void GetMostFrequentLetterReturnsCorrectValue(string sequence, string expected)
        {
            var sut = new AdventOfCode2016.Day6.Day6(sequence);

            var actual = sut.GetRepeatedLetters(RepetitionCode.MostFrequent);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("b\na\nb", "a")]
        [InlineData("eedadn\ndrvtee\neandsr\nraavrd\natevrs\ntsrnev\nsdttsa\nrasrtv\nnssdts\nntnada\nsvetve\ntesnvt\nvntsnd\nvrdear\ndvrsen\nenarar", "advent")]
        public void GetLeastFrequentLetterReturnsCorrectValue(string sequence, string expected)
        {
            var sut = new AdventOfCode2016.Day6.Day6(sequence);

            var actual = sut.GetRepeatedLetters(RepetitionCode.LeastFrequent);

            Assert.Equal(expected, actual);
        }
    }
}
