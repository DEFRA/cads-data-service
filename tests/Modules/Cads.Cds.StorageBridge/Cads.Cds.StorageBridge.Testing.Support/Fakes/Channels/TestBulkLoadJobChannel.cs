using Cads.Cds.StorageBridge.Core.DTOs;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Testing.Support.Fakes.Channels;

public class TestBulkLoadJobChannel
{
    public Channel<CreateS3BulkLoadJobDto> Channel { get; } =
        System.Threading.Channels.Channel.CreateUnbounded<CreateS3BulkLoadJobDto>();

    public async Task<CreateS3BulkLoadJobDto> WaitForJobAsync(CancellationToken token = default)
        => await Channel.Reader.ReadAsync(token);
}