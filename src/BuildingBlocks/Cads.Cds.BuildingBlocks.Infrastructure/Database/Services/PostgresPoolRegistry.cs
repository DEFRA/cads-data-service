using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

public sealed class PostgresPoolRegistry : IPostgresPoolRegistry
{
    // Identifier -> true when the pool targets the reader endpoint
    private readonly Dictionary<string, bool> _isReadPoolByIdentifier = new(StringComparer.OrdinalIgnoreCase)
    {
        [PostgresPools.Default] = false,
        [PostgresPools.ReadOnly] = true,
        [PostgresPools.ApiWrite] = false,
        [PostgresPools.ApiRead] = true,
        [PostgresPools.MiBffWrite] = false,
        [PostgresPools.MiBffRead] = true,
        [PostgresPools.StorageBridgeWrite] = false,
        [PostgresPools.StorageBridgeRead] = true,
        [PostgresPools.SystemAdminWrite] = false,
        [PostgresPools.SystemAdminRead] = true,
        [PostgresPools.CadsGraphQLRead] = true,
        [PostgresPools.CtsGraphQLRead] = true,
        [PostgresPools.CtsTransactionsGraphQLRead] = true,
        [PostgresPools.CtsAuditGraphQLRead] = true,
        [PostgresPools.HealthCheckWrite] = false,
        [PostgresPools.HealthCheckRead] = true
    };

    public IReadOnlyCollection<string> Identifiers => _isReadPoolByIdentifier.Keys;

    public bool IsKnown(string identifier) => _isReadPoolByIdentifier.ContainsKey(identifier);

    public bool IsReadPool(string identifier) =>
        _isReadPoolByIdentifier.TryGetValue(identifier, out var isRead)
            ? isRead
            : throw new ArgumentException(
                $"Unknown Postgres pool identifier '{identifier}'. Known pools: {string.Join(", ", Identifiers)}",
                nameof(identifier));
}