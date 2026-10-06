using Cads.Cds.StorageBridge.Endpoints.Responses;
using System.Text;

namespace Cads.Cds.StorageBridge.Endpoints;

/// <summary>
/// Reads a slice of delimiter-separated rows from a stream without buffering
/// the whole object: rows before the slice are discarded as they are decoded,
/// and reading stops as soon as the slice is complete so the rest of the
/// object is never pulled from storage.
/// </summary>
internal static class StorageRowSliceReader
{
    private const int ChunkChars = 64 * 1024;

    /// <summary>
    /// A row must contain a delimiter within this many characters, or the read
    /// is abandoned — otherwise a wrong delimiter on a huge object would
    /// buffer the whole thing, which this reader exists to avoid.
    /// </summary>
    internal const int MaxRowChars = 1024 * 1024;

    /// <summary>Reads rows [startRow, startRow + rowCount), 1-based.</summary>
    /// <exception cref="InvalidDataException">No delimiter within <see cref="MaxRowChars"/> characters.</exception>
    public static async Task<StorageRowSliceResponse> ReadAsync(
        Stream stream,
        int startRow,
        int rowCount,
        string delimiter,
        CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(stream, Encoding.UTF8);

        var rows = new List<string>(rowCount);
        var chunk = new char[ChunkChars];
        var buffer = string.Empty;
        var rowNumber = 0;

        while (true)
        {
            var read = await reader.ReadAsync(chunk.AsMemory(), cancellationToken);

            if (read == 0)
            {
                break;
            }

            buffer += new string(chunk, 0, read);

            var pieces = buffer.Split(delimiter);
            buffer = pieces[^1]; // keep the trailing partial row (and any partial delimiter)

            for (var i = 0; i < pieces.Length - 1; i++)
            {
                rowNumber++;

                if (rowNumber >= startRow)
                {
                    rows.Add(pieces[i]);

                    if (rows.Count == rowCount)
                    {
                        return new StorageRowSliceResponse(rows, ReachedEnd: false);
                    }
                }
            }

            if (buffer.Length > MaxRowChars)
            {
                throw new InvalidDataException(
                    $"No delimiter found within {MaxRowChars:N0} characters — check the delimiter.");
            }
        }

        // A trailing delimiter leaves an empty buffer, which is not a final row.
        if (buffer.Length > 0)
        {
            rowNumber++;

            if (rowNumber >= startRow)
            {
                rows.Add(buffer);
            }
        }

        return new StorageRowSliceResponse(rows, ReachedEnd: true);
    }
}