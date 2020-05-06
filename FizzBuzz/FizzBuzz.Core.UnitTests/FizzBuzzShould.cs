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
            var result = FizzBuzz.Execute();
            Assert.Equal("Fizz", result[anyMultipleOf3 - 1]);
        }

        [Fact]
        public void WhenExecutingTheProgram_ShouldPrintBuzzOnMultiplesOf5()
        {
            const int anyMultipleOf5 = 5;
            var result = FizzBuzz.Execute();
            Assert.Equal("Buzz", result[anyMultipleOf5 - 1]);
        }

        [Fact]
        public void WhenExecutingTheProgram_ShouldPrintFizzBuzzOnMultiplesOf3And5()
        {
            const int anyMultipleOf3And5 = 15;
            var result = FizzBuzz.Execute();
            Assert.Equal("FizzBuzz", result[anyMultipleOf3And5 - 1]);
        }
    }
}
