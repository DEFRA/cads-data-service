using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Application.Imports.Attributes;
using Cads.Cds.BuildingBlocks.Application.Schema;

namespace Cads.Cds.BuildingBlocks.Application.Imports.Utilities;

public static class TableInfoAttributeExtensions
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