using Cads.Cds.BuildingBlocks.Core.Extensions;

namespace Cads.Cds.BuildingBlocks.Application.Imports;

public class FileUtils
{
   public static (string, string) GetImportParametersFromFileName(string fileName)
   {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException("Invalid file name.");
        }

        var normalisedFileName = StringExtensions.NormalizeToLower(fileName);

        // Assuming the file name format is CTSM_CLA_<env>_<type>_<batchId>_<tablename>_<YYYY-MM-DD-hhmmss>.csv
        var parts = normalisedFileName!.Split('_');

        if (parts.Length < 6)
        {
            throw new ArgumentException("Invalid file name format.");
        }

        // The destination table name is the 5th part of the file name
        return new (parts[4], parts[3]);
    }
}
