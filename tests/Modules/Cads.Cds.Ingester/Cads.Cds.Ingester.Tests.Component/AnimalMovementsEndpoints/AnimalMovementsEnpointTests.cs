using System.Text;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;
using Cads.Cds.Ingester.Tests.Component.TestFixtures;
using Cads.Cds.Ingester.Core.Domain.Enums;
using Cads.Cds.Ingester.Core.DTOs.Common;
using FluentAssertions;
using Newtonsoft.Json;

namespace Cads.Cds.Ingester.Tests.Component.AnimalMovementsEndpoints;

public class AnimalMovementsEnpointTests(IngesterTestFixture testFixture) : IClassFixture<IngesterTestFixture>
{
    [Fact]
    public async Task AnimalMovements_Enpoint_Passes_And_Returns_Accepted()
    {
        // Arrange
        var region = Region.Wales;
        var requestBody = new AnimalMovementsRequest
        {
            AnimalMovement = new AnimalMovement
            {
                CreatedBy = "WA",
                DepartureRegion = "WA",
                DestinationRegion = "WA",
                MessageID = "123456789",
                MovementTime = DateTime.UtcNow,
                MovementID = "123456789",
                MovementGroupID = "123456789",
                MovementGroup = "TestGroup",
                CrossBorderMovementId = "123456789",
                MovementType = "TestMovementType",
                ActionType = "TestActionType",
                ActionStatus = "TestActionStatus",
                SupplierType = "TestSupplierType",
                AnimalType = "TestAnimalType",
                MovementDetails = new MovementDetails()
            }
        };

        var payload = JsonConvert.SerializeObject(requestBody);
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpoint, region);

        // Act
        var result = await testFixture.HttpClient.PostAsync(endpoint, new StringContent(payload, Encoding.UTF8, "application/json"), TestContext.Current.CancellationToken);

        // Assert
        result.IsSuccessStatusCode.Should().BeTrue();
        var responseBody = await result.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        var responseData = JsonConvert.DeserializeObject<IngestionDTO>(responseBody);
        responseData.Should().NotBeNull();
        responseData.IngestionId.Should().Contain(region.ToString());
        responseData.RecordCount.Should().Be(1);
    }
}