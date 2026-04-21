using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;

namespace Cads.Cds.Tests.Unit.TestFixtures;

public class CdsTestFixture : TestFixtureBase<Program, CdsWebApplicationFactory>
{
    public CdsWebApplicationFactory AppWebApplicationFactory => Factory;

    public CdsTestFixture()
        : base(new CdsWebApplicationFactory())
    {
    }
}