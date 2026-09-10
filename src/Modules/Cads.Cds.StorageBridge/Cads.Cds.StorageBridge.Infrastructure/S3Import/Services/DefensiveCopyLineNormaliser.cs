using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.StorageBridge.Application.S3Import.Services;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;

public sealed class DefensiveCopyLineNormaliser : IDefensiveCopyLineNormaliser
{
    public string Normalise(
        string line,
        ImportDataType importDataType,
        char delimiter,
        int expectedColumnCount)
    {
        var lineParts = line.Split(delimiter);
        var columnCount = lineParts.Length;
        if (columnCount == expectedColumnCount)
        {
            return line;
        }

        lineParts = importDataType switch
        {
            ImportDataType.CtParamValue => NormaliseCtParamValue(lineParts, delimiter, expectedColumnCount),
            ImportDataType.CtSuspenseWgAllocRules => NormaliseCtSuspenseWgAllocRules(lineParts, delimiter, expectedColumnCount),
            ImportDataType.CtMovtCorrectSummaries => NormaliseCtMovtCorrectSummariesRules(lineParts, delimiter, expectedColumnCount),
            _ => lineParts
        };
        return string.Join(delimiter, lineParts);
    }

    private static string[] NormaliseCtParamValue(string[] lineParts, char delimiter, int expectedColumnCount)
    {
        // Currently we know the first 5 columns are ok
        var newLineParts = new List<string>(lineParts.Take(5));
        var currentColumnIndex = 5;

        if (!int.TryParse(lineParts[currentColumnIndex + 1], out _))
        {
            // if the 6th column is not an integer, we assume the 6th and 7th columns are actually part of a single value that was split by the delimiter
            newLineParts.Add($"\"{lineParts[currentColumnIndex]}{delimiter}{lineParts[++currentColumnIndex]}\"");
        }
        else
        {
            newLineParts.Add(lineParts[currentColumnIndex]);
        }

        currentColumnIndex++;
        newLineParts.Add(lineParts[currentColumnIndex]);

        currentColumnIndex++;
        if (!int.TryParse(lineParts[currentColumnIndex + 1], out _))
        {
            newLineParts.Add($"\"{lineParts[currentColumnIndex]}{delimiter}{lineParts[++currentColumnIndex]}\"");
        }
        else
        {
            newLineParts.Add(lineParts[currentColumnIndex]);
        }

        newLineParts.AddRange(lineParts[(currentColumnIndex + 1)..]);

        if (newLineParts.Count != expectedColumnCount)
        {
            throw new InvalidOperationException($"Normalisation failed. Expected {expectedColumnCount} columns, but got {newLineParts.Count}.");
        }

        return newLineParts.ToArray();
    }

    private static string[] NormaliseCtSuspenseWgAllocRules(string[] lineParts, char delimiter, int expectedColumnCount)
    {
        if (lineParts.Length <= expectedColumnCount)
        {
            return lineParts;
        }

        const int stablePrefixColumnCount = 5;
        const int stableTailColumnCount = 7;

        var newLineParts = new List<string>(lineParts.Take(5));
        var combinedColumnsCount = lineParts.Length - stablePrefixColumnCount - stableTailColumnCount;
        var combinedColumns = string.Join(delimiter, lineParts.Skip(stablePrefixColumnCount).Take(combinedColumnsCount));
        newLineParts.Add($"\"{combinedColumns}\"");
        newLineParts.AddRange(lineParts.Skip(lineParts.Length - stableTailColumnCount));
        return newLineParts.ToArray();
    }
    
    private static string[] NormaliseCtMovtCorrectSummariesRules(string[] lineParts, char delimiter, int expectedColumnCount)
    {
        if (lineParts.Length <= expectedColumnCount)
        {
            return lineParts;
        }

        if (lineParts[31] == "Determined Movement Type")
        {
            var newLineParts = new List<string>(lineParts.Take(31));
            var combinedColumns = string.Join(delimiter, lineParts.Skip(31).Take(2));
            newLineParts.Add($"\"{combinedColumns}\"");
            newLineParts.AddRange(lineParts.Skip(33));
            return newLineParts.ToArray();
        }

        if (lineParts[30] == "On-line Entry")
        {
            var newLineParts = new List<string>(lineParts.Take(29));
            newLineParts.AddRange(lineParts.Skip(30));
            return newLineParts.ToArray();
        }

        return lineParts;
    }
}