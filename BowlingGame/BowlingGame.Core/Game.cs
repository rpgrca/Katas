using System;
using System.Linq;

namespace BowlingGame.Core
{
    public class Game
    {
        private readonly (int, int)[] _frames = new[]
        {
            (-1, -1), (-1, -1), (-1, -1), (-1, -1), (-1, -1), (-1, -1), (-1, -1), (-1, -1), (-1, -1), (-1, -1), (-1, -1), (-1, -1)
        };

        private int _index = 0;
        public const string INVALID_AMOUNT_OF_FRAMES_EXCEPTION = "Invalid amount of frames";
        public const string INVALID_AMOUNT_OF_PINES_EXCEPTION = "Invalid amount of pines";


        public int Score()
        {
            var nextMultiplier = 0;
            var nextNextMultiplier = 0;
            var bonus = 0;
            var score = 0;
            
            for (int i = 0; i <= _index; i++)
            {
                if (_frames[i].Item1 != -1)
                {
                    score += _frames[i].Item1;
                }

                if (_frames[i].Item2 != -1)
                {
                    score += _frames[i].Item2;
                }

                if (i < 10)
                {
                    if (_frames[i].Item1 != -1 && nextMultiplier > 0)
                    {
                        bonus += _frames[i].Item1 * nextMultiplier;
                        nextMultiplier = nextNextMultiplier;
                        nextNextMultiplier = 0;
                    }

                    if (_frames[i].Item2 != -1 && nextMultiplier > 0)
                    {
                        bonus += _frames[i].Item2 * nextMultiplier;
                        nextMultiplier = nextNextMultiplier;
                        nextNextMultiplier = 0;
                    }

                    if (StrikeInFrame(_frames[i]))
                    {
                        nextMultiplier += 1;
                        nextNextMultiplier = 1;
                    }
                    else
                    {
                        if (SpareInFrame(_frames[i]))
                        {
                            nextMultiplier += 1;
                        }
                    }
                }
            }

            return score + bonus;
        }

        private static bool SpareInFrame((int, int) frame) =>
            frame.Item1 + frame.Item2 == 10;

        private static bool StrikeInFrame((int, int) frame) =>
            frame.Item1 == 10;

        public void Roll(int pins)
        {
            if (pins > 10 || pins < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pins), INVALID_AMOUNT_OF_PINES_EXCEPTION);
            }

            if (_index < 10 && _frames[_index].Item1 == -1)
            {
                _frames[_index].Item1 = pins;
                if (pins == 10)
                {
                    _index++;
                }
            }
            else if (_index < 10 && _frames[_index].Item2 == -1)
            {
                _frames[_index].Item2 = pins;
            }
            else
            {
                if (++_index < 10)
                {
                    _frames[_index].Item1 = pins;
                    if (pins == 10)
                    {
                        _index++;
                    }
                }
                else
                {
                    if (_frames[9].Item1 == 10)
                    {
                        if (_frames[10].Item1 == 10)
                        {
                            if (_frames[11].Item1 == -1)
                            {
                                _frames[11].Item1 = pins;
                            }
                            else
                            {
                                throw new ArgumentException(INVALID_AMOUNT_OF_FRAMES_EXCEPTION);
                            }
                        }
                        else
                        {
                            if (_frames[10].Item1 == -1)
                            {
                                _frames[10].Item1 = pins;
                            }
                            else
                            {
                                if (_frames[10].Item2 == -1)
                                {
                                    _frames[10].Item2 = pins;
                                }
                                else
                                {
                                    throw new ArgumentException(INVALID_AMOUNT_OF_FRAMES_EXCEPTION);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (_frames[9].Item1 + _frames[9].Item2 == 10)
                        {
                            if (_frames[10].Item1 == -1)
                            {
                                _frames[10].Item1 = pins;
                            }
                            else
                            {
                                throw new ArgumentException(INVALID_AMOUNT_OF_FRAMES_EXCEPTION);
                            }
                        }
                        else
                        {
                            throw new ArgumentException(INVALID_AMOUNT_OF_FRAMES_EXCEPTION);
                        }                       
                    }
                }
            }
        }
    }
}