using Cads.Cds.Api.Application;
using Cads.Cds.BuildingBlocks.Application.Behaviors;
using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Handlers;
using Cads.Cds.BuildingBlocks.Infrastructure.Configuration.Aws;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Health;
using Cads.Cds.BuildingBlocks.Infrastructure.Http;
using Cads.Cds.BuildingBlocks.Infrastructure.Identity;
using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using Cads.Cds.Ingester.Application;
using Cads.Cds.MiBff.Application;
using Cads.Cds.StorageBridge.Application;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.IdentityModel.Tokens.Jwt;

namespace Cads.Cds.Setup;

public static class ServiceCollectionExtensions
{
    public static void ConfigureCds(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureAuthentication(configuration);

        services.AddControllers()
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.PropertyNamingPolicy = JsonDefaults.DefaultOptionsWithStringEnumConversion.PropertyNamingPolicy;
                opts.JsonSerializerOptions.WriteIndented = JsonDefaults.DefaultOptionsWithStringEnumConversion.WriteIndented;
                foreach (var converter in JsonDefaults.DefaultOptionsWithStringEnumConversion.Converters)
                {
                    opts.JsonSerializerOptions.Converters.Add(converter);
                }
            });

        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.Configure<AwsConfig>(configuration.GetSection(AwsConfig.SectionName));

        services.ConfigureHealthChecks();

        services.ConfigureMediatR();

        services.AddModules(configuration);

        // Make endpoints available for discovery by tools like Swagger
        services.AddEndpointsApiExplorer();

        // Configure Swagger/OpenAPI
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "CADS API", Version = "v1" });

            // Bearer Authentication
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            // Basic Authentication
            options.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
            {
                Description = "Basic Authentication header. Example: \"Basic {base64(username:password)}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "basic"
            });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("bearer", document)] = [],
                [new OpenApiSecuritySchemeReference("basic", document)] = []
            });
        });
    }

    private static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(IApiApplicationMarker).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(IIngesterApplicationMarker).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(IMiBffApplicationMarker).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(IStorageBridgeApplicationMarker).Assembly);
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(IMiBffApplicationMarker).Assembly);
        services.AddValidatorsFromAssembly(typeof(IStorageBridgeApplicationMarker).Assembly);
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

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        var authBuilder = services.AddAuthentication();

        if (authConfig.ApiKey.Enabled)
        {
            authBuilder.AddApiKeyScheme();
        }

        if (authConfig.Cognito.Enabled)
        {
            authBuilder.AddCognitoScheme(authConfig.Cognito);
        }

        if (authConfig.AzureAD.Enabled)
        {
            authBuilder.AddAzureADScheme(authConfig.AzureAD);
        }

        services.AddAuthorisationPolicies(authConfig);
        services.AddUserContext();
    }

    private static void AddUserContext(this IServiceCollection services)
    {
        services.AddScoped<IUserContext, UserContext>();
    }

    private static void AddApiKeyScheme(this AuthenticationBuilder authenticationBuilder)
    {
        authenticationBuilder.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
            AuthenticationConstants.ApiKeySchemeName, _ => { });
    }

    private static void AddCognitoScheme(this AuthenticationBuilder authenticationBuilder, AuthenticationProviderConfiguration authenticationProviderConfiguration)
    {
        authenticationBuilder.AddJwtBearer(AuthenticationConstants.CognitoSchemeName, (options) =>
        {
            options.Authority = authenticationProviderConfiguration.Authority;
            options.TokenValidationParameters.ValidateAudience = false;
            options.BackchannelHttpHandler = new ProxyHttpMessageHandler();
        });
    }

    private static void AddAzureADScheme(this AuthenticationBuilder authenticationBuilder, AuthenticationProviderConfiguration authenticationProviderConfiguration)
    {
        authenticationBuilder.AddJwtBearer(AuthenticationConstants.AzureADSchemeName, options =>
        {
            options.MapInboundClaims = false;

            if (!string.IsNullOrWhiteSpace(authenticationProviderConfiguration.MetadataAddress))
            {
                options.MetadataAddress = authenticationProviderConfiguration.MetadataAddress;
            }
            else if (!string.IsNullOrWhiteSpace(authenticationProviderConfiguration.Authority))
            {
                options.Authority = authenticationProviderConfiguration.Authority;
            }

            options.RequireHttpsMetadata = authenticationProviderConfiguration.RequireHttpsMetadata;
            options.Audience = authenticationProviderConfiguration.Audience;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = authenticationProviderConfiguration.ValidateIssuer,
                ValidateAudience = true,
                ValidAudience = authenticationProviderConfiguration.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                NameClaimType = "name",
                RoleClaimType = authenticationProviderConfiguration.RoleClaimType,
                ValidTypes = ["JWT", "at+jwt"]
            };

            options.BackchannelHttpHandler = new ProxyHttpMessageHandler();
        });
    }

    private static void AddAuthorisationPolicies(this IServiceCollection services, AuthenticationConfiguration authenticationConfiguration)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(AuthenticationConstants.ApiKeyOrCognitoPolicy, policy =>
            {
                if (authenticationConfiguration.ApiKey.Enabled)
                {
                    policy.AddAuthenticationSchemes(AuthenticationConstants.ApiKeySchemeName);
                }
                if (authenticationConfiguration.Cognito.Enabled)
                {
                    policy.AddAuthenticationSchemes(AuthenticationConstants.CognitoSchemeName);
                }
                policy.RequireAuthenticatedUser();
            })
            .AddPolicy(AuthenticationConstants.AadReportsReadPolicy, policy =>
            {
                if (authenticationConfiguration.ApiKey.Enabled)
                {
                    policy.AddAuthenticationSchemes(AuthenticationConstants.ApiKeySchemeName);
                }
                if (authenticationConfiguration.AzureAD.Enabled)
                {
                    policy.AddAuthenticationSchemes(AuthenticationConstants.AzureADSchemeName);
                }
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(authenticationConfiguration.AzureAD.RoleClaimType, ScopeNames.ReportsRead);
            });
    }
}