using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Application.Services.Reports;

public class CattleRegistrationReportDefinition : IReportDefinition<MiBirthSummary>
{
    public string TemplateFileName => "./ReportTemplates/Cattle-Registrations-Template.xlsx";
    public int TableTemplateRow => 20;
    public int TemplateRowFirstColumn => 2;

    public List<Func<MiBirthSummary, IConvertible>> Selectors =>
    [
        x => x.BirthYear,
        x => x.BirthMonth,
        x => x.Country,
        x => x.GovRegion ?? "",
        x => x.County ?? "",
        x => x.BreedType,
        x => x.Breed,
        x => x.Sex ?? "",
        x => x.ApplicationType ?? "",
        x => x.NumberOfBirths
    ];
}