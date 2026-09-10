using Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;
using FluentAssertions;

namespace Cads.Cds.Api.Application.Tests.Unit.Queries.Bovine;

public class GetAnimalDetailsByIdentifierValidatorTests
{
    [Theory]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("UK324537113234", true)]
    public void ShouldValidateIdentifierCorrectly(string identifier, bool expectedIsValid)
    {
        var sut = new GetAnimalDetailsByIdentifierValidator();

        var result = sut.Validate(new GetAnimalDetailsByIdentifier { Identifier = identifier });

        result.IsValid.Should().Be(expectedIsValid);
    }
}
