using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Testing.Support.Factories;

public static class FileImportDataFactory
{
    // filename format for CTSM CLA imports
    // Filename template:CTSM_CLA_<env>_<type>_<batchId>_<tablename>_<YYYY-MM-DD-hhmmss>.csv

    // Fixed scenarios
    public const string Old_Scenario_Pending_FileName = "CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-PART1";
    public const string Old_Scenario_Importing_FileName = "CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-PART2";
    public const string Old_Scenario_Complete_FileName = "CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-PART3";
    public const string Old_Scenario_Failed_FileName = "CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-PART4";

    // Mutable scenarios
    public const string Old_Scenario_Create_Bulk_FileName = "CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-PART5";
    public const string Old_Scenario_Create_Delta_FileName = "CTSM_CLA_PROD_DELTA_ABC_CT_PARTIES_2026-01-01-012345-PART5";
    public const string Old_Scenario_MarkImporting_FileName = "CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-PART6";
    public const string Old_Scenario_MarkImportComplete_FileName = "CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-PART7";
    public const string Old_Scenario_MarkImportFailed_FileName = "CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-PART8";
    public const string Old_Scenario_Reset_FileName = "CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-PART9";

    public const string Old_Scenario_Create_Invalid_FileName = "CTSM_CLA_PROD_XXXX_ABC_CT_0001_PARTIES_2026-01-01-012345";

    // New filename format for CTSM CADS imports
    // Filename template:CTSM_CADS_<env>_<type>_<batchId>_<partno>_<tablename>_<YYYY-MM-DD-hhmmss>.csv

    // New Fixed scenarios
    public const string New_Scenario_Pending_FileName = "CTSM_CADS_PROD_BULK_ABC_0001_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Importing_FileName = "CTSM_CADS_PROD_BULK_ABC_0002_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Complete_FileName = "CTSM_CADS_PROD_BULK_ABC_0003_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Failed_FileName = "CTSM_CADS_PROD_BULK_ABC_0004_CT_PARTIES_2026-01-01-012345";

    // New Mutable scenarios
    public const string New_Scenario_Create_Bulk_FileName = "CTSM_CADS_PROD_BULK_ABC_0005_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Create_Delta_FileName = "CTSM_CADS_PROD_DELTA_ABC_0005_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_MarkImporting_FileName = "CTSM_CADS_PROD_BULK_ABC_0006_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_MarkImportComplete_FileName = "CTSM_CADS_PROD_BULK_ABC_0007_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_MarkImportFailed_FileName = "CTSM_CADS_PROD_BULK_ABC_0008_CT_PARTIES_2026-01-01-012345";
    public const string New_Scenario_Reset_FileName = "CTSM_CADS_PROD_BULK_ABC_0009_CT_PARTIES_2026-01-01-012345";

    public const string New_Scenario_Create_Invalid_FileName = "CTSM_CADS_PROD_XXXX_ABC_CT_0001_PARTIES_2026-01-01-012345";

    public static List<FileImport> CreateMockData()
    {
        return [
            // Fixed scenarios
            Build(Old_Scenario_Pending_FileName),
            Build(Old_Scenario_Importing_FileName, fi => fi.MarkImporting()),
            Build(Old_Scenario_Complete_FileName, fi =>
            {
                fi.MarkImporting();
                fi.MarkImportComplete();
            }),
            Build(Old_Scenario_Failed_FileName, fi =>
            {
                fi.MarkImporting();
                fi.MarkImportFailed();
            }),

            // Mutable scenarios
            Build(Old_Scenario_MarkImporting_FileName),
            Build(Old_Scenario_MarkImportComplete_FileName, fi => fi.MarkImporting()),
            Build(Old_Scenario_MarkImportFailed_FileName, fi => fi.MarkImporting()),
            Build(Old_Scenario_Reset_FileName, fi =>
            {
                fi.MarkImporting();
                fi.MarkImportFailed();
            }),

            // Fixed scenarios
            Build(New_Scenario_Pending_FileName),
            Build(New_Scenario_Importing_FileName, fi => fi.MarkImporting()),
            Build(New_Scenario_Complete_FileName, fi =>
            {
                fi.MarkImporting();
                fi.MarkImportComplete();
            }),
            Build(New_Scenario_Failed_FileName, fi =>
            {
                fi.MarkImporting();
                fi.MarkImportFailed();
            }),

            // Mutable scenarios
            Build(New_Scenario_MarkImporting_FileName),
            Build(New_Scenario_MarkImportComplete_FileName, fi => fi.MarkImporting()),
            Build(New_Scenario_MarkImportFailed_FileName, fi => fi.MarkImporting()),
            Build(New_Scenario_Reset_FileName, fi =>
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