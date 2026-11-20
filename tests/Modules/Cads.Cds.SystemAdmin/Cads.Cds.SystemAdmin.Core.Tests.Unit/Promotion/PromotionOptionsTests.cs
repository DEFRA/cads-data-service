using Cads.Cds.SystemAdmin.Core.Promotion;
using FluentAssertions;

namespace Cads.Cds.SystemAdmin.Core.Tests.Unit.Promotion;

public class PromotionOptionsTests
{
    public static TheoryData<PromotionType> AllPromotionTypes => new(Enum.GetValues<PromotionType>());

    private static PromotionOptions CreateOptions(params (string Key, bool Value)[] entries) =>
        new() { PromotionTypes = entries.ToDictionary(e => e.Key, e => e.Value) };

    // ---- IsEnabled ----

    [Theory]
    [MemberData(nameof(AllPromotionTypes))]
    public void GivenTypeConfiguredTrue_WhenIsEnabled_ShouldEnableOnlyThatType(PromotionType type)
    {
        var sut = CreateOptions((type.ToString(), true));

        sut.IsEnabled(type).Should().BeTrue();

        foreach (var other in Enum.GetValues<PromotionType>().Where(t => t != type))
        {
            sut.IsEnabled(other).Should().BeFalse($"{other} is not configured");
        }
    }

    [Theory]
    [MemberData(nameof(AllPromotionTypes))]
    public void GivenTypeConfiguredFalse_WhenIsEnabled_ShouldReturnFalse(PromotionType type)
    {
        var sut = CreateOptions((type.ToString(), false));

        sut.IsEnabled(type).Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(AllPromotionTypes))]
    public void GivenTypeNotConfigured_WhenIsEnabled_ShouldReturnFalse(PromotionType type)
    {
        var sut = CreateOptions(("SomethingElse", true));

        sut.IsEnabled(type).Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(AllPromotionTypes))]
    public void GivenNoTypesConfigured_WhenIsEnabled_ShouldReturnFalse(PromotionType type)
    {
        var sut = CreateOptions();

        sut.IsEnabled(type).Should().BeFalse();
    }

    [Fact]
    public void GivenDifferentCasingInKey_WhenIsEnabled_ShouldNotMatch()
    {
        // Documents current behaviour: lookup uses the enum name and the dictionary's
        // default (case-sensitive) comparer.
        var sut = CreateOptions(("ctsbulk", true), ("CTSDELTA", true));

        sut.IsEnabled(PromotionType.CtsBulk).Should().BeFalse();
        sut.IsEnabled(PromotionType.CtsDelta).Should().BeFalse();
    }

    [Fact]
    public void GivenUndefinedEnumValue_WhenIsEnabled_ShouldReturnFalse()
    {
        var sut = CreateOptions(("CtsBulk", true), ("CtsDelta", true));

        sut.IsEnabled((PromotionType)999).Should().BeFalse();
    }

    // ---- GetPromotionTypes ----

    [Fact]
    public void GivenConfiguredTypes_WhenGetPromotionTypes_ShouldReturnSameEntries()
    {
        var sut = CreateOptions(("CtsBulk", true), ("CtsDelta", false));

        var result = sut.GetPromotionTypes();

        result.Should().HaveCount(2);
        result["CtsBulk"].Should().BeTrue();
        result["CtsDelta"].Should().BeFalse();
    }

    [Fact]
    public void GivenNoTypes_WhenGetPromotionTypes_ShouldReturnEmpty()
    {
        var sut = CreateOptions();

        sut.GetPromotionTypes().Should().BeEmpty();
    }

    [Fact]
    public void GivenConfiguredTypes_WhenGetPromotionTypes_ShouldReturnACopy()
    {
        var sut = CreateOptions(("CtsBulk", true));

        var result = sut.GetPromotionTypes();
        sut.PromotionTypes["CtsBulk"] = false;
        sut.PromotionTypes["CtsDelta"] = true;

        result.Should().HaveCount(1);
        result["CtsBulk"].Should().BeTrue("the returned dictionary is a snapshot");
        result.Should().NotContainKey("CtsDelta");
    }
}