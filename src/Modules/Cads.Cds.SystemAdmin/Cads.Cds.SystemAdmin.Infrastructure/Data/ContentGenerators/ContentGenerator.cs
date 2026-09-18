using Bogus;
using Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators.Extensions;
using Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators.Rules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators;

public abstract class ContentGenerator<T>(DbContext dbContext)
    where T : class, new()
{
    protected virtual RuleBuilder<T>? OverrideRulesBuilder { get; init; }

    // hook for per-item bulk transform: (entity, businessKey) => modifiedEntity
    protected virtual Func<T, decimal, T>? BulkTransform { get; init; }

    // hook for single-update transform: (entity, businessKey) => modifiedEntity
    protected virtual Func<T, decimal, T>? UpdateTransform { get; init; }

    public string Table => dbContext.Model.FindEntityType(typeof(T))?.GetTableName() ?? typeof(T).Name;

    private static void ApplyDefaultRules(Faker<T> faker)
    {
        foreach (var property in typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.CanWrite))
        {
            faker.RuleFor(property.Name, f => f.GenerateDefaultValue(property));
        }
    }

    private static void ApplyOverrideRules(Faker<T> faker, RuleBuilder<T>? rulesBuilder)
    {
        if (rulesBuilder == null)
            return;

        foreach (var rule in rulesBuilder.Rules)
        {
            faker.RuleFor(rule.Key, f => rule.Value());
        }
    }

    public Faker<T> CreateFaker(int seed)
    {
        var faker = new Faker<T>();
        ApplyDefaultRules(faker);
        ApplyOverrideRules(faker, OverrideRulesBuilder);
        faker.UseSeed(seed);
        return faker;
    }

    public IReadOnlyList<IReadOnlyDictionary<string, object?>> GenerateBulk(IReadOnlyList<decimal> businessKeys, int seed)
    {
        var faker = CreateFaker(seed);
        return businessKeys
            .Select(key =>
            {
                var entity = faker.Generate();
                if (BulkTransform != null)
                    entity = BulkTransform(entity, key);
                return entity.ToDictionary(dbContext);
            })
            .ToList();
    }

    public IReadOnlyDictionary<string, object?> GenerateUpdate(decimal businessKey, int seed)
    {
        var faker = CreateFaker(seed);
        var entity = faker.Generate();
        if (UpdateTransform != null)
            entity = UpdateTransform(entity, businessKey);
        return entity.ToDictionary(dbContext);
    }
}