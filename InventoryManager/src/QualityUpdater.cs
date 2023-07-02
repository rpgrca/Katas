namespace InventoryManager.Logic;

public class QualityUpdater
{
    private readonly Item _item;
    private readonly Rule[] _rules;

    public QualityUpdater(Item item)
    {
        _item = item;
        _rules = new Rule[]
        {
            new("Sulfuras, Hand of Ragnaros",
                Rule.AlwaysFalse,
                Rule.DoNothing,
                Rule.AlwaysFalse,
                Rule.DoNothing),
            new("Aged Brie",
                Rule.CanIncrementQuality,
                Rule.IncrementQuality,
                Rule.AlwaysTrue,
                i => {
                    i.Quality += 1;
                    if (i.Quality > 50)
                    {
                        i.Quality = 50;
                    }
                }),
            new("Backstage passes to a TAFKAL80ETC concert",
                Rule.CanIncrementQuality,
                i => {
                    i.Quality += i.SellIn switch {
                        < 6 => 3,
                        < 11 => 2,
                        _ => 1
                    };

                    if (i.Quality > 50)
                    {
                        i.Quality = 50;
                    }
                },
                Rule.Expired,
                Rule.ResetQuality),
            new("",
                Rule.CanDecrementQuality,
                Rule.DecrementQuality,
                Rule.AlwaysTrue,
                i => {
                    i.Quality -= 1;
                    if (i.Quality < 0)
                    {
                        i.Quality = 0;
                    }
                })
        };
    }

    public void Update()
    {
        var rule = _rules.First(r => r.CanApplyOn(_item));
        rule?.Apply(_item);
    }
}