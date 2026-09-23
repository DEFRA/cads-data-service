using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Handlers;

public class AwsStsAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IOptions<AuthenticationConfiguration> authConfig,
    IOptions<AclOptions> aclOptions,
    IHttpClientFactory httpClientFactory
) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    private static readonly ConcurrentDictionary<string, ConfigurationManager<OpenIdConnectConfiguration>> ConfigManagers = new();
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var endpoint = Context.GetEndpoint();
        if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            return AuthenticateResult.NoResult();

        if (!Request.Headers.TryGetValue("Authorization", out var headerValue))
            return AuthenticateResult.NoResult();

        var header = AuthenticationHeaderValue.Parse(headerValue!);
        if (!string.Equals(header.Scheme, "Bearer", StringComparison.OrdinalIgnoreCase) || header.Parameter is null)
            return AuthenticateResult.NoResult();

        var token = header.Parameter;
        var config = authConfig.Value.AwsOutboundFederation;

        if (!_tokenHandler.CanReadToken(token))
            return AuthenticateResult.Fail("Malformed token");

        var issuer = _tokenHandler.ReadJwtToken(token).Issuer;
        if (string.IsNullOrWhiteSpace(issuer))
            return AuthenticateResult.Fail("Token has no issuer");

        ClaimsPrincipal principal;
        try
        {
            var discovery = await GetIssuerConfigurationAsync(issuer);
            principal = _tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidateIssuer = true,
                ValidAudience = config.Audience,   // rejects tokens not addressed to us
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKeys = discovery.SigningKeys,
                NameClaimType = "sub"
            }, out _);
        }
        catch (Exception ex) when (ex is SecurityTokenException or InvalidOperationException)
        {
            Logger.LogWarning(ex, "STS token validation failed for issuer {Issuer}", issuer);
            return AuthenticateResult.Fail("Invalid token");
        }

        // Flatten the nested "https://sts.amazonaws.com/" claim into usable values.
        var stsClaimsJson = principal.FindFirst("https://sts.amazonaws.com/")?.Value;
        var stsClaims = string.IsNullOrEmpty(stsClaimsJson)
            ? default
            : JsonSerializer.Deserialize<JsonElement>(stsClaimsJson);

        var orgId = GetString(stsClaims, "org_id");
        var tags = stsClaims.ValueKind == JsonValueKind.Object && stsClaims.TryGetProperty("principal_tags", out var t)
            ? t : default;
        var serviceName = GetString(tags, "ServiceName");
        var environment = GetString(tags, "Environment");

        if (!string.Equals(orgId, config.TrustedOrgId, StringComparison.Ordinal))
            return AuthenticateResult.Fail("Token is not from a trusted AWS organization");

        if (!string.Equals(environment, config.Environment, StringComparison.OrdinalIgnoreCase))
            return AuthenticateResult.Fail($"Token minted for environment '{environment}', this service runs in '{config.Environment}'");

        if (serviceName is null || !aclOptions.Value.StsClients.TryGetValue(serviceName, out var client))
            return AuthenticateResult.Fail($"Caller '{serviceName}' is not on the allow list");

        var claims = new List<Claim> { new(ClaimTypes.Name, serviceName) };
        claims.AddRange(client.Scopes.Select(s => new Claim(AuthenticationConstants.ScopeClaimType, s)));

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name));
    }

    private static string? GetString(JsonElement element, string propertyName) =>
        element.ValueKind == JsonValueKind.Object && element.TryGetProperty(propertyName, out var value)
            ? value.GetString() : null;

    private Task<OpenIdConnectConfiguration> GetIssuerConfigurationAsync(string issuer)
    {
        var manager = ConfigManagers.GetOrAdd(issuer, iss => new ConfigurationManager<OpenIdConnectConfiguration>(
            $"{iss.TrimEnd('/')}/.well-known/openid-configuration",
            new OpenIdConnectConfigurationRetriever(),
            new HttpDocumentRetriever(httpClientFactory.CreateClient("proxy")) { RequireHttps = true }));

        return manager.GetConfigurationAsync(CancellationToken.None);
    }
}