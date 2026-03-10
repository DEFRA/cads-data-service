using CoreWCF.Channels;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Cads.Cds.Api.Application.Soap.ServiceContracts;
using Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

namespace Cads.Cds.Api.Setup;

public static class WebApplicationExtensions
{
    private static readonly CustomBinding _soap12BindingHttp = new(
        new TextMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap12 },
        new HttpTransportBindingElement());
    private static readonly CustomBinding _soap12BindingHttps = new(
        new TextMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap12 },
        new HttpsTransportBindingElement());

    public static IApplicationBuilder UseApiSoapEndpoints(this IApplicationBuilder app, bool isDevelopment = false)
    {
        app.UseServiceModel(builder =>
        {
            builder.AddServiceEndpoints<IAnimalCohortServiceContract, AnimalCohortServiceContract>(
                    "/api/soap/AnimalCohorts.asmx", isDevelopment)
                .AddServiceEndpoints<IAnimalDetailsServiceContract, AnimalDetailsServiceContract>(
                    "/api/soap/AnimalDetails.asmx", isDevelopment)
                .AddServiceEndpoints<IAnimalPassportAndDetailsServiceContract, AnimalPassportAndDetailsServiceContract>(
                    "/api/soap/AnimalPassportAndDetails.asmx", isDevelopment)
                .AddServiceEndpoints<ICattleStatusServiceContract, CattleStatusServiceContract>(
                    "/api/soap/CattleStatus.asmx", isDevelopment)
                .AddServiceEndpoints<ILivestockMovementsServiceContract, LivestockMovementsServiceContract>(
                    "/api/soap/LivestockMovements.asmx", isDevelopment);
        });

        return app;
    }

    private static IServiceBuilder AddServiceEndpoints<TServiceContract, TServiceImplementation>(this IServiceBuilder builder, string path, bool isDevelopment = false)
        where TServiceContract : class
        where TServiceImplementation : class, TServiceContract
    {
        builder.AddService<TServiceImplementation>();
        builder.AddServiceEndpoint<TServiceImplementation, TServiceContract>(
            _soap12BindingHttp,
            path);
        if (!isDevelopment)
        {
            builder.AddServiceEndpoint<TServiceImplementation, TServiceContract>(
                _soap12BindingHttps,
                path);
        }

        return builder;
    }
}