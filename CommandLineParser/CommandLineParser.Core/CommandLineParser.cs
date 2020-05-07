using System;

namespace CommandLineParser.Core
{
    public class CommandLineParser
    {
        public const string FLAG_IS_UNKNOWN_EXCEPTION = "The given flag is invalid.";
        public const string SCHEMA_IS_NULL_EXCEPTION = "Schema is null.";
        public CommandLineParser(object parserSchema)
        {
            _ = parserSchema ?? throw new ArgumentException(SCHEMA_IS_NULL_EXCEPTION);
        }

        public void Parse(string commandLine)
        {
            throw new ArgumentException(FLAG_IS_UNKNOWN_EXCEPTION);
        }
    }
}
