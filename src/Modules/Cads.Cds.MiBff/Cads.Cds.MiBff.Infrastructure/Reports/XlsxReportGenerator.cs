using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Core.Services.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Reports;

public class XlsxReportGenerator : IXlsxReportGenerator
{
    public MemoryStream Generate()
    {
        var output = new MemoryStream();
        return output;
    }

    public MemoryStream Generate(List<CattleRegistration> data)
    {
        var report = new XlsxReport<CattleRegistration>()
        {
            TemplateFileName = "./Reports/Templates/Cattle-Registrations-Template.xlsx",
            Data = data,
            Selectors = new List<Func<CattleRegistration, IConvertible>>()
            {
                (x) => x.BirthYear,
                (x) => x.BirthMonth,
                (x) => x.Country,
                (x) => x.GovRegion,
                (x) => x.County,
                (x) => x.BreedType,
                (x) => x.Breed,
                (x) => x.Sex,
                (x) => x.ApplicationType,
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