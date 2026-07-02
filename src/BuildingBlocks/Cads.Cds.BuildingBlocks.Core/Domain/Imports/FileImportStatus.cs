namespace Cads.Cds.BuildingBlocks.Core.Domain.Imports;

public enum FileImportStatus : short
{
    Pending = 1,
    Importing = 2,
    Complete = 3,
    Failed = 4
}