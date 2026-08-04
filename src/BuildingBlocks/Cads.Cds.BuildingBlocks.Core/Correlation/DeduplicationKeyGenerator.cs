using System.Security.Cryptography;
using System.Text;

namespace Cads.Cds.BuildingBlocks.Core.Correlation;

public static class DeduplicationKeyGenerator
{
    public static string GenerateDeduplicationId(
        string bucket,
        string objectKey,
        string fileImportId,
        string environment)
    {
        var raw = $"{bucket}:{objectKey}:{fileImportId}:{environment}";
        var bytes = Encoding.UTF8.GetBytes(raw);
        return Convert.ToHexString(SHA256.HashData(bytes));
    }

    public static string GenerateMessageGroupId(string objectKey, string environment)
    {
        var prefix = objectKey.Contains('/')
            ? objectKey[..objectKey.LastIndexOf('/')]
            : objectKey;

        return $"{prefix}:{environment}";
    }
}