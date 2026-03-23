using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cads.Cds.MiBff.Tests.Component.TestFixtures;

public class MiBffWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebAppFactoryBase<Program>(
        configOverrides: configOverrides,
        useFakeAuth: useFakeAuth)
{

    protected override void ConfigureDatabase(IServiceCollection services)
    {
        var miBffReadDbContext = DbContextFactory.CreateInMemoryDbContext<MiBffReadDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(miBffReadDbContext);

        var miBffWriteDbContext = DbContextFactory.CreateInMemoryDbContext<MiBffWriteDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(miBffWriteDbContext);

        services.Replace(new ServiceDescriptor(typeof(MiBffReadDbContext), miBffReadDbContext));
        services.Replace(new ServiceDescriptor(typeof(MiBffWriteDbContext), miBffWriteDbContext));
    }
}