using System;
using System.Collections.Generic;
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
            var score = 0;

            for (int i = 0; i < _frames.Length; i++)
            {
                var frameScore = 0;
                frameScore += (_frames[i].Item1 != -1 ? _frames[i].Item1 : 0);
                frameScore += (_frames[i].Item2 != -1 ? _frames[i].Item2 : 0);               
                
                if (_frames[i].Item1 == 10)
                {
//                    frameScore += _frames.Skip(i + 1).Take(2).Sum(p => p.Item1 != -1? p.Item1 : 0);
                    if (i < 9)
                    {
                        if (_frames[i + 1].Item1 != -1)
                        {
                            frameScore += _frames[i + 1].Item1;
                            if (_frames[i + 1].Item1 == 10)
                            {
                                if (_frames[i + 2].Item1 != -1)
                                {
                                    frameScore += _frames[i + 2].Item1;
                                }
                            }
                            else
                            {
                                if (_frames[i + 1].Item2 != -1)
                                {
                                    frameScore += _frames[i + 1].Item2;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (_frames[i].Item1 + _frames[i].Item2 == 10)
                    {
                        if (i < 8)
                        {
                            frameScore += _frames.Skip(i + 1).Take(1).Sum(p => p.Item1 != -1? p.Item1 : 0);
                        }
                    }
                }

                score += frameScore;
            }

            return score;
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