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

        private int _extra_rolls = 0;
        private int _index = 0;
        public const string INVALID_AMOUNT_OF_FRAMES_EXCEPTION = "Invalid amount of frames";
        public const string INVALID_AMOUNT_OF_PINES_EXCEPTION = "Invalid amount of pines";

        public int Score()
        {
            var result = 0;
            var bonus = 0;
            
            for (var i = 0; i <= _index; i++)
            {
                var frameScore = 0;
                var (roll1, roll2) = _frames[i];
                {
                    if (roll1 != -1) // Always != -1
                    {
                        if (roll1 == 10 && _index < 10)
                        {
                            if (bonus > 0)
                            {
                                frameScore += roll1;
                                bonus--;
                            }

                            bonus += 2;
                        }
                        else
                        {
                            if (bonus > 0)
                            {
                                frameScore += roll1;
                                bonus--;
                            }
                        }
 
                        frameScore += roll1;
                    }

                    if (roll2 != -1)
                    {
                        if (roll1 + roll2 == 10 && _index < 10)
                        {
                            bonus += 1;
                        }
                        else
                        {
                            if (bonus > 0)
                            {
                                frameScore += roll2;
                                bonus--;
                            }
                        }
                        
                        frameScore += roll2;
                    }
                }
               
                result += frameScore;
            }
            
            return result;
        }

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
                                    if (_frames[11].Item1 == -1)
                                    {
                                        _frames[10].Item1 = pins;
                                    }
                                    else
                                    {
                                        throw new ArgumentException(INVALID_AMOUNT_OF_FRAMES_EXCEPTION);
                                    }
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