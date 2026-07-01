using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;
using Cads.Cds.Ingester.Testing.Support.Contexts;
using Cads.Cds.Ingester.Testing.Support.Fakes.Behaviours;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Ingester.Tests.Component.TestFixtures;

public class IngesterWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebAppFactoryBase<Program>(
    configOverrides: configOverrides,
    useFakeAuth: useFakeAuth)
{
    private readonly string _dbName = $"IngesterDb_{Guid.NewGuid()}";

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
        services.AddDbContext<IngesterReadDbContext, TestIngesterReadDbContext>(o =>
            o.UseInMemoryDatabase(_dbName));

        services.AddDbContext<IngesterWriteDbContext>(o =>
            o.UseInMemoryDatabase(_dbName));
    }

    private static void ConfigurePersistence(IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();

        var readDb = scope.ServiceProvider.GetRequiredService<IngesterReadDbContext>();

        // Seeds

        readDb.SaveChanges();

        // Real transactions are not suppoted by in memory db so use cut down version
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(TestIngesterCommitBehaviour<,>));
    }
}