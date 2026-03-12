using System.Xml.Serialization;
using Cads.Cds.Api.Application.Soap.Messages.AnimalCohort;
using FluentAssertions;

namespace Cads.Cds.Api.Application.Tests.Unit.Soap.Messages.AnimalCohort;

public class GetAnimalCohortRequestDeserializationTests
{
    [Fact]
    public void Should_Deserialize_GetAnimalCohortRequest_From_Valid_Xml()
    {
        // Arrange
        var xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<ah_lsmoves:GetAnimalCohortRequest xmlns:ah_lsmoves=""http://services.defra.gov.uk/ahw/livestockmovements""
    xmlns:ah_ref_data_sets=""http://types.defra.gov.uk/ahw/common/referencedatasets""
    xmlns:ah_lsmovestype=""http://types.defra.gov.uk/ahw/livestockmovements""
    xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
    xmlns:general=""http://general.types.ws.cara.defra.com""
    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
    <ServiceOptions>
        <DestinationDataBaseName>TMBRP</DestinationDataBaseName>
        <DestinationStoredProcedure>REGISTER_TRACING_COHORT</DestinationStoredProcedure>
    </ServiceOptions>
    <AnimalCohortQuery>
        <TraceIdentifier>
            <TraceSpecificationIdentifier>99999</TraceSpecificationIdentifier>
            <TraceIdentifier>26088</TraceIdentifier>
        </TraceIdentifier>
        <Locations>
            <Location>
                <WindowEndDate>2012-02-15</WindowEndDate>
                <WindowStartDate>2010-02-15</WindowStartDate>
                <TargetLocationIdentifier>07/094/0117</TargetLocationIdentifier>
                <TargetLocationIdentifierType RefDataSetName=""Disease Type"">
                    <ah_ref_data_sets:Code>CPH</ah_ref_data_sets:Code>
                </TargetLocationIdentifierType>
            </Location>
        </Locations>
        <Gender RefDataSetName=""Sex"">
            <ah_ref_data_sets:Code>M</ah_ref_data_sets:Code>
        </Gender>
        <SpeciesCodesAndAnimals>
            <SpeciesCodeAndAnimals>
                <AnimalSpecies RefDataSetName=""Animal Species"">
                    <ah_ref_data_sets:Code>CTT</ah_ref_data_sets:Code>
                </AnimalSpecies>
                <AnimalIdentifiers>
                    <AnimalIdentifier>
                        <AnimalIdentifier>105358479</AnimalIdentifier>
                        <AnimalIdentifierType RefDataSetName=""Animal Identifier Type"">
                            <ah_ref_data_sets:Code>CTSRANID</ah_ref_data_sets:Code>
                        </AnimalIdentifierType>
                    </AnimalIdentifier>
                </AnimalIdentifiers>
            </SpeciesCodeAndAnimals>
        </SpeciesCodesAndAnimals>
        <DateOfBirth>2011-02-15</DateOfBirth>
        <BirthLocationIdentifier>07/094/0117</BirthLocationIdentifier>
        <BirthLocationIdentifierType RefDataSetName=""Disease Type"">
            <ah_ref_data_sets:Code>CPH</ah_ref_data_sets:Code>
        </BirthLocationIdentifierType>
    </AnimalCohortQuery>
</ah_lsmoves:GetAnimalCohortRequest>";

        var serializer = new XmlSerializer(typeof(GetAnimalCohortRequest));

        // Act
        using (var reader = new StringReader(xmlContent))
        {
            var request = (GetAnimalCohortRequest?)serializer.Deserialize(reader);

            // Assert
            request.Should().NotBeNull();
            request!.ServiceOptions.Should().NotBeNull();
            request.ServiceOptions!.DestinationDataBaseName.Should().Be("TMBRP");
            request.ServiceOptions.DestinationStoredProcedure.Should().Be("REGISTER_TRACING_COHORT");

            request.AnimalCohortQuery.Should().NotBeNull();
            request.AnimalCohortQuery!.TraceIdentifier.Should().NotBeNull();
            request.AnimalCohortQuery.TraceIdentifier!.TraceSpecificationIdentifier.Should().Be("99999");
            request.AnimalCohortQuery.TraceIdentifier.TraceIdentifierValue.Should().Be("26088");

            request.AnimalCohortQuery.Locations.Should().NotBeNull();
            request.AnimalCohortQuery.Locations!.Location.Should().HaveCount(1);
            request.AnimalCohortQuery.Locations.Location[0].WindowStartDate.Should().Be("2010-02-15");
            request.AnimalCohortQuery.Locations.Location[0].WindowEndDate.Should().Be("2012-02-15");
            request.AnimalCohortQuery.Locations.Location[0].TargetLocationIdentifier.Should().Be("07/094/0117");

            request.AnimalCohortQuery.Gender.Should().NotBeNull();
            request.AnimalCohortQuery.Gender!.Code.Should().Be("M");
            request.AnimalCohortQuery.Gender.RefDataSetName.Should().Be("Sex");

            request.AnimalCohortQuery.SpeciesCodesAndAnimals.Should().NotBeNull();
            request.AnimalCohortQuery.SpeciesCodesAndAnimals!.SpeciesCodeAndAnimalsList.Should().HaveCount(1);
            request.AnimalCohortQuery.SpeciesCodesAndAnimals.SpeciesCodeAndAnimalsList[0].AnimalSpecies.Should()
                .NotBeNull();
            request.AnimalCohortQuery.SpeciesCodesAndAnimals.SpeciesCodeAndAnimalsList[0].AnimalSpecies!.Code.Should()
                .Be("CTT");

            request.AnimalCohortQuery.DateOfBirth.Should().Be("2011-02-15");
            request.AnimalCohortQuery.BirthLocationIdentifier.Should().Be("07/094/0117");
        }
    }
}