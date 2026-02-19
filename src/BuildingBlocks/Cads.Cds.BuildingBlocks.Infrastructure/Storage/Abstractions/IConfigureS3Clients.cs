namespace Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

public interface IConfigureS3Clients
{
    void Configure(IServiceProvider sp);
}