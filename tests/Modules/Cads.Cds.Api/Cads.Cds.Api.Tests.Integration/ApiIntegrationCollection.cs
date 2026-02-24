using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures;

namespace Cads.Cds.Api.Tests.Integration;

[CollectionDefinition("ApiIntegration")]
public class ApiIntegrationCollection :
    ICollectionFixture<ApiContainerFixture>
{ }