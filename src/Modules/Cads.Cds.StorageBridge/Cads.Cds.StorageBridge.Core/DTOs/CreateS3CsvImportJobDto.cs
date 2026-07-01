using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Core.DTOs;

public class CreateS3CsvImportJobDto : CreateS3ImportJobDto
{
    public ImportDataType ImportDataType { get; set; }

    public ImportActionType ImportActionType { get; set; }

    public char Delimiter { get; set; } = '|';
}