using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Versioning;

public static class VersionedApiExtensions
{
    public static RouteGroupBuilder MapVersionedApi(
        this IEndpointRouteBuilder endpoints,
        string routePrefix)
    {
        var versionSet = endpoints
            .NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .ReportApiVersions()
            .Build();

        return endpoints
            .MapGroup($"/api/v{{version:apiVersion}}/{routePrefix}")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(new ApiVersion(1, 0));
    }
}