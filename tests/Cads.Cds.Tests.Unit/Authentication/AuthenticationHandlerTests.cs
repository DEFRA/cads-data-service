using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.MiBff.Core.Services.Ukv;
using Cads.Cds.Tests.Unit.TestFixtures;
using FluentAssertions;
using Moq;
using System.Net;

namespace Cads.Cds.Tests.Unit.Authentication;

public class AuthenticationHandlerTests
{
    private readonly Mock<IHoldingService> _holdingsServiceMock = new();

    private static string GetBffUkvHoldingsByCphEndpoint
        => string.Format(TestEndpointConstants.BffUkvHoldingsByCphEndpoint, Guid.NewGuid().ToString());

    private CdsWebApplicationFactory GetFactory(IDictionary<string, string?>? configOverrides = null)
    {
        var factory = new CdsWebApplicationFactory(configOverrides: configOverrides, useFakeAuth: true);
        factory.OverrideServiceAsTransient(_holdingsServiceMock.Object);
        return factory;
    }

    public AuthenticationHandlerTests()
    {
        _holdingsServiceMock.Setup(x => x.GetByCphAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
    }

    [Fact]
    public async Task WhenApiGatewayExists_JwtSchemeSucceeds()
    {
        var factory = GetFactory();

        var client = factory.CreateClient();
        client.AddJwt();

        var response = await client.GetAsync(GetBffUkvHoldingsByCphEndpoint, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task WhenApiKeyEnabled_BasicSchemeSucceeds()
    {
        var factory = GetFactory();

        var client = factory.CreateClient();
        client.AddBasicApiKey(TestAuthConstants.BasicApiKey, TestAuthConstants.BasicSecret);

        var response = await client.GetAsync(GetBffUkvHoldingsByCphEndpoint, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}