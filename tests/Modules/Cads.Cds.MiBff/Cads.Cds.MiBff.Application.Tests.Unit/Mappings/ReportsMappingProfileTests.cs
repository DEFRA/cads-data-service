using AutoMapper;
using Cads.Cds.MiBff.Application.Reports.Mappings;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Mappings;

public class ReportsMappingProfileTests
{
    private readonly MapperConfiguration _mapperConfiguration;

    public ReportsMappingProfileTests()
    {
        var loggerFactory = LoggerFactory.Create(builder => { });

        var config = new MapperConfigurationExpression();
        config.AddProfile<ReportsMappingProfile>();

        _mapperConfiguration = new MapperConfiguration(config, loggerFactory);
    }

    [Fact]
    public void GivenReportsMappingProfile_ShouldBeValid()
    {
        _mapperConfiguration.AssertConfigurationIsValid();
    }
}