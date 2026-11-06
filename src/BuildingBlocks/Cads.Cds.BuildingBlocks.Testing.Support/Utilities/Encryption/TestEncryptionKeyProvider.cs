using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers.Configuration;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Encryption;

public static class TestEncryptionKeyProvider
{
    public const string CurrentKeyId = TestEncryptionConfiguration.CurrentKeyId;

    public const string CurrentKeyBase64 = TestEncryptionConfiguration.CurrentKeyBase64;

    public static IReadOnlyDictionary<string, byte[]> Keys { get; } =
        new Dictionary<string, byte[]>
        {
            [CurrentKeyId] = Convert.FromBase64String(CurrentKeyBase64)
        };
}