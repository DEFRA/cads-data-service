using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;

namespace Cads.Cds.StorageBridge.Tests.Component.TestFixtures;

public class StorageManagementTestFixture : TestFixtureBase<Program, StorageBridgeWebApplicationFactory>
{
    public const string Salt = "test-salt";

    public StorageManagementTestFixture()
        : base(CreateFactory())
    {
    }

    private static StorageBridgeWebApplicationFactory CreateFactory()
    {
        Environment.SetEnvironmentVariable("Modules__StorageBridge__Storage__StorageManager__Enabled", "true");
        Environment.SetEnvironmentVariable("Modules__StorageBridge__Storage__StorageManager__Salt", Salt);

        return new StorageBridgeWebApplicationFactory(useFakeAuth: true);
    }
}