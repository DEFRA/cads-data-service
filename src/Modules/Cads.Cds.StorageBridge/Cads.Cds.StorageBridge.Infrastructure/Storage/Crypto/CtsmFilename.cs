// Copied from cads-bridge CadsBridge.Infrastructure/DataLoad/Csv/Files/CtsmFilename.cs
namespace Cads.Cds.StorageBridge.Infrastructure.Storage.Crypto;

public record CtsmFilename(
    string App,
    string Env,
    string Type,
    string BatchId,
    string? PartNo,
    string TableName,
    string Timestamp
);
