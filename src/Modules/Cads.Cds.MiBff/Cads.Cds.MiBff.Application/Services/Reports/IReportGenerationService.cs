namespace Cads.Cds.MiBff.Application.Services.Reports;

public interface IReportGenerationService
{
    Task<MemoryStream> GetCattleRegistrations(DateOnly dateTimeFrom, DateOnly dateTimeTo, CancellationToken cancellationToken);
}