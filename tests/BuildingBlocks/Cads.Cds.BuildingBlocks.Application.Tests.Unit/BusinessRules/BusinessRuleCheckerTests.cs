using Cads.Cds.BuildingBlocks.Application.BusinessRules;
using Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;
using FluentAssertions;
using Moq;

namespace Cads.Cds.BuildingBlocks.Application.Tests.Unit.BusinessRules;

public class BusinessRuleCheckerTests
{
    [Fact]
    public void ShouldReturnTrueWhenAllRulesAreValid()
    {
        // Arrange
        var validRule1 = new Mock<IBusinessRule>();
        validRule1.Setup(r => r.IsBroken()).Returns(false);
        validRule1.Setup(r => r.Message).Returns("Valid Rule 1");

        var validRule2 = new Mock<IBusinessRule>();
        validRule2.Setup(r => r.IsBroken()).Returns(false);
        validRule2.Setup(r => r.Message).Returns("Valid Rule 2");

        // Act
        Action act = () => BusinessRuleChecker.CheckRule(validRule1.Object, validRule2.Object);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ShouldThrowFirstExceptionWhenAnyRuleIsBroken()
    {
        // Arrange
        var validRule = new Mock<IBusinessRule>();
        validRule.Setup(r => r.IsBroken()).Returns(false);
        validRule.Setup(r => r.Message).Returns("Valid Rule");

        var brokenRule1 = new Mock<IBusinessRule>();
        brokenRule1.Setup(r => r.IsBroken()).Returns(true);
        brokenRule1.Setup(r => r.Message).Returns("Broken Rule 1");

        var brokenRule2 = new Mock<IBusinessRule>();
        brokenRule2.Setup(r => r.IsBroken()).Returns(true);
        brokenRule2.Setup(r => r.Message).Returns("Broken Rule 2");

        // Act
        Action act = () => BusinessRuleChecker.CheckRule(validRule.Object, brokenRule1.Object, brokenRule2.Object);

        // Assert
        act.Should().Throw<BusinessRuleValidationException>().WithMessage("Broken Rule 1");
    }
}