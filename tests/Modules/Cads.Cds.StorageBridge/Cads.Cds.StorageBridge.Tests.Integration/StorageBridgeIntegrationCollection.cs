using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures;

namespace Cads.Cds.StorageBridge.Tests.Integration;

[CollectionDefinition("StorageBridgeIntegration")]
public class StorageBridgeIntegrationCollection :
    ICollectionFixture<ApiContainerFixture>
{ }