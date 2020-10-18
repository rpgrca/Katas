using System.Linq;
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

        public override string ToString()
        {
            var result = string.Empty;

            if (_value % 15 == 0)
            {
                result += "FizzBuzz";
            }
            else if (_value % 3 == 0)
            {
                result += "Fizz";
            }
            else if (_value % 5 == 0)
            {
                result += "Buzz";
            }
            else 
            {
                result += _value.ToString();
            }

            return result;
        }
    }

    public class FizzBuzz
    {
        private readonly int _limit;
        private List<Element> _numbers;

        public FizzBuzz(int limit)
        {
            _limit = limit;
            _numbers = new List<Element>();
            foreach (var value in Enumerable.Range(1, limit))
            {
                _numbers.Add(new Number(value));
            }
        }

        public string Value
        {
            get
            {
                return string.Join(" ", _numbers);
            }
        }
    }
}
