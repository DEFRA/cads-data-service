namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiBirthSummaryResult
{
    public int BirthYear { get; set; }

    public string BirthMonth { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string? GovRegion { get; set; }

    public string? County { get; set; }

    public string BreedType { get; set; } = string.Empty;

    public string Breed { get; set; } = string.Empty;

    public string? Sex { get; set; }

    public string? ApplicationType { get; set; }

    public long NumberOfBirths { get; set; }
}