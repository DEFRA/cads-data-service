using Cads.Cds.Api.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Api.Testing.Support.Seeding;

public static class TestApiDataSeeder
{
    /// <summary>
    /// Add in an order that respects FK constraints
    /// </summary>
    /// <param name="context"></param>
    public static void Seed(DbContext context, List<LocationSummary> locationSummaries)
    {
        context.AddRange(locationSummaries);
    }
}