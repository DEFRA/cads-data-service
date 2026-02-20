namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;

public class PostgresConfiguration
{
    public static readonly string SectionName = "Postgres";

    // Traditional connection strings (for local/non-IAM)
    public string DefaultConnection { get; init; } = string.Empty;
    public string? ReadOnlyConnection { get;init; }
    
    // IAM Authentication settings
    public bool UseIamAuthentication { get; init; } = false;
    public string? DbHost { get; init; }
    public int DbPort { get; init; } = 5432;
    public string? DbName { get; init; }
    public string? DbUser { get; init; }
}