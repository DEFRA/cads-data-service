using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Behaviours;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkFileImportFailed;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;
using Cads.Cds.SystemAdmin.Testing.Support.Contexts;
using Cads.Cds.SystemAdmin.Testing.Support.Factories;
using Cads.Cds.SystemAdmin.Testing.Support.Fakes.Behaviours;
using Cads.Cds.SystemAdmin.Testing.Support.Seeding;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
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
            ConfigurePersistence(services);
        });
    }

    protected override void ConfigureDatabase(IServiceCollection services)
    {
        services.AddDbContext<SystemAdminReadDbContext, TestSystemAdminReadDbContext>(o =>
            o.UseInMemoryDatabase("SystemAdminDb"));

        services.AddDbContext<SystemAdminWriteDbContext>(o =>
            o.UseInMemoryDatabase("SystemAdminDb"));
    }

    private void ConfigurePersistence(IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();

        var readDb = scope.ServiceProvider.GetRequiredService<SystemAdminReadDbContext>();
        var writeDb = scope.ServiceProvider.GetRequiredService<SystemAdminWriteDbContext>();

        TestSystemAdminDataSeeder.Seed(readDb, FileImportDataFactory.CreateMockData());
        TestSystemAdminDataSeeder.Seed(writeDb, FileImportDataFactory.CreateMockData());

        readDb.SaveChanges();
        writeDb.SaveChanges();

        // Real transactions are not suppoted by in memory db so use cut down version
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(TestSystemAdminCommitBehaviour<,>));
    }
}