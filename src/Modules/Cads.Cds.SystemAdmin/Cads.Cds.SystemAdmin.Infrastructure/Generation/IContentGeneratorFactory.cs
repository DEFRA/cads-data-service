using Cads.Cds.SystemAdmin.Application.Generation.ContentGenerators;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation;

public interface IContentGeneratorFactory
{
    IContentGenerator Create(string tableName, DbContext dbContext);

    IContentGenerator Create<T>(DbContext dbContext, RuleBuilder<T>? rulesBuilder = null, Func<T, decimal, T>? transform = null)
        where T : class, new();
}
