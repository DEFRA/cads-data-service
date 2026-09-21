using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;

namespace Cads.Cds.BuildingBlocks.Application.Imports.Utilities;

public sealed record CtsmFilenameRequest(ImportActionType ActionType, ImportDataType DataType)
{
    public int PartNo { get; init; } = 1;
    public int? BatchId { get; init; }
    public bool ReusePreviousFilename { get; init; }
    public string? PreviousFilename { get; init; }
}
