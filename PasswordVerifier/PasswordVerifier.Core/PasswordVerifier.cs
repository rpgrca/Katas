﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordVerifier.Core
{
    public class PasswordVerifier
    {
        public const string RULES_IS_NULL_EXCEPTION = "Rules cannot be null.";
        public const string VERIFICATOR_IS_NULL_EXCEPTION = "Verificator cannot be null.";
        public const string PASSWORD_LENGTH_IS_INVALID_EXCEPTION = "Password length is invalid.";
        public const string PASSWORD_IS_NULL_EXCEPTION = "Password cannot be null.";
        public const string AMOUNT_OF_UPPERCASE_IS_INVALID_EXCEPTION = "Amount of uppercase characters is invalid.";
        public const string AMOUNT_OF_LOWERCASE_IS_INVALID_EXCEPTION = "Amount of lowercase characters is invalid.";
        public const string AMOUNT_OF_NUMBERS_IS_INVALID_EXCEPTION = "Amount of numeric characters is invalid.";
        public const string DID_NOT_FULLFILL_MINIMUM_REQUIREMENT_EXCEPTION = "Did not pass enough validations.";

        private readonly List<Func<string, bool>> _rules;
        private Func<List<Func<string, bool>>, string, bool> _verificationMethod;

        public PasswordVerifier(List<Func<string, bool>> rules, Func<List<Func<string, bool>>, string, bool> verification)
        {
            _verificationMethod = verification ?? throw new ArgumentException(VERIFICATOR_IS_NULL_EXCEPTION);
            _rules = rules ?? throw new ArgumentException(RULES_IS_NULL_EXCEPTION);
        }

        public bool Verify(string password)
        {
            var result = _verificationMethod(_rules, password);
            return result;
        }
    }
}
