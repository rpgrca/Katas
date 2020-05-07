using System;
using Xunit;

namespace CommandLineParser.Core.UnitTests
{
    public class CommandLineParserShould
    {
        [Fact]
        public void Test1()
        {
            var exception = Assert.Throws<ArgumentException>(() => new CommandLineParser(null));
            Assert.Equal(CommandLineParser.SCHEMA_IS_NULL_EXCEPTION, exception.Message);
        }

        [Fact]
        public void Test2()
        {
            var commandLineParser = new CommandLineParser(new object());
            Assert.NotNull(commandLineParser);
        }

        [Fact]
        public void Test3()
        {
            var commandLineParser = new CommandLineParser(new object());
            var exception = Assert.Throws<ArgumentException>(() => commandLineParser.Parse("-l"));
            Assert.Equal(CommandLineParser.FLAG_IS_UNKNOWN_EXCEPTION, exception.Message);
        }
    }
}
