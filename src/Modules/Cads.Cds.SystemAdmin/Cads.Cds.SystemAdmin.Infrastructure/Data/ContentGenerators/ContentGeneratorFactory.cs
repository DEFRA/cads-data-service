using Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators.Schemas.CtsTransactions;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators;

public class ContentGeneratorFactory
{
    public IContentGenerator Create(string tableName, DbContext dbContext)
    {
        return tableName.ToLowerInvariant() switch
        {
            "ct_locations" => new CtLocationContentGenerator(dbContext),
            _ => throw new ArgumentException($"No content generator found for table '{tableName}'.", nameof(tableName)),
        };
    }
}