using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Queries;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cts.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsAudit.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsTransactions.Contexts;
using HotChocolate.Execution.Configuration;
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
            .AddTypeExtension<ErdQuery<CadsSystemAdminDbContext>>()
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
            .AddTypeExtension<ErdQuery<CtsSystemAdminDbContext>>()
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
            .AddTypeExtension<ErdQuery<CtsAuditSystemAdminDbContext>>()
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
            .AddTypeExtension<ErdQuery<CtsTransactionsSystemAdminDbContext>>()
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