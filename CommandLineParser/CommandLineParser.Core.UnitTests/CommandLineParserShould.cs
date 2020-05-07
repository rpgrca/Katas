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
        public void GivenACommandLineParser_WhenParsingFlagWithEmptySchema_ThenAnExceptionIsThrown()
        {
            var commandLineParser = new CommandLineParser(new object());
            var exception = Assert.Throws<ArgumentException>(() => commandLineParser.Parse("-l"));
            Assert.Equal(CommandLineParser.FLAG_IS_UNKNOWN_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenACommandLineParser_WhenParsingNull_ThenAnExceptionIsThrown()
        {
            var commandLineParser = new CommandLineParser(new object());
            var exception = Assert.Throws<ArgumentException>(() => commandLineParser.Parse(null));
            Assert.Equal(CommandLineParser.ARGUMENTS_ARE_NULL_EXCEPTION,  exception.Message);
        }

        [Fact]
        public void GivenACommandLineParser_WhenParsingBooleanFlagInSchema_ThenTrueIsReturned()
        {
            var commandLineParser = new CommandLineParser("l");
            commandLineParser.Parse("-l");
            Assert.True(commandLineParser.GetBoolean("l"));
        }

        [Fact]
        public void GivenACommandLineParser_WhenBooleanFlagIsNotInArgs_ThenFalseIsReturned()
        {
            var commandLineParser = new CommandLineParser("l");
            commandLineParser.Parse("");
            Assert.False(commandLineParser.GetBoolean("l"));
        }

        [Theory]
        [InlineData("-l 0", 0)]
        [InlineData("-l 8", 8)]
        [InlineData("-l 9", 9)]
        public void GivenACommandLineParser_WhenParsingAFlagWithNumericValue_ThenTheValueIsReturned(string commandLine, int expectedValue)
        {
            var commandLineParser = new CommandLineParser("l");
            commandLineParser.Parse(commandLine);
            var value = commandLineParser.GetInteger("l");
            Assert.Equal(expectedValue, value);
        }

        [Fact]
        public void GivenACommandLineParser_WhenNumericFlagIsMissingFromArgs_ThenZeroIsReturned()
        {
            var commandLineParser = new CommandLineParser("l");
            commandLineParser.Parse("");
            var value = commandLineParser.GetInteger("l");
            Assert.Equal(0, value);
        }
    }
}
