using Cads.Cds.SystemAdmin.Application.Generation.Utils;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;

public class FileNameGenerator : IFileNameGenerator
{
    // Pattern 1: CTSM_<app>_<env>_<type>_<batchId>_<partno>_<tablename>_<timestamp>.csv
    // Pattern 2: CTSM_<app>_<env>_<type>_<batchId>_<tablename>_<timestamp>.csv
    public string Create(CreateFileNameCommand command)
    {
        // Normalize extension to avoid double dots (caller may pass ".csv" or "csv")
        var ext = command.Extension.TrimStart('.');
        var timestamp = command.Timestamp.ToString("yyyy-MM-dd-HHmss");
        string filename;

        // Include partNo when provided (follows Pattern 1); omit when empty (Pattern 2)
        if (!string.IsNullOrWhiteSpace(command.PartNo))
        {
            filename = $"{command.Prefix}_{command.App}_{command.Env}_{command.Type}_{command.BatchId}_{command.PartNo}_{command.TableName}_{timestamp}.{ext}";
        }
        else
        {
            filename = $"{command.Prefix}_{command.App}_{command.Env}_{command.Type}_{command.BatchId}_{command.TableName}_{timestamp}.{ext}";
        }

        return filename.ToUpper();
    }
}