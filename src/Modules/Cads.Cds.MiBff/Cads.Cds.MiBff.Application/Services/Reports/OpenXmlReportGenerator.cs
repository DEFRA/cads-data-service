using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Application.Services.Reports;

public class OpenXmlReportGenerator : IOpenXmlReportGenerator
{
    public MemoryStream Generate(List<MiBirthSummaryResult> data)
    {
        var report = new OpenXmlReport<MiBirthSummaryResult>()
        {
            TemplateFileName = "./Reports/Templates/Cattle-Registrations-Template.xlsx",
            Data = data.ToList(),
            Selectors = new List<Func<MiBirthSummaryResult, IConvertible>>()
            {
                (x) => x.BirthYear,
                (x) => x.BirthMonth,
                (x) => x.Country,
                (x) => x.GovRegion ?? "",
                (x) => x.County ?? "",
                (x) => x.BreedType,
                (x) => x.Breed,
                (x) => x.Sex ?? "",
                (x) => x.ApplicationType ?? "",
                (x) => x.NumberOfBirths
            },
            TableTemplateRow = 20,
            TemplateRowFirstColumn = 2
        };

        var output = new MemoryStream();
        report.Generate(output);
        return output;
    }
}