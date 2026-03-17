using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using System.Net.Http.Headers;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;

public static class BearerAuthorizationUtilities
{
    public static void AddJwt(this HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(AuthenticationConstants.CognitoSchemeName, "fake-jwt-token");
    }
}