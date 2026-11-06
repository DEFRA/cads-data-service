using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Core.Domain.Outbox;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers;
using Cads.Cds.BuildingBlocks.Testing.Support.TestDoubles.Publishers;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Cads.Cds.MiBff.Application.Reports.Authorisation;
using Cads.Cds.MiBff.Application.Uow;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Authorisation.Reports;
using Cads.Cds.MiBff.Infrastructure.Messaging.Dispatchers;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Testing.Support.Contexts;
using Cads.Cds.MiBff.Testing.Support.Factories;
using Cads.Cds.MiBff.Testing.Support.Fakes.Repositories;
using Cads.Cds.MiBff.Testing.Support.Fakes.Uow;
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
            OverrideOutboxDispatcher(services);
        });
    }

    protected override void ConfigureDatabase(IServiceCollection services)
    {
        services.AddScoped<MiBffReadDbContext>(_ =>
            new TestMiBffReadDbContext(
                new DbContextOptionsBuilder<MiBffReadDbContext>()
                    .UseInMemoryDatabase(_dbName)
                    .Options));

        services.AddDbContext<MiBffWriteDbContext>(o =>
            o.UseInMemoryDatabase(_dbName));

        services.RemoveAll<IMiBffUnitOfWork>();
        services.AddScoped<IMiBffUnitOfWork, FakeMiBffUnitOfWork>();
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

    /// <summary>
    /// Replaces the real (Postgres-backed) outbox repository and SQS-backed publisher with
    /// in-memory test doubles, and registers IOutboxDispatcher{MiBffFifoQueueClient} so component
    /// tests can resolve it directly and drive it manually (the real
    /// ReportJobOutboxDispatcherListener - an IHostedService - is removed by WebAppFactoryBase).
    /// The test doubles must be singletons: MiReportJobOutboxDispatcher creates its own DI scope
    /// per poll via IServiceScopeFactory, which always resolves off the root container rather than
    /// the test's own scope, so a Scoped registration would hand the dispatcher a different
    /// instance than the one the test seeds/asserts against.
    /// </summary>
    private static void OverrideOutboxDispatcher(IServiceCollection services)
    {
        services.RemoveAll<IMiReportJobRepository>();
        services.AddSingleton<FakeMiReportJobRepository>();
        services.AddSingleton<IMiReportJobRepository>(sp => sp.GetRequiredService<FakeMiReportJobRepository>());

        // BaseOutboxDispatcher<TClient,TOutbox> resolves IOutboxRepository<TOutbox> directly
        // from DI rather than the module-specific IMiReportJobRepository.
        services.RemoveAll<IOutboxRepository<MiReportJobOutbox>>();
        services.AddSingleton<IOutboxRepository<MiReportJobOutbox>>(sp => sp.GetRequiredService<FakeMiReportJobRepository>());

        services.RemoveAll<IMessagePublisher<MiBffFifoQueueClient>>();
        services.AddSingleton<TestMessagePublisher<MiBffFifoQueueClient>>();
        services.AddSingleton<IMessagePublisher<MiBffFifoQueueClient>>(sp => sp.GetRequiredService<TestMessagePublisher<MiBffFifoQueueClient>>());

        services.RemoveAll<IOutboxDispatcher<MiBffFifoQueueClient>>();
        services.AddSingleton<IOutboxDispatcher<MiBffFifoQueueClient>, MiReportJobOutboxDispatcher>();
    }
}