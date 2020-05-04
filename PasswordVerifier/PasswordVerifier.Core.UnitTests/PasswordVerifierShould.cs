using System;
using Xunit;
using PasswordVerifier.Core;

namespace PasswordVerifier.Core.UnitTests
{
    public class PasswordVerifierShould
    {
        [Theory]
        [InlineData("anyPassword")]
        [InlineData(null)]
        [InlineData("")]
        public void GivenPasswordVerifierWithoutRules_WhenGivenAnyPassword_ShouldVerifyIt(string anyPassword)
        {
            var passwordVerifier = new PasswordVerifier();
            var result = passwordVerifier.Verify(anyPassword);
            Assert.True(result);
        }
    }
}
