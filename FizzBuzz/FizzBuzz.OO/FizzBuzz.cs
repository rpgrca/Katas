using System;

namespace FizzBuzz.OO
{
    public abstract class Element
    {
        public abstract string ToString();
    }


    public class FizzBuzz
    {
        private readonly int _limit;

        public FizzBuzz(int limit)
        {
            _limit = limit;
        }

        public string Value
        {
            get
            {
                var result = string.Empty;

                for (var i = 1; i <= _limit; i++)
                {
                    if (i % 15 == 0)
                    {
                        result += "FizzBuzz ";
                    }
                    else if (i % 3 == 0)
                    {
                        result += "Fizz ";
                    }
                    else if (i % 5 == 0)
                    {
                        result += "Buzz ";
                    }
                    else 
                    {
                        result += i.ToString() + " ";
                    }

                }

                return result.Trim();
            }
        }
    }
}
