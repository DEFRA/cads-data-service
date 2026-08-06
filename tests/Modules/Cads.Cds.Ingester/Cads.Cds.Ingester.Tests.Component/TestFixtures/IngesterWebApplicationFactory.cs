using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.Ingester.Application.Uow;
using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;
using Cads.Cds.Ingester.Testing.Support.Contexts;
using Cads.Cds.Ingester.Testing.Support.Fakes.Behaviours;
using Cads.Cds.Ingester.Testing.Support.Fakes.Uow;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
        services.AddScoped<IngesterReadDbContext>(_ =>
            new TestIngesterReadDbContext(
                new DbContextOptionsBuilder<IngesterReadDbContext>()
                    .UseInMemoryDatabase(_dbName)
                    .Options));

        services.AddDbContext<IngesterWriteDbContext>(o =>
            o.UseInMemoryDatabase(_dbName));

        services.RemoveAll<IIngesterUnitOfWork>();
        services.AddScoped<IIngesterUnitOfWork, FakeIngesterUnitOfWork>();
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