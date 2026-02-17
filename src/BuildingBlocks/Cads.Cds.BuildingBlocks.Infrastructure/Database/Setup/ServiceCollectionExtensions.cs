using Cads.Cds.BuildingBlocks.Infrastructure.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Health;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConfig = configuration.GetSection(PostgresConfiguration.SectionName).Get<PostgresConfiguration>();

        var connectionString = postgresConfig?.DefaultConnection ?? throw new InvalidOperationException("Connection string"
            + "'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<PostgresHealthCheck>();
        services.AddScoped<IPostgresStatusService, PostgresStatusService>();
    }
}