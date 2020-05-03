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
        public void GivenAWardrobeMaker_WhenCombiningOfNegativeSizedWall_ThenAnExceptionIsThrown()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int>() { 1 });
            var exception = Assert.Throws<ArgumentException>(() => wardrobeMaker.GetCombinations(-1));
            Assert.Equal(WardrobeMaker.WALL_SIZE_IS_INVALID_EXCEPTION, exception.Message);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GivenAWardrobeMaker_WhenCombiningOfZeroSizedWall_ThenAnEmptyListIsReturned(int anySize)
        {
            var wardrobeMaker = new WardrobeMaker(new List<int>() { anySize });
            var wardrobes = wardrobeMaker.GetCombinations(0);
            Assert.Empty(wardrobes);
        }

        [Fact]
        public void GivenAWardrobeMakerWithWardrobesOfSizeOne_WhenCombiningForOneSizedWall_ThenOneElementIsReturned()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int>() { 1 });
            var wardrobes = wardrobeMaker.GetCombinations(1);
            Assert.Collection(wardrobes, e1 => Assert.Single(e1, 1));
        }

        [Fact]
        public void GivenAWardrobeMakerWithWardrobesOfSizeOne_WhenCombiningForTwoSizedWall_ThenTwoElementsAreReturned()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int>() { 1 });
            var wardrobes = wardrobeMaker.GetCombinations(2);
            Assert.Collection(wardrobes, e1 =>
                Assert.Collection(e1,
                    e11 => Assert.Equal(1, e11),
                    e12 => Assert.Equal(1, e12)));
        }

        [Fact]
        public void GivenAWardrobeMakerWithWardrobesOfSizeOneAndTwo_WhenCombiningForTwoSizedWall_ThenTwoElementsAreReturned()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int>(){ 1, 2 });
            var wardrobes = wardrobeMaker.GetCombinations(2);
            Assert.Collection(wardrobes,
                e1 => { Assert.Equal(1, e1[0]); Assert.Equal(1, e1[1]); },
                e2 => { Assert.Equal(2, e2[0]); });
        }

        [Fact]
        public void GivenAWardrobeMakerWithALargeSize_WhenCombiningForSmallWall_ThenAnEmptyListIsReturned()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int>() { 2 });
            var wardrobes = wardrobeMaker.GetCombinations(1);
            Assert.Empty(wardrobes);
        }

        [Fact]
        public void GivenAWardrobeMakerWithWardrobesOfSizeOneAndTwo_WhenCombiningForFourSizedWalls_ThenFiveElementsAreReturned()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int>() { 1, 2, });
            var wardrobes = wardrobeMaker.GetCombinations(4);
            Assert.Equal(5, wardrobes.Count);
            Assert.Contains(new List<int>() {1, 1, 1, 1}, wardrobes);
            Assert.Contains(new List<int>() {1, 1, 2}, wardrobes);
            Assert.Contains(new List<int>() {1, 2, 1}, wardrobes);
            Assert.Contains(new List<int>() {2, 1, 1}, wardrobes);
            Assert.Contains(new List<int>() { 2, 2 }, wardrobes);
        }

        [Fact]
        public void GivenAWardrobeMakerWithFourSizes_WhenCombiningFor250cmWall_ThenSolutionIsObtained()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int>() {50, 75, 100, 120});
            var wardrobes = wardrobeMaker.GetCombinations(250);
            Assert.Equal(17, wardrobes.Count);
            Assert.Contains(new List<int>() {50, 50, 50, 50, 50}, wardrobes);
            Assert.Contains(new List<int>() {50, 100, 100}, wardrobes);
            Assert.Contains(new List<int>() {100, 50, 100}, wardrobes);
            Assert.Contains(new List<int>() {100, 100, 50}, wardrobes);
            Assert.Contains(new List<int>() {50, 50, 50, 100}, wardrobes);
            Assert.Contains(new List<int>() {50, 50, 100, 50}, wardrobes);
            Assert.Contains(new List<int>() {50, 100, 50, 50}, wardrobes);
            Assert.Contains(new List<int>() {100, 50, 50, 50}, wardrobes);
            Assert.Contains(new List<int>() {75, 75, 100}, wardrobes);
            Assert.Contains(new List<int>() {75, 100, 75}, wardrobes);
            Assert.Contains(new List<int>() {100, 75, 75}, wardrobes);
            Assert.Contains(new List<int>() {75, 75, 50, 50}, wardrobes);
            Assert.Contains(new List<int>() {75, 50, 75, 50}, wardrobes);
            Assert.Contains(new List<int>() {75, 50, 50, 75}, wardrobes);
            Assert.Contains(new List<int>() {50, 75, 75, 50}, wardrobes);
            Assert.Contains(new List<int>() {50, 75, 50, 75}, wardrobes);
            Assert.Contains(new List<int>() {50, 50, 75, 75}, wardrobes);
        }
    }
}
