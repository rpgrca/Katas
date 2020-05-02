using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingGame.Core
{
    public class Game
    {
        private readonly List<int> _rolls = new List<int>();
        public const string INVALID_AMOUNT_OF_FRAMES_EXCEPTION = "Invalid amount of frames";
        public const string INVALID_AMOUNT_OF_PINES_EXCEPTION = "Invalid amount of pines";

        public int Score()
        {
            var score = 0;

            for (var i = 0; i < 20; i += 2)
            {
                var frameScore = 0;
                frameScore += (GetRoll(i) != -1 ? GetRoll(i) : 0);
                var strike = frameScore == 10;
                frameScore += (GetRoll(i + 1) != -1 ? GetRoll(i + 1) : 0);

                if (frameScore == 10)
                {
                    frameScore += GetBonus(i, strike ? 2 : 1);
                }

                score += frameScore;
            }

            return score;
        }

        private int GetRoll(int index) =>
            index < _rolls.Count ? _rolls[index] : -1;

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
            const int maximumAmountOfRolls = 21;
            
            if (pins > maximumAmountOfPinsPerRoll || pins < minimumAmountOfPinsPerRoll)
            {
                throw new ArgumentOutOfRangeException(nameof(pins), INVALID_AMOUNT_OF_PINES_EXCEPTION);
            }

            if (_rolls.Count > maximumAmountOfRolls)
            {
                throw new ArgumentException(INVALID_AMOUNT_OF_FRAMES_EXCEPTION);
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
                if (_rolls.Count == 20)
                {
                    if (IsStrikeInRoll(18) || IsSplitInRolls(18, 19))
                    {
                        _rolls.Add(pins);
                    }
                    else
                    {
                        throw new ArgumentException(INVALID_AMOUNT_OF_FRAMES_EXCEPTION);
                    }
                }
                else
                {
                    if (IsStrikeInRoll(18))
                    {
                        _rolls.Add(pins);
                    }
                    else
                    {
                        throw new ArgumentException(INVALID_AMOUNT_OF_FRAMES_EXCEPTION);
                    }
                }
            }
        }

        private bool IsStrikeInRoll(int roll) =>
            GetRoll(roll) == 10;

        private bool IsSplitInRolls(int roll1, int roll2) =>
            GetRoll(roll1) + GetRoll(roll2) == 10;
    }
}