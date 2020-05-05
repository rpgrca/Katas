using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xunit;

namespace PasswordVerifier.Core.UnitTests
{
    public class PasswordVerifierShould
    {
        [Fact]
        public void GivenPasswordVerifier_WhenNullRulesAreGiven_ThenAnExceptionIsThrown()
        {
            Func<List<Func<string, bool>>, string, bool> anyValidator = (l, s) => true;
            var exception = Assert.Throws<ArgumentException>(() => new PasswordVerifier(null, anyValidator));
            Assert.Equal(PasswordVerifier.RULES_IS_NULL_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifier_WhenNullVerificatorIsGiven_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new PasswordVerifier(new List<Func<string, bool>> {}, null));
            Assert.Equal(PasswordVerifier.VERIFICATOR_IS_NULL_EXCEPTION, exception.Message);
        }
        
        [Theory]
        [InlineData("anyPassword")]
        [InlineData("")]
        public void GivenPasswordVerifierWithoutRules_WhenGivenAnyPassword_ThenItsVerified(string anyPassword)
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Build();
            var result = passwordVerifier.Verify(anyPassword);
            Assert.True(result);
        }

        [Fact]
        public void GivenAPasswordVerifierWithoutRules_WhenUsingNullPassword_ThenItsVerified()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Build();
            var result = passwordVerifier.Verify(null);
            Assert.True(result);
        }

        [Fact]
        public void GivenPasswordVerifierWithLengthRule_WhenGivingShorterPassword_ThenAnExceptionIsThrown()
        {
            const string shortPassword = "short";
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(9).CharactersInTotal
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify(shortPassword));
            Assert.Equal(PasswordVerifier.PASSWORD_LENGTH_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifierWithLengthRule_WhenGivingCorrectPassword_ThenItVerifiesCorrectly()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(9).CharactersInTotal
                .Build();

            var result = passwordVerifier.Verify("long password");
            Assert.True(result);
        }

        [Fact]
        public void GivenPasswordVerifierWithUpperCaseRule_WhenGivingPasswordWithoutUpperCaseLetter_ThenAnExceptionIsThrown()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(1).UpperCaseCharacters
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify("invalid password"));
            Assert.Equal(PasswordVerifier.AMOUNT_OF_UPPERCASE_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifierWithUpperCaseRule_WhenGivingPasswordWithUpperCaseLetter_ThenItsVerified()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(1).UpperCaseCharacters
                .Build();

            var result = passwordVerifier.Verify("validPassword");
            Assert.True(result);
        }
        
        [Fact]
        public void GivenPasswordVerifierWithLowerCaseRule_WhenGivingPasswordWithoutLowerCaseLetter_ThenAnExceptionIsThrown()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(1).LowerCaseCharacters
                .Build();
        
            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify("INVALID PASSWORD"));
            Assert.Equal(PasswordVerifier.AMOUNT_OF_LOWERCASE_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifierWithLowerCaseRule_WhenGivingPasswordWithLowerCaseLetter_ThenItsVerified()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(1).LowerCaseCharacters
                .Build();

            var result = passwordVerifier.Verify("VALID pASSWORD");
            Assert.True(result);
        }

        [Fact]
        public void GivenPasswordVerifierWithNumberRule_WhenGivingPasswordWithoutNumber_ThenAnExceptionIsThrown()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(1).Numbers
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify("invalid password"));
            Assert.Equal(PasswordVerifier.AMOUNT_OF_NUMBERS_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifierWithNumberRule_WhenGivingPasswordWithNumber_ThenItsVerified()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(1).Numbers
                .Build();

            var result = passwordVerifier.Verify("v4lid password");
            Assert.True(result);
        }

        [Fact]
        public void GivenPasswordVerifierWithSeveralRules_WhenGivingValidPassword_ThenItsVerified()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(8).CharactersInTotal
                .Require.AtLeast(2).UpperCaseCharacters
                .Require.AtLeast(2).Numbers
                .Build();

            var result = passwordVerifier.Verify("Super_Password123");
            Assert.True(result);
        }

        [Fact]
        public void GivenPasswordVerifierWithNonNull_WhenGivingNullAsPassword_ThenAnExceptionIsThrown()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.NonNull
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify(null));
            Assert.Equal(PasswordVerifier.PASSWORD_IS_NULL_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifierWith3OutOf5_WhenGivingAThreeFailing_ThenVerificationFails()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(8).CharactersInTotal   // False
                .Require.AtLeast(2).UpperCaseCharacters // True
                .Require.AtLeast(2).Numbers             // False
                .Require.AtLeast(3).LowerCaseCharacters // False
                .Require.NonNull                        // True
                .Require.AtLeast(3).PassingRules
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify("UUAABB"));
            Assert.Equal(PasswordVerifier.DID_NOT_FULFILL_MINIMUM_REQUIREMENT_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifierWith2OutOf5_WhenGivingAThreeFailing_ThenVerificationFails()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(8).CharactersInTotal   // False
                .Require.AtLeast(2).UpperCaseCharacters // True
                .Require.AtLeast(2).Numbers             // False
                .Require.AtLeast(3).LowerCaseCharacters // False
                .Require.NonNull                        // True
                .Require.AtLeast(2).PassingRules
                .Build();
        
            var result = passwordVerifier.Verify("UUAABB");
            Assert.True(result);
        }

        [Fact]
        public void GivenPasswordVerifierWith4OutOf5_WhenGivingATwoFailing_ThenVerificationFails()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(8).CharactersInTotal   // False
                .Require.AtLeast(2).UpperCaseCharacters // True
                .Require.AtLeast(2).Numbers             // False
                .Require.AtLeast(3).LowerCaseCharacters // True
                .Require.NonNull                        // True
                .Require.AtLeast(4).PassingRules
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify("UUAAbbb"));
            Assert.Equal(PasswordVerifier.DID_NOT_FULFILL_MINIMUM_REQUIREMENT_EXCEPTION, exception.Message);
        }
        
        [Fact]
        public void GivenPasswordVerifierWith3OutOf5_WhenGivingATwoFailing_ThenVerificationPasses()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(8).CharactersInTotal   // False
                .Require.AtLeast(2).UpperCaseCharacters // True
                .Require.AtLeast(2).Numbers             // False
                .Require.AtLeast(3).LowerCaseCharacters // True
                .Require.NonNull                        // True
                .Require.AtLeast(3).PassingRules
                .Build();

            var result = passwordVerifier.Verify("UUAAbbb");
            Assert.True(result);
        }
    }
}
