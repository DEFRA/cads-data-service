using Cads.Cds.SystemAdmin.Endpoints.Generation.Requests;
using Cads.Cds.SystemAdmin.Endpoints.Generation.Validators;
using FluentAssertions;

namespace Cads.Cds.SystemAdmin.Tests.Component.Validators;

public class CreateGenerationRequestValidatorTests
{
    private readonly CreateGenerationRequestValidator _sut = new();

    [Fact]
    public void GivenBusinessKeysWithoutRowCount_WhenValidated_ShouldBeValid()
    {
        var request = new CreateGenerationRequest
        {
            Scenario = "test-scenario",
            BusinessKeys = [1m]
        };

        var result = _sut.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void GivenRowCountWithoutBusinessKeys_WhenValidated_ShouldBeValid()
    {
        var request = new CreateGenerationRequest
        {
            Scenario = "test-scenario",
            RowCount = 1
        };

        var result = _sut.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void GivenBusinessKeysAndRowCount_WhenValidated_ShouldBeInvalid()
    {
        var request = new CreateGenerationRequest
        {
            Scenario = "test-scenario",
            BusinessKeys = [1m],
            RowCount = 1
        };

        var result = _sut.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void GivenNeitherBusinessKeysNorRowCount_WhenValidated_ShouldBeInvalid()
    {
        var request = new CreateGenerationRequest
        {
            Scenario = "test-scenario"
        };

        var result = _sut.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void GivenNonPositiveRowCountWithoutBusinessKeys_WhenValidated_ShouldBeInvalid(int rowCount)
    {
        var request = new CreateGenerationRequest
        {
            Scenario = "test-scenario",
            RowCount = rowCount
        };

        var result = _sut.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == "RowCount");
    }
}