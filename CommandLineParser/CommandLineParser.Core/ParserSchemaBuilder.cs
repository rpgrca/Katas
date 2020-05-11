using System.Collections.Generic;

namespace CommandLineParser.Core
{
    public class ParserSchemaBuilder
    {
        private readonly List<ParserSchemaItem> _items;

        public ParserSchemaBuilder() =>
            _items = new List<ParserSchemaItem>();

        public ParserSchemaBuilder AddBoolean(string flag)
        {
            _items.Add(new ParserSchemaBooleanItem(flag));
            return this;
        }

        public ParserSchemaBuilder AddInteger(string flag)
        {
            _items.Add(new ParserSchemaIntegerItem(flag));
            return this;
        }

        public ParserSchema Build() =>
            new ParserSchema(_items);
    }


}