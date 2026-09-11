using Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnCph;
using FluentAssertions;

namespace Cads.Cds.Api.Application.Tests.Unit.Queries.Bovine;

public class GetAnimalsOnCphValidatorTests
{
    [Theory]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("08/065/0077", true)]
    public void ShouldValidateCphCorrectly(string cph, bool expectedIsValid)
    {
        var sut = new GetAnimalsOnCphValidator();

        var result = sut.Validate(new GetAnimalsOnCph { Cph = cph });

        result.IsValid.Should().Be(expectedIsValid);
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    [InlineData(1, true)]
    public void ShouldValidatePageCorrectly(int page, bool expectedIsValid)
    {
        var sut = new GetAnimalsOnCphValidator();

        var result = sut.Validate(new GetAnimalsOnCph { Cph = "08/065/0077", Page = page });

        result.IsValid.Should().Be(expectedIsValid);
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    [InlineData(25, true)]
    public void ShouldValidatePageSizeCorrectly(int pageSize, bool expectedIsValid)
    {
        var sut = new GetAnimalsOnCphValidator();

        var result = sut.Validate(new GetAnimalsOnCph { Cph = "08/065/0077", PageSize = pageSize });

        result.IsValid.Should().Be(expectedIsValid);
    }
}
