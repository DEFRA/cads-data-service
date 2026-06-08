using AutoFixture;
using Cads.Cds.Api.Core.Domain.Entities;
using Cads.Cds.Api.Testing.Support.Constants;
using Cads.Cds.Api.Testing.Support.Specimens.Builders;

namespace Cads.Cds.Api.Testing.Support.Specimens.Factories;

public class LocationSummaryDataFactory
{
    private readonly Fixture _fixture;

    public LocationSummaryDataFactory()
    {
        _fixture = new Fixture();
        _fixture.Customize<DateOnly>(c => c.FromFactory(() => DateOnly.FromDateTime(DateTime.Today)));
    }

    public List<LocationSummary> CreateMockData()
    {
        var identifiers = Enumerable.Range(0, 10).Select(x => string.Format(TestLocationConstants.CphIdentifierBase, x)).ToList();

        var dt = new DateOnly(2021, 01, 01);
        var dates = Enumerable.Range(0, 10)
            .Select(dt.AddDays)
            .ToList();

        _fixture.Customizations.Add(new LocationSummaryBuilder(identifiers, dates));

        var locations = _fixture.CreateMany<LocationSummary>(identifiers.Count).ToList();

        return locations;
    }
}