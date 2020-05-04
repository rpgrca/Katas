using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordVerifier.Core
{
    public class PasswordVerifierBuilder
    {
        public class PasswordVerifierBuilderContract
        {
            private readonly PasswordVerifierBuilder _passwordBuilder;
            private readonly int _value;

            public PasswordVerifierBuilderContract(PasswordVerifierBuilder passwordBuilder, int value)
            {
                _passwordBuilder = passwordBuilder;
                _value = value;
            }

            public PasswordVerifierBuilder CharactersInTotal()
            {
                if (_value < 1)
                {
                    throw new ArgumentException(MINIMUM_LENGTH_IS_INVALID_EXCEPTION);
                }

                _passwordBuilder._rules.Add(s =>
                    s.Length < _value
                    ? throw new ArgumentException(PasswordVerifier.PASSWORD_LENGTH_IS_INVALID_EXCEPTION)
                    : true);

                return _passwordBuilder;
            }

            public PasswordVerifierBuilder UpperCaseCharacters()
            {
                if (_value < 0)
                {
                    throw new ArgumentException(UPPERCASE_CHARACTER_AMOUNT_IS_INVALID);
                }

                _passwordBuilder._rules.Add(s =>
                    s.Count(char.IsUpper) < _value
                    ? throw new ArgumentException(PasswordVerifier.AMOUNT_OF_UPPERCASE_IS_INVALID_EXCEPTION)
                    : true);

                return _passwordBuilder;
            }
        }
        
        public const string MINIMUM_LENGTH_IS_INVALID_EXCEPTION = "Minimum length is invalid.";
        public const string UPPERCASE_CHARACTER_AMOUNT_IS_INVALID = "Minimum amount of uppercase characters is invalid.";

        private readonly List<Func<string, bool>> _rules = new List<Func<string, bool>>();

        public PasswordVerifierBuilderContract RequireAtLeast(int amount) =>
            new PasswordVerifierBuilderContract(this, amount);

        public PasswordVerifier Build() =>
            new PasswordVerifier(_rules);
    }
}