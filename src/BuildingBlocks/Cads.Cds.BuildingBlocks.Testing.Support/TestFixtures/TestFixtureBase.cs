using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures;

public abstract class TestFixtureBase<TStart, TFactory>
    where TStart : class
    where TFactory : WebAppFactoryBase<TStart>
{
    public readonly HttpClient HttpClient;
    public readonly TFactory Factory;

    private const string BasicApiKey = "ApiKey";
    private const string BasicSecret = "integration-test-secret";

    protected TestFixtureBase(TFactory factory, bool useFakeAuth = false)
    {
        Factory = factory;
        HttpClient = factory.CreateClient();

        if (useFakeAuth)
            HttpClient.AddJwt();
        else
            HttpClient.AddBasicApiKey(BasicApiKey, BasicSecret);
    }
}