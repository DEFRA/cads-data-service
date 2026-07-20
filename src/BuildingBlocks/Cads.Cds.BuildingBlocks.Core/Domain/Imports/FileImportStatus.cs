namespace Cads.Cds.BuildingBlocks.Core.Domain.Imports;

public enum FileImportStatus : short
{
    Pending = 1,
    Transferred = 2,
    Split = 3,
    Completed = 4,
    Failed = 5
}