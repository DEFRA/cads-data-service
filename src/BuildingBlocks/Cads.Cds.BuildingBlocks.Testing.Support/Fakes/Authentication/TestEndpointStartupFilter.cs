using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;

public class TestEndpointStartupFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            next(app);

            app.UseEndpoints(endpoints =>
            {
                var group = endpoints.MapGroup("/test-auth");

                group.MapGet("/basic/apikey", () => "OK: API Key")
                    .RequireAuthorization(AuthenticationConstants.ApiKeyOrCognitoPolicy);

                group.MapGet("/bearer/cognito", () => "OK: Cognito")
                    .RequireAuthorization(AuthenticationConstants.ApiKeyOrCognitoPolicy);

                group.MapGet("/azuread/reports", () => "OK: AzureAD ReportsRead")
                    .RequireAuthorization(AuthenticationConstants.AadReportsReadPolicy);
            });
        };
    }
}