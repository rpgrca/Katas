using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

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
                var flagWithDash = queue.Dequeue();
                var flagWithoutDash = RemoveDash(flagWithDash);
                var item = _schemaItems
                    .SingleOrDefault(p => flagWithoutDash == p.Flag);

                if (item == null)
                {
                    throw new ArgumentException(FLAG_IS_UNKNOWN_EXCEPTION);
                }

                item.Extract(queue);
            }
        }

        private string RemoveDash(string flag)
        {
            if (flag.StartsWith("-"))
            {
                return flag.Remove(0, 1);
            }

            return flag;
        }

        public int GetInteger(string flag) =>
            _schemaItems
                .Where(p => flag == p.Flag)
                .Select(p => int.Parse(p.Value))
                .First();

        public bool GetBoolean(string flag) =>
            _schemaItems
                .Where(p => flag == p.Flag)
                .Select(p => bool.Parse(p.Value))
                .First();
    }
}