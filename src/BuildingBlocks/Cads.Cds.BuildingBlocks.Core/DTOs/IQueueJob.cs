namespace Cads.Cds.BuildingBlocks.Core.DTOs;

public interface IQueueJob
{
    Guid? JobId { get; set; }

    string? CorrelationId { get; set; }
}