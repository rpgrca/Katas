using System;

namespace PasswordVerifier.Core
{
    public class FatalException : Exception
    {
        public FatalException(string message)
            : base(message)
        {
        }
    }
}