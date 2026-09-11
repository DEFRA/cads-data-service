using Cads.Cds.BuildingBlocks.Testing.Support.ProblemDetails;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.SystemAdmin.Controllers.Requests.Generation;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using Cads.Cds.SystemAdmin.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.SystemAdmin.Tests.Component.Endpoints;

public class GenerationEndpointTests(SystemAdminTestFixture testFixture) : IClassFixture<SystemAdminTestFixture>
{
    private readonly SystemAdminTestFixture _testFixture = testFixture;
    private HttpClient _httpClient => _testFixture.HttpClient;

    [Theory]
    [InlineData("my-table1", "scenario-a", 10, 1L)]
    [InlineData("my-table2", "scenario-b", 20, 11L)]
    [InlineData("my-table3", "scenario-b", 30, 21L)]
    [InlineData("my-table4", "scenario-b", 40, 31L)]
    public async Task Create_Should_Return_CreatedResult_With_ResponseDto(string table, string scenario, int? rowCount, long? businessKey)
    {
        // Arrange
        var request = new CreateGenerationRequest
        {
            Table = table,
            Scenario = scenario,
            RowCount = rowCount,
            BusinessKey = businessKey
        };

        // Act
        var response = await GenerationTestClient.CreateAsync(
          _httpClient,
          request: request,
          TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var dto = await GenerationTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);

        Assert.NotNull(dto);
        Assert.Contains($"{request.Table}_{request.Scenario}_", dto.FileName);
        Assert.Equal($"Generated content for table {request.Table} with scenario {request.Scenario} and row count {request.RowCount}.", dto.Content);
        Assert.True(dto.BusinessKeys.Count() == request.RowCount, $"Expected {request.RowCount} business keys but got {dto.BusinessKeys.Count()}");

        for (var rowIndex = 0; rowIndex < request.RowCount; rowIndex++)
        {
            Assert.Equal(request.BusinessKey.GetValueOrDefault() + rowIndex, dto.BusinessKeys.ElementAt(rowIndex));
        }
    }

    [Theory]
    [InlineData("", "", 0, 0L)]
    [InlineData("", "", 0, null)]
    public async Task GivenInvalidRequest_WhenCreateRequested_ShouldReturnBadRequest(string table, string scenario, int? rowCount, long? businessKey)
    {
        // Arrange
        var request = new CreateGenerationRequest
        {
            Table = table,
            Scenario = scenario,
            RowCount = rowCount,
            BusinessKey = businessKey
        };

        // Act
        var response = await GenerationTestClient.CreateAsync(
            _httpClient,
            request: request,
            TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemDetails = await HttpResponseMessageUtilities.VerifyBadRequest<ValidationProblemDetailsDto>(response);

        problemDetails.Should().NotBeNull();

        if (businessKey.HasValue)
        {
            problemDetails.Errors.Should().HaveCount(4);
        }
        else
        {
            problemDetails.Errors.Should().HaveCount(3);
        }

        problemDetails.Errors["Table"].Should().Contain("Table is required.");
        problemDetails.Errors["Scenario"].Should().Contain("Scenario is required.");
        problemDetails.Errors["RowCount"].Should().Contain("Row count must be greater than zero.");

        if (businessKey.HasValue)
        {
            problemDetails.Errors["BusinessKey"].Should().Contain("Business key must be greater than zero.");
        }
    }
}