namespace Cads.Cds.BuildingBlocks.Core.Extensions;

public static class StringExtensions
{
    extension(string? text)
    {
        public string? NormalizeToUpper() => text?.ToUpperInvariant();

        public string? NormalizeToLower() => text?.ToLowerInvariant();

        public string? ParseUpToFirstOccurrence(string occurence)
        {
            ArgumentNullException.ThrowIfNull(text);

            var index = text!.IndexOf(occurence);
            return index < 0 ? text : text[..index];
        }
    }
}