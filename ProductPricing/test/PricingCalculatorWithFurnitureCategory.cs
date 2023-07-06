using ProductPricing.Logic;

namespace ProductPricing.UnitTests;

public class PricingCalculatorWithFurnitureCategoryMust
{
    public const decimal Price = 1000;
    public const decimal Total = 1100;
    public const decimal TotalWithImportDuties = 1155;
    public const decimal TotalWithTaxes = 1210;
    public const decimal TotalWithImportDutiesAndTaxes = 1270.5M;

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

    [Fact]
    public void ReturnCorrectPrice_WhenIsImported()
    {
        var product = CreateProductWithFurnitureCategory(imported: true);
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(TotalWithImportDuties, result);
    }

    [Fact]
    public void ReturnCorrectPrice_WhenIsTaxed()
    {
        var product = CreateProductWithFurnitureCategory(taxable: true);
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(TotalWithTaxes, result);
    }

    [Fact]
    public void ReturnCorrectPrice_WhenIsImportedAndTaxed()
    {
        var product = CreateProductWithFurnitureCategory(taxable: true, imported: true);
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(TotalWithImportDutiesAndTaxes, result);
    }
}