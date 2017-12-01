using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day18;

namespace AdventOfCode2016Tests.Day18
{
    public class Day18Tests
    {
        [Theory]
        [InlineData("..^^.", ".^^^^")]
        [InlineData(".^^^^", "^^..^")]
        [InlineData(".^^.^.^^^^", "^^^...^..^")]
        public void GenerateRowsIsCorrect(string initialRow, string expected)
        {
            var sut = new AdventOfCode2016.Day18.Day18(initialRow);

            var actual = sut.GenerateRows(2).Skip(1).First();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CountSafeTilesIsCorrect()
        {
            var sut = new AdventOfCode2016.Day18.Day18(".^^.^.^^^^");
            var expected = 38;

            var actual = sut.CountSafeTiles(10);

            Assert.Equal(expected, actual);
        }
    }
}
