using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Utils;

public record ErdEntry(string Entity, IReadOnlyList<string> RelatedEntities);

public sealed class TableDependencyGraph<T> : ITableDependencyGraph<T>
    where T : DbContext
{
    private readonly IReadOnlyDictionary<string, IReadOnlyList<string>> _dependencies;

    public TableDependencyGraph(T dbContext)
    {
        var map = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var entityType in dbContext.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName is null) continue;
            var dependencies = entityType.GetForeignKeys()
                .Select(fk => fk.PrincipalEntityType.GetTableName())
                .Where(t => t is not null && !string.Equals(t, tableName, StringComparison.OrdinalIgnoreCase))
                .Select(t => t!)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
            map[tableName] = dependencies;
        }
        _dependencies = map.ToDictionary(kv => kv.Key, kv => (IReadOnlyList<string>)kv.Value, StringComparer.OrdinalIgnoreCase);
    }
    public IReadOnlyList<string>? DependenciesOf(string table) =>
        _dependencies.TryGetValue(table, out var deps) ? deps : null;
}
