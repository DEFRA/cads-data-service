using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Services.Reports;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using FluentAssertions;
 
namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Reports;

public class XlsxExporterTests
{
    public static List<MiBirthSummaryResult> GetFakeData(int rows)
    {
        Random rnd = new Random();
        return Enumerable.Range(1, rows).Select(x => new MiBirthSummaryResult()
        {    
            BirthYear = 2024,
            BirthMonth = "January",
            Country = "England",
            GovRegion = "South East",
            County = "Oxfordshire",
            BreedType = "NonDairy",
            Breed = "Aberdeen Angus",
            Sex = "F",
            ApplicationType = "Birth Application",
            NumberOfBirths = rnd.Next(1, 25)
        }).ToList();
    }

    [Fact]
    public async Task CreateDocument()
    {
        var sut = new OpenXmlReportGenerator();
        var data = GetFakeData(25);

        using var stream = sut.Generate(data);


        stream.Position = 0;
        using (FileStream file = new FileStream("file.xlsx", FileMode.Create, System.IO.FileAccess.Write))
            await stream.CopyToAsync(file, TestContext.Current.CancellationToken);

        stream.Should().NotBeNull(); // weak assertion, can improve later
    }
}