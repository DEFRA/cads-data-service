namespace Cads.Cds.SystemAdmin.Core.DTOs.Generation;

public class CreateGenerationResponseDto
{
    public required string FileName { get; set; }
    public required string Content { get; set; }
    public required IEnumerable<long> Keys { get; set; }
}