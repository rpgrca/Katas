using System;
using Xunit;

namespace FizzBuzz.Core.UnitTests
{
    public class FizzBuzzShould
    {
        [Fact]
        public void WhenExecutingTheProgram_ShouldPrintFizzOnMultiplesOf3()
        {
            var fizzBuzz = new FizzBuzz();
            var result = fizzBuzz.Execute();
            Assert.Equal("Fizz", result[2]);
        }
    }
}
