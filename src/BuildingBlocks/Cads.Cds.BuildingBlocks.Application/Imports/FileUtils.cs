using Cads.Cds.BuildingBlocks.Core.Extensions;

namespace Cads.Cds.BuildingBlocks.Application.Imports;

public static class FileUtils
{
    public static (string, string) GetImportParametersFromFileName(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException("Invalid file name.");
        }

        // Assuming the file name format is:
        // CTSM_CADS_<env>_<type>_<batchId>_<partno>_<tablename>_<YYYY-MM-DD-hhmmss>.csv
        var normalisedFileName = StringExtensions.NormalizeToLower(StringExtensions.ParseUpToFirstOccurrence(fileName, "."));
        var parts = normalisedFileName!.Split('_');
        var destinationTableName = parts[6] + "_" + parts[7];

        if (parts.Length < 7)
        {
            throw new ArgumentException("Invalid file name format.");
        }

        // The destination table name is the 7th part of the file name
        return new(destinationTableName, parts[3]);
    }
}