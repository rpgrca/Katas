using System;
using System.Collections.Generic;
using System.Linq;

namespace FizzBuzz.Core
{
    public class FizzBuzz
    {
        private bool IsFizz(int value)
            => value % 3 == 0;

        private bool IsBuzz(int value)
            => value % 5 == 0;

        public List<string> Execute() =>
            Enumerable.Range(1, 100)
                .Select(p => IsFizz(p)
                    ? IsBuzz(p)
                        ? "FizzBuzz"
                        : "Fizz"
                    : IsBuzz(p)
                        ? "Buzz"
                        : p.ToString())
                .ToList();

        public void Run() =>
            Execute().ForEach(p => Console.Write($"{p} "));
    }
}