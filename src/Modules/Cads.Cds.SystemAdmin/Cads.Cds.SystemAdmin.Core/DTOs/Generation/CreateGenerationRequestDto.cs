namespace Cads.Cds.SystemAdmin.Core.DTOs.Generation;

public interface ICreateGenerationRequest
{
    string Scenario { get; set; }
    int? RowCount { get; set; }
    IEnumerable<decimal>? BusinessKeys { get; set; }
    string? AudType { get; set; }
}

public class CreateGenerationRequestDto : ICreateGenerationRequest
{
    public required string Scenario { get; set; }

    public int? RowCount { get; set; }

    public IEnumerable<decimal>? BusinessKeys { get; set; }

    public string? AudType { get; set; }
}