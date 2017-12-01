using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day1;

namespace AdventOfCode2016Tests.Day1
{
    public class Day1Tests
    {
        [Theory]
        [InlineData("R2, L3", 5)]
        [InlineData("R2, R2, R2", 2)]
        [InlineData("R5, L5, R5, R3", 12)]
        public void ShortestPathLengthReturnsCorrectValue(string path, int expected)
        {
            var sut = new AdventOfCode2016.Day1.Day1(path);
            var actual = sut.GetShortestPathLength();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("R8, R4, R4, R8", 4)]
        public void FirstCrossingReturnsCorrectValue(string path, int expected) {
            var sut = new AdventOfCode2016.Day1.Day1(path);

            var actual = sut.GetShortestPathLength(stopAtFirstCrossing: true);

            Assert.Equal(expected, actual);

        }
    }
}
