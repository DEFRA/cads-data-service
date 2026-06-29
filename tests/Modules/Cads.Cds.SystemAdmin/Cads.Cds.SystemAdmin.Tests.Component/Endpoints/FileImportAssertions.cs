using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.SystemAdmin.Core.DTOs.Imports;
using FluentAssertions;

namespace Cads.Cds.SystemAdmin.Tests.Component.Endpoints;

public static class FileImportAssertions
{
    public static void ShouldBePending(FileImportDto dto)
    {
        dto.ImportStatus.Should().Be(FileImportStatus.Pending);
        dto.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);
    }

    public static void ShouldBeImporting(FileImportDto dto)
    {
        dto.ImportStatus.Should().Be(FileImportStatus.Importing);
        dto.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);
    }

    public static void ShouldBeComplete(FileImportDto dto)
    {
        dto.ImportStatus.Should().Be(FileImportStatus.Complete);
        dto.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);
    }

    public static void ShouldBeFailed(FileImportDto dto)
    {
        dto.ImportStatus.Should().Be(FileImportStatus.Failed);
        dto.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);
    }

    public static void ShouldBeReset(FileImportDto dto)
    {
        dto.ImportStatus.Should().Be(FileImportStatus.Pending);
        dto.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);
        dto.RowsFound.Should().Be(0);
        dto.ImportStartAt.Should().BeNull();
        dto.ImportEndAt.Should().BeNull();
        dto.ProcessingStartAt.Should().BeNull();
        dto.ProcessingEndAt.Should().BeNull();
    }
}
