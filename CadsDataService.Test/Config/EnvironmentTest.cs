using Microsoft.AspNetCore.Builder;

namespace CadsDataService.Test.Config;

public class EnvironmentTest
{

    [Fact]
    public void IsNotDevModeByDefault()
    {
        var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions());
        var isDev = CadsDataService.Config.Environment.IsDevMode(builder);
        Assert.False(isDev);
    }
}