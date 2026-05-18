using AutoFixture;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Testing.Support.Data;
using Cads.Cds.MiBff.Testing.Support.SpecimenBuilders;

namespace Cads.Cds.MiBff.Testing.Support.Factories;

public class GbCattleReportDataFactory
{
    private readonly Fixture _fixture;

    public GbCattleReportDataFactory()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new IgnoreNavigationProperties());
    }

    public GbCattleReportData CreateMockData()
    {
        var deaths = new List<MiDeathSummary>
        {
            _fixture.Build<MiDeathSummary>()
                .With(x => x.Sex, () => null)
                .With(x => x.DeathYear, () => 2001)
                .Create()
        };

        deaths.AddRange(_fixture
            .Build<MiDeathSummary>()
            .With(x => x.DeathYear, () => 2026)
            .CreateMany(5));

        var births = new List<MiBirthSummary>
        {
            _fixture.Build<MiBirthSummary>()
                .With(x => x.GovRegion, () => null)
                .With(x => x.County, () => null)
                .With(x => x.Sex, () => null)
                .With(x => x.ApplicationType, () => null)
                .With(x => x.BirthYear, () => 2001)
                .Create()
        };

        births.AddRange(_fixture
            .Build<MiBirthSummary>()
            .With(x => x.BirthYear, () => 2026)
            .CreateMany(5));

        return new GbCattleReportData(deaths, births);
    }
}