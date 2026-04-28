using Cads.Cds.BuildingBlocks.Application.Commands;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class DownloadReportCommand
    : ICommand<MemoryStream>
{
    public required string Identifier { get; set; }
    public required string ReportKey { get; set; }
    /// <summary>
    /// The inclusive start date.
    /// </summary>
    public required DateTime StartDate { get; set; }
    /// <summary>
    /// The exclusive end date.
    /// </summary>
    public required DateTime EndDate { get; set; }
}