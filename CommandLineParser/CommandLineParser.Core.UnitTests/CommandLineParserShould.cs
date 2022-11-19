using System;
using Xunit;

namespace CommandLineParser.Core.UnitTests
{
    public class CommandLineParserShould
    {
        [Fact]
        public void GivenACommandLineParser_WhenInitializingWithNullSchema_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new CommandLineParser(null, null));
            Assert.Equal(CommandLineParser.SCHEMA_IS_NULL_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenACommandLineParser_WhenInitializingWithSchemaAndCommandLine_ThenItsBuilt()
        {
            var schema = new ParserSchemaBuilder()
                .Build();
            var commandLineParser = new CommandLineParser(schema, string.Empty);
            Assert.NotNull(commandLineParser);
        }

        [Fact]
        public void GivenACommandLineParser_WhenParsingFlagWithEmptySchema_ThenAnExceptionIsThrown()
        {
            var schema = new ParserSchemaBuilder()
                .Build();
            var commandLineParser = new CommandLineParser(schema, "-l");
            var exception = Assert.Throws<ArgumentException>(() => commandLineParser.Parse());
            Assert.Equal(ParserSchema.FLAG_IS_UNKNOWN_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenACommandLineParser_WhenParsingNull_ThenAnExceptionIsThrown()
        {
            var schema = new ParserSchemaBuilder()
                .Build();
            var exception = Assert.Throws<ArgumentException>(() => new CommandLineParser(schema, null));
            Assert.Equal(CommandLineParser.ARGUMENTS_ARE_NULL_EXCEPTION,  exception.Message);
        }

        [Fact]
        public void GivenACommandLineParser_WhenParsingBooleanFlagInSchema_ThenTrueIsReturned()
        {
            var schema = new ParserSchemaBuilder()
                .AddBoolean("l")
                .Build();
            var commandLineParser = new CommandLineParser(schema, "-l");
            commandLineParser.Parse();
            Assert.True(commandLineParser.GetBoolean("l"));
        }

        [Fact]
        public void GivenACommandLineParser_WhenBooleanFlagIsNotInArgs_ThenFalseIsReturned()
        {
            var schema = new ParserSchemaBuilder()
                .AddBoolean("l")
                .Build();
            var commandLineParser = new CommandLineParser(schema, string.Empty);
            Assert.False(commandLineParser.GetBoolean("l"));
        }

        [Fact]
        public void GivenACommandLineParser_WhenBooleanFlagIsPresentAndNot_ThenTrueFalseAreReturned()
        {
            var schema = new ParserSchemaBuilder()
                .AddBoolean("p")
                .AddBoolean("l")
                .Build();
            var commandLineParser = new CommandLineParser(schema, "-p");
            commandLineParser.Parse();
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
            var commandLineParser = new CommandLineParser(schema, commandLine);
            commandLineParser.Parse();
            Assert.Equal(expectedValue, commandLineParser.GetInteger("l"));
        }

        [Fact]
        public void GivenACommandLineParser_WhenNumericFlagIsMissingFromArgs_ThenZeroIsReturned()
        {
            var schema = new ParserSchemaBuilder()
                .AddInteger("l")
                .Build();
            var commandLineParser = new CommandLineParser(schema, string.Empty);
            commandLineParser.Parse();
            Assert.Equal(0, commandLineParser.GetInteger("l"));
        }

        [Fact]
        public void GivenACommandLineParser_WhenSameFlagAppearsMultipleTimes_ThenLastOneIsReturned()
        {
            var schema = new ParserSchemaBuilder()
                .AddInteger("l")
                .Build();

            var commandLineParser = new CommandLineParser(schema, "-l 4 -l 3 -l 2");
            commandLineParser.Parse();
            Assert.Equal(2, commandLineParser.GetInteger("l"));
        }

        [Fact]
        public void GivenACommandLineParser_WhenExpectingValueAndGetArgument_ThenAnExceptionIsThrown()
        {
            var schema = new ParserSchemaBuilder()
                .AddInteger("l")
                .AddBoolean("r")
                .Build();

            var commandLineParser = new CommandLineParser(schema, "-l -r");
            var exception = Assert.Throws<ArgumentException>(() => commandLineParser.Parse());
            Assert.Equal(ParserSchema.ARGUMENT_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenANewCommandLineParser_WhenRequestingInvalidIntegerFlag_ThenAnExceptionIsThrown()
        {
            var schema = new ParserSchemaBuilder()
                .Build();

            var commandLineParser = new CommandLineParser(schema, string.Empty);
            var exception = Assert.Throws<ArgumentException>(() => commandLineParser.GetInteger("m"));
            Assert.Equal(ParserSchema.FLAG_IS_UNKNOWN_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenANewCommandLineParser_WhenRequestingInvalidBooleanFlag_ThenAnExceptionIsThrown()
        {
            var schema = new ParserSchemaBuilder()
                .Build();

            var commandLineParser = new CommandLineParser(schema, string.Empty);
            var exception = Assert.Throws<ArgumentException>(() => commandLineParser.GetBoolean("m"));
            Assert.Equal(ParserSchema.FLAG_IS_UNKNOWN_EXCEPTION, exception.Message);
        }
    }
}