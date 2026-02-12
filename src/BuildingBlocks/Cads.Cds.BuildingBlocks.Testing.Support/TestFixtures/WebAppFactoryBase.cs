using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures;

public abstract class WebAppFactoryBase<TStart>(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebApplicationFactory<TStart>
    where TStart : class

{
    private readonly List<Action<IServiceCollection>> _serviceOverrides = [];
    private readonly IDictionary<string, string?> _configOverrides = configOverrides ?? new Dictionary<string, string?>();
    private readonly bool _useFakeAuth = useFakeAuth;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting(WebHostDefaults.ApplicationKey, typeof(TStart).Assembly.FullName);

        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            if (_configOverrides.Count > 0)
                configBuilder.AddInMemoryCollection(_configOverrides);
        });

        builder.ConfigureTestServices(services =>
        {
            if (_useFakeAuth)
            {
                ConfigureFakeAuthorization(services);
            }

            foreach (var apply in _serviceOverrides)
                apply(services);

            services.RemoveAll<IHostedService>();
        });
    }

    public void OverrideService(Action<IServiceCollection> action)
        => _serviceOverrides.Add(action);

    private static void ConfigureFakeAuthorization(IServiceCollection services)
    {
    }
}