namespace Cads.Cds.MiBff.Application.Reports.Routing;

[AttributeUsage(AttributeTargets.Class)]
public class ReportHandlerAttribute(string reportKey, Type requestType) : Attribute
{
    public string ReportKey { get; } = reportKey;
    public Type RequestType { get; } = requestType;
}