using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

namespace Cads.Cds.Api.Tests.Integration;

[CollectionDefinition("ApiIntegration")]
public class ApiIntegrationCollection :
    ICollectionFixture<ApiContainerFixture>
{ }