using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Testing.Support.Seeding;

public static class TestSystemAdminDataSeeder
{
    /// <summary>
    /// Add in an order that respects FK constraints
    /// </summary>
    /// <param name="context"></param>
    public static void Seed(DbContext context, List<FileImport> fileImports)
    {
        context.AddRange(fileImports);
    }
}