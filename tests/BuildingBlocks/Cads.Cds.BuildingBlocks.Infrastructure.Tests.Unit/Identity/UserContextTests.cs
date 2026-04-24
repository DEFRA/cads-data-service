using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.BuildingBlocks.Infrastructure.Identity;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Security.Claims;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Identity;

public class UserContextTests
{
    [Fact]
    public void UserContext_UsesOidClaim_WhenPresent()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim(CustomClaimTypes.Oid, "aad-oid"));

        ctx.Oid.Should().Be("aad-oid");
    }

    [Fact]
    public void UserContext_FallsBackToNameIdentifier_WhenOidMissing()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim(ClaimTypes.NameIdentifier, "name-id"));

        ctx.Oid.Should().Be("name-id");
    }

    [Fact]
    public void UserContext_FallsBackToSub_WhenOidAndNameIdMissing()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim(CustomClaimTypes.CognitoSub, "cognito-sub"));

        ctx.Oid.Should().Be("cognito-sub");
    }

    [Fact]
    public void UserContext_UsesEmailClaim_WhenPresent()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim(ClaimTypes.Email, "user@example.com"));

        ctx.Email.Should().Be("user@example.com");
    }

    [Fact]
    public void UserContext_FallsBackToPreferredUsername_WhenEmailMissing()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim("preferred_username", "user@domain.com"));

        ctx.Email.Should().Be("user@domain.com");
    }

    [Fact]
    public void UserContext_UsesNameClaim_WhenPresent()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim("name", "Test User"));

        ctx.DisplayName.Should().Be("Test User");
    }

    [Fact]
    public void UserContext_DisplayNameFallsBackToEmail()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim(ClaimTypes.Email, "user@example.com"));

        ctx.DisplayName.Should().Be("user@example.com");
    }

    [Fact]
    public void UserContext_DisplayNameFallsBackToOid()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim(CustomClaimTypes.Oid, "oid-123"));

        ctx.DisplayName.Should().Be("oid-123");
    }

    [Fact]
    public void UserContext_UsesTenantId_WhenTidPresent()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim(CustomClaimTypes.TenantId, "tenant-123"));

        ctx.TenantId.Should().Be("tenant-123");
    }

    [Fact]
    public void UserContext_FallsBackToCustomTenantId()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim(CustomClaimTypes.CustomTenantId, "cognito-tenant"));

        ctx.TenantId.Should().Be("cognito-tenant");
    }

    [Fact]
    public void UserContext_UserIdentifierUsesEmailClaim_WhenPresent()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim(ClaimTypes.Email, "user.email@example.com"));

        ctx.UserIdentifier.Should().Be("user.email@example.com");
    }

    [Fact]
    public void UserContext_UserIdentifierUsesUniqueNameClaim_WhenEmail_Missing()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim("unique_name", "user.unique_name@example.com"));

        ctx.UserIdentifier.Should().Be("user.unique_name@example.com");
    }

    [Fact]
    public void UserContext_UserIdentifierUsesUpnClaim_WhenEmail_And_UniqueName_Missing()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim("upn", "user.upn@example.com"));

        ctx.UserIdentifier.Should().Be("user.upn@example.com");
    }

    [Fact]
    public void UserContext_UserIdentifierFallsBackToOid_WhenOthersMissing()
    {
        var ctx = UserContextUtilities.CreateUserContext(new Claim(CustomClaimTypes.Oid, "aad-oid"));

        ctx.UserIdentifier.Should().Be("aad-oid");
    }

    [Fact]
    public void UserContext_HandlesNullHttpContext()
    {
        var accessor = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns((HttpContext?)null);

        var ctx = new UserContext(accessor);

        ctx.Oid.Should().BeNull();
        ctx.Email.Should().BeNull();
        ctx.DisplayName.Should().BeNull();
        ctx.TenantId.Should().BeNull();
        ctx.UserIdentifier.Should().BeNull();
    }

    [Fact]
    public void UserContext_HandlesEmptyClaimsPrincipal()
    {
        var ctx = UserContextUtilities.CreateUserContext();

        ctx.Oid.Should().BeNull();
        ctx.Email.Should().BeNull();
        ctx.DisplayName.Should().BeNull();
        ctx.TenantId.Should().BeNull();
        ctx.UserIdentifier.Should().BeNull();
    }
}