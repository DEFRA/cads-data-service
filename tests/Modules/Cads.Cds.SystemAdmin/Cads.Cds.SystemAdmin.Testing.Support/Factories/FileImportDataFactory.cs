using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;

namespace Cads.Cds.SystemAdmin.Testing.Support.Factories;

public static class FileImportDataFactory
{
    public static List<FileImport> CreateMockData()
    {
        return [
            // Fixed scenarios
            Build(TestFileScenarioConstants.New_Scenario_Pending_FileName),
            Build(TestFileScenarioConstants.New_Scenario_Transferred_FileName, fi =>
            {
                fi.MarkTransferred();
            }),
            Build(TestFileScenarioConstants.New_Scenario_Split_FileName, fi =>
            {
                fi.MarkTransferred();
                fi.MarkSplit();
            }),
            Build(TestFileScenarioConstants.New_Scenario_Complete_FileName, fi =>
            {
                fi.MarkTransferred();
                fi.MarkSplit();
                fi.MarkCompleted();
            }),
            Build(TestFileScenarioConstants.New_Scenario_Failed_FileName, fi =>
            {
                fi.MarkTransferred();
                fi.MarkFailed("import failed");
            }),

            // Mutable scenarios
            Build(TestFileScenarioConstants.New_Scenario_MarkTransferred_FileName),
            Build(TestFileScenarioConstants.New_Scenario_Pending_Update_Transferred_FileName),
            Build(TestFileScenarioConstants.New_Scenario_MarkSplit_FileName, fi =>
            {
                fi.MarkTransferred();
            }),
            Build(TestFileScenarioConstants.New_Scenario_Transferred_Update_Split_FileName, fi =>
            {
                fi.MarkTransferred();
            }),
            Build(TestFileScenarioConstants.New_Scenario_Transferred_Update_Failed_FileName, fi =>
            {
                fi.MarkTransferred();
            }),
            Build(TestFileScenarioConstants.New_Scenario_MarkImportComplete_FileName, fi =>
            {
                fi.MarkTransferred();
                fi.MarkSplit();
            }),
            Build(TestFileScenarioConstants.New_Scenario_MarkImportFailed_FileName, fi =>
            {
                fi.MarkTransferred();
            }),
            Build(TestFileScenarioConstants.New_Scenario_Reset_FileName, fi =>
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