namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;

public class PostgresPoolConfiguration
{
    // Overrides DefaultHost/ReadOnlyHost (IAM) or the host in the fallback connection string (non-IAM)
    public string? Host { get; init; }

    // Non-IAM only: overrides DefaultConnection/ReadOnlyConnection
    public string? ConnectionString { get; init; }

    public string? ApplicationName { get; init; }
    public int? MaximumPoolSize { get; init; }
    public int? MinimumPoolSize { get; init; }
}