using Cads.Cds.Api.Application.Queries.Locations;
using FluentAssertions;

namespace Cads.Cds.Api.Application.Tests.Unit.Queries.Locations;

public class GetLocationsQueryValidatorTests
{
    [Theory]
    [MemberData(nameof(TestLocationQueryCases))]
    public void ShouldValidateQueryParametersCorrectly(string? cph, DateTime? lastModifiedDate, int page, int pageSize, string? order, string? sort, bool expectedIsValid)
    {
        var query = new GetLocationsQuery()
        {
            Cph = cph,
            LastModifiedDate = lastModifiedDate,
            Page = page,
            PageSize = pageSize,
            Sort = sort,
            Order = order
        };

        var sut = new GetLocationsQueryValidator();
        var result = sut.Validate(query);
        result.IsValid.Should().Be(expectedIsValid);
    }

    public static IEnumerable<object[]> TestLocationQueryCases =>
    [
        [null!, null!, -1, -1, null!, null!, false],
        [null!, null!, 0, 0, null!, null!, false],
        [null!, null!, 1, 0, null!, null!, false],
        [null!, null!, 1, 1, null!, null!, false],
        [null!, null!, 1, 1, null!, "asc", false],
        ["12/345/6789", null!, 1, 1, null!, "asc", false],
        ["12/345/6789", null!, 1, 1, "property_name", "asc", true],
        ["12/345/6789", null!, 1, 1, "property_name", "desc", true],
        ["12/345/6789", null!, 1, 1, "property_name", "invalid", false],
        [null!, DateTime.Now.AddMonths(-1), 1, 1, "property_name", "asc", true],
        ["12/345/6789", DateTime.Now.AddMonths(-1), 1, 1, "property_name", "asc", true]
    ];
}