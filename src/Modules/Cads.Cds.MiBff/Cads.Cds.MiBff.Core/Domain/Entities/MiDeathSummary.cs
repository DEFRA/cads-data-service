namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiDeathSummary
{
    public int DeathYear { get; set; }
    public string DeathMonth { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? County { get; set; } = string.Empty;
    public string BreedType { get; set; } = string.Empty;
    public string? Breed { get; set; }
    public string? BreedCode { get; set; }
    public string? Sex { get; set; }
    public int AgeAtDeathInMonths { get; set; }
    public long NumberOfDeaths { get; set; }
}