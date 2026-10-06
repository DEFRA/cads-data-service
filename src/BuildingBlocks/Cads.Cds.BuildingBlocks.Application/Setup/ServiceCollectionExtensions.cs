using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Cads.Cds.BuildingBlocks.Application.OpenXml;
using Cads.Cds.BuildingBlocks.Core.OpenXml;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBuildBlocksApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IRequestExecutor, RequestExecutor>();

        services.AddTransient<IOpenXmlReportGenerator, OpenXmlReportGenerator>();

        // Singleton so the batch id counter that keeps generated filenames apart is shared for the run.
        services.AddSingleton<ICtsmFilenameGenerator, CtsmFilenameGenerator>();

        return services;
    }
}