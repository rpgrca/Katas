using ProductPricing.Logic;

namespace ProductPricing.UnitTests;

public class PricingCalculatorWithElectronicsCategoryMust
{
    private const decimal CheapPrice = 100;
    private const decimal CheapPriceWithSmallDiscount = 80;
 
    [Fact]
    public void ReturnSamePrice_WhenIsNotTaxableNorImported()
    {
        var product = CreateProductWithElectronicsCategory();
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(CheapPriceWithSmallDiscount, result);
    }

    private static Product CreateProductWithElectronicsCategory(decimal price = CheapPrice,
        bool taxable = false, bool imported = false) =>
        new()
        {
            Name = "Shirt",
            BasePrice = price,
            Category = "Electronics",
            IsTaxable = taxable,
            IsImported = imported
        };
}