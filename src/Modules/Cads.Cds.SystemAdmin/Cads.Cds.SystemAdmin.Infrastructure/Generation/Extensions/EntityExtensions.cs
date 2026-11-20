using Cads.Cds.SystemAdmin.Infrastructure.Generation.Extensions;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Extensions;

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
        var entityType = dbContext.Model.FindEntityType(type) ?? throw new InvalidOperationException(
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

    public static IReadOnlyDictionary<string, object?> ToDictionary<T>(this T entity, DbContext dbContext, IReadOnlyList<ColumnDefinition> columnDefinitions)
        where T : class, new()
    {
        var entityType = dbContext.Model.FindEntityType(typeof(T)) ?? throw new InvalidOperationException(
            $"Entity '{typeof(T).Name}' is not mapped in the DbContext.");
        var tableIdentifier = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
        var properties = typeof(T).GetProperties().ToDictionary(p => p.Name, StringComparer.Ordinal);

        return columnDefinitions
            .Where(column => column.IsVisible)
            .OrderBy(column => column.Order.HasValue ? 0 : 1)
            .ThenBy(column => column.Order)
            .ToDictionary(
                column => column.Caption ?? entityType.FindProperty(column.PropertyName)?.GetColumnName(tableIdentifier) ?? column.PropertyName,
                column => properties.TryGetValue(column.PropertyName, out var property)
                    ? property.GetValue(entity)
                    : throw new InvalidOperationException($"Column definition references unknown property '{column.PropertyName}' on '{typeof(T).Name}'."));
    }

    public static Dictionary<string, ColumnDefinition> ResolveColumnDefinitions<T>()
    {
        return typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.CanRead)
            .Select(property => new ColumnDefinition(property.Name))
            .ToDictionary(x => x.PropertyName, x => x);
    }
}