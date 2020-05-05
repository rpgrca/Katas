using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordVerifier.Core
{
    public class PasswordVerifierBuilder
    {
        public class PasswordVerifierBuilderAtLeastContract
        {
            private readonly PasswordVerifierBuilder _passwordBuilder;
            private readonly int _value;

            public PasswordVerifierBuilderAtLeastContract(PasswordVerifierBuilder passwordBuilder, int value)
            {
                _passwordBuilder = passwordBuilder;
                _value = value;
            }

            public PasswordVerifierBuilder CharactersInTotal
            {
                get
                {
                    if (_value < 0)
                    {
                        throw new ArgumentException(MINIMUM_LENGTH_IS_INVALID_EXCEPTION);
                    }

                    _passwordBuilder._rules.Add(s =>
                        (s?.Length ?? 0) < _value
                            ? throw new ArgumentException(PasswordVerifier.PASSWORD_LENGTH_IS_INVALID_EXCEPTION)
                            : true);

                    return _passwordBuilder;
                }
            }

            public PasswordVerifierBuilder UpperCaseCharacters
            {
                get
                {
                    if (_value < 0)
                    {
                        throw new ArgumentException(UPPERCASE_CHARACTER_AMOUNT_IS_INVALID);
                    }

                    _passwordBuilder._rules.Add(s =>
                        (s?.Count(char.IsUpper) ?? 0) < _value
                            ? throw new ArgumentException(PasswordVerifier.AMOUNT_OF_UPPERCASE_IS_INVALID_EXCEPTION)
                            : true);

                    return _passwordBuilder;
                }
            }

            public PasswordVerifierBuilder LowerCaseCharacters
            {
                get
                {
                    if (_value < 0)
                    {
                        throw new ArgumentException(LOWERCASE_CHARACTER_AMOUNT_IS_INVALID);
                    }

                    _passwordBuilder._rules.Add(s =>
                        (s?.Count(char.IsLower) ?? 0) < _value
                            ? throw new ArgumentException(PasswordVerifier.AMOUNT_OF_LOWERCASE_IS_INVALID_EXCEPTION)
                            : true);

                    return _passwordBuilder;
                }
            }

            public PasswordVerifierBuilder Numbers
            {
                get
                {
                    if (_value < 0)
                    {
                        throw new ArgumentException(NUMBER_AMOUNT_IS_INVALID_EXCEPTION);
                    }

                    _passwordBuilder._rules.Add(s =>
                        (s?.Count(char.IsNumber) ?? 0) < _value
                            ? throw new ArgumentException(PasswordVerifier.AMOUNT_OF_NUMBERS_IS_INVALID_EXCEPTION)
                            : true);

                    return _passwordBuilder;
                }
            }

            public PasswordVerifierBuilder PassingRules
            {
                get
                {
                    if (_value <= 0)
                    {
                        throw new ArgumentException(NUMBER_OF_MINIMUM_REQUIREMENTS_IS_INVALID_EXCEPTION);
                    }
                    
                    var newRules = new List<Func<string, bool>>();

                    foreach (var rule in _passwordBuilder._rules)
                    {
                        newRules.Add(s =>
                        {
                            try
                            {
                                return rule(s);
                            }
                            catch (Exception)
                            {
                                return false;
                            }
                        });
                    }

                    _passwordBuilder._rules.Clear();
                    _passwordBuilder._rules.AddRange(newRules);

                    _passwordBuilder._verificator = (r, s) =>
                        r.Count(q => q(s)) < _value
                            ? throw new ArgumentException(PasswordVerifier.DID_NOT_FULFILL_MINIMUM_REQUIREMENT_EXCEPTION)
                            : true;

                    return _passwordBuilder;
                }
            }
        }

        public class PasswordVerifierBuilderRequirements
        {
            private readonly PasswordVerifierBuilder _passwordBuilder;

            public PasswordVerifierBuilderRequirements(PasswordVerifierBuilder passwordBuilder) =>
                _passwordBuilder = passwordBuilder;

            public PasswordVerifierBuilderAtLeastContract AtLeast(int value) =>
                new PasswordVerifierBuilderAtLeastContract(_passwordBuilder, value);

            public PasswordVerifierBuilder NonNull
            {
                get
                {
                    _passwordBuilder._rules.Add(s =>
                        s is null
                    ? throw new ArgumentException(PasswordVerifier.PASSWORD_IS_NULL_EXCEPTION)
                    : true);

                    return _passwordBuilder;
                }
            }
                
        }
        
        public const string MINIMUM_LENGTH_IS_INVALID_EXCEPTION = "Minimum length is invalid.";
        public const string UPPERCASE_CHARACTER_AMOUNT_IS_INVALID = "Minimum amount of uppercase characters is invalid.";
        public const string LOWERCASE_CHARACTER_AMOUNT_IS_INVALID = "Minimum amount of lowercase characters is invalid.";
        public const string NUMBER_AMOUNT_IS_INVALID_EXCEPTION = "Minimum amount of numbers is invalid.";
        public const string NUMBER_OF_MINIMUM_REQUIREMENTS_IS_INVALID_EXCEPTION = "Minimum requirements is invalid.";

        private readonly List<Func<string, bool>> _rules = new List<Func<string, bool>>();
        private Func<List<Func<string, bool>>, string, bool> _verificator =
            (p, s) =>
            {
                p.ForEach(q => q(s));
                return true;
            };

        public PasswordVerifierBuilderRequirements Require =>
            new PasswordVerifierBuilderRequirements(this);
        
        public PasswordVerifier Build() =>
            new PasswordVerifier(_rules, _verificator);
    }
}