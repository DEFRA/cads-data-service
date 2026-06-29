using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;
using Cads.Cds.SystemAdmin.Testing.Support.Factories;
using Cads.Cds.SystemAdmin.Testing.Support.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Tests.Component.TestFixtures;

public class SystemAdminWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebAppFactoryBase<Program>(
        configOverrides: configOverrides,
        useFakeAuth: useFakeAuth)
{
    protected override void ConfigureDatabase(IServiceCollection services)
    {
        services.AddDbContext<SystemAdminReadDbContext>(o =>
            o.UseInMemoryDatabase("SystemAdminReadDb_" + Guid.NewGuid()));

        services.AddDbContext<SystemAdminWriteDbContext>(o =>
            o.UseInMemoryDatabase("SystemAdminWriteDb_" + Guid.NewGuid()));

        var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();
        var readDb = scope.ServiceProvider.GetRequiredService<SystemAdminReadDbContext>();
        var writeDb = scope.ServiceProvider.GetRequiredService<SystemAdminWriteDbContext>();

        var fileImportData = FileImportDataFactory.CreateMockData();

        TestSystemAdminDataSeeder.Seed(readDb, fileImportData);
        readDb.SaveChanges();

        TestSystemAdminDataSeeder.Seed(writeDb, fileImportData);
        writeDb.SaveChanges();
    }
}