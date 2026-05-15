using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Testing.Support.Contexts;
using Cads.Cds.MiBff.Testing.Support.Factories;
using Cads.Cds.MiBff.Testing.Support.Seeding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cads.Cds.MiBff.Tests.Component.TestFixtures;

public class MiBffWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false,
    List<Action<TestMiBffReadDbContext>>? dataOverrides = null) : WebAppFactoryBase<Program>(
        configOverrides: configOverrides,
        useFakeAuth: useFakeAuth)
{
    public MiBffWriteDbContext WriteDbContext => Services.GetRequiredService<MiBffWriteDbContext>();

    protected override void ConfigureDatabase(IServiceCollection services)
    {
        var reportPermissionsData = new ReportPermissionsDataFactory().CreateMockData();
        var miBffReadDbContext = DbContextFactory.CreateInMemoryTestDbContextFromDbContext<MiBffReadDbContext, TestMiBffReadDbContext>(Guid.NewGuid().ToString());
        TestMiBffDataSeeder.Seed(miBffReadDbContext, reportPermissionsData);

        var miBffWriteDbContext = DbContextFactory.CreateInMemoryDbContext<MiBffWriteDbContext>(Guid.NewGuid().ToString());
        TestMiBffDataSeeder.Seed(miBffWriteDbContext, reportPermissionsData);

        services.Replace(new ServiceDescriptor(typeof(MiBffReadDbContext), miBffReadDbContext));
        services.Replace(new ServiceDescriptor(typeof(MiBffWriteDbContext), miBffWriteDbContext));

        dataOverrides?.ForEach(overrideAction => overrideAction(miBffReadDbContext));
    }
}