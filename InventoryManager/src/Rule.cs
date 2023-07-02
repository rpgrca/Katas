namespace InventoryManager.Logic;

public class Rule
{
    private readonly string _name;
    private readonly Func<Item, bool> _qualityUpdateCondition;
    private readonly Action<Item> _qualityUpdate;
    private readonly Func<Item, bool> _sellInUpdateCondition;
    private readonly Action<Item> _sellInUpdate;

    public Rule(string name, Func<Item, bool> qualityUpdateCondition, Action<Item> qualityUpdate, Func<Item, bool> sellInUpdateCondition, Action<Item> sellInUpdate)
    {
        _name = name;
        _qualityUpdateCondition = qualityUpdateCondition;
        _qualityUpdate = qualityUpdate;
        _sellInUpdateCondition = sellInUpdateCondition;
        _sellInUpdate = sellInUpdate;
    }

    public bool CanApplyOn(Item item) => _name == item.Name || string.IsNullOrEmpty(_name);

    public void Apply(Item item) {
        if (_qualityUpdateCondition(item))
        {
            _qualityUpdate(item);
        }
    }
}
