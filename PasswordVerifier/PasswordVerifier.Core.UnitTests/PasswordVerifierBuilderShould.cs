using System;
using Xunit;

namespace PasswordVerifier.Core.UnitTests
{
    public class PasswordVerifierBuilderShould
    {
        [Theory]
        [InlineData(-1)]
        public void GivenANewPasswordVerifierBuilder_WhenAddingInvalidLength_ThenAnExceptionIsThrown(int invalidLength)
        {
            var passwordVerifierBuilder = new PasswordVerifierBuilder();
            var exception = Assert.Throws<ArgumentException>(() =>
                passwordVerifierBuilder.Require.AtLeast(invalidLength).CharactersInTotal);
            Assert.Equal(PasswordVerifierBuilder.MINIMUM_LENGTH_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenANewPasswordVerifierBuilder_WhenAddingInvalidUppercase_ThenAnExceptionIsThrown()
        {
            var passwordVerifierBuilder = new PasswordVerifierBuilder();
            var exception = Assert.Throws<ArgumentException>(() =>
                passwordVerifierBuilder.Require.AtLeast(-1).UpperCaseCharacters);
            Assert.Equal(PasswordVerifierBuilder.UPPERCASE_CHARACTER_AMOUNT_IS_INVALID, exception.Message);
        }

        [Fact]
        public void GivenANewPasswordVerifierBuilder_WhenAddingInvalidLowercase_ThenAnExceptionIsThrown()
        {
            var passwordVerifierBuilder = new PasswordVerifierBuilder();
            var exception = Assert.Throws<ArgumentException>(() =>
                passwordVerifierBuilder.Require.AtLeast(-1).LowerCaseCharacters);
            Assert.Equal(PasswordVerifierBuilder.LOWERCASE_CHARACTER_AMOUNT_IS_INVALID, exception.Message);
        }

        [Fact]
        public void GivenANewPasswordVerifierBuilder_WhenAddingInvalidNumbers_ThenAnExceptionIsThrown()
        {
            var passwordVerifierBuilder = new PasswordVerifierBuilder();
            var exception = Assert.Throws<ArgumentException>(() =>
                passwordVerifierBuilder.Require.AtLeast(-1).Numbers);
            Assert.Equal(PasswordVerifierBuilder.NUMBER_AMOUNT_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenANewPasswordVerifierBuilder_WhenAddingInvalidPassingRules_ThenAnExceptionIsThrown(int rules)
        {
            var passwordVerifierBuilder = new PasswordVerifierBuilder();
            var exception = Assert.Throws<ArgumentException>(() =>
                passwordVerifierBuilder.Require.AtLeast(rules).PassingRules);
            Assert.Equal(PasswordVerifierBuilder.NUMBER_OF_MINIMUM_REQUIREMENTS_IS_INVALID_EXCEPTION,
                exception.Message);
        }

        [Fact]
        public void GivenANewPasswordVerifierBuilder_WhenAddingInvalidAlwaysLowerCaseValue_ThenAnExceptionIsThrown()
        {
            var passwordVerifierBuilder = new PasswordVerifierBuilder();
            var exception = Assert.Throws<ArgumentException>(() =>
                passwordVerifierBuilder.Require.Always(-1).LowerCaseCharacters);
            Assert.Equal(PasswordVerifierBuilder.LOWERCASE_CHARACTER_AMOUNT_IS_INVALID, exception.Message);
        }
    }
}