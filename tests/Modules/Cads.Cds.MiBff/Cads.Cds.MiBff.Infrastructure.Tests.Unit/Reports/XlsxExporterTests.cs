using Cads.Cds.MiBff.Infrastructure.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Reports;

public class XlsxExporterTests
{
    class CattleMovementDto
    {
        public string BirthYear { get; set; } = "2024";
        public string BirthMonth { get; set; } = "January";
        public string Country { get; set; } = "England";
        public string GovRegion { get; set; } = "South East";
        public string County { get; set; } = "Oxfordshire";
        public string BreedType { get; set; } = "NonDairy";
        public string Breed { get; set; } = "Aberdeen Angus";
        public string Sex { get; set; } = "F";
        public string ApplicationType { get; set; } = "Birth Application";
        public string NumberOfBirths { get; set; } = "19";
    }
    
    [Fact]
    public void CreateDocument()
    {
        Random rnd = new Random();
        var sut = new XlsxReport<CattleMovementDto>();
        sut.Data = Enumerable.Range(1, 20).Select(x => new CattleMovementDto() { NumberOfBirths = rnd.Next(1, 25).ToString()}).ToList();
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
        sut.Selectors = new List<Func<CattleMovementDto, string>>()
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
        sut.GenerateWithOpenXml("./file.xlsx");
    }
}