using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Queries;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cads.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cts.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsAudit.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsTransactions.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Setup;

public static class ServiceCollectionExtensions
{
    private const int MaxExecutionDepth = 4;

    public static IServiceCollection ConfigureSystemAdminGraphQL(this IServiceCollection services, IConfiguration config)
    {
        var enableIntrospection = config.GetValue("Modules:SystemAdmin:EnableGraphQlIntrospection", false);

        services
            .AddGraphQLServer("CadsSchema")
            .AddAuthorization()
            .AllowIntrospectionWhen(enableIntrospection)
            .AddMaxExecutionDepthRule(MaxExecutionDepth, skipIntrospectionFields: true)
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
            .AllowIntrospectionWhen(enableIntrospection)
            .AddMaxExecutionDepthRule(MaxExecutionDepth, skipIntrospectionFields: true)
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
            .AllowIntrospectionWhen(enableIntrospection)
            .AddMaxExecutionDepthRule(MaxExecutionDepth, skipIntrospectionFields: true)
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
            .AllowIntrospectionWhen(enableIntrospection)
            .AddMaxExecutionDepthRule(MaxExecutionDepth, skipIntrospectionFields: true)
            .AddQueryType(q => q.Name("Query"))
            .AddTypeExtension<ErdQuery<CtsTransactionsGQDbContext>>()
            .AddTypeExtension<CtsTransactionsGraphQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddPagingArguments();

        return services;
    }

    private static IRequestExecutorBuilder AllowIntrospectionWhen(this IRequestExecutorBuilder builder, bool enabled)
        => enabled ? builder.DisableIntrospection(false) : builder;
}