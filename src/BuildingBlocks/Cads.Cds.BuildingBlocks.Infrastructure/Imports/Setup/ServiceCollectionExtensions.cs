using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddImportEnvelope(this IServiceCollection services)
    {
        return services
            .AddTransient<ITablePrefixResolver, TablePrefixResolver>()
            .AddTransient<IBusinessColumnResolver, BusinessColumnResolver>()
            .AddTransient<IEnvelopeTableResolver, EnvelopeTableResolver>();
    }
}