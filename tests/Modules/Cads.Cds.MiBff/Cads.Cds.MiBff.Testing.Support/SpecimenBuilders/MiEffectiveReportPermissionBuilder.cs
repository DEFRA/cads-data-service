using AutoFixture.Kernel;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Testing.Support.SpecimenBuilders;

public class MiEffectiveReportPermissionBuilder(
    List<MiReport> reports,
    List<MiUser> users) : ISpecimenBuilder
{
    private readonly Queue<(MiReport report, MiUser user)> _pairs = new(
        from u in users
        from r in reports
        select (r, u)
    );

    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(MiEffectiveReportPermissionView))
        {
            if (_pairs.Count == 0)
                return new NoSpecimen();

            var (report, user) = _pairs.Dequeue();

            return new MiEffectiveReportPermissionView
            {
                ReportId = report.ReportId,
                ReportKey = report.ReportKey,
                Title = report.Title,
                Description = report.Description,
                IsActive = report.IsActive,
                DisplayName = user.DisplayName,
                ExternalSubject = user.ExternalSubject,
                Granted = true
            };
        }

        return new NoSpecimen();
    }
}
