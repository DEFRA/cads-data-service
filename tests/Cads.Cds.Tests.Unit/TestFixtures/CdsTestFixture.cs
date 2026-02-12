using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures;

namespace Cads.Cds.Tests.Unit.TestFixtures;

public class CdsTestFixture : TestFixtureBase<Program, CdsWebApplicationFactory>
{
    public CdsTestFixture()
        : base(new CdsWebApplicationFactory())
    {
    }
}