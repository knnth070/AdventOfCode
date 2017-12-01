using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day2;

namespace AdventOfCode2016Tests.Day2
{
    public class Day2Tests
    {
        [Theory]
        [InlineData("ULL\nRRDDD\nLURDL\nUUUUD", "1985")]
        [InlineData("L", "4")]
        [InlineData("R", "6")]
        [InlineData("U", "2")]
        [InlineData("D", "8")]
        [InlineData("ULL", "1")]
        [InlineData("ULL\nRRDDD", "19")]
        public void Part1GetBathroomCodeReturnsCorrectValue(string instructions, string expected)
        {
            var sut = new Day2Part1(instructions);

            var actual = sut.GetBathroomCode();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("U", "5")]
        [InlineData("D", "5")]
        [InlineData("L", "5")]
        [InlineData("R", "6")]
        [InlineData("RRUUUUU", "1")]
        [InlineData("RRDDDDD", "D")]
        [InlineData("RRRRR", "9")]
        [InlineData("U\nR", "56")]
        [InlineData("ULL\nRRDDD\nLURDL\nUUUUD", "5DB3")]
        public void Part2GetBathroomCodeReturnsCorrectValue(string instructions, string expected) {
            var sut = new Day2Part2(instructions);

            var actual = sut.GetBathroomCode();
            Assert.Equal(expected, actual);
        }
    }
}
