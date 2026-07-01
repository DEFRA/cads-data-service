using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Testing.Support.Factories;

public static class FileImportDataFactory
{
    // Fixed scenarios
    public const string Scenario_Pending_FileName = "CTSM_UKV_PROD_BULK_######_CT_PARTIES_2026-01-01-012345-part1";
    public const string Scenario_Importing_FileName = "CTSM_UKV_PROD_BULK_######_CT_PARTIES_2026-01-01-012345-part2";
    public const string Scenario_Complete_FileName = "CTSM_UKV_PROD_BULK_######_CT_PARTIES_2026-01-01-012345-part3";
    public const string Scenario_Failed_FileName = "CTSM_UKV_PROD_BULK_######_CT_PARTIES_2026-01-01-012345-part4";

    // Mutable scenarios
    public const string Scenario_Create_FileName = "CTSM_UKV_PROD_BULK_######_CT_PARTIES_2026-01-01-012345-part5";
    public const string Scenario_MarkImporting_FileName = "CTSM_UKV_PROD_BULK_######_CT_PARTIES_2026-01-01-012345-part6";
    public const string Scenario_MarkImportComplete_FileName = "CTSM_UKV_PROD_BULK_######_CT_PARTIES_2026-01-01-012345-part7";
    public const string Scenario_MarkImportFailed_FileName = "CTSM_UKV_PROD_BULK_######_CT_PARTIES_2026-01-01-012345-part8";
    public const string Scenario_Reset_FileName = "CTSM_UKV_PROD_BULK_######_CT_PARTIES_2026-01-01-012345-part9";

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