namespace InventoryManager.Logic;

public class InventoryManager
{
    private const string AnythingElse = "";
    public const string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";
    public const string AgedBrie = "Aged Brie";
    public const string BackstagePassesToTafkal80etcConcert = "Backstage passes to a TAFKAL80ETC concert";

    private readonly List<Rule> _rules;

    public InventoryManager()
    {
        _rules = new List<Rule>
        {
            new(SulfurasHandOfRagnaros, Rule.AlwaysFalse, Rule.DoNothing, Rule.AlwaysFalse, Rule.DoNothing),
            new(AgedBrie, Rule.CanIncrementQuality, Rule.IncrementQuality, Rule.AlwaysTrue,
                i => {
                    Rule.IncrementQuality(i);
                    Rule.CapTopQuality(i);
                }),
            new(BackstagePassesToTafkal80etcConcert, Rule.CanIncrementQuality,
                i => {
                    i.Quality += i.SellIn switch {
                        < 6 => 3,
                        < 11 => 2,
                        _ => 1
                    };

                    Rule.CapTopQuality(i);
                },
                Rule.Expired,
                Rule.ResetQuality),
            new(AnythingElse, Rule.CanDecrementQuality, Rule.DecrementQuality, Rule.AlwaysTrue,
                i => {
                    Rule.DecrementQuality(i);
                    Rule.CapLowerQuality(i);
                })
        };
    }

    public void UpdateQuality(Item[] items)
    {
        foreach (var item in items)
        {
            var qualityUpdater = new QualityUpdater(item, _rules);
            qualityUpdater.Update();
        }
    }
}