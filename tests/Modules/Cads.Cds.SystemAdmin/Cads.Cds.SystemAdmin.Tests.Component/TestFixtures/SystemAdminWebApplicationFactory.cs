using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;
using Cads.Cds.SystemAdmin.Testing.Support.Contexts;
using Cads.Cds.SystemAdmin.Testing.Support.Seeding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cads.Cds.SystemAdmin.Tests.Component.TestFixtures;

public class SystemAdminWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebAppFactoryBase<Program>(
        configOverrides: configOverrides,
        useFakeAuth: useFakeAuth)
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
        });
    }

    protected override void ConfigureDatabase(IServiceCollection services)
    {
        var systemAdminReadDbContext = DbContextFactory.CreateInMemoryTestDbContextFromDbContext<SystemAdminReadDbContext, TestSystemAdminReadDbContext>(Guid.NewGuid().ToString());
        TestSystemAdminDataSeeder.SeedSaveChanges(systemAdminReadDbContext);

        var systemAdminWriteDbContext = DbContextFactory.CreateInMemoryDbContext<SystemAdminWriteDbContext>(Guid.NewGuid().ToString());
        TestSystemAdminDataSeeder.SeedSaveChanges(systemAdminWriteDbContext);

        services.Replace(new ServiceDescriptor(typeof(SystemAdminReadDbContext), systemAdminReadDbContext));
        services.Replace(new ServiceDescriptor(typeof(SystemAdminWriteDbContext), systemAdminWriteDbContext));
    }
}