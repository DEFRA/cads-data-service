using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;

public abstract class TestFixtureBase<TStart, TFactory>
    where TStart : class
    where TFactory : WebAppFactoryBase<TStart>
{
    public readonly HttpClient HttpClient;
    public readonly TFactory Factory;

    protected TestFixtureBase(TFactory factory, bool useFakeAuth = false)
    {
        Factory = factory;
        HttpClient = factory.CreateClient();

        if (useFakeAuth)
            HttpClient.AddJwt();
        else
            HttpClient.AddBasicApiKey(TestAuthConstants.BasicApiKey, TestAuthConstants.BasicSecret);
    }
}