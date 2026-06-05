using Cads.Cds.Api.Core.Domain.Entities;
using Cads.Cds.Api.Core.DTOs;

namespace Cads.Cds.Api.Application.Queries.Locations;

public static class LocationMappingExtensions
{
    public static LocationDto ToDto(this LocationSummary s)
    {
        if (s is null) return null!;

        return new LocationDto
        {
            LidIdentifier = s.LidIdentifier,
            LidFullIdentifier = s.LidFullIdentifier,
            LidSubIdentifier = s.LidSubIdentifier,
            LidEffectiveFromDate = s.LidEffectiveFromDate,
            LidEffectiveToDate = s.LidEffectiveToDate,
            LidCurrentModifiedDate = s.LidCurrentModifiedDate,

            LtyShortDescription = s.LtyShortDescription,
            LtyLongDescription = s.LtyLongDescription,

            LocMapReference = s.LocMapReference,
            LocEffectiveFrom = s.LocEffectiveFrom,
            LocEffectiveTo = s.LocEffectiveTo,
            LocCessationReason = s.LocCessationReason,
            LocComments = s.LocComments,
            LocSourceIdentifier = s.LocSourceIdentifier,
            LocTelNumber = s.LocTelNumber,
            LocMobileNumber = s.LocMobileNumber,
            LocFaxNumber = s.LocFaxNumber,
            LocEmailAddress = s.LocEmailAddress,
            LocCurrentModifiedDate = s.LocCurrentModifiedDate,
            LocReasonCode = s.LocReasonCode,
            LocVersion = s.LocVersion,

            CtyName = s.CtyName,
            CtyVetAreaDesc = s.CtyVetAreaDesc,
            CtyPassportAreaDesc = s.CtyPassportAreaDesc,
            CtyAdminOffice = s.CtyAdminOffice,
            CtyBcmsTeam = s.CtyBcmsTeam,
            CtyInspectionArea = s.CtyInspectionArea,
            CtyDataMgtAreaDesc = s.CtyDataMgtAreaDesc,
            CtyCurrentStatus = s.CtyCurrentStatus,

            LifDescription = s.LifDescription,
            ImportedDate = s.ImportedDate
        };
    }

    public static IEnumerable<LocationDto> ToDtoList(this IEnumerable<LocationSummary> items)
        => items.Select(ToDto);
}