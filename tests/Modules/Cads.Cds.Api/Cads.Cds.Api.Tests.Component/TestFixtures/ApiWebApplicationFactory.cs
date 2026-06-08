using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Cads.Cds.Api.Testing.Support.Contexts;
using Cads.Cds.Api.Testing.Support.Seeding;
using Cads.Cds.Api.Testing.Support.Specimens.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cads.Cds.Api.Tests.Component.TestFixtures;

public class ApiWebApplicationFactory(
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
        var locationSummaryData = new LocationSummaryDataFactory().CreateMockData();

        var apiReadDbContext = DbContextFactory.CreateInMemoryTestDbContextFromDbContext<ApiReadDbContext, TestApiReadDbContext>(Guid.NewGuid().ToString());
        TestApiDataSeeder.Seed(apiReadDbContext, locationSummaryData);
        TestApiDataSeeder.SeedSaveChanges(apiReadDbContext);

        var apiWriteDbContext = DbContextFactory.CreateInMemoryDbContext<ApiWriteDbContext>(Guid.NewGuid().ToString());
        TestApiDataSeeder.SeedSaveChanges(apiWriteDbContext);

        services.Replace(new ServiceDescriptor(typeof(ApiReadDbContext), apiReadDbContext));
        services.Replace(new ServiceDescriptor(typeof(ApiWriteDbContext), apiWriteDbContext));
    }
}