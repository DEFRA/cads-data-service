namespace Cads.Cds.Ingester.Core.Domain.Entities;

public class DataSeedIngestionHistory
{
    public long Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public DateTimeOffset AppliedAt { get; set; }
    public string Checksum { get; set; } = string.Empty;
}