using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AdventOfCode2016.Day17;

namespace AdventOfCode2016Tests.Day17
{
    public class Day17Tests
    {
        [Fact]
        public void CannotMoveIntoWall()
        {
            var sut = new AdventOfCode2016.Day17.Day17("hijkl");

            Assert.False(sut.UpIsOpen(""));
            Assert.False(sut.LeftIsOpen(""));
            Assert.False(sut.RightIsOpen("RRR"));
            Assert.False(sut.DownIsOpen("DDD"));
        }

        [Theory]
        [InlineData("", false, true, false, false)]
        [InlineData("D", true, false, false, true)]
        [InlineData("DR", false, false, false, false)]
        [InlineData("DU", false, false, false, true)]
        [InlineData("DUR", false, false, false, false)]
        public void CorrectDoorsAreOpen(string path, bool upIsOpen, bool downIsOpen,
            bool leftIsOpen, bool rightIsOpen)
        {
            var sut = new AdventOfCode2016.Day17.Day17("hijkl");

            Assert.Equal(upIsOpen, sut.UpIsOpen(path));
            Assert.Equal(downIsOpen, sut.DownIsOpen(path));
            Assert.Equal(leftIsOpen, sut.LeftIsOpen(path));
            Assert.Equal(rightIsOpen, sut.RightIsOpen(path));
        }

        [Theory]
        [InlineData("hijkl", 0, 1, "D")]
        [InlineData("hijkl", 1, 0, "DUR")]
        [InlineData("ihgpwlah", 3, 3, "DDRRRD")]
        [InlineData("kglvqrro", 3, 3, "DDUDRLRRUDRD")]
        [InlineData("ulqzkmiv", 3, 3, "DRURDRUDDLLDLUURRDULRLDUUDDDRR")]
        public void ShortestPathIsCorrect(string passcode, int x, int y, string expected)
        {
            var sut = new AdventOfCode2016.Day17.Day17(passcode);
            var actual = sut.ShortestPath(x, y);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("ihgpwlah", 3, 3, 370)]
        [InlineData("kglvqrro", 3, 3, 492)]
        [InlineData("ulqzkmiv", 3, 3, 830)]
        public void LongestPathIsCorrect(string passcode, int x, int y, int expected)
        {
            var sut = new AdventOfCode2016.Day17.Day17(passcode);
            var actual = sut.LongestPath(x, y);

            Assert.Equal(expected, actual);
        }
    }
}
