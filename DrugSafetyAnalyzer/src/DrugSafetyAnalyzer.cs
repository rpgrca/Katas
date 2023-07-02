namespace DrugSafetyAnalyzer.Logic;

public class DrugSafetyAnalyzer
{
    private readonly List<Rule> _rules;
    private readonly Rule _defaultSafeRule;

    public DrugSafetyAnalyzer()
    {
        _rules = new List<Rule>
        {
            { new(new[] { "Acetaminophen", "Codeine" }, SafetyLevel.Dangerous) },
            { new(new[] { "Aspirin", "Warfarin" }, SafetyLevel.Mortal) },
            { new(new[] { "Ibuprofen", "Paracetamol" }, SafetyLevel.Dangerous) }
        };

        _defaultSafeRule = new(Array.Empty<string>(), SafetyLevel.Safe);
    }

    public SafetyLevel AnalyzeSafetyLevel(Drug drug)
    {
        var rule = FindFittingtRuleFor(drug);
        return rule.Level;
    }

    private Rule FindFittingtRuleFor(Drug drug)
    {
        var rules = _rules.Where(r => !r.Ingredients.Except(drug.Ingredients).Any());
        if (rules.Any())
        {
            return rules.MaxBy(p => p.Level)!;
        }

        return _defaultSafeRule;
    }
}