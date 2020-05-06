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
        public void GivenPasswordVerifier_WhenNullVerifierIsGiven_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new PasswordVerifier(new List<Func<string, bool>> {}, null));
            Assert.Equal(PasswordVerifier.VERIFIER_IS_NULL_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData("anyPassword")]
        [InlineData("")]
        [InlineData(null)]
        public void GivenPasswordVerifierWithoutRules_WhenGivenAnyPassword_ThenItsVerified(string anyPassword)
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Build();
            var result = passwordVerifier.Verify(anyPassword);
            Assert.True(result);
        }

        [Theory]
        [InlineData("short")]
        [InlineData("12345678")]
        [InlineData("")]
        [InlineData(null)]
        public void GivenPasswordVerifierWithLengthRule_WhenGivingShorterPassword_ThenAnExceptionIsThrown(string shortPassword)
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(9).CharactersInTotal
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify(shortPassword));
            Assert.Equal(PasswordVerifier.PASSWORD_LENGTH_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenANewPasswordVerifierBuilder_WhenAddingZeroLength_ThenItsAccepted(string zeroLengthPassword)
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(0).CharactersInTotal
                .Build();

            var result = passwordVerifier.Verify(zeroLengthPassword);
            Assert.True(result);
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

        [Theory]
        [InlineData("invalid password")]
        [InlineData("")]
        [InlineData(null)]
        public void GivenPasswordVerifierWithUpperCaseRule_WhenGivingPasswordWithoutUpperCaseLetter_ThenAnExceptionIsThrown(string invalidPassword)
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(1).UpperCaseCharacters
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify(invalidPassword));
            Assert.Equal(PasswordVerifier.AMOUNT_OF_UPPERCASE_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenPasswordVerifierWithUpperCaseZeroRule_WhenGivingPasswordWithoutUpperCaseLetter_ThenItVerifiesCorrectly()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(0).UpperCaseCharacters
                .Build();

            var result = passwordVerifier.Verify("valid password");
            Assert.True(result);
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

        [Theory]
        [InlineData("INVALID PASSWORD")]
        [InlineData("")]
        [InlineData(null)]
        public void GivenPasswordVerifierWithLowerCaseRule_WhenGivingPasswordWithoutLowerCaseLetter_ThenAnExceptionIsThrown(string invalidPassword)
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(1).LowerCaseCharacters
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify(invalidPassword));
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
        public void GivenPasswordVerifierWithLowerCaseZeroRule_WhenGivingPasswordWithLowerCaseLetter_ThenItsVerified()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(0).LowerCaseCharacters
                .Build();

            var result = passwordVerifier.Verify("VALID PASSWORD");
            Assert.True(result);
        }

        [Theory]
        [InlineData("invalid password")]
        [InlineData("")]
        [InlineData(null)]
        public void GivenPasswordVerifierWithNumberRule_WhenGivingPasswordWithoutNumber_ThenAnExceptionIsThrown(string invalidPassword)
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(1).Numbers
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => passwordVerifier.Verify(invalidPassword));
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
        public void GivenPasswordVerifierWithNumberZeroRule_WhenGivingPasswordWithNumber_ThenItsVerified()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(0).Numbers
                .Build();

            var result = passwordVerifier.Verify("Valid password");
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

        [Fact]
        public void GivenPasswordVerifierWith5OutOf5_WhenGivingZeroFailing_ThenVerificationPasses()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.AtLeast(8).CharactersInTotal
                .Require.AtLeast(2).UpperCaseCharacters
                .Require.AtLeast(2).Numbers
                .Require.AtLeast(3).LowerCaseCharacters
                .Require.NonNull
                .Require.AtLeast(5).PassingRules
                .Build();

            var result = passwordVerifier.Verify("Super_Password123");
            Assert.True(result);
        }

        [Theory]
        [InlineData("INVALID PASSWORD")]
        [InlineData("")]
        [InlineData(null)]
        public void GivenPasswordVerifierWithRequiredRule_WhenNotFulfillingRule_ThenVerificationFails(string invalidPassword)
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.Always(1).LowerCaseCharacters
                .Build();

            var exception = Assert.Throws<FatalException>(() => passwordVerifier.Verify(invalidPassword));
            Assert.Equal(PasswordVerifier.AMOUNT_OF_LOWERCASE_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData(1, "valid password")]
        [InlineData(0, "VALID PASSWORD")]
        public void GivenPasswordVerifierWithRequiredRule_WhenFulfillingRule_ThenVerificationPasses(int minimumRequired, string expectedPassword)
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.Always(minimumRequired).LowerCaseCharacters
                .Build();

            var result = passwordVerifier.Verify(expectedPassword);
            Assert.True(result);
        }

        [Fact]
        public void GivenPasswordVerifierWithRequiredRule_WhenValidPasswordIsGiven_ThenItsVerified()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.Always(1).LowerCaseCharacters
                .Require.AtLeast(1).UpperCaseCharacters
                .Require.AtLeast(1).Numbers
                .Require.AtLeast(1).PassingRules
                .Build();

            var result = passwordVerifier.Verify("aUUUUU1");
            Assert.True(result);
        }

        [Fact]
        public void GivenPasswordVerifierWithRequiredRule_WhenInvalidPasswordIsGiven_ThenItsNotVerified()
        {
            var passwordVerifier = new PasswordVerifierBuilder()
                .Require.Always(1).LowerCaseCharacters
                .Require.AtLeast(1).UpperCaseCharacters
                .Require.AtLeast(1).Numbers
                .Require.AtLeast(2).PassingRules
                .Build();

            var exception = Assert.Throws<FatalException>(() => passwordVerifier.Verify("AUUUUU1"));
            Assert.Equal(PasswordVerifier.AMOUNT_OF_LOWERCASE_IS_INVALID_EXCEPTION, exception.Message);
        }

         [Fact]
         public void GivenPasswordVerifierWithRequiredPassingRule_WhenValidPasswordIsGiven_ThenItsVerified()
         {
             var passwordVerifier = new PasswordVerifierBuilder()
                 .Require.Always(1).LowerCaseCharacters
                 .Require.AtLeast(1).PassingRules
                 .Build();
 
             var result = passwordVerifier.Verify("aUUUUU1");
             Assert.True(result);
         }
    }
}
