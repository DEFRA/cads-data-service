using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Testing.Support.ProblemDetails;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using Cads.Cds.StorageBridge.Tests.Component.TestFixtures;
using FluentAssertions;
using Moq;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Cads.Cds.StorageBridge.Tests.Component.S3BulkLoad;

public class S3BulkLoadEndpointTests(StorageBridgeTestFixture testFixture) : IClassFixture<StorageBridgeTestFixture>
{
    private readonly StorageBridgeTestFixture _testFixture = testFixture;

    private const string Endpoint = TestEndpointConstants.StorageBridgeS3BulkLoadRoot;

    [Fact]
    public async Task GivenInvalidRequest_WhenS3BulkLoadRequested_ShouldReturnBadRequest()
    {
        var response = await _testFixture.HttpClient.PostAsync(Endpoint, InvalidS3BulkLoadRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetailsDto>(TestContext.Current.CancellationToken);
        problemDetails.Should().NotBeNull();
        problemDetails.Errors.Should().NotBeNull().And.HaveCount(3);
        problemDetails.Errors["SourceKey"].Should().Contain("'Source Key' must not be empty.");
        problemDetails.Errors["BulkImportType"].Should().Contain("'Bulk Import Type' must not be equal to 'None'.");
        problemDetails.Errors["ActionType"].Should().Contain("'Action Type' must not be equal to 'None'.");
    }

    [Fact]
    public async Task GivenValidRequest_WhenS3BulkLoadRequested_ShouldSucceed()
    {
        SetupS3MockForLocations(LocationsDataRow1, LocationsDataRow2);

        var response = await _testFixture.HttpClient.PostAsync(Endpoint, ValidS3BulkLoadRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await _testFixture.Factory.TestBulkLoadJobChannel.WaitForJobAsync(TestContext.Current.CancellationToken);
        job.SourceKey.Should().Be("LOCATIONS.part-0001.csv");
        job.BulkImportType.Should().Be(BulkLoadDataTypes.Locations);
    }

    private static StringContent? InvalidS3BulkLoadRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3BulkLoadRequest
        {
            SourceKey = string.Empty,
            BulkImportType = BulkLoadDataTypes.None,
            ActionType = ImportActions.None
        });

    private static StringContent? ValidS3BulkLoadRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3BulkLoadRequest
        {
            SourceKey = "LOCATIONS.part-0001.csv",
            BulkImportType = BulkLoadDataTypes.Locations,
            ActionType = ImportActions.Update
        });

    private void SetupS3MockForLocations(string row1, string row2)
    {
        _testFixture.Factory.AmazonS3Mock.Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
            {
                var csvData = $"{LocationsHeader}\n{row1}\n{row2}";
                return new GetObjectResponse
                {
                    ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes(csvData))
                };
            });
    }

    /*
        record_type|
        record_count|
        loc_id|
        loc_slt_id| XXXX
        loc_lty_id| 
        loc_cty_id|
        loc_receive_labels_flag|
        loc_effective_from|
        loc_effective_to|
        loc_cessation_reason|
        loc_premises_type|
        loc_comments|
        loc_map_reference|
        loc_source_identifier|
        loc_source_reference|
        loc_tel_number|
        loc_mobile_number|
        loc_fax_number|
        loc_email_address|
        loc_current_status|
        loc_current_user|
        loc_current_modified_date|
        loc_current_pid|
        loc_reason_code|
        loc_version|
        loc_receive_ppaf_flag
    */
    private static string LocationsHeader =>
        "record_type|record_count|loc_id|loc_slt_id|loc_lty_id|loc_cty_id|loc_receive_labels_flag|loc_effective_from|loc_effective_to|loc_cessation_reason|loc_premises_type|loc_comments|loc_map_reference|loc_source_identifier|loc_source_reference|loc_tel_number|loc_mobile_number|loc_fax_number|loc_email_address|loc_current_status|loc_current_user|loc_current_modified_date|loc_current_pid|loc_reason_code|loc_version|loc_receive_ppaf_flag";

    private static string LocationsDataRow1 =>
        "D|1|1|XXXX|2|278|N|01-JUL-96|17-JAN-98|BC|AH|Row 1 comments|TL 123456|VT|12345678|0201234567|07712345678|0209876543|email1@internal.test|1|m100000|14-JUN-05|29|AC|1|Y";

    private static string LocationsDataRow2 =>
        "D|2|2|XXXX|2|278|N|01-JUL-21|17-JAN-23|BC|AH|Row 2 comments|TL 234567|VT|23456789|0202345678|07723456789|0209876543|email2@internal.test|1|m100000|10-JUN-25|29|AC|1|Y";
}