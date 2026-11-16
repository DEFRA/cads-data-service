using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;
using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;

public sealed class BusinessRowMapper : IBusinessRowMapper
{
    public IReadOnlyDictionary<string, object?> Map<T>(T entity, IReadOnlyList<BusinessColumn> columns)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(columns);

        var row = new Dictionary<string, object?>(columns.Count, StringComparer.Ordinal);

        foreach (var column in columns)
        {
            var propertyInfo = column.Property.PropertyInfo ?? throw new InvalidOperationException(
                $"Column '{column.Name}' of '{typeof(T).Name}' has no CLR property.");

            row[column.Name] = propertyInfo.GetValue(entity);
        }

        return row;
    }
}
