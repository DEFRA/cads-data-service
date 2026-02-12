using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Cads.Cds.StorageBridge.Tests.Component.TestFixtures;

public class StorageBridgeTestFixture : TestFixtureBase<Program, StorageBridgeWebApplicationFactory>
{
    public StorageBridgeTestFixture()
        : base(new StorageBridgeWebApplicationFactory())
    {
    }
}