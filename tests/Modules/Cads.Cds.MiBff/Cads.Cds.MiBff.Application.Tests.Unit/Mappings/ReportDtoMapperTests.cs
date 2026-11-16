using Cads.Cds.MiBff.Application.Reports.Mappings;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Mappings;

public class ReportDtoMapperTests
{
    [Fact]
    public void GivenReportPermission_ShouldMapDtoProperties()
    {
        var permission = new MiEffectiveReportPermission
        {
            ReportId = Guid.NewGuid(),
            ReportKey = "report-key",
            Title = "Report title",
            Description = "Report description",
            IsActive = true,
            DisplayName = "Display name",
            ExternalSubject = "user@example.com",
            Granted = true
        };

        var result = ReportDtoMapper.Map(permission);

        Assert.Equal(permission.ReportId, result.ReportId);
        Assert.Equal(permission.ReportKey, result.ReportKey);
        Assert.Equal(permission.Title, result.Title);
        Assert.Equal(permission.Description, result.Description);
        Assert.Equal(permission.IsActive, result.IsActive);
    }

    [Fact]
    public void GivenReportPermissionWithoutDescription_ShouldMapEmptyDescription()
    {
        var permission = new MiEffectiveReportPermission
        {
            ReportKey = "report-key",
            Title = "Report title",
            ExternalSubject = "user@example.com"
        };

        var result = ReportDtoMapper.Map(permission);

        Assert.Equal(string.Empty, result.Description);
    }
}