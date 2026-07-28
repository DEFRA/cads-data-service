using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Testing.Support.Factories;

public static class FileImportDataFactory
{
    // New filename format for CTSM CADS imports
    // Filename template:CTSM_CADS_<env>_<type>_<batchId>_<partno>_<tablename>_<YYYY-MM-DD-hhmmss>.csv

    // New Fixed scenarios
    public const string New_Scenario_Pending_FileName = "CTSM_CADS_PROD_BULK_ABC_0001_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Transferred_FileName = "CTSM_CADS_PROD_BULK_ABC_0002_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Split_FileName = "CTSM_CADS_PROD_BULK_ABC_0003_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Complete_FileName = "CTSM_CADS_PROD_BULK_ABC_0004_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Failed_FileName = "CTSM_CADS_PROD_BULK_ABC_0005_CT_PARTIES_2026-01-01-012345";

    // New Mutable scenarios
    public const string New_Scenario_Create_Bulk_FileName = "CTSM_CADS_PROD_BULK_ABC_0006_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Create_Delta_FileName = "CTSM_CADS_PROD_DELTA_ABC_0006_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_MarkTransferred_FileName = "CTSM_CADS_PROD_BULK_ABC_0007_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_MarkSplit_FileName = "CTSM_CADS_PROD_BULK_ABC_0008_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_MarkImportComplete_FileName = "CTSM_CADS_PROD_BULK_ABC_0009_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_MarkImportFailed_FileName = "CTSM_CADS_PROD_BULK_ABC_0010_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Reset_FileName = "CTSM_CADS_PROD_BULK_ABC_0011_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Pending_Update_Transferred_FileName = "CTSM_CADS_PROD_BULK_ABC_0012_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Transferred_Update_Split_FileName = "CTSM_CADS_PROD_BULK_ABC_0013_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Transferred_Update_Failed_FileName = "CTSM_CADS_PROD_BULK_ABC_0014_CT_PARTIES_2026-01-01-012345";

    public const string New_Scenario_Create_Invalid_FileName = "CTSM_CADS_PROD_XXXX_ABC_CT_0001_PARTIES_2026-01-01-012345";

    public static List<FileImport> CreateMockData()
    {
        return [
            // Fixed scenarios
            Build(New_Scenario_Pending_FileName),
            Build(New_Scenario_Transferred_FileName, fi =>
            {
                fi.MarkTransferred();
            }),
            Build(New_Scenario_Split_FileName, fi =>
            {
                fi.MarkTransferred();
                fi.MarkSplit();
            }),
            Build(New_Scenario_Complete_FileName, fi =>
            {
                fi.MarkTransferred();
                fi.MarkSplit();
                fi.MarkCompleted();
            }),
            Build(New_Scenario_Failed_FileName, fi =>
            {
                fi.MarkTransferred();
                fi.MarkFailed("import failed");
            }),

            // Mutable scenarios
            Build(New_Scenario_MarkTransferred_FileName),
            Build(New_Scenario_Pending_Update_Transferred_FileName),
            Build(New_Scenario_MarkSplit_FileName, fi =>
            {
                fi.MarkTransferred();
            }),
            Build(New_Scenario_Transferred_Update_Split_FileName, fi =>
            {
                fi.MarkTransferred();
            }),
            Build(New_Scenario_Transferred_Update_Failed_FileName, fi =>
            {
                fi.MarkTransferred();
            }),
            Build(New_Scenario_MarkImportComplete_FileName, fi =>
            {
                fi.MarkTransferred();
                fi.MarkSplit();
            }),
            Build(New_Scenario_MarkImportFailed_FileName, fi =>
            {
                fi.MarkTransferred();
            }),
            Build(New_Scenario_Reset_FileName, fi =>
            {
                fi.MarkFailed("import failed");
            })
        ];
    }

    private static FileImport Build(string fileName, Action<FileImport>? configure = null)
    {
        var fi = FileImport.Create("dtn", fileName, 100, 0);
        configure?.Invoke(fi);
        return fi;
    }
}