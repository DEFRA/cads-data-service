using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Queries;

// DTO for GraphQL output
[ExcludeFromCodeCoverage]
public record ErdEntry(string Entity, IReadOnlyList<string> RelatedEntities);

// GraphQL Query type
[ExcludeFromCodeCoverage]
[ExtendObjectType("Query")]
public class ErdQuery<T>
    where T : DbContext
{
    public IEnumerable<ErdEntry> GetErd([Service] T db)
    {
        var erd = new List<ErdEntry>();

        foreach (var entityType in db.Model.GetEntityTypes())
        {
            var entityName = entityType.ClrType.Name;
            var relatedEntities = entityType
                .GetNavigations()
                .Select(n => n.TargetEntityType.ClrType.Name)
                .Distinct()
                .ToList();

            erd.Add(new ErdEntry(entityName, relatedEntities));
        }

        return erd;
    }
}