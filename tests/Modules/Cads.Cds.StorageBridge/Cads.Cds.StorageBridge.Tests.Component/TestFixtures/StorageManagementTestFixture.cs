using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using Cads.Cds.StorageBridge.Testing.Support.Constants;

namespace Cads.Cds.StorageBridge.Tests.Component.TestFixtures;

public class StorageManagementTestFixture : TestFixtureBase<Program, StorageBridgeWebApplicationFactory>
{
    public StorageManagementTestFixture()
        : base(CreateFactory())
    {
    }

    private static StorageBridgeWebApplicationFactory CreateFactory()
    {
        Environment.SetEnvironmentVariable("Modules__StorageBridge__Storage__StorageManager__Enabled", "true");
        Environment.SetEnvironmentVariable("Modules__StorageBridge__Storage__StorageManager__Salt", S3StorageConstants.Salt);

        return new StorageBridgeWebApplicationFactory(useFakeAuth: true);
    }
}