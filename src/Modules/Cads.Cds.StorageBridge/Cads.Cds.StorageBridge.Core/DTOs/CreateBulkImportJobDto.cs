using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Core.DTOs;

public class CreateBulkImportJobDto
{
    public Guid? JobId { get; set; } 

    public string SourceKey { get; set; } = string.Empty;

    public BulkImportType BulkImportType { get; set; }
    
    public char Delimiter { get; set; } = '|';
}