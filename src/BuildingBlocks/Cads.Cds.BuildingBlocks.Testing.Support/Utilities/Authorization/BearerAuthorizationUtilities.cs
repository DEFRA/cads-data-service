using System.Net.Http.Headers;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;

public static class BearerAuthorizationUtilities
{
    public static void AddJwt(this HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "fake-jwt-token");
    }
}