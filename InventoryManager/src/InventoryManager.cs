namespace InventoryManager.Logic;

public class InventoryManager
{
    private const string ANYTHING_ELSE = "";
    private readonly List<Rule> _rules;

    public InventoryManager()
    {
        _rules = new List<Rule>
        {
            new("Sulfuras, Hand of Ragnaros", Rule.AlwaysFalse, Rule.DoNothing, Rule.AlwaysFalse, Rule.DoNothing),
            new("Aged Brie", Rule.CanIncrementQuality, Rule.IncrementQuality, Rule.AlwaysTrue,
                i => {
                    Rule.IncrementQuality(i);
                    Rule.CapTopQuality(i);
                }),
            new("Backstage passes to a TAFKAL80ETC concert", Rule.CanIncrementQuality,
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
            new(ANYTHING_ELSE, Rule.CanDecrementQuality, Rule.DecrementQuality, Rule.AlwaysTrue,
                i => {
                    Rule.DecrementQuality(i);
                    Rule.CapLowerQuality(i);
                })
        };
    }

    public void UpdateQuality(Item[] items)
    {
        var builder = new QualityUpdater.Builder();

        builder.Add("Sulfuras, Hand of Ragnaros")
            .Next();

        builder.Add("Aged Brie")
            .CheckingQualityUpdateWith(Rule.CanIncrementQuality)
            .UpdatingQualityWith(Rule.IncrementQuality)
            .CanCheckExpiration(Rule.AlwaysTrue)
            .WhenExpiredDo(i => {
                i.Quality += 1;
                Rule.CapTopQuality(i);
            });

        builder.Add("Backstage passes to a TAFKAL80ETC concert")
            .CheckingQualityUpdateWith(Rule.CanIncrementQuality)
            .UpdatingQualityWith(i => {
                    i.Quality += i.SellIn switch {
                        < 6 => 3,
                        < 11 => 2,
                        _ => 1
                    };

                    Rule.CapTopQuality(i);
                })
            .CanCheckExpiration(Rule.Expired)
            .WhenExpiredDo(Rule.ResetQuality);

        builder.Add(ANYTHING_ELSE)
            .CheckingQualityUpdateWith(Rule.CanDecrementQuality)
            .UpdatingQualityWith(Rule.DecrementQuality)
            .CanCheckExpiration(Rule.AlwaysTrue)
            .WhenExpiredDo(i => {
                Rule.DecrementQuality(i);
                Rule.CapLowerQuality(i);
            });

        foreach (var item in items)
        {
            var qualityUpdater = builder.BuildFor(item);
            qualityUpdater.Update();
        }
    }
}