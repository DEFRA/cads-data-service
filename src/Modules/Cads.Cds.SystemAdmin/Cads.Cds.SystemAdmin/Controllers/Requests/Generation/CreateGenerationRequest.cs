namespace Cads.Cds.SystemAdmin.Controllers.Requests.Generation;

public class CreateGenerationRequest
{
    public string Table { get; set; } = default!;
    public string Scenario { get; set; } = default!;
    public int? RowCount { get; set; }
    public long? BusinessKey { get; set; }
}