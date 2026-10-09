using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Cads.Cds.SystemAdmin.Endpoints.Generation.Requests;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using FluentAssertions;

namespace Cads.Cds.SystemAdmin.Tests.Integration.Endpoints;

[Collection("SystemAdminIntegration"), Trait("Dependence", "testcontainers")]
public class GenerationEndpointTests(ApiContainerFixture apiContainerFixture)
{
    private HttpClient _httpClient => apiContainerFixture.CreateBasicClient();

    [Fact]
    public async Task GivenValidBatchGenerationRequest_WhenCreateRequested_ShouldSucceed()
    {
        var request = new CreateGenerationRequest
        {
            Scenario = "ct_location_bulk_scenario",
            RowCount = 10
        };

        var response = await GenerationTestClient.CreateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await GenerationTestClient.ReadDtoAsync(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto.FileName.Should().NotBeNullOrEmpty();
        dto.FileName.Should().Contain("CT_LOCATIONS");
        dto.FileName.Should().Contain("BULK");
        dto.BusinessKeys.Should().NotBeNull();
        dto.BusinessKeys.Should().HaveCount(10);

        var content = dto.Content.Split("\n");

        content.Should().NotBeNull();
        content.Length.Should().Be(13);

        var header = content[0];
        header.Should().NotBeNullOrEmpty();
        header.Should().Contain("H|");

        var columnHeader = content[1];
        columnHeader.Should().NotBeNullOrEmpty();
        columnHeader.Should().Contain("C|");

        var data = content.Where(i => i.StartsWith("D|"));
        data.Should().NotBeNullOrEmpty();
        data.Count().Should().Be(10);

        var columnHeaders = columnHeader.Split("|");
        var dataHeaders = content[2].Split("|");

        columnHeaders.Length.Should().Be(dataHeaders.Length);

        var footer = content[12];
        footer.Should().NotBeNullOrEmpty();
        footer.Should().Contain("T|");
    }

    [Fact]
    public async Task GivenInvalidBatchGenerationRequest_WhenCreateRequested_ShouldFail()
    {
        var request = new CreateGenerationRequest
        {
            Scenario = "ct_location_bulk_scenario",
        };

        var response = await GenerationTestClient.CreateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeFalse();
    }

    [Fact]
    public async Task GivenValidUpdateGenerationRequest_WhenCreateRequested_ShouldSucceed()
    {
        var request = new CreateGenerationRequest
        {
            Scenario = "ct_location_update_scenario",
            RowCount = 10
        };

        var response = await GenerationTestClient.CreateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await GenerationTestClient.ReadDtoAsync(
            response,
            TestContext.Current.CancellationToken);


        dto.Should().NotBeNull();
        dto.FileName.Should().NotBeNullOrEmpty();
        dto.FileName.Should().Contain("CT_LOCATIONS");
        dto.FileName.Should().Contain("DEV");

        dto.BusinessKeys.Should().NotBeNull();
        dto.BusinessKeys.Should().HaveCount(10);

        var content = dto.Content.Split("\n");

        content.Should().NotBeNull();
        content.Length.Should().Be(13);

        var header = content[0];
        header.Should().NotBeNullOrEmpty();
        header.Should().Contain("H|");

        var columnHeader = content[1];
        columnHeader.Should().NotBeNullOrEmpty();
        columnHeader.Should().Contain("C|");

        var data = content.Where(i => i.StartsWith("D|"));
        data.Should().NotBeNullOrEmpty();
        data.Count().Should().Be(10);

        var columnHeaders = columnHeader.Split("|");
        var dataHeaders = content[2].Split("|");

        columnHeaders.Length.Should().Be(dataHeaders.Length);

        var footer = content[12];
        footer.Should().NotBeNullOrEmpty();
        footer.Should().Contain("T|");
    }

    [Fact]
    public async Task GivenInvalidUpdateGenerationRequest_WhenCreateRequested_ShouldFail()
    {
        var request = new CreateGenerationRequest
        {
            Scenario = "ct_location_update_scenario",
        };

        var response = await GenerationTestClient.CreateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeFalse();
    }

    [Fact]
    public async Task GivenInvalidGenerationRequest_WhenCreateRequested_ShouldFail()
    {
        var request = new CreateGenerationRequest
        {
            Scenario = "invalid_scenario",
        };

        var response = await GenerationTestClient.CreateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeFalse();
    }

    [Fact]
    public async Task GivenValidGetScenariosRequest_WhenCreateRequested_ShouldSucceed()
    {
        var response = await GenerationTestClient.GetScenarios(
            _httpClient,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await GenerationTestClient.ReadDtoAsync<GetScenariosResponseDto>(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto.Scenarios.Should().NotBeEmpty();
        dto.Scenarios.Should().HaveCount(2);
        dto.Scenarios.First().Name.Should().Be("ct_location_bulk_scenario");
        dto.Scenarios.First().TableName.Should().Be("ct_locations");
        dto.Scenarios.Last().Name.Should().Be("ct_location_update_scenario");
        dto.Scenarios.Last().TableName.Should().Be("ct_locations");
    }
}