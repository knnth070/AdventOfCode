using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day7;

namespace AdventOfCode2016Tests.Day7
{
    public class Day7Tests
    {
        [Theory]
        [InlineData("abba", true)]
        [InlineData("abab", false)]
        [InlineData("abba[mnop]qrst", true)]
        [InlineData("abcd[bddb]xyyx", false)]
        [InlineData("aaaa[qwer]tyui", false)]
        [InlineData("ioxxoj[asdfgh]zxcvbn", true)]
        [InlineData("abba[abba]abba", false)]
        [InlineData("abca[abba]abca", false)]
        [InlineData("abca[abca]abca[abba]abba", false)]
        public void IPAddressIsAbba(string address, bool expected)
        {
            var sut = new Day7Part1();

            var actual = sut.IPAddressIsAbba(address);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("a", 1)]
        [InlineData("a[b]", 2)]
        [InlineData("a[b]c", 3)]
        [InlineData("a[b]c[d]e", 5)]
        public void SplitAddressReturnsCorrectCount(string address, int expected)
        {
            var sut = new Day7Part2(address);

            var actual = sut.AddressParts.Count;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("aba[bab]xyz", true)]
        [InlineData("xyx[xyx]xyx", false)]
        [InlineData("aaa[kek]eke", true)]
        [InlineData("zazbz[bzb]cdb", true)]
        public void SupportsSslReturnsCorrectValue(string address, bool expected)
        {
            var sut = new Day7Part2(address);

            var actual = sut.SupportsSsl();

            Assert.Equal(expected, actual);
        }
    }
}
