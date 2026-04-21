using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

namespace Cads.Cds.StorageBridge.Tests.Integration;

[CollectionDefinition("StorageBridgeIntegration")]
public class StorageBridgeIntegrationCollection :
    ICollectionFixture<ApiContainerFixture>
{ }