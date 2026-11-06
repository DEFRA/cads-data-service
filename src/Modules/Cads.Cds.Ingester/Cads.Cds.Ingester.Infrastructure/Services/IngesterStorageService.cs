using Cads.Cds.BuildingBlocks.Application.Encryption;
using Cads.Cds.BuildingBlocks.Infrastructure.Encryption.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.Ingester.Core.DTOs.Common;
using Cads.Cds.Ingester.Core.Services.AnimalMovements;
using Cads.Cds.Ingester.Infrastructure.Storage.Clients;
using Microsoft.Extensions.Options;

namespace Cads.Cds.Ingester.Infrastructure.Services;

public class IngesterStorageService(IStorageWriter<IngesterClient> storageWriter, IFileEncryptor fileEncryptor, IOptions<EncryptionConfig> encryptionOptions)
    : IIngesterStorageService
{
    private readonly EncryptionConfig _encryptionConfig = encryptionOptions.Value;

    public async Task<IngestionDto> WriteAsync(string key, string payload, CancellationToken cancellationToken)
    {
        if (!_encryptionConfig.StorageEncryptionEnabled)
        {
            await storageWriter.WriteAsync(key, payload, cancellationToken);

            return CreateIngestionDto(key);
        }

        using var input = new MemoryStream(
            System.Text.Encoding.UTF8.GetBytes(payload),
            writable: false);

        return await WriteAsync(key,
            input,
            cancellationToken: cancellationToken);
    }

    public async Task<IngestionDto> WriteAsync(string key, Stream fileStream, CancellationToken cancellationToken)
    {
        using var output = new MemoryStream();

        await fileEncryptor.EncryptAsync(
            fileStream,
            output,
            cancellationToken: cancellationToken);

        output.Position = 0;

        await storageWriter.WriteAsync(key, output, cancellationToken);

        return CreateIngestionDto(key);
    }

    private static IngestionDto CreateIngestionDto(string key) => new()
    {
        IngestionId = key,
        ReceivedAt = DateTimeOffset.UtcNow,
        RecordCount = 1
    };
}