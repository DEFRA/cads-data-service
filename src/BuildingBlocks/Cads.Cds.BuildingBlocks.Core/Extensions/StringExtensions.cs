namespace Cads.Cds.BuildingBlocks.Core.Extensions;

public static class StringExtensions
{
    public static string? NormalizeToUpper(string? value) =>
        value?.ToUpperInvariant();

    public static string? NormalizeToLower(string? value) =>
        value?.ToLowerInvariant();

    public static string? ParseUpToFirstOccurrence(string? value, string occurence)
    {
        ArgumentNullException.ThrowIfNull(value);

        var index = value!.IndexOf(occurence);
        return index < 0 ? value : value[..index];
    }
}