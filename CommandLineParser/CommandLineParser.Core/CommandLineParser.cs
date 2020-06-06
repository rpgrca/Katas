﻿using System;

namespace CommandLineParser.Core
{
    public class CommandLineParser
    {
        public const string SCHEMA_IS_NULL_EXCEPTION = "Schema is null.";
        public const string ARGUMENTS_ARE_NULL_EXCEPTION = "Argument line is null.";

        private readonly ParserSchema _parserSchema;
        private string _commandLine;

        public CommandLineParser(ParserSchema parserSchema) =>
            _parserSchema = parserSchema ?? throw new ArgumentException(SCHEMA_IS_NULL_EXCEPTION);

        public void Parse(string commandLine)
        {
            _commandLine = commandLine ?? throw new ArgumentException(ARGUMENTS_ARE_NULL_EXCEPTION);
            if (string.IsNullOrEmpty(_commandLine))
            {
                return;
            }

            _parserSchema.Tokenize(_commandLine);
        }

         public bool GetBoolean(string flag) =>
            _parserSchema.GetBoolean(flag);

        public int GetInteger(string flag) =>
            _parserSchema.GetInteger(flag);
    }
}