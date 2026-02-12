using System.Net.Http.Headers;
using System.Text;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;

public static class BasicAuthorizationUtilities
{
    public static void AddBasicApiKey(this HttpClient client, string clientId, string secret)
    {
        var raw = $"{clientId}:{secret}";
        var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(raw));

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", base64);
    }
}