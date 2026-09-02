using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtsFileImport
{
    public long CtsFileImportId { get; set; }

    public string DestinationTableName { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public long TotalRowsToProcess { get; set; }

    public DateTime AddedAt { get; set; }

    public short ImportStatusId { get; set; }

    public short ProcessingStatusId { get; set; }

    public long RowsFound { get; set; }

    public DateTime? ImportStartAt { get; set; }

    public DateTime? ImportEndAt { get; set; }

    public DateTime? ProcessingStartAt { get; set; }

    public DateTime? ProcessingEndAt { get; set; }

    public short? FailedAttempts { get; set; }

    public string? LastErrorReason { get; set; }

    public string GroupKey { get; set; } = null!;

    public string ImportType { get; set; } = null!;

    public DateTime BatchDate { get; set; }

    public long RowsImported { get; set; }

    public string? LastFilePartImported { get; set; }

    public virtual ICollection<CtAddress1> CtAddress1s { get; set; } = new List<CtAddress1>();

    public virtual ICollection<CtAllocRoutine1> CtAllocRoutine1s { get; set; } = new List<CtAllocRoutine1>();

    public virtual ICollection<CtAnimalChange1> CtAnimalChange1s { get; set; } = new List<CtAnimalChange1>();

    public virtual ICollection<CtAnimalClaim1> CtAnimalClaim1s { get; set; } = new List<CtAnimalClaim1>();

    public virtual ICollection<CtAnimalCorrSummError1> CtAnimalCorrSummError1s { get; set; } = new List<CtAnimalCorrSummError1>();

    public virtual ICollection<CtAnimalCorrectSummary1> CtAnimalCorrectSummary1s { get; set; } = new List<CtAnimalCorrectSummary1>();

    public virtual ICollection<CtAnimalIdentifier1> CtAnimalIdentifier1s { get; set; } = new List<CtAnimalIdentifier1>();

    public virtual ICollection<CtAnimalRelationship1> CtAnimalRelationship1s { get; set; } = new List<CtAnimalRelationship1>();

    public virtual ICollection<CtAnimalStatus1> CtAnimalStatus1s { get; set; } = new List<CtAnimalStatus1>();

    public virtual ICollection<CtApplicStatus1> CtApplicStatus1s { get; set; } = new List<CtApplicStatus1>();

    public virtual ICollection<CtApplicationLateDay1> CtApplicationLateDay1s { get; set; } = new List<CtApplicationLateDay1>();

    public virtual ICollection<CtBatchRetentionConf1> CtBatchRetentionConf1s { get; set; } = new List<CtBatchRetentionConf1>();

    public virtual ICollection<CtBreed1> CtBreed1s { get; set; } = new List<CtBreed1>();

    public virtual ICollection<CtClaExtract1> CtClaExtract1s { get; set; } = new List<CtClaExtract1>();

    public virtual ICollection<CtClaExtractDetail1> CtClaExtractDetail1s { get; set; } = new List<CtClaExtractDetail1>();

    public virtual ICollection<CtClaExtractDm1> CtClaExtractDm1s { get; set; } = new List<CtClaExtractDm1>();

    public virtual ICollection<CtClaMiniDetail1> CtClaMiniDetail1s { get; set; } = new List<CtClaMiniDetail1>();

    public virtual ICollection<CtClaMiniExtract1> CtClaMiniExtract1s { get; set; } = new List<CtClaMiniExtract1>();

    public virtual ICollection<CtClaimStatus1> CtClaimStatus1s { get; set; } = new List<CtClaimStatus1>();

    public virtual ICollection<CtClaimType1> CtClaimType1s { get; set; } = new List<CtClaimType1>();

    public virtual ICollection<CtCmAuthority1> CtCmAuthority1s { get; set; } = new List<CtCmAuthority1>();

    public virtual ICollection<CtCmMeasuresResult1> CtCmMeasuresResult1s { get; set; } = new List<CtCmMeasuresResult1>();

    public virtual ICollection<CtCommsAddress1> CtCommsAddress1s { get; set; } = new List<CtCommsAddress1>();

    public virtual ICollection<CtCondVariantGrouping1> CtCondVariantGrouping1s { get; set; } = new List<CtCondVariantGrouping1>();

    public virtual ICollection<CtCondition1> CtCondition1s { get; set; } = new List<CtCondition1>();

    public virtual ICollection<CtConditionActivity1> CtConditionActivity1s { get; set; } = new List<CtConditionActivity1>();

    public virtual ICollection<CtConditionMarker1> CtConditionMarker1s { get; set; } = new List<CtConditionMarker1>();

    public virtual ICollection<CtConditionMarkerError1> CtConditionMarkerError1s { get; set; } = new List<CtConditionMarkerError1>();

    public virtual ICollection<CtConditionType1> CtConditionType1s { get; set; } = new List<CtConditionType1>();

    public virtual ICollection<CtConditionVariant1> CtConditionVariant1s { get; set; } = new List<CtConditionVariant1>();

    public virtual ICollection<CtCountiesMigration1> CtCountiesMigration1s { get; set; } = new List<CtCountiesMigration1>();

    public virtual ICollection<CtCountry1> CtCountry1s { get; set; } = new List<CtCountry1>();

    public virtual ICollection<CtCounty1> CtCounty1s { get; set; } = new List<CtCounty1>();

    public virtual ICollection<CtCps167Report1> CtCps167Report1s { get; set; } = new List<CtCps167Report1>();

    public virtual ICollection<CtCts164HandshakeFileKey1> CtCts164HandshakeFileKey1s { get; set; } = new List<CtCts164HandshakeFileKey1>();

    public virtual ICollection<CtCtsUser1> CtCtsUser1s { get; set; } = new List<CtCtsUser1>();

    public virtual ICollection<CtEartag1> CtEartag1s { get; set; } = new List<CtEartag1>();

    public virtual ICollection<CtEartagFormat1> CtEartagFormat1s { get; set; } = new List<CtEartagFormat1>();

    public virtual ICollection<CtEartagReason1> CtEartagReason1s { get; set; } = new List<CtEartagReason1>();

    public virtual ICollection<CtEartagReasonFlag1> CtEartagReasonFlag1s { get; set; } = new List<CtEartagReasonFlag1>();

    public virtual ICollection<CtEartagStaging1> CtEartagStaging1s { get; set; } = new List<CtEartagStaging1>();

    public virtual ICollection<CtEartagType1> CtEartagType1s { get; set; } = new List<CtEartagType1>();

    public virtual ICollection<CtElectronicIdentifier1> CtElectronicIdentifier1s { get; set; } = new List<CtElectronicIdentifier1>();

    public virtual ICollection<CtEmailLog1> CtEmailLog1s { get; set; } = new List<CtEmailLog1>();

    public virtual ICollection<CtEreportFile1> CtEreportFile1s { get; set; } = new List<CtEreportFile1>();

    public virtual ICollection<CtEreportLoadMessage1> CtEreportLoadMessage1s { get; set; } = new List<CtEreportLoadMessage1>();

    public virtual ICollection<CtEreportLock1> CtEreportLock1s { get; set; } = new List<CtEreportLock1>();

    public virtual ICollection<CtEreportProcessMessage1> CtEreportProcessMessage1s { get; set; } = new List<CtEreportProcessMessage1>();

    public virtual ICollection<CtExtCetdEartag1> CtExtCetdEartag1s { get; set; } = new List<CtExtCetdEartag1>();

    public virtual ICollection<CtExtNiDistrict1> CtExtNiDistrict1s { get; set; } = new List<CtExtNiDistrict1>();

    public virtual ICollection<CtExtSpecialHerd1> CtExtSpecialHerd1s { get; set; } = new List<CtExtSpecialHerd1>();

    public virtual ICollection<CtFileLayout1> CtFileLayout1s { get; set; } = new List<CtFileLayout1>();

    public virtual ICollection<CtHsfSequence1> CtHsfSequence1s { get; set; } = new List<CtHsfSequence1>();

    public virtual ICollection<CtInsertUpdateLog1> CtInsertUpdateLog1s { get; set; } = new List<CtInsertUpdateLog1>();

    public virtual ICollection<CtIssuedDocument1> CtIssuedDocument1s { get; set; } = new List<CtIssuedDocument1>();

    public virtual ICollection<CtIssuingAuthority1> CtIssuingAuthority1s { get; set; } = new List<CtIssuingAuthority1>();

    public virtual ICollection<CtLabelRequest1> CtLabelRequest1s { get; set; } = new List<CtLabelRequest1>();

    public virtual ICollection<CtLabelSummary1> CtLabelSummary1s { get; set; } = new List<CtLabelSummary1>();

    public virtual ICollection<CtLateDay1> CtLateDay1s { get; set; } = new List<CtLateDay1>();

    public virtual ICollection<CtLetter1> CtLetter1s { get; set; } = new List<CtLetter1>();

    public virtual ICollection<CtLocTypeRelComb1> CtLocTypeRelComb1s { get; set; } = new List<CtLocTypeRelComb1>();

    public virtual ICollection<CtLocation1> CtLocation1s { get; set; } = new List<CtLocation1>();

    public virtual ICollection<CtLocationIdFormat1> CtLocationIdFormat1s { get; set; } = new List<CtLocationIdFormat1>();

    public virtual ICollection<CtLocationIdentifier1> CtLocationIdentifier1s { get; set; } = new List<CtLocationIdentifier1>();

    public virtual ICollection<CtLocationPartyRel1> CtLocationPartyRel1s { get; set; } = new List<CtLocationPartyRel1>();

    public virtual ICollection<CtLocationPartyRelType1> CtLocationPartyRelType1s { get; set; } = new List<CtLocationPartyRelType1>();

    public virtual ICollection<CtLocationRelType1> CtLocationRelType1s { get; set; } = new List<CtLocationRelType1>();

    public virtual ICollection<CtLocationRelationship1> CtLocationRelationship1s { get; set; } = new List<CtLocationRelationship1>();

    public virtual ICollection<CtLocationType1> CtLocationType1s { get; set; } = new List<CtLocationType1>();

    public virtual ICollection<CtLocationsFaker1> CtLocationsFaker1s { get; set; } = new List<CtLocationsFaker1>();

    public virtual ICollection<CtLocrestrictionstoanimal1> CtLocrestrictionstoanimal1s { get; set; } = new List<CtLocrestrictionstoanimal1>();

    public virtual ICollection<CtMgtControlError1> CtMgtControlError1s { get; set; } = new List<CtMgtControlError1>();

    public virtual ICollection<CtMgtWgAllocationRule1> CtMgtWgAllocationRule1s { get; set; } = new List<CtMgtWgAllocationRule1>();

    public virtual ICollection<CtMhsToCph1> CtMhsToCph1s { get; set; } = new List<CtMhsToCph1>();

    public virtual ICollection<CtMovHst1> CtMovHst1s { get; set; } = new List<CtMovHst1>();

    public virtual ICollection<CtMovtCorrSummError1> CtMovtCorrSummError1s { get; set; } = new List<CtMovtCorrSummError1>();

    public virtual ICollection<CtMovtCorrectSummary1> CtMovtCorrectSummary1s { get; set; } = new List<CtMovtCorrectSummary1>();

    public virtual ICollection<CtMsgtxt1> CtMsgtxt1s { get; set; } = new List<CtMsgtxt1>();

    public virtual ICollection<CtNonWorkingDay1> CtNonWorkingDay1s { get; set; } = new List<CtNonWorkingDay1>();

    public virtual ICollection<CtParamGroup1> CtParamGroup1s { get; set; } = new List<CtParamGroup1>();

    public virtual ICollection<CtParamHeader1> CtParamHeader1s { get; set; } = new List<CtParamHeader1>();

    public virtual ICollection<CtParamValue1> CtParamValue1s { get; set; } = new List<CtParamValue1>();

    public virtual ICollection<CtParamValueGroup1> CtParamValueGroup1s { get; set; } = new List<CtParamValueGroup1>();

    public virtual ICollection<CtPartiesFaker1> CtPartiesFaker1s { get; set; } = new List<CtPartiesFaker1>();

    public virtual ICollection<CtParty1> CtParty1s { get; set; } = new List<CtParty1>();

    public virtual ICollection<CtPpafGrouping1> CtPpafGrouping1s { get; set; } = new List<CtPpafGrouping1>();

    public virtual ICollection<CtPreprintedAppnForm1> CtPreprintedAppnForm1s { get; set; } = new List<CtPreprintedAppnForm1>();

    public virtual ICollection<CtProbityCheck1> CtProbityCheck1s { get; set; } = new List<CtProbityCheck1>();

    public virtual ICollection<CtPs9999AhdbDatum1> CtPs9999AhdbDatum1s { get; set; } = new List<CtPs9999AhdbDatum1>();

    public virtual ICollection<CtPs9999AhdbMovHistory1> CtPs9999AhdbMovHistory1s { get; set; } = new List<CtPs9999AhdbMovHistory1>();

    public virtual ICollection<CtRecdApplicationError1> CtRecdApplicationError1s { get; set; } = new List<CtRecdApplicationError1>();

    public virtual ICollection<CtRecdMovementError1> CtRecdMovementError1s { get; set; } = new List<CtRecdMovementError1>();

    public virtual ICollection<CtReceivedApplication1> CtReceivedApplication1s { get; set; } = new List<CtReceivedApplication1>();

    public virtual ICollection<CtReceivedMovement1> CtReceivedMovement1s { get; set; } = new List<CtReceivedMovement1>();

    public virtual ICollection<CtRegisteredAnimal1> CtRegisteredAnimal1s { get; set; } = new List<CtRegisteredAnimal1>();

    public virtual ICollection<CtRegisteredMovement1> CtRegisteredMovement1s { get; set; } = new List<CtRegisteredMovement1>();

    public virtual ICollection<CtResetToExtract1> CtResetToExtract1s { get; set; } = new List<CtResetToExtract1>();

    public virtual ICollection<CtSbcsExt1> CtSbcsExt1s { get; set; } = new List<CtSbcsExt1>();

    public virtual ICollection<CtScheme1> CtScheme1s { get; set; } = new List<CtScheme1>();

    public virtual ICollection<CtStageFile1> CtStageFile1s { get; set; } = new List<CtStageFile1>();

    public virtual ICollection<CtStageLock1> CtStageLock1s { get; set; } = new List<CtStageLock1>();

    public virtual ICollection<CtStageMessage1> CtStageMessage1s { get; set; } = new List<CtStageMessage1>();

    public virtual ICollection<CtSublocationType1> CtSublocationType1s { get; set; } = new List<CtSublocationType1>();

    public virtual ICollection<CtSuspAnimalError1> CtSuspAnimalError1s { get; set; } = new List<CtSuspAnimalError1>();

    public virtual ICollection<CtSuspCmMeasureResult1> CtSuspCmMeasureResult1s { get; set; } = new List<CtSuspCmMeasureResult1>();

    public virtual ICollection<CtSuspConditionMarker1> CtSuspConditionMarker1s { get; set; } = new List<CtSuspConditionMarker1>();

    public virtual ICollection<CtSuspMovementError1> CtSuspMovementError1s { get; set; } = new List<CtSuspMovementError1>();

    public virtual ICollection<CtSuspendedAnimal1> CtSuspendedAnimal1s { get; set; } = new List<CtSuspendedAnimal1>();

    public virtual ICollection<CtSuspendedMovement1> CtSuspendedMovement1s { get; set; } = new List<CtSuspendedMovement1>();

    public virtual ICollection<CtSuspenseCharAllocRule1> CtSuspenseCharAllocRule1s { get; set; } = new List<CtSuspenseCharAllocRule1>();

    public virtual ICollection<CtSuspenseWgAllocRule1> CtSuspenseWgAllocRule1s { get; set; } = new List<CtSuspenseWgAllocRule1>();

    public virtual ICollection<CtValidApplication1> CtValidApplication1s { get; set; } = new List<CtValidApplication1>();

    public virtual ICollection<CtWebUser1> CtWebUser1s { get; set; } = new List<CtWebUser1>();

    public virtual ICollection<CtWgAutoallocation1> CtWgAutoallocation1s { get; set; } = new List<CtWgAutoallocation1>();

    public virtual ICollection<CtWgSuperAssignment1> CtWgSuperAssignment1s { get; set; } = new List<CtWgSuperAssignment1>();

    public virtual ICollection<CtWgUserAssignment1> CtWgUserAssignment1s { get; set; } = new List<CtWgUserAssignment1>();

    public virtual ICollection<CtWorkgroup1> CtWorkgroup1s { get; set; } = new List<CtWorkgroup1>();

    public virtual ICollection<CtsFileImportsLog> CtsFileImportsLogs { get; set; } = new List<CtsFileImportsLog>();

    public virtual CtsFileImportStatus ImportStatus { get; set; } = null!;

    public virtual CtsFileProcessingStatus ProcessingStatus { get; set; } = null!;
}
