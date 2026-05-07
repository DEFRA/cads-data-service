using Cads.Cds.MiBff.Application.Services.Reports;
using Cads.Cds.MiBff.Core.Domain.Entities;
using FluentAssertions;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Reports;

public class OpenXmlExporterTests
{
    public static List<MiBirthSummary> GetFakeData(int rows)
    {
        Random rnd = new Random();
        return Enumerable.Range(1, rows).Select(x => new MiBirthSummary()
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

        stream.Should().NotBeNull();
    }
}