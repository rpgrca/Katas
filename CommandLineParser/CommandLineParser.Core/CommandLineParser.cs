using System;
using System.Linq;

namespace CommandLineParser.Core
{
    public class CommandLineParser
    {
        public const string FLAG_IS_UNKNOWN_EXCEPTION = "The given flag is invalid.";
        public const string SCHEMA_IS_NULL_EXCEPTION = "Schema is null.";
        public const string ARGUMENTS_ARE_NULL_EXCEPTION = "Argument line is null.";
        
        private readonly object _parserSchema;
        private string _commandLine;

        public CommandLineParser(object parserSchema)
        {
            _parserSchema = parserSchema ?? throw new ArgumentException(SCHEMA_IS_NULL_EXCEPTION);
        }

        public void Parse(string commandLine)
        {
            _commandLine = commandLine ?? throw new ArgumentException(ARGUMENTS_ARE_NULL_EXCEPTION);
            if (string.IsNullOrEmpty(_commandLine))
                return;

            if (! _commandLine.StartsWith($"-{_parserSchema}"))
            {
                throw new ArgumentException(FLAG_IS_UNKNOWN_EXCEPTION);
            }
        }

        public bool GetBoolean(string flag)
        {
            return ($"-{_parserSchema}" == _commandLine);
        }

        public int GetInteger(string flag)
        {
            if (_commandLine.StartsWith($"-{_parserSchema}"))
            {
                return int.Parse(_commandLine.Substring(_parserSchema.ToString().Length + 1));
            }
            
            return 0;
        }
    }
}
