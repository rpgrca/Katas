using System;
using System.Collections.Generic;

namespace PasswordVerifier.Core
{
    public class PasswordVerifier
    {
        public const string RULES_IS_NULL_EXCEPTION = "Rules cannot be null.";
        public const string PASSWORD_LENGTH_IS_INVALID_EXCEPTION = "Password length is invalid.";
        public const string PASSWORD_IS_NULL_EXCEPTION = "Password cannot be null.";
        public const string AMOUNT_OF_UPPERCASE_IS_INVALID_EXCEPTION = "Amount of uppercase characters is invalid.";
        public const string AMOUNT_OF_LOWERCASE_IS_INVALID_EXCEPTION = "Amount of lowercase characters is invalid.";
        public const string AMOUNT_OF_NUMBERS_IS_INVALID_EXCEPTION = "Amount of numeric characters is invalid.";

        private readonly List<Func<string, bool>> _rules;

        public PasswordVerifier(List<Func<string, bool>> rules)
        {
            _rules = rules ?? throw new ArgumentException(RULES_IS_NULL_EXCEPTION);
        }
        
        public bool Verify(string password)
        {
            _ = password ?? throw new ArgumentException(PASSWORD_IS_NULL_EXCEPTION);

            _rules.ForEach(a => a(password));
            return true;
        }
    }
}
