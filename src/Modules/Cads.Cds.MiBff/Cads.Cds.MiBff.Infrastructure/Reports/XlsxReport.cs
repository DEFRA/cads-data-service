using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Cads.Cds.MiBff.Infrastructure.Reports;

public class XlsxReport<T>
{
    public required List<T> Data { get; set; }
    public required List<Func<T, IConvertible>> Selectors { get; set; }
    public required string TemplateFileName { get; set; }

    ///<Summary>
    /// row number where table data starts, starting from 1
    /// </Summary>
    public int TableTemplateRow { get; set; }

    ///<Summary>
    /// column number where table data starts, starting from 1
    /// </Summary>
    public int TemplateRowFirstColumn { get; set; }

    public void Generate(MemoryStream stream)
    {
        using var spreadsheet = BuildFromTemplate();
        spreadsheet.Clone(stream);
    }

    private SpreadsheetDocument BuildFromTemplate()
    {
        SpreadsheetDocument? spreadsheet = null;
        try
        {
            spreadsheet = SpreadsheetDocument.Open(TemplateFileName!, true);
            var workbookPart = spreadsheet.WorkbookPart!;
            var worksheet = workbookPart.WorksheetParts!.FirstOrDefault()!.Worksheet!;
            var sheetData = worksheet.GetFirstChild<SheetData>();
            AddTableData(sheetData!);
            worksheet.Save();
            return spreadsheet;
        }
        catch
        {
            spreadsheet?.Dispose();
            throw;
        }
    }

    private int GetColumnIndexFromCellReference(string reference)
    {
        int offset = 0;
        int index = 0;
        while (reference[offset] >= 'A' && reference[offset] <= 'Z')
        {
            index *= 26;
            index += reference[offset] - 'A' + 1;
            offset++;
        }

        return index;
    }

    private void AddTableData(SheetData sheetData)
    {
        var templateRow = sheetData.ChildElements.OfType<Row>().Single(x => x.RowIndex?.Value == TableTemplateRow);
        var header = sheetData.ChildElements.OfType<Row>().Single(x => x.RowIndex?.Value == TableTemplateRow - 1);
        sheetData.RemoveChild(templateRow);

        foreach (var row in sheetData.ChildElements.OfType<Row>().Where(x => x.RowIndex?.Value > TableTemplateRow))
        {
            row.RowIndex = null;
        }

        var previousRow = header;
        foreach (var rowData in Data)
        {
            var nextRow = (Row)templateRow.CloneNode(true);
            nextRow.RowIndex = null;
            var cells = nextRow.ChildElements.OfType<Cell>();
            foreach (var cell in cells)
            {
                var columnIndex = GetColumnIndexFromCellReference(cell!.CellReference!);
                if (columnIndex < TemplateRowFirstColumn || columnIndex > TemplateRowFirstColumn + Selectors.Count - 1)
                    continue;
                cell.CellReference = null;

                var value = Selectors[columnIndex - TemplateRowFirstColumn](rowData);
                cell.CellValue = new CellValue(value.ToString(CultureInfo.InvariantCulture));
                cell.DataType = MapValueToExcelType(value);
            }

            previousRow.InsertAfterSelf(nextRow);
            previousRow = nextRow;
        }
    }

    private static EnumValue<CellValues> MapValueToExcelType(IConvertible value)
    {
        if (value is int)
            return new EnumValue<CellValues>(CellValues.Number);

        if (value is string)
            return new EnumValue<CellValues>(CellValues.String);

        throw new Exception($"Unsupported value type for excel report builder - {value.GetType()}");
    }
}