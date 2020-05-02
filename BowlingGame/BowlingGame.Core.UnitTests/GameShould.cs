using System;
using System.Data;
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

        [Fact]
        public void WhenGettingASpareButWithoutExtraRoll_ScoreShouldBe10()
        {
            var game = new Game();
            game.Roll(5);
            game.Roll(5);
            Assert.Equal(10, game.Score());
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
        public void WhenLastFrameHasSpare_AnExtraRollShouldBeAwarded()
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
            game.Roll(9); // 10.2
            game.Roll(5);
            Assert.Equal(33, game.Score());
        }

        [Fact]
        public void WhenGettingAnStrikeWithoutExtraRolls_ScoreShouldBe10()
        {
            var game = new Game();
            game.Roll(10);
            Assert.Equal(10, game.Score());
        }

        [Fact]
        public void WhenGettingAnStrike_NextRollShouldBeCountedTwice()
        {
            var game = new Game();
            game.Roll(10);
            game.Roll(1);
            Assert.Equal(12, game.Score());
        }

        [Fact]
        public void WhenGettingAnStrike_NextTwoRollsShouldBeCountedTwice()
        {
            var game = new Game();
            game.Roll(10);
            game.Roll(2);
            game.Roll(2);
            Assert.Equal(18, game.Score());
        }

        [Fact]
        public void WhenGettingTwoConsecutiveStrikes_NextRollShouldBeCountedThrice()
        {
            var game = new Game();
            game.Roll(10);
            game.Roll(10);
            game.Roll(5);
            Assert.Equal(45, game.Score());
        }

        [Fact]
        public void WhenGettingTwoConsecutiveStrikes_ShouldAccountForFourBonuses()
        {
            var game = new Game();
            game.Roll(10);
            game.Roll(10);
            game.Roll(4);
            game.Roll(5);
            Assert.Equal(52, game.Score());
        }

        [Fact]
        public void WhenRollingStrikes_FrameShouldEndEarlier()
        {
            var game = new Game();
            game.Roll(10); // 1
            game.Roll(0);  // 2.1
            game.Roll(0);  // 2.2
            game.Roll(10); // 3
            game.Roll(0);  // 4.1
            game.Roll(0);  // 4.2
            game.Roll(10); // 5
            game.Roll(0);  // 6.1
            game.Roll(0);  // 6.2
            game.Roll(10); // 7
            game.Roll(0);  // 8.1
            game.Roll(0);  // 8.2
            game.Roll(10); // 9
            game.Roll(0);  // 10.1
            game.Roll(0);  // 10.2
            var exception = Assert.Throws<ArgumentException>(() => game.Roll(1));
            Assert.Contains(Game.INVALID_AMOUNT_OF_FRAMES_EXCEPTION, exception.Message);
        }

        [Fact]
        public void WhenScoringSpareAtTenthFrame_ExtraRollShouldBeAllowed()
        {
            var game = new Game();
            game.Roll(1);  // 1.1
            game.Roll(4);  // 1.2
            game.Roll(4);  // 2.1
            game.Roll(5);  // 2.2
            game.Roll(6);  // 3.1
            game.Roll(4);  // 3.2
            game.Roll(5);  // 4.1
            game.Roll(5);  // 4.2
            game.Roll(10); // 5
            game.Roll(0);  // 6.1
            game.Roll(1);  // 6.2
            game.Roll(7);  // 7.1
            game.Roll(3);  // 7.2
            game.Roll(6);  // 8.1
            game.Roll(4);  // 8.2
            game.Roll(10); // 9
            game.Roll(2);  // 10.1
            game.Roll(8);  // 10.2
            game.Roll(6);  // 11
            var exception = Assert.Throws<ArgumentException>(() => game.Roll(1));
            Assert.Equal(Game.INVALID_AMOUNT_OF_FRAMES_EXCEPTION, exception.Message);
        }
        
        [Fact]
        public void WhenScoringStrikeAtTenthFrame_TwoExtraRollsShouldBeAllowed()
        {
            var game = new Game();
            game.Roll(1);  // 1.1
            game.Roll(4);  // 1.2
            game.Roll(4);  // 2.1
            game.Roll(5);  // 2.2
            game.Roll(6);  // 3.1
            game.Roll(4);  // 3.2
            game.Roll(5);  // 4.1
            game.Roll(5);  // 4.2
            game.Roll(10); // 5
            game.Roll(0);  // 6.1
            game.Roll(1);  // 6.2
            game.Roll(7);  // 7.1
            game.Roll(3);  // 7.2
            game.Roll(6);  // 8.1
            game.Roll(4);  // 8.2
            game.Roll(10); // 9
            game.Roll(10); // 10
            game.Roll(8);  // 11
            game.Roll(2);  // 12
            var exception = Assert.Throws<ArgumentException>(() => game.Roll(1));
            Assert.Equal(Game.INVALID_AMOUNT_OF_FRAMES_EXCEPTION, exception.Message);
        }

        [Fact]
        public void WhenPlayingAGameFrame1_ScoreShouldBeCalculatedCorrectly()
        {
            var game = new Game();
            game.Roll(1);
            game.Roll(4);
            Assert.Equal(5, game.Score());
        }
        
        [Fact]
        public void WhenPlayingAGameWithSpareAtTenthFrame_ScoreShouldCorrectlyBeCalculated()
        {
            var game = new Game();
            game.Roll(1); // 1.1
            game.Roll(4); // 1.2
            game.Roll(4); // 2.1
            game.Roll(5); // 2.2
            game.Roll(6); // 3.1
            game.Roll(4); // 3.2
            game.Roll(5); // 4.1
            game.Roll(5); // 4.2
            game.Roll(10); // 5
            game.Roll(0); // 6.1
            game.Roll(1); // 6.2
            game.Roll(7); // 7.1
            game.Roll(3); // 7.2
            game.Roll(6); // 8.1
            game.Roll(4); // 8.2
            game.Roll(10); // 9
            game.Roll(2); // 10.1
            game.Roll(8); // 10.2
            game.Roll(6); // 11
            var exception = Assert.Throws<ArgumentException>(() => game.Roll(1));
            Assert.Equal(133, game.Score());
        }

        [Fact]
        public void WhenGettingAStrikeInLastFrame_TwoExtraRollsShouldBeAwarded()
        {
            var game = new Game();
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            var exception = Assert.Throws<ArgumentException>(() => game.Roll(10));
            Assert.Equal(Game.INVALID_AMOUNT_OF_FRAMES_EXCEPTION, exception.Message);
        }

        [Fact]
        public void WhenFinishingAPerfectGame_ScoreShouldBe300()
        {
             var game = new Game();
             game.Roll(10);
             game.Roll(10);
             game.Roll(10);
             game.Roll(10);
             game.Roll(10);
             game.Roll(10);
             game.Roll(10);
             game.Roll(10);
             game.Roll(10);
             game.Roll(10);
             game.Roll(10);
             game.Roll(10);
             Assert.Equal(300, game.Score());
        }

        [Fact]
        public void WhenFinishingAGame_ScoreShouldBeCalculatedCorrectly()
        {
            var game = new Game();
            game.Roll(1);
            game.Roll(8);
            game.Roll(4);
            game.Roll(2);
            game.Roll(10);
            game.Roll(2);
            game.Roll(2);
            game.Roll(1);
            game.Roll(9);
            game.Roll(7);
            game.Roll(3);
            game.Roll(10);
            game.Roll(1);
            game.Roll(2);
            game.Roll(0);
            game.Roll(0);
            game.Roll(5);
            game.Roll(2);
            Assert.Equal(93, game.Score());
        }
    }
}
