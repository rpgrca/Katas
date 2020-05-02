using Xunit;

namespace FizzBuzz.Core.UnitTests
{
    public class FizzBuzzShould
    {
        [Theory]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(99)]
        public void WhenExecutingTheProgram_ShouldPrintFizzOnMultiplesOf3(int anyMultipleOf3)
        {
            var fizzBuzz = new FizzBuzz();
            var result = fizzBuzz.Execute();
            Assert.Equal("Fizz", result[anyMultipleOf3 - 1]);
        }

        [Fact]
        public void WhenExecutingTheProgram_ShouldPrintBuzzOnMultiplesOf5()
        {
            var anyMultipleOf5 = 5;
            var fizzBuzz = new FizzBuzz();
            var result = fizzBuzz.Execute();
            Assert.Equal("Buzz", result[anyMultipleOf5 - 1]);
        }

        [Fact]
        public void WhenExecutingTheProgram_ShouldPrintFizzBuzzOnMultiplesOf3And5()
        {
            var anyMultipleOf3And5 = 15;
            var fizzBuzz = new FizzBuzz();
            var result = fizzBuzz.Execute();
            Assert.Equal("FizzBuzz", result[anyMultipleOf3And5 - 1]);
        }
    }
}
