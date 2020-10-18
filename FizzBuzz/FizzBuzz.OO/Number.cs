using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System;

namespace FizzBuzz.OO
{
    public class Number : Element
    {
        public class NullFormatter : Formatter
        {
            public NullFormatter()
            {
            }

            public NullFormatter(Formatter previousFormatter) : base(previousFormatter)
            {
            }

            public override string ConvertToString(int value) => string.Empty;
        }

        public class FizzFormatter : Formatter
        {
            public FizzFormatter() : base(new NullFormatter())
            {
            }

            public FizzFormatter(Formatter previousFormatter) : base(previousFormatter)
            {                
            }

            public override string ConvertToString(int value) =>
                "Fizz" + _previousFormatter.ConvertToString(value);
        }

        public class BuzzFormatter : Formatter
        {
            public BuzzFormatter() : base(new NullFormatter())
            {
            }

            public BuzzFormatter(Formatter previousFormatter) : base(previousFormatter)
            {                
            }

            public override string ConvertToString(int value) =>
                "Buzz" + _previousFormatter.ConvertToString(value);
        }

        public class DefaultFormatter : Formatter
        {
            public DefaultFormatter() : base(new NullFormatter())
            {
            }

            public DefaultFormatter(Formatter previousFormatter) =>
                _previousFormatter = previousFormatter;

            public override string ConvertToString(int value) =>
                value.ToString() + _previousFormatter.ConvertToString(value);
        }

        public abstract class Formatter
        {
            protected Formatter _previousFormatter;

            public Formatter()
            {                
            }

            public Formatter(Formatter previousFormatter) =>
                _previousFormatter = previousFormatter;

            public abstract string ConvertToString(int value);
        }

        public static Number From(int value)
        {
            if (value % 15 == 0)
            {
                return new Number(value, new FizzFormatter(new BuzzFormatter()));
            }
            else if (value % 3 == 0)
            {
                return new Number(value, new FizzFormatter());
            }
            else if (value % 5 == 0)
            {
                return new Number(value, new BuzzFormatter());
            }

            return new Number(value, new DefaultFormatter());
        }

        protected readonly int _value;
        private readonly Formatter _formatter;

        protected Number(int value, Formatter formatter)
        {
            _value = value;
            _formatter = formatter;
        }

        public override string ToString() => _formatter.ConvertToString(_value);
    }
}