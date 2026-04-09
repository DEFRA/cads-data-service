using Cads.Cds.BuildingBlocks.Testing.Support.Constants;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;

public sealed class TestTokenRequest
{
    public string ClientId { get; init; } = TestAuthConstants.AzureAdCadsApiClientId;
    public string ClientSecret { get; init; } = TestAuthConstants.AzureAdCadsApiClientSecret;

    // Username null => client_credentials
    public string? Username { get; init; }
    public string? Password { get; init; }

    public string[] Scopes { get; init; } = [TestAuthConstants.AzureAdCadsCdsScope];
}