using Cads.Cds.SystemAdmin.Application.Generation.ContentGenerators;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation;

public class ContentGeneratorFactory
{
    public static IContentGenerator Create(string tableName, DbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("tableName is required", nameof(tableName));

        // Find the EF entity whose mapped table name matches the incoming table name
        var entityType = dbContext.Model.GetEntityTypes()
            .FirstOrDefault(e => string.Equals(e.GetTableName(), tableName, StringComparison.OrdinalIgnoreCase)
                                 || string.Equals(e.GetTableName(), tableName.Replace("\"", ""), StringComparison.OrdinalIgnoreCase));

        if (entityType == null)
            throw new InvalidOperationException($"No entity mapping found for table '{tableName}' in the provided DbContext model.");

        var clrType = entityType.ClrType ?? throw new InvalidOperationException($"Entity for table '{tableName}' has a null CLR type.");

        // Call the generic static Create<T>(DbContext) via reflection
        var method = typeof(ContentGeneratorFactory)
            .GetMethod(nameof(Create), BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(DbContext) }, null)
            ?? throw new InvalidOperationException("Factory Create<T>(DbContext) method not found.");

        var generic = method.MakeGenericMethod(clrType);
        return (IContentGenerator)generic.Invoke(null, [dbContext])!;
    }

    public static ContentGenerator<T> Create<T>(DbContext dbContext)
        where T : class, new()
    {
        return new ContentGenerator<T>(dbContext, null, null);
    }

    public static IContentGenerator Create<T>(DbContext dbContext, RuleBuilder<T>? rulesBuilder, Func<T, decimal, T>? transform)
        where T : class, new()
    {
        return new ContentGenerator<T>(dbContext, rulesBuilder, transform);
    }
}