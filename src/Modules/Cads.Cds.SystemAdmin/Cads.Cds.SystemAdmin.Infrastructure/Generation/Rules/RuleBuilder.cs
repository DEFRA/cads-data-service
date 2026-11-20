using Bogus;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;

public class RuleBuilder<T>(Dictionary<string, Func<Faker, object>> rules)
{
    public readonly Dictionary<string, Func<Faker, object>> Rules = rules;

    public RuleBuilder<T> RuleFor<TProperty>(
        Expression<Func<T, TProperty>> property,
        Func<TProperty> generator)
    {
        return RuleFor(property, _ => generator());
    }

    public RuleBuilder<T> RuleFor<TProperty>(
        Expression<Func<T, TProperty>> property,
        Func<Faker, TProperty> generator)
    {
        if (property.Body is not MemberExpression member)
            throw new ArgumentException("Invalid property expression");

        Rules[member.Member.Name] = faker => generator(faker)!;

        return this;
    }
}