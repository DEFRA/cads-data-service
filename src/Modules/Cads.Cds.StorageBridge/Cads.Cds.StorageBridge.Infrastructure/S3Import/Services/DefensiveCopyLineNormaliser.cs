using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;

public sealed class DefensiveCopyLineNormaliser : IDefensiveCopyLineNormaliser
{
    private readonly ILogger<DefensiveCopyLineNormaliser> _logger;

    public DefensiveCopyLineNormaliser(ILogger<DefensiveCopyLineNormaliser> logger)
    {
        _logger = logger;
    }

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

        if (_logger.IsEnabled(LogLevel.Warning))
        {
            _logger.LogWarning("Normalising line with {ColumnCount} columns (expected {ExpectedColumnCount}) for import data type {ImportDataType}: {Line}",
                columnCount, expectedColumnCount, importDataType, line);
        }

        lineParts = importDataType switch
        {
            ImportDataType.CtParamValue => NormaliseCtParamValue(lineParts, delimiter, expectedColumnCount),
            ImportDataType.CtSuspenseWgAllocRules => NormaliseCtSuspenseWgAllocRules(lineParts, delimiter, expectedColumnCount),
            ImportDataType.CtMovtCorrectSummaries => NormaliseCtMovtCorrectSummariesRules(lineParts, delimiter, expectedColumnCount),
            _ => lineParts
        };

        var normalisedLine = string.Join(delimiter, lineParts);
        return normalisedLine;
    }

    private static string[] NormaliseCtParamValue(string[] lineParts, char delimiter, int expectedColumnCount)
    {
        var linePartsCount = lineParts.Length;
        // Currently we know of two conditions that fail
        // 1. 16 columns, where we need to combine columns 6-7 and 9-10 into two quoted columns
        // Example: "D|295|1940|CP.GAP_MCMARK|393|SEO|CHR~GAP~BDR|31|SEO GAP BDR|SEO GAP BDR OVERRIDE|1|f800702|1|26-FEB-01||1"
        if (linePartsCount == 16)
        {
            var firstGroup = $"\"{string.Join(delimiter, lineParts.Skip(5).Take(2))}\"";
            var secondGroup = $"\"{string.Join(delimiter, lineParts.Skip(8).Take(2))}\"";
            return
            [
                .. lineParts[..5],
                firstGroup,
                .. lineParts.Skip(7).Take(1),
                secondGroup,
                .. lineParts[10..]
            ];
        }
        // 2. 18 columns, where we need to combine columns 8-12 into a single quoted column
        // Example: "D|2493|20808|CP.IL_HEAT_JAVA_CMD|20420|0|java encryption cmd|/usr/java7_64/jre/bin/java|-jar|/ctsm/app02/ctsal/external/PRCG/CTS_OWN/BIN/encryptionUtil.jar|-e|-i||CTS_OWN|1|25-FEB-25||1"
        if (linePartsCount == 18)
        {
            var group = $"\"{string.Join(delimiter, lineParts.Skip(7).Take(5))}\"";
            return
            [
                .. lineParts[..7],
                group,
                .. lineParts[12..]
            ];
        }
        throw new InvalidOperationException($"CtParamValue normalisation failed. Expected {expectedColumnCount} columns, but got {linePartsCount}. Line parts: {string.Join(delimiter, lineParts)}");
    }

    private static string[] NormaliseCtSuspenseWgAllocRules(string[] lineParts, char delimiter, int expectedColumnCount)
    {
        var linePartsCount = lineParts.Length;
        const int stablePrefixColumnCount = 5;
        const int stableTailColumnCount = 7;
        // Currently we know of one column that causes issues with a stable prefix of 5 columns and a stable tail of 7 columns, where we need to combine the middle columns into a single quoted column
        // Example: "D|245|154|4|40|EPP,RRP,|,PMX,&,RPL,&,^=|||f800702|1|26-SEP-03|99|1"
        if (linePartsCount > expectedColumnCount)
        {
            var combinedColumnsCount = linePartsCount - stablePrefixColumnCount - stableTailColumnCount;
            var group = $"\"{string.Join(delimiter, lineParts.Skip(stablePrefixColumnCount).Take(combinedColumnsCount))}\"";
            return
            [
                .. lineParts[..5],
                group,
                .. lineParts[(linePartsCount - stableTailColumnCount)..]
            ];
        }
        throw new InvalidOperationException($"CtSuspenseWgAllocRules normalisation failed. Expected {expectedColumnCount} columns, but got {linePartsCount}. Line parts: {string.Join(delimiter, lineParts)}");
    }

    private static string[] NormaliseCtMovtCorrectSummariesRules(string[] lineParts, char delimiter, int expectedColumnCount)
    {
        if (lineParts[31] == "Determined Movement Type")
        {
            var group = $"\"{string.Join(delimiter, lineParts.Skip(31).Take(2))}\"";
            return
            [
                .. lineParts[..31],
                group,
                .. lineParts[33..]
            ];
        }

        if (lineParts[30] == "On-line Entry")
        {
            return
            [
                .. lineParts[..29],
                .. lineParts[30..]
            ];
        }

        throw new InvalidOperationException($"CtMovtCorrectSummariesRules normalisation failed. Expected {expectedColumnCount} columns, but got {lineParts.Length}. Line parts: {string.Join(delimiter, lineParts)}");
    }
}