using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

namespace Cads.Cds.MiBff.Tests.Integration;

[CollectionDefinition("MiBffIntegration")]
public class MiBffIntegrationCollection :
    ICollectionFixture<ApiContainerFixture>
{ }

[CollectionDefinition("MiBffRepositoryIntegration")]
public class MiBffRepositoryIntegrationCollection :
    ICollectionFixture<PostgresFixture>
{ }