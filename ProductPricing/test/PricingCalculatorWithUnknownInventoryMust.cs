using ProductPricing.Logic;

namespace ProductPricing.UnitTests;

public class PricingCalculatorWithUnknownCategoryMust
{
    private const decimal Price = 100;
    private const decimal PriceWithImportDuties = 105;
    private const decimal PriceWithTax = 110;

    [Fact]
    public void ReturnSamePrice_WhenIsNotTaxableNorImported()
    {
        var product = CreateProductWithUnknownCategory();
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(Price, result);
    }

    private static Product CreateProductWithUnknownCategory(decimal price = Price,
        bool taxable = false, bool imported = false) =>
        new()
        {
            Name = "Shirt",
            BasePrice = price,
            Category = "Unknown",
            IsTaxable = taxable,
            IsImported = imported
        };

    [Fact]
    public void ReturnCorrectPrice_WhenProductIsImported()
    {
        var product = CreateProductWithUnknownCategory(imported: true);
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(PriceWithImportDuties, result);
    }

    [Fact]
    public void ReturnCorrectPrice_WhenProductHasTax()
    {
        var product = CreateProductWithUnknownCategory(taxable: true);
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(PriceWithTax, result);
    }
}