using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;

namespace Cads.Cds.MiBff.Tests.Component.TestFixtures;

public class MiBffTestFixture : TestFixtureBase<Program, MiBffWebApplicationFactory>
{
    public MiBffTestFixture()
        : base(new MiBffWebApplicationFactory())
    {
    }
}