using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

namespace Cads.Cds.StorageBridge.Tests.Integration.Consumers;

[Collection("StorageBridgeIntegration"), Trait("Dependence", "testcontainers")]
public class StorageBridgeFifoQueuePollerTests(ApiContainerFixture apiContainerFixture)
{
    private const int ProcessingTimeCircuitBreakerSeconds = 30;


}