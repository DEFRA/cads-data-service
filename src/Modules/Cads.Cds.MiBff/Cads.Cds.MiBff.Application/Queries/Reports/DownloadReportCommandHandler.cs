using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.MiBff.Application.Services.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class DownloadReportCommandHandler(IReportGenerationService service)
    : ICommandHandler<DownloadReportCommand, MemoryStream>
{
    public Task<MemoryStream> Handle(DownloadReportCommand request, CancellationToken cancellationToken)
    {
        //todo generic lookup
        switch (request.ReportKey)
        {
            case "gb_cattle_registrations":
                return service.GetCattleRegistrations(request.StartDate, request.EndDate, cancellationToken);

            default:
                throw new ArgumentException("Invalid report key"); // TODO - better way to handle?
        }
    }
}