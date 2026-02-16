using Cads.Cds.BuildingBlocks.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Cads.Cds.BuildingBlocks.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void ConfigureBuildingBlocks(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConfig = configuration.GetSection(PostgresConfiguration.SectionName).Get<PostgresConfiguration>();

        var connectionString = postgresConfig.DefaultConnection ?? throw new InvalidOperationException("Connection string"
            + "'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<PostgresHealthCheckService>();
        services.AddScoped<IPostgresStatusService, PostgresStatusService>();
    }
}