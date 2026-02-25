using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

namespace Cads.Cds.Tests.Integration;

[CollectionDefinition("CadsIntegration")]
public class CadsIntegrationCollection :
    ICollectionFixture<ApiContainerFixture>
{ }