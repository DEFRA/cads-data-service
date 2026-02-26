namespace Cads.Cds.BuildingBlocks.Core.Configuration;

public class StaticDataConfig
{
    public const string SectionName = "StaticData";

    public bool Enabled { get; set; } = false;

    public string Path { get; set; } = string.Empty;
}