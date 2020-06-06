using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandLineParser.Core
{
    public class ParserSchema
    {
        public const string FLAG_IS_UNKNOWN_EXCEPTION = "The given flag is invalid.";

        private readonly List<ParserSchemaItem> _schemaItems;

        public ParserSchema(List<ParserSchemaItem> schemaItems) =>
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

        public int GetInteger(string flag)
        {
            var item = _schemaItems
                .SingleOrDefault(p => flag == p.Flag);

            return int.Parse(item.Value);
        }

        public bool GetBoolean(string flag)
        {
            var item = _schemaItems
                .SingleOrDefault(p => flag == p.Flag);

            return bool.Parse(item.Value);
        }
    }

    public class ParserSchemaItem
    {
        public string Flag { get; }
        public string Value { get; internal set; }

        protected ParserSchemaItem(string flag)
        {
            Flag = flag;
        }

        internal virtual void Extract(Queue<string> queue) =>
            Value = queue.Dequeue();
    }

    public class ParserSchemaBooleanItem : ParserSchemaItem
    {
        public ParserSchemaBooleanItem(string flag) :
            base(flag)
        {
            Value = false.ToString();
        }

        internal override void Extract(Queue<string> queue) =>
            Value = true.ToString();
    }

    public class ParserSchemaIntegerItem : ParserSchemaItem
    {
        public ParserSchemaIntegerItem(string flag) :
            base(flag)
        {
            Value = 0.ToString();
        }
    }
}
