using Cads.Cds.Api.Core.DTOs;
using Cads.Cds.Api.Testing.Support.Constants;
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
            .WithCph($"AH-{string.Format(TestLocationConstants.CphIdentifierBase, 1)}")
            .Build();

    public static string ValidScenario_WithLastModifiedDate =>
        new LocationQueryBuilder()
            .WithLastModifiedDate(new DateOnly(2021, 01, 01).AddDays(2))
            .Build();

    public static string ValidScenario_WithCph_AndLastModifiedDate =>
        new LocationQueryBuilder()
            .WithCph($"AH-{string.Format(TestLocationConstants.CphIdentifierBase, 3)}")
            .WithLastModifiedDate(new DateOnly(2021, 01, 01).AddDays(3))
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

        locations.Count().Should().Be(1);

        var location = locations.First();
        location.LidFullIdentifier.Should().Be($"AH-{string.Format(TestLocationConstants.CphIdentifierBase, 1)}");
        location.LocCurrentModifiedDate.Should().Be(new DateOnly(2021, 01, 01).AddDays(1));
    }

    public static async Task VerifyValidScenario_WithLastModifiedDate(HttpResponseMessage response)
    {
        var locations = await HttpResponseMessageUtilities.VerifyOk<IEnumerable<LocationDto>>(response);

        locations.Count().Should().Be(1);

        var location = locations.First();
        location.LidFullIdentifier.Should().Be($"AH-{string.Format(TestLocationConstants.CphIdentifierBase, 2)}");
        location.LocCurrentModifiedDate.Should().Be(new DateOnly(2021, 01, 01).AddDays(2));
    }

    public static async Task VerifyValidScenario_WithCph_AndLastModifiedDate(HttpResponseMessage response)
    {
        var locations = await HttpResponseMessageUtilities.VerifyOk<IEnumerable<LocationDto>>(response);

        locations.Count().Should().Be(1);

        var location = locations.First();
        location.LidFullIdentifier.Should().Be($"AH-{string.Format(TestLocationConstants.CphIdentifierBase, 3)}");
        location.LocCurrentModifiedDate.Should().Be(new DateOnly(2021, 01, 01).AddDays(3));
    }
}