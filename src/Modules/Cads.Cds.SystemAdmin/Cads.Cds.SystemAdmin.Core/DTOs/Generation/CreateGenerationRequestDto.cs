namespace Cads.Cds.SystemAdmin.Core.DTOs.Generation;

public class CreateGenerationRequestDto
{
    public required string Scenario { get; set; }

    public required int RowCount { get; set; }
}