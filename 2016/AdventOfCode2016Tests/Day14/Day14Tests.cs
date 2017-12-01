using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day14;

namespace AdventOfCode2016Tests.Day14
{
    public class Day14Tests
    {
        [Fact]
        public void HashContainsTriplets()
        {
            var sut = new AdventOfCode2016.Day14.Day14("abc");
            var hash = sut.GetHash(18);

            var actual = sut.GetTriplet(hash);

            Assert.NotEqual(string.Empty, actual);
        }

        [Fact]
        public void FirstHashesContainNoTriplets()
        {
            var sut = new AdventOfCode2016.Day14.Day14("abc");

            for (int i = 0; i < 18; i++)
            {
                var hash = sut.GetHash(i);
                var expected = string.Empty;
                var actual = sut.GetTriplet(hash);

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [InlineData(18, false)]
        [InlineData(39, true)]
        [InlineData(92, true)]
        [InlineData(22728, true)]
        public void HashIsValidKey(int index, bool expected)
        {
            var sut = new AdventOfCode2016.Day14.Day14("abc");

            var actual = sut.IsValidKey(index);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void StretchedHashIsCorrect()
        {
            var sut = new AdventOfCode2016.Day14.Day14("abc", 2016);
            var expected = "a107ff634856bb300138cac6568c0f24";

            var actual = sut.GetHash(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FirstTripleIsNotValidKeyStretched()
        {
            var sut = new AdventOfCode2016.Day14.Day14("abc", 2016);
            var hash = sut.GetHash(5);

            Assert.Equal("222", sut.GetTriplet(hash));
            Assert.False(sut.IsValidKey(5));
        }

        [Fact]
        public void SecondTripleIsValidKeyStretched()
        {
            var sut = new AdventOfCode2016.Day14.Day14("abc", 2016);
            var hash = sut.GetHash(10);
            Assert.Equal("eee", sut.GetTriplet(hash));
            Assert.True(sut.IsValidKey(10));
        }

        [Fact]
        public void TripleAtEndOfHashIsValid()
        {
            var sut = new AdventOfCode2016.Day14.Day14("abc");
            var expected = "fff";

            var actual  = sut.GetTriplet("22df6e9378c3c53abed6d3508b6285fff");

            Assert.Equal(expected, actual);
        }
    }
}
