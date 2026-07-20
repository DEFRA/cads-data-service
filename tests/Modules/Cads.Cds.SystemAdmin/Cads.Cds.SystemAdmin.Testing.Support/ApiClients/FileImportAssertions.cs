using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.SystemAdmin.Core.DTOs.Imports;
using FluentAssertions;

namespace Cads.Cds.SystemAdmin.Testing.Support.ApiClients;

public static class FileImportAssertions
{
    public static void ShouldBeTotalRowsToProcess(FileImportDto dto, long expected)
    {
        dto.TotalRowsToProcess.Should().Be(expected);
    }

    public static void ShouldBeRowsFound(FileImportDto dto, long expected)
    {
        dto.RowsFound.Should().Be(expected);
    }

    public static void ShouldBePending(FileImportDto dto)
    {
        dto.ImportStatus.Should().Be(FileImportStatus.Pending);
        dto.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);

        dto.ImportStartAt.Should().BeNull();
        dto.ImportEndAt.Should().BeNull();
    }

    public static void ShouldBeTransferred(FileImportDto dto)
    {
        dto.ImportStatus.Should().Be(FileImportStatus.Transferred);
        dto.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);

        dto.ImportStartAt.Should().NotBeNull();
        dto.ImportEndAt.Should().BeNull();
    }

    public static void ShouldBeSplit(FileImportDto dto)
    {
        dto.ImportStatus.Should().Be(FileImportStatus.Split);
        dto.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);

        dto.ImportStartAt.Should().NotBeNull();
        dto.ImportEndAt.Should().BeNull();
    }

    public static void ShouldBeUpdated(FileImportDto dto, FileImportStatus fileImportStatus)
    {
        dto.ImportStatus.Should().Be(fileImportStatus);
        dto.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);

        dto.ImportStartAt.Should().NotBeNull();
        dto.ImportEndAt.Should().BeNull();
    }


    public static void ShouldBeComplete(FileImportDto dto)
    {
        dto.ImportStatus.Should().Be(FileImportStatus.Complete);
        dto.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);

        dto.ImportStartAt.Should().NotBeNull();
        dto.ImportEndAt.Should().NotBeNull();
    }

    public static void ShouldBeFailed(FileImportDto dto)
    {
        dto.ImportStatus.Should().Be(FileImportStatus.Failed);
        dto.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);

        dto.ImportStartAt.Should().NotBeNull();
        dto.ImportEndAt.Should().NotBeNull();
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