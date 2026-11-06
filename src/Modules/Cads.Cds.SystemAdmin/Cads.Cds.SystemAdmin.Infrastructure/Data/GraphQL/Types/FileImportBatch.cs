using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Types;

public sealed class FileImportBatch
{
    public string GroupKey { get; set; } = null!;
    public bool ImportComplete { get; set; }
    public bool ProcessingComplete { get; set; }
    public IReadOnlyList<FileImportBatchPart> Parts { get; set; } = new List<FileImportBatchPart>();
}
