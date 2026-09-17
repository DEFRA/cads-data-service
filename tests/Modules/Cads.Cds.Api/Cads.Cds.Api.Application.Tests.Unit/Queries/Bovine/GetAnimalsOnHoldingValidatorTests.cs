using Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;
using FluentAssertions;

namespace Cads.Cds.Api.Application.Tests.Unit.Queries.Bovine;

public class GetAnimalsOnHoldingValidatorTests
{
    [Theory]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("08/065/0077", true)]
    public void ShouldValidateCphCorrectly(string cph, bool expectedIsValid)
    {
        var sut = new GetAnimalsOnHoldingValidator();

        var result = sut.Validate(new GetAnimalsOnHolding { Cph = cph });

        result.IsValid.Should().Be(expectedIsValid);
    }
}