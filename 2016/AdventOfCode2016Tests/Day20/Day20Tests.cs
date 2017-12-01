using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day20;

namespace AdventOfCode2016Tests.Day20
{
    public class Day20Tests
    {
        [Fact]
        public void BlacklistLeaves3And9()
        {
            var blacklist = new string[] { "5-8", "0-2", "4-7" };
            var sut = new AdventOfCode2016.Day20.Day20(9, blacklist);
            uint expected = 3;

            var actual = sut.GetFirstValidAddress();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BlacklistHasTwoValidAddresses()
        {
            var blacklist = new string[] { "5-8", "0-2", "4-7" };
            var sut = new AdventOfCode2016.Day20.Day20(9, blacklist);
            uint expected = 2;

            var actual = sut.NumberOfValidAddresses();

            Assert.Equal(expected, actual);
        }
    }
}
