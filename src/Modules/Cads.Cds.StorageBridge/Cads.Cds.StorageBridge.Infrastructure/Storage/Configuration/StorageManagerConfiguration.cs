namespace Cads.Cds.StorageBridge.Infrastructure.Storage.Configuration;

public record StorageManagerConfiguration
{
    public bool Enabled { get; init; }

    /// <summary>
    /// Salt for deriving CTSM file decryption keys; must match cads-bridge's
    /// DataLoad:Salt so both services decrypt the same files.
    /// </summary>
    public string Salt { get; init; } = string.Empty;
}
