using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System;

namespace FizzBuzz.OO
{
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
}