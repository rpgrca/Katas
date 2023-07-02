using InventoryManager.Logic;

namespace InventoryManager.UnitTests;

public class UpdateQualityMust
{
    [Theory]
    [InlineData(10, 9)]
    [InlineData(1, 0)]
    [InlineData(0, 0)]
    public void DecreaseItemQualityUntilZero_WhenItIsNotSpecial(int quality, int expectedQuality)
    {
        var item = CreateItem(quality: quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal("Anything", item.Name);
        Assert.Equal(expectedQuality, item.Quality);
    }

    private static Item CreateItem(string name = "Anything", int sellIn = 10, int quality = 10) =>
        new()
        {
            Name = name,
            SellIn = sellIn,
            Quality = quality
        };

    [Theory]
    [InlineData("Anything")]
    [InlineData("Aged Brie")]
    public void DecreaseSellInTime_WhenItIsNotSpecial(string itemName)
    {
        var item = CreateItem(itemName);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(itemName, item.Name);
        Assert.Equal(9, item.SellIn);
    }

    [Theory]
    [InlineData(10, 8)]
    [InlineData(0, 0)]
    public void DecreaseQualityTwiceUntilZero_WhenItemSellInExpired(int quality, int expectedQuality)
    {
        var item = CreateItem(sellIn: 0, quality: quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
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
    public void DoNotDecreaseQuality_WhenItemIsInDateSulfurasHandOfRagnaros()
    {
        var item = CreateItem("Sulfuras, Hand of Ragnaros");
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(10, item.Quality);
    }

    [Fact]
    public void DoNotDecreaseQuality_WhenItemIsExpiredSulfurasHandOfRagnaros()
    {
        var item = CreateItem("Sulfuras, Hand of Ragnaros", -1);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(10, item.Quality);
    }

    [Theory]
    [InlineData(10, 11)]
    [InlineData(49, 50)]
    [InlineData(50, 50)]
    public void IncreaseQualityOnceUntil50_WhenItemIsInDateAgedBrie(int quality, int expectedQuality)
    {
        var item = CreateItem("Aged Brie", quality: quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
    }

    [Theory]
    [InlineData(10, 12)]
    [InlineData(49, 50)]
    [InlineData(50, 50)]
    public void IncreaseQualityTwiceUntil50_WhenItemIsExpiredAgedBrie(int quality, int expectedQuality)
    {
        var item = CreateItem("Aged Brie", 0, quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
    }

    [Theory]
    [InlineData(10, 11)]
    [InlineData(49, 50)]
    [InlineData(50, 50)]
    public void IncreaseQualityOnceUntil50_WhenPassExpirationIsOver11Days(int quality, int expectedQuality)
    {
        var item = CreateItem("Backstage passes to a TAFKAL80ETC concert", 13, quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
    }

    [Theory]
    [InlineData(10, 12)]
    [InlineData(49, 50)]
    [InlineData(50, 50)]
    public void IncreaseQualityTwiceUntil50_WhenPassExpirationIsBetween6And10Days(int quality, int expectedQuality)
    {
        var item = CreateItem("Backstage passes to a TAFKAL80ETC concert", quality: quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
    }

    [Theory]
    [InlineData(10, 13)]
    [InlineData(48, 50)]
    [InlineData(49, 50)]
    [InlineData(50, 50)]
    public void IncreaseQualityThriceUntil50_WhenPassExpirationIsLessThan6Days(int quality, int expectedQuality)
    {
        var item = CreateItem("Backstage passes to a TAFKAL80ETC concert", 5, quality: quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(1)]
    public void DecreaseQualityToZero_WhenPassExpires(int quality)
    {
        var item = CreateItem("Backstage passes to a TAFKAL80ETC concert", 0, quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(0, item.Quality);
    }
}