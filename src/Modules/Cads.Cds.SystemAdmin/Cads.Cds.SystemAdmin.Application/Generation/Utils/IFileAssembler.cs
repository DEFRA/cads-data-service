using Cads.Cds.SystemAdmin.Application.Imports.Envelope;

namespace Cads.Cds.SystemAdmin.Application.Generation.Utils;

public interface IFileAssembler
{
    /// <summary>
    /// Assembles rows into the H/C/D/T envelope a vendor extract uses.
    /// </summary>
    /// <param name="fileName">The file's uploaded filename, written to the H and T records.</param>
    /// <param name="fileCreatedDateTime">The informational H and T datetime, and the default delta aud datetime.</param>
    /// <param name="table">The table's prefix and business columns. Every row must have exactly these columns.</param>
    /// <param name="rows">One dictionary per D record, keyed by business column name.</param>
    /// <param name="options">Bulk or delta layout, and the delta aud values.</param>
    string Create(
        string fileName,
        DateTime fileCreatedDateTime,
        EnvelopeTable table,
        IReadOnlyList<IReadOnlyDictionary<string, object?>> rows,
        FileAssemblyOptions options);
}