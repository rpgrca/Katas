using ProductPricing.Logic;

namespace ProductPricing.UnitTests;

public class PricingCalculatorWithClothingCategoryMust
{
    public const int CheapPrice = 10;
    public const double CheapPriceWith5PercentDiscount = 9.5;
    public const int ExpensivePrice = 100;
    public const double ExpensivePriceWith10PercentDiscount = 90;

    [Theory]
    [InlineData(CheapPrice, CheapPriceWith5PercentDiscount)]
    [InlineData(ExpensivePrice, ExpensivePriceWith10PercentDiscount)]
    public void ReturnSamePrice_WhenIsNotTaxableNorImported(decimal price, decimal expectedTotal)
    {
        var product = CreateProductWithClothingCategory(price: price);
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(expectedTotal, result);
    }

    private static Product CreateProductWithClothingCategory(decimal price = CheapPrice,
        bool taxable = false, bool imported = false) =>
        new()
        {
            Name = "Shirt",
            BasePrice = price,
            Category = "Clothing",
            IsTaxable = taxable,
            IsImported = imported
        };
}
