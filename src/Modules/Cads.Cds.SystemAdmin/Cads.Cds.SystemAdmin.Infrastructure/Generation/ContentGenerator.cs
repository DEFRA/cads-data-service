using Bogus;
using Cads.Cds.SystemAdmin.Application.Generation.ContentGenerators;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Extensions;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation;

public class ContentGenerator<T>(DbContext dbContext, RuleBuilder<T>? rulesBuilder = null, Func<T, decimal, T>? transform = null)
    : IContentGenerator
    where T : class, new()
{
    protected RuleBuilder<T>? OverrideRulesBuilder { get; set; } = rulesBuilder;

    // hook for per-item bulk transform: (entity, businessKey) => modifiedEntity
    protected Func<T, decimal, T>? Transform { get; set; } = transform;

    public string TableName => dbContext.Model.FindEntityType(typeof(T))?.GetTableName() ?? typeof(T).Name;

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

    protected Faker<T> CreateFaker(int seed)
    {
        var faker = new Faker<T>();
        ApplyDefaultRules(faker);
        ApplyOverrideRules(faker, OverrideRulesBuilder);
        faker.UseSeed(seed);
        return faker;
    }

    public IReadOnlyList<IReadOnlyDictionary<string, object?>> Generate(IReadOnlyList<decimal> businessKeys, int seed)
    {
        var faker = CreateFaker(seed);
        return businessKeys
            .Select(key =>
            {
                var entity = faker.Generate();
                if (Transform != null)
                    entity = Transform(entity, key);
                return entity.ToDictionary(dbContext);
            })
            .ToList();
    }
}