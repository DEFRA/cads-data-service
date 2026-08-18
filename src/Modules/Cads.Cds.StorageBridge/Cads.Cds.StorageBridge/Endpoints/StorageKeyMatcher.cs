using System.Text;
using System.Text.RegularExpressions;

namespace Cads.Cds.StorageBridge.Endpoints;

/// <summary>
/// Compiles the storage-management listing filter into a case-insensitive key
/// predicate. Supported modes: contains (default), glob (* and ?, where * also
/// matches /) and regex.
/// </summary>
internal static class StorageKeyMatcher
{
    private static readonly TimeSpan MatchTimeout = TimeSpan.FromMilliseconds(500);

    /// <summary>Returns null when the mode is unknown or the pattern is not a valid regex.</summary>
    public static Func<string, bool>? Create(string pattern, string? mode)
    {
        switch (mode)
        {
            case null or "" or "contains":
                return value => value.Contains(pattern, StringComparison.OrdinalIgnoreCase);
            case "glob":
                return ToPredicate(new Regex(GlobToRegexSource(pattern), RegexOptions.IgnoreCase, MatchTimeout));
            case "regex":
                try
                {
                    return ToPredicate(new Regex(pattern, RegexOptions.IgnoreCase, MatchTimeout));
                }
                catch (ArgumentException)
                {
                    return null;
                }
            default:
                return null;
        }
    }

    private static Func<string, bool> ToPredicate(Regex regex) =>
        value =>
        {
            try
            {
                return regex.IsMatch(value);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        };

    private static string GlobToRegexSource(string glob)
    {
        var source = new StringBuilder("^");

        foreach (var character in glob)
        {
            source.Append(character switch
            {
                '*' => ".*",
                '?' => ".",
                _ => Regex.Escape(character.ToString())
            });
        }

        return source.Append('$').ToString();
    }
}
