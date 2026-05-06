using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Testing.Support.Factories;
using Cads.Cds.MiBff.Testing.Support.Seeding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cads.Cds.MiBff.Tests.Component.TestFixtures;

public class MiBffWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    List<Action<IServiceCollection>>? serviceOverrides = null,
    bool useFakeAuth = false) : WebAppFactoryBase<Program>(
        configOverrides: configOverrides,
        serviceOverrides: serviceOverrides,
        useFakeAuth: useFakeAuth)
{
    protected override void ConfigureDatabase(IServiceCollection services)
    {
        var reportPermissionsData = new ReportPermissionsDataFactory().CreateMockData();

        var miBffReadDbContext = DbContextFactory.CreateInMemoryDbContext<MiBffReadDbContext>(Guid.NewGuid().ToString());
        TestMiBffDataSeeder.Seed(miBffReadDbContext, reportPermissionsData);

        var miBffWriteDbContext = DbContextFactory.CreateInMemoryDbContext<MiBffWriteDbContext>(Guid.NewGuid().ToString());
        TestMiBffDataSeeder.Seed(miBffWriteDbContext, reportPermissionsData);

        services.Replace(new ServiceDescriptor(typeof(MiBffReadDbContext), miBffReadDbContext));
        services.Replace(new ServiceDescriptor(typeof(MiBffWriteDbContext), miBffWriteDbContext));
    }
}