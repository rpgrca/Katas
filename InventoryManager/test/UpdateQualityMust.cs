using InventoryManager.Logic;

namespace InventoryManager.UnitTests;

public class UpdateQualityMust
{
    private const string CommonItemName = "Anything";
    private const int StartingQuality = 10;
    private const int StartingSellIn = 10;

    [Theory]
    [InlineData(StartingQuality, StartingQuality - 1)]
    [InlineData(Rule.MinimumQuality + 1, Rule.MinimumQuality)]
    [InlineData(Rule.MinimumQuality, Rule.MinimumQuality)]
    public void DecreaseItemQualityUntilZero_WhenItIsNotSpecial(int quality, int expectedQuality)
    {
        var item = CreateItem(quality: quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(CommonItemName, item.Name);
        Assert.Equal(expectedQuality, item.Quality);
    }

    private static Item CreateItem(string name = CommonItemName, int sellIn = StartingSellIn, int quality = StartingQuality) =>
        new()
        {
            Name = name,
            SellIn = sellIn,
            Quality = quality
        };

    [Theory]
    [InlineData(CommonItemName)]
    [InlineData(Logic.InventoryManager.AgedBrie)]
    public void DecreaseSellInTime_WhenItIsNotSpecial(string itemName)
    {
        var item = CreateItem(itemName);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(itemName, item.Name);
        Assert.Equal(StartingSellIn - 1, item.SellIn);
    }

    [Theory]
    [InlineData(StartingQuality, StartingQuality - 2)]
    [InlineData(Rule.MinimumQuality, Rule.MinimumQuality)]
    public void DecreaseQualityTwiceUntilZero_WhenItemSellInExpired(int quality, int expectedQuality)
    {
        var item = CreateItem(sellIn: Rule.MinimumSellIn, quality: quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
    }

    [Fact]
    public void DoNotDecreaseSellIn_WhenItemIsSulfurasHandOfRagnaros()
    {
        var item = CreateItem(Logic.InventoryManager.SulfurasHandOfRagnaros);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(StartingSellIn, item.SellIn);
    }

    [Fact]
    public void DoNotDecreaseQuality_WhenItemIsInDateSulfurasHandOfRagnaros()
    {
        var item = CreateItem(Logic.InventoryManager.SulfurasHandOfRagnaros);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(StartingQuality, item.Quality);
    }

    [Fact]
    public void DoNotDecreaseQuality_WhenItemIsExpiredSulfurasHandOfRagnaros()
    {
        var item = CreateItem(Logic.InventoryManager.SulfurasHandOfRagnaros, Rule.MinimumSellIn - 1);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(StartingQuality, item.Quality);
    }

    [Theory]
    [InlineData(StartingQuality, StartingQuality + 1)]
    [InlineData(Rule.MaximumQuality - 1, Rule.MaximumQuality)]
    [InlineData(Rule.MaximumQuality, Rule.MaximumQuality)]
    public void IncreaseQualityOnceUntil50_WhenItemIsInDateAgedBrie(int quality, int expectedQuality)
    {
        var item = CreateItem(Logic.InventoryManager.AgedBrie, quality: quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
    }

    [Theory]
    [InlineData(StartingQuality, StartingQuality + 2)]
    [InlineData(Rule.MaximumQuality - 1, Rule.MaximumQuality)]
    [InlineData(Rule.MaximumQuality, Rule.MaximumQuality)]
    public void IncreaseQualityTwiceUntil50_WhenItemIsExpiredAgedBrie(int quality, int expectedQuality)
    {
        var item = CreateItem(Logic.InventoryManager.AgedBrie, Rule.MinimumSellIn, quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
    }

    [Theory]
    [InlineData(StartingQuality, StartingQuality + 1)]
    [InlineData(Rule.MaximumQuality - 1, Rule.MaximumQuality)]
    [InlineData(Rule.MaximumQuality, Rule.MaximumQuality)]
    public void IncreaseQualityOnceUntil50_WhenPassExpirationIsOver11Days(int quality, int expectedQuality)
    {
        var item = CreateItem(Logic.InventoryManager.BackstagePassesToTafkal80etcConcert, 13, quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
    }

    [Theory]
    [InlineData(StartingQuality, StartingQuality + 2)]
    [InlineData(Rule.MaximumQuality - 1, Rule.MaximumQuality)]
    [InlineData(Rule.MaximumQuality, Rule.MaximumQuality)]
    public void IncreaseQualityTwiceUntil50_WhenPassExpirationIsBetween6And10Days(int quality, int expectedQuality)
    {
        var item = CreateItem(Logic.InventoryManager.BackstagePassesToTafkal80etcConcert, quality: quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
    }

    [Theory]
    [InlineData(StartingQuality, StartingQuality + 3)]
    [InlineData(Rule.MaximumQuality - 2, Rule.MaximumQuality)]
    [InlineData(Rule.MaximumQuality - 1, Rule.MaximumQuality)]
    [InlineData(Rule.MaximumQuality, Rule.MaximumQuality)]
    public void IncreaseQualityThriceUntil50_WhenPassExpirationIsLessThan6Days(int quality, int expectedQuality)
    {
        var item = CreateItem(Logic.InventoryManager.BackstagePassesToTafkal80etcConcert, 5, quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(expectedQuality, item.Quality);
    }

    [Theory]
    [InlineData(StartingQuality)]
    [InlineData(1)]
    public void DecreaseQualityToMinimumQuality_WhenPassExpires(int quality)
    {
        var item = CreateItem(Logic.InventoryManager.BackstagePassesToTafkal80etcConcert, Rule.MinimumSellIn, quality);
        var sut = new Logic.InventoryManager();

        sut.UpdateQuality(new[] { item });

        Assert.Equal(Rule.MinimumQuality, item.Quality);
    }
}