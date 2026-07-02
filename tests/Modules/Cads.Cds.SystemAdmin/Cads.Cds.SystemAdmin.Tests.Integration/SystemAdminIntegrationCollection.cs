using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

namespace Cads.Cds.SystemAdmin.Tests.Integration;

[CollectionDefinition("SystemAdminIntegration")]
public class SystemAdminIntegrationCollection
    : ICollectionFixture<ApiContainerFixture>
{
}