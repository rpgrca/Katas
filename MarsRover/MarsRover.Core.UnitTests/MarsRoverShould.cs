using System;
using Xunit;

namespace MarsRover.Core.UnitTests
{
    public class MarsRoverShould
    {
        [Fact]
        public void Test1()
        {
            var marsRover = new MarsRover(1, 1, "N");
            marsRover.Process("");
            Assert.True(marsRover.IsAtHeadingTo(1, 1, "N"));
        }

        [Fact]
        public void Test2()
        {
            var marsRover = new MarsRover(1, 1, "N");

            marsRover.Process("f");

            Assert.False(marsRover.IsAtHeadingTo(1, 1, "N"));
            Assert.True(marsRover.IsAtHeadingTo(1, 2, "N"));
        }

        [Fact]
        public void Test3()
        {
            var marsRover = new MarsRover(1, 1, "N");

            marsRover.Process("ff");

            Assert.True(marsRover.IsAtHeadingTo(1, 3, "N"));
        }

        [Fact]
        public void Test4()
        {
            var marsRover = new MarsRover(1, 1, "N");

            marsRover.Process("b");

            Assert.True(marsRover.IsAtHeadingTo(1, 0, "N"));
        }

        [Fact]
        public void Test5()
        {
            var marsRover = new MarsRover(1, 1, "N");

            marsRover.Process("l");

            Assert.False(marsRover.IsAtHeadingTo(1, 1, "N"));
            Assert.True(marsRover.IsAtHeadingTo(1, 1, "W"));
        }

        [Fact]
        public void Test6()
        {
            var marsRover = new MarsRover(1, 1, "N");

            marsRover.Process("r");

            Assert.False(marsRover.IsAtHeadingTo(1, 1, "N"));
            Assert.True(marsRover.IsAtHeadingTo(1, 1, "E"));
        }

        [Fact]
        public void Test7()
        {
            var marsRover = new MarsRover(1, 1, "N");

            var exception = Assert.Throws<ArgumentException>(() => marsRover.Process("x"));
            Assert.Equal(MarsRover.INVALID_COMMAND, exception.Message);
        }

        [Fact]
        public void Test71()
        {
            var marsRover = new MarsRover(1, 1, "N");

            Assert.Throws<ArgumentException>(() => marsRover.Process("x"));
            Assert.True(marsRover.IsAtHeadingTo(1, 1, "N"));
        }

        [Fact]
        public void Test8()
        {
            var marsRover = new MarsRover(1, 1, "E");

            marsRover.Process("f");

            Assert.True(marsRover.IsAtHeadingTo(2, 1, "E"));
        }

        [Fact]
        public void Test9()
        {
            var marsRover = new MarsRover(1, 1, "E");

            marsRover.Process("br");

            Assert.True(marsRover.IsAtHeadingTo(0, 1, "S"));
        }

        [Fact]
        public void Test10()
        {
            var marsRover = new MarsRover(1, 1, "E");

            marsRover.Process("l");

            Assert.True(marsRover.IsAtHeadingTo(1, 1, "N"));
        }

        [Fact]
        public void Test11()
        {
            var marsRover = new MarsRover(1, 1, "E");

            var exception = Assert.Throws<ArgumentException>(() => marsRover.Process("x"));
            Assert.Equal(MarsRover.INVALID_COMMAND, exception.Message);
        }

        [Fact]
        public void Test111()
        {
            var marsRover = new MarsRover(1, 1, "E");

            Assert.Throws<ArgumentException>(() => marsRover.Process("x"));
            Assert.True(marsRover.IsAtHeadingTo(1, 1, "E"));
        }
    }
}
