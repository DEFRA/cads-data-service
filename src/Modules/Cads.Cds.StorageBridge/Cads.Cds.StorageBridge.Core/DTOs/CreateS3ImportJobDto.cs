namespace Cads.Cds.StorageBridge.Core.DTOs;

public abstract class CreateS3ImportJobDto
{
    public Guid? JobId { get; set; }

    public string SourceKey { get; set; } = string.Empty;
}