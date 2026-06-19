namespace Cads.Cds.StorageBridge.Core.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class TableNameAttribute(string name, string? key = null, string? schema = null) : Attribute
{
    public string Name { get; } = name;

    public string? Key { get; } = key;

    public string? Schema { get; } = schema;
}