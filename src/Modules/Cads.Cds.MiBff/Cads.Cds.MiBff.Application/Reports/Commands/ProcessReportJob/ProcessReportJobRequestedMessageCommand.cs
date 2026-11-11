using Cads.Cds.BuildingBlocks.Application.Messaging.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;

namespace Cads.Cds.MiBff.Application.Reports.Commands.ProcessReportJob;

public sealed record ProcessReportJobRequestedMessageCommand(UnwrappedMessage Message)
    : IMessageProcessingCommand;
