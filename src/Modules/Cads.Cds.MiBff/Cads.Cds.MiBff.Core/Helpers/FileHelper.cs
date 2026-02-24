using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Core.Helpers
{
    public static class FileHelper
    {
        public static string GetDataFilePath(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", fileName);
        }

        public static string ReadFileContent(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            return File.ReadAllText(filePath);
        }

        public static async Task<string> ReadFileContentAsync(string filePath, CancellationToken cancellationToken = default)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            return await File.ReadAllTextAsync(filePath, cancellationToken);
        }

        public static T ReadJsonFile<T>(string filePath)
        {
            var content = ReadFileContent(filePath);
            var result = System.Text.Json.JsonSerializer.Deserialize<T>(content);
            return result is null ? throw new InvalidOperationException($"Deserialization of file '{filePath}' returned null.") : result;
        }

        public static async Task<T> ReadJsonFileAsync<T>(string filePath, CancellationToken cancellationToken = default)
        {
            var content = await ReadFileContentAsync(filePath, cancellationToken);
            var result = System.Text.Json.JsonSerializer.Deserialize<T>(content);
            return result is null ? throw new InvalidOperationException($"Deserialization of file '{filePath}' returned null.") : result;
        }
    }
}
