namespace Cads.Cds.SystemAdmin.Endpoints.Generation.Requests;

public class CreateGenerationRequest
{
    public required string Scenario { get; set; }
    public int? RowCount { get; set; }
};