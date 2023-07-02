namespace InventoryManager.Logic;

public class Rule
{
    private readonly string _name;
    private readonly Func<Item, bool> _qualityUpdateCondition;
    private readonly Action<Item> _qualityUpdate;

    public Rule(string name, Func<Item, bool> qualityUpdateCondition, Action<Item> qualityUpdate)
    {
        _name = name;
        _qualityUpdateCondition = qualityUpdateCondition;
        _qualityUpdate = qualityUpdate;
    }

    public bool CanApplyOn(Item item) => _name == item.Name || string.IsNullOrEmpty(_name);

    public void UpdateItem(Item item) {
        if (_qualityUpdateCondition(item))
        {
            _qualityUpdate(item);
        }
    }
}

public class QualityUpdater
{
    private readonly Item _item;
    private readonly Rule[] _rules;

    public QualityUpdater(Item item)
    {
        _item = item;
        _rules = new Rule[]
        {
            new("Sulfuras, Hand of Ragnaros", i => false, i => {}),
            new("Aged Brie", i => i.Quality < 50, i => i.Quality += 1),
            new("Backstage passes to a TAFKAL80ETC concert", i => false, i => {}),
            new("", i => false, i => {})
        };
    }

    public void Update()
    {
        var rule = _rules.First(r => r.CanApplyOn(_item));
        rule?.UpdateItem(_item);

        if (_item.Name == "Sulfuras, Hand of Ragnaros")
        {
            return;
        }

        if (_item.Name != "Aged Brie" && _item.Name != "Backstage passes to a TAFKAL80ETC concert")
        {
            if (CanDecreaseQuality())
            {
                DecreaseQuality();
            }
        }

        if (_item.Name == "Backstage passes to a TAFKAL80ETC concert")
        {
            if (CanIncrementQuality())
            {
                IncreaseQuality();

                if (_item.SellIn < 11)
                {
                    if (CanIncrementQuality())
                    {
                        IncreaseQuality();
                    }
                }

                if (_item.SellIn < 6)
                {
                    if (CanIncrementQuality())
                    {
                        IncreaseQuality();
                    }
                }
            }
        }

        DecreaseSellIn();

        if (Expired())
        {
            if (_item.Name != "Aged Brie" && _item.Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                if (CanDecreaseQuality())
                {
                    DecreaseQuality();
                }
            }

            if (_item.Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                ResetQuality();
            }

            if (_item.Name == "Aged Brie")
            {
                if (CanIncrementQuality())
                {
                    IncreaseQuality();
                }
            }
        }
    }

    private void IncreaseQuality() => _item.Quality += 1;

    private void DecreaseQuality() => _item.Quality -= 1;

    private void ResetQuality() => _item.Quality = 0;

    private bool CanIncrementQuality() => _item.Quality < 50;

    private bool CanDecreaseQuality() => _item.Quality > 0;

    private void DecreaseSellIn() => _item.SellIn -= 1;

    private bool Expired() => _item.SellIn < 0;
}