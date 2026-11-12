using Cads.Cds.BuildingBlocks.Application.Encryption;

namespace Cads.Cds.BuildingBlocks.Application.Extensions;

public static class FileEncryptorExtensions
{
    /// <summary>
    /// Decrypts into memory and returns plaintext only after the entire file has authenticated.
    /// The caller owns and must dispose the returned stream.
    /// </summary>
    public static async Task<Stream> DecryptToStreamAsync(this IFileEncryptor encryptor, Stream input,
        long? totalBytes = null, Action<int, string>? progress = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(encryptor);
        ArgumentNullException.ThrowIfNull(input);

        var plaintext = new MemoryStream();
        try
        {
            await encryptor.DecryptAsync(input, plaintext, totalBytes, progress, cancellationToken).ConfigureAwait(false);
            plaintext.Position = 0;
            return plaintext;
        }
        catch
        {
            await plaintext.DisposeAsync().ConfigureAwait(false);
            throw;
        }
    }

    /// <summary>
    /// Decrypts into a temporary file next to <paramref name="destinationPath"/> and moves it into place only after
    /// the whole file has authenticated. On any failure the temporary file is deleted and the destination is untouched.
    /// </summary>
    public static async Task DecryptToFileAsync(this IFileEncryptor encryptor, Stream input, string destinationPath,
        long? totalBytes = null, Action<int, string>? progress = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(encryptor);
        ArgumentException.ThrowIfNullOrEmpty(destinationPath);

        var tempPath = $"{destinationPath}.{Guid.NewGuid():N}.tmp";
        try
        {
            await using (var temp = new FileStream(tempPath, FileMode.CreateNew, FileAccess.Write, FileShare.None,
                             bufferSize: 81920, FileOptions.Asynchronous | FileOptions.SequentialScan))
            {
                await encryptor.DecryptAsync(input, temp, totalBytes, progress, cancellationToken).ConfigureAwait(false);
                await temp.FlushAsync(cancellationToken).ConfigureAwait(false);
            }

            File.Move(tempPath, destinationPath, overwrite: true);
        }
        catch
        {
            try { File.Delete(tempPath); } catch (IOException) { } catch (UnauthorizedAccessException) { }
            throw;
        }
    }
}