using Cads.Cds.BuildingBlocks.Core.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;

namespace Cads.Cds.BuildingBlocks.Application.OpenXml;

public class OpenXmlReport<T>(IReportDefinition<T> definition, List<T> data)
{
    private readonly List<T> _data = data;
    private List<Func<T, IConvertible>> Selectors => definition.Selectors;
    private string TemplateFileName => definition.TemplateFileName;

    private int TableTemplateRow => definition.TableTemplateRow;

    private int TemplateRowFirstColumn => definition.TemplateRowFirstColumn;

    public MemoryStream Generate()
    {
        using var template = SpreadsheetDocument.Open(TemplateFileName!, false);

        var stream = new MemoryStream();
        template.Clone(stream);
        stream.Position = 0;

        using var spreadsheet = SpreadsheetDocument.Open(stream, true);

        var workbookPart = spreadsheet.WorkbookPart!;
        var worksheet = workbookPart.WorksheetParts.First().Worksheet!;
        var sheetData = worksheet.GetFirstChild<SheetData>();

        AddTableData(sheetData!);
        worksheet.Save();

        stream.Position = 0;
        return stream;
    }

    private void AddTableData(SheetData sheetData)
    {
        var templateRow = sheetData.ChildElements.OfType<Row>().Single(x => x.RowIndex?.Value == TableTemplateRow);
        var header = sheetData.ChildElements.OfType<Row>().Single(x => x.RowIndex?.Value == TableTemplateRow - 1);
        sheetData.RemoveChild(templateRow);

        foreach (var row in sheetData.ChildElements.OfType<Row>().Where(x => x.RowIndex?.Value > TableTemplateRow))
        {
            row.ReindexTo(row.RowIndex!.Value + (uint)_data.Count);
        }

        var previousRow = header;
        foreach (var rowData in _data)
        {
            var nextRow = FillCopyOfTemplateRowWithData(templateRow, rowData);
            nextRow.ReindexTo(previousRow.RowIndex!.Value + 1);
            previousRow.InsertAfterSelf(nextRow);
            previousRow = nextRow;
        }
    }

    private Row FillCopyOfTemplateRowWithData(Row templateRow, T rowData)
    {
        var nextRow = (Row)templateRow.CloneNode(true);
        var cells = nextRow.ChildElements.OfType<Cell>();
        foreach (var cell in cells)
        {
            var columnIndex = cell!.CellReference!.GetIntegerColumnIndex();
            if (columnIndex < TemplateRowFirstColumn || columnIndex > TemplateRowFirstColumn + Selectors.Count - 1)
                continue;

            var value = Selectors[columnIndex - TemplateRowFirstColumn](rowData);
            cell.CellValue = new CellValue(value.ToString(CultureInfo.InvariantCulture));
            cell.DataType = value.MapValueToExcelType();
        }

        return nextRow;
    }
}