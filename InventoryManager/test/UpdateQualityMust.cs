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

        var items = new[] { item };

        var sut = new Logic.InventoryManager();
        sut.UpdateQuality(items);

        Assert.Equal("Anything", item.Name);
        Assert.Equal(9, item.SellIn);
    }
}