using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.MiBff.Application.Reports.Authorisation;
using Cads.Cds.MiBff.Infrastructure.Authorisation.Reports;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Testing.Support.Contexts;
using Cads.Cds.MiBff.Testing.Support.Factories;
using Cads.Cds.MiBff.Testing.Support.Seeding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cads.Cds.MiBff.Tests.Component.TestFixtures;

public class MiBffWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebAppFactoryBase<Program>(
        configOverrides: configOverrides,
        useFakeAuth: useFakeAuth)
{
    private readonly string _dbName = $"MiBffDb_{Guid.NewGuid()}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IReportAccessService>();
            services.AddTransient<IReportAccessService, ReportAccessService>();

            ConfigurePersistence(services);
        });
    }

    protected override void ConfigureDatabase(IServiceCollection services)
    {
        services.AddDbContext<MiBffReadDbContext, TestMiBffReadDbContext>(o =>
            o.UseInMemoryDatabase(_dbName));

        services.AddDbContext<MiBffWriteDbContext>(o =>
            o.UseInMemoryDatabase(_dbName));
    }

    private static void ConfigurePersistence(IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();

        var readDb = scope.ServiceProvider.GetRequiredService<MiBffReadDbContext>();

        // Seeds
        var reportPermissionsData = new ReportPermissionsDataFactory().CreateMockData();
        var reportsData = new GbCattleReportDataFactory().CreateMockData();

        TestMiBffDataSeeder.Seed(readDb, reportPermissionsData);
        TestMiBffDataSeeder.Seed(readDb, reportsData);

        readDb.SaveChanges();
    }
}