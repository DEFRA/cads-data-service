using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.CreateFileImport;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Moq;

namespace Cads.Cds.SystemAdmin.Application.Tests.Unit.TestFixtures;

public class CreateFileImportCommandHandlerTests
{
    private readonly Mock<ISystemAdminFileImportRepository> _repository = new(MockBehavior.Strict);

    public CreateFileImportCommandHandlerTests()
    {
        // Uniqueness business rule check: no existing record for the file name.
        _repository
            .Setup(r => r.GetByFileNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((FileImport?)null);

        _repository
            .Setup(r => r.AddAsync(It.IsAny<FileImport>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
    }

    private CreateFileImportCommandHandler CreateSut() => new(_repository.Object);

    [Fact]
    public async Task Handle_WhenDestinationTableNameCannotBeResolved_MarksImportAsFailed()
    {
        // Arrange - valid CTSM filename (parses + valid BULK type) but an unknown
        // table name so GetDestinationTableName() returns null.
        const string fileName = "CTSM_CADS_PREP_BULK_00001_001_CT_UNKNOWN_TABLE_2026-07-28-094638.csv";

        var command = new CreateFileImportCommand(fileName, TotalRowsToProcess: 100, RowsFound: 50);

        var sut = CreateSut();

        // Act
        var result = await sut.Handle(command, TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(FileImportStatus.Failed, result.ImportStatus);
        Assert.Equal("UNKNOWN", result.DestinationTableName);
        Assert.Equal(
            $"Import failed: Unable to determine destination table name from file name '{fileName}'",
            result.LastErrorReason);
        Assert.Equal(1, result.FailedAttempts);

        _repository.Verify(
            r => r.AddAsync(It.Is<FileImport>(f => f.ImportStatus == FileImportStatus.Failed), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WhenDestinationTableNameIsResolved_DoesNotMarkImportAsFailed()
    {
        // Arrange - valid CTSM filename that resolves to a known destination table.
        const string fileName = "CTSM_CADS_PREP_BULK_00001_001_CT_LOCATIONS_2026-07-28-094638.csv";

        var command = new CreateFileImportCommand(fileName, TotalRowsToProcess: 100, RowsFound: 50);

        var sut = CreateSut();

        // Act
        var result = await sut.Handle(command, TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(FileImportStatus.Pending, result.ImportStatus);
        Assert.Equal("cts_transactions.ct_locations", result.DestinationTableName);
        Assert.Null(result.LastErrorReason);
        Assert.Equal(0, result.FailedAttempts);

        _repository.Verify(
            r => r.AddAsync(It.IsAny<FileImport>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}