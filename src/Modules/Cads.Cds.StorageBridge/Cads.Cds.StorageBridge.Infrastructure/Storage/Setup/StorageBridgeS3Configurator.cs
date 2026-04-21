using Amazon.S3;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.StorageBridge.Infrastructure.Storage.Setup;

public class StorageBridgeS3Configurator(StorageBridgeStorageConfiguration config) : IConfigureS3Clients
{
    private readonly StorageBridgeStorageConfiguration _config = config;

    public void Configure(IServiceProvider sp)
    {
        var factory = sp.GetRequiredService<IS3ClientFactory>();
        var amazonConfig = sp.GetRequiredService<AmazonS3Config>();

        factory.AddClient<CadsInternalClient>(
            _config.CadsInternal.BucketName,
            amazonConfig);

        factory.AddClientWithCredentials<CadsExternalClient>(
            _config.CadsExternal.BucketName,
            _config.CadsExternal.AccessKeySecretName,
            _config.CadsExternal.SecretKeySecretName,
            amazonConfig);
    }
}