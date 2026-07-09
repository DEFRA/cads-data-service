using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Application.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;

namespace Cads.Cds.BuildingBlocks.Application.Schema;

public static class SchemaHelper
{
    public static string GetDestinationTableNameFromFilename(string filename)
    {
        var parsedFilename = CtsmFilenameParser.Parse(filename);

        var schemaName = parsedFilename?.Type.ToLower() switch
        {
            "bulk" => SchemaName.Cts,
            "delta" => SchemaName.CtsTransactions,
            _ => SchemaName.NotDefined
        };

        if (schemaName == SchemaName.NotDefined)
        {
            throw new UnprocessableException($"Invalid import action type '{parsedFilename!.Type}' derived from file name '{parsedFilename!.TableName}'.");
        }

        return $"{schemaName.GetDescription()}.{parsedFilename!.TableName}";
    }
}