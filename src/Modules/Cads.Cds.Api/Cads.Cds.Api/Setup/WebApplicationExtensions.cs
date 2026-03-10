using CoreWCF.Channels;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Cads.Cds.Api.Application.Soap.ServiceContracts;
using Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

namespace Cads.Cds.Api.Setup;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder UseApiSoapEndpoints(this IApplicationBuilder app)
    {
        app.UseServiceModel(builder =>
        {
            // Create SOAP 1.2 binding for both endpoints (without WS-Addressing)
            var soap12Binding = new CustomBinding(
                new TextMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap12 },
                new HttpTransportBindingElement());

            // Animal Cohorts endpoint
            builder.AddService<AnimalCohortServiceContract>();
            builder.AddServiceEndpoint<AnimalCohortServiceContract, IAnimalCohortServiceContract>(
                    soap12Binding,
                    "/api/soap/AnimalCohorts.asmx");

            // Animal Details endpoint
            builder.AddService<AnimalDetailsServiceContract>();
            builder.AddServiceEndpoint<AnimalDetailsServiceContract, IAnimalDetailsServiceContract>(
                soap12Binding,
                "/api/soap/AnimalDetails.asmx");

            // Animal Passport and Details endpoint
            builder.AddService<AnimalPassportAndDetailsServiceContract>();
            builder.AddServiceEndpoint<AnimalPassportAndDetailsServiceContract, IAnimalPassportAndDetailsServiceContract>(
                    soap12Binding,
                    "/api/soap/AnimalPassportAndDetails.asmx");

            // Cattle Status endpoint
            builder.AddService<CattleStatusServiceContract>();
            builder.AddServiceEndpoint<CattleStatusServiceContract, ICattleStatusServiceContract>(
                    soap12Binding,
                    "/api/soap/CattleStatus.asmx");

            // Livestock Movements endpoint
            builder.AddService<LivestockMovementsServiceContract>();
            builder.AddServiceEndpoint<LivestockMovementsServiceContract, ILivestockMovementsServiceContract>(
                    soap12Binding,
                    "/api/soap/LivestockMovements.asmx");
        });

        return app;
    }
}