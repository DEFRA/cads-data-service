using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Testing.Support.Factories;

public static class FileImportDataFactory
{
    public const string FileName_Pending = "CTSM_UKV_PROD_BULK_######_CT_PARTIES_2026-02-22-074603_PENDING";
    public const string FileName_Importing = "CTSM_UKV_PROD_BULK_######_CT_LOCATION_IDENTIFIERS_2026-02-22-074603_IMPORTING";

    public static List<FileImport> CreateMockData()
    {
        var fileImportPending = FileImport.Create(Guid.NewGuid().ToString(), FileName_Pending, 100, 0);
        var fileImportImporting = FileImport.Create(Guid.NewGuid().ToString(), FileName_Importing, 200, 0);
        fileImportImporting.MarkImporting();

        return [fileImportPending, fileImportImporting];
    }
}
