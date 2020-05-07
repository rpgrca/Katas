using System;
using Xunit;

namespace CommandLineParser.Core.UnitTests
{
    public class CommandLineParserShould
    {
        [Fact]
        public void GivenACommandLineParser_WhenInitializingWithNullSchema_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new CommandLineParser(null));
            Assert.Equal(CommandLineParser.SCHEMA_IS_NULL_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenACommandLineParser_WhenInitializingWithSchema_ThenItsBuilt()
        {
            var commandLineParser = new CommandLineParser(new object());
            Assert.NotNull(commandLineParser);
        }

        [Fact]
        public void GivenACommandLineParser_WhenParsingFlagNotInSchema_ThenAnExceptionIsThrown()
        {
            var commandLineParser = new CommandLineParser(new object());
            var exception = Assert.Throws<ArgumentException>(() => commandLineParser.Parse("-l"));
            Assert.Equal(CommandLineParser.FLAG_IS_UNKNOWN_EXCEPTION, exception.Message);
        }
    }
}
