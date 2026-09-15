namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Models;

public sealed record FileExecutionContext(
    ImportExecutionContext ImportContext,
    string Key);