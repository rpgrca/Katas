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
            Assert.Equal(Result.Draw, ticTacToe.GetResult());
        }

        [Theory]
        [InlineData(Player.X, Player.O)]
        [InlineData(Player.O, Player.X)]
        public void GivenANewTicTacToeGameStartingWithPlayer_WhenTheOtherTriesToMove_ThenAnExceptionIsThrown(Player starting, Player playing)
        {
            var ticTacToe = new TicTacToe();
            ticTacToe.StartWith(starting);
            var exception = Assert.Throws<ArgumentException>(() => ticTacToe.Play(playing, 0, 0));
            Assert.Equal(TicTacToe.PLAYER_TURN_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenANewTicTacToeStartingWithDefaultPlayer_WhenPlayerOTriesToMove_ThenAnExceptionIsThrown()
        {
            var ticTacToe = new TicTacToe();
            var exception = Assert.Throws<ArgumentException>(() => ticTacToe.Play(Player.O, 0, 0));
            Assert.Equal(TicTacToe.PLAYER_TURN_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void GivenANewTicTacToeGame_WhenTryingToPlayAnInvalidYMovement_ThenAnExceptionIsThrown(int invalidY)
        {
            var ticTacToe = new TicTacToe();
            var exception = Assert.Throws<ArgumentException>(() => ticTacToe.Play(Player.X, 0, invalidY));
            Assert.Equal(TicTacToe.PLAYER_MOVEMENT_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void GivenANewTicTacToeGame_WhenTryingToPlayAnInvalidXMovement_ThenAnExceptionIsThrown(int invalidX)
         {
             var ticTacToe = new TicTacToe();
             var exception = Assert.Throws<ArgumentException>(() => ticTacToe.Play(Player.X, invalidX, 0));
             Assert.Equal(TicTacToe.PLAYER_MOVEMENT_IS_INVALID_EXCEPTION, exception.Message);
         }

        [Fact]
        public void GivenATicTacToeGameWithAPlayedTurn_WhenSamePlayerTriesToPlayAgain_ThenAnExceptionIsThrown()
        {
            var ticTacToe = new TicTacToe();
            ticTacToe.Play(Player.X, 0, 0);
            var exception = Assert.Throws<ArgumentException>(() => ticTacToe.Play(Player.X, 1, 0));
            Assert.Equal(TicTacToe.PLAYER_TURN_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenATicTacToeGameWithPlayedTurn_WhenPlayerPlaysSameMovement_ThenAnExceptionIsThrown()
        {
            var ticTacToe = new TicTacToe();
            ticTacToe.Play(Player.X, 0, 0);
            var exception = Assert.Throws<ArgumentException>(() => ticTacToe.Play(Player.O, 0, 0));
            Assert.Equal(TicTacToe.PLAYER_MOVEMENT_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenATicTacToeGame_WhenPlayerHasFirstRow_ThenResultShowsWinner()
        {
            var ticTacToe = new TicTacToe();
            ticTacToe.Play(Player.X, 0, 0);
            ticTacToe.Play(Player.O, 1, 0);
            ticTacToe.Play(Player.X, 0, 1);
            ticTacToe.Play(Player.O, 2, 0);
            ticTacToe.Play(Player.X, 0, 2);
            Assert.Equal(Result.XWon, ticTacToe.GetResult());
        }

        [Fact]
        public void GivenATicTacToeGameWithAWinner_WhenTheOtherPlayerPlays_ThenAnExceptionIsThrown()
        {
            var ticTacToe = new TicTacToe();
            ticTacToe.Play(Player.X, 0, 0);
            ticTacToe.Play(Player.O, 1, 0);
            ticTacToe.Play(Player.X, 0, 1);
            ticTacToe.Play(Player.O, 2, 0);
            ticTacToe.Play(Player.X, 0, 2);
            var exception = Assert.Throws<ArgumentException>(() => ticTacToe.Play(Player.O, 2, 2));
            Assert.Equal(TicTacToe.PLAYER_MOVEMENT_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenATicTacToeGame_WhenPlayerHasSecondRow_ThenResultShowsWinner()
        {
            var ticTacToe = new TicTacToe();
            ticTacToe.Play(Player.X, 0, 0);
            ticTacToe.Play(Player.O, 1, 0);
            ticTacToe.Play(Player.X, 0, 1);
            ticTacToe.Play(Player.O, 1, 1);
            ticTacToe.Play(Player.X, 2, 2);
            ticTacToe.Play(Player.O, 1, 2);
            Assert.Equal(Result.OWon, ticTacToe.GetResult());
        }

        [Fact]
        public void GivenATicTacToeGame_WhenBoardIsFilled_ThenGameEndsInDraw()
        {
            var ticTacToe = new TicTacToe();
            ticTacToe.Play(Player.X, 0, 0);
            ticTacToe.Play(Player.O, 0, 1);
            ticTacToe.Play(Player.X, 0, 2);
            ticTacToe.Play(Player.O, 1, 0);
            ticTacToe.Play(Player.X, 1, 1);
            ticTacToe.Play(Player.O, 2, 2);
            ticTacToe.Play(Player.X, 2, 1);
            ticTacToe.Play(Player.O, 2, 0);
            ticTacToe.Play(Player.X, 1, 2);
            Assert.Equal(Result.Draw, ticTacToe.GetResult());
        }
    }
}
