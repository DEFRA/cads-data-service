using Cads.Cds.StorageBridge.Core.DTOs;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Testing.Support.Fakes.Channels;

public class TestSqlImportJobChannel
{
    public Channel<CreateS3SqlImportJobDto> Channel { get; } =
        System.Threading.Channels.Channel.CreateUnbounded<CreateS3SqlImportJobDto>();

    public async Task<CreateS3SqlImportJobDto> WaitForJobAsync(CancellationToken token = default)
        => await Channel.Reader.ReadAsync(token);
}