using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cads.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cts.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsAudit.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsTransactions.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Setup;
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
        services.AddPostgresDbContext<CadsGQDbContext>(PostgresPools.CadsGraphQLRead);
        services.AddPostgresDbContext<CtsAuditGQDbContext>(PostgresPools.CtsAuditGraphQLRead);
        services.AddPostgresDbContext<CtsGQDbContext>(PostgresPools.CtsGraphQLRead);
        services.AddPostgresDbContext<CtsTransactionsGQDbContext>(PostgresPools.CtsTransactionsGraphQLRead);
    }

    private static void RegisterQueries(this IServiceCollection services)
    {
        services.AddSingleton<ITableDependencyGraph<CadsGQDbContext>>(sp =>
        {
            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CadsGQDbContext>();
            return new TableDependencyGraph<CadsGQDbContext>(dbContext);
        });

        services.AddSingleton<ITableDependencyGraph<CtsAuditGQDbContext>>(sp =>
        {
            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CtsAuditGQDbContext>();
            return new TableDependencyGraph<CtsAuditGQDbContext>(dbContext);
        });

        services.AddSingleton<ITableDependencyGraph<CtsGQDbContext>>(sp =>
        {
            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CtsGQDbContext>();
            return new TableDependencyGraph<CtsGQDbContext>(dbContext);
        });

        services.AddSingleton<ITableDependencyGraph<CtsTransactionsGQDbContext>>(sp =>
        {
            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CtsTransactionsGQDbContext>();
            return new TableDependencyGraph<CtsTransactionsGQDbContext>(dbContext);
        });
    }
}