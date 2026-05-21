namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.BulkLoad.Services;

public class S3ToPostgresCopyServiceTests
{
    //private readonly Mock<IServiceScopeFactory> _scopeFactory = new();    
    //private readonly Mock<IServiceScope> _scope = new();
    //private readonly Mock<IServiceProvider> _provider = new();
    //private readonly Mock<IStorageReader<CadsInternalClient>> _storageReader = new();
    //private readonly Mock<IS3BulkLoadCommandFactoryProvider> _factoryProvider = new();
    //private readonly Mock<IS3BulkLoadCommandFactory> _factory = new();
    //private readonly Mock<ILogger<S3ToPostgresCopyService>> _logger = new();

    //[Fact]
    //public async Task ExecuteAsync_ShouldThrow_WhenImportActionTypeIsNone()
    //{
    //    var service = CreateService();

    //    var job = new CreateS3BulkLoadJobDto
    //    {
    //        ImportActionType = ImportActions.None,
    //        SourceKey = TestFileName
    //    };

    //    await Assert.ThrowsAsync<InvalidOperationException>(() => service.ExecuteAsync(job, TestContext.Current.CancellationToken));
    //}

    //[Fact]
    //public async Task ExecuteAsync_ShouldThrow_WhenSourceKeyMissing()
    //{
    //    var service = CreateService();

    //    var job = new CreateS3BulkLoadJobDto
    //    {
    //        ImportActionType = ImportActions.Insert,
    //        SourceKey = ""
    //    };

    //    await Assert.ThrowsAsync<InvalidOperationException>(() => service.ExecuteAsync(job, TestContext.Current.CancellationToken));
    //}

    //[Fact]
    //public async Task ExecuteAsync_ShouldReturnFalse_WhenNoKeysFound()
    //{
    //    var service = CreateService();

    //    _storageReader.Setup(x => x.ListKeysAsync(TestFileName, It.IsAny<CancellationToken>()))
    //        .ReturnsAsync([]);

    //    var job = new CreateS3BulkLoadJobDto
    //    {
    //        ImportActionType = ImportActions.Insert,
    //        SourceKey = TestFileName,
    //        BulkImportType = BulkLoadDataTypes.Locations
    //    };

    //    var result = await service.ExecuteAsync(job, TestContext.Current.CancellationToken);

    //    result.Should().BeFalse();
    //}

    //[Theory]
    //[InlineData("A\"B", "A\"\"B")]
    //[InlineData("A\u0001B", "A B")]
    //[InlineData(null, "")]
    //public void SanitiseLine_ShouldSanitiseCorrectly(string? input, string expected)
    //{
    //    var result = InvokeSanitise(input!);
    //    result.Should().Be(expected);
    //}

    //[Fact]
    //public async Task GetCommandsAsync_ShouldReturnInsertCommand()
    //{
    //    var insertCmd = new Mock<DbCommand>().Object;
    //    var factory = GetFactory();
    //    factory.InsertCommand = insertCmd;

    //    var job = new CreateS3BulkLoadJobDto
    //    {
    //        ImportActionType = ImportActions.Insert,
    //        BulkImportType = BulkLoadDataTypes.Locations
    //    };

    //    var result = await InvokeGetCommandsAsync(job, factory);

    //    result.Should().ContainSingle().Which.Should().Be(insertCmd);
    //}

    //[Fact]
    //public async Task GetCommandsAsync_ShouldReturnUpdateCommand()
    //{
    //    var updateCmd = new Mock<DbCommand>().Object;

    //    var factory = GetFactory();
    //    factory.UpdateCommand = updateCmd;

    //    var job = new CreateS3BulkLoadJobDto
    //    {
    //        ImportActionType = ImportActions.Update,
    //        BulkImportType = BulkLoadDataTypes.Locations
    //    };

    //    var result = await InvokeGetCommandsAsync(job, factory);

    //    result.Should().ContainSingle().Which.Should().Be(updateCmd);
    //}

    //[Fact]
    //public async Task GetCommandsAsync_ShouldReturnUpsertCommand_WhenInsertAndUpdate()
    //{
    //    var upsertCmd = new Mock<DbCommand>().Object;

    //    var factory = GetFactory();
    //    factory.UpsertCommand = upsertCmd;

    //    var job = new CreateS3BulkLoadJobDto
    //    {
    //        ImportActionType = ImportActions.Insert | ImportActions.Update,
    //        BulkImportType = BulkLoadDataTypes.Locations
    //    };

    //    var result = await InvokeGetCommandsAsync(job, factory);

    //    result.Should().ContainSingle().Which.Should().Be(upsertCmd);
    //}

    //[Fact]
    //public async Task GetCommandsAsync_ShouldReturnDeleteCommand()
    //{
    //    var deleteCmd = new Mock<DbCommand>().Object;

    //    var factory = GetFactory();
    //    factory.DeleteCommand = deleteCmd;

    //    var job = new CreateS3BulkLoadJobDto
    //    {
    //        ImportActionType = ImportActions.Delete,
    //        BulkImportType = BulkLoadDataTypes.Locations
    //    };

    //    var result = await InvokeGetCommandsAsync(job, factory);

    //    result.Should().ContainSingle().Which.Should().Be(deleteCmd);
    //}

    //[Fact]
    //public async Task ExecuteActionCommandsAsync_ShouldSumResults()
    //{
    //    var service = CreateService();

    //    var cmd1 = new Mock<DbCommand>();
    //    cmd1.Setup(x => x.ExecuteNonQueryAsync(It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(5);

    //    var cmd2 = new Mock<DbCommand>();
    //    cmd2.Setup(x => x.ExecuteNonQueryAsync(It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(7);

    //    var method = MethodInfoUtility.GetPrivate<S3ToPostgresCopyService>("ExecuteActionCommandsAsync");

    //    var result = await (Task<int>)method!.Invoke(
    //        service,
    //        [new[] { cmd1.Object, cmd2.Object }, CancellationToken.None])!;

    //    result.Should().Be(12);
    //}

    //[Fact]
    //public async Task CopyFileToStagingAsync_ShouldWriteSanitisedLines()
    //{
    //    var service = CreateService();

    //    var response = new GetObjectResponse
    //    {
    //        ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes("col1|col2\nA\"B|C\nT|END"))
    //    };

    //    _storageReader
    //        .Setup(x => x.GetObjectResponseAsync(TestFileName, It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(response);

    //    var outputStream = new MemoryStream();
    //    var writer = new NonDisposingStreamWriter(outputStream);

    //    var factory = new TestableS3BulkLoadCommandFactory(
    //        new NpgsqlConnection(),
    //        ["col1", "col2"])
    //    {
    //        CreateTextImportOverride = (_, _, _) => writer
    //    };

    //    var method = typeof(S3ToPostgresCopyService)
    //        .GetMethod("CopyFileToStagingAsync", BindingFlags.NonPublic | BindingFlags.Instance);

    //    await (Task)method!.Invoke(service,
    //        [
    //        BulkLoadDataTypes.Locations,
    //        '|',
    //        TestFileName,
    //        factory,
    //        CancellationToken.None
    //        ])!;

    //    writer.Flush();
    //    var output = Encoding.UTF8.GetString(outputStream.ToArray());
    //    output = output.Replace("\r\n", "\n");
    //    output.Should().Be("A\"\"B|C\n");
    //}

    //private const string TestFileName = "LOCATIONS.part-0001.csv";

    //private static TestableS3BulkLoadCommandFactory GetFactory() =>
    //    new(new NpgsqlConnection("Host=cads-postgres;Port=5432;Database=cads_data_service;Username=postgres;Password=postgres"));

    //private static string LocationsHeader =>
    //    "record_type|record_count|loc_id";

    //private S3ToPostgresCopyService CreateService()
    //{
    //    _provider.Setup(x => x.GetService(typeof(StorageBridgeWriteDbContext)))
    //        .Returns(null!);
    //    _scope.Setup(x => x.ServiceProvider).Returns(_provider.Object);
    //    _scopeFactory.Setup(x => x.CreateScope()).Returns(_scope.Object);

    //    _factoryProvider
    //        .Setup(x => x.Create(It.IsAny<NpgsqlConnection>()))
    //        .Returns(_factory.Object);

    //    return new S3ToPostgresCopyService(
    //        _scopeFactory.Object,
    //        _storageReader.Object,
    //        _factoryProvider.Object,
    //        _logger.Object);
    //}

    //private static string? InvokeSanitise(string input)
    //{
    //    var method = MethodInfoUtility.GetPrivateStatic<S3ToPostgresCopyService>("SanitiseLine");

    //    return (string?)method!.Invoke(null, [input]);
    //}

    //private static async Task<List<DbCommand>> InvokeGetCommandsAsync(CreateS3BulkLoadJobDto job, S3BulkLoadCommandFactory factory)
    //{
    //    var method = MethodInfoUtility.GetPrivateStatic<S3ToPostgresCopyService>("GetCommandsAsync");

    //    var task = (Task<List<DbCommand>>)method.Invoke(
    //        null,
    //        [job, factory, CancellationToken.None])!;

    //    return await task;
    //}
}
