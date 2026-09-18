using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators.Extensions;

public static class EntityExtensions
{
    public static IReadOnlyDictionary<string, object?> ToDictionary<T>(this T entity)
        where T : class, new()
    {
        return typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead)
            .ToDictionary(p => p.Name, p => p.GetValue(entity));
    }

    public static IReadOnlyDictionary<string, object?> ToDictionary<T>(this T entity, DbContext dbContext)
        where T : class, new()
    {
        var type = typeof(T);
        var entityType = dbContext.Model.FindEntityType(type);

        if (entityType == null)
            throw new InvalidOperationException(
            $"Entity '{typeof(T).Name}' is not mapped in the DbContext.");

        var tableIdentifier = StoreObjectIdentifier.Table(
            entityType.GetTableName()!,
            entityType.GetSchema());

        return typeof(T)
            .GetProperties()
            .Where(p => p.CanRead)
            .ToDictionary(
                p =>
                {
                    var property = entityType.FindProperty(p.Name);

                    return property?.GetColumnName(tableIdentifier) ?? p.Name;
                },
                p => p.GetValue(entity));
    }
}
