using System;
using System.Collections.Generic;
using System.Linq;
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
        public void
            GivenAListOfSizesWithValidAndInvalidValues_WhenCreatingAWardrobeMakerWithIt_ThenAnExceptionIsThrown()
        {
            var invalidAndValidValues = new List<int> {1, -1};
            var exception = Assert.Throws<ArgumentException>(() => new WardrobeMaker(invalidAndValidValues));
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
            Assert.Collection(wardrobes,
                p1 => { Assert.Equal(1, p1[0]); Assert.Equal(2, p1[1]); Assert.Equal(1, p1[2]); },
                p2 => { Assert.Equal(1, p2[0]); Assert.Equal(1, p2[1]); Assert.Equal(2, p2[2]); },
                p3 => { Assert.Equal(1, p3[0]); Assert.Equal(1, p3[1]); Assert.Equal(1, p3[2]); Assert.Equal(1, p3[3]); },
                p4 => { Assert.Equal(2, p4[0]); Assert.Equal(2, p4[1]); },
                p5 => { Assert.Equal(2, p5[0]); Assert.Equal(1, p5[1]); Assert.Equal(1, p5[2]); });
        }

        [Fact]
        public void GivenAWardrobeMakerWithFourSizes_WhenCombiningFor250cmWall_ThenSolutionIsObtained()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int>() {50, 75, 100, 120});
            var wardrobes = wardrobeMaker.GetCombinations(250);
            Assert.Collection(wardrobes,
                p1 => { Assert.Equal(50, p1[0]); Assert.Equal(100, p1[1]); Assert.Equal(100, p1[2]); },
                p2 => { Assert.Equal(50, p2[0]); Assert.Equal(100, p2[1]); Assert.Equal( 50, p2[2]); Assert.Equal(p2[3], 50); },
                p3 => { Assert.Equal(50, p3[0]); Assert.Equal(75,  p3[1]); Assert.Equal(75, p3[2]); Assert.Equal(50, p3[3]); },
                p4 => { Assert.Equal(50, p4[0]); Assert.Equal(50,  p4[1]); Assert.Equal(100, p4[2]); Assert.Equal(50, p4[3]); }, 
                p5 => { Assert.Equal(50, p5[0]); Assert.Equal(75,  p5[1]); Assert.Equal(50, p5[2]); Assert.Equal(75, p5[3]); },
                p6 => { Assert.Equal(50, p6[0]); Assert.Equal(50,  p6[1]); Assert.Equal(75, p6[2]); Assert.Equal(75, p6[3]); }, 
                p7 => { Assert.Equal(50, p7[0]); Assert.Equal(50,  p7[1]); Assert.Equal(50, p7[2]); Assert.Equal(100, p7[3]); },
                p8 => { Assert.Equal(50, p8[0]); Assert.Equal(50,  p8[1]); Assert.Equal(50, p8[2]); Assert.Equal(50, p8[3]); Assert.Equal(50, p8[4]); }, // 59*5 = 295
                p9 => { Assert.Equal(75, p9[0]); Assert.Equal(100, p9[1]); Assert.Equal(75, p9[2]); },
                pa => { Assert.Equal(75, pa[0]); Assert.Equal(75,  pa[1]); Assert.Equal(100, pa[2]); }, // 62*2 + 90 = 214
                pb => { Assert.Equal(75, pb[0]); Assert.Equal(75,  pb[1]); Assert.Equal(50, pb[2]); Assert.Equal(50, pb[3]); },
                pc => { Assert.Equal(75, pc[0]); Assert.Equal(50,  pc[1]); Assert.Equal(75, pc[2]); Assert.Equal(50, pc[3]); },
                pd => { Assert.Equal(75, pd[0]); Assert.Equal(50,  pd[1]); Assert.Equal(50, pd[2]); Assert.Equal(75, pd[3]); }, // 59*2 + 62*2 = 242
                pe => { Assert.Equal(100, pe[0]); Assert.Equal(100, pe[1]); Assert.Equal(50, pe[2]); }, // 59 + 2*90 = 239
                pf => { Assert.Equal(100, pf[0]); Assert.Equal(75, pf[1]); Assert.Equal(75, pf[2]); },
                pg => { Assert.Equal(100, pg[0]); Assert.Equal(50, pg[1]); Assert.Equal(100, pg[2]); },
                ph => { Assert.Equal(100, ph[0]); Assert.Equal(50, ph[1]); Assert.Equal(50, ph[2]); Assert.Equal(50, ph[3]); }); // 59*3 + 90 = 267
        }

        [Fact]
        public void GivenAWardrobeMaker_WhenPriceListIsNull_ThenAnExceptionIsBeThrown()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int> { 1 });
            var wardrobes = new List<List<int>> { new List<int> { 1 }};
            var exception = Assert.Throws<ArgumentException>(() => wardrobeMaker.GetBestQuotes(wardrobes, null));
            Assert.Equal(WardrobeMaker.PRICE_LIST_IS_NULL_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenAWardrobeMaker_WhenPriceListIsEmpty_ThenAnExceptionIsThrown()
        {
             var wardrobeMaker = new WardrobeMaker(new List<int> { 1 });
             var wardrobes = new List<List<int>> { new List<int> { 1 }};
             var emptyPriceList = new Dictionary<int, int> {};
             var exception = Assert.Throws<ArgumentException>(() => wardrobeMaker.GetBestQuotes(wardrobes, emptyPriceList));
             Assert.Equal(WardrobeMaker.PRICE_LIST_IS_EMPTY_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenAWardrobeMaker_WhenPriceListDoesNotHaveEverySize_ThenAnExceptionIsThrown()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int> {1});
            var wardrobes = new List<List<int>> {new List<int> {1}};
            var priceList = new Dictionary<int, int> {{2, 1}};
            var exception = Assert.Throws<ArgumentException>(() => wardrobeMaker.GetBestQuotes(wardrobes, priceList));
            Assert.Equal(WardrobeMaker.PRICE_LIST_IS_MISSING_SIZE_EXCEPTION, exception.Message);
        }

        [Fact]
        public void GivenAWardrobeMaker_WhenPriceListIsComplete_ThenPriceShouldBeCalculated()
        {
             var wardrobeMaker = new WardrobeMaker(new List<int> {1});
             var wardrobes = new List<List<int>> {new List<int> {1}};
             var priceList = new Dictionary<int, int> {{1, 10}};

             var bestQuotes = wardrobeMaker.GetBestQuotes(wardrobes, priceList);
             Assert.Single(bestQuotes, wardrobes[0]);
        }

        [Fact]
        public void GivenAWardrobeMaker_WhenTwoDifferentWardrobesAndDifferentPrices_ThenPriceShouldBeCalculated()
        {
              var wardrobeMaker = new WardrobeMaker(new List<int> {1, 2});
              var wardrobes = new List<List<int>> {new List<int> {1,1,}, new List<int> {2}};
              var priceList = new Dictionary<int, int> {{1, 6}, {2, 1}};
 
              var bestQuotes = wardrobeMaker.GetBestQuotes(wardrobes, priceList);
              Assert.Single(bestQuotes, wardrobes[1]);
        }

        [Fact]
        public void GivenAWardrobeMaker_WhenTwoDifferentWardrobesAndSamePrices_ThenAnyShouldBeReturned()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int> {1, 2});
            var wardrobes = new List<List<int>> {new List<int> {1,1}, new List<int> {2}};
            var priceList = new Dictionary<int, int> {{2, 6}, {1, 3}};

            var bestQuotes = wardrobeMaker.GetBestQuotes(wardrobes, priceList);
            Assert.Collection(bestQuotes,
                e1 => { Assert.Same(wardrobes[0], e1 );},
                e2 => { Assert.Same(wardrobes[1], e2 );});
        }

        [Fact]
        public void GivenAWardrobeMaker_WhenKataInfoIsInserted_ThenTheCheapestCombinationShouldBeReturned()
        {
            var wardrobeMaker = new WardrobeMaker(new List<int>() {50, 75, 100, 120});
            var wardrobes = wardrobeMaker.GetCombinations(250);
            var priceList = new Dictionary<int, int> {{50, 59}, {75, 62}, {100, 90}, {120, 111}};

            var bestQuotes = wardrobeMaker.GetBestQuotes(wardrobes, priceList);
            Assert.Equal(3, bestQuotes.Count);
            Assert.Equal(new List<int>() {75, 100, 75}, bestQuotes[0]);
            Assert.Equal(new List<int>() {75, 75, 100}, bestQuotes[1]);
            Assert.Equal(new List<int>() {100, 75, 75}, bestQuotes[2]);
        }
    }
}
