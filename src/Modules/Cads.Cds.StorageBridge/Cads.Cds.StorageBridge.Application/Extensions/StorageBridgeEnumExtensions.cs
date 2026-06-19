using Cads.Cds.StorageBridge.Core.Attributes;
using System.Reflection;

namespace Cads.Cds.StorageBridge.Application.Extensions;

public static class StorageBridgeEnumExtensions
{
    public static T? GetAttribute<T>(this Enum value)
        where T : Attribute
    {
        ArgumentNullException.ThrowIfNull(value);

        var type = value.GetType();
        var name = Enum.GetName(type, value);
        if (name == null) return null;

        var field = type.GetField(name);
        if (field == null) return null;

        return field.GetCustomAttribute<T>();
    }

    public static string? GetTableName(this Enum value)
    {
        return value.GetAttribute<TableNameAttribute>()?.Name;
    }

    public static string? GetTableKey(this Enum value)
    {
        return value.GetAttribute<TableNameAttribute>()?.Key;
    }

    public static string? GetTableSchema(this Enum value)
    {
        return value.GetAttribute<TableNameAttribute>()?.Schema;
    }
}