using System;
using Xunit;

namespace ScoreKeeper.Core.UnitTests
{
    public class ScoreKeeperShould
    {
        [Theory]
        [InlineData(0, 0, "000:000")]
        [InlineData(999, 0, "999:000")]
        [InlineData(0, 999, "000:999")]
        [InlineData(999, 999, "999:999")]
        public void GivenANewScoreKeeper_WhenStartingANewGame_ThenScoreIsSet(int initialScoreA, int initialScoreB,
            string visualScore)
        {
            var scoreKeeper = new ScoreKeeper(initialScoreA, initialScoreB);
            Assert.Equal(visualScore, scoreKeeper.GetScore());
        }

        [Theory]
        [InlineData(1000, 0)]
        [InlineData(0, 1000)]
        [InlineData(1000, 1000)]
        public void GivenANewScoreKeeper_WhenStartingANewGameWithMoreThan999Points_ThenAnExceptionIsThrown(int startingPointA, int startingPointB)
        {
            var exception = Assert.Throws<ArgumentException>(() => new ScoreKeeper(startingPointA, startingPointB));
            Assert.Equal(ScoreKeeper.SCORE_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        public void GivenANewScoreKeeper_WhenStartingANewGameWithLessThan1Point_ThenAnExceptionIsThrown(int startingPointA, int startingPointB)
        {
            var exception = Assert.Throws<ArgumentException>(() => new ScoreKeeper(startingPointA, startingPointB));
            Assert.Equal(ScoreKeeper.SCORE_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenANewScoreKeeper_WhenTeamAScoresOnePoint_ThenScoreIs1Vs0()
        {
            var scoreKeeper = new ScoreKeeper(0, 0);
            scoreKeeper.ScoreTeamA1();
            Assert.Equal("001:000", scoreKeeper.GetScore());
        }

        [Fact]
        public void GivenANewScoreKeeper_WhenTeamBScoresOnePoint_ThenScoreIs0Vs1()
        {
            var scoreKeeper = new ScoreKeeper(0, 0);
            scoreKeeper.ScoreTeamB1();
            Assert.Equal("000:001", scoreKeeper.GetScore());
        }

        [Fact]
        public void GivenANewScoreKeeper_WhenTeamAScoresTwoPoints_ThenScoreIs2Vs0()
        {
            var scoreKeeper = new ScoreKeeper(0, 0);
            scoreKeeper.ScoreTeamA2();
            Assert.Equal("002:000", scoreKeeper.GetScore());
        }

        [Fact]
        public void GivenANewScoreKeeper_WhenTeamBScoresTwoPoints_ThenScoreIs0Vs2()
        {
            var scoreKeeper = new ScoreKeeper(0, 0);
            scoreKeeper.ScoreTeamB2();
            Assert.Equal("000:002", scoreKeeper.GetScore());
        }
        
         [Fact]
         public void GivenANewScoreKeeper_WhenTeamAScoresThreePoints_ThenScoreIs3Vs0()
         {
             var scoreKeeper = new ScoreKeeper(0, 0);
             scoreKeeper.ScoreTeamA3();
             Assert.Equal("003:000", scoreKeeper.GetScore());
         }
 
         [Fact]
         public void GivenANewScoreKeeper_WhenTeamBScoresThreePoints_ThenScoreIs0Vs3()
         {
             var scoreKeeper = new ScoreKeeper(0, 0);
             scoreKeeper.ScoreTeamB3();
             Assert.Equal("000:003", scoreKeeper.GetScore());
         }

         [Fact]
         public void GivenANewScoreKeeper_WhenScoringSeveralTimes_ThenScoreUpdatesCorrectly()
         {
             var scoreKeeper = new ScoreKeeper(0, 0);
             scoreKeeper.ScoreTeamA1();
             scoreKeeper.ScoreTeamA3();
             scoreKeeper.ScoreTeamB2();
             scoreKeeper.ScoreTeamB2();
             scoreKeeper.ScoreTeamB3();
             Assert.Equal("004:007", scoreKeeper.GetScore());
         }
    }
}
