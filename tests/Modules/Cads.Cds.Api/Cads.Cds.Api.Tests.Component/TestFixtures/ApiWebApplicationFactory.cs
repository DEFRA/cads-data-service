using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Cads.Cds.Api.Testing.Support.Contexts;
using Cads.Cds.Api.Testing.Support.Fakes.Behaviours;
using Cads.Cds.Api.Testing.Support.Seeding;
using Cads.Cds.Api.Testing.Support.Specimens.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Tests.Component.TestFixtures;

public class ApiWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebAppFactoryBase<Program>(
        configOverrides: configOverrides,
        useFakeAuth: useFakeAuth)
{
    private readonly string _dbName = $"ApiDb_{Guid.NewGuid()}";

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
        services.AddDbContext<ApiReadDbContext, TestApiReadDbContext>(o =>
            o.UseInMemoryDatabase(_dbName));

        services.AddDbContext<ApiWriteDbContext>(o =>
            o.UseInMemoryDatabase(_dbName));
    }

    private static void ConfigurePersistence(IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();

        var readDb = scope.ServiceProvider.GetRequiredService<ApiReadDbContext>();

        // Seeds
        TestApiDataSeeder.Seed(readDb, new LocationSummaryDataFactory().CreateMockData());

        readDb.SaveChanges();

        // Real transactions are not suppoted by in memory db so use cut down version
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(TestApiCommitBehaviour<,>));
    }
}