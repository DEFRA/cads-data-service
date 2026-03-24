using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.Setup;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Cads.Cds.Tests.Unit.Authentication;

public class ConfigureAuthenticationTests
{
    [Fact]
    public void ConfigureAuthentication_AddsApiKeyScheme_WhenEnabled()
    {
        // Arrange
        var configDict = new Dictionary<string, string?>
        {
            ["AuthenticationConfiguration:ApiKey:Enabled"] = "true",
            ["AuthenticationConfiguration:Cognito:Enabled"] = "false",
            ["AuthenticationConfiguration:AzureAD:Enabled"] = "false"
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(configDict)
            .Build();

        var services = new ServiceCollection();

        // Act
        services.ConfigureAuthentication(config);

        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptionsMonitor<AuthenticationOptions>>();

        // Assert
        options.Get(AuthenticationConstants.ApiKeySchemeName).Should().NotBeNull();
    }

    [Fact]
    public void ConfigureAuthentication_AddsCognitoScheme_WhenEnabled()
    {
        var configDict = new Dictionary<string, string?>
        {
            ["AuthenticationConfiguration:ApiKey:Enabled"] = "false",
            ["AuthenticationConfiguration:Cognito:Enabled"] = "true",
            ["AuthenticationConfiguration:Cognito:Authority"] = "https://cognito-test",
            ["AuthenticationConfiguration:AzureAD:Enabled"] = "false"
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(configDict)
            .Build();

        var services = new ServiceCollection();
        services.ConfigureAuthentication(config);

        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>();

        var cognito = options.Get(AuthenticationConstants.CognitoSchemeName);

        cognito.Authority.Should().Be("https://cognito-test");
        cognito.TokenValidationParameters.ValidateAudience.Should().BeFalse();
    }

    [Fact]
    public void ConfigureAuthentication_UsesMetadataAddress_WhenProvided()
    {
        var configDict = new Dictionary<string, string?>
        {
            ["AuthenticationConfiguration:AzureAD:Enabled"] = "true",
            ["AuthenticationConfiguration:AzureAD:MetadataAddress"] = "http://fake-metadata",
            ["AuthenticationConfiguration:AzureAD:Authority"] = "https://fake-issuer",
            ["AuthenticationConfiguration:AzureAD:RequireHttpsMetadata"] = "false"
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(configDict)
            .Build();

        var services = new ServiceCollection();
        services.ConfigureAuthentication(config);

        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>();

        var azure = options.Get(AuthenticationConstants.AzureADSchemeName);

        azure.MetadataAddress.Should().Be("http://fake-metadata");
        azure.Authority.Should().BeNull();
    }

    [Fact]
    public async Task ConfigureAuthentication_AddsCorrectPolicies()
    {
        var configDict = new Dictionary<string, string?>
        {
            ["AuthenticationConfiguration:ApiKey:Enabled"] = "true",
            ["AuthenticationConfiguration:Cognito:Enabled"] = "true",
            ["AuthenticationConfiguration:AzureAD:Enabled"] = "false"
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(configDict)
            .Build();

        var services = new ServiceCollection();
        services.ConfigureAuthentication(config);

        var provider = services.BuildServiceProvider();
        var policyProvider = provider.GetRequiredService<IAuthorizationPolicyProvider>();

        var policy = await policyProvider.GetPolicyAsync(AuthenticationConstants.ApiKeyOrCognitoPolicy);

        policy!.AuthenticationSchemes.Should().Contain(
        [
            AuthenticationConstants.ApiKeySchemeName,
            AuthenticationConstants.CognitoSchemeName
        ]);

        policy.Requirements.Should().ContainSingle(r => r is DenyAnonymousAuthorizationRequirement);
    }
}