using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Core.Domain.Extensions;

public static class DomainExtensions
{
    public static SchemaName GetSchemaName(this ImportActionType importActionType)
    {
        return importActionType switch
        {
            ImportActionType.Bulk => SchemaName.Cts,
            ImportActionType.Delta => SchemaName.CtsTransactions,
            _ => SchemaName.NotDefined
        };
    }

}
