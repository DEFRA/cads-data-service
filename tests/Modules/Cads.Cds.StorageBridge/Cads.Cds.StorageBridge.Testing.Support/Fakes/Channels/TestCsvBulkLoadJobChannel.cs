using Cads.Cds.StorageBridge.Core.DTOs;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Testing.Support.Fakes.Channels;

public class TestCsvBulkLoadJobChannel
{
    public Channel<CreateS3CsvImportJobDto> Channel { get; } =
        System.Threading.Channels.Channel.CreateUnbounded<CreateS3CsvImportJobDto>();

    public async Task<CreateS3CsvImportJobDto> WaitForJobAsync(CancellationToken token = default)
        => await Channel.Reader.ReadAsync(token);
}