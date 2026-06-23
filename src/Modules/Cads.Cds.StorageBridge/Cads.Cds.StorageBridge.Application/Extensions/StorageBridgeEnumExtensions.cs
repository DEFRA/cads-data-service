using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Core.Attributes;

namespace Cads.Cds.StorageBridge.Application.Extensions;

public static class StorageBridgeEnumExtensions
{
    public static string? GetTableName(this Enum value)
    {
        return value.GetAttribute<TableNameAttribute>()?.Name;
    }

    public static string? GetTableKey(this Enum value)
    {
        return value.GetAttribute<TableNameAttribute>()?.Key;
    }

    public static SchemaName? GetTableSchema(this Enum value)
    {
        return value.GetAttribute<TableNameAttribute>()?.Schema;
    }
}