using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImports;
using Cads.Cds.SystemAdmin.Application.Imports.Utilities;

namespace Cads.Cds.SystemAdmin.Application.Tests.Unit.TestFixtures;

public class FileImportExpressionBuilderTests
{
    [Fact]
    public void CreateFilterExpression_NoFilters_ReturnsAlwaysTrue()
    {
        var query = new GetFileImportsQuery();
        var predicate = ExpressionBuilder.CreateFilterExpression(query).Compile();

        var fileImport = new FileImport
        {
            FileName = "anything.csv",
            GroupKey = "group",
            ImportStatus = FileImportStatus.Pending,
            ProcessingStatus = FileProcessingStatus.Pending
        };

        Assert.True(predicate(fileImport));
    }

    [Fact]
    public void CreateFilterExpression_FileNameFilter_MatchesOnlyWhenEqual()
    {
        var query = new GetFileImportsQuery(FileName: "data.csv");
        var predicate = ExpressionBuilder.CreateFilterExpression(query).Compile();

        var matching = new FileImport { FileName = "data.csv" };
        var nonMatching = new FileImport { FileName = "other.csv" };

        Assert.True(predicate(matching));
        Assert.False(predicate(nonMatching));
    }

    [Fact]
    public void CreateFilterExpression_GroupKeyFilter_MatchesOnlyWhenEqual()
    {
        var query = new GetFileImportsQuery(GroupKey: "G1");
        var predicate = ExpressionBuilder.CreateFilterExpression(query).Compile();

        var matching = new FileImport { GroupKey = "G1" };
        var nonMatching = new FileImport { GroupKey = "G2" };

        Assert.True(predicate(matching));
        Assert.False(predicate(nonMatching));
    }

    [Fact]
    public void CreateFilterExpression_FileImportStatusFilter_MatchesOnlyWhenEqual()
    {
        var query = new GetFileImportsQuery(FileImportStatus: FileImportStatus.Completed);
        var predicate = ExpressionBuilder.CreateFilterExpression(query).Compile();

        var matching = new FileImport { ImportStatus = FileImportStatus.Completed };
        var nonMatching = new FileImport { ImportStatus = FileImportStatus.Transferred };

        Assert.True(predicate(matching));
        Assert.False(predicate(nonMatching));
    }

    [Fact]
    public void CreateFilterExpression_FileProcessingStatusFilter_MatchesOnlyWhenEqual()
    {
        var query = new GetFileImportsQuery(FileProcessingStatus: FileProcessingStatus.Complete);
        var predicate = ExpressionBuilder.CreateFilterExpression(query).Compile();

        var matching = new FileImport { ProcessingStatus = FileProcessingStatus.Complete };
        var nonMatching = new FileImport { ProcessingStatus = FileProcessingStatus.Processing };

        Assert.True(predicate(matching));
        Assert.False(predicate(nonMatching));
    }

    [Fact]
    public void CreateFilterExpression_CombinedFilters_AllMustMatch()
    {
        var query = new GetFileImportsQuery(
            FileName: "combined.csv",
            GroupKey: "group-a",
            FileImportStatus: FileImportStatus.Completed,
            FileProcessingStatus: FileProcessingStatus.Complete);

        var predicate = ExpressionBuilder.CreateFilterExpression(query).Compile();

        var matching = new FileImport
        {
            FileName = "combined.csv",
            GroupKey = "group-a",
            ImportStatus = FileImportStatus.Completed,
            ProcessingStatus = FileProcessingStatus.Complete
        };

        var notMatchingFileName = new FileImport
        {
            FileName = "other.csv",
            GroupKey = "group-a",
            ImportStatus = FileImportStatus.Completed,
            ProcessingStatus = FileProcessingStatus.Complete
        };

        var notMatchingStatus = new FileImport
        {
            FileName = "combined.csv",
            GroupKey = "group-a",
            ImportStatus = FileImportStatus.Transferred,
            ProcessingStatus = FileProcessingStatus.Complete
        };

        Assert.True(predicate(matching));
        Assert.False(predicate(notMatchingFileName));
        Assert.False(predicate(notMatchingStatus));
    }
}
