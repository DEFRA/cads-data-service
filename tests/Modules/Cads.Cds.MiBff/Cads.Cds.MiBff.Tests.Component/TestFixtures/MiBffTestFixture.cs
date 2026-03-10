using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using FluentAssertions;
using System.Text.Json;

namespace Cads.Cds.MiBff.Tests.Component.TestFixtures;

public class MiBffTestFixture : TestFixtureBase<Program, MiBffWebApplicationFactory>
{
    public MiBffTestFixture()
        : base(new MiBffWebApplicationFactory())
    {
    }

    public static JsonResponseData<T>? GetResponseData<T>(object? responseData)
    {
        if (responseData == null) return null;

        return JsonSerializer.Deserialize<JsonResponseData<T>>(JsonSerializer.Serialize(responseData));
    }

    public static void ValidateResponseWithMetaData(JsonResponseWithMetaData response, string expectedEndpoint)
    {
        response.Meta.Should().NotBeNull();
        response.Meta.RequestId.Should().NotBeNull();
        response.Meta.Status.Should().NotBeNull().And.Contain("Request successful");
        response.Meta.Timestamp.Should().NotBeNull();

        response.Links.Should().NotBeNull();
        response.Links.Self.Should().NotBeNull().And.Contain(expectedEndpoint);

        response.Data.Should().NotBeNull();
    }
}