using System.ComponentModel;
using System.Reflection;

namespace Cads.Cds.BuildingBlocks.Core.Extensions.Enums;

public static class EnumExtensions
{
    public static string? GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description;
    }
}