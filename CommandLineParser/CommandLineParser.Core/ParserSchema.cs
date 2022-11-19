using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandLineParser.Core
{
    public class ParserSchema
    {
        public const string FLAG_IS_UNKNOWN_EXCEPTION = "The given flag is invalid.";
        public const string VALUE_IS_MISSING_EXCEPTION = "The flag is missing an argument.";
        public const string ARGUMENT_IS_INVALID_EXCEPTION = "The argument is invalid.";

        private readonly List<ParserSchemaItem> _schemaItems;

        internal ParserSchema(List<ParserSchemaItem> schemaItems) =>
            _schemaItems = schemaItems;

        public void Tokenize(string commandLine)
        {
            var flags = commandLine.Split(' ');
            var queue = new Queue<string>(flags);
            while (queue.Count > 0)
            {
                var flagWithoutDash = RemoveDash(queue.Dequeue());
                var item = _schemaItems
                    .SingleOrDefault(p => flagWithoutDash == p.Flag);

                if (item == null)
                {
                    throw new ArgumentException(FLAG_IS_UNKNOWN_EXCEPTION);
                }

                item.Extract(queue);
            }
        }

        private string RemoveDash(string flag) =>
            flag[0] == '-'? flag.Substring(1) : flag;

        public int GetInteger(string flag) =>
            int.Parse(GetValueFor(flag));

        public bool GetBoolean(string flag) =>
            bool.Parse(GetValueFor(flag));

        private string GetValueFor(string flag) =>
            _schemaItems
                .SingleOrDefault(p => flag == p.Flag)
                ?. Value
                ?? throw new ArgumentException(FLAG_IS_UNKNOWN_EXCEPTION);
    }
}