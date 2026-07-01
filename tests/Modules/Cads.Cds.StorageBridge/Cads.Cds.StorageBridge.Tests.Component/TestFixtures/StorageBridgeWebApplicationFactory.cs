using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Testing.Support.Contexts;
using Cads.Cds.StorageBridge.Testing.Support.Fakes.Behaviours;
using Cads.Cds.StorageBridge.Testing.Support.Fakes.Channels;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
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
    private readonly string _dbName = $"StorageBridgeDb_{Guid.NewGuid()}";

    public TestCsvBulkLoadJobChannel TestCsvBulkLoadJobChannel { get; } = new();

    public TestSqlImportJobChannel TestSqlImportJobChannel { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            ConfigurePersistence(services);
            OverrideS3ImportChannels(services);
        });
    }

    protected override void ConfigureDatabase(IServiceCollection services)
    {
        services.AddDbContext<StorageBridgeReadDbContext, TestStorageBridgeReadDbContext>(o =>
            o.UseInMemoryDatabase(_dbName));

        services.AddDbContext<StorageBridgeWriteDbContext>(o =>
            o.UseInMemoryDatabase(_dbName));
    }

    private static void ConfigurePersistence(IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();

        var readDb = scope.ServiceProvider.GetRequiredService<StorageBridgeReadDbContext>();

        // Seeds

        readDb.SaveChanges();

        // Real transactions are not suppoted by in memory db so use cut down version
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(TestStorageBridgeCommitBehaviour<,>));
    }

    private void OverrideS3ImportChannels(IServiceCollection services)
    {
        services.RemoveAll<Channel<CreateS3ImportJobDto>>();
        services.RemoveAll<Channel<CreateS3SqlImportJobDto>>();

        services.AddSingleton(TestCsvBulkLoadJobChannel.Channel);
        services.AddSingleton(TestSqlImportJobChannel.Channel);
    }
}