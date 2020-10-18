using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System;

namespace FizzBuzz.OO
{
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
                _numbers.Add(Number.From(value));
            }
        }

        public string Value => string.Join(" ", _numbers);
    }
}