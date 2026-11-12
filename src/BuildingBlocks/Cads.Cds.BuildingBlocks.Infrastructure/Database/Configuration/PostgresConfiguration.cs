namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;

public class PostgresConfiguration
{
    public static readonly string SectionName = "Postgres";

    // Traditional connection strings (for local/non-IAM)
    public string DefaultConnection { get; init; } = string.Empty;
    public string ReadOnlyConnection { get; init; } = string.Empty;
    // IAM Authentication settings
    public bool UseIamAuthentication { get; init; } = false;
    public bool UsePasswordBasedAuthentication { get; init; } = false;
    public required string DefaultHost { get; init; }
    public required string ReadOnlyHost { get; init; }
    public int DefaultPort { get; init; } = 5432;
    public int ReadOnlyPort { get; init; } = 5432;
    public string Name { get; init; } = string.Empty;
    public string User { get; init; } = string.Empty;
    public string? Password { get; init; }
    // Default Npgsql command timeout (seconds) applied to every pool unless overridden per-pool. Npgsql's own default is 30s.
    public int CommandTimeoutSeconds { get; init; } = 60;
    // Optional per-module pool settings keyed by PostgresPools identifier; unset values fall back to the settings above
    public IReadOnlyDictionary<string, PostgresPoolConfiguration> Pools { get; init; } = new Dictionary<string, PostgresPoolConfiguration>();
}