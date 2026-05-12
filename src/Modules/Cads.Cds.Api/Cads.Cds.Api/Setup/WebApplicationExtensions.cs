using Cads.Cds.Api.Application.Soap.ServiceContracts;
using Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;
using CoreWCF.Channels;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Setup;

public static class WebApplicationExtensions
{
    private static readonly CustomBinding _soap12BindingHttp = new(
        new TextMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap12 },
        new HttpTransportBindingElement());
    private static readonly CustomBinding _soap12BindingHttps = new(
        new TextMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap12 },
        new HttpsTransportBindingElement());

    public static IApplicationBuilder UseApiSoapEndpoints(this IApplicationBuilder app)
    {
        var hasHttps = HasHttpsAddress(app);

        app.UseServiceModel(builder =>
        {
            builder.AddServiceEndpoints<IAnimalCohortServiceContract, AnimalCohortServiceContract>(
                    "/api/soap/AnimalCohorts.asmx", hasHttps)
                .AddServiceEndpoints<IAnimalDetailsServiceContract, AnimalDetailsServiceContract>(
                    "/api/soap/AnimalDetails.asmx", hasHttps)
                .AddServiceEndpoints<IAnimalPassportAndDetailsServiceContract, AnimalPassportAndDetailsServiceContract>(
                    "/api/soap/AnimalPassportAndDetails.asmx", hasHttps)
                .AddServiceEndpoints<ICattleStatusServiceContract, CattleStatusServiceContract>(
                    "/api/soap/CattleStatus.asmx", hasHttps)
                .AddServiceEndpoints<ILivestockMovementsServiceContract, LivestockMovementsServiceContract>(
                    "/api/soap/LivestockMovements.asmx", hasHttps);
        });

        return app;
    }

    private static bool HasHttpsAddress(IApplicationBuilder app)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();

        var aspNetCoreUrls = configuration["ASPNETCORE_URLS"] ?? string.Empty;

        // If URLS is explicitly set, it wins (mirrors ASP.NET Core's own precedence)
        if (!string.IsNullOrEmpty(aspNetCoreUrls))
            return aspNetCoreUrls.Split(';').Any(u => u.StartsWith("https", StringComparison.OrdinalIgnoreCase));

        // Only check HTTPS_PORTS if URLS is not set
        if (!string.IsNullOrEmpty(configuration["ASPNETCORE_HTTPS_PORTS"]))
            return true;

        return configuration.GetSection("Kestrel:Endpoints")
            .GetChildren()
            .Any(e => e["Url"]?.StartsWith("https", StringComparison.OrdinalIgnoreCase) == true);
    }

    private static IServiceBuilder AddServiceEndpoints<TServiceContract, TServiceImplementation>(this IServiceBuilder builder, string path, bool includeHttps)
        where TServiceContract : class
        where TServiceImplementation : class, TServiceContract
    {
        builder.AddService<TServiceImplementation>();
        builder.AddServiceEndpoint<TServiceImplementation, TServiceContract>(
            _soap12BindingHttp,
            path);

        if (includeHttps)
        {
            builder.AddServiceEndpoint<TServiceImplementation, TServiceContract>(
                _soap12BindingHttps,
                path);
        }

        return builder;
    }
}