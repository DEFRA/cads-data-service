using Cads.Cds.StorageBridge.Core.Attributes;
using System.Reflection;

namespace Cads.Cds.StorageBridge.Core.Extensions;

public static class EnumExtensions
{
    public static string? GetTableName(this Enum value)
    {
        ArgumentNullException.ThrowIfNull(value);

        // Get the enum type and member info
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        if (name == null) return null;

        var field = type.GetField(name);
        if (field == null) return null;

        // Retrieve the TableNameAttribute if it exists
        var attribute = field.GetCustomAttribute<TableNameAttribute>();
        return attribute?.Name;
    }
}
