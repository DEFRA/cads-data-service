using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.MiBff.Application.Reports.Requests;

namespace Cads.Cds.MiBff.Application.Reports.Commands.CreateReportJob;

public class CreateReportJobCommand : ICommand<Guid>
{
    public required string ReportKey { get; set; }
    public required string ExternalSubject { get; set; }
    public required GetReportRequest Request { get; set; }
}

