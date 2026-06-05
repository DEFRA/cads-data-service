using AutoFixture.Kernel;
using Cads.Cds.Api.Core.Domain.Entities;
using Cads.Cds.BuildingBlocks.Testing.Support.Specimens.Generators;

namespace Cads.Cds.Api.Testing.Support.Specimens.Builders;

public class LocationSummaryBuilder(
    List<string> identifiers,
    List<DateOnly> dates) : ISpecimenBuilder
{
    private readonly Queue<(string identifier, DateOnly dt)> _pairs =
        new(identifiers.Zip(dates, (i, d) => (i, d)));

    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(LocationSummary))
        {
            if (_pairs.Count == 0)
                return new NoSpecimen();

            var (identifier, dt) = _pairs.Dequeue();
            var (_, _, _, _, address5, _) = AddressGenerator.GenerateAddress();

            return new LocationSummary
            {
                LidIdentifier = identifier,
                LidFullIdentifier = $"AH-{identifier}",
                LidSubIdentifier = null,
                LidEffectiveFromDate = dt,
                LidEffectiveToDate = null,
                LidCurrentModifiedDate = dt,

                LtyShortDescription = null,
                LtyLongDescription = null,

                LocMapReference = AddressGenerator.GenerateMapReference(),
                LocEffectiveFrom = dt,
                LocEffectiveTo = null,
                LocCessationReason = null,
                LocComments = null,
                LocSourceIdentifier = null,
                LocTelNumber = CommunicationGenerator.GenerateTelephoneNumber(),
                LocMobileNumber = CommunicationGenerator.GenerateMobileNumber(),
                LocFaxNumber = null,
                LocEmailAddress = CommunicationGenerator.GenerateEmail(),
                LocCurrentModifiedDate = dt,
                LocReasonCode = null,
                LocVersion = 1,

                CtyName = address5,
                CtyVetAreaDesc = null,
                CtyPassportAreaDesc = null,
                CtyAdminOffice = null,
                CtyBcmsTeam = null,
                CtyInspectionArea = null,
                CtyDataMgtAreaDesc = null,
                CtyCurrentStatus = "1",

                LifDescription = null,
                ImportedDate = DateTime.UtcNow
            };
        }

        return new NoSpecimen();
    }
}