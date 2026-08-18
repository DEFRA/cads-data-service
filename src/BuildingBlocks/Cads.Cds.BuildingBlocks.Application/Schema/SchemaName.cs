using System.ComponentModel;

namespace Cads.Cds.BuildingBlocks.Application.Schema;

public enum SchemaName
{
    NotDefined = 0,

    [Description("public")]
    Public = 1,

    [Description("cts")]
    Cts = 2,

    [Description("cts_audit")]
    CtsAudit = 3,

    [Description("cts_transactions")]
    CtsTransactions = 4,

    [Description("cads")]
    Cads = 5
}