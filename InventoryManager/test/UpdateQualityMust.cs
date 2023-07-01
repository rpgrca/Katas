using InventoryManager.Logic;

namespace InventoryManager.UnitTests;

public class UpdateQualityMust
{
    [Fact]
    public void DecreaseItemQuality_WhenItIsNotSpecial()
    {
        var item = CreateItem();
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal("Anything", item.Name);
        Assert.Equal(9, item.Quality);
    }

    private static Item CreateItem(string name = "Anything", int sellIn = 10, int quality = 10) =>
        new()
        {
            Name = name,
            SellIn = sellIn,
            Quality = quality
        };

    [Fact]
    public void DecreaseSellInTime_WhenItIsNotSpecial()
    {
        var item = CreateItem();
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal("Anything", item.Name);
        Assert.Equal(9, item.SellIn);
    }

    [Fact]
    public void DecreaseQualityTwice_WhenItemSellInExpired()
    {
        var item = CreateItem(sellIn: 0);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(8, item.Quality);
    }

    [Fact]
    public void DoNotDecreaseSellIn_WhenItemIsSulfurasHandOfRagnaros()
    {
        var item = CreateItem("Sulfuras, Hand of Ragnaros");
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(10, item.SellIn);
    }

    [Fact]
    public void DoNotDecreaseQuality_WhenItemIsSulfurasHandOfRagnaros()
    {
        var item = CreateItem("Sulfuras, Hand of Ragnaros");
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(10, item.Quality);
    }

    [Theory]
    [InlineData(10, 11)]
    [InlineData(49, 50)]
    [InlineData(50, 50)]
    public void IncreaseQualityUntil50_WhenItemIsAgedBrieAndIsNotExpired(int quality, int expectedQuality)
    {
        var item = CreateItem("Aged Brie", quality: quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
    }
}