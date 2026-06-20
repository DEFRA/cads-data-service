using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSystemAdminInfrastructureLayer(this IServiceCollection services, IConfiguration config)
    {
        services.AddPostgresDbContext<SystemAdminWriteDbContext>();
        services.AddPostgresDbContext<SystemAdminReadDbContext>(PostgresDataSourceFactory.ReadOnlyConnectionIdentifier);

        return services;
    }
}