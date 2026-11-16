using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;
using Cads.Cds.SystemAdmin.Application.Generation.ContentGenerators;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation;

public class ContentGeneratorFactory(
    IBusinessColumnResolver businessColumnResolver,
    IBusinessRowMapper businessRowMapper) : IContentGeneratorFactory
{
    public IContentGenerator Create(string tableName, DbContext dbContext)
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

        // Call the generic CreateDefault<T>(DbContext) via reflection
        var method = typeof(ContentGeneratorFactory)
            .GetMethod(nameof(CreateDefault), BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new InvalidOperationException($"Factory {nameof(CreateDefault)}<T>(DbContext) method not found.");

        var generic = method.MakeGenericMethod(clrType);
        return (IContentGenerator)generic.Invoke(this, [dbContext])!;
    }

    public IContentGenerator Create<T>(DbContext dbContext, RuleBuilder<T>? rulesBuilder = null, Func<T, decimal, T>? transform = null)
        where T : class, new()
    {
        return new ContentGenerator<T>(dbContext, businessColumnResolver, businessRowMapper, rulesBuilder, transform);
    }

    private IContentGenerator CreateDefault<T>(DbContext dbContext)
        where T : class, new()
    {
        return Create<T>(dbContext);
    }
}
