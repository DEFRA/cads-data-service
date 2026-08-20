using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Core.Tests.Unit.Domain.Imports;

public class FileImportTests
{
    [Fact]
    public void SetImportStatus_WhenTransitioningToFailed_ThrowsInvalidOperationException()
    {
        // Arrange
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Pending
        };

        // Act
        Action act = () => fileImport.SetImportStatus(FileImportStatus.Failed);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Invalid import status transition from Pending to Failed. Use MarkFailed(reason) instead.");
    }

    [Fact]
    public void SetImportStatus_WhenAlreadyFailed_DoesNotThrow()
    {
        // Arrange - guarded by the early "same status" return before the Failed check.
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Failed
        };

        // Act
        Action act = () => fileImport.SetImportStatus(FileImportStatus.Failed);

        // Assert
        act.Should().NotThrow();
        fileImport.ImportStatus.Should().Be(FileImportStatus.Failed);
    }
}