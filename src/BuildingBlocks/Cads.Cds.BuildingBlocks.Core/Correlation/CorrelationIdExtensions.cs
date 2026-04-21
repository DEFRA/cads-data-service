using Microsoft.AspNetCore.Builder;

namespace Cads.Cds.BuildingBlocks.Core.Correlation;

public static class CorrelationIdExtensions
{
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CorrelationIdMiddleware>();
    }
}