using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;

namespace Cads.Cds.Ingester.Tests.Component.TestFixtures;

public class IngesterTestFixture : TestFixtureBase<Program, IngesterWebApplicationFactory>
{
    public IngesterTestFixture()
        : base(new IngesterWebApplicationFactory(useFakeAuth: true))
    {
    }
}