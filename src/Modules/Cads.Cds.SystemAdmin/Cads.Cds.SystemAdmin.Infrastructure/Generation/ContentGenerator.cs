using Bogus;
using Cads.Cds.SystemAdmin.Application.Generation.ContentGenerators;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Extensions;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Models;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation;

public class ContentGenerator<T>(DbContext dbContext, RuleBuilder<T>? rulesBuilder = null, Func<T, decimal, T>? transform = null, IEnumerable<ColumnDefinition>? columnDefinitions = null)
    : IContentGenerator
    where T : class, new()
{
    private readonly IReadOnlyList<ColumnDefinition> _columnDefinitions = ResolveColumnDefinitions(columnDefinitions);

    protected RuleBuilder<T>? OverrideRulesBuilder { get; set; } = rulesBuilder;

    // hook for per-item bulk transform: (entity, businessKey) => modifiedEntity
    protected Func<T, decimal, T>? Transform { get; set; } = transform;

    public string TableName => dbContext.Model.FindEntityType(typeof(T))?.GetTableName() ?? typeof(T).Name;

    private static ColumnDefinition[] ResolveColumnDefinitions(IEnumerable<ColumnDefinition>? columnDefinitions)
    {
        if (columnDefinitions != null)
            return [.. columnDefinitions];

        return [.. typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.CanRead)
            .Select((property, order) => new ColumnDefinition(property.Name, order: order))];
    }

    private static void ApplyDefaultRules(Faker<T> faker, HashSet<string> hiddenProperties)
    {
        foreach (var property in typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.CanWrite && !hiddenProperties.Contains(x.Name)))
        {
            faker.RuleFor(property.Name, f => f.GenerateDefaultValue(property));
        }
    }

    private static void ApplyOverrideRules(Faker<T> faker, RuleBuilder<T>? rulesBuilder, HashSet<string> hiddenProperties)
    {
        if (rulesBuilder == null)
            return;

        foreach (var rule in rulesBuilder.Rules.Where(x => !hiddenProperties.Contains(x.Key)))
        {
            faker.RuleFor(rule.Key, f => rule.Value(f));
        }
    }

    protected Faker<T> CreateFaker(int seed)
    {
        var faker = new Faker<T>();
        var hiddenProperties = _columnDefinitions
            .Where(x => !x.IsVisible)
            .Select(x => x.PropertyName)
            .ToHashSet(StringComparer.Ordinal);
        ApplyDefaultRules(faker, hiddenProperties);
        ApplyOverrideRules(faker, OverrideRulesBuilder, hiddenProperties);
        faker.UseSeed(seed);
        return faker;
    }

    public IReadOnlyList<IReadOnlyDictionary<string, object?>> Generate(IEnumerable<decimal> businessKeys, int seed)
    {
        var faker = CreateFaker(seed);
        return [.. businessKeys
            .Select(key =>
            {
                var entity = faker.Generate();
                if (Transform != null)
                    entity = Transform(entity, key);
                return entity.ToDictionary(dbContext, _columnDefinitions);
            })];
    }
}