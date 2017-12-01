using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2016.Day24;
using Xunit;

namespace AdventOfCode2016Tests.Day24
{
    public class Day24Tests
    {
        [Fact]
        public void ExampleIsCorrect()
        {
            var input = new string[] { "###########", "#0.1.....2#", "#.#######.#", "#4.......3#", "###########" };
            var expected = 14;
            var sut = new AdventOfCode2016.Day24.Day24(input);
            var actual = sut.FindShortestPath();

            Assert.Equal(expected, actual);
        }
    }
}
