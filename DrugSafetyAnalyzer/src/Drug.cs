namespace DrugSafetyAnalyzer.Logic;

public class Drug
{
    public string Name { get; set; }
    public List<string> Ingredients { get; set; }

    public Drug()
    {
        Name = string.Empty;
        Ingredients = new List<string>();
    }
}
