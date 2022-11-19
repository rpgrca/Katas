using System;
using System.Collections.Generic;

namespace CommandLineParser.Core
{
    internal class ParserSchemaIntegerItem : ParserSchemaItem
    {
        public ParserSchemaIntegerItem(string flag) : base(flag) =>
            Value = 0.ToString();

        public override void Extract(Queue<string> queue)
        {
            Value = queue.Dequeue();
            if (! int.TryParse(Value, out var _))
            {
                throw new ArgumentException(ParserSchema.ARGUMENT_IS_INVALID_EXCEPTION);
            }
        }
    }
}