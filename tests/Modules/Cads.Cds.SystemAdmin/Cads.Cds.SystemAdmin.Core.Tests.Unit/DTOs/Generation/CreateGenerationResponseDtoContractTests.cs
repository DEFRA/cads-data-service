using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using FluentAssertions;
using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Core.Tests.Unit.DTOs.Generation;

// Contract (snapshot) of the JSON property names consumed by the e2e journey tests.
// If this fails, the e2e clients break: change both sides together (LANI-1293).
public class CreateGenerationResponseDtoContractTests
{
    private static readonly string[] s_expectedPropertyNames = ["fileName", "groupKey", "content", "businessKeys"];

    private static readonly CreateGenerationResponseDto s_dto = new()
    {
        FileName = "CTSM_CADS_ETE_BULK_01711_CT_LOCATIONS_2026-11-12-133516.CSV",
        GroupKey = "group",
        Content = "content",
        BusinessKeys = [1m]
    };

    [Theory]
    [InlineData(JsonSerializerDefaults.General)]
    [InlineData(JsonSerializerDefaults.Web)]
    public void GivenResponse_WhenSerialised_ShouldUseExactCamelCasePropertyNames(JsonSerializerDefaults defaults)
    {
        var json = JsonSerializer.Serialize(s_dto, new JsonSerializerOptions(defaults));

        using var document = JsonDocument.Parse(json);
        var names = document.RootElement.EnumerateObject().Select(p => p.Name);

        names.Should().Equal(s_expectedPropertyNames);
    }

    [Fact]
    public void GivenResponse_WhenSerialised_ShouldNotContainLowercaseFilename()
    {
        var json = JsonSerializer.Serialize(s_dto);

        json.Should().Contain("\"fileName\"").And.NotContain("\"filename\"");
    }
}