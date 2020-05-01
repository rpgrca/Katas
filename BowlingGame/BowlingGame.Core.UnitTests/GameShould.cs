using System;
using Xunit;

namespace BowlingGame.Core.UnitTests
{
    public class GameShould
    {
        [Fact]
        public void WhenStartingANewGame_ScoreShouldBeZero()
        {
            var game = new Core.Game();
            Assert.Equal(0, game.Score());
        }

        [Fact]
        public void WhenDoingOneRoll_ScoreShouldBeEqualToRollValue()
        {
            var game = new Core.Game();
            game.Roll(1);
            Assert.Equal(1, game.Score());
        }

        [Fact]
        public void WhenDoingASecondRoll_ScoreShouldBeEqualToSumOfRolls()
        {
            var game = new Game();
            game.Roll(1);
            game.Roll(2);
            Assert.Equal(3, game.Score());
        }

        [Fact]
        public void WhenDoingAThirdRoll_ScoreShouldBeEqualToSumOfRolls()
        {
            var game = new Game();
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            Assert.Equal(3, game.Score());
        }

        [Fact]
        public void WhenDoingTwentyRolls_ExtraRollsShouldNotBeAwarded()
        {
            var game = new Game();
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            game.Roll(1);
            var exception = Assert.Throws<ArgumentException>(() => game.Roll(1));
            Assert.Equal(Game.INVALID_AMOUNT_OF_FRAMES_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(-1)]
        public void WhenTryingToTumbleMoreThan10Pines_ShouldThrowAnException(int invalidPins)
        {
            const string expectedParameter = "pins";
            var game = new Game();
            var exception = Assert.Throws<ArgumentOutOfRangeException>(expectedParameter, () => game.Roll(invalidPins));
            Assert.Contains(Game.INVALID_AMOUNT_OF_PINES_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData(5, 5, 5, 20)]
        [InlineData(1, 9, 9, 28)]
        [InlineData(8, 2, 2, 14)]
        public void WhenGettingASpare_BonusShouldBeAwardedAfterNextThrow(int first, int second, int third, int expectedResult)
        {
            var game = new Game();
            game.Roll(first);
            game.Roll(second);
            game.Roll(third);
            Assert.Equal(expectedResult, game.Score());
        }

        [Fact]
        public void WhenGettingASpareButWithoutExtraRoll_ScoreShouldBe10()
        {
            var game = new Game();
            game.Roll(5);
            game.Roll(5);
            Assert.Equal(10, game.Score());
        }
    }
}
