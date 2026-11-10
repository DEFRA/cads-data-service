using Cads.Cds.BuildingBlocks.Application.Encryption;
using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.DTOs;
using Cads.Cds.BuildingBlocks.Infrastructure.Encryption;
using Cads.Cds.BuildingBlocks.Infrastructure.Encryption.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Encryption.Exceptions;
using Cads.Cds.BuildingBlocks.Infrastructure.Encryption.Providers;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Streams;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Encryption;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Logging;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Methods;
using Cads.Cds.StorageBridge.Application.Imports.Repositories;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Helpers;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Models;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Npgsql;
using System.Data.Common;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.S3Import.Services;

public class S3ToPostgresCopyServiceTests
{
    private readonly Mock<IServiceScopeFactory> _scopeFactory = new();
    private readonly Mock<IServiceScope> _scope = new();
    private readonly Mock<IServiceProvider> _provider = new();
    private readonly Mock<IStorageService<CadsInternalClient>> _storageService = new();
    private readonly Mock<IS3ImportCommandFactoryProvider> _factoryProvider = new();
    private readonly Mock<IS3ImportCommandFactory> _factory = new();
    private readonly Mock<IDefensiveCopyLineNormaliserService> _normaliser = new();
    private readonly Mock<StorageBridgeWriteDbContext> _dbContext = new(new DbContextOptions<StorageBridgeWriteDbContext>());
    private readonly Mock<DbCommand> _dbCommand = new();
    private readonly Mock<IReadOnlyList<DbCommand>> _actionCommands = new();
    private readonly Mock<ILogger<S3ToPostgresCopyService>> _logger = new Mock<ILogger<S3ToPostgresCopyService>>().EnableAllLogLevels();
    private readonly Mock<IStorageBridgeFileImportRepository> _fileImportRepository = new();
    private readonly Mock<IFileEncryptor> _fileEncryptorMock = new();
    private readonly Mock<IOptions<EncryptionConfig>> _encryptionOptionsMock = new();

    // Filename template:CTSM_CADS_<env>_<type>_<batchId>_<partno>_<tablename>_<YYYY-MM-DD-hhmmss>.csv
    private const string ValidTestFileName1 = "CTSM_CADS_ENV_DELTA_0001_0001_CT_LOCATIONS_2023-10-01-123456.part-0001.csv";
    private const string ValidTestFileName2 = "CTSM_CADS_ENV_BULK_0001_CT_LOCATIONS_2023-10-01-123456.part-0001.csv";
    private const string InvalidTestFileName1 = "CTSM_CADS_ENV_XXXX_0001_0001_CT_LOCATIONS_2023-10-01-123456.part-0001.csv";
    private const string InvalidTestFileName2 = "CTSM_CADS_ENV_XXXX_0001__0001_CT_LOCATIONS_2023-10-01-123456.part-0001.csv";

    private static EncryptionConfig EncryptionConfigWith(bool storageEncryptionEnabled) => new()
    {
        CurrentKeyId = "V1",
        StorageEncryptionEnabled = storageEncryptionEnabled,
        Keys = []
    };

    [Fact]
    public async Task ExecuteAsync_ShouldThrow_WhenImportTypeIsNone()
    {
        var service = CreateService();

        var fileImport = new FileImport { FileName = InvalidTestFileName1, DestinationPrefix = "import/cts/bulk", TotalRowsToProcess = 100, RowsFound = 100 };

        _fileImportRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
           .ReturnsAsync(fileImport);
        _storageService.Setup(x => x.ListKeysAsync(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(["somekey"]);

        var job = new CreateS3CsvImportJobDto
        {
            FileImportId = 1
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.ExecuteAsync(job, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrow_WhenFileNameFormatIsInvalid()
    {
        var service = CreateService();

        var fileImport = new FileImport { FileName = InvalidTestFileName1, DestinationPrefix = "import/cts/bulk", TotalRowsToProcess = 100, RowsFound = 100 };

        _fileImportRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
           .ReturnsAsync(fileImport);
        _storageService.Setup(x => x.ListKeysAsync(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(["somekey"]);

        var job = new CreateS3CsvImportJobDto
        {
            FileImportId = 1
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.ExecuteAsync(job, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrow_WhenSourceKeyMissing()
    {
        var service = CreateService();

        _fileImportRepository.Setup(x => x.GetByFileNameAsync("", It.IsAny<CancellationToken>()))
           .ReturnsAsync((FileImport?)null);

        var job = new CreateS3CsvImportJobDto
        {
            FileImportId = 1
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.ExecuteAsync(job, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnZero_WhenNoKeysFound()
    {
        var service = CreateService();
        var fileImport = new FileImport { FileName = ValidTestFileName1, DestinationPrefix = "import/cts/bulk", TotalRowsToProcess = 100, RowsFound = 100 };

        _storageService.Setup(x => x.ListKeysAsync(S3ToPostgresCopyService.GetSplitPartsPrefix(fileImport), null, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        _fileImportRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(fileImport);

        var job = new CreateS3CsvImportJobDto
        {
            FileImportId = 1,
        };

        var result = await service.ExecuteAsync(job, TestContext.Current.CancellationToken);

        result.TotalRowsProcessed.Should().Be(0);
    }

    [Theory]
    [InlineData("import/cts/bulk", "CTSM_FILE_2026-01-01-000000.csv", "import/cts/bulk/CTSM_FILE_2026-01-01-000000")]
    [InlineData("import/cts/daily/", "CTSM_FILE_2026-01-01-000000.CSV", "import/cts/daily/CTSM_FILE_2026-01-01-000000")]
    [InlineData("import", "CTSM_FILE_2026-01-01-000000.csv", "import/CTSM_FILE_2026-01-01-000000")]
    public void GetSplitPartsPrefix_ShouldCombineDestinationPrefixAndFileNameWithoutExtension(string destinationPrefix, string fileName, string expected)
    {
        var fileImport = new FileImport { FileName = fileName, DestinationPrefix = destinationPrefix };

        S3ToPostgresCopyService.GetSplitPartsPrefix(fileImport).Should().Be(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void GetSplitPartsPrefix_ShouldThrow_WhenDestinationPrefixIsMissing(string? destinationPrefix)
    {
        var fileImport = new FileImport { FileName = "CTSM_FILE_2026-01-01-000000.csv", DestinationPrefix = destinationPrefix! };

        var act = () => S3ToPostgresCopyService.GetSplitPartsPrefix(fileImport);

        act.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData("A\"B", "A\"\"B")]
    [InlineData("A\u0001B", "A B")]
    [InlineData(null, "")]
    public void SanitiseLine_ShouldSanitiseCorrectly(string? input, string expected)
    {
        var result = InvokeSanitise(input!);
        result.Should().Be(expected);
    }

    [Fact]
    public async Task GetCommandsAsync_ShouldReturnInsertCommand_WhenDelta()
    {
        var insertCmd = new Mock<DbCommand>().Object;

        _factory.Setup(x => x.CreateInsertCommandAsync(It.IsAny<ImportDataType>(), It.IsAny<SchemaName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(insertCmd);

        var result = await InvokeGetCommandsAsync(ImportDataType.CtLocations, SchemaName.CtsTransactions, ImportActionType.Delta, _factory.Object);

        result.Should().ContainSingle().Which.Should().Be(insertCmd);
    }

    [Fact]
    public async Task GetCommandsAsync_ShouldReturnInsertCommand_WhenBulk()
    {
        var insertCmd = new Mock<DbCommand>().Object;

        _factory.Setup(x => x.CreateInsertCommandAsync(It.IsAny<ImportDataType>(), It.IsAny<SchemaName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(insertCmd);

        var result = await InvokeGetCommandsAsync(ImportDataType.CtLocations, SchemaName.CtsTransactions, ImportActionType.Bulk, _factory.Object);

        result.Should().ContainSingle().Which.Should().Be(insertCmd);
    }

    [Fact]
    public async Task ExecuteActionCommandsAsync_ShouldSumResults()
    {
        var service = CreateService();

        var cmd1 = new Mock<DbCommand>();
        cmd1.Setup(x => x.ExecuteNonQueryAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(5);

        var cmd2 = new Mock<DbCommand>();
        cmd2.Setup(x => x.ExecuteNonQueryAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(7);

        var method = MethodInfoUtility.GetPrivate<S3ToPostgresCopyService>("ExecuteActionCommandsAsync");

        var result = await (Task<int>)method!.Invoke(
            service,
            [new[] { cmd1.Object, cmd2.Object }, CancellationToken.None])!;

        result.Should().Be(12);
    }

    // CopyFileToStagingAsync (takes the already-opened source stream)

    [Fact]
    public async Task GivenNullSourceStream_CopyFileToStagingAsync_ShouldReturnEmptyAndStageNothing()
    {
        var service = CreateService();

        var result = await InvokeCopyFileToStagingAsync(service, CreateFileExecutionContext(), source: null);

        result.Should().BeEmpty();
        _factory.Verify(
            x => x.CreateTextImport(It.IsAny<ImportDataType>(), It.IsAny<SchemaName>(), It.IsAny<char>(), It.IsAny<List<string>>()),
            Times.Never);
    }

    [Fact]
    public async Task CopyFileToStagingAsync_ShouldWriteSanitisedLines()
    {
        var service = CreateService();

        var outputStream = new MemoryStream();
        var writer = new NonDisposingStreamWriter(outputStream);

        _factory.Setup(x => x.CreateTextImport(It.IsAny<ImportDataType>(), It.IsAny<SchemaName>(), It.IsAny<char>(), It.IsAny<List<string>>()))
            .Returns(writer);

        var source = new MemoryStream(Encoding.UTF8.GetBytes("record_type|col2\nA\"B|C\nT|END"));

        await InvokeCopyFileToStagingAsync(service, CreateFileExecutionContext(), source);

        writer.Flush();
        var output = Encoding.UTF8.GetString(outputStream.ToArray());
        output = output.Replace("\r\n", "\n");
        output.Should().Be("A\"\"B|C\n");
    }

    // OpenSourceAsync: download -> (optional) decrypt

    [Fact]
    public async Task OpenSourceAsync_ShouldReturnNull_WhenDownloadedStreamIsNull()
    {
        var service = CreateService(storageEncryptionEnabled: true);

        var result = await InvokeOpenSourceAsync(service, downloaded: null);

        result.Should().BeNull();
        VerifyDecryptNeverCalled();
    }

    [Fact]
    public async Task OpenSourceAsync_ShouldReturnDownloadedStreamUntouched_WhenStorageEncryptionDisabled()
    {
        var service = CreateService(storageEncryptionEnabled: false);
        using var downloaded = new MemoryStream(Encoding.UTF8.GetBytes("record_type|col2\nA|B"));

        var result = await InvokeOpenSourceAsync(service, downloaded);

        result.Should().BeSameAs(downloaded);
        VerifyDecryptNeverCalled();
    }

    [Fact]
    public async Task OpenSourceAsync_ShouldReturnDecryptedPlaintext_WhenStorageEncryptionEnabled()
    {
        var service = CreateService(storageEncryptionEnabled: true);
        var plaintext = Encoding.UTF8.GetBytes("record_type|col2\nA|B\nT|END");
        using var downloaded = new MemoryStream([1, 2, 3]); // content is irrelevant, the encryptor is mocked

        _fileEncryptorMock
            .Setup(x => x.DecryptAsync(downloaded, It.IsAny<Stream>(), It.IsAny<long?>(), It.IsAny<Action<int, string>?>(), It.IsAny<CancellationToken>()))
            .Callback<Stream, Stream, long?, Action<int, string>?, CancellationToken>((_, output, _, _, _) => output.Write(plaintext))
            .Returns(Task.CompletedTask);

        var result = await InvokeOpenSourceAsync(service, downloaded);

        result.Should().NotBeNull();
        result.Should().NotBeSameAs(downloaded);

        var tempPath = ((FileStream)result!).Name;
        File.Exists(tempPath).Should().BeTrue();

        using (var reader = new MemoryStream())
        {
            await result!.CopyToAsync(reader, TestContext.Current.CancellationToken);
            reader.ToArray().Should().Equal(plaintext);
        }

        await result!.DisposeAsync();
        File.Exists(tempPath).Should().BeFalse("the decrypted temp file must be deleted when the stream is disposed");
    }

    [Fact]
    public async Task OpenSourceAsync_ShouldPropagateAuthenticationFailure_WhenDecryptionFails()
    {
        var service = CreateService(storageEncryptionEnabled: true);
        using var downloaded = new MemoryStream([1, 2, 3]);

        _fileEncryptorMock
            .Setup(x => x.DecryptAsync(It.IsAny<Stream>(), It.IsAny<Stream>(), It.IsAny<long?>(), It.IsAny<Action<int, string>?>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new EncryptedFileAuthenticationException("tampered", new CryptographicException()));

        await Assert.ThrowsAsync<EncryptedFileAuthenticationException>(() => InvokeOpenSourceAsync(service, downloaded));
    }

    [Fact]
    public async Task OpenSourceAsync_WithRealEncryptor_ShouldDecryptSoStagingSeesPlaintext()
    {
        var encryptor = CreateRealEncryptor();
        var service = CreateService(storageEncryptionEnabled: true, fileEncryptor: encryptor);

        using var encrypted = await EncryptionUtilities.EncryptAsync(encryptor, "record_type|col2\nA\"B|C\nT|END");

        var outputStream = new MemoryStream();
        var writer = new NonDisposingStreamWriter(outputStream);
        _factory.Setup(x => x.CreateTextImport(It.IsAny<ImportDataType>(), It.IsAny<SchemaName>(), It.IsAny<char>(), It.IsAny<List<string>>()))
            .Returns(writer);

        await using var source = await InvokeOpenSourceAsync(service, encrypted);
        await InvokeCopyFileToStagingAsync(service, CreateFileExecutionContext(), source);

        writer.Flush();
        Encoding.UTF8.GetString(outputStream.ToArray()).Replace("\r\n", "\n").Should().Be("A\"\"B|C\n");
    }

    [Fact]
    public async Task OpenSourceAsync_WithRealEncryptor_ShouldRejectTamperedFile_AndStageNothing()
    {
        var encryptor = CreateRealEncryptor();
        var service = CreateService(storageEncryptionEnabled: true, fileEncryptor: encryptor);

        using var encrypted = await EncryptionUtilities.EncryptAsync(encryptor, "record_type|col2\nA|B\nT|END");
        var bytes = encrypted.ToArray();
        bytes[^1] ^= 0x01; // corrupt the final authentication tag
        using var tampered = new MemoryStream(bytes);

        await Assert.ThrowsAsync<EncryptedFileAuthenticationException>(() => InvokeOpenSourceAsync(service, tampered));

        _factory.Verify(
            x => x.CreateTextImport(It.IsAny<ImportDataType>(), It.IsAny<SchemaName>(), It.IsAny<char>(), It.IsAny<List<string>>()),
            Times.Never);
    }

    [Fact]
    public async Task OpenSourceAsync_WithRealEncryptor_ShouldRejectPlaintextFile_WhenEncryptionEnabled()
    {
        // Guards the "flag turned on while plaintext parts remain" case: fail loudly rather than import garbage.
        var encryptor = CreateRealEncryptor();
        var service = CreateService(storageEncryptionEnabled: true, fileEncryptor: encryptor);
        using var plaintext = new MemoryStream(Encoding.UTF8.GetBytes(new string('x', 500)));

        await Assert.ThrowsAsync<EncryptedFileFormatException>(() => InvokeOpenSourceAsync(service, plaintext));
    }

    // Helpers

    private S3ToPostgresCopyService CreateService(bool storageEncryptionEnabled = false, IFileEncryptor? fileEncryptor = null)
    {
        _factoryProvider
          .Setup(x => x.Create(It.IsAny<NpgsqlConnection>()))
          .Returns(_factory.Object);

        _provider.Setup(x => x.GetService(typeof(StorageBridgeWriteDbContext)))
            .Returns(null!);

        _provider.Setup(x => x.GetService(typeof(IStorageService<CadsInternalClient>)))
           .Returns(_storageService.Object);

        _provider.Setup(x => x.GetService(typeof(IS3ImportCommandFactoryProvider)))
           .Returns(_factoryProvider.Object);

        _provider.Setup(x => x.GetService(typeof(IStorageBridgeFileImportRepository)))
          .Returns(_fileImportRepository.Object);

        _scope.Setup(x => x.ServiceProvider).Returns(_provider.Object);

        _scopeFactory.Setup(x => x.CreateScope()).Returns(_scope.Object);

        _encryptionOptionsMock
            .Setup(x => x.Value)
            .Returns(EncryptionConfigWith(storageEncryptionEnabled));

        return new S3ToPostgresCopyService(
            _scopeFactory.Object,
            fileEncryptor ?? _fileEncryptorMock.Object,
            _encryptionOptionsMock.Object,
            _logger.Object);
    }

    private void VerifyDecryptNeverCalled() =>
        _fileEncryptorMock.Verify(
            x => x.DecryptAsync(It.IsAny<Stream>(), It.IsAny<Stream>(), It.IsAny<long?>(), It.IsAny<Action<int, string>?>(), It.IsAny<CancellationToken>()),
            Times.Never);

    private static FileEncryptor CreateRealEncryptor() =>
        new(new InMemoryMasterKeyProvider(TestEncryptionKeyProvider.CurrentKeyId, TestEncryptionKeyProvider.Keys));

    private FileExecutionContext CreateFileExecutionContext()
    {
        var importContext = new ImportExecutionContext(
            new FileImport(),
            new ImportParameters
            {
                ImportActionType = ImportActionType.Bulk,
                ImportDataType = ImportDataType.CtLocations,
                SchemaName = SchemaName.Cts,
            },
            '|',
            _factory.Object,
            _normaliser.Object,
            _dbContext.Object,
            _dbCommand.Object,
            _actionCommands.Object);

        return new FileExecutionContext(importContext, ValidTestFileName1);
    }

    private static async Task<List<string>> InvokeCopyFileToStagingAsync(
        S3ToPostgresCopyService service,
        FileExecutionContext fileExecutionContext,
        Stream? source)
    {
        var method = typeof(S3ToPostgresCopyService)
            .GetMethod("CopyFileToStagingAsync", BindingFlags.NonPublic | BindingFlags.Instance);

        return await (Task<List<string>>)method!.Invoke(service,
            [
                fileExecutionContext,
                source,
                false,
                CancellationToken.None
            ])!;
    }

    private static async Task<Stream?> InvokeOpenSourceAsync(S3ToPostgresCopyService service, Stream? downloaded)
    {
        var method = MethodInfoUtility.GetPrivate<S3ToPostgresCopyService>("OpenSourceAsync");

        return await (Task<Stream?>)method!.Invoke(service,
            [
                downloaded,
                ValidTestFileName1,
                CancellationToken.None
            ])!;
    }

    private static string? InvokeSanitise(string input)
    {
        var method = MethodInfoUtility.GetPrivateStatic<S3ToPostgresCopyService>("SanitiseLine");

        return (string?)method!.Invoke(null, [input]);
    }

    private static async Task<List<DbCommand>> InvokeGetCommandsAsync(
        ImportDataType importDataType,
        SchemaName schemaName,
        ImportActionType importActionType,
        IS3ImportCommandFactory factory)
    {
        var method = MethodInfoUtility.GetPrivateStatic<ImportExecutionContext>("GetCommandsAsync");

        var task = (Task<List<DbCommand>>)method.Invoke(
            null,
            [importDataType, schemaName, importActionType, factory, CancellationToken.None])!;

        return await task;
    }
}