using Cads.Cds.Api.Application.Services;
using Microsoft.AspNetCore.Routing;
using SoapCore;

namespace Cads.Cds.Api.Setup;

public static class WebApplicationExtensions
{
    public static IEndpointRouteBuilder UseApiSoapEndpoints(this IEndpointRouteBuilder app)
    {
        app.UseSoapEndpoint<ICattleStatusService>(options =>
        {
            options.Path = "/api/soap/CattleStatus.asmx";
            options.SoapSerializer = SoapSerializer.XmlSerializer;
            options.EncoderOptions = new[]
            {
                new SoapEncoderOptions
                {
                    MessageVersion = System.ServiceModel.Channels.MessageVersion.Soap12WSAddressing10,
                    WriteEncoding = System.Text.Encoding.UTF8
                }
            };
        });

        return app;
    }
}