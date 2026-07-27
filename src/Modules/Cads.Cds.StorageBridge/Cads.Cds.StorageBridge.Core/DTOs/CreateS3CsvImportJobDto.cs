namespace Cads.Cds.StorageBridge.Core.DTOs;

public class CreateS3CsvImportJobDto : CreateS3ImportJobDto
{
    public char Delimiter { get; set; } = '|';
}