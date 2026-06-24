using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Behaviours;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Persistance.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSystemAdminPersistence(this IServiceCollection services)
    {
        services.RegisterDbContexts();

        services.RegisterBehaviours();

        return services;
    }

    private static void RegisterDbContexts(this IServiceCollection services)
    {
        services.AddPostgresDbContext<SystemAdminWriteDbContext>();
        services.AddPostgresDbContext<SystemAdminReadDbContext>(PostgresDataSourceFactory.ReadOnlyConnectionIdentifier);

        services.AddScoped<
            IDbContextFactory<SystemAdminReadDbContext, SystemAdminWriteDbContext>,
            DbContextFactory<SystemAdminReadDbContext, SystemAdminWriteDbContext>>();
    }

    private static void RegisterBehaviours(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(SystemAdminTransactionBehaviour<,>));
    }
}