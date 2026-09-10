using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;

namespace Cads.Cds.StorageBridge.Application.S3Import.Services;

public interface IDefensiveCopyLineNormaliser
{
    string Normalise(
        string line,
        ImportDataType importDataType,
        char delimiter,
        int expectedColumnCount);
}