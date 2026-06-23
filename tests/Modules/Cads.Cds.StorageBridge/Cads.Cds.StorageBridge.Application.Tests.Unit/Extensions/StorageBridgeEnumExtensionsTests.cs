using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Application.Extensions;
using Cads.Cds.StorageBridge.Core.Attributes;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using FluentAssertions;

namespace Cads.Cds.StorageBridge.Application.Tests.Unit.Extensions;

public class StorageBridgeEnumExtensionsTests
{
    [Fact]
    public void GetAttribute_ShouldReturnAttribute_WhenPresent()
    {
        var attr = BulkLoadDataType.Locations.GetAttribute<TableNameAttribute>();

        attr.Should().NotBeNull();
        attr!.Name.Should().Be("ct_locations");
        attr.Key.Should().Be("loc_id");
        attr.Schema.Should().Be(SchemaName.Cts);
    }

    [Fact]
    public void GetAttribute_ShouldReturnNull_WhenAttributeMissing()
    {
        var attr = BulkLoadDataType.None.GetAttribute<TableNameAttribute>();

        attr.Should().BeNull();
    }

    [Fact]
    public void GetTableName_ShouldReturnCorrectName()
    {
        var name = BulkLoadDataType.Locations.GetTableName();

        name.Should().Be("ct_locations");
    }

    [Fact]
    public void GetTableKey_ShouldReturnCorrectKey()
    {
        var key = BulkLoadDataType.Locations.GetTableKey();

        key.Should().Be("loc_id");
    }

    [Fact]
    public void GetTableSchema_ShouldReturnCorrectSchema()
    {
        var schema = BulkLoadDataType.Locations.GetTableSchema();

        schema.Should().Be(SchemaName.Cts);
    }

    [Fact]
    public void GetTableSchema_ShouldReturnNull_WhenNoAttribute()
    {
        var schema = BulkLoadDataType.None.GetTableSchema();

        schema.Should().Be(SchemaName.Public);
    }

    [Fact]
    public void GetTableName_ShouldReturnNull_WhenNoAttribute()
    {
        var name = BulkLoadDataType.None.GetTableName();

        name.Should().BeNull();
    }

    [Fact]
    public void GetTableKey_ShouldReturnNull_WhenNoAttribute()
    {
        var key = BulkLoadDataType.None.GetTableKey();

        key.Should().BeNull();
    }
}