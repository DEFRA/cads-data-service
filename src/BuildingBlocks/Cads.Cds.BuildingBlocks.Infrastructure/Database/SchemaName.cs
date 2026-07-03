using System.ComponentModel;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database;

public enum SchemaName
{
    NotDefined = 0,

    [Description("public")]
    Public = 12,

    [Description("cts")]
    Cts = 1,

    [Description("cts_audit")]
    CtsAudit = 3,

    [Description("cts_transactions")]
    CtsTransactions = 4,

    [Description("cads")]
    Cads = 5
}