using ProductPricing.Logic;

namespace ProductPricing.UnitTests;

public class PricingCalculatorWithElectronicsCategoryMust
{
    private const int CheapPrice = 100;
    private const int CheapPriceWithSmallDiscount = 80;
    private const int ExpensivePrice = 10000;
    private const int ExpensivePriceWithLargeDiscount = 9950;
 
    [Theory]
    [InlineData(CheapPrice, CheapPriceWithSmallDiscount)]
    [InlineData(ExpensivePrice, ExpensivePriceWithLargeDiscount)]
    public void ReturnSamePrice_WhenIsNotTaxableNorImported(decimal price, decimal expectedTotal)
    {
        var product = CreateProductWithElectronicsCategory(price: price);
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(expectedTotal, result);
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