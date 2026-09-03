using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSystemAdminGraphQL(this IServiceCollection services)
    {
        services.RegisterDbContexts();

        services
            .AddGraphQLServer()
            .AddQueryType<GraphQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddPagingArguments();

        return services;
    }

    private static void RegisterDbContexts(this IServiceCollection services)
    {
        services.AddPostgresDbContext<GraphQLDbContext>();
    }
}