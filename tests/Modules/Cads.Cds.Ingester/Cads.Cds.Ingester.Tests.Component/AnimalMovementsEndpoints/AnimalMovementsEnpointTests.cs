using Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;
using Cads.Cds.Ingester.Core.Domain.Enums;
using Cads.Cds.Ingester.Core.DTOs.Common;
using Cads.Cds.Ingester.Testing.Support.Constants;
using Cads.Cds.Ingester.Tests.Component.TestFixtures;
using FluentAssertions;
using Newtonsoft.Json;
using System.Text;

namespace Cads.Cds.Ingester.Tests.Component.AnimalMovementsEndpoints;

public class AnimalMovementsEnpointTests(IngesterTestFixture testFixture) : IClassFixture<IngesterTestFixture>
{
    [Fact]
    public async Task AnimalMovements_Enpoint_Passes_And_Returns_Accepted()
    {
        // Arrange
        var nation = Nation.Wales;
        var requestBody = new AnimalMovementsRequest
        {
            AnimalMovement = new AnimalMovement
            {
                CreatedBy = "WA",
                DepartureRegion = "WA",
                DestinationRegion = "WA",
                MessageId = "123456789",
                MovementTime = DateTime.UtcNow,
                MovementId = "123456789",
                MovementGroupId = "123456789",
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
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpoint, nation);

        // Act
        var result = await testFixture.HttpClient.PostAsync(endpoint, new StringContent(payload, Encoding.UTF8, "application/json"), TestContext.Current.CancellationToken);

        // Assert
        result.IsSuccessStatusCode.Should().BeTrue();
        var responseBody = await result.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        var responseData = JsonConvert.DeserializeObject<IngestionDto>(responseBody);
        responseData.Should().NotBeNull();
        responseData.IngestionId.Should().Contain(nation.ToString().ToLower());
        responseData.RecordCount.Should().Be(1);
    }
}