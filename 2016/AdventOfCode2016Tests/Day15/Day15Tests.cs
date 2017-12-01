using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day15;

namespace AdventOfCode2016Tests.Day15
{
    public class Day15Tests
    {
        [Fact]
        public void CapsuleFallsThroughDisc()
        {
            var disc = new Disc { Positions = 5, Current = 4 };
            var expected = true;

            var sut = new AdventOfCode2016.Day15.Day15(time: 0, discs: disc);

            var actual = sut.CapsuleFallsThrough();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(5, true)]
        public void CapsuleBouncesOffSecondDisc(int time, bool expected)
        {
            var discs = new Disc[] {
                new Disc { Positions = 5, Current = 4 },
                new Disc { Positions = 2, Current = 1 }
            };

            var sut = new AdventOfCode2016.Day15.Day15(time, discs);

            var actual = sut.CapsuleFallsThrough();

            Assert.Equal(expected, actual);
        }
    }
}
