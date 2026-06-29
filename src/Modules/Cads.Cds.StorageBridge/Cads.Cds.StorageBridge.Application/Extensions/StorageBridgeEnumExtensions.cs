using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Core.Attributes;

namespace Cads.Cds.StorageBridge.Application.Extensions;

public static class StorageBridgeEnumExtensions
{
    public static TableInfoAttribute? GetTableInfoAttribute(this Enum value, SchemaName schemaName)
    {
        return value.GetAttributes<TableInfoAttribute>()?.FirstOrDefault(t => t.Schema == schemaName);
    }

    public static string? GetTableName(this Enum value, SchemaName schemaName)
    {
        return value.GetTableInfoAttribute(schemaName)?.Name;
    }

    public static string? GetTableKey(this Enum value, SchemaName schemaName)
    {
        return value.GetTableInfoAttribute(schemaName)?.PrimaryKey;
    }
}