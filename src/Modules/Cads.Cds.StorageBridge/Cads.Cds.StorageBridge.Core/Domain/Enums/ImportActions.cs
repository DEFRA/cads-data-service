namespace Cads.Cds.StorageBridge.Core.Domain.Enums;

[Flags]
public enum ImportActions
{
    None,
    Insert = 1 << 0, // 1
    Update = 1 << 1, // 2
    Delete = 1 << 2, // 4
}