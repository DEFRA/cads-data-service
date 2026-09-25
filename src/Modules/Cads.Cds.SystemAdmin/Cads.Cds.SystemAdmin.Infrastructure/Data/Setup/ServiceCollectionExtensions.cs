using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Setup;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cts.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsAudit.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsTransactions.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSystemAdminData(this IServiceCollection services)
    {
        services.RegisterDbContexts();
        services.ConfigureSystemAdminGraphQL();

        return services;
    }

    private static void RegisterDbContexts(this IServiceCollection services)
    {
        services.AddPostgresDbContext<CadsSystemAdminDbContext>(PostgresPools.CadsGraphQLRead);
        services.AddPostgresDbContext<CtsSystemAdminDbContext>(PostgresPools.CtsGraphQLRead);
        services.AddPostgresDbContext<CtsAuditSystemAdminDbContext>(PostgresPools.CtsAuditGraphQLRead);
        services.AddPostgresDbContext<CtsTransactionsSystemAdminDbContext>(PostgresPools.CtsTransactionsGraphQLRead);
    }
}