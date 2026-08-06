namespace Cads.Cds.BuildingBlocks.Core.DTOs;
public class CreateS3SqlImportJobDto : CreateS3ImportJobDto
{
    public string SourceKey { get; set; } = string.Empty;
}