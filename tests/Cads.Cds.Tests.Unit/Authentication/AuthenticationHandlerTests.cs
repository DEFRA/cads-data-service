using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.Tests.Unit.TestFixtures;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;

namespace Cads.Cds.Tests.Unit.Authentication;

public class AuthenticationHandlerTests
{
    private static CdsWebApplicationFactory GetFactory(bool useFakeAuth = false)
    {
        var factory = new CdsWebApplicationFactory(useFakeAuth: useFakeAuth);
        return factory;
    }

    [Fact]
    public async Task GivenTheApiKeyOrCognitoPolicyExists_WhenApiKeyEndpointRequested_AndNoTokenProvided_ReturnsUnauthorized()
    {
        var factory = GetFactory();
        var client = factory.CreateClient();

        var response = await client.GetAsync("test-auth/basic/apikey", TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GivenTheApiKeyOrCognitoPolicyExists_WhenApiKeyEndpointRequested_AndInvalidBasicTokenProvided_ReturnsUnauthorized()
    {
        var factory = GetFactory();
        var client = factory.CreateClient();
        client.AddBasicApiKey(TestAuthConstants.BasicApiKey, Guid.NewGuid().ToString());

        var response = await client.GetAsync("test-auth/basic/apikey", TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GivenTheApiKeyOrCognitoPolicyExists_WhenApiKeyEndpointRequested_AndValidBasicTokenProvided_ReturnsOk()
    {
        var factory = GetFactory();
        var client = factory.CreateClient();
        client.AddBasicApiKey(TestAuthConstants.BasicApiKey, TestAuthConstants.BasicSecret);

        var response = await client.GetAsync("test-auth/basic/apikey", TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GivenTheApiKeyOrCognitoPolicyExists_WhenCognitoEndpointRequested_AndInvalidCognitoTokenProvided_ReturnsUnauthorized()
    {
        var factory = GetFactory();
        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-token");

        var response = await client.GetAsync("test-auth/bearer/cognito", TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GivenTheApiKeyOrCognitoPolicyExists_WhenCognitoEndpointRequested_AndValidCognitoTokenProvided_ReturnsOk()
    {
        var factory = GetFactory(true);
        var client = factory.CreateClient();
        client.AddJwt();

        var response = await client.GetAsync("test-auth/bearer/cognito", TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GivenTheAadReportsReadPolicy_WhenReportsEndpointRequested_AndInvalidAadTokenProvided_ReturnsUnauthorized()
    {
        var factory = GetFactory();
        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-token");

        var response = await client.GetAsync("test-auth/azuread/reports", TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GivenTheAadReportsReadPolicy_WhenReportsEndpointRequested_AndValidAadTokenProvided_ReturnsOk()
    {
        var factory = GetFactory(true);
        var client = factory.CreateClient();
        client.AddJwt();

        var response = await client.GetAsync("test-auth/azuread/reports", TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}