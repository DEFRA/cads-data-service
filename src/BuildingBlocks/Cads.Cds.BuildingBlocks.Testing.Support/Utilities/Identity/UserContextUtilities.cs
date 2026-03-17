using Cads.Cds.BuildingBlocks.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Security.Claims;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Identity;

public static class UserContextUtilities
{
    public static UserContext CreateUserContext(params Claim[] claims)
    {
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext
        {
            User = principal
        };

        var accessor = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns(httpContext);

        return new UserContext(accessor);
    }
}