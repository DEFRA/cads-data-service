using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Queries;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cads.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cts.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsAudit.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsTransactions.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSystemAdminGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer("CadsSchema")
            .AddAuthorization()
            .AddQueryType(q => q.Name("Query"))
            .AddTypeExtension<ErdQuery<CadsGQDbContext>>()
            .AddTypeExtension<CadsGraphQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddPagingArguments();

        services
            .AddGraphQLServer("CtsSchema")
            .AddAuthorization()
            .AddQueryType(q => q.Name("Query"))
            .AddTypeExtension<ErdQuery<CtsGQDbContext>>()
            .AddTypeExtension<CtsGraphQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddPagingArguments();

        services
            .AddGraphQLServer("CtsAuditSchema")
            .AddAuthorization()
            .AddQueryType(q => q.Name("Query"))
            .AddTypeExtension<ErdQuery<CtsAuditGQDbContext>>()
            .AddTypeExtension<CtsAuditGraphQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddPagingArguments();

        services
            .AddGraphQLServer("CtsTransactionsSchema")
            .AddAuthorization()
            .AddQueryType(q => q.Name("Query"))
            .AddTypeExtension<ErdQuery<CtsTransactionsGQDbContext>>()
            .AddTypeExtension<CtsTransactionsGraphQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddPagingArguments();

        return services;
    }
}