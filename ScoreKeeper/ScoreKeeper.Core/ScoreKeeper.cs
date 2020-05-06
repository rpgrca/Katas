using System;

namespace ScoreKeeper.Core
{
    public class ScoreKeeper
    {
        public const string SCORE_IS_INVALID_EXCEPTION = "Score cannot be over 999 points.";
        private int _scoreTeamA = 0;
        private int _scoreTeamB = 0;

        public ScoreKeeper(int startingScoreTeamA, int startingScoreTeamB)
        {
            _scoreTeamA = startingScoreTeamA >= 0 && startingScoreTeamA <= 999
                ? startingScoreTeamA
                : throw new ArgumentException(SCORE_IS_INVALID_EXCEPTION);

            _scoreTeamB = startingScoreTeamB >= 0 && startingScoreTeamB <= 999
                ? startingScoreTeamB
                : throw new ArgumentException(SCORE_IS_INVALID_EXCEPTION);
        }
        
        public string GetScore() =>
            $"{_scoreTeamA:D3}:{_scoreTeamB:D3}";

        public void ScoreTeamA1() =>
            AddToTeamA(1);

        public void ScoreTeamB1() =>
            AddToTeamB(1);

        public void ScoreTeamA2() =>
            AddToTeamA(2);

        public void ScoreTeamB2() =>
            AddToTeamB(2);

        public void ScoreTeamA3() =>
            AddToTeamA(3);

        public void ScoreTeamB3() =>
            AddToTeamB(3);

        private void AddToTeamA(int scoreToAdd) =>
            _scoreTeamA = _scoreTeamA + scoreToAdd <= 999
                ? _scoreTeamA + scoreToAdd
                : throw new ArgumentOutOfRangeException();

        private void AddToTeamB(int scoreToAdd) =>
            _scoreTeamB = _scoreTeamB + scoreToAdd <= 999
                ? _scoreTeamB + scoreToAdd
                : throw new ArgumentOutOfRangeException();
    }
}
