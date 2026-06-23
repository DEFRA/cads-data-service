using Cads.Cds.BuildingBlocks.Infrastructure.Database;

namespace Cads.Cds.StorageBridge.Core.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class TableNameAttribute(string name, string? key = null, SchemaName schemaName = SchemaName.Public) : Attribute
{
    public string Name { get; } = name;

    public string? Key { get; } = key;

    public SchemaName Schema { get; } = schemaName;
}