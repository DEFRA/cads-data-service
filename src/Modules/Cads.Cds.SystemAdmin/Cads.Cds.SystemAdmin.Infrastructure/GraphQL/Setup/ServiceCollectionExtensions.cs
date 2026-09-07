using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cads.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cads.Queries;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cts.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cts.Queries;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsAudit.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsAudit.Queries;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSystemAdminGraphQL(this IServiceCollection services)
    {
        services.RegisterDbContexts();

        services
            .AddGraphQLServer("CadsSchema")
            .AddAuthorization()
            .AddQueryType(q => q.Name("Query"))
            .AddTypeExtension<CadsGraphQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddPagingArguments();

        services
            .AddGraphQLServer("CtsSchema")
            .AddAuthorization()
            .AddQueryType(q => q.Name("Query"))
            .AddTypeExtension<CtsGraphQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddPagingArguments();

        services
            .AddGraphQLServer("CtsAuditSchema")
            .AddAuthorization()
            .AddQueryType(q => q.Name("Query"))
            .AddTypeExtension<CtsAuditGraphQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddPagingArguments();

        services
            .AddGraphQLServer("CtsTransactionsSchema")
            .AddAuthorization()
            .AddQueryType(q => q.Name("Query"))
            .AddTypeExtension<CtsTransactionsGraphQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddPagingArguments();

        return services;
    }

    private static void RegisterDbContexts(this IServiceCollection services)
    {
        services.AddPostgresDbContext<CadsGraphQLDbContext>();
        services.AddPostgresDbContext<CtsGraphQLDbContext>();
        services.AddPostgresDbContext<CtsAuditGraphQLDbContext>();
        services.AddPostgresDbContext<CtsTransactionsGraphQLDbContext>();
    }
}