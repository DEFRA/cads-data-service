namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers.Configuration;

public class TestEncryptionConfiguration
{
    public const string CurrentKeyId = "integration-test-key";

    public const string CurrentKeyBase64 =
        "MDEyMzQ1Njc4OWFiY2RlZjAxMjM0NTY3ODlhYmNkZWY=";

    public static IReadOnlyDictionary<string, byte[]> Keys { get; } =
        new Dictionary<string, byte[]>
        {
            [CurrentKeyId] = Convert.FromBase64String(CurrentKeyBase64)
        };
}
