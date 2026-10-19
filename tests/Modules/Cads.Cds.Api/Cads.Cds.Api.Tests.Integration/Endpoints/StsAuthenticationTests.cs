using Cads.Cds.Api.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.Api.Tests.Integration.Endpoints;

[Collection("ApiIntegration"), Trait("Dependence", "testcontainers")]
public class StsAuthenticationTests(ApiContainerFixture apiContainerFixture)
{
    private const string RequiresStsHandler = "Requires AwsStsAuthenticationHandler and the imports endpoint from LANI-1034";

    private const string Endpoint = TestEndpointConstants.ApiImportsScopedRoot + "test-file.csv";

    private static readonly StsTokenRequest s_validToken = new()
    {
        Sub = TestAuthConstants.StsRoleArn,
        Aud = TestAuthConstants.StsAudience,
        OrgId = TestAuthConstants.StsTrustedOrgId,
        AwsAccount = TestAuthConstants.StsAwsAccount,
        ServiceName = TestAuthConstants.StsServiceName,
        Environment = TestAuthConstants.StsEnvironment
    };

    [Fact(Skip = RequiresStsHandler)]
    public async Task GivenValidStsToken_WhenScopedImportRequested_ShouldReturnOk()
    {
        var response = await ExecuteTest(s_validToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact(Skip = RequiresStsHandler)]
    public async Task GivenWrongAudience_WhenScopedImportRequested_ShouldReturnUnauthorized()
    {
        var response = await ExecuteTest(s_validToken with { Aud = "https://some-other-service" });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact(Skip = RequiresStsHandler)]
    public async Task GivenWrongOrgId_WhenScopedImportRequested_ShouldReturnUnauthorized()
    {
        var response = await ExecuteTest(s_validToken with { OrgId = "o-wrong" });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact(Skip = RequiresStsHandler)]
    public async Task GivenWrongEnvironment_WhenScopedImportRequested_ShouldReturnUnauthorized()
    {
        var response = await ExecuteTest(s_validToken with { Environment = "Prod" });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact(Skip = RequiresStsHandler)]
    public async Task GivenCallerNotOnAcl_WhenScopedImportRequested_ShouldReturnUnauthorized()
    {
        var response = await ExecuteTest(s_validToken with { ServiceName = "not-on-acl" });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact(Skip = RequiresStsHandler)]
    public async Task GivenExpiredToken_WhenScopedImportRequested_ShouldReturnUnauthorized()
    {
        var response = await ExecuteTest(s_validToken with { Expired = true });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    private async Task<HttpResponseMessage> ExecuteTest(StsTokenRequest request)
    {
        var client = await apiContainerFixture.CreateStsClientAsync(request);

        return await client.GetAsync(Endpoint, TestContext.Current.CancellationToken);
    }
}
