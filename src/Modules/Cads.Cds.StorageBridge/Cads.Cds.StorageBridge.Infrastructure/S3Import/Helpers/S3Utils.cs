namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Helpers;

public static class S3Utils
{
    /// <summary>
    /// Parses an S3 URL into bucket name, object key, and filename.
    /// Supports s3:// and https:// S3 URL formats.
    /// </summary>
    /// <param name="s3Url">The S3 URL to parse.</param>
    /// <param name="bucketName">Output bucket name.</param>
    /// <param name="objectKey">Output object key (path inside bucket).</param>
    /// <param name="fileName">Output file name (last segment of key).</param>
    /// <returns>True if parsing succeeded, false otherwise.</returns>
    public static bool TryParseS3Url(string s3Url, out string bucketName, out string objectKey, out string? fileName)
    {
        // initialize non-nullable out params with non-null values
        bucketName = objectKey = string.Empty;
        fileName = null;

        if (string.IsNullOrWhiteSpace(s3Url))
            return false;

        try
        {
            if (Uri.TryCreate(s3Url, UriKind.Absolute, out var uri))
            {
                if (uri.Scheme.Equals("s3", StringComparison.OrdinalIgnoreCase))
                {
                    // Format: s3://bucket-name/path/to/object.txt
                    bucketName = uri.Host;
                    objectKey = uri.AbsolutePath.TrimStart('/');
                }
                else if (uri.Scheme.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    // Handle virtual-hosted–style: https://bucket-name.s3.region.amazonaws.com/path/to/object.txt
                    // Or path-style: https://s3.region.amazonaws.com/bucket-name/path/to/object.txt
                    var hostParts = uri.Host.Split('.');

                    if (hostParts.Length >= 3 && hostParts[1].Equals("s3", StringComparison.OrdinalIgnoreCase))
                    {
                        // Virtual-hosted–style
                        bucketName = hostParts[0];
                        objectKey = uri.AbsolutePath.TrimStart('/');
                    }
                    else
                    {
                        // Path-style
                        var segments = uri.AbsolutePath.TrimStart('/').Split('/', 2);
                        if (segments.Length >= 1)
                            bucketName = segments[0];
                        if (segments.Length == 2)
                            objectKey = segments[1];
                    }
                }
                else
                {
                    return false; // Unsupported scheme
                }
            }
            else
            {
                objectKey = s3Url; // If URI creation fails, treat the entire string as the object key
            }

            // Extract filename from object key
            if (!string.IsNullOrEmpty(objectKey))
                fileName = objectKey.Contains('/') ? objectKey.Substring(objectKey.LastIndexOf('/') + 1) : objectKey;

            return !string.IsNullOrEmpty(bucketName) || !string.IsNullOrEmpty(objectKey) || !string.IsNullOrEmpty(fileName);
        }
        catch
        {
            return false;
        }
    }
}