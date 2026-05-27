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
        var attr = BulkLoadDataTypes.Locations.GetAttribute<TableNameAttribute>();

        attr.Should().NotBeNull();
        attr!.Name.Should().Be("_ct_locations");
        attr.Key.Should().Be("loc_id");
    }

    [Fact]
    public void GetAttribute_ShouldReturnNull_WhenAttributeMissing()
    {
        var attr = BulkLoadDataTypes.None.GetAttribute<TableNameAttribute>();

        attr.Should().BeNull();
    }

    [Fact]
    public void GetTableName_ShouldReturnCorrectName()
    {
        var name = BulkLoadDataTypes.Locations.GetTableName();

        name.Should().Be("_ct_locations");
    }

    [Fact]
    public void GetTableKey_ShouldReturnCorrectKey()
    {
        var key = BulkLoadDataTypes.Locations.GetTableKey();

        key.Should().Be("loc_id");
    }

    [Fact]
    public void GetTableName_ShouldReturnNull_WhenNoAttribute()
    {
        var name = BulkLoadDataTypes.None.GetTableName();

        name.Should().BeNull();
    }

    [Fact]
    public void GetTableKey_ShouldReturnNull_WhenNoAttribute()
    {
        var key = BulkLoadDataTypes.None.GetTableKey();

        key.Should().BeNull();
    }
}