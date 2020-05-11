using System.Collections.Generic;
using System.Linq;

namespace CommandLineParser.Core
{
    public class ParserSchema
    {
        private List<ParserSchemaItem> _schemaItems;
        
        public ParserSchema(List<ParserSchemaItem> schemaItems) =>
            _schemaItems = schemaItems;

        public void Tokenize(string commandLine)
        {
            var flags = commandLine.Split(' ');

            foreach (var flag in flags)
            {
                var flagWithoutDash = RemoveDash(flag);
                var item = _schemaItems
                    .Single(p => flagWithoutDash == p.Flag);
                    
                //item.CloneWith()
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
    }

    public class ParserSchemaItem
    {
        public string Flag { get; }

        protected ParserSchemaItem(string flag) =>
            Flag = flag;
    }

    public class ParserSchemaBooleanItem : ParserSchemaItem
    {
        public ParserSchemaBooleanItem(string flag) :
            base(flag)
        {
        }
    }

    public class ParserSchemaIntegerItem : ParserSchemaItem
    {
        public ParserSchemaIntegerItem(string flag) :
            base(flag)
        {
        }
    }   
}
