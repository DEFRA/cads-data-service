namespace Cads.Cds.MiBff.Core.DTOs.Reports;

public class XlsxReport<T>
{
    public List<string> Headers { get; set; }
    public List<T> Data { get; set; }
    public List<Func<T, string>> Selectors { get; set; }

    public void Generate(string filePath)
    {
        using var spreadsheet = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);

        var workbookPart = BuildWorkbook(spreadsheet);
        workbookPart.Workbook!.Save();
    }
    
    public void Generate(MemoryStream stream)
    {
        using var spreadsheet = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);

        var workbookPart = BuildWorkbook(spreadsheet);
        workbookPart.Workbook!.Save();
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

    private void AddTableData(SheetData sheetData)
    {
        foreach (var rowData in Data)
        {
            var row = new Row();
            foreach (var columnMapper in Selectors)
            {
                row.Append(new Cell { CellValue = new CellValue(columnMapper(rowData)), DataType = CellValues.String });
            }
            sheetData.Append(row);
        }
    }
}