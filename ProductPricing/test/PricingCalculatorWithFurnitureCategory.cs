using ProductPricing.Logic;

namespace ProductPricing.UnitTests;

public class PricingCalculatorWithFurnitureCategoryMust
{
    public const decimal Price = 1000;
    public const decimal Total = 1100;

    [Fact]
    public void ReturnCorrectPrice_WhenIsNotTaxableNorImported()
    {
        var product = CreateProductWithFurnitureCategory();
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(Total, result);
    }

    private static Product CreateProductWithFurnitureCategory(bool taxable = false, bool imported = false) =>
        new()
        {
            Name = "Table",
            BasePrice = Price,
            Category = "Furniture",
            IsTaxable = taxable,
            IsImported = imported
        };
}