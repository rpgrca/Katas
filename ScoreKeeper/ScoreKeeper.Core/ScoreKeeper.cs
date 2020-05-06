using System;

namespace ScoreKeeper.Core
{
    public class ScoreKeeper
    {
        public const string SCORE_IS_INVALID_EXCEPTION = "Score cannot be over 999 points.";
        private const int TEAM_A = 0;
        private const int TEAM_B = 1;
        private readonly Score[] _scores = { null, null };

        public ScoreKeeper(int startingScoreTeamA, int startingScoreTeamB)
        {
            _scores[TEAM_A] = Score.CreateFrom(startingScoreTeamA);
            _scores[TEAM_B] = Score.CreateFrom(startingScoreTeamB);
        }

        public void ScoreTeamA1() => AddTo(TEAM_A, 1);
        public void ScoreTeamB1() => AddTo(TEAM_B, 1);
        public void ScoreTeamA2() => AddTo(TEAM_A, 2);
        public void ScoreTeamB2() => AddTo(TEAM_B, 2);
        public void ScoreTeamA3() => AddTo(TEAM_A, 3);
        public void ScoreTeamB3() => AddTo(TEAM_B, 3);

        private void AddTo(int team, int scoreToAdd) =>
            _scores[team] = Score.CreateFrom(_scores[team].Value + scoreToAdd);

        public string GetScore() =>
            $"{_scores[TEAM_A].Value:D3}:{_scores[TEAM_B].Value:D3}";       
    }
}
