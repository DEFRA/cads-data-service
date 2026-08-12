namespace Cads.Cds.BuildingBlocks.Core.Extensions;

public static class StringExtensions
{
    extension(string? text)
    {
        public string? NormalizeToUpper() => text?.ToUpperInvariant();

        public string? NormalizeToLower() => text?.ToLowerInvariant();

        public string? ParseUpToFirstOccurrence(string? occurence)
        {
            ArgumentNullException.ThrowIfNull(text);
            
            var index = string.IsNullOrWhiteSpace(occurence) ? -1 : text.IndexOf(occurence, StringComparison.Ordinal);
            return index < 0 ? text : text[..index];
        }
    }
}