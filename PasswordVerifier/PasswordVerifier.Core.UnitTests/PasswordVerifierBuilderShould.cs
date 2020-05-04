using System;
using Xunit;

namespace PasswordVerifier.Core.UnitTests
{
    public class PasswordVerifierBuilderShould
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenANewPasswordVerifierBuilder_WhenAddingInvalidLength_ThenAnExceptionIsThrown(int invalidLength)
        {
            var passwordVerifierBuilder = new PasswordVerifierBuilder();
            var exception = Assert.Throws<ArgumentException>(() =>
                passwordVerifierBuilder.RequireAtLeast(invalidLength).CharactersInTotal());
            Assert.Equal(PasswordVerifierBuilder.MINIMUM_LENGTH_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenANewPasswordVerifierBuilder_WhenAddingInvalidUppercase_ThenAnExceptionIsThrown()
        {
            var passwordVerifierBuilder = new PasswordVerifierBuilder();
            var exception = Assert.Throws<ArgumentException>(() =>
                passwordVerifierBuilder.RequireAtLeast(-1).UpperCaseCharacters());
            Assert.Equal(PasswordVerifierBuilder.UPPERCASE_CHARACTER_AMOUNT_IS_INVALID, exception.Message);
        }
    }
}