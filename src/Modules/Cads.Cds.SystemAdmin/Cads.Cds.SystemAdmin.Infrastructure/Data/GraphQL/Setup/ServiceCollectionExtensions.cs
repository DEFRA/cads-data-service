using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Queries;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cts.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsAudit.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsTransactions.Contexts;
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
            .AddTypeExtension<ErdQuery<CadsSystemAdminDbContext>>()
            .AddTypeExtension<CadsGraphQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddPagingArguments();

        services
            .AddGraphQLServer("CtsSchema")
            .AddAuthorization()
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
            .AddQueryType(q => q.Name("Query"))
            .AddTypeExtension<ErdQuery<CtsTransactionsSystemAdminDbContext>>()
            .AddTypeExtension<CtsTransactionsGraphQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddPagingArguments();

        return services;
    }
}