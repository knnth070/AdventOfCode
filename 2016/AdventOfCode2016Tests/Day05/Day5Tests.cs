using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day5;

namespace AdventOfCode2016Tests.Day5
{
    public class Day5Tests
    {
        [Theory]
        [InlineData("abc", 1, 3231928, "1")]
        [InlineData("abc", 2, 5017306, "8f")]
        //[InlineData("abc", 8, 3231928, "18f47a30")] // very slow
        public void CharacterOfPasswordIsCorrect(string doorId, int numCharacters, int index, string expected)
        {
            var sut = new Day5Part1(doorId);

            var actual = sut.GetPassword(numCharacters, index);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("abc", 8, 1, 3231928, "_5______")]
        [InlineData("abc", 8, 2, 3231928, "_5__e___")]
        public void PasswordIsCorrect(string doorId, int length, int charactersToFill,
            int index, string expected) {
            var sut = new Day5Part2(doorId, length);

            var actual = sut.GetPassword(charactersToFill, index);

            Assert.Equal(expected, actual);
        }
    }
}
