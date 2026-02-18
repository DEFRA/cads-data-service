namespace Cads.Cds.BuildingBlocks.Infrastructure.Configuration;

public class PostgresConfiguration
{
    public static readonly string SectionName = "Postgres";

    public string DefaultConnection { get; init; } = string.Empty;
    public string? ReadOnlyConnection { get; init; }
}