namespace InventoryManager.Logic;

public class Rule
{
    public const int MaximumQuality = 50;
    public const int MinimumQuality = 0;
    public const int MinimumSellIn = 0;

    private readonly string _name;
    private readonly Func<Item, bool> _canUpdateQuality;
    private readonly Action<Item> _updateQuality;
    private readonly Func<Item, bool> _canExpire;
    private readonly Action<Item> _whenExpired;

    public Rule(string name, Func<Item, bool> canUpdateQuality, Action<Item> updateQuality, Func<Item, bool> canExpire, Action<Item> whenExpired)
    {
        _name = name;
        _canUpdateQuality = canUpdateQuality;
        _updateQuality = updateQuality;
        _canExpire = canExpire;
        _whenExpired = whenExpired;
    }

    public bool CanApplyOn(Item item) => _name == item.Name || string.IsNullOrEmpty(_name);

    public void Apply(Item item) {
        if (_canUpdateQuality(item))
        {
            _updateQuality(item);

            CapLowerQuality(item);
            CapTopQuality(item);
        }

        if (_canExpire(item))
        {
            MakeItemOlder(item);

            if (Expired(item))
            {
                _whenExpired(item);
            }
        }
    }

    public static bool CanIncrementQuality(Item item) => item.Quality < MaximumQuality;

    public static bool CanDecrementQuality(Item item) => item.Quality > MinimumQuality;

    public static void DecrementQuality(Item item) => item.Quality -= 1;

    public static void IncrementQuality(Item item) => item.Quality += 1;

    public static void ResetQuality(Item item) => item.Quality = MinimumQuality;

    public static bool AlwaysTrue(Item _) => true;

    public static bool AlwaysFalse(Item _) => false;

    public static bool Expired(Item item) => item.SellIn <= MinimumSellIn;

    public static void DoNothing(Item _)
    {
    }

    public static void CapTopQuality(Item item)
    {
        if (item.Quality > MaximumQuality)
        {
            item.Quality = MaximumQuality;
        }
    }

    public static void CapLowerQuality(Item item)
    {
        if (item.Quality < MinimumQuality)
        {
            item.Quality = MinimumQuality;
        }
    }

    private static void MakeItemOlder(Item item) => item.SellIn -= 1;
}
