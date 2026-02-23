using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.Credentials;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
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
                if (string.IsNullOrWhiteSpace(postgresConfig.DefaultHost) ||
                    string.IsNullOrWhiteSpace(postgresConfig.ReadOnlyHost) ||
                    string.IsNullOrWhiteSpace(postgresConfig.Name) ||
                    string.IsNullOrWhiteSpace(postgresConfig.User))
                {
                    throw new InvalidOperationException(
                        "IAM authentication requires DefaultHost, ReadOnlyHost, Name, and User to be configured");
                }

                services.AddSingleton<IPostgresIamTokenGeneratorService>(sp =>
                {
                    var credentials = sp.GetService<AWSCredentials>()
                                      ?? DefaultAWSCredentialsIdentityResolver.GetCredentials();
                    var region = RegionEndpoint.GetBySystemName(
                        configuration.GetValue<string>("AWS:Region") ?? "eu-west-2");
                    return new PostgresIamTokenGeneratorService(credentials, region);
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(postgresConfig.DefaultConnection) || string.IsNullOrWhiteSpace(postgresConfig.ReadOnlyConnection))
                {
                    var missingString = postgresConfig.DefaultConnection == string.Empty ? "DefaultConnection" : "ReadOnlyConnection";
                    throw new InvalidOperationException(
                        $"Connection string '{missingString}' not found or empty");
                }
            }

            services.AddSingleton<IPostgresDataSourceFactory, PostgresDataSourceFactory>();
            services.AddPostgresDbContext<HealthCheckDbContext>();
            services.AddPostgresDbContext<HealthCheckDbContext>(PostgresDataSourceFactory.ReadOnlyConnectionIdentifier);
            services.AddScoped<PostgresHealthCheck>();
            services.AddScoped<IPostgresStatusService, PostgresStatusService>();

            return services;
        }

        public IServiceCollection AddPostgresDbContext<TContext>() where TContext : DbContext
        {
            services.AddDbContext<TContext>((sp, options) =>
            {
                var dataSourceFactory = sp.GetRequiredService<IPostgresDataSourceFactory>();
                var dataSource = dataSourceFactory.CreateDataSource(PostgresDataSourceFactory.DefaultConnectionIdentifier);
                options.UseNpgsql(dataSource);
            });
            return services;
        }

        // Overload for modules that need to specify connection identifier
        public IServiceCollection AddPostgresDbContext<TContext>(string connectionIdentifier) where TContext : DbContext
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