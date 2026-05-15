using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Cads.Cds.BuildingBlocks.Application.OpenXml;

public static class OpenXmlHelper
{
    extension(IConvertible value)
    {
        public CellValue MapValueToExcelValue()
        {
            return value switch
            {
                int or long => new CellValue(value.ToString(CultureInfo.InvariantCulture)),
                string s => new CellValue(s),
                DateTime time => new CellValue(time.ToOADate().ToString(CultureInfo.InvariantCulture)),
                _ => throw new ArgumentException($"Unsupported value type for excel report builder - {value.GetType()}")
            };
        }

        public EnumValue<CellValues>? MapValueToExcelType()
        {
            return value switch
            {
                int or long => new EnumValue<CellValues>(CellValues.Number),
                string => new EnumValue<CellValues>(CellValues.String),
                DateTime => null,
                _ => throw new ArgumentException($"Unsupported value type for excel report builder - {value.GetType()}")
            };
        }
    }

    extension(StringValue cellReference)
    {
        /// <summary>
        /// Get the 1-based index of the column
        /// </summary>
        public int GetIntegerColumnIndex()
        {
            var value = cellReference.Value!;
            var offset = 0;
            var index = 0;
            while (value[offset] >= 'A' && value[offset] <= 'Z')
            {
                index *= 26;
                index += value[offset] - 'A' + 1;
                offset++;
            }

            return index;
        }

        public string GetColumnIndexPart()
        {
            var value = cellReference.Value!;
            var indexOfRowPart = value.IndexOfAny(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']);
            return value.Substring(0, indexOfRowPart);
        }
    }

    public static void ReindexTo(this Row row, uint newIndex)
    {
        row.RowIndex = newIndex;

        foreach (var cell in row.ChildElements.OfType<Cell>())
        {
            cell.ReindexTo(cell.CellReference!.GetColumnIndexPart(), newIndex);
        }
    }

    public static void ReindexTo(this Cell cell, string columnIndex, uint rowIndex)
    {
        cell.CellReference = $"{columnIndex}{rowIndex}";
    }
}