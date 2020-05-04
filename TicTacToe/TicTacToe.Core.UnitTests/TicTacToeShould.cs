using System;
using Xunit;

namespace TicTacToe.Core.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void GivenANewTicTacToeGame_WhenStarting_ThenThereIsNoWinner()
        {
            var ticTacToe = new TicTacToe();
            Assert.False(ticTacToe.HasWinner());
        }

        [Theory]
        [InlineData(Player.X, Player.O)]
        [InlineData(Player.O, Player.X)]
        public void GivenANewTicTacToeGameStartingWithPlayer_WhenTheOtherTriesToMove_AnExceptionIsThrown(Player starting, Player playing)
        {
            var ticTacToe = new TicTacToe();
            ticTacToe.StartWith(starting);
            var exception = Assert.Throws<ArgumentException>(() => ticTacToe.Play(playing, 0, 0));
            Assert.Equal(TicTacToe.PLAYER_TURN_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void Test1(int invalidY)
        {
            var ticTacToe = new TicTacToe();
            ticTacToe.StartWith(Player.X);
            var exception = Assert.Throws<ArgumentException>(() => ticTacToe.Play(Player.X, 0, invalidY));
            Assert.Equal(TicTacToe.PLAYER_MOVEMENT_IS_INVALID_EXCEPTION, exception.Message);
        }
    }
}
