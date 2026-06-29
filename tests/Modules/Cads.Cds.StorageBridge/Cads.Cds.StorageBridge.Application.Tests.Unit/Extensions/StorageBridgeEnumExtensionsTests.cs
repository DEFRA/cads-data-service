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
        var attrs = ImportDataType.CtLocations.GetAttributes<TableInfoAttribute>()?.ToArray();

        attrs.Should().NotBeNull();
        attrs[0]!.Name.Should().Be("ct_locations");
        attrs[0].PrimaryKey.Should().Be("loc_id");
        attrs[0].Schema.Should().Be(SchemaName.Cts);
    }

    [Fact]
    public void GetAttribute_ShouldReturnNull_WhenAttributeMissing()
    {
        var attr = ImportDataType.None.GetAttribute<TableInfoAttribute>();

        attr.Should().BeNull();
    }

    [Fact]
    public void GetTableName_ShouldReturnCorrectName()
    {
        var name = ImportDataType.CtLocations.GetTableName(SchemaName.Cts);

        name.Should().Be("ct_locations");
    }

    [Fact]
    public void GetTableKey_ShouldReturnCorrectKey()
    {
        var key = ImportDataType.CtLocations.GetTableKey(SchemaName.Cts);

        key.Should().Be("loc_id");
    }

    [Fact]
    public void GetTableName_ShouldReturnNull_WhenNoAttribute()
    {
        var name = ImportDataType.None.GetTableName(SchemaName.Cts);

        name.Should().BeNull();
    }

    [Fact]
    public void GetTableKey_ShouldReturnNull_WhenNoAttribute()
    {
        var key = ImportDataType.None.GetTableKey(SchemaName.Cts);

        key.Should().BeNull();
    }
}