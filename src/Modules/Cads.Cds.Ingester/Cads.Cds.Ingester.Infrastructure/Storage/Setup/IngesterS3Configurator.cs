using Amazon.S3;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.Ingester.Infrastructure.Storage.Clients;
using Cads.Cds.Ingester.Infrastructure.Storage.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Ingester.Infrastructure.Storage.Setup;

public class IngesterS3Configurator(IngesterStorageConfiguration config) : IConfigureS3Clients
{
    public void Configure(IServiceProvider sp)
    {
        var factory = sp.GetRequiredService<IS3ClientFactory>();
        var amazonConfig = sp.GetRequiredService<AmazonS3Config>();

        factory.AddClient<CadsIngesterClient>(
            config.CadsIngester.BucketName,
            amazonConfig);
    }
}