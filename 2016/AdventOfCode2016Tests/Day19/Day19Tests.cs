using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day19;

namespace AdventOfCode2016Tests.Day19
{
    public class Day19Tests
    {
        [Fact]
        public void ThirdElfGetsThePresents()
        {
            var sut = new AdventOfCode2016.Day19.Day19();
            var expected = 3;

            var actual = sut.RemainingElfPuzzle1(5);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SecondElfGetsThePresents()
        {
            var sut = new AdventOfCode2016.Day19.Day19();
            var expected = 2;

            var actual = sut.SlowRemainingElfPuzzle2(5);

            Assert.Equal(expected, actual);
        }
    }
}
