namespace Cads.Cds.MiBff.Application.Configuration;

public class MiBffConfig
{
    public const string SectionName = "Modules:MiBff";

    public StaticDataConfig StaticData { get; set; } = new StaticDataConfig();
}