using ProductPricing.Logic;

namespace ProductPricing.UnitTests;

public class PricingCalculatorMust
{
    [Fact]
    public void ReturnSamePrice_WhenCategoryIsUnknownAndIsNotTaxableNorImported()
    {
        var product = new Product
        {
            Name = "Shirt",
            BasePrice = 100,
            Category = "Unknown",
            IsTaxable = false,
            IsImported = false
        };
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(100, result);
    }
}