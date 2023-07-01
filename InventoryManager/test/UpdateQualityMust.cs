using InventoryManager.Logic;

namespace InventoryManager.UnitTests;

public class UpdateQualityMust
{
    [Fact]
    public void DecreaseItemQuality_WhenItIsNotSpecial()
    {
        var item = new Item
        {
            Name = "Anything",
            SellIn = 10,
            Quality = 10
        };

        var items = new[] { item };

        var sut = new Logic.InventoryManager();
        sut.UpdateQuality(items);

        Assert.Equal("Anything", item.Name);
        Assert.Equal(9, item.Quality);
    }

    [Fact]
    public void DecreaseSellInTime_WhenItIsNotSpecial()
    {
        var item = new Item
        {
            Name = "Anything",
            SellIn = 10,
            Quality = 10
        };

        var sut = new Logic.InventoryManager();
        sut.UpdateQuality(new[] { item });

        Assert.Equal("Anything", item.Name);
        Assert.Equal(9, item.SellIn);
    }

    [Fact]
    public void DecreaseQualityTwice_WhenItemSellInExpired()
    {
        var item = new Item
        {
            Name = "Anything",
            SellIn = 0,
            Quality = 10
        };

        var sut = new Logic.InventoryManager();
        sut.UpdateQuality(new[] { item });

        Assert.Equal(8, item.Quality);
    }

    [Fact]
    public void DoNotDecreaseSellIn_WhenItemIsSulfurasHandOfRagnaros()
    {
        var item = new Item
        {
            Name = "Sulfuras, Hand of Ragnaros",
            SellIn = 10,
            Quality = 10
        };

        var sut = new Logic.InventoryManager();
        sut.UpdateQuality(new[] { item });

        Assert.Equal(10, item.Quality);
    }

}