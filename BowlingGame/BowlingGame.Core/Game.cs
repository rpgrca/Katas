using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingGame.Core
{
    public class Game
    {
        public const string INVALID_AMOUNT_OF_FRAMES_EXCEPTION = "Invalid amount of frames";
        public const string INVALID_AMOUNT_OF_PINES_EXCEPTION = "Invalid amount of pines";
        private readonly List<int> _rolls = new List<int>();

        public int Score()
        {
            var score = 0;

            for (var i = 0; i < 20; i += 2)
            {
                var frameScore = GetRoll(i);
                var strike = frameScore == 10;
                frameScore += GetRoll(i + 1);

                if (frameScore == 10)
                {
                    frameScore += GetBonus(i, strike ? 2 : 1);
                }

                score += frameScore;
            }

            return score;
        }

        private int GetRoll(int index) =>
            index < _rolls.Count ? (_rolls[index] != -1? _rolls[index] : 0) : 0;

        private int GetBonus(int i, int count) =>
            _rolls
                .Skip(i + 2)
                .Where(p => p != -1)
                .Take(count)
                .Sum();

        public void Roll(int pins)
        {
            const int maximumAmountOfPinsPerRoll = 10;
            const int minimumAmountOfPinsPerRoll = 0;

            if (pins > maximumAmountOfPinsPerRoll || pins < minimumAmountOfPinsPerRoll)
            {
                throw new ArgumentOutOfRangeException(nameof(pins), INVALID_AMOUNT_OF_PINES_EXCEPTION);
            }

            if (_rolls.Count < 20)
            {
                _rolls.Add(pins);
                if (pins == 10)
                {
                    _rolls.Add(-1);
                }
            }
            else
            {
                switch (_rolls.Count)
                {
                    case 20 when (IsStrikeInRoll(18) || IsSplitInRolls(18, 19)):
                    case 21 when IsStrikeInRoll(18):
                        _rolls.Add(pins);
                        break;
                    default:
                        throw new ArgumentException(INVALID_AMOUNT_OF_FRAMES_EXCEPTION);
                }
            }
        }

        private bool IsStrikeInRoll(int roll) =>
            GetRoll(roll) == 10;

        private bool IsSplitInRolls(int roll1, int roll2) =>
            GetRoll(roll1) + GetRoll(roll2) == 10;
    }
}