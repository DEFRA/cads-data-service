using Cads.Cds.BuildingBlocks.Testing.Support.Specimens.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using Cads.Cds.SystemAdmin.Application.Uow;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;
using Cads.Cds.SystemAdmin.Testing.Support.Contexts;
using Cads.Cds.SystemAdmin.Testing.Support.Fakes.Behaviours;
using Cads.Cds.SystemAdmin.Testing.Support.Fakes.Uow;
using Cads.Cds.SystemAdmin.Testing.Support.Seeding;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace Cads.Cds.SystemAdmin.Tests.Component.TestFixtures;

public class SystemAdminWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebAppFactoryBase<Program>(
        configOverrides: MergeConfigOverrides(configOverrides),
        useFakeAuth: useFakeAuth)
{
    private readonly string _dbName = $"SystemAdminDb_{Guid.NewGuid()}";

    private static IDictionary<string, string?> MergeConfigOverrides(IDictionary<string, string?>? overrides)
    {
        var merged = new Dictionary<string, string?>(overrides ?? new Dictionary<string, string?>());
        // Ensure DB admin endpoints are mapped for tests, since the production default is now false.
        merged.TryAdd("Modules:SystemAdmin:EnableDbAdminEndpoints", "true");
        return merged;
    }

    public Mock<IDbAdminExecuteCommandService> DbAdminExecuteCommandServiceMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            ConfigurePersistence(services);
            services.RemoveAll<IDbAdminExecuteCommandService>();
            services.AddScoped(_ => DbAdminExecuteCommandServiceMock.Object);
        });
    }

    protected override void ConfigureDatabase(IServiceCollection services)
    {
        services.AddScoped<SystemAdminReadDbContext>(_ =>
            new TestSystemAdminReadDbContext(
                new DbContextOptionsBuilder<SystemAdminReadDbContext>()
                    .UseInMemoryDatabase(_dbName)
                    .Options));

        services.AddDbContext<SystemAdminWriteDbContext>(o =>
            o.UseInMemoryDatabase(_dbName));

        services.RemoveAll<ISystemAdminUnitOfWork>();
        services.AddScoped<ISystemAdminUnitOfWork, FakeSystemAdminUnitOfWork>();
    }

    private static void ConfigurePersistence(IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();

        var readDb = scope.ServiceProvider.GetRequiredService<SystemAdminReadDbContext>();

        // Seeds
        TestSystemAdminDataSeeder.Seed(readDb, FileImportDataFactory.CreateMockData());

        readDb.SaveChanges();

        // Real transactions are not suppoted by in memory db so use cut down version
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(TestSystemAdminCommitBehaviour<,>));
    }
}