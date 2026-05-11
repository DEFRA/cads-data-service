namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiDeathSummary
{
    public string MonthName { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public string BreedCode { get; set; } = string.Empty;
    public string BreedType { get; set; } = string.Empty;
    public string? Sex { get; set; }
    public string Country { get; set; } = string.Empty;
    public int AgeAtDeathInMonths { get; set; }
    public string PremiseTypeGroups { get; set; } = string.Empty;
    public long NumberOfDeaths { get; set; }
}