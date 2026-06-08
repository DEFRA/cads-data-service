namespace Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;

public interface IReportRegistry
{
    (IReportHandler Handler, Type RequestType) Resolve(string reportKey, IServiceProvider provider);
}