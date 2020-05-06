using System;
using Xunit;

namespace ScoreKeeper.Core.UnitTests
{
    public class ScoreKeeperShould
    {
        [Fact]
        public void GivenANewScoreKeeper_WhenStartingANewGame_ThenScoreIs0Vs0()
        {
            var scoreKeeper = new ScoreKeeper();
            Assert.Equal("000:000", scoreKeeper.GetScore());
        }

        [Fact]
        public void GivenANewScoreKeeper_WhenTeamAScoresOnePoint_ThenScoreIs1Vs0()
        {
            var scoreKeeper = new ScoreKeeper();
            scoreKeeper.ScoreTeamA1();
            Assert.Equal("001:000", scoreKeeper.GetScore());
        }

        [Fact]
        public void GivenANewScoreKeeper_WhenTeamBScoresOnePoint_ThenScoreIs0Vs1()
        {
            var scoreKeeper = new ScoreKeeper();
            scoreKeeper.ScoreTeamB1();
            Assert.Equal("000:001", scoreKeeper.GetScore());
        }

        [Fact]
        public void GivenANewScoreKeeper_WhenTeamAScoresTwoPoints_ThenScoreIs2Vs0()
        {
            var scoreKeeper = new ScoreKeeper();
            scoreKeeper.ScoreTeamA2();
            Assert.Equal("002:000", scoreKeeper.GetScore());
        }

        [Fact]
        public void GivenANewScoreKeeper_WhenTeamBScoresTwoPoints_ThenScoreIs0Vs2()
        {
            var scoreKeeper = new ScoreKeeper();
            scoreKeeper.ScoreTeamB2();
            Assert.Equal("000:002", scoreKeeper.GetScore());
        }
        
         [Fact]
         public void GivenANewScoreKeeper_WhenTeamAScoresThreePoints_ThenScoreIs3Vs0()
         {
             var scoreKeeper = new ScoreKeeper();
             scoreKeeper.ScoreTeamA3();
             Assert.Equal("003:000", scoreKeeper.GetScore());
         }
 
         [Fact]
         public void GivenANewScoreKeeper_WhenTeamBScoresThreePoints_ThenScoreIs0Vs3()
         {
             var scoreKeeper = new ScoreKeeper();
             scoreKeeper.ScoreTeamB3();
             Assert.Equal("000:003", scoreKeeper.GetScore());
         }

         [Fact]
         public void GivenANewScoreKeeper_WhenScoringSeveralTimes_ThenScoreUpdatesCorrectly()
         {
             var scoreKeeper = new ScoreKeeper();
             scoreKeeper.ScoreTeamA1();
             scoreKeeper.ScoreTeamA3();
             scoreKeeper.ScoreTeamB2();
             scoreKeeper.ScoreTeamB2();
             scoreKeeper.ScoreTeamB3();
             Assert.Equal("004:007", scoreKeeper.GetScore());
         }
    }
}
