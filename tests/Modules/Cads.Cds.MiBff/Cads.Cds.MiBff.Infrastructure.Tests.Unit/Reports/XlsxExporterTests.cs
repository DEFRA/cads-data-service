using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Infrastructure.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Reports;

public class XlsxExporterTests
{
    
    [Fact]
    public void CreateDocument()
    {
        var sut = new XlsxReport<CattleRegistration>();
        sut.Data = CattleRegistration.GetFakeData(25);
        sut.Headers = new List<string>()
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
        sut.Selectors = new List<Func<CattleRegistration, string>>()
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
        sut.Generate("./file.xlsx");
    }
}