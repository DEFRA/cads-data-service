using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMiBffPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<MiBffReadDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("ReadDb")));

        services.AddDbContext<MiBffWriteDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("WriteDb")));

        services.AddScoped<IMiUserRepository, MiUserRepository>();
        services.AddScoped<MiUserWriteRepository>();

        services.AddScoped<
            IDbContextFactory<MiBffReadDbContext, MiBffWriteDbContext>,
            DbContextFactory<MiBffReadDbContext, MiBffWriteDbContext>>();

        return services;
    }
}