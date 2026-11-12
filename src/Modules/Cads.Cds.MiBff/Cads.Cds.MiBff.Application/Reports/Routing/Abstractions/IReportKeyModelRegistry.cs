namespace Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;

public interface IReportKeyModelRegistry
{
    bool TryGetModelType(string reportKey, out Type? modelType);
}

