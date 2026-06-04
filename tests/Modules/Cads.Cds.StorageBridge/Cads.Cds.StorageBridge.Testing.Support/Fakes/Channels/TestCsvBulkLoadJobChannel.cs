using Cads.Cds.StorageBridge.Core.DTOs;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Testing.Support.Fakes.Channels;

public class TestCsvBulkLoadJobChannel
{
    public Channel<CreateS3CsvBulkLoadJobDto> Channel { get; } =
        System.Threading.Channels.Channel.CreateUnbounded<CreateS3CsvBulkLoadJobDto>();

    public async Task<CreateS3CsvBulkLoadJobDto> WaitForJobAsync(CancellationToken token = default)
        => await Channel.Reader.ReadAsync(token);
}