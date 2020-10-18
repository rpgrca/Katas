using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System;

namespace FizzBuzz.OO
{
    public class MultipleOfThree : Number
    {
        public MultipleOfThree(int value) : base(value)
        {
        }

        public override string ToString() => "Fizz";
    }

    public class MultipleOfFive : Number
    {
        public MultipleOfFive(int value) : base(value)
        {
        }

        public override string ToString() => "Buzz";
    }

    public class MultipleOfThreeAndFive : Number
    {
        public MultipleOfThreeAndFive(int value) : base(value)
        {
        }

        public override string ToString() => "FizzBuzz";
    }

    public class Number : Element
    {
        public static Number From(int value)
        {
            return new Number(value);
        }

        protected readonly int _value;

        protected Number(int value) => _value = value;

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