using Amazon.S3;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.MiBff.Infrastructure.Storage.Clients;
using Cads.Cds.MiBff.Infrastructure.Storage.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Infrastructure.Storage.Setup;

public class MiBffS3Configurator(MiBffStorageConfiguration config) : IConfigureS3Clients
{
    public void Configure(IServiceProvider sp)
    {
        var factory = sp.GetRequiredService<IS3ClientFactory>();
        var amazonConfig = sp.GetRequiredService<AmazonS3Config>();

        factory.AddClient<MiBffInternalStorageClient>(
            config.CadsInternal.BucketName,
            config.CadsInternal.HealthcheckEnabled,
            amazonConfig);
    }
}