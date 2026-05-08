namespace Cads.Cds.BuildingBlocks.Core.OpenXml;

public interface IReportDefinition<T>
{
    string TemplateFileName { get; }

    ///<Summary>
    /// row number where table data starts, starting from 1
    /// </Summary>
    int TableTemplateRow { get; }

    ///<Summary>
    /// column number where table data starts, starting from 1
    /// </Summary>
    int TemplateRowFirstColumn { get; }
    List<Func<T, IConvertible>> Selectors { get; }
}