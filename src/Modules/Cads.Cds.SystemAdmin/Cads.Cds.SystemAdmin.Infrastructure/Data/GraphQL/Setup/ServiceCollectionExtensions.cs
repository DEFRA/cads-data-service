using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Queries;
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
}