using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

namespace Cads.Cds.Ingester.Tests.Integration;

[CollectionDefinition("IngesterIntegration")]
public class IngesterIntegrationCollection :
    ICollectionFixture<ApiContainerFixture>
{ }