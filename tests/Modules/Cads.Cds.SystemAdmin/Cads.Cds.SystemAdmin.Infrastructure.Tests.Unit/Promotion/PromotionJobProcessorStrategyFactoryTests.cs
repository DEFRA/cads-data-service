using Cads.Cds.SystemAdmin.Application.Promotion.Factories;
using Cads.Cds.SystemAdmin.Core.Promotion;
using Cads.Cds.SystemAdmin.Infrastructure.Promotion.Factories;
using FluentAssertions;
using Moq;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Promotion;

public class PromotionJobProcessorStrategyFactoryTests
{
    private static Mock<IPromotionJobProcessorStrategy> StrategyFor(PromotionType type)
    {
        var mock = new Mock<IPromotionJobProcessorStrategy>();
        mock.SetupGet(x => x.PromotionType).Returns(type);
        return mock;
    }

    [Theory]
    [InlineData(PromotionType.CtsBulk)]
    [InlineData(PromotionType.CtsDelta)]
    public void GivenRegisteredType_WhenGetStrategy_ShouldReturnMatchingStrategy(PromotionType type)
    {
        var bulk = StrategyFor(PromotionType.CtsBulk).Object;
        var delta = StrategyFor(PromotionType.CtsDelta).Object;
        var sut = new PromotionJobProcessorStrategyFactory([bulk, delta]);

        var result = sut.GetStrategy(type);

        result.PromotionType.Should().Be(type);
        result.Should().BeSameAs(type == PromotionType.CtsBulk ? bulk : delta);
    }

    [Fact]
    public void GivenUnregisteredType_WhenGetStrategy_ShouldThrowNamingTheType()
    {
        var sut = new PromotionJobProcessorStrategyFactory([StrategyFor(PromotionType.CtsBulk).Object]);

        var act = () => sut.GetStrategy(PromotionType.CtsDelta);

        act.Should().Throw<InvalidOperationException>().WithMessage("No strategy found for PromotionType*");
    }
}