using Cads.Cds.Api.Core.DTOs;
using Cads.Cds.BuildingBlocks.Testing.Support.ProblemDetails;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using FluentAssertions;

namespace Cads.Cds.Api.Testing.Support.Utilities.Locations;

public static class LocationEndpointTestUtilities
{
    public static string InvalidScenario =>
        new LocationQueryBuilder().Build();

    public static string ValidScenario_WithCph =>
        new LocationQueryBuilder()
            .WithCph("12/345/6789")
            .Build();

    public static string ValidScenario_WithLastModifiedDate =>
        new LocationQueryBuilder()
            .WithLastModifiedDate(DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-1)))
            .Build();

    public static string ValidScenario_WithCph_AndLastModifiedDate =>
        new LocationQueryBuilder()
            .WithCph("12/345/6789")
            .WithLastModifiedDate(DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-1)))
            .Build();

    public static async Task VerifyInvalidScenario(HttpResponseMessage response)
    {
        var problemDetails = await HttpResponseMessageUtilities.VerifyBadRequest<ValidationProblemDetailsDto>(response);

        problemDetails.Errors.Should().HaveCount(2);
        problemDetails.Errors["Cph"].Should().Contain("'Cph' must not be empty.");
        problemDetails.Errors["LastModifiedDate"].Should().Contain("'Last Modified Date' must not be empty.");
    }

    public static async Task VerifyValidScenario_WithCph(HttpResponseMessage response)
    {
        var locations = await HttpResponseMessageUtilities.VerifyOk<IEnumerable<LocationDto>>(response);

        locations.Count().Should().Be(0);
    }

    public static async Task VerifyValidScenario_WithLastModifiedDate(HttpResponseMessage response)
    {
        var locations = await HttpResponseMessageUtilities.VerifyOk<IEnumerable<LocationDto>>(response);

        locations.Count().Should().Be(0);
    }

    public static async Task VerifyValidScenario_WithCph_AndLastModifiedDate(HttpResponseMessage response)
    {
        var locations = await HttpResponseMessageUtilities.VerifyOk<IEnumerable<LocationDto>>(response);

        locations.Count().Should().Be(0);
    }
}