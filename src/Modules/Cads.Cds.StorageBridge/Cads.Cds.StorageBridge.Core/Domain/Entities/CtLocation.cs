namespace Cads.Cds.StorageBridge.Core.Domain.Entities;

public class CtLocation
{
    public int LocId { get; set; }

    public string? ReceivePpafFlag { get; set; }
    public long? SltId { get; set; }
    public long? LtyId { get; set; }
    public long? CtyId { get; set; }
    public string? ReceiveLabelsFlag { get; set; }

    public DateOnly? EffectiveFrom { get; set; }
    public DateOnly? EffectiveTo { get; set; }

    public string? CessationReason { get; set; }
    public string? PremisesType { get; set; }
    public string? Comments { get; set; }
    public string? MapReference { get; set; }
    public string? SourceIdentifier { get; set; }
    public string? SourceReference { get; set; }

    public string? TelNumber { get; set; }
    public string? MobileNumber { get; set; }
    public string? FaxNumber { get; set; }
    public string? EmailAddress { get; set; }

    public string? CurrentStatus { get; set; }
    public string? CurrentUser { get; set; }
    public DateOnly? CurrentModifiedDate { get; set; }
    public int? CurrentPid { get; set; }

    public string? ReasonCode { get; set; }
    public int? Version { get; set; }

    public int FakeData { get; set; }
    public long? RowNumber { get; set; }
    public string? RecordType { get; set; }
    public long? RecordCount { get; set; }

    public DateTime ImportedDate { get; set; }
}
