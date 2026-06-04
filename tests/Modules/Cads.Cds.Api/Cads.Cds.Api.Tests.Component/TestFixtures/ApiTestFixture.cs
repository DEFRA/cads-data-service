using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;

namespace Cads.Cds.Api.Tests.Component.TestFixtures;

public class ApiTestFixture : TestFixtureBase<Program, ApiWebApplicationFactory>
{
    public ApiTestFixture()
        : base(new ApiWebApplicationFactory(useFakeAuth: true))
    {
    }
}