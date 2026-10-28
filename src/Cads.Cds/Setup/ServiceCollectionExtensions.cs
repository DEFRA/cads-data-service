using Asp.Versioning;
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
using Cads.Cds.Setup.Providers;
using Cads.Cds.StorageBridge.Application;
using Cads.Cds.SystemAdmin;
using Cads.Cds.SystemAdmin.Application;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cads.Cds.Setup;

public static class ServiceCollectionExtensions
{
    public static void ConfigureCds(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureAuthentication(configuration);

        services.AddControllers(options =>
        {
            options.ModelBinderProviders.Insert(0, new DateOnlyModelBinderProvider());
        })
        .AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.PropertyNamingPolicy = JsonDefaults.DefaultOptionsWithStringEnumConversion.PropertyNamingPolicy;
            opts.JsonSerializerOptions.WriteIndented = JsonDefaults.DefaultOptionsWithStringEnumConversion.WriteIndented;
            foreach (var converter in JsonDefaults.DefaultOptionsWithStringEnumConversion.Converters)
            {
                opts.JsonSerializerOptions.Converters.Add(converter);
            }
        });

        // Api versioning and OpenAPI configuration
        _ = services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        })
        .AddOpenApi(options =>
        {
            options.Document.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme"
                };

                document.Components.SecuritySchemes["Basic"] = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Description = "Basic authentication header"
                };

                return Task.CompletedTask;
            });
        });

        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.Configure<AwsConfig>(configuration.GetSection(AwsConfig.SectionName));

        services.ConfigureHealthChecks();

        services.ConfigureMediatR();

        services.AddModules(configuration);
    }

    private static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(IApiApplicationMarker).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(IIngesterApplicationMarker).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(IMiBffApplicationMarker).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(IStorageBridgeApplicationMarker).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(ISystemAdminApplicationMarker).Assembly);
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(IApiApplicationMarker).Assembly);
        services.AddValidatorsFromAssembly(typeof(IMiBffApplicationMarker).Assembly);
        services.AddValidatorsFromAssembly(typeof(IStorageBridgeApplicationMarker).Assembly);
        services.AddValidatorsFromAssembly(typeof(ISystemAdminApplicationMarker).Assembly);
        services.AddValidatorsFromAssembly(typeof(ISystemAdminMarker).Assembly);
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
        var schemes = new List<string>();

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
            schemes.Add(AuthenticationConstants.ApiKeySchemeName);
        }

        if (authConfig.Cognito.Enabled)
        {
            authBuilder.AddCognitoScheme(authConfig.Cognito);
            schemes.Add(AuthenticationConstants.CognitoSchemeName);
        }

        if (authConfig.AzureAD.Enabled)
        {
            authBuilder.AddAzureADScheme(authConfig.AzureAD);
            schemes.Add(AuthenticationConstants.AzureADSchemeName);
        }

        if (schemes.Count == 0)
        {
            throw new InvalidOperationException("At least one authentication scheme must be enabled.");
        }

        services.AddAuthorisationPolicies(authConfig, schemes);
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

    private static void AddAuthorisationPolicies(
        this IServiceCollection services,
        AuthenticationConfiguration authConfig,
        List<string> schemes)
    {
        var builder = services.AddAuthorizationBuilder();

        builder.AddPolicy(AuthenticationConstants.DefaultPolicy, p => p
            .AddAuthenticationSchemes([.. schemes])
            .RequireAuthenticatedUser());

        (string Policy, string Scope)[] customScopePolicies =
        [
            (AuthenticationConstants.AadReportsReadPolicy, ScopeNames.ReportsRead),
            (AuthenticationConstants.AadDbAdminExecutePolicy, ScopeNames.DbAdminExecute),
            (AuthenticationConstants.AadSqsAdminExecutePolicy, ScopeNames.SqsAdminManager),
            (AuthenticationConstants.AadS3AdminExecutePolicy, ScopeNames.AdminS3Manager)
        ];

        foreach (var (name, scope) in customScopePolicies)
        {
            builder.AddPolicy(name, policy => policy.RequireEnabledSchemes(authConfig)
                .RequireScopeFromEnabledSchemes(authConfig, scope));
        }

        builder.SetFallbackPolicy(new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes([.. schemes])
            .RequireAuthenticatedUser()
            .Build());
    }

    private static AuthorizationPolicyBuilder RequireEnabledSchemes(
        this AuthorizationPolicyBuilder policy,
        AuthenticationConfiguration config)
    {
        return policy
            .AddSchemeIf(config.ApiKey.Enabled, AuthenticationConstants.ApiKeySchemeName)
            .AddSchemeIf(config.Cognito.Enabled, AuthenticationConstants.CognitoSchemeName)
            .AddSchemeIf(config.AzureAD.Enabled, AuthenticationConstants.AzureADSchemeName)
            .RequireAuthenticatedUser();
    }

    private static AuthorizationPolicyBuilder AddSchemeIf(
        this AuthorizationPolicyBuilder policy, bool enabled, string scheme) =>
        enabled ? policy.AddAuthenticationSchemes(scheme) : policy;

    private static AuthorizationPolicyBuilder RequireScopeFromEnabledSchemes(
        this AuthorizationPolicyBuilder policy,
        AuthenticationConfiguration config,
        string scope)
    {
        return policy.RequireAssertion(ctx =>
        {
            var user = ctx.User;

            // Azure AD scopes
            if (config.AzureAD.Enabled &&
                HasScope(user, config.AzureAD.ScopeClaimType, scope))
                return true;

            // Cognito scopes
            if (config.Cognito.Enabled &&
                HasScope(user, config.Cognito.ScopeClaimType, scope))
                return true;

            return false;
        });
    }

    private static bool HasScope(ClaimsPrincipal user, string claimType, string scope) =>
        user.Claims
            .Where(c => c.Type == claimType)
            .SelectMany(c => c.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Contains(scope, StringComparer.Ordinal);
}