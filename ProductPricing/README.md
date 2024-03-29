## Product Pricing

### Description
The "Product Pricing" kata focuses on refactoring a piece of code that calculates the prices of various products. The current implementation is convoluted, difficult to maintain, and lacks flexibility. Your task is to refactor the code while preserving its behavior and improving its readability, maintainability, and extensibility.

The pricing of products is determined by several factors, including the base price, discounts, taxes, and any additional charges. The current code applies these factors in a procedural manner, making it hard to understand and modify. Your goal is to refactor the code into a more modular and flexible structure, allowing for easy modification and extension of pricing rules.

### Code to Refactor

```csharp
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

public class Product
{
    public string Name { get; set; }
    public decimal BasePrice { get; set; }
    public string Category { get; set; }
    public bool IsTaxable { get; set; }
    public bool IsImported { get; set; }
}
```

The above code represents the current implementation of the product pricing calculator. Your task is to refactor the `CalculatePrice` method and any other parts of the code that you think can be improved while maintaining the same behavior.

Your refactored code should be easier to understand, modify, and extend. It should follow best practices such as clean code principles, SOLID principles, and appropriate design patterns if applicable. Remember to preserve the behavior of the original code and ensure that all the pricing rules are still applied correctly.

Feel free to modify the code as needed to achieve a better design and structure.

(Generated by ChatGPT)
