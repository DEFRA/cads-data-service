namespace Cads.Cds.BuildingBlocks.Testing.Support.ProblemDetails;

public class ValidationProblemDetailsDto
{
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public int? Status { get; set; }
    public string? Instance { get; set; }

    public Dictionary<string, string[]>? Errors { get; set; }
}