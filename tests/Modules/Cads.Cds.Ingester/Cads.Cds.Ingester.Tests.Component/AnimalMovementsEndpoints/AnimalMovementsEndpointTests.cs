using Cads.Cds.Ingester.Core.Domain.Enums;
using Cads.Cds.Ingester.Core.DTOs.Common;
using Cads.Cds.Ingester.Testing.Support.Constants;
using Cads.Cds.Ingester.Tests.Component.TestFixtures;
using FluentAssertions;
using Newtonsoft.Json;
using System.Text;

using V1 = Cads.Cds.Ingester.Controllers.AnimalMovements.v1;
using V2 = Cads.Cds.Ingester.Controllers.AnimalMovements.v2;

namespace Cads.Cds.Ingester.Tests.Component.AnimalMovementsEndpoints;

public class AnimalMovementsEndpointTests(IngesterTestFixture testFixture) : IClassFixture<IngesterTestFixture>
{
    [Fact]
    public async Task AnimalMovements_EnpointV1_Passes_And_Returns_Accepted()
    {
        // Arrange
        var nation = Nation.Wales;
        var requestBody = new V1.AnimalMovementMessage
        {
            AnimalMovement = new V1.AnimalMovement
            {
                CreatedBy = V1.Region.Wales,
                DepartureRegion = V1.Region.Wales,
                DestinationRegion = V1.Region.Wales,
                MessageID = "message-123456789",
                MovementTime = "2026-03-23T12:00:00Z",
                MovementID = "movement-123456789",
                MovementGroupID = "movement-group-123456789",
                MovementGroup = "test-movement-group",
                CrossBorderMovementId = "cross-border-movement-123456789",
                MovementType = V1.MovementType.Keeper,
                ActionType = V1.ActionType.New,
                ActionStatus = V1.ActionStatus.Completed,
                SupplierType = V1.SupplierType.Receiver,
                AnimalType = V1.AnimalType.Sheep,
                MovementDetails = new V1.MovementDetails
                {
                    DepartureDate = "2026-03-23",
                    DepartureLocation = "55/015/0071",
                    DepartureCoreID = "departure-core-123",
                    DepartureLocationType = "Agricultural Holding",
                    DeparturePremisesActivity = "AH",
                    OffExemptionCode = "0",
                    ArrivalDate = "2026-03-23",
                    DestinationLocation = "55/014/0006",
                    DestinationCoreID = "destination-core-123",
                    DestinationLocationType = "Agricultural Holding",
                    DestinationPremisesActivity = "AH",
                    OnExemptionCode = "0",
                    LotNumber = "lot-123",
                    NumberOfAnimals = 2,
                    NumberOfDOAs = 0,
                    NumberOfReads = 2,
                    LoadingTimestamp = "2026-03-23T10:00:00Z",
                    ExpectedJourneyDuration = "02:00:00",
                    UnloadingTimestamp = "2026-03-23T12:00:00Z",
                    AnimalTransporter = V1.AnimalTransporter.Haulier,
                    HaulierDetails = new V1.HaulierDetails
                    {
                        HaulierID = "haulier-123",
                        Name = "Test Haulier",
                        AddressLine1 = "1 Test Street",
                        AddressLine2 = "Test District",
                        AddressLine3 = "Test Town",
                        AddressLine4 = "Test County",
                        Postcode = "CF10 1AA",
                        Telephone = "01234567890",
                        Email = "test.haulier@example.com",
                        TotalVehicles = 1,
                        VehicleDetails =
                        [
                            new V1.VehicleDetail
                            {
                                VehicleRegistrationNumber = "AB12 CDE",
                                DriverName = "Test Driver",
                                TrailerIdentificationNumber = "trailer-123",
                                TransportAuthorisationNumber = "authorisation-123"
                            }
                        ]
                    }
                },
                AnimalGroupDetails =
                [
                    new V1.AnimalGroupDetail
                    {
                        AnimalGroupNumber = "flock-123",
                        AnimalGroupType = V1.AnimalGroupType.FlockMark,
                        NumberOfAnimals = 2,
                        NumberOfDOAs = 0
                    }
                ],
                AnimalDetails =
                [
                    new V1.AnimalDetail
                    {
                        AnimalID = "animal-123",
                        AnimalIDType = V1.AnimalIdType.FlockMark,
                        AnimalIDTransponderHex = "ABC123",
                        AnimalIDTransponderISO = "826074123456789",
                        TagIssueNumber = "1",
                        DOA = false,
                        Breed = "Texel",
                        Gender = V1.Gender.Female,
                        DOB = "2024-01-15",
                        DamID = "dam-123",
                        SurrogateDamID = "surrogate-dam-123",
                        SireID = "sire-123",
                        DateOfDeath = null,
                        LicenceType = "Standard",
                        AnimalGroups =
                        [
                            new V1.AnimalGroup
                            {
                                AnimalGroupNumber = "flock-123",
                                AnimalGroupType = V1.AnimalGroupMarkType.FlockMark
                            }
                        ],
                        PassportDetails = new V1.PassportDetails
                        {
                            DocumentType = V1.DocumentType.Passport,
                            DateOfRegistration = "2024-01-20",
                            PassportStatusCode = "Valid",
                            PassportVersion = 1,
                            RefusedPassport = false
                        }
                    }
                ]
            }
        };

        var payload = JsonConvert.SerializeObject(requestBody);
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpointV1, nation);

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

    [Fact]
    public async Task AnimalMovements_EnpointV2_Passes_And_Returns_Accepted()
    {
        // Arrange
        var nation = Nation.Wales;
        var requestBody = new V2.EventMessage
        {
            Event = new V2.LivestockEvent
            {
                RecordID = "record-123456789",
                EventID = "event-123456789",
                EventType = V2.EventType.Movement,
                ActionType = V2.ActionType.New,
                CreatedBy = V2.Region.Wales,
                CreatedDate = "2026-03-23T12:00:00Z",
                AnimalMovement = new V2.AnimalMovement
                {
                    MovementType = V2.MovementType.Keeper,
                    SupplierType = V2.SupplierType.Receiver,
                    NumberOfAnimals = 2,
                    MovementDirection = V2.MovementDirection.On,
                    DepartureRegion = V2.Region.Wales,
                    DestinationRegion = V2.Region.Wales,
                    MovementGroup = "test-movement-group",
                    DepartureDate = "2026-03-23",
                    ArrivalDate = "2026-03-23",
                    DepartureLocation = "55/015/0071",
                    DestinationLocation = "55/014/0006",
                    DepartureCoreID = "departure-core-123",
                    DestinationCoreID = "destination-core-123",
                    DepartureLocationType = "Agricultural Holding",
                    DestinationLocationType = "Agricultural Holding",
                    DeparturePremisesActivity = "AH",
                    DestinationPremisesActivity = "AH",
                    NumberOfDOAs = 0,
                    NumberOfReads = 2,
                    LoadingTimestamp = "2026-03-23T10:00:00Z",
                    ExpectedJourneyDuration = "02:00:00",
                    UnloadingTimestamp = "2026-03-23T12:00:00Z",
                    LotNumber = "lot-123",
                    CrossBorderMovementID = "cross-border-movement-123456789",
                    CrossBorderSource = V2.Region.Wales,
                    WithinBusinessMovement = V2.YesNo.No,
                    WithinBusinessMainCPH = "55/015/0071",
                    InitialWithinBusinessLocation = "55/015/0071",
                    EmergencySlaughter = V2.YesNo.No
                }
            }
        };

        var payload = JsonConvert.SerializeObject(requestBody);
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpointV2, nation);

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


    [Fact]
    public async Task AnimalMovements_EndpointV2_Returns_BadRequest_For_InvalidJson()
    {
        // Arrange
        var nation = Nation.Wales;
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpointV2, nation);
        using var content = new StringContent(
            "{ invalid json }",
            Encoding.UTF8,
            "application/json");

        // Act
        var result = await testFixture.HttpClient.PostAsync(
            endpoint,
            content,
            TestContext.Current.CancellationToken);

        // Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AnimalMovements_EndpointV2_Returns_BadRequest_For_EmptyRequestBody()
    {
        // Arrange
        var nation = Nation.Wales;
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpointV2, nation);
        using var content = new StringContent(
            string.Empty,
            Encoding.UTF8,
            "application/json");

        // Act
        var result = await testFixture.HttpClient.PostAsync(
            endpoint,
            content,
            TestContext.Current.CancellationToken);

        // Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AnimalMovements_EndpointV2_Returns_BadRequest_When_Event_Is_Missing()
    {
        // Arrange
        var nation = Nation.Wales;
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpointV2, nation);
        using var content = new StringContent(
            "{}",
            Encoding.UTF8,
            "application/json");

        // Act
        var result = await testFixture.HttpClient.PostAsync(
            endpoint,
            content,
            TestContext.Current.CancellationToken);

        // Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}