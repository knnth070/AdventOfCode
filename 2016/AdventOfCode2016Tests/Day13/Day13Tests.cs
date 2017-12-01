using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day13;

namespace AdventOfCode2016Tests.Day13
{
    public class Day13Tests
    {
        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 0, false)]
        [InlineData(8, 3, false)]
        [InlineData(6, 5, true)]
        public void DetermineOpenSpaceIsValid(int x, int y, bool expected)
        {
            var sut = new AdventOfCode2016.Day13.Day13(10);

            var actual = sut.IsOpenSpace(x, y);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShortestPathIsCorrect()
        {
            var sut = new AdventOfCode2016.Day13.Day13(10);
            var expected = 11;
            var actual = sut.FindShortestPath(7, 4);

            Assert.Equal(expected, actual);
        }
    }
}
