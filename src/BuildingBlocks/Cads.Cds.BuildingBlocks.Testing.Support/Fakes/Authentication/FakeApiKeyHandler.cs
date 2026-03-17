using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;

public class FakeApiKeyHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "test-apikey-user"),
            new(ClaimTypes.Email, "test-apikey-user@example.com"),
            new("name", "Test API Key User"),
            new(CustomClaimTypes.Oid, Guid.NewGuid().ToString()),
            new(CustomClaimTypes.TenantId, "test-internal-tenant")
        };

        var identity = new ClaimsIdentity(
            claims,
            AuthenticationConstants.ApiKeySchemeName
        );

        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AuthenticationConstants.ApiKeySchemeName);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}