using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Core.DTOs;

public class CreateS3BulkLoadJobDto
{
    public Guid? JobId { get; set; }

    public string SourceKey { get; set; } = string.Empty;

    public BulkLoadDataTypes BulkImportType { get; set; }

    public char Delimiter { get; set; } = '|';

    public ImportActions ImportActionType { get; set; }

    public bool UpdateConstraints { get; set; } = false;
}