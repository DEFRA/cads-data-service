using Cads.Cds.Api.Core.Domain.Repositories;
using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Cads.Cds.Api.Infrastructure.Persistence.Repositories;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Infrastructure.Persistence.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApiPersistence(this IServiceCollection services)
    {
        services.RegisterDbContexts();
        services.RegisterTableRepositories();
        services.RegisterFunctionRepositories();

        return services;
    }

    private static void RegisterDbContexts(this IServiceCollection services)
    {
        services.AddPostgresDbContext<ApiWriteDbContext>();
        services.AddPostgresDbContext<ApiReadDbContext>(PostgresDataSourceFactory.ReadOnlyConnectionIdentifier);

        services.AddScoped<
            IDbContextFactory<ApiReadDbContext, ApiWriteDbContext>,
            DbContextFactory<ApiReadDbContext, ApiWriteDbContext>>();
    }

    private static void RegisterTableRepositories(this IServiceCollection services)
    {
    }

    private static void RegisterFunctionRepositories(this IServiceCollection services)
    {
        services.AddScoped<ILocationSummaryRepository, LocationSummaryRepository>();
    }
}