namespace ProductPricing.Logic;

public class Product
{
    public string Name { get; set; }
    public decimal BasePrice { get; set; }
    public string Category { get; set; }
    public bool IsTaxable { get; set; }
    public bool IsImported { get; set; }
}