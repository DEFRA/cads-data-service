using AutoMapper;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Mappings;

public class ReportsMappingProfile : Profile
{
    public ReportsMappingProfile()
    {
        CreateMap<MiEffectiveReportPermissionView, ReportDto>()
            .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => src.Granted))
            .ForMember(dest => dest.ReportId,
                opt => opt.MapFrom(src => Guid.Empty));
    }
}