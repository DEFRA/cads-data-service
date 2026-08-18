namespace Cads.Cds.BuildingBlocks.Core.DTOs;

public abstract class CreateS3ImportJobDto
{
    public Guid? JobId { get; set; }

    public string? CorrelationId { get; set; }
}