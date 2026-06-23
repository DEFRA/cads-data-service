using System.ComponentModel;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database;

public enum SchemaName
{
    [Description("public")]
    Public = 0,

    [Description("cts")]
    Cts = 1,

    [Description("cts_audit")]
    CtsAudit = 2,

    [Description("cts_transactions")]
    CtsTransactions = 3,

    [Description("cads")]
    Cads = 4
}