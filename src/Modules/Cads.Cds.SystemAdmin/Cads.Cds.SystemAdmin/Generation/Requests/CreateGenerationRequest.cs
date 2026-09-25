namespace Cads.Cds.SystemAdmin.Generation.Requests;

public class CreateGenerationRequest
{
    public required string Scenario { get; set; }
    public int? RowCount { get; set; }
};