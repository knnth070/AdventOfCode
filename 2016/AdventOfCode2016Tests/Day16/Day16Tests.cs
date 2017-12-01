using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day16;

namespace AdventOfCode2016Tests.Day16
{
    public class Day16Tests
    {
        [Theory]
        [InlineData("1", "100")]
        [InlineData("0", "001")]
        [InlineData("11111", "11111000000")]
        [InlineData("111100001010", "1111000010100101011110000")]
        public void SingleStepIsCorrect(string input, string expected)
        {
            var sut = new AdventOfCode2016.Day16.Day16(input);

            var actual = sut.Double(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FillReturnsCorrectLength()
        {
            var sut = new AdventOfCode2016.Day16.Day16("10000");
            var expected = "10000011110010000111";

            var actual = sut.FillToLength(20);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ChecksumIsCorrect()
        {
            var sut = new AdventOfCode2016.Day16.Day16("110010110100");
            var expected = "100";

            var actual = sut.Checksum("110010110100");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FillAndChecksumIsCorrect()
        {
            var sut = new AdventOfCode2016.Day16.Day16("10000");
            var expected = "01100";

            var actual = sut.Checksum(sut.FillToLength(20));

            Assert.Equal(expected, actual);
        }
    }
}
