using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Application.Reports.Routing;
using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Reports.Routing;

public class ReportRegistryTests
{
    private readonly IServiceProvider _provider;

    public ReportRegistryTests()
    {
        var services = new ServiceCollection();

        services.AddTransient<TestHandler1>();
        services.AddTransient<TestHandler2>();

        _provider = services.BuildServiceProvider();
    }

    [Fact]
    public void Constructor_ShouldDiscoverHandlers()
    {
        var registry = new ReportRegistry([typeof(ReportRegistryTests).Assembly]);

        var handlerTypesField = typeof(ReportRegistry)
            .GetField("_handlerTypes", BindingFlags.NonPublic | BindingFlags.Instance);

        var dict = (Dictionary<string, (Type Handler, Type Request)>)handlerTypesField!.GetValue(registry)!;

        dict.Should().ContainKey("TestReport1");
        dict.Should().ContainKey("TestReport2");

        dict["TestReport1"].Handler.Should().Be<TestHandler1>();
        dict["TestReport1"].Request.Should().Be<TestRequest1>();

        dict["TestReport2"].Handler.Should().Be<TestHandler2>();
        dict["TestReport2"].Request.Should().Be<TestRequest2>();
    }

    [Fact]
    public void Resolve_ShouldReturnHandlerAndRequestType()
    {
        var registry = new ReportRegistry([typeof(ReportRegistryTests).Assembly]);

        var (handler, requestType) = registry.Resolve("TestReport1", _provider);

        handler.Should().BeOfType<TestHandler1>();
        requestType.Should().Be<TestRequest1>();
    }

    [Fact]
    public void Resolve_ShouldThrow_WhenReportKeyUnknown()
    {
        var registry = new ReportRegistry([typeof(ReportRegistryTests).Assembly]);

        Action act = () => registry.Resolve("DoesNotExist", _provider);

        act.Should().Throw<KeyNotFoundException>()
            .WithMessage("Unknown reportKey: DoesNotExist");
    }

    [Fact]
    public void Resolve_ShouldUseServiceProviderToCreateHandler()
    {
        var registry = new ReportRegistry([typeof(ReportRegistryTests).Assembly]);

        var (handler, _) = registry.Resolve("TestReport2", _provider);

        handler.Should().BeOfType<TestHandler2>();
    }

    [ReportHandler("TestReport1", typeof(TestRequest1))]
    public class TestHandler1 : IReportHandler
    {
        public object BuildUntypedQuery(GetReportRequest request) => throw new NotImplementedException();
    }

    public class TestRequest1 { }

    [ReportHandler("TestReport2", typeof(TestRequest2))]
    public class TestHandler2 : IReportHandler
    {
        public object BuildUntypedQuery(GetReportRequest request) => throw new NotImplementedException();
    }

    public class TestRequest2 { }
}