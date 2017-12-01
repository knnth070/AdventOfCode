using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day9;

namespace AdventOfCode2016Tests.Day9
{
    public class Day9Tests
    {
        [Fact]
        public void NoMarkerDecompressesToSelf()
        {
            var input = "ADVENT";
            var sut = new AdventOfCode2016.Day9.Day9();

            var expected = input.Length;
            var actual = sut.Decompress(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("A(1x5)BC", "ABBBBBC", false)]
        [InlineData("(3x3)XYZ", "XYZXYZXYZ", false)]
        [InlineData("A(2x2)BCD(2x2)EFG", "ABCBCDEFEFG", false)]
        [InlineData("(6x1)(1x3)A", "(1x3)A", false)]
        [InlineData("X(8x2)(3x3)ABCY","X(3x3)ABC(3x3)ABCY", false)]
        [InlineData("(6x1)(1x3)A", "AAA", true)]
        [InlineData("X(8x2)(3x3)ABCY", "XABCABCABCABCABCABCY", true)]
        public void MarkerIsProcessedCorrectly(string input, string expected, bool recurse)
        {
            var sut = new AdventOfCode2016.Day9.Day9();

            var actual = sut.Decompress(input, recurse);

            Assert.Equal(expected.Length, actual);
        }

        [Theory]
        [InlineData("(27x12)(20x12)(13x14)(7x10)(1x12)A", 241920)]
        [InlineData("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", 445)]
        public void MultipleMarkersAreProcessedCorrectly(string input, long expected)
        {
            var sut = new AdventOfCode2016.Day9.Day9();

            var actual = sut.Decompress(input, recurse: true);

            Assert.Equal(expected, actual);
        }

    }
}
