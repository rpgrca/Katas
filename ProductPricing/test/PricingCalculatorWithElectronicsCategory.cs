using ProductPricing.Logic;

namespace ProductPricing.UnitTests;

public class PricingCalculatorWithElectronicsCategoryMust
{
    private const int CheapPrice = 100;
    private const int CheapPriceWithSmallDiscount = 80;
    private const int CheapPriceWithSmallDiscountWithTax = 88;
    private const int CheapPriceWithSmallDiscountImported = 84;
    private const double CheapPriceWithSmallDiscountImportedAndTaxed = 92.4;
    private const int ExpensivePrice = 10000;
    private const int ExpensivePriceWithLargeDiscount = 9950;
    private const double ExpensivePriceWithLargeDiscountWithTax = 10945;
    private const double ExpensivePriceWithLargeDiscountImported = 10447.5;
    private const double ExpensivePriceWithLargeDiscountImportedAndTaxed = 11492.25;
 
    [Theory]
    [InlineData(CheapPrice, CheapPriceWithSmallDiscount)]
    [InlineData(ExpensivePrice, ExpensivePriceWithLargeDiscount)]
    public void ReturnCorrectPrice_WhenIsNotTaxableNorImported(decimal price, decimal expectedTotal)
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

    [Theory]
    [InlineData(CheapPrice, CheapPriceWithSmallDiscountWithTax)]
    [InlineData(ExpensivePrice, ExpensivePriceWithLargeDiscountWithTax)]
    public void ReturnCorrectPrice_WhenIsTaxedAndNotImported(decimal price, decimal expectedTotal)
    {
        var product = CreateProductWithElectronicsCategory(price: price, taxable: true);
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(expectedTotal, result);
    }

    [Theory]
    [InlineData(CheapPrice, CheapPriceWithSmallDiscountImported)]
    [InlineData(ExpensivePrice, ExpensivePriceWithLargeDiscountImported)]
    public void ReturnCorrectPrice_WhenIsImportedAndNotTaxed(decimal price, decimal expectedTotal)
    {
        var product = CreateProductWithElectronicsCategory(price: price, imported: true);
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(expectedTotal, result);
    }

    [Theory]
    [InlineData(CheapPrice, CheapPriceWithSmallDiscountImportedAndTaxed)]
    [InlineData(ExpensivePrice, ExpensivePriceWithLargeDiscountImportedAndTaxed)]
    public void ReturnCorrectPrice_WhenIsImportedAndTaxed(decimal price, decimal expectedTotal)
    {
        var product = CreateProductWithElectronicsCategory(price: price, taxable: true, imported: true);
        var sut = new PricingCalculator();

        var result = sut.CalculatePrice(product);
        Assert.Equal(expectedTotal, result);
    }
}