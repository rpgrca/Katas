using System;

namespace CommandLineParser.Core
{
    public class CommandLineParser
    {
        public const string SCHEMA_IS_NULL_EXCEPTION = "Schema is null.";
        public const string ARGUMENTS_ARE_NULL_EXCEPTION = "Argument line is null.";

        private readonly ParserSchema _parserSchema;
        private readonly string _commandLine;

        public CommandLineParser(ParserSchema parserSchema, string commandLine)
        {
            _parserSchema = parserSchema ?? throw new ArgumentException(SCHEMA_IS_NULL_EXCEPTION);
            _commandLine = commandLine ?? throw new ArgumentException(ARGUMENTS_ARE_NULL_EXCEPTION);
        }

        public void Parse()
        {
            if (! string.IsNullOrEmpty(_commandLine))
            {
                _parserSchema.Tokenize(_commandLine);
            }
        }

         public bool GetBoolean(string flag) =>
            _parserSchema.GetBoolean(flag);

        public int GetInteger(string flag) =>
            _parserSchema.GetInteger(flag);
    }
}