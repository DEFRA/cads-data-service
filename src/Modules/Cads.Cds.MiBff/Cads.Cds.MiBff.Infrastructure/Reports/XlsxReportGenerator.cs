using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Core.Services.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Reports;

public class XlsxReportGenerator : IXlsxReportGenerator
{
    public void Save(List<CattleRegistration> data, string fileName)
    {
        var report = new XlsxReport<CattleRegistration>();
        report.TemplateFileName = "./Reports/Templates/Cattle-Registrations-Template.xlsx";
        report.Data = data;
        report.Headers = new List<string>()
        {
            "Birth Year",
            "Birth Month",
            "Country",
            "Gov Region",
            "County",
            "Breed Type",
            "Breed",
            "Sex",
            "Application Type",
            "Number Of Births",
        };
        report.Selectors = new List<Func<CattleRegistration, string>>()
        {
            (x) => "",
            (x) => x.BirthYear,
            (x) => x.BirthMonth,
            (x) => x.Country,
            (x) => x.GovRegion,
            (x) => x.County,
            (x) => x.BreedType,
            (x) => x.Breed,
            (x) => x.Sex,
            (x) => x.ApplicationType,
            (x) => x.NumberOfBirths,
        };
        report.Generate(fileName);
    }
    
    public MemoryStream Generate(List<CattleRegistration> data)
    {
        var report = new XlsxReport<CattleRegistration>();
        report.Data = data;
        report.Headers = new List<string>()
        {
            "Birth Year",
            "Birth Month",
            "Country",
            "Gov Region",
            "County",
            "Breed Type",
            "Breed",
            "Sex",
            "Application Type",
            "Number Of Births",
        };
        report.Selectors = new List<Func<CattleRegistration, string>>()
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
            (x) => x.NumberOfBirths,
        };
        
        var output = new  MemoryStream();
        report.Generate(output);
        return output;
    }
}