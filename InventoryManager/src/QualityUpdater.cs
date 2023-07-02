namespace InventoryManager.Logic;

public class QualityUpdater
{
    public interface IBuilderQualityUpdateCondition
    {
        IBuilderQualityUpdate CheckingQualityUpdateWith(Func<Item, bool> condition);
        Builder Next();
    }

    public interface IBuilderQualityUpdate
    {
        IBuilderExpirationCondition UpdatingQualityWith(Action<Item> action);
    }

    public interface IBuilderExpirationCondition
    {
        IBuilderWhenExpired CanCheckExpiration(Func<Item, bool> condition);
    }

    public interface IBuilderWhenExpired
    {
        Builder WhenExpiredDo(Action<Item> action);
    }

    public class Builder : IBuilderQualityUpdateCondition, IBuilderQualityUpdate, IBuilderExpirationCondition, IBuilderWhenExpired
    {
        private string _name;
        private Func<Item, bool> _canUpdateQuality;
        private Action<Item> _updateQuality;
        private Func<Item, bool> _canExpire;

        private readonly List<Rule> _rules = new();

        public Builder()
        {
            _name = string.Empty;
            _canUpdateQuality = Rule.AlwaysFalse;
            _updateQuality = Rule.DoNothing;
            _canExpire = Rule.AlwaysFalse;
        }

        public IBuilderQualityUpdateCondition Add(string name)
        {
            _name = name;
            return this;
        }

        IBuilderQualityUpdate IBuilderQualityUpdateCondition.CheckingQualityUpdateWith(Func<Item, bool> canUpdateQuality)
        {
            _canUpdateQuality = canUpdateQuality;
            return this;
        }

        Builder IBuilderQualityUpdateCondition.Next()
        {
            _rules.Add(new(_name, _canUpdateQuality, _updateQuality, _canExpire, Rule.DoNothing));
            return this;
        }

        IBuilderExpirationCondition IBuilderQualityUpdate.UpdatingQualityWith(Action<Item> action)
        {
            _updateQuality = action;
            return this;
        }

        IBuilderWhenExpired IBuilderExpirationCondition.CanCheckExpiration(Func<Item, bool> condition)
        {
            _canExpire = condition;
            return this;
        }

        Builder IBuilderWhenExpired.WhenExpiredDo(Action<Item> action)
        {
            _rules.Add(new(_name, _canUpdateQuality, _updateQuality, _canExpire, action));
            return this;
        }

        public QualityUpdater BuildFor(Item item) => new QualityUpdater(item, _rules);
    }

    private readonly Item _item;
    private readonly List<Rule> _rules;

    private QualityUpdater(Item item, List<Rule> rules)
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