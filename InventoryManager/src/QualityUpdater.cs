namespace InventoryManager.Logic;

public class QualityUpdater
{
    private const string ANYTHING_ELSE = "";
    private readonly Item _item;
    private readonly Rule[] _rules;

    public QualityUpdater(Item item)
    {
        _item = item;
        _rules = new Rule[]
        {
            new("Sulfuras, Hand of Ragnaros", Rule.AlwaysFalse, Rule.DoNothing, Rule.AlwaysFalse, Rule.DoNothing),
            new("Aged Brie", Rule.CanIncrementQuality, Rule.IncrementQuality, Rule.AlwaysTrue,
                i => {
                    i.Quality += 1;
                    Rule.CapTopQuality(i);
                }),
            new("Backstage passes to a TAFKAL80ETC concert",
                Rule.CanIncrementQuality,
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
                    i.Quality -= 1;
                    Rule.CapLowerQuality(i);
                })
        };
    }

    public void Update()
    {
        var rule = _rules.First(r => r.CanApplyOn(_item));
        rule.Apply(_item);
    }
}