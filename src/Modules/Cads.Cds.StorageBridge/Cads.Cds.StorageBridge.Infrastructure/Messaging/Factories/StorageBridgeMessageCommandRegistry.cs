using Cads.Cds.BuildingBlocks.Application.Messaging.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.StorageBridge.Application.Messaging.Commands;

namespace Cads.Cds.StorageBridge.Infrastructure.Messaging.Factories;

public sealed class StorageBridgeMessageCommandRegistry
{
    private readonly Dictionary<string, IMessageCommandFactory> _map = [];

    public void Register<TFactory>(string subject)
        where TFactory : IMessageCommandFactory, new()
    {
        _map[subject] = new TFactory();
    }

    public IMessageProcessingCommand CreateCommand(UnwrappedMessage message)
    {
        if (!_map.TryGetValue(message.Subject, out var factory))
            throw new InvalidOperationException($"No command registered for subject {message.Subject}");

        return factory.Create(message);
    }
}

public sealed class S3ToPostgresCopyMessageCommandFactory : IMessageCommandFactory
{
    public IMessageProcessingCommand Create(UnwrappedMessage message)
        => new ProcessS3ToPostgresCopyMessageCommand(message);
}
