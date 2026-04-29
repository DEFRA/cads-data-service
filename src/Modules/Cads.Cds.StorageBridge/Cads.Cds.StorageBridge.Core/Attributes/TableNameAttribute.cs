namespace Cads.Cds.StorageBridge.Core.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
internal sealed class TableNameAttribute(string name, string? key = null) : Attribute
{
    public string Name { get; } = name;

    public string? Key { get; } = key;
}