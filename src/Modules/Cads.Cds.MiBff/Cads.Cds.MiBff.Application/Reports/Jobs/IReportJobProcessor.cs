namespace Cads.Cds.MiBff.Application.Reports.Jobs;

public interface IReportJobProcessor
{
    Task ProcessAsync(Guid reportJobId, CancellationToken cancellationToken = default);
}