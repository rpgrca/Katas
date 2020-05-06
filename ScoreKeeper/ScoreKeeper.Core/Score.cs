using System;

namespace ScoreKeeper.Core
{
    internal class Score
    {
        public int Value { get; }

        private Score(int score)
        {
            Value = score >= 0 && score <= 999
                ? score
                : throw new ArgumentException(ScoreKeeper.SCORE_IS_INVALID_EXCEPTION);
        }

        internal static Score CreateFrom(int value) =>
            new Score(value);
    }
}