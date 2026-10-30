using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Versioning;
using Cads.Cds.SystemAdmin.Application.Setup;
using Cads.Cds.SystemAdmin.Endpoints;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin;
using Cads.Cds.SystemAdmin.Endpoints.Generation;
using Cads.Cds.SystemAdmin.Endpoints.SqsAdmin;
using Cads.Cds.SystemAdmin.Infrastructure.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Setup;

public sealed class SystemAdminModule : IModule
{
    public void AddServices(IServiceCollection services, IConfiguration config)
    {
        services.AddSystemAdminInfrastructureLayer(config);

        services.AddSystemAdminApplicationLayer(config);
    }

    /// <summary>
    /// Add minimal API endpoints here. This excludes standard controllers.
    /// </summary>
    /// <param name="app"></param>
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var config = app.ServiceProvider.GetRequiredService<IConfiguration>();
        var systemAdminApp = app.MapVersionedApi(SystemAdminEndpointsConstants.ApiRoutePrefix);

        if (config.GetValue("Modules:SystemAdmin:EnableAdminEndpoints", false))
        {
            systemAdminApp.CreateDbAdminEndpoints();
            systemAdminApp.CreateSqsAdminEndpoints();
        }

        if (config.GetValue("Modules:SystemAdmin:EnablePrepareCtsImportEndpoints", false))
        {
            systemAdminApp.CreateDbAdminPrepareCtsImportEndpoints();
        }

        if (config.GetValue("Modules:SystemAdmin:EnableGenerationEndpoints", false))
        {
            systemAdminApp.CreateGenerationEndpoints();
        }

        if (config.GetValue("Modules:SystemAdmin:EnableGraphQlEndpoints", false))
        {
            app.MapGraphQL("/graphql/cads", schemaName: "CadsSchema")
                .RequireAuthorization(AuthenticationConstants.DefaultPolicy);
            app.MapGraphQL("/graphql/cts", schemaName: "CtsSchema")
                .RequireAuthorization(AuthenticationConstants.DefaultPolicy);
            app.MapGraphQL("/graphql/cts-audit", schemaName: "CtsAuditSchema")
                .RequireAuthorization(AuthenticationConstants.DefaultPolicy);
            app.MapGraphQL("/graphql/cts-transactions", schemaName: "CtsTransactionsSchema")
                .RequireAuthorization(AuthenticationConstants.DefaultPolicy);
        }
    }
}