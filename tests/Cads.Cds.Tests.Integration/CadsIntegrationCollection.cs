using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures;
using Xunit;

namespace Cads.Cds.Tests.Integration;

[CollectionDefinition("CadsIntegration")]
public class CadsIntegrationCollection :
    ICollectionFixture<ApiContainerFixture>
{ }