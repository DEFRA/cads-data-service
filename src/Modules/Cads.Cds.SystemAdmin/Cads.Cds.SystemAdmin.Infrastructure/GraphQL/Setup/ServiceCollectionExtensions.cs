using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Contexts;
using EntityGraphQL.Schema;
using EntityGraphQL.AspNet;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSystemAdminGraphQL(this IServiceCollection services)
    {
        services.RegisterDbContexts();

        // TODO: Uncomment this line when the GraphQL schema is ready to be registered
        //services.AddGraphQLSchema<GraphQLDbContext>();

        var schema = SchemaBuilder.FromObject<GraphQLDbContext>();

        services.AddSingleton(schema);

        return services;
    }

    private static void RegisterDbContexts(this IServiceCollection services)
    {
        services.AddPostgresDbContext<GraphQLDbContext>();
    }
}