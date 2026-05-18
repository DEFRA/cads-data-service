using Cads.Cds.BuildingBlocks.Testing.Support.Persistence;
using Cads.Cds.MiBff.Testing.Support.Contexts;
using Cads.Cds.MiBff.Testing.Support.Data;
using Cads.Cds.MiBff.Testing.Support.Factories;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Testing.Support.Seeding;

public static class TestMiBffDataSeeder
{
    /// <summary>
    /// Add in an order that respects FK constraints
    /// </summary>
    /// <param name="context"></param>
    /// <param name="reportPermissionsData"></param>
    public static void Seed(DbContext context, ReportPermissionsData reportPermissionsData)
    {
        // Add in an order that respects FK constraints
        context.AddRange(reportPermissionsData.MiRoles);
        context.AddRange(reportPermissionsData.MiPermissions);
        context.AddRange(reportPermissionsData.MiReports);
        context.AddRange(reportPermissionsData.MiUsers);
        context.AddRange(reportPermissionsData.MiRoleReportPermissions);
        context.AddRange(reportPermissionsData.MiUserRoles);
        context.AddRange(reportPermissionsData.MiRoleReportPermissions);
        context.AddRange(reportPermissionsData.MiEffectiveReportPermissions);
        context.AddRange(reportPermissionsData.MiEffectiveReportAllPermissions);
        context.SaveChanges();
    }

    public static void Seed(TestMiBffReadDbContext context, ReportData reportData)
    {
        FakeQueryProvider.SetQuery(reportData.Births);
        FakeQueryProvider.SetQuery(reportData.Deaths);
    }
}