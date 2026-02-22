namespace Cads.Cds.Api.Tests.Integration;

[CollectionDefinition("Integration")]
public class IntegrationCollection :
    ICollectionFixture<PostgresFixture>,
    ICollectionFixture<LocalStackFixture>,
    ICollectionFixture<ApiContainerFixture>
{ }