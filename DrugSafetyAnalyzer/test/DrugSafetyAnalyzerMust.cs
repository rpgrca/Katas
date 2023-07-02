using DrugSafetyAnalyzer.Logic;

namespace DrugSafetyAnalyzer.UnitTests;

public class DrugSafetyAnalyzerMust
{
    [Theory]
    [InlineData("Caffeine")]
    [InlineData("Aspirin")]
    [InlineData("Paracetamol")]
    [InlineData("Ibuprofen")]
    [InlineData("Acetaminophen")]
    [InlineData("Codeine")]
    [InlineData("Warfarin")]
    public void ReturnSafeLevel_WhenDrugHasOnlyIngredient(string ingredient)
    {
        var drug = new Drug
        {
            Name = "Safe Drug",
            Ingredients = new() { ingredient }
        };

        var sut = new Logic.DrugSafetyAnalyzer();
        var result = sut.AnalyzeSafetyLevel(drug);

        Assert.Equal(SafetyLevel.Safe, result);
    }

    [Fact]
    public void ReturnDangerousLevel_WhenDrugHasParacetamolAndIbuprofen()
    {
        var drug = new Drug
        {
            Name = "Dangerous Drug",
            Ingredients = new() { "Paracetamol", "Ibuprofen" }
        };

        var sut = new Logic.DrugSafetyAnalyzer();
        var result = sut.AnalyzeSafetyLevel(drug);

        Assert.Equal(SafetyLevel.Dangerous, result);
    }

    [Fact]
    public void ReturnDangerousLevel_WhenDrugHasAcetaminophenAndCodeine()
    {
        var drug = new Drug
        {
            Name = "Dangerous Drug",
            Ingredients = new() { "Acetaminophen", "Codeine" }
        };

        var sut = new Logic.DrugSafetyAnalyzer();
        var result = sut.AnalyzeSafetyLevel(drug);

        Assert.Equal(SafetyLevel.Dangerous, result);
    }

    [Fact]
    public void ReturnMortalLevel_WhenDrugHasAspirinAndWarfarin()
    {
        var drug = new Drug
        {
            Name = "Mortal Drug",
            Ingredients = new() { "Aspirin", "Warfarin" }
        };

        var sut = new Logic.DrugSafetyAnalyzer();
        var result = sut.AnalyzeSafetyLevel(drug);

        Assert.Equal(SafetyLevel.Mortal, result);
    }
}