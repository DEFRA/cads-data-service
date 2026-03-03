using Cads.Cds.MiBff.Application.Queries.DataQuality;
using FluentValidation.TestHelper;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Queries.Validators;

public class GetDataQualityUnregisteredQueryValidatorTests
{
    private readonly GetDataQualityUnregisteredQueryValidator _sut = new();

    [Fact]
    public void Page_WhenZeroOrNegative_ShouldHaveValidationError()
    {
        var model = new GetDataQualityUnregisteredQuery { Page = 0 };

        var result = _sut.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Page);
    }

    [Fact]
    public void Page_WhenPositive_ShouldBeValid()
    {
        var model = new GetDataQualityUnregisteredQuery { Page = 1 };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Page);
    }

    [Fact]
    public void PageSize_WhenZeroOrNegative_ShouldHaveValidationError()
    {
        var model = new GetDataQualityUnregisteredQuery { PageSize = 0 };

        var result = _sut.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PageSize);
    }

    [Fact]
    public void PageSize_WhenPositive_ShouldBeValid()
    {
        var model = new GetDataQualityUnregisteredQuery { PageSize = 10 };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PageSize);
    }

    [Theory]
    [InlineData("asc")]
    [InlineData("desc")]
    public void Sort_WhenValid_ShouldNotHaveValidationError(string sort)
    {
        var model = new GetDataQualityUnregisteredQuery { Sort = sort, Order = "name" };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Sort);
    }

    [Theory]
    [InlineData("ascending")]
    [InlineData("DESCENDING")]
    [InlineData("invalid")]
    public void Sort_WhenInvalid_ShouldHaveValidationError(string sort)
    {
        var model = new GetDataQualityUnregisteredQuery { Sort = sort, Order = "name" };

        var result = _sut.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Sort);
    }

    [Fact]
    public void Order_WhenSortProvidedAndOrderMissing_ShouldHaveValidationError()
    {
        var model = new GetDataQualityUnregisteredQuery { Sort = "asc", Order = null };

        var result = _sut.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Order);
    }

    [Fact]
    public void Order_WhenSortProvidedAndOrderPresent_ShouldBeValid()
    {
        var model = new GetDataQualityUnregisteredQuery { Sort = "asc", Order = "name" };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Order);
    }

    [Fact]
    public void Order_WhenSortNotProvided_ShouldNotBeValidated()
    {
        var model = new GetDataQualityUnregisteredQuery { Sort = null, Order = null };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Order);
    }
}