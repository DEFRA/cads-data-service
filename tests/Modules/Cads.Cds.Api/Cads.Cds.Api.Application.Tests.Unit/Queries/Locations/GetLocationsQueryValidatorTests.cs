using Cads.Cds.Api.Application.Queries.Locations;
using FluentAssertions;

namespace Cads.Cds.Api.Application.Tests.Unit.Queries.Locations;

public class GetLocationsQueryValidatorTests
{
    [Theory]
    [MemberData(nameof(TestLocationQueryCases))]
    public void ShouldValidateQueryParametersCorrectly(string? cph, DateOnly? lastModifiedDate, bool expectedIsValid)
    {
        var query = new GetLocationsQuery()
        {
            Cph = cph,
            LastModifiedDate = lastModifiedDate
        };

        var sut = new GetLocationsQueryValidator();
        var result = sut.Validate(query);
        result.IsValid.Should().Be(expectedIsValid);
    }

    public static TheoryData<string?, DateOnly?, bool> TestLocationQueryCases =>
        new()
        {
            { null, null, false },
            { "12/345/6789", null, false },
            { "AH-12/345/6789", null, true },
            { "az-12/345/6789", null, false },
            { null, DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)), true },
            { "AB-12/345/6789", DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)), true }
        };
}