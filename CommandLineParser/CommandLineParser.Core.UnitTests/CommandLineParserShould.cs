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
            var schema = new ParserSchemaBuilder()
                .Build();
            var commandLineParser = new CommandLineParser(schema);
            Assert.NotNull(commandLineParser);
        }

        [Fact]
        public void GivenACommandLineParser_WhenParsingFlagWithEmptySchema_ThenAnExceptionIsThrown()
        {
            var schema = new ParserSchemaBuilder()
                .Build();
            var commandLineParser = new CommandLineParser(schema);
            var exception = Assert.Throws<ArgumentException>(() => commandLineParser.Parse("-l"));
            Assert.Equal(ParserSchema.FLAG_IS_UNKNOWN_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenACommandLineParser_WhenParsingNull_ThenAnExceptionIsThrown()
        {
            var schema = new ParserSchemaBuilder()
                .Build();
            var commandLineParser = new CommandLineParser(schema);
            var exception = Assert.Throws<ArgumentException>(() => commandLineParser.Parse(null));
            Assert.Equal(CommandLineParser.ARGUMENTS_ARE_NULL_EXCEPTION,  exception.Message);
        }

        [Fact]
        public void GivenACommandLineParser_WhenParsingBooleanFlagInSchema_ThenTrueIsReturned()
        {
            var schema = new ParserSchemaBuilder()
                .AddBoolean("l")
                .Build();
            var commandLineParser = new CommandLineParser(schema);
            commandLineParser.Parse("-l");
            Assert.True(commandLineParser.GetBoolean("l"));
        }

        [Fact]
        public void GivenACommandLineParser_WhenBooleanFlagIsNotInArgs_ThenFalseIsReturned()
        {
            var schema = new ParserSchemaBuilder()
                .AddBoolean("l")
                .Build();
            var commandLineParser = new CommandLineParser(schema);
            commandLineParser.Parse(string.Empty);
            Assert.False(commandLineParser.GetBoolean("l"));
        }

        [Fact]
        public void GivenACommandLineParser_WhenBooleanFlagIsPresentAndNot_ThenTrueFalseAreReturned()
        {
            var schema = new ParserSchemaBuilder()
                .AddBoolean("p")
                .AddBoolean("l")
                .Build();
            var commandLineParser = new CommandLineParser(schema);
            commandLineParser.Parse("-p");
            Assert.True(commandLineParser.GetBoolean("p"));
            Assert.False(commandLineParser.GetBoolean("l"));
        }

        [Theory]
        [InlineData("-l 0", 0)]
        [InlineData("-l 8", 8)]
        [InlineData("-l 9", 9)]
        [InlineData("-l -1", -1)]
        public void GivenACommandLineParser_WhenParsingAFlagWithNumericValue_ThenTheValueIsReturned(string commandLine, int expectedValue)
        {
            var schema = new ParserSchemaBuilder()
                .AddInteger("l")
                .Build();
            var commandLineParser = new CommandLineParser(schema);
            commandLineParser.Parse(commandLine);
            Assert.Equal(expectedValue, commandLineParser.GetInteger("l"));
        }

        [Fact]
        public void GivenACommandLineParser_WhenNumericFlagIsMissingFromArgs_ThenZeroIsReturned()
        {
            var schema = new ParserSchemaBuilder()
                .AddInteger("l")
                .Build();
            var commandLineParser = new CommandLineParser(schema);
            commandLineParser.Parse(string.Empty);
            Assert.Equal(0, commandLineParser.GetInteger("l"));
        }

        [Fact]
        public void GivenACommandLineParser_WhenSameFlagAppearsMultipleTimes_ThenLastOneIsReturned()
        {
            var schema = new ParserSchemaBuilder()
                .AddInteger("l")
                .Build();

            var commandLineParser = new CommandLineParser(schema);
            commandLineParser.Parse("-l 4 -l 3 -l 2");
            Assert.Equal(2, commandLineParser.GetInteger("l"));
        }

        [Fact]
        public void GivenACommandLineParser_WhenExpectingValueAndGetArgument_ThenAnExceptionIsThrown()
        {
            var schema = new ParserSchemaBuilder()
                .AddInteger("l")
                .AddBoolean("r")
                .Build();

            var commandLineParser = new CommandLineParser(schema);
            var exception = Assert.Throws<ArgumentException>(() => commandLineParser.Parse("-l -r"));
            Assert.Equal(ParserSchema.ARGUMENT_IS_INVALID_EXCEPTION, exception.Message);
        }
    }
}
