using Cads.Cds.BuildingBlocks.Infrastructure.Database;

namespace Cads.Cds.StorageBridge.Core.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
public sealed class TableInfoAttribute(string name, string? primaryKey = null, SchemaName schemaName = SchemaName.Public) : Attribute
{
    public string Name { get; } = name;

    public string? PrimaryKey { get; } = primaryKey;

    public SchemaName Schema { get; } = schemaName;
}