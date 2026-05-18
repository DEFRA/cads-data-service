namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiImportSummary
{
    public string Country { get; set; } = string.Empty;
    public DateTime MonthYear { get; set; }
    public int AgeAtImport { get; set; }
    public string AgeBand { get; set; } = string.Empty;
    public string BreedType { get; set; } = string.Empty;
    public string? Sex { get; set; }
    public long NumberOfImports { get; set; }
}