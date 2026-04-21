using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;
using System.Text.Json;

namespace Cads.Cds.MiBff.Tests.Component.TestFixtures;

public class MiBffTestFixture : TestFixtureBase<Program, MiBffWebApplicationFactory>
{
    public MiBffTestFixture()
        : base(new MiBffWebApplicationFactory(useFakeAuth: true))
    {
    }

    public static JsonResponseData<T>? GetResponseData<T>(object? responseData)
    {
        if (responseData == null) return null;

        return JsonSerializer.Deserialize<JsonResponseData<T>>(JsonSerializer.Serialize(responseData));
    }
}