using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Cads.Cds.MiBff.Application.Services.Reports;

public static class OpenXmlHelper
{
    public static EnumValue<CellValues> MapValueToExcelType(this IConvertible value)
    {
        if (value is int || value is long)
            return new EnumValue<CellValues>(CellValues.Number);

        if (value is string)
            return new EnumValue<CellValues>(CellValues.String);

        throw new ArgumentException($"Unsupported value type for excel report builder - {value.GetType()}");
    }

    public static int GetColumnIndex(this StringValue cellReference)
    {
        var value = cellReference.Value!;
        int offset = 0;
        int index = 0;
        while (value[offset] >= 'A' && value[offset] <= 'Z')
        {
            index *= 26;
            index += value[offset] - 'A' + 1;
            offset++;
        }

        return index;
    }
}