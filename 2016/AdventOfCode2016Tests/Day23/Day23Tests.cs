using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2016.Day23;
using Xunit;

namespace AdventOfCode2016Tests.Day23
{
    public class Day23Tests
    {
        [Fact]
        public void TglTogglesInstruction()
        {
            var instructions = new string[] { "cpy 2 a", "tgl a", "tgl a", "tgl a" };
            var sut = new AdventOfCode2016.Day23.Day23(instructions);
            var expected = 3;

            sut.Run();
            var actual = sut.RegisterA;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Day23ExampleIsCorrect()
        {
            var instructions = new string[] { "cpy 2 a", "tgl a", "tgl a", "tgl a",
                                              "cpy 1 a", "dec a", "dec a" };
            var sut = new AdventOfCode2016.Day23.Day23(instructions);
            var expected = 3;

            sut.Run();
            var actual = sut.RegisterA;

            Assert.Equal(expected, actual);
        }
    }
}
