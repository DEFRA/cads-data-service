using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Setup;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cts.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsAudit.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsTransactions.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSystemAdminData(this IServiceCollection services)
    {
        services.RegisterDbContexts();
        services.RegisterQueries();
        services.ConfigureSystemAdminGraphQL();

        return services;
    }

    private static void RegisterDbContexts(this IServiceCollection services)
    {
        services.AddPostgresDbContext<CadsSystemAdminDbContext>(PostgresPools.CadsGraphQLRead);
        services.AddPostgresDbContext<CtsAuditSystemAdminDbContext>(PostgresPools.CtsAuditGraphQLRead);
        services.AddPostgresDbContext<CtsSystemAdminDbContext>(PostgresPools.CtsGraphQLRead);
        services.AddPostgresDbContext<CtsTransactionsSystemAdminDbContext>(PostgresPools.CtsTransactionsGraphQLRead);
    }

    private static void RegisterQueries(this IServiceCollection services)
    {
        services.AddSingleton<ITableDependencyGraph<CadsSystemAdminDbContext>>(sp =>
        {
            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CadsSystemAdminDbContext>();
            return new TableDependencyGraph<CadsSystemAdminDbContext>(dbContext);
        });

        services.AddSingleton<ITableDependencyGraph<CtsAuditSystemAdminDbContext>>(sp =>
        {
            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CtsAuditSystemAdminDbContext>();
            return new TableDependencyGraph<CtsAuditSystemAdminDbContext>(dbContext);
        });

        services.AddSingleton<ITableDependencyGraph<CtsSystemAdminDbContext>>(sp =>
        {
            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CtsSystemAdminDbContext>();
            return new TableDependencyGraph<CtsSystemAdminDbContext>(dbContext);
        });

        services.AddSingleton<ITableDependencyGraph<CtsTransactionsSystemAdminDbContext>>(sp =>
        {
            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CtsTransactionsSystemAdminDbContext>();
            return new TableDependencyGraph<CtsTransactionsSystemAdminDbContext>(dbContext);
        });
    }
}