using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day3;

namespace AdventOfCode2016Tests.Day3
{
    public class Day3Tests
    {
        [Theory]
        [InlineData("5 10 25", false)]
        [InlineData("5 6 7", true)]
        public void IsValidTriangleReturnsCorrectValue(string triangle, bool expected)
        {
            var sut = new AdventOfCode2016.Day3.Day3();
            var actual = sut.IsValidTriangle(triangle);

            Assert.Equal(expected, actual);
        }
    }
}
