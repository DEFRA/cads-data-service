using Cads.Cds.StorageBridge.Infrastructure.Storage.Crypto;
using FluentAssertions;
using System.Security.Cryptography;
using System.Text;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.Storage.Crypto;

public class AesCryptoTransformTests
{
    // Synthetic values only - must not mirror real CTSM filenames or the bridge's salt.
    private const string Passphrase = "unit-test-passphrase";
    private const string Salt = "not-a-real-salt-just-for-tests";

    [Fact]
    public void DeriveKey_matches_the_cads_bridge_derivation()
    {
        var key = AesCryptoTransform.DeriveKey(Passphrase, Encoding.UTF8.GetBytes(Salt));

        Convert.ToBase64String(key).Should().Be("RAXkGSK1ayJBwMmsI05oabOfHckb8P/9rC2LhXObE0Q=");
    }

    [Fact]
    public void CreateDecryptor_round_trips_data_encrypted_like_ctsm_does()
    {
        var plaintext = Encoding.UTF8.GetBytes("ANIMAL_ID,STATUS,UPDATED\n1001,ACTIVE,2026-07-30\n1002,SOLD,2026-07-29\n");
        var ciphertext = Encrypt(plaintext, Passphrase, Salt);

        var decrypted = Decrypt(ciphertext, Passphrase, Salt);

        decrypted.Should().Equal(plaintext);
    }

    [Fact]
    public void CreateDecryptor_fails_on_a_wrong_passphrase()
    {
        var ciphertext = Encrypt(Encoding.UTF8.GetBytes("some,csv,content\n"), Passphrase, Salt);

        var decrypt = () => Decrypt(ciphertext, "not-the-passphrase", Salt);

        decrypt.Should().Throw<CryptographicException>();
    }

    /// <summary>Mirrors cads-bridge's AesCryptoTransform.EncryptStreamAsync setup.</summary>
    private static byte[] Encrypt(byte[] plaintext, string passphrase, string salt)
    {
        using var aes = Aes.Create();
        aes.Key = AesCryptoTransform.DeriveKey(passphrase, Encoding.UTF8.GetBytes(salt));
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor();
        return encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
    }

    private static byte[] Decrypt(byte[] ciphertext, string passphrase, string salt)
    {
        using var decryptor = AesCryptoTransform.CreateDecryptor(passphrase, salt);
        using var cryptoStream = new CryptoStream(new MemoryStream(ciphertext), decryptor, CryptoStreamMode.Read);
        using var output = new MemoryStream();
        cryptoStream.CopyTo(output);
        return output.ToArray();
    }
}
