using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Models;
using Cads.Cds.StorageBridge.Testing.Support.Fakes.Factories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Npgsql;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.S3Import.Models;

public class ImportExecutionContextTests
{
    private static readonly Mock<StorageBridgeWriteDbContext> s_dbContext = new Mock<StorageBridgeWriteDbContext>(new DbContextOptions<StorageBridgeWriteDbContext>());
    private static readonly IS3ImportCommandFactory s_s3ImportCommandFactory = GetFactory();
    private static readonly Mock<IDefensiveCopyLineNormaliserService> s_defensiveCopyLineNormaliser = new Mock<IDefensiveCopyLineNormaliserService>();
    private static TestableS3BulkLoadCommandFactory GetFactory() =>
        new(new NpgsqlConnection("Host=cads-postgres;Port=5432;Database=cads_data_service;Username=postgres;Password=postgres"), LocationsHeader.Split('|'));

    private static string LocationsHeader =>
        "record_type|record_count|loc_id";

    [Fact]
    public async Task CreateAsync_ShouldReturnImportExecutionContext()
    {
        // Arrange
        var fileImport = new FileImport
        {
            Id = 100,
            FileName = "CTSM_CADS_PROD_BULK_00001_001_CT_SUSPENSE_WG_ALLOC_RULES_2026-08-22-072826.csv"
        };
        char delimiter = ',';

        // Act
        var result = await ImportExecutionContext.CreateAsync(
            fileImport,
            s_dbContext.Object,
            delimiter,
            s_s3ImportCommandFactory,
            s_defensiveCopyLineNormaliser.Object,
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(fileImport, result.FileImport);
        Assert.Equal(delimiter, result.Delimiter);
        Assert.NotNull(result.ImportParameters);
        Assert.NotNull(result.Factory);
        Assert.NotNull(result.DefensiveCopyLineNormaliserService);
        Assert.NotNull(result.DbContext);
        Assert.NotNull(result.CreateTempTableCommand);
        Assert.NotNull(result.ActionCommands);
    }
}