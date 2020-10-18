using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System;

namespace FizzBuzz.OO
{
    public class Number : Element
    {
        private class MultipleOfThree : Number
        {
            public MultipleOfThree(int value) : base(value)
            {
            }

            public override string ToString() => "Fizz";
        }

        private class MultipleOfFive : Number
        {
            public MultipleOfFive(int value) : base(value)
            {
            }

            public override string ToString() => "Buzz";
        }

        private class MultipleOfThreeAndFive : Number
        {
            public MultipleOfThreeAndFive(int value) : base(value)
            {
            }

            public override string ToString() => "FizzBuzz";
        }

        public static Number From(int value)
        {
            if (value % 15 == 0)
            {
                return new MultipleOfThreeAndFive(value);
            }
            else if (value % 3 == 0)
            {
                return new MultipleOfThree(value);
            }
            else if (value % 5 == 0)
            {
                return new MultipleOfFive(value);
            }

            return new Number(value);
        }

        protected readonly int _value;

        protected Number(int value) => _value = value;

        public override string ToString() => _value.ToString();
    }
}