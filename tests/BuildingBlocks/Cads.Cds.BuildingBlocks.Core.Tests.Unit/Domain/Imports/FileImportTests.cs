using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Core.Tests.Unit.Domain.Imports;

public class FileImportTests
{
    // ---------------------------------------------------------
    // SetImportStatus
    // ---------------------------------------------------------

    [Fact]
    public void SetImportStatus_WhenTransitioningToFailed_ThrowsInvalidOperationException()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Pending
        };

        Action act = () => fileImport.SetImportStatus(FileImportStatus.Failed);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Invalid import status transition from Pending to Failed. Use MarkFailed(reason) instead.");
    }

    [Fact]
    public void SetImportStatus_WhenSameStatus_DoesNothing()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Split
        };

        Action act = () => fileImport.SetImportStatus(FileImportStatus.Split);

        act.Should().NotThrow();
        fileImport.ImportStatus.Should().Be(FileImportStatus.Split);
    }

    [Fact]
    public void SetImportStatus_WhenTransferred_InvokesMarkTransferred()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Pending
        };

        fileImport.SetImportStatus(FileImportStatus.Transferred);

        fileImport.ImportStatus.Should().Be(FileImportStatus.Transferred);
        fileImport.ImportStartAt.Should().NotBeNull();
    }

    [Fact]
    public void SetImportStatus_WhenSplit_InvokesMarkSplit()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Transferred
        };

        fileImport.SetImportStatus(FileImportStatus.Split);

        fileImport.ImportStatus.Should().Be(FileImportStatus.Split);
    }

    [Fact]
    public void SetImportStatus_WhenCompleted_InvokesMarkCompleted()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Split
        };

        fileImport.SetImportStatus(FileImportStatus.Completed);

        fileImport.ImportStatus.Should().Be(FileImportStatus.Completed);
        fileImport.ImportEndAt.Should().NotBeNull();
    }

    // ---------------------------------------------------------
    // MarkTransferred
    // ---------------------------------------------------------

    [Fact]
    public void MarkTransferred_FromPending_SetsStatusAndStartTime()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Pending
        };

        fileImport.MarkTransferred();

        fileImport.ImportStatus.Should().Be(FileImportStatus.Transferred);
        fileImport.ImportStartAt.Should().NotBeNull();
    }

    [Fact]
    public void MarkTransferred_FromInvalidState_ThrowsBusinessRuleValidationException()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Completed
        };

        Action act = () => fileImport.MarkTransferred();

        act.Should().Throw<BusinessRuleValidationException>();
    }

    // ---------------------------------------------------------
    // MarkSplit
    // ---------------------------------------------------------

    [Fact]
    public void MarkSplit_FromTransferred_SetsSplit()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Transferred
        };

        fileImport.MarkSplit();

        fileImport.ImportStatus.Should().Be(FileImportStatus.Split);
    }

    [Fact]
    public void MarkSplit_FromInvalidState_ThrowsBusinessRuleValidationException()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Pending
        };

        Action act = () => fileImport.MarkSplit();

        act.Should().Throw<BusinessRuleValidationException>();
    }

    // ---------------------------------------------------------
    // MarkCompleted
    // ---------------------------------------------------------

    [Fact]
    public void MarkCompleted_FromSplit_SetsCompletedAndEndTime()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Split
        };

        fileImport.MarkCompleted();

        fileImport.ImportStatus.Should().Be(FileImportStatus.Completed);
        fileImport.ImportEndAt.Should().NotBeNull();
    }

    [Fact]
    public void MarkCompleted_FromInvalidState_ThrowsBusinessRuleValidationException()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Pending
        };

        Action act = () => fileImport.MarkCompleted();

        act.Should().Throw<BusinessRuleValidationException>();
    }

    // ---------------------------------------------------------
    // MarkFailed
    // ---------------------------------------------------------

    [Fact]
    public void MarkFailed_SetsStatusAndTimesAndReason()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Transferred
        };

        fileImport.MarkFailed("boom");

        fileImport.ImportStatus.Should().Be(FileImportStatus.Failed);
        fileImport.ImportStartAt.Should().NotBeNull();
        fileImport.ImportEndAt.Should().NotBeNull();
        fileImport.LastErrorReason.Should().Be("boom");
        fileImport.FailedAttempts.Should().Be(1);
    }

    [Fact]
    public void MarkFailed_NonTransient_SetsFailedAttemptsToThree()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Split
        };

        fileImport.MarkFailed("boom", isTransient: false);

        fileImport.FailedAttempts.Should().Be(3);
    }

    // ---------------------------------------------------------
    // Processing workflow
    // ---------------------------------------------------------

    [Fact]
    public void MarkProcessingStarted_FromPending_SetsProcessing()
    {
        var fileImport = new FileImport
        {
            ProcessingStatus = FileProcessingStatus.Pending
        };

        fileImport.MarkProcessingStarted();

        fileImport.ProcessingStatus.Should().Be(FileProcessingStatus.Processing);
        fileImport.ProcessingStartAt.Should().NotBeNull();
    }

    [Fact]
    public void MarkProcessingStarted_FromInvalidState_ThrowsDomainException()
    {
        var fileImport = new FileImport
        {
            ProcessingStatus = FileProcessingStatus.Complete
        };

        Action act = () => fileImport.MarkProcessingStarted();

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void MarkProcessingComplete_FromProcessing_SetsComplete()
    {
        var fileImport = new FileImport
        {
            ProcessingStatus = FileProcessingStatus.Processing
        };

        fileImport.MarkProcessingComplete();

        fileImport.ProcessingStatus.Should().Be(FileProcessingStatus.Complete);
        fileImport.ProcessingEndAt.Should().NotBeNull();
    }

    [Fact]
    public void MarkProcessingComplete_FromInvalidState_ThrowsDomainException()
    {
        var fileImport = new FileImport
        {
            ProcessingStatus = FileProcessingStatus.Pending
        };

        Action act = () => fileImport.MarkProcessingComplete();

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void MarkProcessingFailed_FromProcessing_SetsFailed()
    {
        var fileImport = new FileImport
        {
            ProcessingStatus = FileProcessingStatus.Processing
        };

        fileImport.MarkProcessingFailed();

        fileImport.ProcessingStatus.Should().Be(FileProcessingStatus.Failed);
        fileImport.ProcessingEndAt.Should().NotBeNull();
    }

    [Fact]
    public void MarkProcessingFailed_FromInvalidState_ThrowsDomainException()
    {
        var fileImport = new FileImport
        {
            ProcessingStatus = FileProcessingStatus.Pending
        };

        Action act = () => fileImport.MarkProcessingFailed();

        act.Should().Throw<DomainException>();
    }

    // ---------------------------------------------------------
    // Replay workflow
    // ---------------------------------------------------------

    [Fact]
    public void ResetForReplay_ResetsAllFields()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Completed,
            ProcessingStatus = FileProcessingStatus.Complete,
            RowsFound = 10,
            ImportStartAt = DateTimeOffset.UtcNow,
            ImportEndAt = DateTimeOffset.UtcNow,
            ProcessingStartAt = DateTimeOffset.UtcNow,
            ProcessingEndAt = DateTimeOffset.UtcNow
        };

        fileImport.ResetForReplay();

        fileImport.ImportStatus.Should().Be(FileImportStatus.Pending);
        fileImport.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);
        fileImport.RowsFound.Should().Be(0);
        fileImport.ImportStartAt.Should().BeNull();
        fileImport.ImportEndAt.Should().BeNull();
        fileImport.ProcessingStartAt.Should().BeNull();
        fileImport.ProcessingEndAt.Should().BeNull();
    }

    [Fact]
    public void ForceResetImportStatus_SetsImportStatusAndResetsProcessing()
    {
        var fileImport = new FileImport
        {
            ImportStatus = FileImportStatus.Completed,
            ProcessingStatus = FileProcessingStatus.Complete
        };

        fileImport.ForceResetImportStatus(FileImportStatus.Split);

        fileImport.ImportStatus.Should().Be(FileImportStatus.Split);
        fileImport.ProcessingStatus.Should().Be(FileProcessingStatus.Pending);
    }
}