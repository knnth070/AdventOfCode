using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day21;

namespace AdventOfCode2016Tests.Day21
{
    public class Day21Tests
    {
        [Theory]
        [InlineData("swap position 4 with position 0", "abcde", "ebcda")]
        [InlineData("swap letter d with letter b", "ebcda", "edcba")]
        [InlineData("reverse positions 0 through 4", "edcba", "abcde")]
        [InlineData("rotate left 1 step", "abcde", "bcdea")]
        [InlineData("rotate left 2 steps", "abcde", "cdeab")]
        [InlineData("rotate right 1 step", "abcde", "eabcd")]
        [InlineData("rotate right 2 steps", "abcde", "deabc")]
        [InlineData("move position 1 to position 4", "bcdea", "bdeac")]
        [InlineData("rotate based on position of letter b", "abdec", "ecabd")]
        [InlineData("rotate based on position of letter d", "ecabd", "decab")]
        public void ScrambleScramblesCorrectly(string instruction, string password, string expected)
        {
            var instructions = new string[] { instruction };
            var sut = new AdventOfCode2016.Day21.Day21(instructions);

            var actual = sut.Scramble(password);

            Assert.Equal(expected, actual);
        }
    }
}
