using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;

namespace Cads.Cds.BuildingBlocks.Application.Schema;

public static class SchemaHelper
{
    public static string GetDestinationTableNameFromFilename(string filename)
    {
        var parsedFilename = CtsmFilenameParser.Parse(filename);

        if (Enum.TryParse<ImportActionType>(parsedFilename?.Type ?? string.Empty, true, out var importActionType) == false)
        {
            throw new UnprocessableException($"Invalid import action type '{parsedFilename!.Type}' derived from file name '{filename}'.");
        }

        var schemaName = importActionType.GetSchemaName();

        if (schemaName == SchemaName.NotDefined)
        {
            throw new UnprocessableException($"Invalid import action type '{parsedFilename!.Type}' derived from file name '{filename}'.");
        }

        return $"{schemaName.GetDescription()}.{parsedFilename!.TableName.ToLower()}";
    }
}