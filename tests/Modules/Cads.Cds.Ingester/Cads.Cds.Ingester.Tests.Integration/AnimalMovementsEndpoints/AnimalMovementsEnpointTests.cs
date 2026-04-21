using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.Ingester.Core.Domain.Enums;
using Cads.Cds.Ingester.Core.DTOs.Common;
using FluentAssertions;
using Newtonsoft.Json;
using System.Text;

namespace Cads.Cds.Ingester.Tests.Integration.AnimalMovementsEndpoints;

[Collection("IngesterIntegration"), Trait("Dependence", "testcontainers")]
public class AnimalMovementsEnpointTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidAnimalMovements_WhenPostingToEndpoint_ShouldSucceed()
    {
        var nation = Nation.Wales;
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpoint, nation);
        var client = apiContainerFixture.CreateBasicClient();

        var result = await client.PostAsync(endpoint, new StringContent(payload, Encoding.UTF8, "application/json"),
            TestContext.Current.CancellationToken);
        result.IsSuccessStatusCode.Should().BeTrue();
        var responseBody = await result.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        var responseData = JsonConvert.DeserializeObject<IngestionDto>(responseBody);
        responseData.Should().NotBeNull();
        responseData.IngestionId.Should().Contain(nation.ToString().ToLower());
        responseData.RecordCount.Should().Be(1);
    }

    private readonly string payload = @"{
 ""AnimalMovement"": {
   ""CreatedBy"": ""SC"",
   ""DepartureRegion"": ""SC"",
   ""DestinationRegion"": ""SC"",
   ""MessageID"": ""14567413"",
   ""MovementTime"": ""2026-03-23T00:00:00Z"",
   ""MovementID"": ""8308310"",
   ""MovementGroupID"": ""2474189:31710"",
   ""MovementGroup"": ""2474189"",
   ""CrossBorderMovementId"": """",
   ""MovementType"": ""01"",
   ""ActionType"": ""New"",
   ""ActionStatus"": ""Completed"",
   ""SupplierType"": ""Receiver"",
   ""AnimalType"": ""Sheep"",
   ""MovementDetails"": {
     ""DepartureDate"": ""2026-02-26"",
     ""DepartureLocation"": ""55/015/0071"",
     ""DepartureCoreID"": ""98005647"",
     ""DepartureLocationType"": ""Agricultural Holding"",
     ""DeparturePremisesActivity"": ""AH"",
     ""OffExemptionCode"": """",
     ""ArrivalDate"": ""2026-02-26"",
     ""DestinationLocation"": ""55/014/0006"",
     ""DestinationCoreID"": ""98045305"",
     ""DestinationLocationType"": ""Agricultural Holding"",
     ""DestinationPremisesActivity"": ""AH"",
     ""OnExemptionCode"": """",
     ""LotNumber"": """",
     ""NumberOfAnimals"": 3,
     ""NumberOfDOAs"": 0,
     ""NumberOfReads"": 3,
     ""LoadingTimestamp"": """",
     ""ExpectedJourneyDuration"": """",
     ""UnloadingTimestamp"": """",
     ""AnimalTransporter"": ""01"",
     ""HaulierDetails"": {
       ""Name"": """",
       ""Telephone"": """",
       ""Email"": """",
       ""VehicleDetails"": [
         {
           ""VehicleRegistrationNumber"": """",
           ""DriverName"": """",
           ""TransportAuthorisationNumber"": """"
         }
       ]
     }
   },
   ""AnimalGroupDetails"": [
   ],
   ""AnimalDetails"": [
     {
       ""AnimalID"": ""826074973200188"",
       ""AnimalIDType"": ""IM"",
       ""AnimalIDTransponderHex"": """",
       ""AnimalIDTransponderISO"": """",
       ""TagIssueNumber"": """",
       ""DOA"": false
     },
     {
       ""AnimalID"": ""826074191409879"",
       ""AnimalIDType"": ""IM"",
       ""AnimalIDTransponderHex"": """",
       ""AnimalIDTransponderISO"": """",
       ""TagIssueNumber"": """",
       ""DOA"": false
     },
     {
       ""AnimalID"": ""826074973200206"",
       ""AnimalIDType"": ""IM"",
       ""AnimalIDTransponderHex"": """",
       ""AnimalIDTransponderISO"": """",
       ""TagIssueNumber"": """",
       ""DOA"": false
     }
   ]
 }
}";
}