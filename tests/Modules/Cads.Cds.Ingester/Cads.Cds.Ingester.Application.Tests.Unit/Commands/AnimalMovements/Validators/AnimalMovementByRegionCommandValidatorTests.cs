using Cads.Cds.Ingester.Application.Commands.AnimalMovements;
using Cads.Cds.Ingester.Application.Commands.AnimalMovements.Validators;
using Cads.Cds.Ingester.Core.Domain.Enums;
using FluentValidation.TestHelper;

namespace Cads.Cds.Ingester.Application.Tests.Unit.Commands.AnimalMovements.Validators;

public class AnimalMovementByRegionCommandValidatorTests
{
    private readonly AnimalMovementByRegionCommandValidator _sut = new();

    [Fact]
    public void Region_WithValue_ShouldHaveBeValid()
    {
        var model = new AnimalMovementByRegionCommand(Region.Wales, "json");

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Region);
    }
    
    [Fact]
    public void Payload_WhenNull_ShouldHaveValidationError()
    {
        var model = new AnimalMovementByRegionCommand(Region.Wales, null);

        var result = _sut.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Payload);
    }

    [Fact]
    public void Payload_WithValue_ShouldHaveBeValid()
    {
        var model = new AnimalMovementByRegionCommand(Region.Wales, "json");

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Payload);
    }
}