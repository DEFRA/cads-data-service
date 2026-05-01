namespace Cads.Cds.MiBff.Application.Routing.Reports;

[AttributeUsage(AttributeTargets.Class)]
public class ReportHandlerAttribute(string reportKey) : Attribute
{
    public string ReportKey { get; } = reportKey;
}
