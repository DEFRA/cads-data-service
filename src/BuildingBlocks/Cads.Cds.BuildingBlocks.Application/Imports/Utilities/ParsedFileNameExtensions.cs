using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Core.Exceptions;

namespace Cads.Cds.BuildingBlocks.Application.Imports.Utilities;

public static class ParsedFileNameExtensions
{
    public static string? GetDestinationTableName(this CtsmFilename parsedFileName)
    {
        if (Enum.TryParse<ImportActionType>(parsedFileName?.Type ?? string.Empty, true, out var importActionType) == false)
        {
            throw new UnprocessableException($"Invalid import action type '{parsedFileName!.Type}' derived from file name '{parsedFileName!}'.");
        }

        var schemaName = importActionType.GetSchemaName();

        if (schemaName == SchemaName.NotDefined)
        {
            throw new UnprocessableException($"Invalid import action type '{parsedFileName!.Type}' derived from file name '{parsedFileName!}'.");
        }

        var importDataType = Enum.GetValues<ImportDataType>()
           .FirstOrDefault(v => v.GetTableName(schemaName)?.Equals(parsedFileName?.TableName, StringComparison.InvariantCultureIgnoreCase) == true);

        if (importDataType == ImportDataType.None)
        {
            return null;
        }

        return $"{schemaName.GetDescription()}.{parsedFileName!.TableName.ToLower()}";
    }

    public static string GetGroupKey(this CtsmFilename parsedFileName)
    {
        ArgumentNullException.ThrowIfNull(parsedFileName);

        return $"CTSM_" +
            $"{parsedFileName.App}_" +
            $"{parsedFileName.Env}_" +
            $"{parsedFileName.Type}_" +
            $"{parsedFileName.BatchId}_" +
            $"{parsedFileName.TableName}";
    }
}