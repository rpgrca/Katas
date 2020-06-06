using System;
using System.Collections.Generic;

namespace CommandLineParser.Core
{
    internal class ParserSchemaItem
    {
        public string Flag { get; }
        public string Value { get; internal set; }

        protected ParserSchemaItem(string flag) =>
            Flag = flag;

        public virtual void Extract(Queue<string> queue)
        {
            Value = queue.Dequeue();
            if (Value.StartsWith("-"))
            {
                throw new ArgumentException(ParserSchema.VALUE_IS_MISSING_EXCEPTION);
            }
        }
    }
}