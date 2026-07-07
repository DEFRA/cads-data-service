using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Testing.Support.Factories;

public static class FileImportDataFactory
{
    // Filename template:CTSM_CADS_<env>_<type>_<batchId>_<partno>_<tablename>_<YYYY-MM-DD-hhmmss>.csv

    // Fixed scenarios
    public const string Scenario_Pending_FileName = "CTSM_CADS_PROD_BULK_ABC_0001_CT_PARTIES_2026-01-01-012345";
    public const string Scenario_Importing_FileName = "CTSM_CADS_PROD_BULK_ABC_0002_CT_PARTIES_2026-01-01-012345";
    public const string Scenario_Complete_FileName = "CTSM_CADS_PROD_BULK_ABC_0003_CT_PARTIES_2026-01-01-012345";
    public const string Scenario_Failed_FileName = "CTSM_CADS_PROD_BULK_ABC_0004_CT_PARTIES_2026-01-01-012345";

    // Mutable scenarios
    public const string Scenario_Create_Bulk_FileName = "CTSM_CADS_PROD_BULK_ABC_0005_CT_PARTIES_2026-01-01-012345";
    public const string Scenario_Create_Delta_FileName = "CTSM_CADS_PROD_DELTA_ABC_0005_CT_PARTIES_2026-01-01-012345";
    public const string Scenario_MarkImporting_FileName = "CTSM_CADS_PROD_BULK_ABC_0006_CT_PARTIES_2026-01-01-012345";
    public const string Scenario_MarkImportComplete_FileName = "CTSM_CADS_PROD_BULK_ABC_0007_CT_PARTIES_2026-01-01-012345";
    public const string Scenario_MarkImportFailed_FileName = "CTSM_CADS_PROD_BULK_ABC_0008_CT_PARTIES_2026-01-01-012345";
    public const string Scenario_Reset_FileName = "CTSM_CADS_PROD_BULK_ABC_0009_CT_PARTIES_2026-01-01-012345";

    public const string Scenario_Create_Invalid_FileName = "CTSM_CADS_PROD_XXXX_ABC_CT_0001_PARTIES_2026-01-01-012345";

    public static List<FileImport> CreateMockData()
    {
        return [
            // Fixed scenarios
            Build(Scenario_Pending_FileName),
            Build(Scenario_Importing_FileName, fi => fi.MarkImporting()),
            Build(Scenario_Complete_FileName, fi =>
            {
                fi.MarkImporting();
                fi.MarkImportComplete();
            }),
            Build(Scenario_Failed_FileName, fi =>
            {
                fi.MarkImporting();
                fi.MarkImportFailed();
            }),

            // Mutable scenarios
            Build(Scenario_MarkImporting_FileName),
            Build(Scenario_MarkImportComplete_FileName, fi => fi.MarkImporting()),
            Build(Scenario_MarkImportFailed_FileName, fi => fi.MarkImporting()),
            Build(Scenario_Reset_FileName, fi =>
            {
                fi.MarkImporting();
                fi.MarkImportFailed();
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