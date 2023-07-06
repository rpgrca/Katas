namespace ProductPricing.Logic;

public class PricingCalculator
{
    public decimal CalculatePrice(Product product)
    {
        decimal price = product.BasePrice;

        if (product.Category == "Electronics")
        {
            if (product.BasePrice > 1000)
            {
                price -= 50;
            }
            else
            {
                price -= 20;
            }
        }
        else if (product.Category == "Clothing")
        {
            if (product.BasePrice > 50)
            {
                price *= 0.9m;
            }
            else
            {
                price *= 0.95m;
            }
        }
        else if (product.Category == "Furniture")
        {
            price += 100;
        }

        if (product.IsTaxable)
        {
            price += price * 0.1m;
        }

        if (product.IsImported)
        {
            price += price * 0.05m;
        }

        return price;
    }
}