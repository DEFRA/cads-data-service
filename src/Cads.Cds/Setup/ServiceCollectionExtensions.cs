using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Handlers;
using Cads.Cds.BuildingBlocks.Infrastructure.Configuration.Aws;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Health;
using Cads.Cds.BuildingBlocks.Infrastructure.Http;
using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Cads.Cds.Setup;

public static class ServiceCollectionExtensions
{
    public static void ConfigureCds(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureAuthentication(configuration);

        services.AddControllers(options => 
        {
            options.Filters.Add(new AuthorizeFilter("BasicOrBearer"));
        })
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.PropertyNamingPolicy = JsonDefaults.DefaultOptions.PropertyNamingPolicy;
                opts.JsonSerializerOptions.WriteIndented = JsonDefaults.DefaultOptions.WriteIndented;
                foreach (var converter in JsonDefaults.DefaultOptions.Converters)
                {
                    opts.JsonSerializerOptions.Converters.Add(converter);
                }
            });

        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.Configure<AwsConfig>(configuration.GetSection(AwsConfig.SectionName));

        services.ConfigureHealthChecks();

        services.AddModules(configuration);
    }

    private static void ConfigureHealthChecks(this IServiceCollection services)
    {
        var builder = services.AddHealthChecks();

        builder.Add(new HealthCheckRegistration(
            name: "postgres",
            factory: (x) => x.GetRequiredService<PostgresHealthCheck>(),
            failureStatus: HealthStatus.Unhealthy,
            tags: ["db", "postgres"]));
    }

    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var authConfig = configuration.GetSection(nameof(AuthenticationConfiguration)).Get<AuthenticationConfiguration>()!;

        services.Configure<AclOptions>(
            configuration.GetSection("Acl"));

        services.Configure<AuthenticationConfiguration>(
            configuration.GetSection("AuthenticationConfiguration"));

        services.AddSingleton<IConfigureOptions<AuthenticationOptions>, AuthenticationOptionsConfigurator>();
        services.AddSingleton<IConfigureNamedOptions<Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerOptions>, JwtBearerOptionsConfigurator>();

        var authBuilder = services.AddAuthentication();

        if (authConfig.EnableApiKey)
        {
            authBuilder.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
                BasicAuthenticationHandler.SchemeName, _ => { });
        }

        if (authConfig.ApiGatewayExists)
        {
            authBuilder.AddJwtBearer("Bearer", (options) =>
            {
                options.Authority = authConfig.Authority;
                options.TokenValidationParameters.ValidateAudience = false;
                options.BackchannelHttpHandler = new ProxyHttpMessageHandler();
            });
        }

        services.AddAuthorizationBuilder()
            .AddPolicy("BasicOrBearer", policy =>
            {
                if (authConfig.EnableApiKey)
                {
                    policy.AddAuthenticationSchemes("Basic");
                }
                if (authConfig.ApiGatewayExists)
                {
                    policy.AddAuthenticationSchemes("Bearer");
                }
                policy.RequireAuthenticatedUser();
            });
    }
}