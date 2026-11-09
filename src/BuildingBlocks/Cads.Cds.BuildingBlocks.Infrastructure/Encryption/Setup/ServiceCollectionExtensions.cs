using Cads.Cds.BuildingBlocks.Application.Encryption;
using Cads.Cds.BuildingBlocks.Infrastructure.Encryption.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Encryption.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Encryption.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEncryption(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<EncryptionConfig>()
            .Bind(configuration.GetSection(EncryptionConfig.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddTransient<IMasterKeyProvider, InMemoryMasterKeyProvider>();
        services.AddTransient<IFileEncryptor, FileEncryptor>();

        return services;
    }
}