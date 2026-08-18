using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;

namespace Cads.Cds.BuildingBlocks.Application.Schema;

public static class EnumExtensions
{
    public static SchemaName GetSchemaName(this ImportActionType importActionType)
    {
        // Map ImportActionType to SchemaName
        // Fix to always return CtsTransactions for both Bulk and Delta
        return importActionType switch
        {
            ImportActionType.Bulk => SchemaName.CtsTransactions,
            ImportActionType.Delta => SchemaName.CtsTransactions,
            _ => SchemaName.NotDefined
        };
    }

    public static char GetTransTypeChar(this ImportActionType importActionType)
    {
        return importActionType switch
        {
            ImportActionType.Bulk => 'B',
            ImportActionType.Delta => 'D',
            _ => throw new ArgumentOutOfRangeException(nameof(importActionType), importActionType, "Unsupported ImportActionType.")
        };
    }
}