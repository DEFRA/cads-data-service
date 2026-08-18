using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;

namespace Cads.Cds.SystemAdmin.Tests.Component.TestFixtures;

public class SystemAdminTestFixture : TestFixtureBase<Program, SystemAdminWebApplicationFactory>
{
    public SystemAdminTestFixture()
        : base(new SystemAdminWebApplicationFactory(useFakeAuth: true))
    {
    }
}