using System;

namespace ScoreKeeper.Core
{
    public class ScoreKeeper
    {
        private int _scoreTeamA = 0;
        private int _scoreTeamB = 0;

        public string GetScore()
        {
            return $"{_scoreTeamA:D3}:{_scoreTeamB:D3}";
        }

        public void ScoreTeamA1()
        {
            _scoreTeamA++;
        }

        public void ScoreTeamB1()
        {
            _scoreTeamB++;
        }

        public void ScoreTeamA2()
        {
            _scoreTeamA += 2;
        }

        public void ScoreTeamB2()
        {
            _scoreTeamB += 2;
        }

        public void ScoreTeamA3()
        {
            _scoreTeamA += 3;
        }

        public void ScoreTeamB3()
        {
            _scoreTeamB += 3;
        }
    }
}
