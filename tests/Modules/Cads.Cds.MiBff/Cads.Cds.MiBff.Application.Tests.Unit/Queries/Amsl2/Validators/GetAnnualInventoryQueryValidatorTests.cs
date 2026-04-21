using Cads.Cds.MiBff.Application.Queries.Amsl2.AnnualInventory;
using Cads.Cds.MiBff.Application.Queries.Amsl2.AnnualInventory.Validators;
using FluentValidation.TestHelper;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Queries.Amsl2.Validators;

public class GetAnnualInventoryQueryValidatorTests
{
    private readonly GetAnnualInventoryQueryValidator _sut = new();

    [Fact]
    public void Page_WhenZeroOrNegative_ShouldHaveValidationError()
    {
        var model = new GetAnnualInventoryQuery { Page = 0 };

        var result = _sut.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Page);
    }

    [Fact]
    public void Page_WhenPositive_ShouldBeValid()
    {
        var model = new GetAnnualInventoryQuery { Page = 1 };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Page);
    }

    [Fact]
    public void PageSize_WhenZeroOrNegative_ShouldHaveValidationError()
    {
        var model = new GetAnnualInventoryQuery { PageSize = 0 };

        var result = _sut.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PageSize);
    }

    [Fact]
    public void PageSize_WhenPositive_ShouldBeValid()
    {
        var model = new GetAnnualInventoryQuery { PageSize = 10 };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PageSize);
    }

    [Theory]
    [InlineData("asc")]
    [InlineData("desc")]
    public void Sort_WhenValid_ShouldNotHaveValidationError(string sort)
    {
        var model = new GetAnnualInventoryQuery { Sort = sort, Order = "name" };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Sort);
    }

    [Theory]
    [InlineData("ascending")]
    [InlineData("DESCENDING")]
    [InlineData("invalid")]
    public void Sort_WhenInvalid_ShouldHaveValidationError(string sort)
    {
        var model = new GetAnnualInventoryQuery { Sort = sort, Order = "name" };

        var result = _sut.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Sort);
    }

    [Fact]
    public void Order_WhenSortProvidedAndOrderMissing_ShouldHaveValidationError()
    {
        var model = new GetAnnualInventoryQuery { Sort = "asc", Order = null };

        var result = _sut.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Order);
    }

    [Fact]
    public void Order_WhenSortProvidedAndOrderPresent_ShouldBeValid()
    {
        var model = new GetAnnualInventoryQuery { Sort = "asc", Order = "name" };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Order);
    }

    [Fact]
    public void Order_WhenSortNotProvided_ShouldNotBeValidated()
    {
        var model = new GetAnnualInventoryQuery { Sort = null, Order = null };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Order);
    }
}