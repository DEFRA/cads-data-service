using Amazon;
using Amazon.Runtime.Credentials;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Health;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection ConfigureDatabase(IConfiguration configuration)
        {
            var postgresConfig = configuration
                                     .GetSection(PostgresConfiguration.SectionName)
                                     .Get<PostgresConfiguration>()
                                 ?? throw new InvalidOperationException(
                                     $"Configuration section '{PostgresConfiguration.SectionName}' is missing");

            services.AddSingleton(postgresConfig);

            if (postgresConfig.UseIamAuthentication)
            {
                if (string.IsNullOrWhiteSpace(postgresConfig.DbHost) ||
                    string.IsNullOrWhiteSpace(postgresConfig.DbName) ||
                    string.IsNullOrWhiteSpace(postgresConfig.DbUser))
                {
                    throw new InvalidOperationException(
                        "IAM authentication requires DbHost, DbName, and DbUser to be configured");
                }

                services.AddSingleton<IPostgresIamTokenGenerator>(sp =>
                {
                    var credentials = DefaultAWSCredentialsIdentityResolver.GetCredentials();
                    var region = RegionEndpoint.GetBySystemName(
                        configuration.GetValue<string>("AWS:Region") ?? "eu-west-2");
                    return new PostgresIamTokenGenerator(credentials, region);
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(postgresConfig.DefaultConnection))
                {
                    throw new InvalidOperationException(
                        "Connection string 'DefaultConnection' not found or empty");
                }
            }

            services.AddSingleton<IPostgresDataSourceFactory, PostgresDataSourceFactory>();
            services.AddPostgresDbContext<HealthCheckDbContext>();
            services.AddScoped<PostgresHealthCheck>();
            services.AddScoped<IPostgresStatusService, PostgresStatusService>();

            return services;
        }

        private IServiceCollection AddPostgresDbContext<TContext>()
            where TContext : DbContext
        {
            services.AddDbContext<TContext>((sp, options) =>
            {
                var dataSourceFactory = sp.GetRequiredService<IPostgresDataSourceFactory>();
                var dataSource = dataSourceFactory.CreateDataSource("Default");
                options.UseNpgsql(dataSource);
            });

            return services;
        }

        // Overload for modules that need to specify connection identifier
        public IServiceCollection AddPostgresDbContext<TContext>(string connectionIdentifier)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>((sp, options) =>
            {
                var dataSourceFactory = sp.GetRequiredService<IPostgresDataSourceFactory>();
                var dataSource = dataSourceFactory.CreateDataSource(connectionIdentifier);
                options.UseNpgsql(dataSource);
            });

            return services;
        }
    }
}