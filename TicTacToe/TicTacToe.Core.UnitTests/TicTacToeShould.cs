using System;
using Xunit;

namespace TicTacToe.Core.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var ticTacToe = new TicTacToe();
            Assert.False(ticTacToe.HasWinner());
        }
    }
}
