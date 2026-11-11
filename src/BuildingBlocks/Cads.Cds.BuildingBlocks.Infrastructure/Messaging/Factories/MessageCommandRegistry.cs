using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;
using Cads.Cds.BuildingBlocks.Application.Messaging.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Core.Exceptions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;

/// <summary>
/// Maps an incoming message subject to the factory that builds its MediatR processing command.
/// Generic on the queue client so each queue gets its own registry singleton in DI.
/// </summary>
public sealed class MessageCommandRegistry<TClient>
    where TClient : IQueueClient
{
    private readonly Dictionary<string, IMessageCommandFactory> _map = new(StringComparer.Ordinal);

    public MessageCommandRegistry<TClient> Register<TFactory>(string subject)
        where TFactory : IMessageCommandFactory, new()
    {
        _map[subject] = new TFactory();
        return this;
    }

    public IMessageProcessingCommand CreateCommand(UnwrappedMessage message)
    {
        if (!_map.TryGetValue(message.Subject, out var factory))
            throw new NonRetryableException($"No command registered for subject {message.Subject}");

        return factory.Create(message);
    }
}
