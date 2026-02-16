using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Setup;

public static class ServiceCollectionExtensions
{
    public static void AddAmazonSQSDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration["LOCALSTACK_ENDPOINT"] != null)
        {
            services.AddSingleton<IAmazonSQS>(sp =>
            {
                var config = new AmazonSQSConfig
                {
                    ServiceURL = configuration["AWS:ServiceURL"],
                    AuthenticationRegion = configuration["AWS:Region"],
                    UseHttp = true
                };
                var credentials = new Amazon.Runtime.BasicAWSCredentials("test", "test");
                return new AmazonSQSClient(credentials, config);
            });
        }
        else
        {
            services.AddAWSService<IAmazonSQS>();
        }
    }
}