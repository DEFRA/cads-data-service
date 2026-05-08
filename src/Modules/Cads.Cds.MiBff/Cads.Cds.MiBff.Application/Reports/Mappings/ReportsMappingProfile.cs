using AutoMapper;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Reports.Mappings;

public class ReportsMappingProfile : Profile
{
    public ReportsMappingProfile()
    {
        CreateMap<MiEffectiveReportPermission, ReportDto>();
    }
}