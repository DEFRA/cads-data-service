using Cads.Cds.BuildingBlocks.Application.Messaging.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.MiBff.Application.Reports.Commands.ProcessReportJob;

namespace Cads.Cds.MiBff.Infrastructure.Messaging.Factories;

public sealed class ReportJobRequestedMessageCommandFactory : IMessageCommandFactory
{
    public IMessageProcessingCommand Create(UnwrappedMessage message)
        => new ProcessReportJobRequestedMessageCommand(message);
}