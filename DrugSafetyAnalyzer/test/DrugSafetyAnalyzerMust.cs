using DrugSafetyAnalyzer.Logic;

namespace DrugSafetyAnalyzer.UnitTests;

public class DrugSafetyAnalyzerMust
{
    [Fact]
    public void ReturnSafeLevel_WhenDrugHasOnlyAspirinAsIngredient()
    {
        var drug = new Drug
        {
            Name = "Bayaspirina",
            Ingredients = new() { "Aspirin" }
        };

        var sut = new Logic.DrugSafetyAnalyzer();
        var result = sut.AnalyzeSafetyLevel(drug);

        Assert.Equal(SafetyLevel.Safe, result);
    }
}