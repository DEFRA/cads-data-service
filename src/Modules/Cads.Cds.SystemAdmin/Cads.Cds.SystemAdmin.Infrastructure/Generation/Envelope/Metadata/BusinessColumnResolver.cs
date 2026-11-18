using System;
using System.Collections.Generic;
using System.Linq;
using Cads.Cds.SystemAdmin.Application.Imports.Envelope;
using Cads.Cds.SystemAdmin.Application.Imports.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Envelope.Metadata;

public sealed class BusinessColumnResolver(
    ITablePrefixResolver tablePrefixResolver,
    IPipelineColumns pipelineColumns,
    IImportDataTypeLookup importDataTypeLookup,
    IEntityTypeStore entityTypeStore) : IBusinessColumnResolver
{
    public IReadOnlyList<BusinessColumn> Resolve(IReadOnlyEntityType entityType)
    {
        ArgumentNullException.ThrowIfNull(entityType);

        var tableName = entityTypeStore.GetTable(entityType).Name;

        return Resolve(entityType, importDataTypeLookup.GetBusinessKey(tableName));
    }

    public IReadOnlyList<BusinessColumn> Resolve(IReadOnlyEntityType entityType, string businessKey)
    {
        ArgumentNullException.ThrowIfNull(entityType);
        ArgumentException.ThrowIfNullOrWhiteSpace(businessKey);

        var storeObject = entityTypeStore.GetTable(entityType);
        var prefix = tablePrefixResolver.Resolve(entityType);

        // EF Core returns properties key-first then alphabetically, not in DDL order. The scaffolded CLR
        // properties are declared in DDL order, so order by declaration (metadata token) instead.
        var columns = entityType.GetProperties()
            .Select(p => (Property: p, Column: p.GetColumnName(storeObject)))
            .Where(x => x.Column is not null && !pipelineColumns.IsExcluded(x.Column, prefix))
            .OrderBy(x => x.Property.PropertyInfo?.MetadataToken ?? int.MaxValue)
            .ThenBy(x => x.Column, StringComparer.Ordinal)
            .Select(x => new BusinessColumn(x.Column!.ToLowerInvariant(), x.Property))
            .ToList();

        var keyIndex = columns.FindIndex(c => string.Equals(c.Name, businessKey, StringComparison.OrdinalIgnoreCase));

        if (keyIndex < 0)
        {
            throw new InvalidOperationException(
                $"Business key '{businessKey}' is not a business column of table '{storeObject.DisplayName()}'.");
        }

        var key = columns[keyIndex];
        columns.RemoveAt(keyIndex);
        columns.Insert(0, key);

        return columns;
    }
}