using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Files.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Files.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileInfrastructure(this IServiceCollection services)
    {
        return services
            .AddTransient<IFileService, FileService>();
    }
}