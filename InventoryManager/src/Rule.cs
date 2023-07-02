namespace InventoryManager.Logic;

public class Rule
{
    private readonly string _name;
    private readonly Func<Item, bool> _qualityUpdateCondition;
    private readonly Action<Item> _qualityUpdate;
    private readonly Func<Item, bool> _canExpire;
    private readonly Action<Item> _whenExpired;

    public Rule(string name, Func<Item, bool> qualityUpdateCondition, Action<Item> qualityUpdate, Func<Item, bool> canExpire, Action<Item> whenExpired)
    {
        _name = name;
        _qualityUpdateCondition = qualityUpdateCondition;
        _qualityUpdate = qualityUpdate;
        _canExpire = canExpire;
        _whenExpired = whenExpired;
    }

    public bool CanApplyOn(Item item) => _name == item.Name || string.IsNullOrEmpty(_name);

    public void Apply(Item item) {
        if (_qualityUpdateCondition(item))
        {
            _qualityUpdate(item);
        }

        if (_canExpire(item))
        {
            item.SellIn -= 1;

            if (item.SellIn < 1)
            {
                _whenExpired(item);
            }
        }
    }
}
