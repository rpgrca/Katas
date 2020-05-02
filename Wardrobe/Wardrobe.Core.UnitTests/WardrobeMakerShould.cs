using System;
using System.Collections.Generic;
using Xunit;

namespace Wardrobe.Core.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void GivenANullListOfSizes_WhenCreatingAWardrobeMakerWithIt_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new WardrobeMaker(null));
            Assert.Equal(WardrobeMaker.SIZE_LIST_IS_NULL_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenAnEmptyListOfSizes_WhenCreatingAWardrobeMakerWithIt_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new WardrobeMaker(new List<int>()));
            Assert.Equal(WardrobeMaker.SIZE_LIST_IS_EMPTY_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenAListOfSizesWithInvalidValues_WhenCreatingAWardrobeMakerWithIt_ThenAnExceptionIsThrown(int anyInvalidValue)
        {
            var exception = Assert.Throws<ArgumentException>(() => new WardrobeMaker(new List<int>() { anyInvalidValue }));
            Assert.Equal(WardrobeMaker.SIZE_LIST_CONTAINS_INVALID_VALUES_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenAListOfSizes_WhenCreatingAWardrobeMakerWithIt_ThenAnInstanceShouldBeReturned()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int> { 50, 75, 100, 120 });
            Assert.NotNull(wardrobeMaker);
        }

        [Fact]
        public void GivenAWardrobeMaker_WhenGettingCombinationsOfNegativeSizedWall_ThenAnExceptionIsThrown()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int>() { 1 });
            var exception = Assert.Throws<ArgumentException>(() => wardrobeMaker.GetCombinations(-1));
            Assert.Equal(WardrobeMaker.WALL_SIZE_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GivenAWardrobeMaker_WhenGettingCombinationsOfZeroSizedWall_ThenAnEmptyListIsReturned(int anySize)
        {
            var wardrobeMaker = new WardrobeMaker(new List<int>() { anySize });
            var wardrobes = wardrobeMaker.GetCombinations(0);
            Assert.Empty(wardrobes);
        }
    }
}
