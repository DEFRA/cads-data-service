using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.SystemAdmin.Application.Imports.Mappings;

namespace Cads.Cds.SystemAdmin.Application.Tests.Unit.TestFixtures;

public class FileImportMapperTests
{
    [Fact]
    public void MapToDto_MapsAllProperties()
    {
        // Arrange
        var addedAt = new DateTimeOffset(2024, 01, 01, 12, 0, 0, TimeSpan.Zero);
        var importStart = new DateTimeOffset(2024, 01, 02, 12, 0, 0, TimeSpan.Zero);
        var importEnd = new DateTimeOffset(2024, 01, 03, 12, 0, 0, TimeSpan.Zero);
        var processingStart = new DateTimeOffset(2024, 01, 04, 12, 0, 0, TimeSpan.Zero);
        var processingEnd = new DateTimeOffset(2024, 01, 05, 12, 0, 0, TimeSpan.Zero);

        var fileImport = new FileImport
        {
            Id = 123,
            DestinationTableName = "DEST_TABLE",
            FileName = "file.csv",
            GroupKey = "group-1",
            TotalRowsToProcess = 1000,
            RowsFound = 950,
            ImportStatus = FileImportStatus.Transferred,
            ProcessingStatus = FileProcessingStatus.Processing,
            AddedAt = addedAt,
            ImportStartAt = importStart,
            ImportEndAt = importEnd,
            ProcessingStartAt = processingStart,
            ProcessingEndAt = processingEnd,
            FailedAttempts = 2,
            LastErrorReason = "Some error"
        };

        // Act
        var dto = fileImport.MapToDto();

        // Assert
        Assert.Equal(fileImport.Id, dto.Id);
        Assert.Equal(fileImport.DestinationTableName, dto.DestinationTableName);
        Assert.Equal(fileImport.FileName, dto.FileName);
        Assert.Equal(fileImport.GroupKey, dto.GroupKey);
        Assert.Equal(fileImport.TotalRowsToProcess, dto.TotalRowsToProcess);
        Assert.Equal(fileImport.RowsFound, dto.RowsFound);
        Assert.Equal(fileImport.ImportStatus, dto.ImportStatus);
        Assert.Equal(fileImport.ProcessingStatus, dto.ProcessingStatus);
        Assert.Equal(fileImport.AddedAt, dto.AddedAt);
        Assert.Equal(fileImport.ImportStartAt, dto.ImportStartAt);
        Assert.Equal(fileImport.ImportEndAt, dto.ImportEndAt);
        Assert.Equal(fileImport.ProcessingStartAt, dto.ProcessingStartAt);
        Assert.Equal(fileImport.ProcessingEndAt, dto.ProcessingEndAt);
        Assert.Equal(fileImport.FailedAttempts, dto.FailedAttempts);
        Assert.Equal(fileImport.LastErrorReason, dto.LastErrorReason);
    }

    [Fact]
    public void MapToDto_ForEnumerable_MapsAllItems()
    {
        // Arrange
        var now = DateTimeOffset.UtcNow;

        var f1 = new FileImport
        {
            Id = 1,
            DestinationTableName = "A",
            FileName = "a.csv",
            TotalRowsToProcess = 10,
            RowsFound = 9,
            ImportStatus = FileImportStatus.Pending,
            ProcessingStatus = FileProcessingStatus.Pending,
            AddedAt = now
        };

        var f2 = new FileImport
        {
            Id = 2,
            DestinationTableName = "B",
            FileName = "b.csv",
            TotalRowsToProcess = 20,
            RowsFound = 20,
            ImportStatus = FileImportStatus.Completed,
            ProcessingStatus = FileProcessingStatus.Complete,
            AddedAt = now
        };

        var list = new[] { f1, f2 };

        // Act
        var dtos = list.MapToDto().ToList();

        // Assert
        Assert.Equal(2, dtos.Count);

        Assert.Equal(f1.Id, dtos[0].Id);
        Assert.Equal(f1.DestinationTableName, dtos[0].DestinationTableName);
        Assert.Equal(f1.FileName, dtos[0].FileName);

        Assert.Equal(f2.Id, dtos[1].Id);
        Assert.Equal(f2.DestinationTableName, dtos[1].DestinationTableName);
        Assert.Equal(f2.FileName, dtos[1].FileName);
    }
}