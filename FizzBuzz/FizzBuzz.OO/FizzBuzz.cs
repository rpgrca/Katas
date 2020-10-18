using System.Globalization;
using System.Collections.Generic;
using System;

namespace FizzBuzz.OO
{
    public interface Element
    {
        string ToString();
    }

    public class Number : Element
    {
        private readonly int _value;

        public Number(int value) => _value = value;

        public string ToString() => _value.ToString();
    }

    public class FizzBuzz
    {
        private readonly int _limit;
        private List<Element> _numbers;

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
