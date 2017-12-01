using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day12;

namespace AdventOfCode2016Tests.Day12
{
    public class Day12Tests
    {
        [Fact]
        public void CpyCopiesValueToRegister()
        {
            var instructions = new string[] { "cpy 41 a" };
            var sut = new AdventOfCode2016.Day12.Day12(instructions);
            var expected = 41;

            sut.Run();
            var actual = sut.RegisterA;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IncIncreasesValueInRegister()
        {
            var instructions = new string[] { "inc a", "inc a" };
            var sut = new AdventOfCode2016.Day12.Day12(instructions);
            var expected = 2;

            sut.Run();
            var actual = sut.RegisterA;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DecDecreasesValueInRegister()
        {
            var instructions = new string[] { "cpy 43 a", "dec a" };
            var sut = new AdventOfCode2016.Day12.Day12(instructions);
            var expected = 42;

            sut.Run();
            var actual = sut.RegisterA;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void JnzPerformsPositiveJump()
        {
            var instructions = new string[] { "cpy 1 a", "jnz a 2", "inc b", "inc b" };
            var sut = new AdventOfCode2016.Day12.Day12(instructions);
            var expected = 1;

            sut.Run();
            var actual = sut.RegisterB;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void JnzPerformsNegativeJump()
        {
            var instructions = new string[] { "cpy 2 a", "dec a", "inc b", "jnz a -2" };
            var sut = new AdventOfCode2016.Day12.Day12(instructions);
            var expected = 2;

            sut.Run();
            var actual = sut.RegisterB;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void JnzSupportsIntegers()
        {
            var instructions = new string[] { "jnz 1 2", "inc a", "inc a" };
            var sut = new AdventOfCode2016.Day12.Day12(instructions);
            var expected = 1;

            sut.Run();
            var actual = sut.RegisterA;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CpySupportsRegisters()
        {
            var instructions = new string[] { "cpy 42 a", "cpy a b" };
            var sut = new AdventOfCode2016.Day12.Day12(instructions);
            var expected = 42;

            sut.Run();
            var actual = sut.RegisterB;

            Assert.Equal(expected, actual);
        }
    }
}
