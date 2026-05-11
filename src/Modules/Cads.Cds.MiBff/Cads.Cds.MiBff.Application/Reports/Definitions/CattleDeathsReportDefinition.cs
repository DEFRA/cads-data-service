using Cads.Cds.BuildingBlocks.Core.OpenXml;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Application.Reports.Definitions;

public class CattleDeathsReportDefinition : IReportDefinition<MiDeathSummary>
{
    public string TemplateFileName => "./ReportTemplates/Cattle-Deaths-Template.xlsx";
    public int TableTemplateRow => 12;
    public int TemplateRowFirstColumn => 2;

    public List<Func<MiDeathSummary, IConvertible>> Selectors =>
    [
        x => x.MonthName,
        x => x.Breed,
        x => x.BreedCode,
        x => x.BreedType,
        x => x.Sex ?? "",
        x => x.Country,
        x => x.AgeAtDeathInMonths,
        x => x.PremiseTypeGroups,
        x => x.NumberOfDeaths
    ];
}