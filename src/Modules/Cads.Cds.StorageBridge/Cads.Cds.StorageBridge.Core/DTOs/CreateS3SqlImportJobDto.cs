namespace Cads.Cds.StorageBridge.Core.DTOs;

public class CreateS3SqlImportJobDto
{
    public Guid? JobId { get; set; }

    public string SourceKey { get; set; } = string.Empty;
}