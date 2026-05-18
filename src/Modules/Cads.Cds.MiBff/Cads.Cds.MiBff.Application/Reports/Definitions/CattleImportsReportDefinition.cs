using Cads.Cds.BuildingBlocks.Core.OpenXml;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Application.Reports.Definitions;

public class CattleImportsReportDefinition : IReportDefinition<MiImportSummary>
{
    public string TemplateFileName => "./ReportTemplates/Cattle-Imports-Template.xlsx";
    public int TableTemplateRow => 13;
    public int TemplateRowFirstColumn => 2;

    public List<Func<MiImportSummary, IConvertible>> Selectors =>
    [
        x => x.Country,
        x => x.MonthYear,
        x => x.AgeAtImport,
        x => x.AgeBand,
        x => x.BreedType,
        x => x.Sex ?? "",
        x => x.NumberOfImports
    ];
}