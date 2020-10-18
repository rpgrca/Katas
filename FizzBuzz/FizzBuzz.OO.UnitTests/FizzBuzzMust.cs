using System.Reflection;
using System;
using Xunit;
using FizzBuzz.OO;

namespace FizzBuzz.OO.UnitTests
{
    public class FizzBuzzMust
    {
        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "1")]
        [InlineData(2, "1 2")]
        [InlineData(3, "1 2 Fizz")]
        [InlineData(5, "1 2 Fizz 4 Buzz")]
        [InlineData(15, "1 2 Fizz 4 Buzz Fizz 7 8 Fizz Buzz 11 Fizz 13 14 FizzBuzz")]
        public void Test0(int limit, string expectedValue)
        {
            var sut = new FizzBuzz(limit);
            Assert.Equal(expectedValue, sut.Value);
        }
    }
}