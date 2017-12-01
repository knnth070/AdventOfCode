using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day4;

namespace AdventOfCode2016Tests.Day4
{
    public class Day4Tests
    {
        [Theory]
        [InlineData("aa-a", "a")]
        [InlineData("aa-b-a", "ab")]
        [InlineData("aa-abb-bbb-aa", "ab")]
        [InlineData("aa-abb-bbb-bb", "ba")]
        [InlineData("bbbb-c-aaaa", "abc")]
        [InlineData("abcdef", "abcde")]
        [InlineData("aabcccdef-[cabde]", "cabde")]
        [InlineData("aaaaa-bbb-z-y-x-123[abxyz]", "abxyz")]
        [InlineData("a-b-c-d-e-f-g-h-987[abcde]", "abcde")]
        [InlineData("not-a-real-room-404[oarel]", "oarel")]
        public void GetChecksumReturnsCorrectValue(string name, string expected)
        {
            var sut = new AdventOfCode2016.Day4.Day4(name);

            var actual = sut.CalculateChecksum();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("aaaaa-bbb-z-y-x-123[abxyz]", 123)]
        [InlineData("a-b-c-d-e-f-g-h-987[abcde]", 987)]
        [InlineData("not-a-real-room-404[oarel]", 404)]
        [InlineData("totally-real-room-200[decoy]", 200)]
        public void GetSectorIdReturnsCorrectValue(string name, int expected)
        {
            var sut = new AdventOfCode2016.Day4.Day4(name);

            var actual = sut.GetSectorId();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("aaaaa-bbb-z-y-x-123[abxyz]", "abxyz")]
        [InlineData("a-b-c-d-e-f-g-h-987[abcde]", "abcde")]
        [InlineData("not-a-real-room-404[oarel]", "oarel")]
        [InlineData("totally-real-room-200[decoy]", "decoy")]
        public void GetSuggestedChecksumReturnsCorrectValue(string name, string expected)
        {
            var sut = new AdventOfCode2016.Day4.Day4(name);

            var actual = sut.GetSuggestedChecksum();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("aaaaa-bbb-z-y-x-123[abxyz]", true)]
        [InlineData("a-b-c-d-e-f-g-h-987[abcde]", true)]
        [InlineData("not-a-real-room-404[oarel]", true)]
        [InlineData("totally-real-room-200[decoy]", false)]
        public void IsRealRoomReturnsCorrectValue(string name, bool expected)
        {
            var sut = new AdventOfCode2016.Day4.Day4(name);

            var actual = sut.IsRealRoom();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("aaaaa-bbb-z-y-x-123[abxyz]", "aaaaa-bbb-z-y-x")]
        [InlineData("a-b-c-d-e-f-g-h-987[abcde]", "a-b-c-d-e-f-g-h")]
        [InlineData("not-a-real-room-404[oarel]", "not-a-real-room")]
        [InlineData("totally-real-room-200[decoy]", "totally-real-room")]
        public void GetNamePartReturnsCorrectValue(string name, string expected) {
            var sut = new AdventOfCode2016.Day4.Day4(name);

            var actual = sut.GetNamePart();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("qzmt-zixmtkozy-ivhz-343", "very encrypted name")]
        public void DecryptReturnsCorrectvalue(string name, string expected) {
            var sut = new AdventOfCode2016.Day4.Day4(name);

            var actual = sut.Decrypt();

            Assert.Equal(expected, actual);
        }
    }
}
