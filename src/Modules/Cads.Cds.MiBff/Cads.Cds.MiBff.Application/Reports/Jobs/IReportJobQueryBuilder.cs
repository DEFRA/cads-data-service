using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Application.Reports.Jobs;

public interface IReportJobQueryBuilder
{
    object Build(MiReportJob job);
}