using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Types;

[ExcludeFromCodeCoverage]
public sealed class FileImportBatch
{
    public string GroupKey { get; set; } = null!;
    public bool ImportComplete { get; set; }
    public bool ProcessingComplete { get; set; }
    public IReadOnlyList<FileImportBatchPart> Parts { get; set; } = new List<FileImportBatchPart>();
}