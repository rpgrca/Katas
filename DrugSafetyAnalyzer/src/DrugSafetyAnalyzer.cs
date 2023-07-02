namespace DrugSafetyAnalyzer.Logic;

public class DrugSafetyAnalyzer
{
    public SafetyLevel AnalyzeSafetyLevel(Drug drug)
    {
        var ingredients = drug.Ingredients;

        if (ingredients.Contains("Paracetamol") && ingredients.Contains("Ibuprofen"))
        {
            return SafetyLevel.Dangerous;
        }
        else if (ingredients.Contains("Acetaminophen") && ingredients.Contains("Codeine"))
        {
            return SafetyLevel.Dangerous;
        }
        else if (ingredients.Contains("Aspirin") && ingredients.Contains("Warfarin"))
        {
            return SafetyLevel.Mortal;
        }
        else
        {
            return SafetyLevel.Safe;
        }
    }
}