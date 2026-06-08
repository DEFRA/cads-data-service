using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Core.DTOs;

public class CreateS3CsvBulkLoadJobDto : CreateS3BulkLoadJobDto
{
    public BulkLoadDataType BulkImportType { get; set; }

    public char Delimiter { get; set; } = '|';

    public ImportActions ImportActionType { get; set; }

    public bool UpdateConstraints { get; set; } = false;
}