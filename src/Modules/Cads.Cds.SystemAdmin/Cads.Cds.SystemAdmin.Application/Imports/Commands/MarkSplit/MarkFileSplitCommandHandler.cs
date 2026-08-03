using Cads.Cds.ApiSurface.Messages.Imports;
using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Application.Messaging.Clients;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkSplit;

public sealed class MarkFileSplitCommandHandler(
    ISystemAdminFileImportRepository fileImportRepository,
    IMessagePublisher<SystemAdminFifoQueueClient> messagePublisher)
    : ICommandHandler<MarkFileSplitCommand, Unit>
{
    public async Task<Unit> Handle(MarkFileSplitCommand command, CancellationToken cancellationToken)
    {
        var fileImport = await fileImportRepository.GetById(command.Id, cancellationToken)
                         ?? throw new NotFoundException(nameof(FileImport), command.Id);

        fileImport.MarkSplit();

        await PublishS3ToPostgresCopyMessage(cancellationToken);

        return Unit.Value;
    }

    // TODO - Wire up FifoMessageMetadata e2e
    private async Task PublishS3ToPostgresCopyMessage(CancellationToken cancellationToken)
    {
        var correlationId = Guid.NewGuid().ToString();
        var messageGroupId = Guid.NewGuid().ToString();
        var messageDeduplicationId = Guid.NewGuid().ToString();
        var message = new S3ToPostgresCopyMessage();

        var metadata = new FifoMessageMetadata(
            messageGroupId,
            messageDeduplicationId,
            correlationId);

        await messagePublisher.PublishAsync(message, metadata, cancellationToken);
    }
}