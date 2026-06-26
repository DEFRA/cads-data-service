using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Testing.Support.Factories;

public class FileImportDataFactory
{
    public List<FileImport> CreateMockData()
    {
        var fileImportPending = FileImport.Create("dtnPending", "fnPending", 100, 0);
        var fileImportImporting = FileImport.Create("dtnImporting", "fnImporting", 100, 0);
        fileImportImporting.MarkImporting();

        return [fileImportPending, fileImportImporting];
    }
}
