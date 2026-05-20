using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Testing.Support.Contexts;
using Cads.Cds.StorageBridge.Testing.Support.Seeding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cads.Cds.StorageBridge.Tests.Component.TestFixtures;

public class StorageBridgeWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebAppFactoryBase<Program>(
        configOverrides: configOverrides,
        useFakeAuth: useFakeAuth)
{
    protected override void ConfigureDatabase(IServiceCollection services)
    {
        var storageBridgeReadDbContext = DbContextFactory.CreateInMemoryTestDbContextFromDbContext<StorageBridgeReadDbContext, TestStorageBridgeReadDbContext>(Guid.NewGuid().ToString());
        TestStorageBridgeDataSeeder.SeedSaveChanges(storageBridgeReadDbContext);

        var storageBridgeWriteDbContext = DbContextFactory.CreateInMemoryDbContext<StorageBridgeWriteDbContext>(Guid.NewGuid().ToString());
        TestStorageBridgeDataSeeder.SeedSaveChanges(storageBridgeWriteDbContext);

        services.Replace(new ServiceDescriptor(typeof(StorageBridgeReadDbContext), storageBridgeReadDbContext));
        services.Replace(new ServiceDescriptor(typeof(StorageBridgeWriteDbContext), storageBridgeWriteDbContext));
    }
}