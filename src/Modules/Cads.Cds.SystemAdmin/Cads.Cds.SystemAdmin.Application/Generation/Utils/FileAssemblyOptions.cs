namespace Cads.Cds.SystemAdmin.Application.Generation.Utils;

public enum FileLayout
{
    Bulk,
    Delta
}

/// <summary>
/// How to assemble a vendor extract file.
/// </summary>
/// <param name="Layout">Bulk or delta envelope layout.</param>
/// <param name="AudType">Delta only: the audit type, <c>I</c>, <c>U</c> or <c>D</c>, written to every row.</param>
/// <param name="AudIds">Delta only: one audit id per row, supplied by the caller. The assembler doesn't allocate them.</param>
/// <param name="AudDateTime">Delta only: the audit datetime written to every row. Defaults to the file-created time.</param>
public sealed record FileAssemblyOptions(
    FileLayout Layout,
    string? AudType = null,
    IReadOnlyList<long>? AudIds = null,
    DateTime? AudDateTime = null)
{
    public static readonly IReadOnlySet<string> ValidAudTypes = new HashSet<string>(StringComparer.Ordinal) { "I", "U", "D" };

    public static FileAssemblyOptions Bulk() => new(FileLayout.Bulk);

    public static FileAssemblyOptions Delta(string audType, IReadOnlyList<long> audIds, DateTime? audDateTime = null) =>
        new(FileLayout.Delta, audType, audIds, audDateTime);
}