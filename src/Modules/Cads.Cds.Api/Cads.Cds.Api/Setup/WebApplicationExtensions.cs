using Cads.Cds.Api.Application.Services;
using CoreWCF.Channels;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using System.Text;

namespace Cads.Cds.Api.Setup;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder UseApiSoapEndpoints(this IApplicationBuilder app)
    {
        app.UseServiceModel(builder =>
        {
            // Create SOAP 1.2 binding for both endpoints (without WS-Addressing)
            var soap12Binding = new CustomBinding(
                new TextMessageEncodingBindingElement(MessageVersion.Soap12, Encoding.UTF8),
                new HttpTransportBindingElement
                {
                    MaxReceivedMessageSize = int.MaxValue,
                    MaxBufferSize = int.MaxValue
                }
            );

            // Cattle Status endpoint - uses XmlSerializer for simple parameter unwrapping
            builder.AddService<CattleStatusService>(serviceOptions =>
            {
                serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
            })
                .AddServiceEndpoint<CattleStatusService, ICattleStatusService>(
                    soap12Binding,
                    "/api/soap/CattleStatus.asmx");

            // Livestock Movements endpoint
            builder.AddService<LivestockMovementsService>(wcfOptions =>
            {
                wcfOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
            })
                .AddServiceEndpoint<LivestockMovementsService, ILivestockMovementsService>(
                    soap12Binding,
                    "/api/soap/LivestockMovements.asmx");
        });

        return app;
    }
}