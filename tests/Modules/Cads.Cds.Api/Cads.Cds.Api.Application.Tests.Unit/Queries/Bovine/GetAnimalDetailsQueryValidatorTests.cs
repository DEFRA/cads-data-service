using Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;
using FluentAssertions;

namespace Cads.Cds.Api.Application.Tests.Unit.Queries.Bovine;

public class GetAnimalDetailsQueryValidatorTests
{
    [Theory]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("UK324537113234", true)]
    public void ShouldValidateIdentifierCorrectly(string identifier, bool expectedIsValid)
    {
        var sut = new GetAnimalDetailsQueryValidator();

        var result = sut.Validate(new GetAnimalDetailsQuery { Identifier = identifier });

        result.IsValid.Should().Be(expectedIsValid);
    }
}
