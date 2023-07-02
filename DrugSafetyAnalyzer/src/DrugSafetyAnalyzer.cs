namespace DrugSafetyAnalyzer.Logic;

public record Rule(string[] Ingredients, SafetyLevel Level);

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
        foreach (var rule in _rules)
        {
            if (! rule.Ingredients.Except(drug.Ingredients).Any())
            {
                return rule;
            }
        }

        return _defaultSafeRule;
    }
}