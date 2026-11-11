using Cads.Cds.BuildingBlocks.Infrastructure.Encryption;
using System.Text;
using Xunit;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Encryption;

public static class EncryptionUtilities
{
    public static async Task<MemoryStream> EncryptAsync(FileEncryptor encryptor, string plaintext)
    {
        var encrypted = new MemoryStream();
        await encryptor.EncryptAsync(
            new MemoryStream(Encoding.UTF8.GetBytes(plaintext)),
            encrypted,
            cancellationToken: TestContext.Current.CancellationToken);
        encrypted.Position = 0;
        return encrypted;
    }
}