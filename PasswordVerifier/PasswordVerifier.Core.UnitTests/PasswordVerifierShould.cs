using System;
using System.Collections.Generic;
using Xunit;

namespace PasswordVerifier.Core.UnitTests
{
    public class PasswordVerifierShould
    {
        [Fact]
        public void GivenPasswordVerifier_WhenNullRulesAreGiven_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new PasswordVerifier(null));
            Assert.Equal(PasswordVerifier.RULES_IS_NULL_EXCEPTION, exception.Message);
        }
        
        [Theory]
        [InlineData("anyPassword")]
        [InlineData("")]
        public void GivenPasswordVerifierWithoutRules_WhenGivenAnyPassword_ThenItsVerified(string anyPassword)
        {
            var passwordVerifier = new PasswordVerifier(new List<Func<string, bool>>());
            var result = passwordVerifier.Verify(anyPassword);
            Assert.True(result);
        }

        [Fact]
        public void GivenAPasswordVerifier_WhenUsingNullPassword_ThenAnExceptionIsThrown()
        {
            var passwordVerifier = new PasswordVerifier(new List<Func<string, bool>>());
            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify(null));
            Assert.Equal(PasswordVerifier.PASSWORD_IS_NULL_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifierWithLengthRule_WhenGivingShorterPassword_ThenAnExceptionIsThrown()
        {
            const string shortPassword = "short";
            var passwordVerifier = new PasswordVerifierBuilder()
                .RequireAtLeast(9).CharactersInTotal()
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify(shortPassword));
            Assert.Equal(PasswordVerifier.PASSWORD_LENGTH_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifierWithLengthRule_WhenGivingCorrectPassword_ThenItVerifiesCorrectly()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .RequireAtLeast(9).CharactersInTotal()
                .Build();

            var result = passwordVerifier.Verify("long password");
            Assert.True(result);
        }

        [Fact]
        public void GivenPasswordVerifierWithUpperCaseRule_WhenGivingPasswordWithoutUpperCaseLetter_ThenAnExceptionIsThrown()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .RequireAtLeast(1).UpperCaseCharacters()
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify("invalid password"));
            Assert.Equal(PasswordVerifier.AMOUNT_OF_UPPERCASE_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifierWithUpperCaseRule_WhenGivingPasswordWithUpperCaseLetter_ThenItsVerified()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .RequireAtLeast(1).UpperCaseCharacters()
                .Build();

            var result = passwordVerifier.Verify("validPassword");
            Assert.True(result);
        }
        
        [Fact]
        public void GivenPasswordVerifierWithLowerCaseRule_WhenGivingPasswordWithoutLowerCaseLetter_ThenAnExceptionIsThrown()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .RequireAtLeast(1).LowerCaseCharacters()
                .Build();
        
            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify("INVALID PASSWORD"));
            Assert.Equal(PasswordVerifier.AMOUNT_OF_LOWERCASE_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifierWithLowerCaseRule_WhenGivingPasswordWithLowerCaseLetter_ThenItsVerified()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .RequireAtLeast(1).LowerCaseCharacters()
                .Build();

            var result = passwordVerifier.Verify("VALID pASSWORD");
            Assert.True(result);
        }

        [Fact]
        public void GivenPasswordVerifierWithNumberRule_WhenGivingPasswordWithoutNumber_ThenAnExceptionIsThrown()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .RequireAtLeast(1).Number()
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify("invalid password"));
            Assert.Equal(PasswordVerifier.AMOUNT_OF_NUMBERS_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifierWithNumberRule_WhenGivingPasswordWithNumber_ThenItsVerified()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .RequireAtLeast(1).Number()
                .Build();

            var result = passwordVerifier.Verify("v4lid password");
            Assert.True(result);
        }
    }
}
