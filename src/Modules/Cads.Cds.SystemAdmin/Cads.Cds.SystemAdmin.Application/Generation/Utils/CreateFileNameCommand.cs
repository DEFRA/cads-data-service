using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;

namespace Cads.Cds.SystemAdmin.Application.Generation.Utils;

public record CreateFileNameCommand
{
    public string Prefix { get; set; } = "CTSM";
    public required string App { get; set; }
    public string Env { get; set; } = "DEV";
    public required ImportActionType Type { get; set; }
    public required string BatchId { get; set; }
    public string? PartNo { get; set; }
    public required string TableName { get; set; }
    public required DateTime Timestamp { get; set; }
    public string Extension { get; set; } = ".csv";
}