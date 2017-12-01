using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2016.Day10;
using Xunit;

namespace AdventOfCode2016Tests.Day10
{
    public class Day10Tests
    {
        [Fact]
        public void ValueGoesToCorrectBot()
        {
            string[] lines = { "values 5 goes to bot 2", "value 3 goes to bot 1" };
            var sut = new AdventOfCode2016.Day10.Day10(lines);

            var actual = sut.DestinationHasValue("bot2", value: 5) && sut.DestinationHasValue("bot1", value: 3);

            Assert.True(actual);
        }

        [Fact]
        public void BotsCanBeAssignedMultipleValues()
        {
            string[] lines = { "values 5 goes to bot 2", "value 2 goes to bot 2" };
            var sut = new AdventOfCode2016.Day10.Day10(lines);

            var actual = sut.DestinationHasValue("bot2", value: 5) && sut.DestinationHasValue("bot2", value: 2);

            Assert.True(actual);
        }

        [Fact]
        public void BotPassesCorrectValue()
        {
            string[] lines = {"value 5 goes to bot 2",
                "bot 2 gives low to bot 1 and high to bot 0",
                "value 3 goes to bot 1",
                "bot 1 gives low to output 1 and high to bot 0",
                "bot 0 gives low to output 2 and high to output 0",
                "value 2 goes to bot 2" };

            var sut = new AdventOfCode2016.Day10.Day10(lines);

            sut.Process();

            Assert.True(sut.DestinationHasValue("output1", value: 2));
            Assert.True(sut.DestinationHasValue("output2", value: 3));
            Assert.True(sut.DestinationHasValue("output0", value: 5));

            Assert.False(sut.DestinationHasValue("bot2", value: 2));
            Assert.False(sut.DestinationHasValue("bot1", value: 3));
            Assert.False(sut.DestinationHasValue("bot2", value: 5));


        }
    }
}
