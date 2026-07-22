using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Core.Domain.Extensions;

public static class DomainExtensions
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
}