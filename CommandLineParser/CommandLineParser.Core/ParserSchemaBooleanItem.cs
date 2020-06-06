using System.Collections.Generic;

namespace CommandLineParser.Core
{
    internal class ParserSchemaBooleanItem : ParserSchemaItem
    {
        public ParserSchemaBooleanItem(string flag) :
            base(flag) =>
            Value = false.ToString();

        public override void Extract(Queue<string> queue) =>
            Value = true.ToString();
    }
}