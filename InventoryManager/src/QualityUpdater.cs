namespace InventoryManager.Logic;

public class QualityUpdater
{
    private readonly Item _item;
    private readonly List<Rule> _rules;

    public QualityUpdater(Item item, List<Rule> rules)
    {
        _item = item;
        _rules = rules;
    }

    public void Update()
    {
        var rule = _rules.First(r => r.CanApplyOn(_item));
        rule.Apply(_item);
    }
}