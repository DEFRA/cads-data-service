using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Testing.Support.Contexts;
using Cads.Cds.StorageBridge.Testing.Support.Fakes.Channels;
using Cads.Cds.StorageBridge.Testing.Support.Seeding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Tests.Component.TestFixtures;

public class StorageBridgeWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebAppFactoryBase<Program>(
        configOverrides: configOverrides,
        useFakeAuth: useFakeAuth)
{
    public TestCsvBulkLoadJobChannel TestCsvBulkLoadJobChannel { get; } = new();

    public TestSqlImportJobChannel TestSqlImportJobChannel { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            OverrideS3ImportChannels(services);
        });
    }

    protected override void ConfigureDatabase(IServiceCollection services)
    {
        var storageBridgeReadDbContext = DbContextFactory.CreateInMemoryTestDbContextFromDbContext<StorageBridgeReadDbContext, TestStorageBridgeReadDbContext>(Guid.NewGuid().ToString());
        TestStorageBridgeDataSeeder.SeedSaveChanges(storageBridgeReadDbContext);

        var storageBridgeWriteDbContext = DbContextFactory.CreateInMemoryDbContext<StorageBridgeWriteDbContext>(Guid.NewGuid().ToString());
        TestStorageBridgeDataSeeder.SeedSaveChanges(storageBridgeWriteDbContext);

        services.Replace(new ServiceDescriptor(typeof(StorageBridgeReadDbContext), storageBridgeReadDbContext));
        services.Replace(new ServiceDescriptor(typeof(StorageBridgeWriteDbContext), storageBridgeWriteDbContext));
    }

    private void OverrideS3ImportChannels(IServiceCollection services)
    {
        services.RemoveAll<Channel<CreateS3ImportJobDto>>();
        services.RemoveAll<Channel<CreateS3SqlImportJobDto>>();

        services.AddSingleton(TestCsvBulkLoadJobChannel.Channel);
        services.AddSingleton(TestSqlImportJobChannel.Channel);
    }
}