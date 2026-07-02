namespace Cads.Cds.BuildingBlocks.Core.Extensions;

public static class StringExtensions
{
    public static string? NormalizeToUpper(string? value) =>
        value?.ToUpperInvariant();
}