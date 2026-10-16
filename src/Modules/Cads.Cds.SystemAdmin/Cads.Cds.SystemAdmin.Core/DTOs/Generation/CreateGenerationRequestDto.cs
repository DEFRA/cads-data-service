namespace Cads.Cds.SystemAdmin.Core.DTOs.Generation;

public class CreateGenerationRequestDto
{
    public required string Scenario { get; set; }

    public int? RowCount { get; set; }

    public IEnumerable<decimal>? BusinessKeys { get; set; }
}