using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMiBffInfrastructureLayer(this IServiceCollection services)
    {
        services.AddPostgresDbContext<MiBffWriteDbContext>();
        services.AddPostgresDbContext<MiBffReadDbContext>(PostgresDataSourceFactory.ReadOnlyConnectionIdentifier);

        // Tables
        services.AddScoped<IMiPermissionRepository, MiPermissionRepository>();
        services.AddScoped<IMiReportGroupMapRepository, MiReportGroupMapRepository>();
        services.AddScoped<IMiReportGroupRepository, MiReportGroupRepository>();
        services.AddScoped<IMiReportRepository, MiReportRepository>();
        services.AddScoped<IMiUserReportPermissionRepository, MiUserReportPermissionRepository>();
        services.AddScoped<IMiRoleRepository, MiRoleRepository>();
        services.AddScoped<IMiUserReportPermissionRepository, MiUserReportPermissionRepository>();
        services.AddScoped<IMiUserRepository, MiUserRepository>();
        services.AddScoped<IMiUserRoleRepository, MiUserRoleRepository>();

        // Views
        services.AddScoped<IMiEffectiveReportPermissionRepository, MiEffectiveReportPermissionRepository>();
        services.AddScoped<IMiEffectiveReportAllPermissionRepository, MiEffectiveReportAllPermissionRepository>();

        services.AddScoped<
            IDbContextFactory<MiBffReadDbContext, MiBffWriteDbContext>,
            DbContextFactory<MiBffReadDbContext, MiBffWriteDbContext>>();

        return services;
    }
}