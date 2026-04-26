using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Cads.Cds.MiBff.Infrastructure.Reports;

public class XlsxReport<T>
{
    public List<string> Headers { get; set; }
    public List<T> Data { get; set; }
    public List<Func<T, IConvertible>> Selectors { get; set; }
    public string TemplateFileName { get; set; }
    
    ///<Summary>
    /// row number where table data starts, starting from 1
    /// </Summary>
    public int TableTemplateRow { get; set; }
    
    ///<Summary>
    /// column number where table data starts, starting from 1
    /// </Summary>
    public int TemplateRowFirstColumn { get; set; }

    public void Generate(string filePath)
    {
        if (!String.IsNullOrEmpty(TemplateFileName))
        {
            using var spreadsheet = SpreadsheetDocument.Open(TemplateFileName, true);
            
            var workbookPart = spreadsheet.WorkbookPart!;
            var worksheet = workbookPart.WorksheetParts!.FirstOrDefault()!.Worksheet!;
            var sheetData = worksheet.GetFirstChild<SheetData>();
            AddTableData(sheetData!);
            worksheet.Save();
            spreadsheet.Clone(filePath);
        }
        else
        {
            using var spreadsheet = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);
            var workbookPart = BuildWorkbook(spreadsheet);
            workbookPart.Workbook!.Save();
        }
    }
    
    public void Generate(MemoryStream stream)
    {
        if (!String.IsNullOrEmpty(TemplateFileName))
        {
            using var spreadsheet = SpreadsheetDocument.Open(TemplateFileName, true);
            
            var workbookPart = spreadsheet.WorkbookPart!;
            var worksheet = workbookPart.WorksheetParts!.FirstOrDefault()!.Worksheet!;
            var sheetData = worksheet.GetFirstChild<SheetData>();
            AddTableData(sheetData!);
            worksheet.Save();
            spreadsheet.Clone(stream);
        }
        else
        {
            using var spreadsheet = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
            var workbookPart = BuildWorkbook(spreadsheet);
            workbookPart.Workbook!.Save();
        }
    }

    private WorkbookPart BuildWorkbook(SpreadsheetDocument spreadsheet)
    {
        var workbookPart = spreadsheet.AddWorkbookPart();
        workbookPart.Workbook = new Workbook();

        var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        var sheetData = new SheetData();
        worksheetPart.Worksheet = new Worksheet(sheetData);

        var sheets = spreadsheet.WorkbookPart!.Workbook!.AppendChild(new Sheets());
        sheets.Append(new Sheet { Id = spreadsheet.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Report" });

        ApplyHeaderRow(sheetData, Headers); 
        AddTableData(sheetData);
        return workbookPart;
    }

    private static void ApplyHeaderRow(SheetData sheetData, List<string> headers)
    {
        var headerRow = new Row();
        headerRow.Append(
            headers.Select(x => (OpenXmlElement)new Cell { CellValue = new CellValue(x), DataType = CellValues.String }).ToArray()
        );
        sheetData.Append(headerRow);
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
        return value is int
            ? new EnumValue<CellValues>(CellValues.Number)
            : new EnumValue<CellValues>(CellValues.String);
    }
}