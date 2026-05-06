namespace Cads.Cds.MiBff.Application.Services.Reports;

public class OpenXmlReportGenerator : IOpenXmlReportGenerator
{
    public MemoryStream Generate<T>(List<T> data)
    {
        var report = new OpenXmlReport<T>(GetReportDefinition<T>(), data);
        return report.Generate();
    }

    private IReportDefinition<T> GetReportDefinition<T>()
    {
        var reportType = typeof(IReportDefinition<T>);
        var assembly = reportType.Assembly;
        var candidateDefinitionTypes = assembly!
            .GetTypes()
            .Where(type =>
                type is { IsClass: true, IsAbstract: false } &&
                reportType.IsAssignableFrom(type) &&
                type.GetConstructor(Type.EmptyTypes) is not null)
            .ToList();

        if (!candidateDefinitionTypes.Any())
        {
            throw new InvalidOperationException($"No report definition found for type {typeof(T)}");
        }

        if (candidateDefinitionTypes.Count > 1)
        {
            throw new InvalidOperationException($"Multiple report definitions found for type {typeof(T)}");
        }

        var instance = Activator.CreateInstance(candidateDefinitionTypes.Single());

        return (IReportDefinition<T>)instance!;
    }
}