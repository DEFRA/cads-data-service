using Cads.Cds.MiBff.Core.DTOs.Reports;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers.Extensions;

public static class ControllerFileExtensions
{
    public static FileContentResult XlsxFile(this ControllerBase controller, FileReportResult fileReportResult)
    {
        fileReportResult.Stream.Position = 0;

        return controller.File(
            fileContents: fileReportResult.Stream.ToArray(),
            contentType: fileReportResult.ContentType,
            fileDownloadName: fileReportResult.FileName
        );
    }
}
