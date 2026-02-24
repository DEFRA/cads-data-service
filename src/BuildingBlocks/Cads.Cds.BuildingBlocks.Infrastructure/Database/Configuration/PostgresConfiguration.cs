namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;

public class PostgresConfiguration
{
    public static readonly string SectionName = "Postgres";

    // Traditional connection strings (for local/non-IAM)
    public string DefaultConnection { get; init; } = string.Empty;
    public string ReadOnlyConnection { get; init; } = string.Empty;
    // IAM Authentication settings
    public bool UseIamAuthentication { get; init; } = false;
    public required string DefaultHost { get; init; }
    public required string ReadOnlyHost { get; init; }
    public int Port { get; init; } = 5432;
    public string Name { get; init; } = string.Empty;
    public string User { get; init; } = string.Empty;
}