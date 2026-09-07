using System;
using System.Collections.Generic;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Contexts;

public partial class CtsTransactionsGraphQLDbContext : DbContext
{
    public CtsTransactionsGraphQLDbContext(DbContextOptions<CtsTransactionsGraphQLDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CtAddress> CtAddresses { get; set; }

    public virtual DbSet<CtAllocRoutine> CtAllocRoutines { get; set; }

    public virtual DbSet<CtAnimalChange> CtAnimalChanges { get; set; }

    public virtual DbSet<CtAnimalClaim> CtAnimalClaims { get; set; }

    public virtual DbSet<CtAnimalCorrSummError> CtAnimalCorrSummErrors { get; set; }

    public virtual DbSet<CtAnimalCorrectSummary> CtAnimalCorrectSummaries { get; set; }

    public virtual DbSet<CtAnimalIdentifier> CtAnimalIdentifiers { get; set; }

    public virtual DbSet<CtAnimalRelationship> CtAnimalRelationships { get; set; }

    public virtual DbSet<CtAnimalStatus> CtAnimalStatuses { get; set; }

    public virtual DbSet<CtApplicStatus> CtApplicStatuses { get; set; }

    public virtual DbSet<CtApplicationLateDay> CtApplicationLateDays { get; set; }

    public virtual DbSet<CtBatchRetentionConf> CtBatchRetentionConfs { get; set; }

    public virtual DbSet<CtBreed> CtBreeds { get; set; }

    public virtual DbSet<CtClaExtract> CtClaExtracts { get; set; }

    public virtual DbSet<CtClaExtractDetail> CtClaExtractDetails { get; set; }

    public virtual DbSet<CtClaExtractDm> CtClaExtractDms { get; set; }

    public virtual DbSet<CtClaMiniDetail> CtClaMiniDetails { get; set; }

    public virtual DbSet<CtClaMiniExtract> CtClaMiniExtracts { get; set; }

    public virtual DbSet<CtClaimStatus> CtClaimStatuses { get; set; }

    public virtual DbSet<CtClaimType> CtClaimTypes { get; set; }

    public virtual DbSet<CtCmAuthority> CtCmAuthorities { get; set; }

    public virtual DbSet<CtCmMeasuresResult> CtCmMeasuresResults { get; set; }

    public virtual DbSet<CtCommsAddress> CtCommsAddresses { get; set; }

    public virtual DbSet<CtCondVariantGrouping> CtCondVariantGroupings { get; set; }

    public virtual DbSet<CtCondition> CtConditions { get; set; }

    public virtual DbSet<CtConditionActivity> CtConditionActivities { get; set; }

    public virtual DbSet<CtConditionMarker> CtConditionMarkers { get; set; }

    public virtual DbSet<CtConditionMarkerError> CtConditionMarkerErrors { get; set; }

    public virtual DbSet<CtConditionType> CtConditionTypes { get; set; }

    public virtual DbSet<CtConditionVariant> CtConditionVariants { get; set; }

    public virtual DbSet<CtCountiesMigration> CtCountiesMigrations { get; set; }

    public virtual DbSet<CtCountry> CtCountries { get; set; }

    public virtual DbSet<CtCounty> CtCounties { get; set; }

    public virtual DbSet<CtCps167Report> CtCps167Reports { get; set; }

    public virtual DbSet<CtCts164HandshakeFileKey> CtCts164HandshakeFileKeys { get; set; }

    public virtual DbSet<CtCtsUser> CtCtsUsers { get; set; }

    public virtual DbSet<CtEartag> CtEartags { get; set; }

    public virtual DbSet<CtEartagFormat> CtEartagFormats { get; set; }

    public virtual DbSet<CtEartagReason> CtEartagReasons { get; set; }

    public virtual DbSet<CtEartagReasonFlag> CtEartagReasonFlags { get; set; }

    public virtual DbSet<CtEartagStaging> CtEartagStagings { get; set; }

    public virtual DbSet<CtEartagType> CtEartagTypes { get; set; }

    public virtual DbSet<CtElectronicIdentifier> CtElectronicIdentifiers { get; set; }

    public virtual DbSet<CtEmailLog> CtEmailLogs { get; set; }

    public virtual DbSet<CtEreportFile> CtEreportFiles { get; set; }

    public virtual DbSet<CtEreportLoadMessage> CtEreportLoadMessages { get; set; }

    public virtual DbSet<CtEreportLock> CtEreportLocks { get; set; }

    public virtual DbSet<CtEreportProcessMessage> CtEreportProcessMessages { get; set; }

    public virtual DbSet<CtExtCetdEartag> CtExtCetdEartags { get; set; }

    public virtual DbSet<CtExtNiDistrict> CtExtNiDistricts { get; set; }

    public virtual DbSet<CtExtSpecialHerd> CtExtSpecialHerds { get; set; }

    public virtual DbSet<CtFileLayout> CtFileLayouts { get; set; }

    public virtual DbSet<CtHsfSequence> CtHsfSequences { get; set; }

    public virtual DbSet<CtInsertUpdateLog> CtInsertUpdateLogs { get; set; }

    public virtual DbSet<CtIssuedDocument> CtIssuedDocuments { get; set; }

    public virtual DbSet<CtIssuingAuthority> CtIssuingAuthorities { get; set; }

    public virtual DbSet<CtLabelRequest> CtLabelRequests { get; set; }

    public virtual DbSet<CtLabelSummary> CtLabelSummaries { get; set; }

    public virtual DbSet<CtLateDay> CtLateDays { get; set; }

    public virtual DbSet<CtLetter> CtLetters { get; set; }

    public virtual DbSet<CtLocTypeRelComb> CtLocTypeRelCombs { get; set; }

    public virtual DbSet<CtLocation> CtLocations { get; set; }

    public virtual DbSet<CtLocationIdFormat> CtLocationIdFormats { get; set; }

    public virtual DbSet<CtLocationIdentifier> CtLocationIdentifiers { get; set; }

    public virtual DbSet<CtLocationPartyRel> CtLocationPartyRels { get; set; }

    public virtual DbSet<CtLocationPartyRelType> CtLocationPartyRelTypes { get; set; }

    public virtual DbSet<CtLocationRelType> CtLocationRelTypes { get; set; }

    public virtual DbSet<CtLocationRelationship> CtLocationRelationships { get; set; }

    public virtual DbSet<CtLocationType> CtLocationTypes { get; set; }

    public virtual DbSet<CtLocationsFaker> CtLocationsFakers { get; set; }

    public virtual DbSet<CtLocrestrictionstoanimal> CtLocrestrictionstoanimals { get; set; }

    public virtual DbSet<CtMgtControlError> CtMgtControlErrors { get; set; }

    public virtual DbSet<CtMgtWgAllocationRule> CtMgtWgAllocationRules { get; set; }

    public virtual DbSet<CtMhsToCph> CtMhsToCphs { get; set; }

    public virtual DbSet<CtMovHst> CtMovHsts { get; set; }

    public virtual DbSet<CtMovtCorrSummError> CtMovtCorrSummErrors { get; set; }

    public virtual DbSet<CtMovtCorrectSummary> CtMovtCorrectSummaries { get; set; }

    public virtual DbSet<CtMsgtxt> CtMsgtxts { get; set; }

    public virtual DbSet<CtNonWorkingDay> CtNonWorkingDays { get; set; }

    public virtual DbSet<CtParamGroup> CtParamGroups { get; set; }

    public virtual DbSet<CtParamHeader> CtParamHeaders { get; set; }

    public virtual DbSet<CtParamValue> CtParamValues { get; set; }

    public virtual DbSet<CtParamValueGroup> CtParamValueGroups { get; set; }

    public virtual DbSet<CtPartiesFaker> CtPartiesFakers { get; set; }

    public virtual DbSet<CtParty> CtParties { get; set; }

    public virtual DbSet<CtPpafGrouping> CtPpafGroupings { get; set; }

    public virtual DbSet<CtPreprintedAppnForm> CtPreprintedAppnForms { get; set; }

    public virtual DbSet<CtProbityCheck> CtProbityChecks { get; set; }

    public virtual DbSet<CtPs9999AhdbDatum> CtPs9999AhdbData { get; set; }

    public virtual DbSet<CtPs9999AhdbMovHistory> CtPs9999AhdbMovHistories { get; set; }

    public virtual DbSet<CtRecdApplicationError> CtRecdApplicationErrors { get; set; }

    public virtual DbSet<CtRecdMovementError> CtRecdMovementErrors { get; set; }

    public virtual DbSet<CtReceivedApplication> CtReceivedApplications { get; set; }

    public virtual DbSet<CtReceivedMovement> CtReceivedMovements { get; set; }

    public virtual DbSet<CtRegisteredAnimal> CtRegisteredAnimals { get; set; }

    public virtual DbSet<CtRegisteredMovement> CtRegisteredMovements { get; set; }

    public virtual DbSet<CtResetToExtract> CtResetToExtracts { get; set; }

    public virtual DbSet<CtSbcsExt> CtSbcsExts { get; set; }

    public virtual DbSet<CtScheme> CtSchemes { get; set; }

    public virtual DbSet<CtStageFile> CtStageFiles { get; set; }

    public virtual DbSet<CtStageLock> CtStageLocks { get; set; }

    public virtual DbSet<CtStageMessage> CtStageMessages { get; set; }

    public virtual DbSet<CtSublocationType> CtSublocationTypes { get; set; }

    public virtual DbSet<CtSuspAnimalError> CtSuspAnimalErrors { get; set; }

    public virtual DbSet<CtSuspCmMeasureResult> CtSuspCmMeasureResults { get; set; }

    public virtual DbSet<CtSuspConditionMarker> CtSuspConditionMarkers { get; set; }

    public virtual DbSet<CtSuspMovementError> CtSuspMovementErrors { get; set; }

    public virtual DbSet<CtSuspendedAnimal> CtSuspendedAnimals { get; set; }

    public virtual DbSet<CtSuspendedMovement> CtSuspendedMovements { get; set; }

    public virtual DbSet<CtSuspenseCharAllocRule> CtSuspenseCharAllocRules { get; set; }

    public virtual DbSet<CtSuspenseWgAllocRule> CtSuspenseWgAllocRules { get; set; }

    public virtual DbSet<CtValidApplication> CtValidApplications { get; set; }

    public virtual DbSet<CtWebUser> CtWebUsers { get; set; }

    public virtual DbSet<CtWgAutoallocation> CtWgAutoallocations { get; set; }

    public virtual DbSet<CtWgSuperAssignment> CtWgSuperAssignments { get; set; }

    public virtual DbSet<CtWgUserAssignment> CtWgUserAssignments { get; set; }

    public virtual DbSet<CtWorkgroup> CtWorkgroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CtAddress>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_addresses_pkey");

            entity.ToTable("ct_addresses", "cts_transactions");

            entity.HasIndex(e => e.AdrLocId, "ct_addresses_adr_loc_id_idx");

            entity.HasIndex(e => e.AdrParId, "ct_addresses_adr_par_id_idx");

            entity.HasIndex(e => e.AdrAudDatetime, "ct_addresses_aud_datetime_idx");

            entity.HasIndex(e => e.AdrAudId, "ct_addresses_aud_id_idx");

            entity.HasIndex(e => new { e.AdrAudType, e.AdrAudDatetime }, "ct_addresses_aud_type_datetime_idx");

            entity.HasIndex(e => e.AdrAudType, "ct_addresses_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_addresses_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_addresses_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_addresses_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_addresses_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_addresses_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_addresses_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_addresses_record_type_idx");

            entity.HasIndex(e => e.AdrId, "ct_addresses_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_addresses_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.AdrAddress2)
                .HasMaxLength(35)
                .HasColumnName("adr_address_2");
            entity.Property(e => e.AdrAddress3)
                .HasMaxLength(35)
                .HasColumnName("adr_address_3");
            entity.Property(e => e.AdrAddress4)
                .HasMaxLength(35)
                .HasColumnName("adr_address_4");
            entity.Property(e => e.AdrAddress5)
                .HasMaxLength(35)
                .HasColumnName("adr_address_5");
            entity.Property(e => e.AdrAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("adr_aud_datetime");
            entity.Property(e => e.AdrAudId).HasColumnName("adr_aud_id");
            entity.Property(e => e.AdrAudType).HasColumnName("adr_aud_type");
            entity.Property(e => e.AdrCurrentModifiedDate).HasColumnName("adr_current_modified_date");
            entity.Property(e => e.AdrCurrentPid)
                .HasPrecision(3)
                .HasColumnName("adr_current_pid");
            entity.Property(e => e.AdrCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("adr_current_status");
            entity.Property(e => e.AdrCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("adr_current_user");
            entity.Property(e => e.AdrId)
                .HasPrecision(12)
                .HasColumnName("adr_id");
            entity.Property(e => e.AdrLocId)
                .HasPrecision(12)
                .HasColumnName("adr_loc_id");
            entity.Property(e => e.AdrName)
                .HasMaxLength(35)
                .HasColumnName("adr_name");
            entity.Property(e => e.AdrParId)
                .HasPrecision(12)
                .HasColumnName("adr_par_id");
            entity.Property(e => e.AdrPostCode)
                .HasMaxLength(8)
                .HasColumnName("adr_post_code");
            entity.Property(e => e.AdrVersion)
                .HasPrecision(6)
                .HasColumnName("adr_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtAllocRoutine>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_alloc_routines_transactions_pkey");

            entity.ToTable("ct_alloc_routines", "cts_transactions");

            entity.HasIndex(e => e.RouAudDatetime, "ct_alloc_routines_aud_datetime_idx");

            entity.HasIndex(e => e.RouAudId, "ct_alloc_routines_aud_id_idx");

            entity.HasIndex(e => new { e.RouAudType, e.RouAudDatetime }, "ct_alloc_routines_aud_type_datetime_idx");

            entity.HasIndex(e => e.RouAudType, "ct_alloc_routines_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_alloc_routines_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_alloc_routines_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_alloc_routines_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_alloc_routines_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_alloc_routines_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_alloc_routines_record_type_idx");

            entity.HasIndex(e => e.RouId, "ct_alloc_routines_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_alloc_routines_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RouAllocationType)
                .HasMaxLength(1)
                .HasColumnName("rou_allocation_type");
            entity.Property(e => e.RouAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("rou_aud_datetime");
            entity.Property(e => e.RouAudId).HasColumnName("rou_aud_id");
            entity.Property(e => e.RouAudType).HasColumnName("rou_aud_type");
            entity.Property(e => e.RouCurrentModifiedDate).HasColumnName("rou_current_modified_date");
            entity.Property(e => e.RouCurrentPid)
                .HasPrecision(3)
                .HasColumnName("rou_current_pid");
            entity.Property(e => e.RouCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("rou_current_status");
            entity.Property(e => e.RouCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("rou_current_user");
            entity.Property(e => e.RouId)
                .HasPrecision(12)
                .HasColumnName("rou_id");
            entity.Property(e => e.RouLongDescription)
                .HasMaxLength(40)
                .HasColumnName("rou_long_description");
            entity.Property(e => e.RouRoutine)
                .HasMaxLength(6)
                .HasColumnName("rou_routine");
            entity.Property(e => e.RouVersion)
                .HasPrecision(6)
                .HasColumnName("rou_version");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtAnimalChange>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_animal_changes_pkey");

            entity.ToTable("ct_animal_changes", "cts_transactions");

            entity.HasIndex(e => e.AchLocIdDocIssued, "ct_animal_changes_ach_loc_id_doc_issued_idx");

            entity.HasIndex(e => e.AchMovIdDeathCancel, "ct_animal_changes_ach_mov_id_death_cancel_idx");

            entity.HasIndex(e => e.AchRanIdDocIssued, "ct_animal_changes_ach_ran_id_doc_issued_idx");

            entity.HasIndex(e => e.AchAudDatetime, "ct_animal_changes_aud_datetime_idx");

            entity.HasIndex(e => e.AchAudId, "ct_animal_changes_aud_id_idx");

            entity.HasIndex(e => new { e.AchAudType, e.AchAudDatetime }, "ct_animal_changes_aud_type_datetime_idx");

            entity.HasIndex(e => e.AchAudType, "ct_animal_changes_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_animal_changes_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_changes_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_animal_changes_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_changes_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_animal_changes_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_animal_changes_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_animal_changes_record_type_idx");

            entity.HasIndex(e => e.AchId, "ct_animal_changes_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_animal_changes_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.AchAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("ach_aud_datetime");
            entity.Property(e => e.AchAudId).HasColumnName("ach_aud_id");
            entity.Property(e => e.AchAudType).HasColumnName("ach_aud_type");
            entity.Property(e => e.AchBirthDateNew).HasColumnName("ach_birth_date_new");
            entity.Property(e => e.AchBirthDateOriginal).HasColumnName("ach_birth_date_original");
            entity.Property(e => e.AchBreedNew)
                .HasMaxLength(5)
                .HasColumnName("ach_breed_new");
            entity.Property(e => e.AchBreedOriginal)
                .HasMaxLength(5)
                .HasColumnName("ach_breed_original");
            entity.Property(e => e.AchCurrentModifiedDate).HasColumnName("ach_current_modified_date");
            entity.Property(e => e.AchCurrentPid)
                .HasPrecision(3)
                .HasColumnName("ach_current_pid");
            entity.Property(e => e.AchCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("ach_current_status");
            entity.Property(e => e.AchCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("ach_current_user");
            entity.Property(e => e.AchDocIssuedDate).HasColumnName("ach_doc_issued_date");
            entity.Property(e => e.AchEartagNew)
                .HasMaxLength(14)
                .HasColumnName("ach_eartag_new");
            entity.Property(e => e.AchEartagOriginal)
                .HasMaxLength(14)
                .HasColumnName("ach_eartag_original");
            entity.Property(e => e.AchId)
                .HasPrecision(12)
                .HasColumnName("ach_id");
            entity.Property(e => e.AchLocIdDocIssued)
                .HasPrecision(12)
                .HasColumnName("ach_loc_id_doc_issued");
            entity.Property(e => e.AchMovIdDeathCancel)
                .HasPrecision(12)
                .HasColumnName("ach_mov_id_death_cancel");
            entity.Property(e => e.AchPassportVersionNumber)
                .HasMaxLength(3)
                .HasColumnName("ach_passport_version_number");
            entity.Property(e => e.AchRanIdDocIssued)
                .HasPrecision(12)
                .HasColumnName("ach_ran_id_doc_issued");
            entity.Property(e => e.AchSexNew)
                .HasMaxLength(1)
                .HasColumnName("ach_sex_new");
            entity.Property(e => e.AchSexOriginal)
                .HasMaxLength(1)
                .HasColumnName("ach_sex_original");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtAnimalClaim>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_animal_claims_pkey");

            entity.ToTable("ct_animal_claims", "cts_transactions");

            entity.HasIndex(e => e.AncClsId, "ct_animal_claims_anc_cls_id_idx");

            entity.HasIndex(e => e.AncCltId, "ct_animal_claims_anc_clt_id_idx");

            entity.HasIndex(e => e.AncRanId, "ct_animal_claims_anc_ran_id_idx");

            entity.HasIndex(e => e.AncAudDatetime, "ct_animal_claims_aud_datetime_idx");

            entity.HasIndex(e => e.AncAudId, "ct_animal_claims_aud_id_idx");

            entity.HasIndex(e => new { e.AncAudType, e.AncAudDatetime }, "ct_animal_claims_aud_type_datetime_idx");

            entity.HasIndex(e => e.AncAudType, "ct_animal_claims_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_animal_claims_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_claims_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_animal_claims_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_claims_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_animal_claims_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_animal_claims_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_animal_claims_record_type_idx");

            entity.HasIndex(e => e.AncId, "ct_animal_claims_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_animal_claims_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.AncAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("anc_aud_datetime");
            entity.Property(e => e.AncAudId).HasColumnName("anc_aud_id");
            entity.Property(e => e.AncAudType).HasColumnName("anc_aud_type");
            entity.Property(e => e.AncClaimReference)
                .HasMaxLength(20)
                .HasColumnName("anc_claim_reference");
            entity.Property(e => e.AncClaimSequence)
                .HasPrecision(3)
                .HasColumnName("anc_claim_sequence");
            entity.Property(e => e.AncClsId)
                .HasPrecision(12)
                .HasColumnName("anc_cls_id");
            entity.Property(e => e.AncCltId)
                .HasPrecision(12)
                .HasColumnName("anc_clt_id");
            entity.Property(e => e.AncCurrentModifiedDate).HasColumnName("anc_current_modified_date");
            entity.Property(e => e.AncCurrentPid)
                .HasPrecision(3)
                .HasColumnName("anc_current_pid");
            entity.Property(e => e.AncCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("anc_current_status");
            entity.Property(e => e.AncCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("anc_current_user");
            entity.Property(e => e.AncId)
                .HasPrecision(12)
                .HasColumnName("anc_id");
            entity.Property(e => e.AncOffice)
                .HasMaxLength(2)
                .HasColumnName("anc_office");
            entity.Property(e => e.AncRanId)
                .HasPrecision(12)
                .HasColumnName("anc_ran_id");
            entity.Property(e => e.AncRetentionEndDate).HasColumnName("anc_retention_end_date");
            entity.Property(e => e.AncRetentionStartDate).HasColumnName("anc_retention_start_date");
            entity.Property(e => e.AncSchemeModifiedDatetime).HasColumnName("anc_scheme_modified_datetime");
            entity.Property(e => e.AncSchemeYear)
                .HasPrecision(4)
                .HasColumnName("anc_scheme_year");
            entity.Property(e => e.AncVersion)
                .HasPrecision(6)
                .HasColumnName("anc_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtAnimalCorrSummError>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_animal_corr_summ_errors_pkey");

            entity.ToTable("ct_animal_corr_summ_errors", "cts_transactions");

            entity.HasIndex(e => e.AseAcsId, "ct_animal_corr_summ_errors_ase_acs_id_idx");

            entity.HasIndex(e => e.AseAudDatetime, "ct_animal_corr_summ_errors_aud_datetime_idx");

            entity.HasIndex(e => e.AseAudId, "ct_animal_corr_summ_errors_aud_id_idx");

            entity.HasIndex(e => new { e.AseAudType, e.AseAudDatetime }, "ct_animal_corr_summ_errors_aud_type_datetime_idx");

            entity.HasIndex(e => e.AseAudType, "ct_animal_corr_summ_errors_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_animal_corr_summ_errors_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_corr_summ_errors_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_animal_corr_summ_errors_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_corr_summ_errors_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_animal_corr_summ_errors_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_animal_corr_summ_errors_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_animal_corr_summ_errors_record_type_idx");

            entity.HasIndex(e => e.AseId, "ct_animal_corr_summ_errors_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_animal_corr_summ_errors_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.AseAcsId)
                .HasPrecision(12)
                .HasColumnName("ase_acs_id");
            entity.Property(e => e.AseAttributeName)
                .HasMaxLength(30)
                .HasColumnName("ase_attribute_name");
            entity.Property(e => e.AseAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("ase_aud_datetime");
            entity.Property(e => e.AseAudId).HasColumnName("ase_aud_id");
            entity.Property(e => e.AseAudType).HasColumnName("ase_aud_type");
            entity.Property(e => e.AseCurrentModifiedDate).HasColumnName("ase_current_modified_date");
            entity.Property(e => e.AseCurrentPid)
                .HasPrecision(12)
                .HasColumnName("ase_current_pid");
            entity.Property(e => e.AseCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("ase_current_status");
            entity.Property(e => e.AseCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("ase_current_user");
            entity.Property(e => e.AseErrorCode)
                .HasMaxLength(10)
                .HasColumnName("ase_error_code");
            entity.Property(e => e.AseId)
                .HasPrecision(12)
                .HasColumnName("ase_id");
            entity.Property(e => e.AseVersion)
                .HasPrecision(6)
                .HasColumnName("ase_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtAnimalCorrectSummary>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_animal_correct_summaries_pkey");

            entity.ToTable("ct_animal_correct_summaries", "cts_transactions");

            entity.HasIndex(e => e.AcsRanId, "ct_animal_correct_summaries_acs_ran_id_idx");

            entity.HasIndex(e => e.AcsRapId, "ct_animal_correct_summaries_acs_rap_id_idx");

            entity.HasIndex(e => e.AcsSanId, "ct_animal_correct_summaries_acs_san_id_idx");

            entity.HasIndex(e => e.AcsVapId, "ct_animal_correct_summaries_acs_vap_id_idx");

            entity.HasIndex(e => e.AcsAudDatetime, "ct_animal_correct_summaries_aud_datetime_idx");

            entity.HasIndex(e => e.AcsAudId, "ct_animal_correct_summaries_aud_id_idx");

            entity.HasIndex(e => new { e.AcsAudType, e.AcsAudDatetime }, "ct_animal_correct_summaries_aud_type_datetime_idx");

            entity.HasIndex(e => e.AcsAudType, "ct_animal_correct_summaries_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_animal_correct_summaries_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_correct_summaries_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_animal_correct_summaries_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_correct_summaries_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_animal_correct_summaries_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_animal_correct_summaries_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_animal_correct_summaries_record_type_idx");

            entity.HasIndex(e => e.AcsId, "ct_animal_correct_summaries_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_animal_correct_summaries_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.AcsAmendRetagInd)
                .HasMaxLength(1)
                .HasColumnName("acs_amend_retag_ind");
            entity.Property(e => e.AcsApplicationType)
                .HasMaxLength(1)
                .HasColumnName("acs_application_type");
            entity.Property(e => e.AcsAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("acs_aud_datetime");
            entity.Property(e => e.AcsAudId).HasColumnName("acs_aud_id");
            entity.Property(e => e.AcsAudType).HasColumnName("acs_aud_type");
            entity.Property(e => e.AcsChangeReceivedDate).HasColumnName("acs_change_received_date");
            entity.Property(e => e.AcsChrCorrectionType)
                .HasMaxLength(1)
                .HasColumnName("acs_chr_correction_type");
            entity.Property(e => e.AcsChrLocationInd)
                .HasMaxLength(1)
                .HasColumnName("acs_chr_location_ind");
            entity.Property(e => e.AcsCtsIndicator)
                .HasMaxLength(1)
                .HasColumnName("acs_cts_indicator");
            entity.Property(e => e.AcsCurrentModifiedDate).HasColumnName("acs_current_modified_date");
            entity.Property(e => e.AcsCurrentPid)
                .HasPrecision(12)
                .HasColumnName("acs_current_pid");
            entity.Property(e => e.AcsCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("acs_current_status");
            entity.Property(e => e.AcsCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("acs_current_user");
            entity.Property(e => e.AcsId)
                .HasPrecision(12)
                .HasColumnName("acs_id");
            entity.Property(e => e.AcsInitApplicReceiptDate)
                .HasMaxLength(20)
                .HasColumnName("acs_init_applic_receipt_date");
            entity.Property(e => e.AcsInitApplicTargetDate).HasColumnName("acs_init_applic_target_date");
            entity.Property(e => e.AcsInitBirthDate)
                .HasMaxLength(20)
                .HasColumnName("acs_init_birth_date");
            entity.Property(e => e.AcsInitBreed)
                .HasMaxLength(20)
                .HasColumnName("acs_init_breed");
            entity.Property(e => e.AcsInitCountryOfOrigin)
                .HasMaxLength(30)
                .HasColumnName("acs_init_country_of_origin");
            entity.Property(e => e.AcsInitEartag)
                .HasMaxLength(30)
                .HasColumnName("acs_init_eartag");
            entity.Property(e => e.AcsInitEartagType)
                .HasMaxLength(20)
                .HasColumnName("acs_init_eartag_type");
            entity.Property(e => e.AcsInitElectronicIdentifier)
                .HasMaxLength(30)
                .HasColumnName("acs_init_electronic_identifier");
            entity.Property(e => e.AcsInitGeneticDamEartag)
                .HasMaxLength(30)
                .HasColumnName("acs_init_genetic_dam_eartag");
            entity.Property(e => e.AcsInitGeneticDamEtType)
                .HasMaxLength(20)
                .HasColumnName("acs_init_genetic_dam_et_type");
            entity.Property(e => e.AcsInitHealthCertificateNo)
                .HasMaxLength(30)
                .HasColumnName("acs_init_health_certificate_no");
            entity.Property(e => e.AcsInitImportIdentifier)
                .HasMaxLength(50)
                .HasColumnName("acs_init_import_identifier");
            entity.Property(e => e.AcsInitInitialLocIdent)
                .HasMaxLength(30)
                .HasColumnName("acs_init_initial_loc_ident");
            entity.Property(e => e.AcsInitInitialLocType)
                .HasMaxLength(2)
                .HasColumnName("acs_init_initial_loc_type");
            entity.Property(e => e.AcsInitInitialSublocIdent)
                .HasMaxLength(2)
                .HasColumnName("acs_init_initial_subloc_ident");
            entity.Property(e => e.AcsInitIntendedAction)
                .HasMaxLength(60)
                .HasColumnName("acs_init_intended_action");
            entity.Property(e => e.AcsInitNumberCalfMovts)
                .HasPrecision(2)
                .HasColumnName("acs_init_number_calf_movts");
            entity.Property(e => e.AcsInitPlacementDate)
                .HasMaxLength(30)
                .HasColumnName("acs_init_placement_date");
            entity.Property(e => e.AcsInitPreviousEartag)
                .HasMaxLength(30)
                .HasColumnName("acs_init_previous_eartag");
            entity.Property(e => e.AcsInitRequestLocIdent)
                .HasMaxLength(30)
                .HasColumnName("acs_init_request_loc_ident");
            entity.Property(e => e.AcsInitRequestLocType)
                .HasMaxLength(2)
                .HasColumnName("acs_init_request_loc_type");
            entity.Property(e => e.AcsInitRequestSublocIdent)
                .HasMaxLength(30)
                .HasColumnName("acs_init_request_subloc_ident");
            entity.Property(e => e.AcsInitSex)
                .HasMaxLength(20)
                .HasColumnName("acs_init_sex");
            entity.Property(e => e.AcsInitSireEartag)
                .HasMaxLength(30)
                .HasColumnName("acs_init_sire_eartag");
            entity.Property(e => e.AcsInitSireEtType)
                .HasMaxLength(20)
                .HasColumnName("acs_init_sire_et_type");
            entity.Property(e => e.AcsInitSurrDamEartag)
                .HasMaxLength(30)
                .HasColumnName("acs_init_surr_dam_eartag");
            entity.Property(e => e.AcsInitSurrDamEtType)
                .HasMaxLength(20)
                .HasColumnName("acs_init_surr_dam_et_type");
            entity.Property(e => e.AcsInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("acs_interface_file_name");
            entity.Property(e => e.AcsInterfaceFileTxn)
                .HasPrecision(4)
                .HasColumnName("acs_interface_file_txn");
            entity.Property(e => e.AcsLateAppLetter).HasColumnName("acs_late_app_letter");
            entity.Property(e => e.AcsMigratedAppsusKey)
                .HasPrecision(12)
                .HasColumnName("acs_migrated_appsus_key");
            entity.Property(e => e.AcsNewEartag)
                .HasMaxLength(30)
                .HasColumnName("acs_new_eartag");
            entity.Property(e => e.AcsNewEartagType)
                .HasMaxLength(20)
                .HasColumnName("acs_new_eartag_type");
            entity.Property(e => e.AcsPassportVersionNo)
                .HasMaxLength(2)
                .HasColumnName("acs_passport_version_no");
            entity.Property(e => e.AcsRanId)
                .HasPrecision(12)
                .HasColumnName("acs_ran_id");
            entity.Property(e => e.AcsRapId)
                .HasPrecision(12)
                .HasColumnName("acs_rap_id");
            entity.Property(e => e.AcsRefusedLetter).HasColumnName("acs_refused_letter");
            entity.Property(e => e.AcsReminderLetter).HasColumnName("acs_reminder_letter");
            entity.Property(e => e.AcsRequestLetter).HasColumnName("acs_request_letter");
            entity.Property(e => e.AcsSanId)
                .HasPrecision(12)
                .HasColumnName("acs_san_id");
            entity.Property(e => e.AcsSanOrRapInd)
                .HasMaxLength(3)
                .HasColumnName("acs_san_or_rap_ind");
            entity.Property(e => e.AcsSourceReference)
                .HasMaxLength(20)
                .HasColumnName("acs_source_reference");
            entity.Property(e => e.AcsSourceType)
                .HasMaxLength(3)
                .HasColumnName("acs_source_type");
            entity.Property(e => e.AcsSubmitAmendReason)
                .HasMaxLength(60)
                .HasColumnName("acs_submit_amend_reason");
            entity.Property(e => e.AcsSubmitDate).HasColumnName("acs_submit_date");
            entity.Property(e => e.AcsSubmitIntendedAction)
                .HasMaxLength(60)
                .HasColumnName("acs_submit_intended_action");
            entity.Property(e => e.AcsSubmitStatus)
                .HasMaxLength(20)
                .HasColumnName("acs_submit_status");
            entity.Property(e => e.AcsSubmitUser)
                .HasMaxLength(10)
                .HasColumnName("acs_submit_user");
            entity.Property(e => e.AcsSubmitWorkgroup)
                .HasMaxLength(6)
                .HasColumnName("acs_submit_workgroup");
            entity.Property(e => e.AcsSuspenseDatetime).HasColumnName("acs_suspense_datetime");
            entity.Property(e => e.AcsVapId)
                .HasPrecision(12)
                .HasColumnName("acs_vap_id");
            entity.Property(e => e.AcsVersion)
                .HasPrecision(6)
                .HasColumnName("acs_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtAnimalIdentifier>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_animal_identifiers_pkey");

            entity.ToTable("ct_animal_identifiers", "cts_transactions");

            entity.HasIndex(e => e.AidAidIdOriginal, "ct_animal_identifiers_aid_aid_id_original_idx");

            entity.HasIndex(e => e.AidAidIdPrevious, "ct_animal_identifiers_aid_aid_id_previous_idx");

            entity.HasIndex(e => e.AidEidId, "ct_animal_identifiers_aid_eid_id_idx");

            entity.HasIndex(e => e.AidEtgId, "ct_animal_identifiers_aid_etg_id_idx");

            entity.HasIndex(e => e.AidLocIdAssigned, "ct_animal_identifiers_aid_loc_id_assigned_idx");

            entity.HasIndex(e => e.AidRanId, "ct_animal_identifiers_aid_ran_id_idx");

            entity.HasIndex(e => e.AidAudDatetime, "ct_animal_identifiers_aud_datetime_idx");

            entity.HasIndex(e => e.AidAudId, "ct_animal_identifiers_aud_id_idx");

            entity.HasIndex(e => new { e.AidAudType, e.AidAudDatetime }, "ct_animal_identifiers_aud_type_datetime_idx");

            entity.HasIndex(e => e.AidAudType, "ct_animal_identifiers_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_animal_identifiers_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_identifiers_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_animal_identifiers_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_identifiers_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_animal_identifiers_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_animal_identifiers_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_animal_identifiers_record_type_idx");

            entity.HasIndex(e => e.AidId, "ct_animal_identifiers_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_animal_identifiers_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.AidAidIdOriginal)
                .HasPrecision(12)
                .HasColumnName("aid_aid_id_original");
            entity.Property(e => e.AidAidIdPrevious)
                .HasPrecision(12)
                .HasColumnName("aid_aid_id_previous");
            entity.Property(e => e.AidAssignedLocationRepd)
                .HasMaxLength(17)
                .HasColumnName("aid_assigned_location_repd");
            entity.Property(e => e.AidAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("aid_aud_datetime");
            entity.Property(e => e.AidAudId).HasColumnName("aid_aud_id");
            entity.Property(e => e.AidAudType).HasColumnName("aid_aud_type");
            entity.Property(e => e.AidCurrentFlag)
                .HasMaxLength(1)
                .HasColumnName("aid_current_flag");
            entity.Property(e => e.AidCurrentModifiedDate).HasColumnName("aid_current_modified_date");
            entity.Property(e => e.AidCurrentPid)
                .HasPrecision(3)
                .HasColumnName("aid_current_pid");
            entity.Property(e => e.AidCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("aid_current_status");
            entity.Property(e => e.AidCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("aid_current_user");
            entity.Property(e => e.AidEffectiveFromDate).HasColumnName("aid_effective_from_date");
            entity.Property(e => e.AidEffectiveToDate).HasColumnName("aid_effective_to_date");
            entity.Property(e => e.AidEidId)
                .HasPrecision(12)
                .HasColumnName("aid_eid_id");
            entity.Property(e => e.AidEtgId)
                .HasPrecision(12)
                .HasColumnName("aid_etg_id");
            entity.Property(e => e.AidId)
                .HasPrecision(12)
                .HasColumnName("aid_id");
            entity.Property(e => e.AidIdentifier)
                .HasMaxLength(50)
                .HasColumnName("aid_identifier");
            entity.Property(e => e.AidIdentifierType)
                .HasMaxLength(2)
                .HasColumnName("aid_identifier_type");
            entity.Property(e => e.AidLocIdAssigned)
                .HasPrecision(12)
                .HasColumnName("aid_loc_id_assigned");
            entity.Property(e => e.AidRanId)
                .HasPrecision(12)
                .HasColumnName("aid_ran_id");
            entity.Property(e => e.AidVersion)
                .HasPrecision(6)
                .HasColumnName("aid_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtAnimalRelationship>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_animal_relationships_pkey");

            entity.ToTable("ct_animal_relationships", "cts_transactions");

            entity.HasIndex(e => e.AarLocId, "ct_animal_relationships_aar_loc_id_idx");

            entity.HasIndex(e => e.AarRanIdChild, "ct_animal_relationships_aar_ran_id_child_idx");

            entity.HasIndex(e => e.AarRanIdParent, "ct_animal_relationships_aar_ran_id_parent_idx");

            entity.HasIndex(e => e.AarAudDatetime, "ct_animal_relationships_aud_datetime_idx");

            entity.HasIndex(e => e.AarAudId, "ct_animal_relationships_aud_id_idx");

            entity.HasIndex(e => new { e.AarAudType, e.AarAudDatetime }, "ct_animal_relationships_aud_type_datetime_idx");

            entity.HasIndex(e => e.AarAudType, "ct_animal_relationships_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_animal_relationships_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_relationships_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_animal_relationships_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_relationships_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_animal_relationships_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_animal_relationships_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_animal_relationships_record_type_idx");

            entity.HasIndex(e => e.AarId, "ct_animal_relationships_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_animal_relationships_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.AarAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("aar_aud_datetime");
            entity.Property(e => e.AarAudId).HasColumnName("aar_aud_id");
            entity.Property(e => e.AarAudType).HasColumnName("aar_aud_type");
            entity.Property(e => e.AarCancelledReason)
                .HasMaxLength(3)
                .HasColumnName("aar_cancelled_reason");
            entity.Property(e => e.AarConfidenceIndicator)
                .HasPrecision(1)
                .HasColumnName("aar_confidence_indicator");
            entity.Property(e => e.AarCurrentModifiedDate).HasColumnName("aar_current_modified_date");
            entity.Property(e => e.AarCurrentPid)
                .HasPrecision(3)
                .HasColumnName("aar_current_pid");
            entity.Property(e => e.AarCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("aar_current_status");
            entity.Property(e => e.AarCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("aar_current_user");
            entity.Property(e => e.AarEffectiveFromDate).HasColumnName("aar_effective_from_date");
            entity.Property(e => e.AarEffectiveToDate).HasColumnName("aar_effective_to_date");
            entity.Property(e => e.AarId)
                .HasPrecision(12)
                .HasColumnName("aar_id");
            entity.Property(e => e.AarLocId)
                .HasPrecision(12)
                .HasColumnName("aar_loc_id");
            entity.Property(e => e.AarParentIdentifier)
                .HasMaxLength(20)
                .HasColumnName("aar_parent_identifier");
            entity.Property(e => e.AarParentIdentifierType)
                .HasMaxLength(2)
                .HasColumnName("aar_parent_identifier_type");
            entity.Property(e => e.AarRanIdChild)
                .HasPrecision(12)
                .HasColumnName("aar_ran_id_child");
            entity.Property(e => e.AarRanIdParent)
                .HasPrecision(12)
                .HasColumnName("aar_ran_id_parent");
            entity.Property(e => e.AarRelType)
                .HasMaxLength(3)
                .HasColumnName("aar_rel_type");
            entity.Property(e => e.AarVersion)
                .HasPrecision(6)
                .HasColumnName("aar_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtAnimalStatus>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_animal_statuses_pkey");

            entity.ToTable("ct_animal_statuses", "cts_transactions");

            entity.HasIndex(e => e.AstRanId, "ct_animal_statuses_ast_ran_id_idx");

            entity.HasIndex(e => e.AstAudDatetime, "ct_animal_statuses_aud_datetime_idx");

            entity.HasIndex(e => e.AstAudId, "ct_animal_statuses_aud_id_idx");

            entity.HasIndex(e => new { e.AstAudType, e.AstAudDatetime }, "ct_animal_statuses_aud_type_datetime_idx");

            entity.HasIndex(e => e.AstAudType, "ct_animal_statuses_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_animal_statuses_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_statuses_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_animal_statuses_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_animal_statuses_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_animal_statuses_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_animal_statuses_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_animal_statuses_record_type_idx");

            entity.HasIndex(e => e.AstId, "ct_animal_statuses_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_animal_statuses_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.AstAddMoves)
                .HasPrecision(4)
                .HasColumnName("ast_add_moves");
            entity.Property(e => e.AstAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("ast_aud_datetime");
            entity.Property(e => e.AstAudId).HasColumnName("ast_aud_id");
            entity.Property(e => e.AstAudType).HasColumnName("ast_aud_type");
            entity.Property(e => e.AstChangeReceivedDate).HasColumnName("ast_change_received_date");
            entity.Property(e => e.AstId)
                .HasPrecision(12)
                .HasColumnName("ast_id");
            entity.Property(e => e.AstIntendedAction)
                .HasMaxLength(2)
                .HasColumnName("ast_intended_action");
            entity.Property(e => e.AstModifiedDate).HasColumnName("ast_modified_date");
            entity.Property(e => e.AstPid)
                .HasPrecision(3)
                .HasColumnName("ast_pid");
            entity.Property(e => e.AstRanId)
                .HasPrecision(12)
                .HasColumnName("ast_ran_id");
            entity.Property(e => e.AstStatus)
                .HasMaxLength(2)
                .HasColumnName("ast_status");
            entity.Property(e => e.AstTracedMoves)
                .HasPrecision(4)
                .HasColumnName("ast_traced_moves");
            entity.Property(e => e.AstUser)
                .HasMaxLength(10)
                .HasColumnName("ast_user");
            entity.Property(e => e.AstVersion)
                .HasPrecision(6)
                .HasColumnName("ast_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtApplicStatus>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_applic_statuses_pkey");

            entity.ToTable("ct_applic_statuses", "cts_transactions");

            entity.HasIndex(e => e.ApsVapId, "ct_applic_statuses_aps_vap_id_idx");

            entity.HasIndex(e => e.ApsAudDatetime, "ct_applic_statuses_aud_datetime_idx");

            entity.HasIndex(e => e.ApsAudId, "ct_applic_statuses_aud_id_idx");

            entity.HasIndex(e => new { e.ApsAudType, e.ApsAudDatetime }, "ct_applic_statuses_aud_type_datetime_idx");

            entity.HasIndex(e => e.ApsAudType, "ct_applic_statuses_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_applic_statuses_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_applic_statuses_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_applic_statuses_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_applic_statuses_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_applic_statuses_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_applic_statuses_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_applic_statuses_record_type_idx");

            entity.HasIndex(e => e.ApsId, "ct_applic_statuses_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_applic_statuses_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.ApsAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("aps_aud_datetime");
            entity.Property(e => e.ApsAudId).HasColumnName("aps_aud_id");
            entity.Property(e => e.ApsAudType).HasColumnName("aps_aud_type");
            entity.Property(e => e.ApsId)
                .HasPrecision(12)
                .HasColumnName("aps_id");
            entity.Property(e => e.ApsIntendedAction)
                .HasMaxLength(2)
                .HasColumnName("aps_intended_action");
            entity.Property(e => e.ApsModifiedDate).HasColumnName("aps_modified_date");
            entity.Property(e => e.ApsPid)
                .HasPrecision(3)
                .HasColumnName("aps_pid");
            entity.Property(e => e.ApsStatus)
                .HasMaxLength(2)
                .HasColumnName("aps_status");
            entity.Property(e => e.ApsUser)
                .HasMaxLength(10)
                .HasColumnName("aps_user");
            entity.Property(e => e.ApsVapId)
                .HasPrecision(12)
                .HasColumnName("aps_vap_id");
            entity.Property(e => e.ApsVersion)
                .HasPrecision(6)
                .HasColumnName("aps_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtApplicationLateDay>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_application_late_days_pkey");

            entity.ToTable("ct_application_late_days", "cts_transactions");

            entity.HasIndex(e => e.AldAudDatetime, "ct_application_late_days_aud_datetime_idx");

            entity.HasIndex(e => e.AldAudId, "ct_application_late_days_aud_id_idx");

            entity.HasIndex(e => new { e.AldAudType, e.AldAudDatetime }, "ct_application_late_days_aud_type_datetime_idx");

            entity.HasIndex(e => e.AldAudType, "ct_application_late_days_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_application_late_days_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_application_late_days_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_application_late_days_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_application_late_days_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_application_late_days_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_application_late_days_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_application_late_days_record_type_idx");

            entity.HasIndex(e => e.AldId, "ct_application_late_days_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_application_late_days_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.AldAdditionalDaysLate)
                .HasPrecision(3)
                .HasColumnName("ald_additional_days_late");
            entity.Property(e => e.AldApplicationType)
                .HasMaxLength(2)
                .HasColumnName("ald_application_type");
            entity.Property(e => e.AldAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("ald_aud_datetime");
            entity.Property(e => e.AldAudId).HasColumnName("ald_aud_id");
            entity.Property(e => e.AldAudType).HasColumnName("ald_aud_type");
            entity.Property(e => e.AldCurrentModifiedDate).HasColumnName("ald_current_modified_date");
            entity.Property(e => e.AldCurrentPid)
                .HasPrecision(3)
                .HasColumnName("ald_current_pid");
            entity.Property(e => e.AldCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("ald_current_status");
            entity.Property(e => e.AldCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("ald_current_user");
            entity.Property(e => e.AldEffectiveFromDate).HasColumnName("ald_effective_from_date");
            entity.Property(e => e.AldId)
                .HasPrecision(12)
                .HasColumnName("ald_id");
            entity.Property(e => e.AldValidDays)
                .HasPrecision(3)
                .HasColumnName("ald_valid_days");
            entity.Property(e => e.AldVersion)
                .HasPrecision(6)
                .HasColumnName("ald_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtBatchRetentionConf>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_batch_retention_conf_transactions_pkey");

            entity.ToTable("ct_batch_retention_conf", "cts_transactions");

            entity.HasIndex(e => e.BrtAudDatetime, "ct_batch_retention_conf_aud_datetime_idx");

            entity.HasIndex(e => e.BrtAudId, "ct_batch_retention_conf_aud_id_idx");

            entity.HasIndex(e => new { e.BrtAudType, e.BrtAudDatetime }, "ct_batch_retention_conf_aud_type_datetime_idx");

            entity.HasIndex(e => e.BrtAudType, "ct_batch_retention_conf_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_batch_retention_conf_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_batch_retention_conf_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_batch_retention_conf_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_batch_retention_conf_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_batch_retention_conf_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_batch_retention_conf_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_batch_retention_conf_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.BrtAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("brt_aud_datetime");
            entity.Property(e => e.BrtAudId).HasColumnName("brt_aud_id");
            entity.Property(e => e.BrtAudType).HasColumnName("brt_aud_type");
            entity.Property(e => e.BrtDescription)
                .HasMaxLength(200)
                .HasColumnName("brt_description");
            entity.Property(e => e.BrtItemId)
                .HasMaxLength(50)
                .HasColumnName("brt_item_id");
            entity.Property(e => e.BrtRetentionDays)
                .HasPrecision(4)
                .HasColumnName("brt_retention_days");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtBreed>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_breeds_transactions_pkey");

            entity.ToTable("ct_breeds", "cts_transactions");

            entity.HasIndex(e => e.BrdAudDatetime, "ct_breeds_aud_datetime_idx");

            entity.HasIndex(e => e.BrdAudId, "ct_breeds_aud_id_idx");

            entity.HasIndex(e => new { e.BrdAudType, e.BrdAudDatetime }, "ct_breeds_aud_type_datetime_idx");

            entity.HasIndex(e => e.BrdAudType, "ct_breeds_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.BrdType, e.BrdAudType, e.BrdAudDatetime }, "ct_breeds_file_brd_type_aud_datetime_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.BrdType, e.BrdAudType }, "ct_breeds_file_brd_type_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.BrdType }, "ct_breeds_file_brd_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_breeds_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_breeds_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_breeds_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_breeds_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_breeds_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_breeds_record_type_idx");

            entity.HasIndex(e => e.BrdId, "ct_breeds_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_breeds_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.BrdAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("brd_aud_datetime");
            entity.Property(e => e.BrdAudId).HasColumnName("brd_aud_id");
            entity.Property(e => e.BrdAudType).HasColumnName("brd_aud_type");
            entity.Property(e => e.BrdCode)
                .HasMaxLength(5)
                .HasColumnName("brd_code");
            entity.Property(e => e.BrdCurrentModifiedDate).HasColumnName("brd_current_modified_date");
            entity.Property(e => e.BrdCurrentPid)
                .HasPrecision(3)
                .HasColumnName("brd_current_pid");
            entity.Property(e => e.BrdCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("brd_current_status");
            entity.Property(e => e.BrdCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("brd_current_user");
            entity.Property(e => e.BrdId)
                .HasPrecision(12)
                .HasColumnName("brd_id");
            entity.Property(e => e.BrdLongDescription)
                .HasMaxLength(60)
                .HasColumnName("brd_long_description");
            entity.Property(e => e.BrdSchemeEligibility)
                .HasMaxLength(10)
                .HasColumnName("brd_scheme_eligibility");
            entity.Property(e => e.BrdShortDescription)
                .HasMaxLength(20)
                .HasColumnName("brd_short_description");
            entity.Property(e => e.BrdType)
                .HasMaxLength(2)
                .HasColumnName("brd_type");
            entity.Property(e => e.BrdVersion)
                .HasPrecision(6)
                .HasColumnName("brd_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtClaExtract>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_cla_extract_pkey");

            entity.ToTable("ct_cla_extract", "cts_transactions");

            entity.HasIndex(e => e.CleAudDatetime, "ct_cla_extract_aud_datetime_idx");

            entity.HasIndex(e => e.CleAudId, "ct_cla_extract_aud_id_idx");

            entity.HasIndex(e => new { e.CleAudType, e.CleAudDatetime }, "ct_cla_extract_aud_type_datetime_idx");

            entity.HasIndex(e => e.CleAudType, "ct_cla_extract_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_cla_extract_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cla_extract_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_cla_extract_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cla_extract_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_cla_extract_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_cla_extract_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_cla_extract_record_type_idx");

            entity.HasIndex(e => e.CleId, "ct_cla_extract_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_cla_extract_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CleAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cle_aud_datetime");
            entity.Property(e => e.CleAudId).HasColumnName("cle_aud_id");
            entity.Property(e => e.CleAudType).HasColumnName("cle_aud_type");
            entity.Property(e => e.CleBatchId)
                .HasPrecision(12)
                .HasColumnName("cle_batch_id");
            entity.Property(e => e.CleBulkRunStop)
                .HasMaxLength(1)
                .HasColumnName("cle_bulk_run_stop");
            entity.Property(e => e.CleCurrentModifiedDate).HasColumnName("cle_current_modified_date");
            entity.Property(e => e.CleDataReadEnd).HasColumnName("cle_data_read_end");
            entity.Property(e => e.CleDataReadStart).HasColumnName("cle_data_read_start");
            entity.Property(e => e.CleId)
                .HasPrecision(12)
                .HasColumnName("cle_id");
            entity.Property(e => e.CleRunEnd).HasColumnName("cle_run_end");
            entity.Property(e => e.CleRunStart).HasColumnName("cle_run_start");
            entity.Property(e => e.CleRunStatus)
                .HasMaxLength(1000)
                .HasColumnName("cle_run_status");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtClaExtractDetail>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_cla_extract_detail_pkey");

            entity.ToTable("ct_cla_extract_detail", "cts_transactions");

            entity.HasIndex(e => e.CldAudDatetime, "ct_cla_extract_detail_aud_datetime_idx");

            entity.HasIndex(e => e.CldAudId, "ct_cla_extract_detail_aud_id_idx");

            entity.HasIndex(e => new { e.CldAudType, e.CldAudDatetime }, "ct_cla_extract_detail_aud_type_datetime_idx");

            entity.HasIndex(e => e.CldAudType, "ct_cla_extract_detail_aud_type_idx");

            entity.HasIndex(e => e.CldCleId, "ct_cla_extract_detail_cld_cle_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_cla_extract_detail_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_cla_extract_detail_file_trans_type_idx");

            entity.HasIndex(e => e.CtsFileImportId, "ct_cla_extract_detail_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_cla_extract_detail_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_cla_extract_detail_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_cla_extract_detail_record_type_idx");

            entity.HasIndex(e => e.CldId, "ct_cla_extract_detail_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_cla_extract_detail_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CldAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cld_aud_datetime");
            entity.Property(e => e.CldAudId).HasColumnName("cld_aud_id");
            entity.Property(e => e.CldAudType).HasColumnName("cld_aud_type");
            entity.Property(e => e.CldBatchId)
                .HasPrecision(12)
                .HasColumnName("cld_batch_id");
            entity.Property(e => e.CldCleId)
                .HasPrecision(12)
                .HasColumnName("cld_cle_id");
            entity.Property(e => e.CldCurrentModifiedDate).HasColumnName("cld_current_modified_date");
            entity.Property(e => e.CldId)
                .HasPrecision(12)
                .HasColumnName("cld_id");
            entity.Property(e => e.CldRunEnd).HasColumnName("cld_run_end");
            entity.Property(e => e.CldRunStart).HasColumnName("cld_run_start");
            entity.Property(e => e.CldTableName)
                .HasMaxLength(30)
                .HasColumnName("cld_table_name");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtClaExtractDm>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_cla_extract_dm_pkey");

            entity.ToTable("ct_cla_extract_dm", "cts_transactions");

            entity.HasIndex(e => e.CleAudDatetime, "ct_cla_extract_dm_aud_datetime_idx");

            entity.HasIndex(e => e.CleAudId, "ct_cla_extract_dm_aud_id_idx");

            entity.HasIndex(e => new { e.CleAudType, e.CleAudDatetime }, "ct_cla_extract_dm_aud_type_datetime_idx");

            entity.HasIndex(e => e.CleAudType, "ct_cla_extract_dm_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_cla_extract_dm_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cla_extract_dm_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_cla_extract_dm_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cla_extract_dm_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_cla_extract_dm_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_cla_extract_dm_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_cla_extract_dm_record_type_idx");

            entity.HasIndex(e => e.CleId, "ct_cla_extract_dm_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_cla_extract_dm_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CleAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cle_aud_datetime");
            entity.Property(e => e.CleAudId).HasColumnName("cle_aud_id");
            entity.Property(e => e.CleAudType).HasColumnName("cle_aud_type");
            entity.Property(e => e.CleBatchId)
                .HasPrecision(12)
                .HasColumnName("cle_batch_id");
            entity.Property(e => e.CleBulkRunStop)
                .HasMaxLength(1)
                .HasColumnName("cle_bulk_run_stop");
            entity.Property(e => e.CleCurrentModifiedDate).HasColumnName("cle_current_modified_date");
            entity.Property(e => e.CleDataReadEnd).HasColumnName("cle_data_read_end");
            entity.Property(e => e.CleDataReadStart).HasColumnName("cle_data_read_start");
            entity.Property(e => e.CleId)
                .HasPrecision(12)
                .HasColumnName("cle_id");
            entity.Property(e => e.CleRunEnd).HasColumnName("cle_run_end");
            entity.Property(e => e.CleRunStart).HasColumnName("cle_run_start");
            entity.Property(e => e.CleRunStatus)
                .HasMaxLength(1000)
                .HasColumnName("cle_run_status");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtClaMiniDetail>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_cla_mini_detail_pkey");

            entity.ToTable("ct_cla_mini_detail", "cts_transactions");

            entity.HasIndex(e => e.CldAudDatetime, "ct_cla_mini_detail_aud_datetime_idx");

            entity.HasIndex(e => e.CldAudId, "ct_cla_mini_detail_aud_id_idx");

            entity.HasIndex(e => new { e.CldAudType, e.CldAudDatetime }, "ct_cla_mini_detail_aud_type_datetime_idx");

            entity.HasIndex(e => e.CldAudType, "ct_cla_mini_detail_aud_type_idx");

            entity.HasIndex(e => e.CldCleId, "ct_cla_mini_detail_cld_cle_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_cla_mini_detail_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_cla_mini_detail_file_trans_type_idx");

            entity.HasIndex(e => e.CtsFileImportId, "ct_cla_mini_detail_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_cla_mini_detail_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_cla_mini_detail_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_cla_mini_detail_record_type_idx");

            entity.HasIndex(e => e.CldId, "ct_cla_mini_detail_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_cla_mini_detail_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CldAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cld_aud_datetime");
            entity.Property(e => e.CldAudId).HasColumnName("cld_aud_id");
            entity.Property(e => e.CldAudType).HasColumnName("cld_aud_type");
            entity.Property(e => e.CldBatchId)
                .HasPrecision(12)
                .HasColumnName("cld_batch_id");
            entity.Property(e => e.CldCleId)
                .HasPrecision(12)
                .HasColumnName("cld_cle_id");
            entity.Property(e => e.CldCurrentModifiedDate).HasColumnName("cld_current_modified_date");
            entity.Property(e => e.CldId)
                .HasPrecision(12)
                .HasColumnName("cld_id");
            entity.Property(e => e.CldRunEnd).HasColumnName("cld_run_end");
            entity.Property(e => e.CldRunStart).HasColumnName("cld_run_start");
            entity.Property(e => e.CldTableName)
                .HasMaxLength(30)
                .HasColumnName("cld_table_name");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtClaMiniExtract>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_cla_mini_extract_pkey");

            entity.ToTable("ct_cla_mini_extract", "cts_transactions");

            entity.HasIndex(e => e.CleAudDatetime, "ct_cla_mini_extract_aud_datetime_idx");

            entity.HasIndex(e => e.CleAudId, "ct_cla_mini_extract_aud_id_idx");

            entity.HasIndex(e => new { e.CleAudType, e.CleAudDatetime }, "ct_cla_mini_extract_aud_type_datetime_idx");

            entity.HasIndex(e => e.CleAudType, "ct_cla_mini_extract_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_cla_mini_extract_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cla_mini_extract_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_cla_mini_extract_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cla_mini_extract_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_cla_mini_extract_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_cla_mini_extract_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_cla_mini_extract_record_type_idx");

            entity.HasIndex(e => e.CleId, "ct_cla_mini_extract_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_cla_mini_extract_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CleAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cle_aud_datetime");
            entity.Property(e => e.CleAudId).HasColumnName("cle_aud_id");
            entity.Property(e => e.CleAudType).HasColumnName("cle_aud_type");
            entity.Property(e => e.CleBatchId)
                .HasPrecision(12)
                .HasColumnName("cle_batch_id");
            entity.Property(e => e.CleBulkRunStop)
                .HasMaxLength(1)
                .HasColumnName("cle_bulk_run_stop");
            entity.Property(e => e.CleCurrentModifiedDate).HasColumnName("cle_current_modified_date");
            entity.Property(e => e.CleDataReadEnd).HasColumnName("cle_data_read_end");
            entity.Property(e => e.CleDataReadStart).HasColumnName("cle_data_read_start");
            entity.Property(e => e.CleId)
                .HasPrecision(12)
                .HasColumnName("cle_id");
            entity.Property(e => e.CleRunEnd).HasColumnName("cle_run_end");
            entity.Property(e => e.CleRunStart).HasColumnName("cle_run_start");
            entity.Property(e => e.CleRunStatus)
                .HasMaxLength(1000)
                .HasColumnName("cle_run_status");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtClaimStatus>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_claim_statuses_transactions_pkey");

            entity.ToTable("ct_claim_statuses", "cts_transactions");

            entity.HasIndex(e => e.ClsAudDatetime, "ct_claim_statuses_aud_datetime_idx");

            entity.HasIndex(e => e.ClsAudId, "ct_claim_statuses_aud_id_idx");

            entity.HasIndex(e => new { e.ClsAudType, e.ClsAudDatetime }, "ct_claim_statuses_aud_type_datetime_idx");

            entity.HasIndex(e => e.ClsAudType, "ct_claim_statuses_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_claim_statuses_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_claim_statuses_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_claim_statuses_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_claim_statuses_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_claim_statuses_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_claim_statuses_record_type_idx");

            entity.HasIndex(e => e.ClsId, "ct_claim_statuses_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_claim_statuses_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.ClsAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cls_aud_datetime");
            entity.Property(e => e.ClsAudId).HasColumnName("cls_aud_id");
            entity.Property(e => e.ClsAudType).HasColumnName("cls_aud_type");
            entity.Property(e => e.ClsClaimStatus)
                .HasMaxLength(2)
                .HasColumnName("cls_claim_status");
            entity.Property(e => e.ClsCurrentModifiedDate).HasColumnName("cls_current_modified_date");
            entity.Property(e => e.ClsCurrentPid)
                .HasPrecision(3)
                .HasColumnName("cls_current_pid");
            entity.Property(e => e.ClsCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("cls_current_status");
            entity.Property(e => e.ClsCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("cls_current_user");
            entity.Property(e => e.ClsDescription)
                .HasMaxLength(240)
                .HasColumnName("cls_description");
            entity.Property(e => e.ClsId)
                .HasPrecision(12)
                .HasColumnName("cls_id");
            entity.Property(e => e.ClsVersion)
                .HasPrecision(6)
                .HasColumnName("cls_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtClaimType>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_claim_types_transactions_pkey");

            entity.ToTable("ct_claim_types", "cts_transactions");

            entity.HasIndex(e => e.CltAudDatetime, "ct_claim_types_aud_datetime_idx");

            entity.HasIndex(e => e.CltAudId, "ct_claim_types_aud_id_idx");

            entity.HasIndex(e => new { e.CltAudType, e.CltAudDatetime }, "ct_claim_types_aud_type_datetime_idx");

            entity.HasIndex(e => e.CltAudType, "ct_claim_types_aud_type_idx");

            entity.HasIndex(e => e.CltSchId, "ct_claim_types_clt_sch_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_claim_types_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_claim_types_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_claim_types_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_claim_types_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_claim_types_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_claim_types_record_type_idx");

            entity.HasIndex(e => e.CltId, "ct_claim_types_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_claim_types_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CltAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("clt_aud_datetime");
            entity.Property(e => e.CltAudId).HasColumnName("clt_aud_id");
            entity.Property(e => e.CltAudType).HasColumnName("clt_aud_type");
            entity.Property(e => e.CltClaimType)
                .HasMaxLength(1)
                .HasColumnName("clt_claim_type");
            entity.Property(e => e.CltCurrentModifiedDate).HasColumnName("clt_current_modified_date");
            entity.Property(e => e.CltCurrentPid)
                .HasPrecision(3)
                .HasColumnName("clt_current_pid");
            entity.Property(e => e.CltCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("clt_current_status");
            entity.Property(e => e.CltCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("clt_current_user");
            entity.Property(e => e.CltDescription)
                .HasMaxLength(240)
                .HasColumnName("clt_description");
            entity.Property(e => e.CltId)
                .HasPrecision(12)
                .HasColumnName("clt_id");
            entity.Property(e => e.CltSchId)
                .HasPrecision(12)
                .HasColumnName("clt_sch_id");
            entity.Property(e => e.CltVersion)
                .HasPrecision(6)
                .HasColumnName("clt_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtCmAuthority>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_cm_authorities_transactions_pkey");

            entity.ToTable("ct_cm_authorities", "cts_transactions");

            entity.HasIndex(e => e.CmaAudDatetime, "ct_cm_authorities_aud_datetime_idx");

            entity.HasIndex(e => e.CmaAudId, "ct_cm_authorities_aud_id_idx");

            entity.HasIndex(e => new { e.CmaAudType, e.CmaAudDatetime }, "ct_cm_authorities_aud_type_datetime_idx");

            entity.HasIndex(e => e.CmaAudType, "ct_cm_authorities_aud_type_idx");

            entity.HasIndex(e => e.CmaCotId, "ct_cm_authorities_cma_cot_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_cm_authorities_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cm_authorities_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_cm_authorities_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_cm_authorities_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_cm_authorities_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_cm_authorities_record_type_idx");

            entity.HasIndex(e => e.CmaId, "ct_cm_authorities_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_cm_authorities_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CmaAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cma_aud_datetime");
            entity.Property(e => e.CmaAudId).HasColumnName("cma_aud_id");
            entity.Property(e => e.CmaAudType).HasColumnName("cma_aud_type");
            entity.Property(e => e.CmaAuthorityCode)
                .HasMaxLength(10)
                .HasColumnName("cma_authority_code");
            entity.Property(e => e.CmaCotId)
                .HasPrecision(12)
                .HasColumnName("cma_cot_id");
            entity.Property(e => e.CmaCurrentModifiedDate).HasColumnName("cma_current_modified_date");
            entity.Property(e => e.CmaCurrentPid)
                .HasPrecision(3)
                .HasColumnName("cma_current_pid");
            entity.Property(e => e.CmaCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("cma_current_status");
            entity.Property(e => e.CmaCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("cma_current_user");
            entity.Property(e => e.CmaId)
                .HasPrecision(12)
                .HasColumnName("cma_id");
            entity.Property(e => e.CmaLongName)
                .HasMaxLength(60)
                .HasColumnName("cma_long_name");
            entity.Property(e => e.CmaShortName)
                .HasMaxLength(30)
                .HasColumnName("cma_short_name");
            entity.Property(e => e.CmaVersion)
                .HasPrecision(6)
                .HasColumnName("cma_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtCmMeasuresResult>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_cm_measures_results_pkey");

            entity.ToTable("ct_cm_measures_results", "cts_transactions");

            entity.HasIndex(e => e.CmrAudDatetime, "ct_cm_measures_results_aud_datetime_idx");

            entity.HasIndex(e => e.CmrAudId, "ct_cm_measures_results_aud_id_idx");

            entity.HasIndex(e => new { e.CmrAudType, e.CmrAudDatetime }, "ct_cm_measures_results_aud_type_datetime_idx");

            entity.HasIndex(e => e.CmrAudType, "ct_cm_measures_results_aud_type_idx");

            entity.HasIndex(e => e.CmrComId, "ct_cm_measures_results_cmr_com_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_cm_measures_results_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cm_measures_results_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_cm_measures_results_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cm_measures_results_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_cm_measures_results_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_cm_measures_results_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_cm_measures_results_record_type_idx");

            entity.HasIndex(e => e.CmrId, "ct_cm_measures_results_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_cm_measures_results_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CmrAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cmr_aud_datetime");
            entity.Property(e => e.CmrAudId).HasColumnName("cmr_aud_id");
            entity.Property(e => e.CmrAudType).HasColumnName("cmr_aud_type");
            entity.Property(e => e.CmrComId)
                .HasPrecision(12)
                .HasColumnName("cmr_com_id");
            entity.Property(e => e.CmrCurrentModifiedDate).HasColumnName("cmr_current_modified_date");
            entity.Property(e => e.CmrCurrentPid)
                .HasPrecision(3)
                .HasColumnName("cmr_current_pid");
            entity.Property(e => e.CmrCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("cmr_current_status");
            entity.Property(e => e.CmrCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("cmr_current_user");
            entity.Property(e => e.CmrId)
                .HasPrecision(12)
                .HasColumnName("cmr_id");
            entity.Property(e => e.CmrMeasureChar)
                .HasMaxLength(10)
                .HasColumnName("cmr_measure_char");
            entity.Property(e => e.CmrMeasureNum)
                .HasPrecision(9)
                .HasColumnName("cmr_measure_num");
            entity.Property(e => e.CmrResultChar)
                .HasMaxLength(10)
                .HasColumnName("cmr_result_char");
            entity.Property(e => e.CmrResultNum)
                .HasPrecision(9)
                .HasColumnName("cmr_result_num");
            entity.Property(e => e.CmrVersion)
                .HasPrecision(6)
                .HasColumnName("cmr_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtCommsAddress>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_comms_addresses_pkey");

            entity.ToTable("ct_comms_addresses", "cts_transactions");

            entity.HasIndex(e => e.CoaAudDatetime, "ct_comms_addresses_aud_datetime_idx");

            entity.HasIndex(e => e.CoaAudId, "ct_comms_addresses_aud_id_idx");

            entity.HasIndex(e => new { e.CoaAudType, e.CoaAudDatetime }, "ct_comms_addresses_aud_type_datetime_idx");

            entity.HasIndex(e => e.CoaAudType, "ct_comms_addresses_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_comms_addresses_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_comms_addresses_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_comms_addresses_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_comms_addresses_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_comms_addresses_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_comms_addresses_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_comms_addresses_record_type_idx");

            entity.HasIndex(e => e.CoaId, "ct_comms_addresses_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_comms_addresses_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CoaAttachment)
                .HasMaxLength(1)
                .HasColumnName("coa_attachment");
            entity.Property(e => e.CoaAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("coa_aud_datetime");
            entity.Property(e => e.CoaAudId).HasColumnName("coa_aud_id");
            entity.Property(e => e.CoaAudType).HasColumnName("coa_aud_type");
            entity.Property(e => e.CoaCurrentModifiedDate).HasColumnName("coa_current_modified_date");
            entity.Property(e => e.CoaCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("coa_current_status");
            entity.Property(e => e.CoaCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("coa_current_user");
            entity.Property(e => e.CoaEmailAddress)
                .HasMaxLength(200)
                .HasColumnName("coa_email_address");
            entity.Property(e => e.CoaId)
                .HasPrecision(12)
                .HasColumnName("coa_id");
            entity.Property(e => e.CoaPid)
                .HasPrecision(3)
                .HasColumnName("coa_pid");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtCondVariantGrouping>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_cond_variant_groupings_transactions_pkey");

            entity.ToTable("ct_cond_variant_groupings", "cts_transactions");

            entity.HasIndex(e => e.CvgAudDatetime, "ct_cond_variant_groupings_aud_datetime_idx");

            entity.HasIndex(e => e.CvgAudId, "ct_cond_variant_groupings_aud_id_idx");

            entity.HasIndex(e => new { e.CvgAudType, e.CvgAudDatetime }, "ct_cond_variant_groupings_aud_type_datetime_idx");

            entity.HasIndex(e => e.CvgAudType, "ct_cond_variant_groupings_aud_type_idx");

            entity.HasIndex(e => e.CvgCovId, "ct_cond_variant_groupings_cvg_cov_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_cond_variant_groupings_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cond_variant_groupings_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_cond_variant_groupings_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_cond_variant_groupings_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_cond_variant_groupings_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_cond_variant_groupings_record_type_idx");

            entity.HasIndex(e => e.CvgId, "ct_cond_variant_groupings_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_cond_variant_groupings_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.CvgAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cvg_aud_datetime");
            entity.Property(e => e.CvgAudId).HasColumnName("cvg_aud_id");
            entity.Property(e => e.CvgAudType).HasColumnName("cvg_aud_type");
            entity.Property(e => e.CvgCovId)
                .HasPrecision(12)
                .HasColumnName("cvg_cov_id");
            entity.Property(e => e.CvgCurrentModifiedDate).HasColumnName("cvg_current_modified_date");
            entity.Property(e => e.CvgCurrentPid)
                .HasPrecision(3)
                .HasColumnName("cvg_current_pid");
            entity.Property(e => e.CvgCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("cvg_current_status");
            entity.Property(e => e.CvgCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("cvg_current_user");
            entity.Property(e => e.CvgGroupingCode)
                .HasMaxLength(10)
                .HasColumnName("cvg_grouping_code");
            entity.Property(e => e.CvgId)
                .HasPrecision(12)
                .HasColumnName("cvg_id");
            entity.Property(e => e.CvgVersion)
                .HasPrecision(6)
                .HasColumnName("cvg_version");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtCondition>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_conditions_transactions_pkey");

            entity.ToTable("ct_conditions", "cts_transactions");

            entity.HasIndex(e => e.ConAudDatetime, "ct_conditions_aud_datetime_idx");

            entity.HasIndex(e => e.ConAudId, "ct_conditions_aud_id_idx");

            entity.HasIndex(e => new { e.ConAudType, e.ConAudDatetime }, "ct_conditions_aud_type_datetime_idx");

            entity.HasIndex(e => e.ConAudType, "ct_conditions_aud_type_idx");

            entity.HasIndex(e => e.ConCotId, "ct_conditions_con_cot_id_idx");

            entity.HasIndex(e => e.ConPchId, "ct_conditions_con_pch_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_conditions_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_conditions_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_conditions_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_conditions_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_conditions_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_conditions_record_type_idx");

            entity.HasIndex(e => e.ConId, "ct_conditions_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_conditions_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.ConAllocationProcess)
                .HasMaxLength(10)
                .HasColumnName("con_allocation_process");
            entity.Property(e => e.ConAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("con_aud_datetime");
            entity.Property(e => e.ConAudId).HasColumnName("con_aud_id");
            entity.Property(e => e.ConAudType).HasColumnName("con_aud_type");
            entity.Property(e => e.ConConditionCode)
                .HasMaxLength(12)
                .HasColumnName("con_condition_code");
            entity.Property(e => e.ConCotId)
                .HasPrecision(12)
                .HasColumnName("con_cot_id");
            entity.Property(e => e.ConCurrentModifiedDate).HasColumnName("con_current_modified_date");
            entity.Property(e => e.ConCurrentPid)
                .HasPrecision(3)
                .HasColumnName("con_current_pid");
            entity.Property(e => e.ConCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("con_current_status");
            entity.Property(e => e.ConCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("con_current_user");
            entity.Property(e => e.ConId)
                .HasPrecision(12)
                .HasColumnName("con_id");
            entity.Property(e => e.ConLongDescription)
                .HasMaxLength(60)
                .HasColumnName("con_long_description");
            entity.Property(e => e.ConPchId)
                .HasPrecision(12)
                .HasColumnName("con_pch_id");
            entity.Property(e => e.ConReportRecipient)
                .HasMaxLength(2)
                .HasColumnName("con_report_recipient");
            entity.Property(e => e.ConScope)
                .HasMaxLength(1)
                .HasColumnName("con_scope");
            entity.Property(e => e.ConShortDescription)
                .HasMaxLength(20)
                .HasColumnName("con_short_description");
            entity.Property(e => e.ConVersion)
                .HasPrecision(6)
                .HasColumnName("con_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtConditionActivity>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_condition_activities_transactions_pkey");

            entity.ToTable("ct_condition_activities", "cts_transactions");

            entity.HasIndex(e => e.CacAudDatetime, "ct_condition_activities_aud_datetime_idx");

            entity.HasIndex(e => e.CacAudId, "ct_condition_activities_aud_id_idx");

            entity.HasIndex(e => new { e.CacAudType, e.CacAudDatetime }, "ct_condition_activities_aud_type_datetime_idx");

            entity.HasIndex(e => e.CacAudType, "ct_condition_activities_aud_type_idx");

            entity.HasIndex(e => e.CacConId, "ct_condition_activities_cac_con_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_condition_activities_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_condition_activities_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_condition_activities_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_condition_activities_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_condition_activities_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_condition_activities_record_type_idx");

            entity.HasIndex(e => e.CacId, "ct_condition_activities_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_condition_activities_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CacActivityCode)
                .HasMaxLength(8)
                .HasColumnName("cac_activity_code");
            entity.Property(e => e.CacAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cac_aud_datetime");
            entity.Property(e => e.CacAudId).HasColumnName("cac_aud_id");
            entity.Property(e => e.CacAudType).HasColumnName("cac_aud_type");
            entity.Property(e => e.CacConId)
                .HasPrecision(12)
                .HasColumnName("cac_con_id");
            entity.Property(e => e.CacCurrentModifiedDate).HasColumnName("cac_current_modified_date");
            entity.Property(e => e.CacCurrentPid)
                .HasPrecision(3)
                .HasColumnName("cac_current_pid");
            entity.Property(e => e.CacCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("cac_current_status");
            entity.Property(e => e.CacCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("cac_current_user");
            entity.Property(e => e.CacId)
                .HasPrecision(12)
                .HasColumnName("cac_id");
            entity.Property(e => e.CacLongDescription)
                .HasMaxLength(60)
                .HasColumnName("cac_long_description");
            entity.Property(e => e.CacShortDescription)
                .HasMaxLength(20)
                .HasColumnName("cac_short_description");
            entity.Property(e => e.CacVersion)
                .HasPrecision(6)
                .HasColumnName("cac_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtConditionMarker>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_condition_markers_pkey");

            entity.ToTable("ct_condition_markers", "cts_transactions");

            entity.HasIndex(e => e.ComAudDatetime, "ct_condition_markers_aud_datetime_idx");

            entity.HasIndex(e => e.ComAudId, "ct_condition_markers_aud_id_idx");

            entity.HasIndex(e => new { e.ComAudType, e.ComAudDatetime }, "ct_condition_markers_aud_type_datetime_idx");

            entity.HasIndex(e => e.ComAudType, "ct_condition_markers_aud_type_idx");

            entity.HasIndex(e => e.ComCacId, "ct_condition_markers_com_cac_id_idx");

            entity.HasIndex(e => e.ComCmaId, "ct_condition_markers_com_cma_id_idx");

            entity.HasIndex(e => e.ComCovId, "ct_condition_markers_com_cov_id_idx");

            entity.HasIndex(e => e.ComLocId, "ct_condition_markers_com_loc_id_idx");

            entity.HasIndex(e => e.ComMovId, "ct_condition_markers_com_mov_id_idx");

            entity.HasIndex(e => e.ComRanId, "ct_condition_markers_com_ran_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_condition_markers_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_condition_markers_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_condition_markers_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_condition_markers_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_condition_markers_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_condition_markers_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_condition_markers_record_type_idx");

            entity.HasIndex(e => e.ComId, "ct_condition_markers_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_condition_markers_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.ComAmendmentReasonCode)
                .HasMaxLength(3)
                .HasColumnName("com_amendment_reason_code");
            entity.Property(e => e.ComAmendmentReasonText)
                .HasMaxLength(60)
                .HasColumnName("com_amendment_reason_text");
            entity.Property(e => e.ComAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("com_aud_datetime");
            entity.Property(e => e.ComAudId).HasColumnName("com_aud_id");
            entity.Property(e => e.ComAudType).HasColumnName("com_aud_type");
            entity.Property(e => e.ComAutotagWaveNumber)
                .HasPrecision(2)
                .HasColumnName("com_autotag_wave_number");
            entity.Property(e => e.ComBranchNumber)
                .HasPrecision(1)
                .HasColumnName("com_branch_number");
            entity.Property(e => e.ComCacId)
                .HasPrecision(12)
                .HasColumnName("com_cac_id");
            entity.Property(e => e.ComCmaId)
                .HasPrecision(12)
                .HasColumnName("com_cma_id");
            entity.Property(e => e.ComComments)
                .HasMaxLength(1000)
                .HasColumnName("com_comments");
            entity.Property(e => e.ComCovId)
                .HasPrecision(12)
                .HasColumnName("com_cov_id");
            entity.Property(e => e.ComCurrentModifiedDate).HasColumnName("com_current_modified_date");
            entity.Property(e => e.ComCurrentPid)
                .HasPrecision(3)
                .HasColumnName("com_current_pid");
            entity.Property(e => e.ComCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("com_current_status");
            entity.Property(e => e.ComCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("com_current_user");
            entity.Property(e => e.ComDocumentRefs)
                .HasMaxLength(60)
                .HasColumnName("com_document_refs");
            entity.Property(e => e.ComEffectiveFromDate).HasColumnName("com_effective_from_date");
            entity.Property(e => e.ComEffectiveToDate).HasColumnName("com_effective_to_date");
            entity.Property(e => e.ComGroupingReference)
                .HasMaxLength(16)
                .HasColumnName("com_grouping_reference");
            entity.Property(e => e.ComId)
                .HasPrecision(12)
                .HasColumnName("com_id");
            entity.Property(e => e.ComLastProbityDate).HasColumnName("com_last_probity_date");
            entity.Property(e => e.ComLastUsedBudNumber)
                .HasPrecision(12)
                .HasColumnName("com_last_used_bud_number");
            entity.Property(e => e.ComLocId)
                .HasPrecision(12)
                .HasColumnName("com_loc_id");
            entity.Property(e => e.ComMarkerType)
                .HasMaxLength(1)
                .HasColumnName("com_marker_type");
            entity.Property(e => e.ComMovId)
                .HasPrecision(12)
                .HasColumnName("com_mov_id");
            entity.Property(e => e.ComRanId)
                .HasPrecision(12)
                .HasColumnName("com_ran_id");
            entity.Property(e => e.ComSource)
                .HasMaxLength(2)
                .HasColumnName("com_source");
            entity.Property(e => e.ComVersion)
                .HasPrecision(6)
                .HasColumnName("com_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.FakeData)
                .HasPrecision(1)
                .HasColumnName("fake_data");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtConditionMarkerError>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_condition_marker_errors_pkey");

            entity.ToTable("ct_condition_marker_errors", "cts_transactions");

            entity.HasIndex(e => e.CmeAudDatetime, "ct_condition_marker_errors_aud_datetime_idx");

            entity.HasIndex(e => e.CmeAudId, "ct_condition_marker_errors_aud_id_idx");

            entity.HasIndex(e => new { e.CmeAudType, e.CmeAudDatetime }, "ct_condition_marker_errors_aud_type_datetime_idx");

            entity.HasIndex(e => e.CmeAudType, "ct_condition_marker_errors_aud_type_idx");

            entity.HasIndex(e => e.CmeScmId, "ct_condition_marker_errors_cme_scm_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_condition_marker_errors_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_condition_marker_errors_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_condition_marker_errors_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_condition_marker_errors_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_condition_marker_errors_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_condition_marker_errors_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_condition_marker_errors_record_type_idx");

            entity.HasIndex(e => e.CmeId, "ct_condition_marker_errors_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_condition_marker_errors_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CmeAttributeName)
                .HasMaxLength(30)
                .HasColumnName("cme_attribute_name");
            entity.Property(e => e.CmeAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cme_aud_datetime");
            entity.Property(e => e.CmeAudId).HasColumnName("cme_aud_id");
            entity.Property(e => e.CmeAudType).HasColumnName("cme_aud_type");
            entity.Property(e => e.CmeCurrentModifiedDate).HasColumnName("cme_current_modified_date");
            entity.Property(e => e.CmeCurrentPid)
                .HasPrecision(3)
                .HasColumnName("cme_current_pid");
            entity.Property(e => e.CmeCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("cme_current_status");
            entity.Property(e => e.CmeCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("cme_current_user");
            entity.Property(e => e.CmeErrorCode)
                .HasMaxLength(5)
                .HasColumnName("cme_error_code");
            entity.Property(e => e.CmeId)
                .HasPrecision(12)
                .HasColumnName("cme_id");
            entity.Property(e => e.CmeScmId)
                .HasPrecision(12)
                .HasColumnName("cme_scm_id");
            entity.Property(e => e.CmeVersion)
                .HasPrecision(6)
                .HasColumnName("cme_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtConditionType>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_condition_types_transactions_pkey");

            entity.ToTable("ct_condition_types", "cts_transactions");

            entity.HasIndex(e => e.CotAudDatetime, "ct_condition_types_aud_datetime_idx");

            entity.HasIndex(e => e.CotAudId, "ct_condition_types_aud_id_idx");

            entity.HasIndex(e => new { e.CotAudType, e.CotAudDatetime }, "ct_condition_types_aud_type_datetime_idx");

            entity.HasIndex(e => e.CotAudType, "ct_condition_types_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_condition_types_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_condition_types_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_condition_types_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_condition_types_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_condition_types_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_condition_types_record_type_idx");

            entity.HasIndex(e => e.CotId, "ct_condition_types_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_condition_types_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CotAccessGroup)
                .HasMaxLength(10)
                .HasColumnName("cot_access_group");
            entity.Property(e => e.CotAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cot_aud_datetime");
            entity.Property(e => e.CotAudId).HasColumnName("cot_aud_id");
            entity.Property(e => e.CotAudType).HasColumnName("cot_aud_type");
            entity.Property(e => e.CotCessationReason)
                .HasMaxLength(3)
                .HasColumnName("cot_cessation_reason");
            entity.Property(e => e.CotConditionType)
                .HasMaxLength(5)
                .HasColumnName("cot_condition_type");
            entity.Property(e => e.CotCurrentModifiedDate).HasColumnName("cot_current_modified_date");
            entity.Property(e => e.CotCurrentPid)
                .HasPrecision(3)
                .HasColumnName("cot_current_pid");
            entity.Property(e => e.CotCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("cot_current_status");
            entity.Property(e => e.CotCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("cot_current_user");
            entity.Property(e => e.CotEffectiveFromDate).HasColumnName("cot_effective_from_date");
            entity.Property(e => e.CotEffectiveToDate).HasColumnName("cot_effective_to_date");
            entity.Property(e => e.CotId)
                .HasPrecision(12)
                .HasColumnName("cot_id");
            entity.Property(e => e.CotLongDescription)
                .HasMaxLength(60)
                .HasColumnName("cot_long_description");
            entity.Property(e => e.CotShortDescription)
                .HasMaxLength(20)
                .HasColumnName("cot_short_description");
            entity.Property(e => e.CotVersion)
                .HasPrecision(6)
                .HasColumnName("cot_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtConditionVariant>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_condition_variants_transactions_pkey");

            entity.ToTable("ct_condition_variants", "cts_transactions");

            entity.HasIndex(e => e.CovAudDatetime, "ct_condition_variants_aud_datetime_idx");

            entity.HasIndex(e => e.CovAudId, "ct_condition_variants_aud_id_idx");

            entity.HasIndex(e => new { e.CovAudType, e.CovAudDatetime }, "ct_condition_variants_aud_type_datetime_idx");

            entity.HasIndex(e => e.CovAudType, "ct_condition_variants_aud_type_idx");

            entity.HasIndex(e => e.CovConId, "ct_condition_variants_cov_con_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_condition_variants_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_condition_variants_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_condition_variants_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_condition_variants_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_condition_variants_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_condition_variants_record_type_idx");

            entity.HasIndex(e => e.CovId, "ct_condition_variants_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_condition_variants_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CovAccessRestricted)
                .HasMaxLength(1)
                .HasColumnName("cov_access_restricted");
            entity.Property(e => e.CovAlertMarkerCreation)
                .HasMaxLength(1)
                .HasColumnName("cov_alert_marker_creation");
            entity.Property(e => e.CovAlertMovement)
                .HasMaxLength(1)
                .HasColumnName("cov_alert_movement");
            entity.Property(e => e.CovAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cov_aud_datetime");
            entity.Property(e => e.CovAudId).HasColumnName("cov_aud_id");
            entity.Property(e => e.CovAudType).HasColumnName("cov_aud_type");
            entity.Property(e => e.CovAutoSnaffle)
                .HasMaxLength(1)
                .HasColumnName("cov_auto_snaffle");
            entity.Property(e => e.CovCessationReason)
                .HasMaxLength(2)
                .HasColumnName("cov_cessation_reason");
            entity.Property(e => e.CovConId)
                .HasPrecision(12)
                .HasColumnName("cov_con_id");
            entity.Property(e => e.CovConditionVariant)
                .HasMaxLength(20)
                .HasColumnName("cov_condition_variant");
            entity.Property(e => e.CovCurrentModifiedDate).HasColumnName("cov_current_modified_date");
            entity.Property(e => e.CovCurrentPid)
                .HasPrecision(3)
                .HasColumnName("cov_current_pid");
            entity.Property(e => e.CovCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("cov_current_status");
            entity.Property(e => e.CovCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("cov_current_user");
            entity.Property(e => e.CovDefaultPeriod)
                .HasPrecision(3)
                .HasColumnName("cov_default_period");
            entity.Property(e => e.CovEffectiveFromDate).HasColumnName("cov_effective_from_date");
            entity.Property(e => e.CovEffectiveToDate).HasColumnName("cov_effective_to_date");
            entity.Property(e => e.CovId)
                .HasPrecision(12)
                .HasColumnName("cov_id");
            entity.Property(e => e.CovLetterDataSource)
                .HasMaxLength(1)
                .HasColumnName("cov_letter_data_source");
            entity.Property(e => e.CovLetterMaxAnimals)
                .HasPrecision(3)
                .HasColumnName("cov_letter_max_animals");
            entity.Property(e => e.CovLetterType)
                .HasMaxLength(3)
                .HasColumnName("cov_letter_type");
            entity.Property(e => e.CovLiveIndicator)
                .HasMaxLength(1)
                .HasColumnName("cov_live_indicator");
            entity.Property(e => e.CovLongDescription)
                .HasMaxLength(60)
                .HasColumnName("cov_long_description");
            entity.Property(e => e.CovMovtRestrictType)
                .HasMaxLength(2)
                .HasColumnName("cov_movt_restrict_type");
            entity.Property(e => e.CovMultipleUsage)
                .HasMaxLength(1)
                .HasColumnName("cov_multiple_usage");
            entity.Property(e => e.CovReportMovement)
                .HasMaxLength(1)
                .HasColumnName("cov_report_movement");
            entity.Property(e => e.CovScope)
                .HasMaxLength(1)
                .HasColumnName("cov_scope");
            entity.Property(e => e.CovShortDescription)
                .HasMaxLength(20)
                .HasColumnName("cov_short_description");
            entity.Property(e => e.CovVersion)
                .HasPrecision(6)
                .HasColumnName("cov_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtCountiesMigration>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_counties_migration_transactions_pkey");

            entity.ToTable("ct_counties_migration", "cts_transactions");

            entity.HasIndex(e => e.CtyAudDatetime, "ct_counties_migration_aud_datetime_idx");

            entity.HasIndex(e => e.CtyAudId, "ct_counties_migration_aud_id_idx");

            entity.HasIndex(e => new { e.CtyAudType, e.CtyAudDatetime }, "ct_counties_migration_aud_type_datetime_idx");

            entity.HasIndex(e => e.CtyAudType, "ct_counties_migration_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_counties_migration_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_counties_migration_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_counties_migration_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_counties_migration_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_counties_migration_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_counties_migration_record_type_idx");

            entity.HasIndex(e => e.CtyId, "ct_counties_migration_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_counties_migration_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.CtyAdminOffice)
                .HasMaxLength(2)
                .HasColumnName("cty_admin_office");
            entity.Property(e => e.CtyAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cty_aud_datetime");
            entity.Property(e => e.CtyAudId).HasColumnName("cty_aud_id");
            entity.Property(e => e.CtyAudType).HasColumnName("cty_aud_type");
            entity.Property(e => e.CtyBcmsTeam)
                .HasMaxLength(10)
                .HasColumnName("cty_bcms_team");
            entity.Property(e => e.CtyCode)
                .HasMaxLength(2)
                .HasColumnName("cty_code");
            entity.Property(e => e.CtyCurrentModifiedDate).HasColumnName("cty_current_modified_date");
            entity.Property(e => e.CtyCurrentPid)
                .HasPrecision(3)
                .HasColumnName("cty_current_pid");
            entity.Property(e => e.CtyCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("cty_current_status");
            entity.Property(e => e.CtyCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("cty_current_user");
            entity.Property(e => e.CtyDataMgtArea)
                .HasMaxLength(3)
                .HasColumnName("cty_data_mgt_area");
            entity.Property(e => e.CtyDateMigrated).HasColumnName("cty_date_migrated");
            entity.Property(e => e.CtyDueForMigration)
                .HasMaxLength(1)
                .HasColumnName("cty_due_for_migration");
            entity.Property(e => e.CtyId)
                .HasPrecision(12)
                .HasColumnName("cty_id");
            entity.Property(e => e.CtyInspectionArea)
                .HasMaxLength(2)
                .HasColumnName("cty_inspection_area");
            entity.Property(e => e.CtyName)
                .HasMaxLength(25)
                .HasColumnName("cty_name");
            entity.Property(e => e.CtyPassportArea)
                .HasMaxLength(3)
                .HasColumnName("cty_passport_area");
            entity.Property(e => e.CtyPrintPassports)
                .HasMaxLength(1)
                .HasColumnName("cty_print_passports");
            entity.Property(e => e.CtyUkArea)
                .HasMaxLength(1)
                .HasColumnName("cty_uk_area");
            entity.Property(e => e.CtyVersion)
                .HasPrecision(6)
                .HasColumnName("cty_version");
            entity.Property(e => e.CtyVetArea)
                .HasMaxLength(3)
                .HasColumnName("cty_vet_area");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtCountry>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_countries_transactions_pkey");

            entity.ToTable("ct_countries", "cts_transactions");

            entity.HasIndex(e => e.CryAudDatetime, "ct_countries_aud_datetime_idx");

            entity.HasIndex(e => e.CryAudId, "ct_countries_aud_id_idx");

            entity.HasIndex(e => new { e.CryAudType, e.CryAudDatetime }, "ct_countries_aud_type_datetime_idx");

            entity.HasIndex(e => e.CryAudType, "ct_countries_aud_type_idx");

            entity.HasIndex(e => e.CryCryIdMainEu, "ct_countries_cry_cry_id_main_eu_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_countries_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_countries_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_countries_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_countries_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_countries_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_countries_record_type_idx");

            entity.HasIndex(e => e.CryId, "ct_countries_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_countries_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CryAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cry_aud_datetime");
            entity.Property(e => e.CryAudId).HasColumnName("cry_aud_id");
            entity.Property(e => e.CryAudType).HasColumnName("cry_aud_type");
            entity.Property(e => e.CryBackCapture)
                .HasMaxLength(1)
                .HasColumnName("cry_back_capture");
            entity.Property(e => e.CryCode)
                .HasMaxLength(2)
                .HasColumnName("cry_code");
            entity.Property(e => e.CryCryIdMainEu)
                .HasPrecision(12)
                .HasColumnName("cry_cry_id_main_eu");
            entity.Property(e => e.CryCurrentModifiedDate).HasColumnName("cry_current_modified_date");
            entity.Property(e => e.CryCurrentPid)
                .HasPrecision(3)
                .HasColumnName("cry_current_pid");
            entity.Property(e => e.CryCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("cry_current_status");
            entity.Property(e => e.CryCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("cry_current_user");
            entity.Property(e => e.CryEuMember)
                .HasMaxLength(1)
                .HasColumnName("cry_eu_member");
            entity.Property(e => e.CryId)
                .HasPrecision(12)
                .HasColumnName("cry_id");
            entity.Property(e => e.CryImportExport)
                .HasMaxLength(1)
                .HasColumnName("cry_import_export");
            entity.Property(e => e.CryName)
                .HasMaxLength(25)
                .HasColumnName("cry_name");
            entity.Property(e => e.CryVersion)
                .HasPrecision(6)
                .HasColumnName("cry_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtCounty>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_counties_transactions_pkey");

            entity.ToTable("ct_counties", "cts_transactions");

            entity.HasIndex(e => e.CtyAudDatetime, "ct_counties_aud_datetime_idx");

            entity.HasIndex(e => e.CtyAudId, "ct_counties_aud_id_idx");

            entity.HasIndex(e => new { e.CtyAudType, e.CtyAudDatetime }, "ct_counties_aud_type_datetime_idx");

            entity.HasIndex(e => e.CtyAudType, "ct_counties_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_counties_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_counties_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_counties_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_counties_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_counties_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_counties_record_type_idx");

            entity.HasIndex(e => e.CtyId, "ct_counties_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_counties_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.CtyAdminOffice)
                .HasMaxLength(2)
                .HasColumnName("cty_admin_office");
            entity.Property(e => e.CtyAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cty_aud_datetime");
            entity.Property(e => e.CtyAudId).HasColumnName("cty_aud_id");
            entity.Property(e => e.CtyAudType).HasColumnName("cty_aud_type");
            entity.Property(e => e.CtyBcmsTeam)
                .HasMaxLength(10)
                .HasColumnName("cty_bcms_team");
            entity.Property(e => e.CtyCode)
                .HasMaxLength(2)
                .HasColumnName("cty_code");
            entity.Property(e => e.CtyCurrentModifiedDate).HasColumnName("cty_current_modified_date");
            entity.Property(e => e.CtyCurrentPid)
                .HasPrecision(3)
                .HasColumnName("cty_current_pid");
            entity.Property(e => e.CtyCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("cty_current_status");
            entity.Property(e => e.CtyCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("cty_current_user");
            entity.Property(e => e.CtyDataMgtArea)
                .HasMaxLength(3)
                .HasColumnName("cty_data_mgt_area");
            entity.Property(e => e.CtyId)
                .HasPrecision(12)
                .HasColumnName("cty_id");
            entity.Property(e => e.CtyInspectionArea)
                .HasMaxLength(2)
                .HasColumnName("cty_inspection_area");
            entity.Property(e => e.CtyName)
                .HasMaxLength(25)
                .HasColumnName("cty_name");
            entity.Property(e => e.CtyPassportArea)
                .HasMaxLength(3)
                .HasColumnName("cty_passport_area");
            entity.Property(e => e.CtyUkArea)
                .HasMaxLength(1)
                .HasColumnName("cty_uk_area");
            entity.Property(e => e.CtyVersion)
                .HasPrecision(6)
                .HasColumnName("cty_version");
            entity.Property(e => e.CtyVetArea)
                .HasMaxLength(3)
                .HasColumnName("cty_vet_area");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtCps167Report>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_cps167_report_pkey");

            entity.ToTable("ct_cps167_report", "cts_transactions");

            entity.HasIndex(e => e.KnsAudDatetime, "ct_cps167_report_aud_datetime_idx");

            entity.HasIndex(e => e.KnsAudId, "ct_cps167_report_aud_id_idx");

            entity.HasIndex(e => new { e.KnsAudType, e.KnsAudDatetime }, "ct_cps167_report_aud_type_datetime_idx");

            entity.HasIndex(e => e.KnsAudType, "ct_cps167_report_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_cps167_report_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cps167_report_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_cps167_report_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cps167_report_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_cps167_report_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_cps167_report_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_cps167_report_record_type_idx");

            entity.HasIndex(e => e.KnsId, "ct_cps167_report_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_cps167_report_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.KnsActionType)
                .HasMaxLength(5)
                .HasColumnName("kns_action_type");
            entity.Property(e => e.KnsAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("kns_aud_datetime");
            entity.Property(e => e.KnsAudId).HasColumnName("kns_aud_id");
            entity.Property(e => e.KnsAudType).HasColumnName("kns_aud_type");
            entity.Property(e => e.KnsDestinationDirectory)
                .HasMaxLength(200)
                .HasColumnName("kns_destination_directory");
            entity.Property(e => e.KnsFilename)
                .HasMaxLength(25)
                .HasColumnName("kns_filename");
            entity.Property(e => e.KnsId)
                .HasPrecision(12)
                .HasColumnName("kns_id");
            entity.Property(e => e.KnsMessage)
                .HasMaxLength(200)
                .HasColumnName("kns_message");
            entity.Property(e => e.KnsRunDateTime).HasColumnName("kns_run_date_time");
            entity.Property(e => e.KnsSourceDirectory)
                .HasMaxLength(200)
                .HasColumnName("kns_source_directory");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtCts164HandshakeFileKey>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_cts164_handshake_file_keys_transactions_pkey");

            entity.ToTable("ct_cts164_handshake_file_keys", "cts_transactions");

            entity.HasIndex(e => e.BjkAudDatetime, "ct_cts164_handshake_file_keys_aud_datetime_idx");

            entity.HasIndex(e => e.BjkAudId, "ct_cts164_handshake_file_keys_aud_id_idx");

            entity.HasIndex(e => new { e.BjkAudType, e.BjkAudDatetime }, "ct_cts164_handshake_file_keys_aud_type_datetime_idx");

            entity.HasIndex(e => e.BjkAudType, "ct_cts164_handshake_file_keys_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_cts164_handshake_file_keys_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_cts164_handshake_file_keys_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_cts164_handshake_file_keys_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_cts164_handshake_file_keys_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_cts164_handshake_file_keys_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_cts164_handshake_file_keys_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.BjkAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("bjk_aud_datetime");
            entity.Property(e => e.BjkAudId).HasColumnName("bjk_aud_id");
            entity.Property(e => e.BjkAudType).HasColumnName("bjk_aud_type");
            entity.Property(e => e.BjkBatchId)
                .HasPrecision(12)
                .HasColumnName("bjk_batch_id");
            entity.Property(e => e.BjkFiletype)
                .HasMaxLength(30)
                .HasColumnName("bjk_filetype");
            entity.Property(e => e.BjkGroupId)
                .HasPrecision(12)
                .HasColumnName("bjk_group_id");
            entity.Property(e => e.BjkKey)
                .HasMaxLength(30)
                .HasColumnName("bjk_key");
            entity.Property(e => e.BjkModifiedDate).HasColumnName("bjk_modified_date");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtCtsUser>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_cts_users_pkey");

            entity.ToTable("ct_cts_users", "cts_transactions");

            entity.HasIndex(e => e.CusAudDatetime, "ct_cts_users_aud_datetime_idx");

            entity.HasIndex(e => e.CusAudId, "ct_cts_users_aud_id_idx");

            entity.HasIndex(e => new { e.CusAudType, e.CusAudDatetime }, "ct_cts_users_aud_type_datetime_idx");

            entity.HasIndex(e => e.CusAudType, "ct_cts_users_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_cts_users_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cts_users_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_cts_users_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_cts_users_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_cts_users_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_cts_users_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_cts_users_record_type_idx");

            entity.HasIndex(e => e.CusId, "ct_cts_users_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_cts_users_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.CusAccessGroup)
                .HasMaxLength(3)
                .HasColumnName("cus_access_group");
            entity.Property(e => e.CusAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cus_aud_datetime");
            entity.Property(e => e.CusAudId).HasColumnName("cus_aud_id");
            entity.Property(e => e.CusAudType).HasColumnName("cus_aud_type");
            entity.Property(e => e.CusColonFlag)
                .HasMaxLength(1)
                .HasColumnName("cus_colon_flag");
            entity.Property(e => e.CusEmailAddress)
                .HasMaxLength(200)
                .HasColumnName("cus_email_address");
            entity.Property(e => e.CusGrade)
                .HasMaxLength(3)
                .HasColumnName("cus_grade");
            entity.Property(e => e.CusId)
                .HasPrecision(12)
                .HasColumnName("cus_id");
            entity.Property(e => e.CusRoomName)
                .HasMaxLength(20)
                .HasColumnName("cus_room_name");
            entity.Property(e => e.CusTeamReference)
                .HasMaxLength(4)
                .HasColumnName("cus_team_reference");
            entity.Property(e => e.CusUserIdentifier)
                .HasMaxLength(10)
                .HasColumnName("cus_user_identifier");
            entity.Property(e => e.CusVersion)
                .HasPrecision(6)
                .HasColumnName("cus_version");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtEartag>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_eartags_pkey");

            entity.ToTable("ct_eartags", "cts_transactions");

            entity.HasIndex(e => e.EtgAudDatetime, "ct_eartags_aud_datetime_idx");

            entity.HasIndex(e => e.EtgAudId, "ct_eartags_aud_id_idx");

            entity.HasIndex(e => new { e.EtgAudType, e.EtgAudDatetime }, "ct_eartags_aud_type_datetime_idx");

            entity.HasIndex(e => e.EtgAudType, "ct_eartags_aud_type_idx");

            entity.HasIndex(e => e.EtgErfId, "ct_eartags_etg_erf_id_idx");

            entity.HasIndex(e => e.EtgEttId, "ct_eartags_etg_ett_id_idx");

            entity.HasIndex(e => e.EtgLocIdOrder, "ct_eartags_etg_loc_id_order_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_eartags_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_eartags_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_eartags_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_eartags_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_eartags_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_eartags_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_eartags_record_type_idx");

            entity.HasIndex(e => e.EtgId, "ct_eartags_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_eartags_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.EtgAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("etg_aud_datetime");
            entity.Property(e => e.EtgAudId).HasColumnName("etg_aud_id");
            entity.Property(e => e.EtgAudType).HasColumnName("etg_aud_type");
            entity.Property(e => e.EtgCurrentModifiedDate).HasColumnName("etg_current_modified_date");
            entity.Property(e => e.EtgCurrentPid)
                .HasPrecision(3)
                .HasColumnName("etg_current_pid");
            entity.Property(e => e.EtgCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("etg_current_status");
            entity.Property(e => e.EtgCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("etg_current_user");
            entity.Property(e => e.EtgEartag)
                .HasMaxLength(20)
                .HasColumnName("etg_eartag");
            entity.Property(e => e.EtgEartagAuthority)
                .HasMaxLength(2)
                .HasColumnName("etg_eartag_authority");
            entity.Property(e => e.EtgEartagDefraFormat)
                .HasMaxLength(20)
                .HasColumnName("etg_eartag_defra_format");
            entity.Property(e => e.EtgErfId)
                .HasPrecision(12)
                .HasColumnName("etg_erf_id");
            entity.Property(e => e.EtgEttId)
                .HasPrecision(12)
                .HasColumnName("etg_ett_id");
            entity.Property(e => e.EtgFuzzyEartag1)
                .HasMaxLength(20)
                .HasColumnName("etg_fuzzy_eartag_1");
            entity.Property(e => e.EtgFuzzyEartag2)
                .HasMaxLength(20)
                .HasColumnName("etg_fuzzy_eartag_2");
            entity.Property(e => e.EtgId)
                .HasPrecision(12)
                .HasColumnName("etg_id");
            entity.Property(e => e.EtgIdentifierAvailability)
                .HasMaxLength(2)
                .HasColumnName("etg_identifier_availability");
            entity.Property(e => e.EtgLocIdOrder)
                .HasPrecision(12)
                .HasColumnName("etg_loc_id_order");
            entity.Property(e => e.EtgOrderLocationRepd)
                .HasMaxLength(17)
                .HasColumnName("etg_order_location_repd");
            entity.Property(e => e.EtgPpafIndicator)
                .HasMaxLength(1)
                .HasColumnName("etg_ppaf_indicator");
            entity.Property(e => e.EtgSource)
                .HasMaxLength(2)
                .HasColumnName("etg_source");
            entity.Property(e => e.EtgSpecies)
                .HasMaxLength(240)
                .HasColumnName("etg_species");
            entity.Property(e => e.EtgTypeDefraFormat)
                .HasMaxLength(10)
                .HasColumnName("etg_type_defra_format");
            entity.Property(e => e.EtgUsageCode)
                .HasMaxLength(2)
                .HasColumnName("etg_usage_code");
            entity.Property(e => e.EtgVersion)
                .HasPrecision(6)
                .HasColumnName("etg_version");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtEartagFormat>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_eartag_formats_transactions_pkey");

            entity.ToTable("ct_eartag_formats", "cts_transactions");

            entity.HasIndex(e => e.EtfAudDatetime, "ct_eartag_formats_aud_datetime_idx");

            entity.HasIndex(e => e.EtfAudId, "ct_eartag_formats_aud_id_idx");

            entity.HasIndex(e => new { e.EtfAudType, e.EtfAudDatetime }, "ct_eartag_formats_aud_type_datetime_idx");

            entity.HasIndex(e => e.EtfAudType, "ct_eartag_formats_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_eartag_formats_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_eartag_formats_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_eartag_formats_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_eartag_formats_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_eartag_formats_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_eartag_formats_record_type_idx");

            entity.HasIndex(e => e.EtfId, "ct_eartag_formats_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_eartag_formats_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.EtfAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("etf_aud_datetime");
            entity.Property(e => e.EtfAudId).HasColumnName("etf_aud_id");
            entity.Property(e => e.EtfAudType).HasColumnName("etf_aud_type");
            entity.Property(e => e.EtfCurrentModifiedDate).HasColumnName("etf_current_modified_date");
            entity.Property(e => e.EtfCurrentPid)
                .HasPrecision(3)
                .HasColumnName("etf_current_pid");
            entity.Property(e => e.EtfCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("etf_current_status");
            entity.Property(e => e.EtfCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("etf_current_user");
            entity.Property(e => e.EtfDescription)
                .HasMaxLength(60)
                .HasColumnName("etf_description");
            entity.Property(e => e.EtfExtraCharsAllowed)
                .HasMaxLength(30)
                .HasColumnName("etf_extra_chars_allowed");
            entity.Property(e => e.EtfFormatPattern)
                .HasMaxLength(30)
                .HasColumnName("etf_format_pattern");
            entity.Property(e => e.EtfId)
                .HasPrecision(12)
                .HasColumnName("etf_id");
            entity.Property(e => e.EtfMaxInputLength)
                .HasPrecision(3)
                .HasColumnName("etf_max_input_length");
            entity.Property(e => e.EtfVersion)
                .HasPrecision(6)
                .HasColumnName("etf_version");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtEartagReason>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_eartag_reasons_transactions_pkey");

            entity.ToTable("ct_eartag_reasons", "cts_transactions");

            entity.HasIndex(e => e.EtrAudDatetime, "ct_eartag_reasons_aud_datetime_idx");

            entity.HasIndex(e => e.EtrAudId, "ct_eartag_reasons_aud_id_idx");

            entity.HasIndex(e => new { e.EtrAudType, e.EtrAudDatetime }, "ct_eartag_reasons_aud_type_datetime_idx");

            entity.HasIndex(e => e.EtrAudType, "ct_eartag_reasons_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_eartag_reasons_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_eartag_reasons_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_eartag_reasons_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_eartag_reasons_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_eartag_reasons_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_eartag_reasons_record_type_idx");

            entity.HasIndex(e => e.EtrId, "ct_eartag_reasons_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_eartag_reasons_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.EtrAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("etr_aud_datetime");
            entity.Property(e => e.EtrAudId).HasColumnName("etr_aud_id");
            entity.Property(e => e.EtrAudType).HasColumnName("etr_aud_type");
            entity.Property(e => e.EtrCurrentModifiedDate).HasColumnName("etr_current_modified_date");
            entity.Property(e => e.EtrCurrentPid)
                .HasPrecision(3)
                .HasColumnName("etr_current_pid");
            entity.Property(e => e.EtrCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("etr_current_status");
            entity.Property(e => e.EtrCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("etr_current_user");
            entity.Property(e => e.EtrEartagReasonCode)
                .HasMaxLength(2)
                .HasColumnName("etr_eartag_reason_code");
            entity.Property(e => e.EtrId)
                .HasPrecision(12)
                .HasColumnName("etr_id");
            entity.Property(e => e.EtrLongDescription)
                .HasMaxLength(60)
                .HasColumnName("etr_long_description");
            entity.Property(e => e.EtrReasonCodeType)
                .HasMaxLength(1)
                .HasColumnName("etr_reason_code_type");
            entity.Property(e => e.EtrShortDescription)
                .HasMaxLength(20)
                .HasColumnName("etr_short_description");
            entity.Property(e => e.EtrVersion)
                .HasPrecision(6)
                .HasColumnName("etr_version");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtEartagReasonFlag>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_eartag_reason_flags_transactions_pkey");

            entity.ToTable("ct_eartag_reason_flags", "cts_transactions");

            entity.HasIndex(e => e.ErfAudDatetime, "ct_eartag_reason_flags_aud_datetime_idx");

            entity.HasIndex(e => e.ErfAudId, "ct_eartag_reason_flags_aud_id_idx");

            entity.HasIndex(e => new { e.ErfAudType, e.ErfAudDatetime }, "ct_eartag_reason_flags_aud_type_datetime_idx");

            entity.HasIndex(e => e.ErfAudType, "ct_eartag_reason_flags_aud_type_idx");

            entity.HasIndex(e => e.ErfEtrId, "ct_eartag_reason_flags_erf_etr_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_eartag_reason_flags_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_eartag_reason_flags_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_eartag_reason_flags_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_eartag_reason_flags_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_eartag_reason_flags_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_eartag_reason_flags_record_type_idx");

            entity.HasIndex(e => e.ErfId, "ct_eartag_reason_flags_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_eartag_reason_flags_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ErfAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("erf_aud_datetime");
            entity.Property(e => e.ErfAudId).HasColumnName("erf_aud_id");
            entity.Property(e => e.ErfAudType).HasColumnName("erf_aud_type");
            entity.Property(e => e.ErfBackcaptureRegnFlag)
                .HasPrecision(1)
                .HasColumnName("erf_backcapture_regn_flag");
            entity.Property(e => e.ErfBatchUpdateAmendFlag)
                .HasPrecision(1)
                .HasColumnName("erf_batch_update_amend_flag");
            entity.Property(e => e.ErfCtsAnimalRegFlag)
                .HasPrecision(1)
                .HasColumnName("erf_cts_animal_reg_flag");
            entity.Property(e => e.ErfCtsGenSurrSireAllowed)
                .HasPrecision(1)
                .HasColumnName("erf_cts_gen_surr_sire_allowed");
            entity.Property(e => e.ErfCurrentModifiedDate).HasColumnName("erf_current_modified_date");
            entity.Property(e => e.ErfCurrentPid)
                .HasPrecision(3)
                .HasColumnName("erf_current_pid");
            entity.Property(e => e.ErfCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("erf_current_status");
            entity.Property(e => e.ErfCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("erf_current_user");
            entity.Property(e => e.ErfEartagAuthority)
                .HasMaxLength(2)
                .HasColumnName("erf_eartag_authority");
            entity.Property(e => e.ErfEtrId)
                .HasPrecision(12)
                .HasColumnName("erf_etr_id");
            entity.Property(e => e.ErfId)
                .HasPrecision(12)
                .HasColumnName("erf_id");
            entity.Property(e => e.ErfManualDeletionInd)
                .HasPrecision(1)
                .HasColumnName("erf_manual_deletion_ind");
            entity.Property(e => e.ErfManualEntryDefaultInd)
                .HasPrecision(1)
                .HasColumnName("erf_manual_entry_default_ind");
            entity.Property(e => e.ErfManualEntryInd)
                .HasPrecision(1)
                .HasColumnName("erf_manual_entry_ind");
            entity.Property(e => e.ErfManualOverride)
                .HasPrecision(1)
                .HasColumnName("erf_manual_override");
            entity.Property(e => e.ErfManualUpdateFlag)
                .HasPrecision(1)
                .HasColumnName("erf_manual_update_flag");
            entity.Property(e => e.ErfVersion)
                .HasPrecision(6)
                .HasColumnName("erf_version");
            entity.Property(e => e.FakeData)
                .HasPrecision(1)
                .HasColumnName("fake_data");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtEartagStaging>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_eartag_staging_pkey");

            entity.ToTable("ct_eartag_staging", "cts_transactions");

            entity.HasIndex(e => e.EstAudDatetime, "ct_eartag_staging_aud_datetime_idx");

            entity.HasIndex(e => e.EstAudId, "ct_eartag_staging_aud_id_idx");

            entity.HasIndex(e => new { e.EstAudType, e.EstAudDatetime }, "ct_eartag_staging_aud_type_datetime_idx");

            entity.HasIndex(e => e.EstAudType, "ct_eartag_staging_aud_type_idx");

            entity.HasIndex(e => e.EstErfId, "ct_eartag_staging_est_erf_id_idx");

            entity.HasIndex(e => e.EstLocIdOrder, "ct_eartag_staging_est_loc_id_order_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_eartag_staging_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_eartag_staging_file_trans_type_idx");

            entity.HasIndex(e => e.CtsFileImportId, "ct_eartag_staging_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_eartag_staging_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_eartag_staging_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_eartag_staging_record_type_idx");

            entity.HasIndex(e => e.EstId, "ct_eartag_staging_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_eartag_staging_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.EstAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("est_aud_datetime");
            entity.Property(e => e.EstAudId).HasColumnName("est_aud_id");
            entity.Property(e => e.EstAudType).HasColumnName("est_aud_type");
            entity.Property(e => e.EstCurrentModifiedDate).HasColumnName("est_current_modified_date");
            entity.Property(e => e.EstEartag)
                .HasMaxLength(20)
                .HasColumnName("est_eartag");
            entity.Property(e => e.EstEartagReasonCode)
                .HasMaxLength(2)
                .HasColumnName("est_eartag_reason_code");
            entity.Property(e => e.EstErfId)
                .HasPrecision(12)
                .HasColumnName("est_erf_id");
            entity.Property(e => e.EstId)
                .HasPrecision(12)
                .HasColumnName("est_id");
            entity.Property(e => e.EstIdentifierAvailability)
                .HasMaxLength(2)
                .HasColumnName("est_identifier_availability");
            entity.Property(e => e.EstLocIdOrder)
                .HasPrecision(12)
                .HasColumnName("est_loc_id_order");
            entity.Property(e => e.EstOrderLocationRepd)
                .HasMaxLength(17)
                .HasColumnName("est_order_location_repd");
            entity.Property(e => e.EstUsageCode)
                .HasMaxLength(2)
                .HasColumnName("est_usage_code");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtEartagType>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_eartag_types_transactions_pkey");

            entity.ToTable("ct_eartag_types", "cts_transactions");

            entity.HasIndex(e => e.EttAudDatetime, "ct_eartag_types_aud_datetime_idx");

            entity.HasIndex(e => e.EttAudId, "ct_eartag_types_aud_id_idx");

            entity.HasIndex(e => new { e.EttAudType, e.EttAudDatetime }, "ct_eartag_types_aud_type_datetime_idx");

            entity.HasIndex(e => e.EttAudType, "ct_eartag_types_aud_type_idx");

            entity.HasIndex(e => e.EttEtfId, "ct_eartag_types_ett_etf_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_eartag_types_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_eartag_types_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_eartag_types_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_eartag_types_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_eartag_types_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_eartag_types_record_type_idx");

            entity.HasIndex(e => e.EttId, "ct_eartag_types_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_eartag_types_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.EttAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("ett_aud_datetime");
            entity.Property(e => e.EttAudId).HasColumnName("ett_aud_id");
            entity.Property(e => e.EttAudType).HasColumnName("ett_aud_type");
            entity.Property(e => e.EttCrExport)
                .HasMaxLength(1)
                .HasColumnName("ett_cr_export");
            entity.Property(e => e.EttCurrentModifiedDate).HasColumnName("ett_current_modified_date");
            entity.Property(e => e.EttCurrentPid)
                .HasPrecision(3)
                .HasColumnName("ett_current_pid");
            entity.Property(e => e.EttCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("ett_current_status");
            entity.Property(e => e.EttCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("ett_current_user");
            entity.Property(e => e.EttDescription)
                .HasMaxLength(60)
                .HasColumnName("ett_description");
            entity.Property(e => e.EttEartagType)
                .HasMaxLength(2)
                .HasColumnName("ett_eartag_type");
            entity.Property(e => e.EttEtfId)
                .HasPrecision(12)
                .HasColumnName("ett_etf_id");
            entity.Property(e => e.EttId)
                .HasPrecision(12)
                .HasColumnName("ett_id");
            entity.Property(e => e.EttShortDescription)
                .HasMaxLength(20)
                .HasColumnName("ett_short_description");
            entity.Property(e => e.EttVersion)
                .HasPrecision(6)
                .HasColumnName("ett_version");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtElectronicIdentifier>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_electronic_identifiers_pkey");

            entity.ToTable("ct_electronic_identifiers", "cts_transactions");

            entity.HasIndex(e => e.EidAudDatetime, "ct_electronic_identifiers_aud_datetime_idx");

            entity.HasIndex(e => e.EidAudId, "ct_electronic_identifiers_aud_id_idx");

            entity.HasIndex(e => new { e.EidAudType, e.EidAudDatetime }, "ct_electronic_identifiers_aud_type_datetime_idx");

            entity.HasIndex(e => e.EidAudType, "ct_electronic_identifiers_aud_type_idx");

            entity.HasIndex(e => e.EidIsaId, "ct_electronic_identifiers_eid_isa_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_electronic_identifiers_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_electronic_identifiers_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_electronic_identifiers_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_electronic_identifiers_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_electronic_identifiers_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_electronic_identifiers_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_electronic_identifiers_record_type_idx");

            entity.HasIndex(e => e.EidId, "ct_electronic_identifiers_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_electronic_identifiers_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.EidAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("eid_aud_datetime");
            entity.Property(e => e.EidAudId).HasColumnName("eid_aud_id");
            entity.Property(e => e.EidAudType).HasColumnName("eid_aud_type");
            entity.Property(e => e.EidCurrentModifiedDate).HasColumnName("eid_current_modified_date");
            entity.Property(e => e.EidCurrentPid)
                .HasPrecision(3)
                .HasColumnName("eid_current_pid");
            entity.Property(e => e.EidCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("eid_current_status");
            entity.Property(e => e.EidCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("eid_current_user");
            entity.Property(e => e.EidElectronicIdentifier)
                .HasPrecision(16)
                .HasColumnName("eid_electronic_identifier");
            entity.Property(e => e.EidId)
                .HasPrecision(12)
                .HasColumnName("eid_id");
            entity.Property(e => e.EidIsaId)
                .HasPrecision(12)
                .HasColumnName("eid_isa_id");
            entity.Property(e => e.EidUniqueNumber)
                .HasMaxLength(12)
                .HasColumnName("eid_unique_number");
            entity.Property(e => e.EidVersion)
                .HasPrecision(6)
                .HasColumnName("eid_version");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtEmailLog>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_email_log_pkey");

            entity.ToTable("ct_email_log", "cts_transactions");

            entity.HasIndex(e => e.EmlAudDatetime, "ct_email_log_aud_datetime_idx");

            entity.HasIndex(e => e.EmlAudId, "ct_email_log_aud_id_idx");

            entity.HasIndex(e => new { e.EmlAudType, e.EmlAudDatetime }, "ct_email_log_aud_type_datetime_idx");

            entity.HasIndex(e => e.EmlAudType, "ct_email_log_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_email_log_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_email_log_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_email_log_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_email_log_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_email_log_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_email_log_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_email_log_record_type_idx");

            entity.HasIndex(e => e.EmlId, "ct_email_log_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_email_log_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.EmlAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("eml_aud_datetime");
            entity.Property(e => e.EmlAudId).HasColumnName("eml_aud_id");
            entity.Property(e => e.EmlAudType).HasColumnName("eml_aud_type");
            entity.Property(e => e.EmlCurrentModifiedDate).HasColumnName("eml_current_modified_date");
            entity.Property(e => e.EmlCurrentPid)
                .HasPrecision(3)
                .HasColumnName("eml_current_pid");
            entity.Property(e => e.EmlCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("eml_current_status");
            entity.Property(e => e.EmlCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("eml_current_user");
            entity.Property(e => e.EmlEmailAddrRecd)
                .HasMaxLength(70)
                .HasColumnName("eml_email_addr_recd");
            entity.Property(e => e.EmlEmailAddrSent)
                .HasMaxLength(70)
                .HasColumnName("eml_email_addr_sent");
            entity.Property(e => e.EmlFileName)
                .HasMaxLength(50)
                .HasColumnName("eml_file_name");
            entity.Property(e => e.EmlId)
                .HasPrecision(12)
                .HasColumnName("eml_id");
            entity.Property(e => e.EmlReceivedDatetime).HasColumnName("eml_received_datetime");
            entity.Property(e => e.EmlSendReturnCode)
                .HasMaxLength(100)
                .HasColumnName("eml_send_return_code");
            entity.Property(e => e.EmlSentDatetime).HasColumnName("eml_sent_datetime");
            entity.Property(e => e.EmlVersion)
                .HasPrecision(6)
                .HasColumnName("eml_version");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtEreportFile>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_ereport_files_pkey");

            entity.ToTable("ct_ereport_files", "cts_transactions");

            entity.HasIndex(e => e.EreAudDatetime, "ct_ereport_files_aud_datetime_idx");

            entity.HasIndex(e => e.EreAudId, "ct_ereport_files_aud_id_idx");

            entity.HasIndex(e => new { e.EreAudType, e.EreAudDatetime }, "ct_ereport_files_aud_type_datetime_idx");

            entity.HasIndex(e => e.EreAudType, "ct_ereport_files_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_ereport_files_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_ereport_files_file_trans_type_idx");

            entity.HasIndex(e => e.CtsFileImportId, "ct_ereport_files_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_ereport_files_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_ereport_files_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_ereport_files_record_type_idx");

            entity.HasIndex(e => e.EreId, "ct_ereport_files_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_ereport_files_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.EreAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("ere_aud_datetime");
            entity.Property(e => e.EreAudId).HasColumnName("ere_aud_id");
            entity.Property(e => e.EreAudType).HasColumnName("ere_aud_type");
            entity.Property(e => e.EreFileName)
                .HasMaxLength(2000)
                .HasColumnName("ere_file_name");
            entity.Property(e => e.EreFileType)
                .HasMaxLength(100)
                .HasColumnName("ere_file_type");
            entity.Property(e => e.EreId)
                .HasPrecision(12)
                .HasColumnName("ere_id");
            entity.Property(e => e.EreLineNumber)
                .HasPrecision(12)
                .HasColumnName("ere_line_number");
            entity.Property(e => e.EreRecord)
                .HasMaxLength(2000)
                .HasColumnName("ere_record");
            entity.Property(e => e.EreTimestamp).HasColumnName("ere_timestamp");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtEreportLoadMessage>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_ereport_load_messages_pkey");

            entity.ToTable("ct_ereport_load_messages", "cts_transactions");

            entity.HasIndex(e => e.ErmAudDatetime, "ct_ereport_load_messages_aud_datetime_idx");

            entity.HasIndex(e => e.ErmAudId, "ct_ereport_load_messages_aud_id_idx");

            entity.HasIndex(e => new { e.ErmAudType, e.ErmAudDatetime }, "ct_ereport_load_messages_aud_type_datetime_idx");

            entity.HasIndex(e => e.ErmAudType, "ct_ereport_load_messages_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_ereport_load_messages_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ereport_load_messages_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_ereport_load_messages_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ereport_load_messages_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_ereport_load_messages_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_ereport_load_messages_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_ereport_load_messages_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_ereport_load_messages_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ErmAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("erm_aud_datetime");
            entity.Property(e => e.ErmAudId).HasColumnName("erm_aud_id");
            entity.Property(e => e.ErmAudType).HasColumnName("erm_aud_type");
            entity.Property(e => e.ErmDirectoryKey)
                .HasMaxLength(100)
                .HasColumnName("erm_directory_key");
            entity.Property(e => e.ErmFilePrefix)
                .HasMaxLength(100)
                .HasColumnName("erm_file_prefix");
            entity.Property(e => e.ErmFileSuffix)
                .HasMaxLength(100)
                .HasColumnName("erm_file_suffix");
            entity.Property(e => e.ErmFileType)
                .HasMaxLength(100)
                .HasColumnName("erm_file_type");
            entity.Property(e => e.ErmSleepPeriod)
                .HasPrecision(10)
                .HasColumnName("erm_sleep_period");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtEreportLock>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_ereport_locks_pkey");

            entity.ToTable("ct_ereport_locks", "cts_transactions");

            entity.HasIndex(e => e.ErlAudDatetime, "ct_ereport_locks_aud_datetime_idx");

            entity.HasIndex(e => e.ErlAudId, "ct_ereport_locks_aud_id_idx");

            entity.HasIndex(e => new { e.ErlAudType, e.ErlAudDatetime }, "ct_ereport_locks_aud_type_datetime_idx");

            entity.HasIndex(e => e.ErlAudType, "ct_ereport_locks_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_ereport_locks_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_ereport_locks_file_trans_type_idx");

            entity.HasIndex(e => e.CtsFileImportId, "ct_ereport_locks_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_ereport_locks_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_ereport_locks_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_ereport_locks_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_ereport_locks_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ErlAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("erl_aud_datetime");
            entity.Property(e => e.ErlAudId).HasColumnName("erl_aud_id");
            entity.Property(e => e.ErlAudType).HasColumnName("erl_aud_type");
            entity.Property(e => e.ErlFileName)
                .HasMaxLength(2000)
                .HasColumnName("erl_file_name");
            entity.Property(e => e.ErlFileType)
                .HasMaxLength(100)
                .HasColumnName("erl_file_type");
            entity.Property(e => e.ErlProcessed)
                .HasMaxLength(1)
                .HasColumnName("erl_processed");
            entity.Property(e => e.ErlTimestamp).HasColumnName("erl_timestamp");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtEreportProcessMessage>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_ereport_process_messages_pkey");

            entity.ToTable("ct_ereport_process_messages", "cts_transactions");

            entity.HasIndex(e => e.ErqAudDatetime, "ct_ereport_process_messages_aud_datetime_idx");

            entity.HasIndex(e => e.ErqAudId, "ct_ereport_process_messages_aud_id_idx");

            entity.HasIndex(e => new { e.ErqAudType, e.ErqAudDatetime }, "ct_ereport_process_messages_aud_type_datetime_idx");

            entity.HasIndex(e => e.ErqAudType, "ct_ereport_process_messages_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_ereport_process_messages_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ereport_process_messages_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_ereport_process_messages_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ereport_process_messages_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_ereport_process_messages_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_ereport_process_messages_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_ereport_process_messages_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_ereport_process_messages_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ErqAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("erq_aud_datetime");
            entity.Property(e => e.ErqAudId).HasColumnName("erq_aud_id");
            entity.Property(e => e.ErqAudType).HasColumnName("erq_aud_type");
            entity.Property(e => e.ErqDelayPeriod)
                .HasPrecision(10)
                .HasColumnName("erq_delay_period");
            entity.Property(e => e.ErqFileType)
                .HasMaxLength(3)
                .HasColumnName("erq_file_type");
            entity.Property(e => e.ErqSleepPeriod)
                .HasPrecision(10)
                .HasColumnName("erq_sleep_period");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtExtCetdEartag>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_ext_cetd_eartag_pkey");

            entity.ToTable("ct_ext_cetd_eartag", "cts_transactions");

            entity.HasIndex(e => e.CetAudDatetime, "ct_ext_cetd_eartag_aud_datetime_idx");

            entity.HasIndex(e => e.CetAudId, "ct_ext_cetd_eartag_aud_id_idx");

            entity.HasIndex(e => new { e.CetAudType, e.CetAudDatetime }, "ct_ext_cetd_eartag_aud_type_datetime_idx");

            entity.HasIndex(e => e.CetAudType, "ct_ext_cetd_eartag_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_ext_cetd_eartag_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ext_cetd_eartag_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_ext_cetd_eartag_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ext_cetd_eartag_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_ext_cetd_eartag_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_ext_cetd_eartag_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_ext_cetd_eartag_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_ext_cetd_eartag_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CetAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cet_aud_datetime");
            entity.Property(e => e.CetAudId).HasColumnName("cet_aud_id");
            entity.Property(e => e.CetAudType).HasColumnName("cet_aud_type");
            entity.Property(e => e.CetBsps)
                .HasMaxLength(6)
                .HasColumnName("cet_bsps");
            entity.Property(e => e.CetCid)
                .HasMaxLength(14)
                .HasColumnName("cet_cid");
            entity.Property(e => e.CetDate).HasColumnName("cet_date");
            entity.Property(e => e.CetHerd)
                .HasMaxLength(10)
                .HasColumnName("cet_herd");
            entity.Property(e => e.CetKey)
                .HasMaxLength(20)
                .HasColumnName("cet_key");
            entity.Property(e => e.CetRsc)
                .HasPrecision(3)
                .HasColumnName("cet_rsc");
            entity.Property(e => e.CetScps)
                .HasMaxLength(9)
                .HasColumnName("cet_scps");
            entity.Property(e => e.CetVersion)
                .HasPrecision(6)
                .HasColumnName("cet_version");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtExtNiDistrict>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_ext_ni_district_transactions_pkey");

            entity.ToTable("ct_ext_ni_district", "cts_transactions");

            entity.HasIndex(e => e.NidAudDatetime, "ct_ext_ni_district_aud_datetime_idx");

            entity.HasIndex(e => e.NidAudId, "ct_ext_ni_district_aud_id_idx");

            entity.HasIndex(e => new { e.NidAudType, e.NidAudDatetime }, "ct_ext_ni_district_aud_type_datetime_idx");

            entity.HasIndex(e => e.NidAudType, "ct_ext_ni_district_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_ext_ni_district_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ext_ni_district_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_ext_ni_district_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_ext_ni_district_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_ext_ni_district_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_ext_ni_district_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_ext_ni_district_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.NidAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("nid_aud_datetime");
            entity.Property(e => e.NidAudId).HasColumnName("nid_aud_id");
            entity.Property(e => e.NidAudType).HasColumnName("nid_aud_type");
            entity.Property(e => e.NidElectoralDistrict)
                .HasMaxLength(16)
                .HasColumnName("nid_electoral_district");
            entity.Property(e => e.NidHerdCode)
                .HasMaxLength(2)
                .HasColumnName("nid_herd_code");
            entity.Property(e => e.NidVersion)
                .HasPrecision(6)
                .HasColumnName("nid_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtExtSpecialHerd>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_ext_special_herd_transactions_pkey");

            entity.ToTable("ct_ext_special_herd", "cts_transactions");

            entity.HasIndex(e => e.SphAudDatetime, "ct_ext_special_herd_aud_datetime_idx");

            entity.HasIndex(e => e.SphAudId, "ct_ext_special_herd_aud_id_idx");

            entity.HasIndex(e => new { e.SphAudType, e.SphAudDatetime }, "ct_ext_special_herd_aud_type_datetime_idx");

            entity.HasIndex(e => e.SphAudType, "ct_ext_special_herd_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_ext_special_herd_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ext_special_herd_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_ext_special_herd_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_ext_special_herd_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_ext_special_herd_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_ext_special_herd_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_ext_special_herd_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SphAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("sph_aud_datetime");
            entity.Property(e => e.SphAudId).HasColumnName("sph_aud_id");
            entity.Property(e => e.SphAudType).HasColumnName("sph_aud_type");
            entity.Property(e => e.SphHerdCode)
                .HasMaxLength(10)
                .HasColumnName("sph_herd_code");
            entity.Property(e => e.SphHerdRegion)
                .HasMaxLength(30)
                .HasColumnName("sph_herd_region");
            entity.Property(e => e.SphVersion)
                .HasPrecision(6)
                .HasColumnName("sph_version");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtFileLayout>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_file_layouts_transactions_pkey");

            entity.ToTable("ct_file_layouts", "cts_transactions");

            entity.HasIndex(e => e.FltAudDatetime, "ct_file_layouts_aud_datetime_idx");

            entity.HasIndex(e => e.FltAudId, "ct_file_layouts_aud_id_idx");

            entity.HasIndex(e => new { e.FltAudType, e.FltAudDatetime }, "ct_file_layouts_aud_type_datetime_idx");

            entity.HasIndex(e => e.FltAudType, "ct_file_layouts_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_file_layouts_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_file_layouts_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_file_layouts_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_file_layouts_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_file_layouts_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_file_layouts_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_file_layouts_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.FltAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("flt_aud_datetime");
            entity.Property(e => e.FltAudId).HasColumnName("flt_aud_id");
            entity.Property(e => e.FltAudType).HasColumnName("flt_aud_type");
            entity.Property(e => e.FltConversionFormat)
                .HasMaxLength(30)
                .HasColumnName("flt_conversion_format");
            entity.Property(e => e.FltDataLength).HasColumnName("flt_data_length");
            entity.Property(e => e.FltDataPrecision).HasColumnName("flt_data_precision");
            entity.Property(e => e.FltDataType)
                .HasMaxLength(8)
                .HasColumnName("flt_data_type");
            entity.Property(e => e.FltElementDesc)
                .HasMaxLength(50)
                .HasColumnName("flt_element_desc");
            entity.Property(e => e.FltElementIndex).HasColumnName("flt_element_index");
            entity.Property(e => e.FltElementName)
                .HasMaxLength(30)
                .HasColumnName("flt_element_name");
            entity.Property(e => e.FltElementTests)
                .HasMaxLength(10)
                .HasColumnName("flt_element_tests");
            entity.Property(e => e.FltFileFormat)
                .HasMaxLength(30)
                .HasColumnName("flt_file_format");
            entity.Property(e => e.FltId).HasColumnName("flt_id");
            entity.Property(e => e.FltProcessName)
                .HasMaxLength(20)
                .HasColumnName("flt_process_name");
            entity.Property(e => e.FltRecordType)
                .HasMaxLength(1)
                .HasColumnName("flt_record_type");
            entity.Property(e => e.FltUnidataName)
                .HasMaxLength(10)
                .HasColumnName("flt_unidata_name");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtHsfSequence>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_hsf_sequences_transactions_pkey");

            entity.ToTable("ct_hsf_sequences", "cts_transactions");

            entity.HasIndex(e => e.HssAudDatetime, "ct_hsf_sequences_aud_datetime_idx");

            entity.HasIndex(e => e.HssAudId, "ct_hsf_sequences_aud_id_idx");

            entity.HasIndex(e => new { e.HssAudType, e.HssAudDatetime }, "ct_hsf_sequences_aud_type_datetime_idx");

            entity.HasIndex(e => e.HssAudType, "ct_hsf_sequences_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_hsf_sequences_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_hsf_sequences_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_hsf_sequences_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_hsf_sequences_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_hsf_sequences_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_hsf_sequences_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_hsf_sequences_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.HssAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("hss_aud_datetime");
            entity.Property(e => e.HssAudId).HasColumnName("hss_aud_id");
            entity.Property(e => e.HssAudType).HasColumnName("hss_aud_type");
            entity.Property(e => e.HssSequence)
                .HasPrecision(12)
                .HasColumnName("hss_sequence");
            entity.Property(e => e.HssSequenceKey)
                .HasMaxLength(20)
                .HasColumnName("hss_sequence_key");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtInsertUpdateLog>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_insert_update_log_pkey");

            entity.ToTable("ct_insert_update_log", "cts_transactions");

            entity.HasIndex(e => e.IulAudDatetime, "ct_insert_update_log_aud_datetime_idx");

            entity.HasIndex(e => e.IulAudId, "ct_insert_update_log_aud_id_idx");

            entity.HasIndex(e => new { e.IulAudType, e.IulAudDatetime }, "ct_insert_update_log_aud_type_datetime_idx");

            entity.HasIndex(e => e.IulAudType, "ct_insert_update_log_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_insert_update_log_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_insert_update_log_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_insert_update_log_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_insert_update_log_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_insert_update_log_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_insert_update_log_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_insert_update_log_record_type_idx");

            entity.HasIndex(e => e.IulId, "ct_insert_update_log_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_insert_update_log_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.IulAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("iul_aud_datetime");
            entity.Property(e => e.IulAudId).HasColumnName("iul_aud_id");
            entity.Property(e => e.IulAudType).HasColumnName("iul_aud_type");
            entity.Property(e => e.IulCurrentModifiedDate).HasColumnName("iul_current_modified_date");
            entity.Property(e => e.IulCurrentPid)
                .HasPrecision(3)
                .HasColumnName("iul_current_pid");
            entity.Property(e => e.IulCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("iul_current_status");
            entity.Property(e => e.IulCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("iul_current_user");
            entity.Property(e => e.IulDateProcessed).HasColumnName("iul_date_processed");
            entity.Property(e => e.IulDateProcessedMis).HasColumnName("iul_date_processed_mis");
            entity.Property(e => e.IulId)
                .HasPrecision(12)
                .HasColumnName("iul_id");
            entity.Property(e => e.IulInsertDeleteFlag)
                .HasMaxLength(1)
                .HasColumnName("iul_insert_delete_flag");
            entity.Property(e => e.IulName)
                .HasMaxLength(25)
                .HasColumnName("iul_name");
            entity.Property(e => e.IulRecordKey)
                .HasMaxLength(240)
                .HasColumnName("iul_record_key");
            entity.Property(e => e.IulSystem)
                .HasMaxLength(240)
                .HasColumnName("iul_system");
            entity.Property(e => e.IulTableName)
                .HasMaxLength(240)
                .HasColumnName("iul_table_name");
            entity.Property(e => e.IulVersion)
                .HasPrecision(6)
                .HasColumnName("iul_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtIssuedDocument>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_issued_documents_pkey");

            entity.ToTable("ct_issued_documents", "cts_transactions");

            entity.HasIndex(e => e.IdoAudDatetime, "ct_issued_documents_aud_datetime_idx");

            entity.HasIndex(e => e.IdoAudId, "ct_issued_documents_aud_id_idx");

            entity.HasIndex(e => new { e.IdoAudType, e.IdoAudDatetime }, "ct_issued_documents_aud_type_datetime_idx");

            entity.HasIndex(e => e.IdoAudType, "ct_issued_documents_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_issued_documents_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_issued_documents_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_issued_documents_file_trans_type_idx");

            entity.HasIndex(e => e.IdoLocId, "ct_issued_documents_ido_loc_id_idx");

            entity.HasIndex(e => e.IdoRanId, "ct_issued_documents_ido_ran_id_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_issued_documents_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_issued_documents_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_issued_documents_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_issued_documents_record_type_idx");

            entity.HasIndex(e => e.IdoId, "ct_issued_documents_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_issued_documents_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.IdoAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("ido_aud_datetime");
            entity.Property(e => e.IdoAudId).HasColumnName("ido_aud_id");
            entity.Property(e => e.IdoAudType).HasColumnName("ido_aud_type");
            entity.Property(e => e.IdoCreationDate).HasColumnName("ido_creation_date");
            entity.Property(e => e.IdoCurrentModifiedDate).HasColumnName("ido_current_modified_date");
            entity.Property(e => e.IdoCurrentPid)
                .HasPrecision(3)
                .HasColumnName("ido_current_pid");
            entity.Property(e => e.IdoCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("ido_current_status");
            entity.Property(e => e.IdoCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("ido_current_user");
            entity.Property(e => e.IdoId)
                .HasPrecision(12)
                .HasColumnName("ido_id");
            entity.Property(e => e.IdoInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("ido_interface_file_name");
            entity.Property(e => e.IdoInterfaceTxnNumber)
                .HasPrecision(4)
                .HasColumnName("ido_interface_txn_number");
            entity.Property(e => e.IdoLocId)
                .HasPrecision(12)
                .HasColumnName("ido_loc_id");
            entity.Property(e => e.IdoPassportVersionNumber)
                .HasPrecision(3)
                .HasColumnName("ido_passport_version_number");
            entity.Property(e => e.IdoPassptLayoutVerNumber)
                .HasMaxLength(10)
                .HasColumnName("ido_passpt_layout_ver_number");
            entity.Property(e => e.IdoRanId)
                .HasPrecision(12)
                .HasColumnName("ido_ran_id");
            entity.Property(e => e.IdoReasonCode)
                .HasMaxLength(2)
                .HasColumnName("ido_reason_code");
            entity.Property(e => e.IdoVersion)
                .HasPrecision(6)
                .HasColumnName("ido_version");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtIssuingAuthority>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_issuing_authorities_transactions_pkey");

            entity.ToTable("ct_issuing_authorities", "cts_transactions");

            entity.HasIndex(e => e.IsaAudDatetime, "ct_issuing_authorities_aud_datetime_idx");

            entity.HasIndex(e => e.IsaAudId, "ct_issuing_authorities_aud_id_idx");

            entity.HasIndex(e => new { e.IsaAudType, e.IsaAudDatetime }, "ct_issuing_authorities_aud_type_datetime_idx");

            entity.HasIndex(e => e.IsaAudType, "ct_issuing_authorities_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.IsaType, e.IsaAudType, e.IsaAudDatetime }, "ct_issuing_authorities_file_isa_type_aud_datetime_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.IsaType, e.IsaAudType }, "ct_issuing_authorities_file_isa_type_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.IsaType }, "ct_issuing_authorities_file_isa_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_issuing_authorities_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_issuing_authorities_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_issuing_authorities_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_issuing_authorities_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_issuing_authorities_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_issuing_authorities_record_type_idx");

            entity.HasIndex(e => e.IsaId, "ct_issuing_authorities_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_issuing_authorities_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.IsaAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("isa_aud_datetime");
            entity.Property(e => e.IsaAudId).HasColumnName("isa_aud_id");
            entity.Property(e => e.IsaAudType).HasColumnName("isa_aud_type");
            entity.Property(e => e.IsaCountryName)
                .HasMaxLength(240)
                .HasColumnName("isa_country_name");
            entity.Property(e => e.IsaCurrentModifiedDate).HasColumnName("isa_current_modified_date");
            entity.Property(e => e.IsaCurrentPid)
                .HasPrecision(3)
                .HasColumnName("isa_current_pid");
            entity.Property(e => e.IsaCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("isa_current_status");
            entity.Property(e => e.IsaCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("isa_current_user");
            entity.Property(e => e.IsaId)
                .HasPrecision(12)
                .HasColumnName("isa_id");
            entity.Property(e => e.IsaManufacturersName)
                .HasMaxLength(240)
                .HasColumnName("isa_manufacturers_name");
            entity.Property(e => e.IsaType)
                .HasMaxLength(10)
                .HasColumnName("isa_type");
            entity.Property(e => e.IsaVersion)
                .HasPrecision(6)
                .HasColumnName("isa_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLabelRequest>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_label_requests_pkey");

            entity.ToTable("ct_label_requests", "cts_transactions");

            entity.HasIndex(e => e.LarAudDatetime, "ct_label_requests_aud_datetime_idx");

            entity.HasIndex(e => e.LarAudId, "ct_label_requests_aud_id_idx");

            entity.HasIndex(e => new { e.LarAudType, e.LarAudDatetime }, "ct_label_requests_aud_type_datetime_idx");

            entity.HasIndex(e => e.LarAudType, "ct_label_requests_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_label_requests_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_label_requests_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_label_requests_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_label_requests_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_label_requests_imported_date_idx");

            entity.HasIndex(e => e.LarLasId, "ct_label_requests_lar_las_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_label_requests_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_label_requests_record_type_idx");

            entity.HasIndex(e => e.LarId, "ct_label_requests_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_label_requests_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LarAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("lar_aud_datetime");
            entity.Property(e => e.LarAudId).HasColumnName("lar_aud_id");
            entity.Property(e => e.LarAudType).HasColumnName("lar_aud_type");
            entity.Property(e => e.LarCorrAddress2)
                .HasMaxLength(35)
                .HasColumnName("lar_corr_address_2");
            entity.Property(e => e.LarCorrAddress3)
                .HasMaxLength(35)
                .HasColumnName("lar_corr_address_3");
            entity.Property(e => e.LarCorrAddress4)
                .HasMaxLength(35)
                .HasColumnName("lar_corr_address_4");
            entity.Property(e => e.LarCorrAddress5)
                .HasMaxLength(35)
                .HasColumnName("lar_corr_address_5");
            entity.Property(e => e.LarCorrInitials)
                .HasMaxLength(12)
                .HasColumnName("lar_corr_initials");
            entity.Property(e => e.LarCorrLocIdentifier)
                .HasMaxLength(20)
                .HasColumnName("lar_corr_loc_identifier");
            entity.Property(e => e.LarCorrLocName)
                .HasMaxLength(35)
                .HasColumnName("lar_corr_loc_name");
            entity.Property(e => e.LarCorrLocType)
                .HasMaxLength(2)
                .HasColumnName("lar_corr_loc_type");
            entity.Property(e => e.LarCorrPostCode)
                .HasMaxLength(8)
                .HasColumnName("lar_corr_post_code");
            entity.Property(e => e.LarCorrSublocIdentifier)
                .HasMaxLength(2)
                .HasColumnName("lar_corr_subloc_identifier");
            entity.Property(e => e.LarCorrSurname)
                .HasMaxLength(30)
                .HasColumnName("lar_corr_surname");
            entity.Property(e => e.LarCorrTitle)
                .HasMaxLength(10)
                .HasColumnName("lar_corr_title");
            entity.Property(e => e.LarCurrentModifiedDate).HasColumnName("lar_current_modified_date");
            entity.Property(e => e.LarCurrentPid)
                .HasPrecision(3)
                .HasColumnName("lar_current_pid");
            entity.Property(e => e.LarCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("lar_current_status");
            entity.Property(e => e.LarCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("lar_current_user");
            entity.Property(e => e.LarId)
                .HasPrecision(12)
                .HasColumnName("lar_id");
            entity.Property(e => e.LarKeeperInitials)
                .HasMaxLength(12)
                .HasColumnName("lar_keeper_initials");
            entity.Property(e => e.LarKeeperSurname)
                .HasMaxLength(30)
                .HasColumnName("lar_keeper_surname");
            entity.Property(e => e.LarKeeperTitle)
                .HasMaxLength(10)
                .HasColumnName("lar_keeper_title");
            entity.Property(e => e.LarLabelAddress2)
                .HasMaxLength(35)
                .HasColumnName("lar_label_address_2");
            entity.Property(e => e.LarLabelAddress3)
                .HasMaxLength(35)
                .HasColumnName("lar_label_address_3");
            entity.Property(e => e.LarLabelAddress4)
                .HasMaxLength(35)
                .HasColumnName("lar_label_address_4");
            entity.Property(e => e.LarLabelAddress5)
                .HasMaxLength(35)
                .HasColumnName("lar_label_address_5");
            entity.Property(e => e.LarLabelLocIdentifier)
                .HasMaxLength(20)
                .HasColumnName("lar_label_loc_identifier");
            entity.Property(e => e.LarLabelLocName)
                .HasMaxLength(35)
                .HasColumnName("lar_label_loc_name");
            entity.Property(e => e.LarLabelLocType)
                .HasMaxLength(2)
                .HasColumnName("lar_label_loc_type");
            entity.Property(e => e.LarLabelPostCode)
                .HasMaxLength(8)
                .HasColumnName("lar_label_post_code");
            entity.Property(e => e.LarLabelSublocIdentifier)
                .HasMaxLength(2)
                .HasColumnName("lar_label_subloc_identifier");
            entity.Property(e => e.LarLabelType)
                .HasMaxLength(2)
                .HasColumnName("lar_label_type");
            entity.Property(e => e.LarLabelVersion)
                .HasPrecision(6)
                .HasColumnName("lar_label_version");
            entity.Property(e => e.LarLabelsInterfaceFile)
                .HasMaxLength(25)
                .HasColumnName("lar_labels_interface_file");
            entity.Property(e => e.LarLasId)
                .HasPrecision(12)
                .HasColumnName("lar_las_id");
            entity.Property(e => e.LarPrintMethod)
                .HasMaxLength(2)
                .HasColumnName("lar_print_method");
            entity.Property(e => e.LarReasonCode)
                .HasMaxLength(2)
                .HasColumnName("lar_reason_code");
            entity.Property(e => e.LarRequestedDate).HasColumnName("lar_requested_date");
            entity.Property(e => e.LarSheetQuantity)
                .HasPrecision(10)
                .HasColumnName("lar_sheet_quantity");
            entity.Property(e => e.LarSubmittedDate).HasColumnName("lar_submitted_date");
            entity.Property(e => e.LarVersion)
                .HasPrecision(6)
                .HasColumnName("lar_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLabelSummary>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_label_summaries_pkey");

            entity.ToTable("ct_label_summaries", "cts_transactions");

            entity.HasIndex(e => e.LasAudDatetime, "ct_label_summaries_aud_datetime_idx");

            entity.HasIndex(e => e.LasAudId, "ct_label_summaries_aud_id_idx");

            entity.HasIndex(e => new { e.LasAudType, e.LasAudDatetime }, "ct_label_summaries_aud_type_datetime_idx");

            entity.HasIndex(e => e.LasAudType, "ct_label_summaries_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_label_summaries_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_label_summaries_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_label_summaries_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_label_summaries_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_label_summaries_imported_date_idx");

            entity.HasIndex(e => e.LasLocIdIdentifying, "ct_label_summaries_las_loc_id_identifying_idx");

            entity.HasIndex(e => e.LasLocIdLabels, "ct_label_summaries_las_loc_id_labels_idx");

            entity.HasIndex(e => e.RecordCount, "ct_label_summaries_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_label_summaries_record_type_idx");

            entity.HasIndex(e => e.LasId, "ct_label_summaries_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_label_summaries_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LasAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("las_aud_datetime");
            entity.Property(e => e.LasAudId).HasColumnName("las_aud_id");
            entity.Property(e => e.LasAudType).HasColumnName("las_aud_type");
            entity.Property(e => e.LasCurrentModifiedDate).HasColumnName("las_current_modified_date");
            entity.Property(e => e.LasCurrentPid)
                .HasPrecision(3)
                .HasColumnName("las_current_pid");
            entity.Property(e => e.LasCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("las_current_status");
            entity.Property(e => e.LasCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("las_current_user");
            entity.Property(e => e.LasDefaultLabelType)
                .HasMaxLength(2)
                .HasColumnName("las_default_label_type");
            entity.Property(e => e.LasDefaultSheetQuantity)
                .HasPrecision(4)
                .HasColumnName("las_default_sheet_quantity");
            entity.Property(e => e.LasId)
                .HasPrecision(12)
                .HasColumnName("las_id");
            entity.Property(e => e.LasLabelVersionNumber)
                .HasPrecision(2)
                .HasColumnName("las_label_version_number");
            entity.Property(e => e.LasLastSubmittedDate).HasColumnName("las_last_submitted_date");
            entity.Property(e => e.LasLocIdIdentifying)
                .HasPrecision(12)
                .HasColumnName("las_loc_id_identifying");
            entity.Property(e => e.LasLocIdLabels)
                .HasPrecision(12)
                .HasColumnName("las_loc_id_labels");
            entity.Property(e => e.LasVersion)
                .HasPrecision(6)
                .HasColumnName("las_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLateDay>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_late_days_transactions_pkey");

            entity.ToTable("ct_late_days", "cts_transactions");

            entity.HasIndex(e => e.LdaAudDatetime, "ct_late_days_aud_datetime_idx");

            entity.HasIndex(e => e.LdaAudId, "ct_late_days_aud_id_idx");

            entity.HasIndex(e => new { e.LdaAudType, e.LdaAudDatetime }, "ct_late_days_aud_type_datetime_idx");

            entity.HasIndex(e => e.LdaAudType, "ct_late_days_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_late_days_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_late_days_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_late_days_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_late_days_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_late_days_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_late_days_record_type_idx");

            entity.HasIndex(e => e.LdaId, "ct_late_days_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_late_days_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LdaApplicType)
                .HasMaxLength(1)
                .HasColumnName("lda_applic_type");
            entity.Property(e => e.LdaAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("lda_aud_datetime");
            entity.Property(e => e.LdaAudId).HasColumnName("lda_aud_id");
            entity.Property(e => e.LdaAudType).HasColumnName("lda_aud_type");
            entity.Property(e => e.LdaCurrentModifiedDate).HasColumnName("lda_current_modified_date");
            entity.Property(e => e.LdaCurrentPid)
                .HasPrecision(3)
                .HasColumnName("lda_current_pid");
            entity.Property(e => e.LdaCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("lda_current_status");
            entity.Property(e => e.LdaCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("lda_current_user");
            entity.Property(e => e.LdaId)
                .HasPrecision(12)
                .HasColumnName("lda_id");
            entity.Property(e => e.LdaStartDate).HasColumnName("lda_start_date");
            entity.Property(e => e.LdaValidDays)
                .HasPrecision(4)
                .HasColumnName("lda_valid_days");
            entity.Property(e => e.LdaVersion)
                .HasPrecision(6)
                .HasColumnName("lda_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLetter>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_letters_pkey");

            entity.ToTable("ct_letters", "cts_transactions");

            entity.HasIndex(e => e.LetAudDatetime, "ct_letters_aud_datetime_idx");

            entity.HasIndex(e => e.LetAudId, "ct_letters_aud_id_idx");

            entity.HasIndex(e => new { e.LetAudType, e.LetAudDatetime }, "ct_letters_aud_type_datetime_idx");

            entity.HasIndex(e => e.LetAudType, "ct_letters_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.LetType, e.LetAudType, e.LetAudDatetime }, "ct_letters_file_let_type_aud_datetime_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.LetType, e.LetAudType }, "ct_letters_file_let_type_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.LetType }, "ct_letters_file_let_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_letters_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_letters_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_letters_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_letters_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_letters_imported_date_idx");

            entity.HasIndex(e => e.LetWgpId, "ct_letters_let_wgp_id_idx");

            entity.HasIndex(e => e.LetWgpIdSent, "ct_letters_let_wgp_id_sent_idx");

            entity.HasIndex(e => e.RecordCount, "ct_letters_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_letters_record_type_idx");

            entity.HasIndex(e => e.LetId, "ct_letters_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_letters_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LetAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("let_aud_datetime");
            entity.Property(e => e.LetAudId).HasColumnName("let_aud_id");
            entity.Property(e => e.LetAudType).HasColumnName("let_aud_type");
            entity.Property(e => e.LetCurrentModifiedDate).HasColumnName("let_current_modified_date");
            entity.Property(e => e.LetCurrentPid)
                .HasPrecision(3)
                .HasColumnName("let_current_pid");
            entity.Property(e => e.LetCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("let_current_status");
            entity.Property(e => e.LetCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("let_current_user");
            entity.Property(e => e.LetDescription)
                .HasMaxLength(30)
                .HasColumnName("let_description");
            entity.Property(e => e.LetId)
                .HasPrecision(12)
                .HasColumnName("let_id");
            entity.Property(e => e.LetProgramName)
                .HasMaxLength(10)
                .HasColumnName("let_program_name");
            entity.Property(e => e.LetType)
                .HasMaxLength(3)
                .HasColumnName("let_type");
            entity.Property(e => e.LetVersion)
                .HasPrecision(6)
                .HasColumnName("let_version");
            entity.Property(e => e.LetWgpId)
                .HasPrecision(12)
                .HasColumnName("let_wgp_id");
            entity.Property(e => e.LetWgpIdSent)
                .HasPrecision(12)
                .HasColumnName("let_wgp_id_sent");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLocTypeRelComb>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_loc_type_rel_combs_transactions_pkey");

            entity.ToTable("ct_loc_type_rel_combs", "cts_transactions");

            entity.HasIndex(e => e.LrcAudDatetime, "ct_loc_type_rel_combs_aud_datetime_idx");

            entity.HasIndex(e => e.LrcAudId, "ct_loc_type_rel_combs_aud_id_idx");

            entity.HasIndex(e => new { e.LrcAudType, e.LrcAudDatetime }, "ct_loc_type_rel_combs_aud_type_datetime_idx");

            entity.HasIndex(e => e.LrcAudType, "ct_loc_type_rel_combs_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_loc_type_rel_combs_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_loc_type_rel_combs_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_loc_type_rel_combs_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_loc_type_rel_combs_imported_date_idx");

            entity.HasIndex(e => e.LrcLrtId, "ct_loc_type_rel_combs_lrc_lrt_id_idx");

            entity.HasIndex(e => e.LrcLtyId1, "ct_loc_type_rel_combs_lrc_lty_id_1_idx");

            entity.HasIndex(e => e.LrcLtyId2, "ct_loc_type_rel_combs_lrc_lty_id_2_idx");

            entity.HasIndex(e => e.RecordCount, "ct_loc_type_rel_combs_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_loc_type_rel_combs_record_type_idx");

            entity.HasIndex(e => e.LrcId, "ct_loc_type_rel_combs_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_loc_type_rel_combs_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LrcAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("lrc_aud_datetime");
            entity.Property(e => e.LrcAudId).HasColumnName("lrc_aud_id");
            entity.Property(e => e.LrcAudType).HasColumnName("lrc_aud_type");
            entity.Property(e => e.LrcCurrentModifiedDate).HasColumnName("lrc_current_modified_date");
            entity.Property(e => e.LrcCurrentPid)
                .HasPrecision(3)
                .HasColumnName("lrc_current_pid");
            entity.Property(e => e.LrcCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("lrc_current_status");
            entity.Property(e => e.LrcCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("lrc_current_user");
            entity.Property(e => e.LrcId)
                .HasPrecision(12)
                .HasColumnName("lrc_id");
            entity.Property(e => e.LrcLrtId)
                .HasPrecision(12)
                .HasColumnName("lrc_lrt_id");
            entity.Property(e => e.LrcLtyId1)
                .HasPrecision(12)
                .HasColumnName("lrc_lty_id_1");
            entity.Property(e => e.LrcLtyId2)
                .HasPrecision(12)
                .HasColumnName("lrc_lty_id_2");
            entity.Property(e => e.LrcVersion)
                .HasPrecision(6)
                .HasColumnName("lrc_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLocation>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_locations_pkey");

            entity.ToTable("ct_locations", "cts_transactions");

            entity.HasIndex(e => e.LocAudDatetime, "ct_locations_aud_datetime_idx");

            entity.HasIndex(e => e.LocAudId, "ct_locations_aud_id_idx");

            entity.HasIndex(e => new { e.LocAudType, e.LocAudDatetime }, "ct_locations_aud_type_datetime_idx");

            entity.HasIndex(e => e.LocAudType, "ct_locations_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_locations_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_locations_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_locations_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_locations_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_locations_imported_date_idx");

            entity.HasIndex(e => e.LocCtyId, "ct_locations_loc_cty_id_idx");

            entity.HasIndex(e => e.LocLtyId, "ct_locations_loc_lty_id_idx");

            entity.HasIndex(e => e.LocSltId, "ct_locations_loc_slt_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_locations_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_locations_record_type_idx");

            entity.HasIndex(e => e.LocId, "ct_locations_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_locations_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.FakeData)
                .HasPrecision(1)
                .HasColumnName("fake_data");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LocAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("loc_aud_datetime");
            entity.Property(e => e.LocAudId).HasColumnName("loc_aud_id");
            entity.Property(e => e.LocAudType).HasColumnName("loc_aud_type");
            entity.Property(e => e.LocCessationReason)
                .HasMaxLength(2)
                .HasColumnName("loc_cessation_reason");
            entity.Property(e => e.LocComments)
                .HasMaxLength(400)
                .HasColumnName("loc_comments");
            entity.Property(e => e.LocCtyId)
                .HasPrecision(12)
                .HasColumnName("loc_cty_id");
            entity.Property(e => e.LocCurrentModifiedDate).HasColumnName("loc_current_modified_date");
            entity.Property(e => e.LocCurrentPid)
                .HasPrecision(3)
                .HasColumnName("loc_current_pid");
            entity.Property(e => e.LocCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("loc_current_status");
            entity.Property(e => e.LocCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("loc_current_user");
            entity.Property(e => e.LocEffectiveFrom).HasColumnName("loc_effective_from");
            entity.Property(e => e.LocEffectiveTo).HasColumnName("loc_effective_to");
            entity.Property(e => e.LocEmailAddress)
                .HasMaxLength(50)
                .HasColumnName("loc_email_address");
            entity.Property(e => e.LocFaxNumber)
                .HasMaxLength(25)
                .HasColumnName("loc_fax_number");
            entity.Property(e => e.LocId)
                .HasPrecision(12)
                .HasColumnName("loc_id");
            entity.Property(e => e.LocLtyId)
                .HasPrecision(12)
                .HasColumnName("loc_lty_id");
            entity.Property(e => e.LocMapReference)
                .HasMaxLength(12)
                .HasColumnName("loc_map_reference");
            entity.Property(e => e.LocMobileNumber)
                .HasMaxLength(25)
                .HasColumnName("loc_mobile_number");
            entity.Property(e => e.LocPremisesType)
                .HasMaxLength(4)
                .HasColumnName("loc_premises_type");
            entity.Property(e => e.LocReasonCode)
                .HasMaxLength(2)
                .HasColumnName("loc_reason_code");
            entity.Property(e => e.LocReceiveLabelsFlag)
                .HasMaxLength(1)
                .HasColumnName("loc_receive_labels_flag");
            entity.Property(e => e.LocReceivePpafFlag)
                .HasMaxLength(1)
                .HasColumnName("loc_receive_ppaf_flag");
            entity.Property(e => e.LocSltId)
                .HasPrecision(12)
                .HasColumnName("loc_slt_id");
            entity.Property(e => e.LocSourceIdentifier)
                .HasMaxLength(2)
                .HasColumnName("loc_source_identifier");
            entity.Property(e => e.LocSourceReference)
                .HasMaxLength(20)
                .HasColumnName("loc_source_reference");
            entity.Property(e => e.LocTelNumber)
                .HasMaxLength(25)
                .HasColumnName("loc_tel_number");
            entity.Property(e => e.LocVersion)
                .HasPrecision(6)
                .HasColumnName("loc_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLocationIdFormat>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_location_id_formats_transactions_pkey");

            entity.ToTable("ct_location_id_formats", "cts_transactions");

            entity.HasIndex(e => e.LifAudDatetime, "ct_location_id_formats_aud_datetime_idx");

            entity.HasIndex(e => e.LifAudId, "ct_location_id_formats_aud_id_idx");

            entity.HasIndex(e => new { e.LifAudType, e.LifAudDatetime }, "ct_location_id_formats_aud_type_datetime_idx");

            entity.HasIndex(e => e.LifAudType, "ct_location_id_formats_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_location_id_formats_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_location_id_formats_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_location_id_formats_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_location_id_formats_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_location_id_formats_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_location_id_formats_record_type_idx");

            entity.HasIndex(e => e.LifId, "ct_location_id_formats_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_location_id_formats_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LifAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("lif_aud_datetime");
            entity.Property(e => e.LifAudId).HasColumnName("lif_aud_id");
            entity.Property(e => e.LifAudType).HasColumnName("lif_aud_type");
            entity.Property(e => e.LifCurrentModifiedDate).HasColumnName("lif_current_modified_date");
            entity.Property(e => e.LifCurrentPid)
                .HasPrecision(3)
                .HasColumnName("lif_current_pid");
            entity.Property(e => e.LifCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("lif_current_status");
            entity.Property(e => e.LifCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("lif_current_user");
            entity.Property(e => e.LifDescription)
                .HasMaxLength(30)
                .HasColumnName("lif_description");
            entity.Property(e => e.LifFormatPattern)
                .HasMaxLength(15)
                .HasColumnName("lif_format_pattern");
            entity.Property(e => e.LifId)
                .HasPrecision(12)
                .HasColumnName("lif_id");
            entity.Property(e => e.LifLocTypeReqd)
                .HasMaxLength(1)
                .HasColumnName("lif_loc_type_reqd");
            entity.Property(e => e.LifSublocTypeReqd)
                .HasMaxLength(1)
                .HasColumnName("lif_subloc_type_reqd");
            entity.Property(e => e.LifVersion)
                .HasPrecision(6)
                .HasColumnName("lif_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLocationIdentifier>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_location_identifiers_pkey");

            entity.ToTable("ct_location_identifiers", "cts_transactions");

            entity.HasIndex(e => e.LidAudDatetime, "ct_location_identifiers_aud_datetime_idx");

            entity.HasIndex(e => e.LidAudId, "ct_location_identifiers_aud_id_idx");

            entity.HasIndex(e => new { e.LidAudType, e.LidAudDatetime }, "ct_location_identifiers_aud_type_datetime_idx");

            entity.HasIndex(e => e.LidAudType, "ct_location_identifiers_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_location_identifiers_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_location_identifiers_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_location_identifiers_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_location_identifiers_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_location_identifiers_imported_date_idx");

            entity.HasIndex(e => e.LidLocId, "ct_location_identifiers_lid_loc_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_location_identifiers_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_location_identifiers_record_type_idx");

            entity.HasIndex(e => e.LidId, "ct_location_identifiers_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_location_identifiers_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LidAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("lid_aud_datetime");
            entity.Property(e => e.LidAudId).HasColumnName("lid_aud_id");
            entity.Property(e => e.LidAudType).HasColumnName("lid_aud_type");
            entity.Property(e => e.LidCurrentAmendReason)
                .HasMaxLength(2)
                .HasColumnName("lid_current_amend_reason");
            entity.Property(e => e.LidCurrentModifiedDate).HasColumnName("lid_current_modified_date");
            entity.Property(e => e.LidCurrentPid)
                .HasPrecision(3)
                .HasColumnName("lid_current_pid");
            entity.Property(e => e.LidCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("lid_current_status");
            entity.Property(e => e.LidCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("lid_current_user");
            entity.Property(e => e.LidEffectiveFromDate).HasColumnName("lid_effective_from_date");
            entity.Property(e => e.LidEffectiveToDate).HasColumnName("lid_effective_to_date");
            entity.Property(e => e.LidFullIdentifier)
                .HasMaxLength(17)
                .HasColumnName("lid_full_identifier");
            entity.Property(e => e.LidId)
                .HasPrecision(12)
                .HasColumnName("lid_id");
            entity.Property(e => e.LidIdentifier)
                .HasMaxLength(14)
                .HasColumnName("lid_identifier");
            entity.Property(e => e.LidLocId)
                .HasPrecision(12)
                .HasColumnName("lid_loc_id");
            entity.Property(e => e.LidSubIdentifier)
                .HasMaxLength(2)
                .HasColumnName("lid_sub_identifier");
            entity.Property(e => e.LidVersion)
                .HasPrecision(6)
                .HasColumnName("lid_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLocationPartyRel>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_location_party_rels_pkey");

            entity.ToTable("ct_location_party_rels", "cts_transactions");

            entity.HasIndex(e => e.LprAudDatetime, "ct_location_party_rels_aud_datetime_idx");

            entity.HasIndex(e => e.LprAudId, "ct_location_party_rels_aud_id_idx");

            entity.HasIndex(e => new { e.LprAudType, e.LprAudDatetime }, "ct_location_party_rels_aud_type_datetime_idx");

            entity.HasIndex(e => e.LprAudType, "ct_location_party_rels_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_location_party_rels_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_location_party_rels_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_location_party_rels_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_location_party_rels_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_location_party_rels_imported_date_idx");

            entity.HasIndex(e => e.LprLocId, "ct_location_party_rels_lpr_loc_id_idx");

            entity.HasIndex(e => e.LprLptId, "ct_location_party_rels_lpr_lpt_id_idx");

            entity.HasIndex(e => e.LprParId, "ct_location_party_rels_lpr_par_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_location_party_rels_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_location_party_rels_record_type_idx");

            entity.HasIndex(e => e.LprId, "ct_location_party_rels_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_location_party_rels_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LprAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("lpr_aud_datetime");
            entity.Property(e => e.LprAudId).HasColumnName("lpr_aud_id");
            entity.Property(e => e.LprAudType).HasColumnName("lpr_aud_type");
            entity.Property(e => e.LprCessationReason)
                .HasMaxLength(3)
                .HasColumnName("lpr_cessation_reason");
            entity.Property(e => e.LprComments)
                .HasMaxLength(250)
                .HasColumnName("lpr_comments");
            entity.Property(e => e.LprCurrentModifiedDate).HasColumnName("lpr_current_modified_date");
            entity.Property(e => e.LprCurrentPid)
                .HasPrecision(3)
                .HasColumnName("lpr_current_pid");
            entity.Property(e => e.LprCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("lpr_current_status");
            entity.Property(e => e.LprCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("lpr_current_user");
            entity.Property(e => e.LprEffectiveFromDate).HasColumnName("lpr_effective_from_date");
            entity.Property(e => e.LprEffectiveToDate).HasColumnName("lpr_effective_to_date");
            entity.Property(e => e.LprId)
                .HasPrecision(12)
                .HasColumnName("lpr_id");
            entity.Property(e => e.LprLocId)
                .HasPrecision(12)
                .HasColumnName("lpr_loc_id");
            entity.Property(e => e.LprLptId)
                .HasPrecision(12)
                .HasColumnName("lpr_lpt_id");
            entity.Property(e => e.LprParId)
                .HasPrecision(12)
                .HasColumnName("lpr_par_id");
            entity.Property(e => e.LprVersion)
                .HasPrecision(6)
                .HasColumnName("lpr_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLocationPartyRelType>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_location_party_rel_types_transactions_pkey");

            entity.ToTable("ct_location_party_rel_types", "cts_transactions");

            entity.HasIndex(e => e.LptAudDatetime, "ct_location_party_rel_types_aud_datetime_idx");

            entity.HasIndex(e => e.LptAudId, "ct_location_party_rel_types_aud_id_idx");

            entity.HasIndex(e => new { e.LptAudType, e.LptAudDatetime }, "ct_location_party_rel_types_aud_type_datetime_idx");

            entity.HasIndex(e => e.LptAudType, "ct_location_party_rel_types_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_location_party_rel_types_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_location_party_rel_types_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_location_party_rel_types_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_location_party_rel_types_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_location_party_rel_types_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_location_party_rel_types_record_type_idx");

            entity.HasIndex(e => e.LptId, "ct_location_party_rel_types_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_location_party_rel_types_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LptAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("lpt_aud_datetime");
            entity.Property(e => e.LptAudId).HasColumnName("lpt_aud_id");
            entity.Property(e => e.LptAudType).HasColumnName("lpt_aud_type");
            entity.Property(e => e.LptCode)
                .HasMaxLength(2)
                .HasColumnName("lpt_code");
            entity.Property(e => e.LptCurrentModifiedDate).HasColumnName("lpt_current_modified_date");
            entity.Property(e => e.LptCurrentPid)
                .HasPrecision(3)
                .HasColumnName("lpt_current_pid");
            entity.Property(e => e.LptCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("lpt_current_status");
            entity.Property(e => e.LptCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("lpt_current_user");
            entity.Property(e => e.LptDescription)
                .HasMaxLength(60)
                .HasColumnName("lpt_description");
            entity.Property(e => e.LptGapsAllowed)
                .HasMaxLength(1)
                .HasColumnName("lpt_gaps_allowed");
            entity.Property(e => e.LptHierarchicalLink)
                .HasMaxLength(1)
                .HasColumnName("lpt_hierarchical_link");
            entity.Property(e => e.LptId)
                .HasPrecision(12)
                .HasColumnName("lpt_id");
            entity.Property(e => e.LptMandatory)
                .HasMaxLength(1)
                .HasColumnName("lpt_mandatory");
            entity.Property(e => e.LptPrimarySingleLink)
                .HasMaxLength(1)
                .HasColumnName("lpt_primary_single_link");
            entity.Property(e => e.LptRelshipTextDown)
                .HasMaxLength(30)
                .HasColumnName("lpt_relship_text_down");
            entity.Property(e => e.LptRelshipTextUp)
                .HasMaxLength(30)
                .HasColumnName("lpt_relship_text_up");
            entity.Property(e => e.LptSecondSingleLink)
                .HasMaxLength(1)
                .HasColumnName("lpt_second_single_link");
            entity.Property(e => e.LptVersion)
                .HasPrecision(6)
                .HasColumnName("lpt_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLocationRelType>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_location_rel_types_transactions_pkey");

            entity.ToTable("ct_location_rel_types", "cts_transactions");

            entity.HasIndex(e => e.LrtAudDatetime, "ct_location_rel_types_aud_datetime_idx");

            entity.HasIndex(e => e.LrtAudId, "ct_location_rel_types_aud_id_idx");

            entity.HasIndex(e => new { e.LrtAudType, e.LrtAudDatetime }, "ct_location_rel_types_aud_type_datetime_idx");

            entity.HasIndex(e => e.LrtAudType, "ct_location_rel_types_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_location_rel_types_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_location_rel_types_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_location_rel_types_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_location_rel_types_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_location_rel_types_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_location_rel_types_record_type_idx");

            entity.HasIndex(e => e.LrtId, "ct_location_rel_types_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_location_rel_types_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LrtAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("lrt_aud_datetime");
            entity.Property(e => e.LrtAudId).HasColumnName("lrt_aud_id");
            entity.Property(e => e.LrtAudType).HasColumnName("lrt_aud_type");
            entity.Property(e => e.LrtCode)
                .HasMaxLength(2)
                .HasColumnName("lrt_code");
            entity.Property(e => e.LrtCurrentModifiedDate).HasColumnName("lrt_current_modified_date");
            entity.Property(e => e.LrtCurrentPid)
                .HasPrecision(3)
                .HasColumnName("lrt_current_pid");
            entity.Property(e => e.LrtCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("lrt_current_status");
            entity.Property(e => e.LrtCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("lrt_current_user");
            entity.Property(e => e.LrtDescription)
                .HasMaxLength(30)
                .HasColumnName("lrt_description");
            entity.Property(e => e.LrtGapsAllowed)
                .HasMaxLength(1)
                .HasColumnName("lrt_gaps_allowed");
            entity.Property(e => e.LrtHierarchicalLink)
                .HasMaxLength(1)
                .HasColumnName("lrt_hierarchical_link");
            entity.Property(e => e.LrtId)
                .HasPrecision(12)
                .HasColumnName("lrt_id");
            entity.Property(e => e.LrtMandatory)
                .HasMaxLength(1)
                .HasColumnName("lrt_mandatory");
            entity.Property(e => e.LrtPrimarySingleLink)
                .HasMaxLength(1)
                .HasColumnName("lrt_primary_single_link");
            entity.Property(e => e.LrtRelshipTextDown)
                .HasMaxLength(30)
                .HasColumnName("lrt_relship_text_down");
            entity.Property(e => e.LrtRelshipTextUp)
                .HasMaxLength(30)
                .HasColumnName("lrt_relship_text_up");
            entity.Property(e => e.LrtSecondSingleLink)
                .HasMaxLength(1)
                .HasColumnName("lrt_second_single_link");
            entity.Property(e => e.LrtVersion)
                .HasPrecision(6)
                .HasColumnName("lrt_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLocationRelationship>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_location_relationships_pkey");

            entity.ToTable("ct_location_relationships", "cts_transactions");

            entity.HasIndex(e => e.LlrAudDatetime, "ct_location_relationships_aud_datetime_idx");

            entity.HasIndex(e => e.LlrAudId, "ct_location_relationships_aud_id_idx");

            entity.HasIndex(e => new { e.LlrAudType, e.LlrAudDatetime }, "ct_location_relationships_aud_type_datetime_idx");

            entity.HasIndex(e => e.LlrAudType, "ct_location_relationships_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_location_relationships_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_location_relationships_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_location_relationships_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_location_relationships_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_location_relationships_imported_date_idx");

            entity.HasIndex(e => e.LlrLocIdChild, "ct_location_relationships_llr_loc_id_child_idx");

            entity.HasIndex(e => e.LlrLocIdParent, "ct_location_relationships_llr_loc_id_parent_idx");

            entity.HasIndex(e => e.LlrLrtId, "ct_location_relationships_llr_lrt_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_location_relationships_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_location_relationships_record_type_idx");

            entity.HasIndex(e => e.LlrId, "ct_location_relationships_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_location_relationships_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LlrAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("llr_aud_datetime");
            entity.Property(e => e.LlrAudId).HasColumnName("llr_aud_id");
            entity.Property(e => e.LlrAudType).HasColumnName("llr_aud_type");
            entity.Property(e => e.LlrCessationReason)
                .HasMaxLength(2)
                .HasColumnName("llr_cessation_reason");
            entity.Property(e => e.LlrComments)
                .HasMaxLength(200)
                .HasColumnName("llr_comments");
            entity.Property(e => e.LlrCurrentModifiedDate).HasColumnName("llr_current_modified_date");
            entity.Property(e => e.LlrCurrentPid)
                .HasPrecision(3)
                .HasColumnName("llr_current_pid");
            entity.Property(e => e.LlrCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("llr_current_status");
            entity.Property(e => e.LlrCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("llr_current_user");
            entity.Property(e => e.LlrEffectiveFromDate).HasColumnName("llr_effective_from_date");
            entity.Property(e => e.LlrEffectiveToDate).HasColumnName("llr_effective_to_date");
            entity.Property(e => e.LlrId)
                .HasPrecision(12)
                .HasColumnName("llr_id");
            entity.Property(e => e.LlrLocIdChild)
                .HasPrecision(12)
                .HasColumnName("llr_loc_id_child");
            entity.Property(e => e.LlrLocIdParent)
                .HasPrecision(12)
                .HasColumnName("llr_loc_id_parent");
            entity.Property(e => e.LlrLrtId)
                .HasPrecision(12)
                .HasColumnName("llr_lrt_id");
            entity.Property(e => e.LlrVersion)
                .HasPrecision(6)
                .HasColumnName("llr_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLocationType>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_location_types_transactions_pkey");

            entity.ToTable("ct_location_types", "cts_transactions");

            entity.HasIndex(e => e.LtyAudDatetime, "ct_location_types_aud_datetime_idx");

            entity.HasIndex(e => e.LtyAudId, "ct_location_types_aud_id_idx");

            entity.HasIndex(e => new { e.LtyAudType, e.LtyAudDatetime }, "ct_location_types_aud_type_datetime_idx");

            entity.HasIndex(e => e.LtyAudType, "ct_location_types_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_location_types_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_location_types_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_location_types_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_location_types_imported_date_idx");

            entity.HasIndex(e => e.LtyLifId, "ct_location_types_lty_lif_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_location_types_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_location_types_record_type_idx");

            entity.HasIndex(e => e.LtyId, "ct_location_types_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_location_types_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LtyAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("lty_aud_datetime");
            entity.Property(e => e.LtyAudId).HasColumnName("lty_aud_id");
            entity.Property(e => e.LtyAudType).HasColumnName("lty_aud_type");
            entity.Property(e => e.LtyCiiLocationType)
                .HasMaxLength(1)
                .HasColumnName("lty_cii_location_type");
            entity.Property(e => e.LtyCurrentModifiedDate).HasColumnName("lty_current_modified_date");
            entity.Property(e => e.LtyCurrentPid)
                .HasPrecision(3)
                .HasColumnName("lty_current_pid");
            entity.Property(e => e.LtyCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("lty_current_status");
            entity.Property(e => e.LtyCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("lty_current_user");
            entity.Property(e => e.LtyHierLinkPermitted)
                .HasMaxLength(1)
                .HasColumnName("lty_hier_link_permitted");
            entity.Property(e => e.LtyId)
                .HasPrecision(12)
                .HasColumnName("lty_id");
            entity.Property(e => e.LtyLifId)
                .HasPrecision(12)
                .HasColumnName("lty_lif_id");
            entity.Property(e => e.LtyLocType)
                .HasMaxLength(2)
                .HasColumnName("lty_loc_type");
            entity.Property(e => e.LtyLocationTypeReqd)
                .HasPrecision(1)
                .HasColumnName("lty_location_type_reqd");
            entity.Property(e => e.LtyLongDescription)
                .HasMaxLength(60)
                .HasColumnName("lty_long_description");
            entity.Property(e => e.LtyMovementLocInd)
                .HasMaxLength(1)
                .HasColumnName("lty_movement_loc_ind");
            entity.Property(e => e.LtyOwnership)
                .HasMaxLength(2)
                .HasColumnName("lty_ownership");
            entity.Property(e => e.LtyPeerLinkPermitted)
                .HasMaxLength(1)
                .HasColumnName("lty_peer_link_permitted");
            entity.Property(e => e.LtyPerformAnomalyCheck)
                .HasMaxLength(1)
                .HasColumnName("lty_perform_anomaly_check");
            entity.Property(e => e.LtyPremisesGroup)
                .HasMaxLength(2)
                .HasColumnName("lty_premises_group");
            entity.Property(e => e.LtyShortDescription)
                .HasMaxLength(20)
                .HasColumnName("lty_short_description");
            entity.Property(e => e.LtySublocTypeReqd)
                .HasPrecision(1)
                .HasColumnName("lty_subloc_type_reqd");
            entity.Property(e => e.LtyVersion)
                .HasPrecision(6)
                .HasColumnName("lty_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLocationsFaker>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_locations_faker_pkey");

            entity.ToTable("ct_locations_faker", "cts_transactions");

            entity.HasIndex(e => e.LocAudDatetime, "ct_locations_faker_aud_datetime_idx");

            entity.HasIndex(e => e.LocAudId, "ct_locations_faker_aud_id_idx");

            entity.HasIndex(e => new { e.LocAudType, e.LocAudDatetime }, "ct_locations_faker_aud_type_datetime_idx");

            entity.HasIndex(e => e.LocAudType, "ct_locations_faker_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_locations_faker_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_locations_faker_file_trans_type_idx");

            entity.HasIndex(e => e.CtsFileImportId, "ct_locations_faker_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_locations_faker_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_locations_faker_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_locations_faker_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_locations_faker_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LocAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("loc_aud_datetime");
            entity.Property(e => e.LocAudId).HasColumnName("loc_aud_id");
            entity.Property(e => e.LocAudType).HasColumnName("loc_aud_type");
            entity.Property(e => e.LocComments).HasColumnName("loc_comments");
            entity.Property(e => e.LocEmailAddress).HasColumnName("loc_email_address");
            entity.Property(e => e.LocFaxNumber).HasColumnName("loc_fax_number");
            entity.Property(e => e.LocMobileNumber).HasColumnName("loc_mobile_number");
            entity.Property(e => e.LocSourceReference).HasColumnName("loc_source_reference");
            entity.Property(e => e.LocTelNumber).HasColumnName("loc_tel_number");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtLocrestrictionstoanimal>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_locrestrictionstoanimals_pkey");

            entity.ToTable("ct_locrestrictionstoanimals", "cts_transactions");

            entity.HasIndex(e => e.LraAudDatetime, "ct_locrestrictionstoanimals_aud_datetime_idx");

            entity.HasIndex(e => e.LraAudId, "ct_locrestrictionstoanimals_aud_id_idx");

            entity.HasIndex(e => new { e.LraAudType, e.LraAudDatetime }, "ct_locrestrictionstoanimals_aud_type_datetime_idx");

            entity.HasIndex(e => e.LraAudType, "ct_locrestrictionstoanimals_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_locrestrictionstoanimals_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_locrestrictionstoanimals_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_locrestrictionstoanimals_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_locrestrictionstoanimals_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_locrestrictionstoanimals_imported_date_idx");

            entity.HasIndex(e => e.LraComId, "ct_locrestrictionstoanimals_lra_com_id_idx");

            entity.HasIndex(e => e.LraLocId, "ct_locrestrictionstoanimals_lra_loc_id_idx");

            entity.HasIndex(e => e.LraRanId, "ct_locrestrictionstoanimals_lra_ran_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_locrestrictionstoanimals_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_locrestrictionstoanimals_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_locrestrictionstoanimals_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LraAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("lra_aud_datetime");
            entity.Property(e => e.LraAudId).HasColumnName("lra_aud_id");
            entity.Property(e => e.LraAudType).HasColumnName("lra_aud_type");
            entity.Property(e => e.LraComEffectiveFrom).HasColumnName("lra_com_effective_from");
            entity.Property(e => e.LraComEffectiveTo).HasColumnName("lra_com_effective_to");
            entity.Property(e => e.LraComId)
                .HasPrecision(12)
                .HasColumnName("lra_com_id");
            entity.Property(e => e.LraLastProbityDate).HasColumnName("lra_last_probity_date");
            entity.Property(e => e.LraLocId)
                .HasPrecision(12)
                .HasColumnName("lra_loc_id");
            entity.Property(e => e.LraRanId)
                .HasPrecision(12)
                .HasColumnName("lra_ran_id");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtMgtControlError>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_mgt_control_errors_pkey");

            entity.ToTable("ct_mgt_control_errors", "cts_transactions");

            entity.HasIndex(e => e.MceAudDatetime, "ct_mgt_control_errors_aud_datetime_idx");

            entity.HasIndex(e => e.MceAudId, "ct_mgt_control_errors_aud_id_idx");

            entity.HasIndex(e => new { e.MceAudType, e.MceAudDatetime }, "ct_mgt_control_errors_aud_type_datetime_idx");

            entity.HasIndex(e => e.MceAudType, "ct_mgt_control_errors_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_mgt_control_errors_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_mgt_control_errors_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_mgt_control_errors_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_mgt_control_errors_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_mgt_control_errors_imported_date_idx");

            entity.HasIndex(e => e.MceRanId, "ct_mgt_control_errors_mce_ran_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_mgt_control_errors_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_mgt_control_errors_record_type_idx");

            entity.HasIndex(e => e.MceId, "ct_mgt_control_errors_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_mgt_control_errors_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.MceAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("mce_aud_datetime");
            entity.Property(e => e.MceAudId).HasColumnName("mce_aud_id");
            entity.Property(e => e.MceAudType).HasColumnName("mce_aud_type");
            entity.Property(e => e.MceCurrentModifiedDate).HasColumnName("mce_current_modified_date");
            entity.Property(e => e.MceCurrentPid)
                .HasPrecision(3)
                .HasColumnName("mce_current_pid");
            entity.Property(e => e.MceCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("mce_current_status");
            entity.Property(e => e.MceCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("mce_current_user");
            entity.Property(e => e.MceErrorCode)
                .HasMaxLength(5)
                .HasColumnName("mce_error_code");
            entity.Property(e => e.MceId)
                .HasPrecision(12)
                .HasColumnName("mce_id");
            entity.Property(e => e.MceNumberOfDaysLate)
                .HasPrecision(4)
                .HasColumnName("mce_number_of_days_late");
            entity.Property(e => e.McePassportVersionIssued)
                .HasPrecision(4)
                .HasColumnName("mce_passport_version_issued");
            entity.Property(e => e.MceRanId)
                .HasPrecision(12)
                .HasColumnName("mce_ran_id");
            entity.Property(e => e.MceVersion)
                .HasPrecision(6)
                .HasColumnName("mce_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtMgtWgAllocationRule>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_mgt_wg_allocation_rules_transactions_pkey");

            entity.ToTable("ct_mgt_wg_allocation_rules", "cts_transactions");

            entity.HasIndex(e => e.WarAudDatetime, "ct_mgt_wg_allocation_rules_aud_datetime_idx");

            entity.HasIndex(e => e.WarAudId, "ct_mgt_wg_allocation_rules_aud_id_idx");

            entity.HasIndex(e => new { e.WarAudType, e.WarAudDatetime }, "ct_mgt_wg_allocation_rules_aud_type_datetime_idx");

            entity.HasIndex(e => e.WarAudType, "ct_mgt_wg_allocation_rules_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_mgt_wg_allocation_rules_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_mgt_wg_allocation_rules_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_mgt_wg_allocation_rules_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_mgt_wg_allocation_rules_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_mgt_wg_allocation_rules_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_mgt_wg_allocation_rules_record_type_idx");

            entity.HasIndex(e => e.WarId, "ct_mgt_wg_allocation_rules_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_mgt_wg_allocation_rules_trans_type_idx");

            entity.HasIndex(e => e.WarRouId, "ct_mgt_wg_allocation_rules_war_rou_id_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
            entity.Property(e => e.WarAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("war_aud_datetime");
            entity.Property(e => e.WarAudId).HasColumnName("war_aud_id");
            entity.Property(e => e.WarAudType).HasColumnName("war_aud_type");
            entity.Property(e => e.WarCurrentModifiedDate).HasColumnName("war_current_modified_date");
            entity.Property(e => e.WarCurrentPid)
                .HasPrecision(3)
                .HasColumnName("war_current_pid");
            entity.Property(e => e.WarCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("war_current_status");
            entity.Property(e => e.WarCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("war_current_user");
            entity.Property(e => e.WarId)
                .HasPrecision(12)
                .HasColumnName("war_id");
            entity.Property(e => e.WarPriority)
                .HasPrecision(3)
                .HasColumnName("war_priority");
            entity.Property(e => e.WarRouId)
                .HasPrecision(12)
                .HasColumnName("war_rou_id");
            entity.Property(e => e.WarRule)
                .HasMaxLength(100)
                .HasColumnName("war_rule");
            entity.Property(e => e.WarRuleFormula)
                .HasMaxLength(100)
                .HasColumnName("war_rule_formula");
            entity.Property(e => e.WarSuspenseType)
                .HasMaxLength(1)
                .HasColumnName("war_suspense_type");
            entity.Property(e => e.WarVersion)
                .HasPrecision(6)
                .HasColumnName("war_version");
        });

        modelBuilder.Entity<CtMhsToCph>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_mhs_to_cph_pkey");

            entity.ToTable("ct_mhs_to_cph", "cts_transactions");

            entity.HasIndex(e => e.CphAudDatetime, "ct_mhs_to_cph_aud_datetime_idx");

            entity.HasIndex(e => e.CphAudId, "ct_mhs_to_cph_aud_id_idx");

            entity.HasIndex(e => new { e.CphAudType, e.CphAudDatetime }, "ct_mhs_to_cph_aud_type_datetime_idx");

            entity.HasIndex(e => e.CphAudType, "ct_mhs_to_cph_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_mhs_to_cph_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_mhs_to_cph_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_mhs_to_cph_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_mhs_to_cph_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_mhs_to_cph_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_mhs_to_cph_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_mhs_to_cph_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_mhs_to_cph_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.Cph)
                .HasMaxLength(14)
                .HasColumnName("cph");
            entity.Property(e => e.CphAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("cph_aud_datetime");
            entity.Property(e => e.CphAudId).HasColumnName("cph_aud_id");
            entity.Property(e => e.CphAudType).HasColumnName("cph_aud_type");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.MhsNumber)
                .HasPrecision(4)
                .HasColumnName("mhs_number");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtMovHst>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_mov_hst_pkey");

            entity.ToTable("ct_mov_hst", "cts_transactions");

            entity.HasIndex(e => e.HstAudDatetime, "ct_mov_hst_aud_datetime_idx");

            entity.HasIndex(e => e.HstAudId, "ct_mov_hst_aud_id_idx");

            entity.HasIndex(e => new { e.HstAudType, e.HstAudDatetime }, "ct_mov_hst_aud_type_datetime_idx");

            entity.HasIndex(e => e.HstAudType, "ct_mov_hst_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_mov_hst_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_mov_hst_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_mov_hst_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_mov_hst_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_mov_hst_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_mov_hst_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_mov_hst_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_mov_hst_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.HstAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("hst_aud_datetime");
            entity.Property(e => e.HstAudId).HasColumnName("hst_aud_id");
            entity.Property(e => e.HstAudType).HasColumnName("hst_aud_type");
            entity.Property(e => e.HstKey).HasColumnName("hst_key");
            entity.Property(e => e.HstLkey).HasColumnName("hst_lkey");
            entity.Property(e => e.HstOffdate).HasColumnName("hst_offdate");
            entity.Property(e => e.HstOffkey).HasColumnName("hst_offkey");
            entity.Property(e => e.HstOffsource)
                .HasMaxLength(3)
                .HasColumnName("hst_offsource");
            entity.Property(e => e.HstOfftype).HasColumnName("hst_offtype");
            entity.Property(e => e.HstOndate).HasColumnName("hst_ondate");
            entity.Property(e => e.HstOnkey).HasColumnName("hst_onkey");
            entity.Property(e => e.HstOnsource)
                .HasMaxLength(3)
                .HasColumnName("hst_onsource");
            entity.Property(e => e.HstOntype).HasColumnName("hst_ontype");
            entity.Property(e => e.HstPairind)
                .HasMaxLength(1)
                .HasColumnName("hst_pairind");
            entity.Property(e => e.HstSplitflg)
                .HasMaxLength(1)
                .HasColumnName("hst_splitflg");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtMovtCorrSummError>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_movt_corr_summ_errors_pkey");

            entity.ToTable("ct_movt_corr_summ_errors", "cts_transactions");

            entity.HasIndex(e => e.MseAudDatetime, "ct_movt_corr_summ_errors_aud_datetime_idx");

            entity.HasIndex(e => e.MseAudId, "ct_movt_corr_summ_errors_aud_id_idx");

            entity.HasIndex(e => new { e.MseAudType, e.MseAudDatetime }, "ct_movt_corr_summ_errors_aud_type_datetime_idx");

            entity.HasIndex(e => e.MseAudType, "ct_movt_corr_summ_errors_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_movt_corr_summ_errors_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_movt_corr_summ_errors_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_movt_corr_summ_errors_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_movt_corr_summ_errors_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_movt_corr_summ_errors_imported_date_idx");

            entity.HasIndex(e => e.MseMcsId, "ct_movt_corr_summ_errors_mse_mcs_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_movt_corr_summ_errors_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_movt_corr_summ_errors_record_type_idx");

            entity.HasIndex(e => e.MseId, "ct_movt_corr_summ_errors_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_movt_corr_summ_errors_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.MseAttributeName)
                .HasMaxLength(30)
                .HasColumnName("mse_attribute_name");
            entity.Property(e => e.MseAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("mse_aud_datetime");
            entity.Property(e => e.MseAudId).HasColumnName("mse_aud_id");
            entity.Property(e => e.MseAudType).HasColumnName("mse_aud_type");
            entity.Property(e => e.MseCurrentModifiedDate).HasColumnName("mse_current_modified_date");
            entity.Property(e => e.MseCurrentPid)
                .HasPrecision(12)
                .HasColumnName("mse_current_pid");
            entity.Property(e => e.MseCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("mse_current_status");
            entity.Property(e => e.MseCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("mse_current_user");
            entity.Property(e => e.MseErrorCode)
                .HasMaxLength(10)
                .HasColumnName("mse_error_code");
            entity.Property(e => e.MseId)
                .HasPrecision(12)
                .HasColumnName("mse_id");
            entity.Property(e => e.MseMcsId)
                .HasPrecision(12)
                .HasColumnName("mse_mcs_id");
            entity.Property(e => e.MseVersion)
                .HasPrecision(6)
                .HasColumnName("mse_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtMovtCorrectSummary>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_movt_correct_summaries_pkey");

            entity.ToTable("ct_movt_correct_summaries", "cts_transactions");

            entity.HasIndex(e => e.McsAudDatetime, "ct_movt_correct_summaries_aud_datetime_idx");

            entity.HasIndex(e => e.McsAudId, "ct_movt_correct_summaries_aud_id_idx");

            entity.HasIndex(e => new { e.McsAudType, e.McsAudDatetime }, "ct_movt_correct_summaries_aud_type_datetime_idx");

            entity.HasIndex(e => e.McsAudType, "ct_movt_correct_summaries_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_movt_correct_summaries_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_movt_correct_summaries_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_movt_correct_summaries_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_movt_correct_summaries_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_movt_correct_summaries_imported_date_idx");

            entity.HasIndex(e => e.McsMovId, "ct_movt_correct_summaries_mcs_mov_id_idx");

            entity.HasIndex(e => e.McsRmoId, "ct_movt_correct_summaries_mcs_rmo_id_idx");

            entity.HasIndex(e => e.McsSmoId, "ct_movt_correct_summaries_mcs_smo_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_movt_correct_summaries_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_movt_correct_summaries_record_type_idx");

            entity.HasIndex(e => e.McsId, "ct_movt_correct_summaries_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_movt_correct_summaries_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.McsAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("mcs_aud_datetime");
            entity.Property(e => e.McsAudId).HasColumnName("mcs_aud_id");
            entity.Property(e => e.McsAudType).HasColumnName("mcs_aud_type");
            entity.Property(e => e.McsCurrentModifiedDate).HasColumnName("mcs_current_modified_date");
            entity.Property(e => e.McsCurrentPid)
                .HasPrecision(12)
                .HasColumnName("mcs_current_pid");
            entity.Property(e => e.McsCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("mcs_current_status");
            entity.Property(e => e.McsCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("mcs_current_user");
            entity.Property(e => e.McsId)
                .HasPrecision(12)
                .HasColumnName("mcs_id");
            entity.Property(e => e.McsInitEartag)
                .HasMaxLength(14)
                .HasColumnName("mcs_init_eartag");
            entity.Property(e => e.McsInitEidReported)
                .HasMaxLength(20)
                .HasColumnName("mcs_init_eid_reported");
            entity.Property(e => e.McsInitKillNumber)
                .HasMaxLength(20)
                .HasColumnName("mcs_init_kill_number");
            entity.Property(e => e.McsInitLocIdentifier)
                .HasMaxLength(30)
                .HasColumnName("mcs_init_loc_identifier");
            entity.Property(e => e.McsInitLocType)
                .HasMaxLength(2)
                .HasColumnName("mcs_init_loc_type");
            entity.Property(e => e.McsInitMovementDate)
                .HasMaxLength(20)
                .HasColumnName("mcs_init_movement_date");
            entity.Property(e => e.McsInitMovementRcvdDate)
                .HasMaxLength(20)
                .HasColumnName("mcs_init_movement_rcvd_date");
            entity.Property(e => e.McsInitMovementType)
                .HasMaxLength(2)
                .HasColumnName("mcs_init_movement_type");
            entity.Property(e => e.McsInitOriginator)
                .HasMaxLength(7)
                .HasColumnName("mcs_init_originator");
            entity.Property(e => e.McsInitOriginatorsReference)
                .HasMaxLength(12)
                .HasColumnName("mcs_init_originators_reference");
            entity.Property(e => e.McsInitPurposeCode)
                .HasMaxLength(1)
                .HasColumnName("mcs_init_purpose_code");
            entity.Property(e => e.McsInitSublocIdentifier)
                .HasMaxLength(30)
                .HasColumnName("mcs_init_subloc_identifier");
            entity.Property(e => e.McsInitSuspenseReason)
                .HasMaxLength(60)
                .HasColumnName("mcs_init_suspense_reason");
            entity.Property(e => e.McsInitWorkgroup)
                .HasMaxLength(6)
                .HasColumnName("mcs_init_workgroup");
            entity.Property(e => e.McsInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("mcs_interface_file_name");
            entity.Property(e => e.McsInterfaceFileTxn)
                .HasPrecision(4)
                .HasColumnName("mcs_interface_file_txn");
            entity.Property(e => e.McsMovId)
                .HasPrecision(12)
                .HasColumnName("mcs_mov_id");
            entity.Property(e => e.McsOrigInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("mcs_orig_interface_file_name");
            entity.Property(e => e.McsOrigInterfaceFileTxn)
                .HasPrecision(4)
                .HasColumnName("mcs_orig_interface_file_txn");
            entity.Property(e => e.McsRmoId)
                .HasPrecision(12)
                .HasColumnName("mcs_rmo_id");
            entity.Property(e => e.McsSmoId)
                .HasPrecision(12)
                .HasColumnName("mcs_smo_id");
            entity.Property(e => e.McsSmoOrRmoInd)
                .HasMaxLength(3)
                .HasColumnName("mcs_smo_or_rmo_ind");
            entity.Property(e => e.McsSourceType)
                .HasMaxLength(3)
                .HasColumnName("mcs_source_type");
            entity.Property(e => e.McsSubmitAmendmentReason)
                .HasMaxLength(60)
                .HasColumnName("mcs_submit_amendment_reason");
            entity.Property(e => e.McsSubmitDate).HasColumnName("mcs_submit_date");
            entity.Property(e => e.McsSubmitPurposeCode)
                .HasMaxLength(1)
                .HasColumnName("mcs_submit_purpose_code");
            entity.Property(e => e.McsSubmitStatus)
                .HasMaxLength(20)
                .HasColumnName("mcs_submit_status");
            entity.Property(e => e.McsSubmitUser)
                .HasMaxLength(10)
                .HasColumnName("mcs_submit_user");
            entity.Property(e => e.McsSubmitWorkgroup)
                .HasMaxLength(6)
                .HasColumnName("mcs_submit_workgroup");
            entity.Property(e => e.McsSuspenseDatetime).HasColumnName("mcs_suspense_datetime");
            entity.Property(e => e.McsVersion)
                .HasPrecision(6)
                .HasColumnName("mcs_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtMsgtxt>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_msgtxt_transactions_pkey");

            entity.ToTable("ct_msgtxt", "cts_transactions");

            entity.HasIndex(e => e.MsgAudDatetime, "ct_msgtxt_aud_datetime_idx");

            entity.HasIndex(e => e.MsgAudId, "ct_msgtxt_aud_id_idx");

            entity.HasIndex(e => new { e.MsgAudType, e.MsgAudDatetime }, "ct_msgtxt_aud_type_datetime_idx");

            entity.HasIndex(e => e.MsgAudType, "ct_msgtxt_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_msgtxt_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_msgtxt_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_msgtxt_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_msgtxt_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_msgtxt_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_msgtxt_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_msgtxt_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.MsgAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("msg_aud_datetime");
            entity.Property(e => e.MsgAudId).HasColumnName("msg_aud_id");
            entity.Property(e => e.MsgAudType).HasColumnName("msg_aud_type");
            entity.Property(e => e.MsgId)
                .HasMaxLength(5)
                .HasColumnName("msg_id");
            entity.Property(e => e.MsgText)
                .HasMaxLength(1000)
                .HasColumnName("msg_text");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtNonWorkingDay>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_non_working_days_transactions_pkey");

            entity.ToTable("ct_non_working_days", "cts_transactions");

            entity.HasIndex(e => e.NwdAudDatetime, "ct_non_working_days_aud_datetime_idx");

            entity.HasIndex(e => e.NwdAudId, "ct_non_working_days_aud_id_idx");

            entity.HasIndex(e => new { e.NwdAudType, e.NwdAudDatetime }, "ct_non_working_days_aud_type_datetime_idx");

            entity.HasIndex(e => e.NwdAudType, "ct_non_working_days_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_non_working_days_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_non_working_days_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_non_working_days_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_non_working_days_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_non_working_days_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_non_working_days_record_type_idx");

            entity.HasIndex(e => e.NwdId, "ct_non_working_days_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_non_working_days_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.NwdAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("nwd_aud_datetime");
            entity.Property(e => e.NwdAudId).HasColumnName("nwd_aud_id");
            entity.Property(e => e.NwdAudType).HasColumnName("nwd_aud_type");
            entity.Property(e => e.NwdCurrentModifiedDate).HasColumnName("nwd_current_modified_date");
            entity.Property(e => e.NwdCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("nwd_current_status");
            entity.Property(e => e.NwdCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("nwd_current_user");
            entity.Property(e => e.NwdDate).HasColumnName("nwd_date");
            entity.Property(e => e.NwdDescription)
                .HasMaxLength(50)
                .HasColumnName("nwd_description");
            entity.Property(e => e.NwdId)
                .HasPrecision(12)
                .HasColumnName("nwd_id");
            entity.Property(e => e.NwdPid)
                .HasPrecision(3)
                .HasColumnName("nwd_pid");
            entity.Property(e => e.NwdVersion)
                .HasPrecision(6)
                .HasColumnName("nwd_version");
            entity.Property(e => e.NwdYear)
                .HasPrecision(4)
                .HasColumnName("nwd_year");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtParamGroup>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_param_group_transactions_pkey");

            entity.ToTable("ct_param_group", "cts_transactions");

            entity.HasIndex(e => e.PgpAudDatetime, "ct_param_group_aud_datetime_idx");

            entity.HasIndex(e => e.PgpAudId, "ct_param_group_aud_id_idx");

            entity.HasIndex(e => new { e.PgpAudType, e.PgpAudDatetime }, "ct_param_group_aud_type_datetime_idx");

            entity.HasIndex(e => e.PgpAudType, "ct_param_group_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_param_group_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_param_group_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_param_group_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_param_group_imported_date_idx");

            entity.HasIndex(e => e.PgpPhdId, "ct_param_group_pgp_phd_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_param_group_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_param_group_record_type_idx");

            entity.HasIndex(e => e.PgpId, "ct_param_group_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_param_group_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.PgpAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("pgp_aud_datetime");
            entity.Property(e => e.PgpAudId).HasColumnName("pgp_aud_id");
            entity.Property(e => e.PgpAudType).HasColumnName("pgp_aud_type");
            entity.Property(e => e.PgpCurrentModifiedDate).HasColumnName("pgp_current_modified_date");
            entity.Property(e => e.PgpCurrentPid)
                .HasPrecision(3)
                .HasColumnName("pgp_current_pid");
            entity.Property(e => e.PgpCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("pgp_current_status");
            entity.Property(e => e.PgpCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("pgp_current_user");
            entity.Property(e => e.PgpGroupValue)
                .HasMaxLength(15)
                .HasColumnName("pgp_group_value");
            entity.Property(e => e.PgpId)
                .HasPrecision(12)
                .HasColumnName("pgp_id");
            entity.Property(e => e.PgpLongDesc)
                .HasMaxLength(100)
                .HasColumnName("pgp_long_desc");
            entity.Property(e => e.PgpParam)
                .HasMaxLength(30)
                .HasColumnName("pgp_param");
            entity.Property(e => e.PgpPhdId)
                .HasPrecision(12)
                .HasColumnName("pgp_phd_id");
            entity.Property(e => e.PgpShortDesc)
                .HasMaxLength(30)
                .HasColumnName("pgp_short_desc");
            entity.Property(e => e.PgpVersion)
                .HasPrecision(6)
                .HasColumnName("pgp_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtParamHeader>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_param_header_transactions_pkey");

            entity.ToTable("ct_param_header", "cts_transactions");

            entity.HasIndex(e => e.PhdAudDatetime, "ct_param_header_aud_datetime_idx");

            entity.HasIndex(e => e.PhdAudId, "ct_param_header_aud_id_idx");

            entity.HasIndex(e => new { e.PhdAudType, e.PhdAudDatetime }, "ct_param_header_aud_type_datetime_idx");

            entity.HasIndex(e => e.PhdAudType, "ct_param_header_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_param_header_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_param_header_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_param_header_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_param_header_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_param_header_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_param_header_record_type_idx");

            entity.HasIndex(e => e.PhdId, "ct_param_header_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_param_header_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.PhdAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("phd_aud_datetime");
            entity.Property(e => e.PhdAudId).HasColumnName("phd_aud_id");
            entity.Property(e => e.PhdAudType).HasColumnName("phd_aud_type");
            entity.Property(e => e.PhdCurrentModifiedDate).HasColumnName("phd_current_modified_date");
            entity.Property(e => e.PhdCurrentPid)
                .HasPrecision(3)
                .HasColumnName("phd_current_pid");
            entity.Property(e => e.PhdCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("phd_current_status");
            entity.Property(e => e.PhdCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("phd_current_user");
            entity.Property(e => e.PhdDontCache)
                .HasMaxLength(1)
                .HasColumnName("phd_dont_cache");
            entity.Property(e => e.PhdId)
                .HasPrecision(12)
                .HasColumnName("phd_id");
            entity.Property(e => e.PhdLongDesc)
                .HasMaxLength(60)
                .HasColumnName("phd_long_desc");
            entity.Property(e => e.PhdParam)
                .HasMaxLength(30)
                .HasColumnName("phd_param");
            entity.Property(e => e.PhdShortDesc)
                .HasMaxLength(20)
                .HasColumnName("phd_short_desc");
            entity.Property(e => e.PhdUseShort)
                .HasMaxLength(1)
                .HasColumnName("phd_use_short");
            entity.Property(e => e.PhdVersion)
                .HasPrecision(6)
                .HasColumnName("phd_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtParamValue>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_param_value_transactions_pkey");

            entity.ToTable("ct_param_value", "cts_transactions");

            entity.HasIndex(e => e.PvlAudDatetime, "ct_param_value_aud_datetime_idx");

            entity.HasIndex(e => e.PvlAudId, "ct_param_value_aud_id_idx");

            entity.HasIndex(e => new { e.PvlAudType, e.PvlAudDatetime }, "ct_param_value_aud_type_datetime_idx");

            entity.HasIndex(e => e.PvlAudType, "ct_param_value_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_param_value_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_param_value_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_param_value_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_param_value_imported_date_idx");

            entity.HasIndex(e => e.PvlPhdId, "ct_param_value_pvl_phd_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_param_value_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_param_value_record_type_idx");

            entity.HasIndex(e => e.PvlId, "ct_param_value_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_param_value_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.PvlAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("pvl_aud_datetime");
            entity.Property(e => e.PvlAudId).HasColumnName("pvl_aud_id");
            entity.Property(e => e.PvlAudType).HasColumnName("pvl_aud_type");
            entity.Property(e => e.PvlCurrentModifiedDate).HasColumnName("pvl_current_modified_date");
            entity.Property(e => e.PvlCurrentPid)
                .HasPrecision(3)
                .HasColumnName("pvl_current_pid");
            entity.Property(e => e.PvlCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("pvl_current_status");
            entity.Property(e => e.PvlCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("pvl_current_user");
            entity.Property(e => e.PvlId)
                .HasPrecision(12)
                .HasColumnName("pvl_id");
            entity.Property(e => e.PvlParam)
                .HasMaxLength(30)
                .HasColumnName("pvl_param");
            entity.Property(e => e.PvlParamLongDesc)
                .HasMaxLength(100)
                .HasColumnName("pvl_param_long_desc");
            entity.Property(e => e.PvlParamShortDesc)
                .HasMaxLength(20)
                .HasColumnName("pvl_param_short_desc");
            entity.Property(e => e.PvlParamValue)
                .HasMaxLength(30)
                .HasColumnName("pvl_param_value");
            entity.Property(e => e.PvlPhdId)
                .HasPrecision(12)
                .HasColumnName("pvl_phd_id");
            entity.Property(e => e.PvlSequence)
                .HasPrecision(4)
                .HasColumnName("pvl_sequence");
            entity.Property(e => e.PvlVersion)
                .HasPrecision(6)
                .HasColumnName("pvl_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtParamValueGroup>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_param_value_group_transactions_pkey");

            entity.ToTable("ct_param_value_group", "cts_transactions");

            entity.HasIndex(e => e.PvgAudDatetime, "ct_param_value_group_aud_datetime_idx");

            entity.HasIndex(e => e.PvgAudId, "ct_param_value_group_aud_id_idx");

            entity.HasIndex(e => new { e.PvgAudType, e.PvgAudDatetime }, "ct_param_value_group_aud_type_datetime_idx");

            entity.HasIndex(e => e.PvgAudType, "ct_param_value_group_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_param_value_group_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_param_value_group_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_param_value_group_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_param_value_group_imported_date_idx");

            entity.HasIndex(e => e.PvgPgpId, "ct_param_value_group_pvg_pgp_id_idx");

            entity.HasIndex(e => e.PvgPvlId, "ct_param_value_group_pvg_pvl_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_param_value_group_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_param_value_group_record_type_idx");

            entity.HasIndex(e => e.PvgId, "ct_param_value_group_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_param_value_group_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.PvgAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("pvg_aud_datetime");
            entity.Property(e => e.PvgAudId).HasColumnName("pvg_aud_id");
            entity.Property(e => e.PvgAudType).HasColumnName("pvg_aud_type");
            entity.Property(e => e.PvgCurrentModifiedDate).HasColumnName("pvg_current_modified_date");
            entity.Property(e => e.PvgCurrentPid)
                .HasPrecision(3)
                .HasColumnName("pvg_current_pid");
            entity.Property(e => e.PvgCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("pvg_current_status");
            entity.Property(e => e.PvgCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("pvg_current_user");
            entity.Property(e => e.PvgGroupValue)
                .HasMaxLength(15)
                .HasColumnName("pvg_group_value");
            entity.Property(e => e.PvgId)
                .HasPrecision(12)
                .HasColumnName("pvg_id");
            entity.Property(e => e.PvgParam)
                .HasMaxLength(30)
                .HasColumnName("pvg_param");
            entity.Property(e => e.PvgParamValue)
                .HasMaxLength(30)
                .HasColumnName("pvg_param_value");
            entity.Property(e => e.PvgPgpId)
                .HasPrecision(12)
                .HasColumnName("pvg_pgp_id");
            entity.Property(e => e.PvgPvlId)
                .HasPrecision(12)
                .HasColumnName("pvg_pvl_id");
            entity.Property(e => e.PvgVersion)
                .HasPrecision(6)
                .HasColumnName("pvg_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtPartiesFaker>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_parties_faker_pkey");

            entity.ToTable("ct_parties_faker", "cts_transactions");

            entity.HasIndex(e => e.ParAudDatetime, "ct_parties_faker_aud_datetime_idx");

            entity.HasIndex(e => e.ParAudId, "ct_parties_faker_aud_id_idx");

            entity.HasIndex(e => new { e.ParAudType, e.ParAudDatetime }, "ct_parties_faker_aud_type_datetime_idx");

            entity.HasIndex(e => e.ParAudType, "ct_parties_faker_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_parties_faker_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_parties_faker_file_trans_type_idx");

            entity.HasIndex(e => e.CtsFileImportId, "ct_parties_faker_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_parties_faker_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_parties_faker_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_parties_faker_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_parties_faker_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.ParAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("par_aud_datetime");
            entity.Property(e => e.ParAudId).HasColumnName("par_aud_id");
            entity.Property(e => e.ParAudType).HasColumnName("par_aud_type");
            entity.Property(e => e.ParEmailAddress).HasColumnName("par_email_address");
            entity.Property(e => e.ParInitials).HasColumnName("par_initials");
            entity.Property(e => e.ParMobileNumber).HasColumnName("par_mobile_number");
            entity.Property(e => e.ParSurname).HasColumnName("par_surname");
            entity.Property(e => e.ParTelNumber).HasColumnName("par_tel_number");
            entity.Property(e => e.ParTitle).HasColumnName("par_title");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtParty>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_parties_pkey");

            entity.ToTable("ct_parties", "cts_transactions");

            entity.HasIndex(e => e.ParAudDatetime, "ct_parties_aud_datetime_idx");

            entity.HasIndex(e => e.ParAudId, "ct_parties_aud_id_idx");

            entity.HasIndex(e => new { e.ParAudType, e.ParAudDatetime }, "ct_parties_aud_type_datetime_idx");

            entity.HasIndex(e => e.ParAudType, "ct_parties_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_parties_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_parties_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_parties_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_parties_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_parties_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_parties_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_parties_record_type_idx");

            entity.HasIndex(e => e.ParId, "ct_parties_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_parties_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.ParAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("par_aud_datetime");
            entity.Property(e => e.ParAudId).HasColumnName("par_aud_id");
            entity.Property(e => e.ParAudType).HasColumnName("par_aud_type");
            entity.Property(e => e.ParCessationReason)
                .HasMaxLength(2)
                .HasColumnName("par_cessation_reason");
            entity.Property(e => e.ParComments)
                .HasMaxLength(400)
                .HasColumnName("par_comments");
            entity.Property(e => e.ParCurrentModifiedDate).HasColumnName("par_current_modified_date");
            entity.Property(e => e.ParCurrentPid)
                .HasPrecision(12)
                .HasColumnName("par_current_pid");
            entity.Property(e => e.ParCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("par_current_status");
            entity.Property(e => e.ParCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("par_current_user");
            entity.Property(e => e.ParEffectiveFromDate).HasColumnName("par_effective_from_date");
            entity.Property(e => e.ParEffectiveToDate).HasColumnName("par_effective_to_date");
            entity.Property(e => e.ParEmailAddress)
                .HasMaxLength(50)
                .HasColumnName("par_email_address");
            entity.Property(e => e.ParFaxNumber)
                .HasMaxLength(25)
                .HasColumnName("par_fax_number");
            entity.Property(e => e.ParId)
                .HasPrecision(12)
                .HasColumnName("par_id");
            entity.Property(e => e.ParInitials)
                .HasMaxLength(12)
                .HasColumnName("par_initials");
            entity.Property(e => e.ParMobileNumber)
                .HasMaxLength(25)
                .HasColumnName("par_mobile_number");
            entity.Property(e => e.ParSurname)
                .HasMaxLength(30)
                .HasColumnName("par_surname");
            entity.Property(e => e.ParTelNumber)
                .HasMaxLength(25)
                .HasColumnName("par_tel_number");
            entity.Property(e => e.ParTitle)
                .HasMaxLength(10)
                .HasColumnName("par_title");
            entity.Property(e => e.ParVersion)
                .HasPrecision(6)
                .HasColumnName("par_version");
            entity.Property(e => e.ParWelshIndicator)
                .HasMaxLength(1)
                .HasColumnName("par_welsh_indicator");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtPpafGrouping>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_ppaf_groupings_pkey");

            entity.ToTable("ct_ppaf_groupings", "cts_transactions");

            entity.HasIndex(e => e.PpgAudDatetime, "ct_ppaf_groupings_aud_datetime_idx");

            entity.HasIndex(e => e.PpgAudId, "ct_ppaf_groupings_aud_id_idx");

            entity.HasIndex(e => new { e.PpgAudType, e.PpgAudDatetime }, "ct_ppaf_groupings_aud_type_datetime_idx");

            entity.HasIndex(e => e.PpgAudType, "ct_ppaf_groupings_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_ppaf_groupings_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ppaf_groupings_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_ppaf_groupings_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ppaf_groupings_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_ppaf_groupings_imported_date_idx");

            entity.HasIndex(e => e.PpgLocIdBirth, "ct_ppaf_groupings_ppg_loc_id_birth_idx");

            entity.HasIndex(e => e.PpgLocIdCorres, "ct_ppaf_groupings_ppg_loc_id_corres_idx");

            entity.HasIndex(e => e.RecordCount, "ct_ppaf_groupings_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_ppaf_groupings_record_type_idx");

            entity.HasIndex(e => e.PpgId, "ct_ppaf_groupings_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_ppaf_groupings_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.PpgAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("ppg_aud_datetime");
            entity.Property(e => e.PpgAudId).HasColumnName("ppg_aud_id");
            entity.Property(e => e.PpgAudType).HasColumnName("ppg_aud_type");
            entity.Property(e => e.PpgBirthLocationRepd)
                .HasMaxLength(17)
                .HasColumnName("ppg_birth_location_repd");
            entity.Property(e => e.PpgCorresLocationRepd)
                .HasMaxLength(17)
                .HasColumnName("ppg_corres_location_repd");
            entity.Property(e => e.PpgCurrentModifiedDate).HasColumnName("ppg_current_modified_date");
            entity.Property(e => e.PpgCurrentPid)
                .HasPrecision(3)
                .HasColumnName("ppg_current_pid");
            entity.Property(e => e.PpgCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("ppg_current_status");
            entity.Property(e => e.PpgCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("ppg_current_user");
            entity.Property(e => e.PpgFormIdentifier)
                .HasMaxLength(30)
                .HasColumnName("ppg_form_identifier");
            entity.Property(e => e.PpgId)
                .HasPrecision(12)
                .HasColumnName("ppg_id");
            entity.Property(e => e.PpgInterfaceFilename)
                .HasMaxLength(25)
                .HasColumnName("ppg_interface_filename");
            entity.Property(e => e.PpgInterfaceTxnNumber)
                .HasPrecision(10)
                .HasColumnName("ppg_interface_txn_number");
            entity.Property(e => e.PpgLocIdBirth)
                .HasPrecision(12)
                .HasColumnName("ppg_loc_id_birth");
            entity.Property(e => e.PpgLocIdCorres)
                .HasPrecision(12)
                .HasColumnName("ppg_loc_id_corres");
            entity.Property(e => e.PpgPpafAddedDate).HasColumnName("ppg_ppaf_added_date");
            entity.Property(e => e.PpgPrintingDate).HasColumnName("ppg_printing_date");
            entity.Property(e => e.PpgVersion)
                .HasPrecision(6)
                .HasColumnName("ppg_version");
            entity.Property(e => e.PpgWelshIndicator)
                .HasMaxLength(1)
                .HasColumnName("ppg_welsh_indicator");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtPreprintedAppnForm>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_preprinted_appn_forms_pkey");

            entity.ToTable("ct_preprinted_appn_forms", "cts_transactions");

            entity.HasIndex(e => e.PafAudDatetime, "ct_preprinted_appn_forms_aud_datetime_idx");

            entity.HasIndex(e => e.PafAudId, "ct_preprinted_appn_forms_aud_id_idx");

            entity.HasIndex(e => new { e.PafAudType, e.PafAudDatetime }, "ct_preprinted_appn_forms_aud_type_datetime_idx");

            entity.HasIndex(e => e.PafAudType, "ct_preprinted_appn_forms_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_preprinted_appn_forms_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_preprinted_appn_forms_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_preprinted_appn_forms_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_preprinted_appn_forms_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_preprinted_appn_forms_imported_date_idx");

            entity.HasIndex(e => e.PafEtgId, "ct_preprinted_appn_forms_paf_etg_id_idx");

            entity.HasIndex(e => e.PafPpgId, "ct_preprinted_appn_forms_paf_ppg_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_preprinted_appn_forms_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_preprinted_appn_forms_record_type_idx");

            entity.HasIndex(e => e.PafId, "ct_preprinted_appn_forms_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_preprinted_appn_forms_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.PafAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("paf_aud_datetime");
            entity.Property(e => e.PafAudId).HasColumnName("paf_aud_id");
            entity.Property(e => e.PafAudType).HasColumnName("paf_aud_type");
            entity.Property(e => e.PafCurrentModifiedDate).HasColumnName("paf_current_modified_date");
            entity.Property(e => e.PafCurrentPid)
                .HasPrecision(3)
                .HasColumnName("paf_current_pid");
            entity.Property(e => e.PafCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("paf_current_status");
            entity.Property(e => e.PafCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("paf_current_user");
            entity.Property(e => e.PafDateIssued).HasColumnName("paf_date_issued");
            entity.Property(e => e.PafEtgId)
                .HasPrecision(12)
                .HasColumnName("paf_etg_id");
            entity.Property(e => e.PafId)
                .HasPrecision(12)
                .HasColumnName("paf_id");
            entity.Property(e => e.PafInterfaceFilename)
                .HasMaxLength(25)
                .HasColumnName("paf_interface_filename");
            entity.Property(e => e.PafInterfaceTxnNumber)
                .HasPrecision(10)
                .HasColumnName("paf_interface_txn_number");
            entity.Property(e => e.PafPpgId)
                .HasPrecision(12)
                .HasColumnName("paf_ppg_id");
            entity.Property(e => e.PafReasonForIssue)
                .HasMaxLength(1)
                .HasColumnName("paf_reason_for_issue");
            entity.Property(e => e.PafVersion)
                .HasPrecision(6)
                .HasColumnName("paf_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtProbityCheck>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_probity_checks_transactions_pkey");

            entity.ToTable("ct_probity_checks", "cts_transactions");

            entity.HasIndex(e => e.PchAudDatetime, "ct_probity_checks_aud_datetime_idx");

            entity.HasIndex(e => e.PchAudId, "ct_probity_checks_aud_id_idx");

            entity.HasIndex(e => new { e.PchAudType, e.PchAudDatetime }, "ct_probity_checks_aud_type_datetime_idx");

            entity.HasIndex(e => e.PchAudType, "ct_probity_checks_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_probity_checks_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_probity_checks_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_probity_checks_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_probity_checks_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_probity_checks_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_probity_checks_record_type_idx");

            entity.HasIndex(e => e.PchId, "ct_probity_checks_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_probity_checks_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.PchAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("pch_aud_datetime");
            entity.Property(e => e.PchAudId).HasColumnName("pch_aud_id");
            entity.Property(e => e.PchAudType).HasColumnName("pch_aud_type");
            entity.Property(e => e.PchCheckPeriod)
                .HasPrecision(3)
                .HasColumnName("pch_check_period");
            entity.Property(e => e.PchCheckedToDate).HasColumnName("pch_checked_to_date");
            entity.Property(e => e.PchCurrentModifiedDate).HasColumnName("pch_current_modified_date");
            entity.Property(e => e.PchCurrentPid)
                .HasPrecision(3)
                .HasColumnName("pch_current_pid");
            entity.Property(e => e.PchCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("pch_current_status");
            entity.Property(e => e.PchCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("pch_current_user");
            entity.Property(e => e.PchId)
                .HasPrecision(12)
                .HasColumnName("pch_id");
            entity.Property(e => e.PchLongDescription)
                .HasMaxLength(60)
                .HasColumnName("pch_long_description");
            entity.Property(e => e.PchNextCheckDate).HasColumnName("pch_next_check_date");
            entity.Property(e => e.PchShortDescription)
                .HasMaxLength(20)
                .HasColumnName("pch_short_description");
            entity.Property(e => e.PchVersion)
                .HasPrecision(6)
                .HasColumnName("pch_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtPs9999AhdbDatum>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_ps9999_ahdb_data_pkey");

            entity.ToTable("ct_ps9999_ahdb_data", "cts_transactions");

            entity.HasIndex(e => e.RanAudDatetime, "ct_ps9999_ahdb_data_aud_datetime_idx");

            entity.HasIndex(e => e.RanAudId, "ct_ps9999_ahdb_data_aud_id_idx");

            entity.HasIndex(e => new { e.RanAudType, e.RanAudDatetime }, "ct_ps9999_ahdb_data_aud_type_datetime_idx");

            entity.HasIndex(e => e.RanAudType, "ct_ps9999_ahdb_data_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_ps9999_ahdb_data_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ps9999_ahdb_data_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_ps9999_ahdb_data_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ps9999_ahdb_data_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_ps9999_ahdb_data_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_ps9999_ahdb_data_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_ps9999_ahdb_data_record_type_idx");

            entity.HasIndex(e => e.RanId, "ct_ps9999_ahdb_data_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_ps9999_ahdb_data_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.AnimalEartag)
                .HasMaxLength(50)
                .HasColumnName("animal_eartag");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.BreedCode)
                .HasMaxLength(5)
                .HasColumnName("breed_code");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.CurrentCph)
                .HasMaxLength(14)
                .HasColumnName("current_cph");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RanAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("ran_aud_datetime");
            entity.Property(e => e.RanAudId).HasColumnName("ran_aud_id");
            entity.Property(e => e.RanAudType).HasColumnName("ran_aud_type");
            entity.Property(e => e.RanId).HasColumnName("ran_id");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SexOfAnimal)
                .HasMaxLength(1)
                .HasColumnName("sex_of_animal");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtPs9999AhdbMovHistory>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_ps9999_ahdb_mov_history_pkey");

            entity.ToTable("ct_ps9999_ahdb_mov_history", "cts_transactions");

            entity.HasIndex(e => e.LocAudDatetime, "ct_ps9999_ahdb_mov_history_aud_datetime_idx");

            entity.HasIndex(e => e.LocAudId, "ct_ps9999_ahdb_mov_history_aud_id_idx");

            entity.HasIndex(e => new { e.LocAudType, e.LocAudDatetime }, "ct_ps9999_ahdb_mov_history_aud_type_datetime_idx");

            entity.HasIndex(e => e.LocAudType, "ct_ps9999_ahdb_mov_history_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_ps9999_ahdb_mov_history_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ps9999_ahdb_mov_history_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_ps9999_ahdb_mov_history_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_ps9999_ahdb_mov_history_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_ps9999_ahdb_mov_history_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_ps9999_ahdb_mov_history_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_ps9999_ahdb_mov_history_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_ps9999_ahdb_mov_history_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.LocAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("loc_aud_datetime");
            entity.Property(e => e.LocAudId).HasColumnName("loc_aud_id");
            entity.Property(e => e.LocAudType).HasColumnName("loc_aud_type");
            entity.Property(e => e.LocFullIdentifier)
                .HasMaxLength(14)
                .HasColumnName("loc_full_identifier");
            entity.Property(e => e.LocId).HasColumnName("loc_id");
            entity.Property(e => e.OffDate).HasColumnName("off_date");
            entity.Property(e => e.OnDate).HasColumnName("on_date");
            entity.Property(e => e.RanId).HasColumnName("ran_id");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtRecdApplicationError>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_recd_application_errors_pkey");

            entity.ToTable("ct_recd_application_errors", "cts_transactions");

            entity.HasIndex(e => e.RaeAudDatetime, "ct_recd_application_errors_aud_datetime_idx");

            entity.HasIndex(e => e.RaeAudId, "ct_recd_application_errors_aud_id_idx");

            entity.HasIndex(e => new { e.RaeAudType, e.RaeAudDatetime }, "ct_recd_application_errors_aud_type_datetime_idx");

            entity.HasIndex(e => e.RaeAudType, "ct_recd_application_errors_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_recd_application_errors_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_recd_application_errors_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_recd_application_errors_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_recd_application_errors_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_recd_application_errors_imported_date_idx");

            entity.HasIndex(e => e.RaeRapId, "ct_recd_application_errors_rae_rap_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_recd_application_errors_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_recd_application_errors_record_type_idx");

            entity.HasIndex(e => e.RaeId, "ct_recd_application_errors_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_recd_application_errors_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RaeAttributeName)
                .HasMaxLength(30)
                .HasColumnName("rae_attribute_name");
            entity.Property(e => e.RaeAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("rae_aud_datetime");
            entity.Property(e => e.RaeAudId).HasColumnName("rae_aud_id");
            entity.Property(e => e.RaeAudType).HasColumnName("rae_aud_type");
            entity.Property(e => e.RaeCurrentModifiedDate).HasColumnName("rae_current_modified_date");
            entity.Property(e => e.RaeCurrentPid)
                .HasPrecision(3)
                .HasColumnName("rae_current_pid");
            entity.Property(e => e.RaeCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("rae_current_status");
            entity.Property(e => e.RaeCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("rae_current_user");
            entity.Property(e => e.RaeErrorCode)
                .HasMaxLength(4)
                .HasColumnName("rae_error_code");
            entity.Property(e => e.RaeId)
                .HasPrecision(12)
                .HasColumnName("rae_id");
            entity.Property(e => e.RaeRapId)
                .HasPrecision(12)
                .HasColumnName("rae_rap_id");
            entity.Property(e => e.RaeVersion)
                .HasPrecision(6)
                .HasColumnName("rae_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtRecdMovementError>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_recd_movement_errors_pkey");

            entity.ToTable("ct_recd_movement_errors", "cts_transactions");

            entity.HasIndex(e => e.RmeAudDatetime, "ct_recd_movement_errors_aud_datetime_idx");

            entity.HasIndex(e => e.RmeAudId, "ct_recd_movement_errors_aud_id_idx");

            entity.HasIndex(e => new { e.RmeAudType, e.RmeAudDatetime }, "ct_recd_movement_errors_aud_type_datetime_idx");

            entity.HasIndex(e => e.RmeAudType, "ct_recd_movement_errors_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_recd_movement_errors_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_recd_movement_errors_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_recd_movement_errors_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_recd_movement_errors_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_recd_movement_errors_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_recd_movement_errors_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_recd_movement_errors_record_type_idx");

            entity.HasIndex(e => e.RmeRmoId, "ct_recd_movement_errors_rme_rmo_id_idx");

            entity.HasIndex(e => e.RmeId, "ct_recd_movement_errors_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_recd_movement_errors_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RmeAttributeName)
                .HasMaxLength(30)
                .HasColumnName("rme_attribute_name");
            entity.Property(e => e.RmeAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("rme_aud_datetime");
            entity.Property(e => e.RmeAudId).HasColumnName("rme_aud_id");
            entity.Property(e => e.RmeAudType).HasColumnName("rme_aud_type");
            entity.Property(e => e.RmeCurrentModifiedDate).HasColumnName("rme_current_modified_date");
            entity.Property(e => e.RmeCurrentPid)
                .HasPrecision(3)
                .HasColumnName("rme_current_pid");
            entity.Property(e => e.RmeCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("rme_current_status");
            entity.Property(e => e.RmeCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("rme_current_user");
            entity.Property(e => e.RmeErrorCode)
                .HasMaxLength(4)
                .HasColumnName("rme_error_code");
            entity.Property(e => e.RmeId)
                .HasPrecision(12)
                .HasColumnName("rme_id");
            entity.Property(e => e.RmeRmoId)
                .HasPrecision(12)
                .HasColumnName("rme_rmo_id");
            entity.Property(e => e.RmeVersion)
                .HasPrecision(6)
                .HasColumnName("rme_version");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtReceivedApplication>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_received_applications_pkey");

            entity.ToTable("ct_received_applications", "cts_transactions");

            entity.HasIndex(e => e.RapAudDatetime, "ct_received_applications_aud_datetime_idx");

            entity.HasIndex(e => e.RapAudId, "ct_received_applications_aud_id_idx");

            entity.HasIndex(e => new { e.RapAudType, e.RapAudDatetime }, "ct_received_applications_aud_type_datetime_idx");

            entity.HasIndex(e => e.RapAudType, "ct_received_applications_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_received_applications_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_received_applications_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_received_applications_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_received_applications_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_received_applications_imported_date_idx");

            entity.HasIndex(e => e.RapRanIdReserved, "ct_received_applications_rap_ran_id_reserved_idx");

            entity.HasIndex(e => e.RapWgpId, "ct_received_applications_rap_wgp_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_received_applications_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_received_applications_record_type_idx");

            entity.HasIndex(e => e.RapId, "ct_received_applications_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_received_applications_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RapAmendedBy)
                .HasMaxLength(10)
                .HasColumnName("rap_amended_by");
            entity.Property(e => e.RapAmendedDatetime).HasColumnName("rap_amended_datetime");
            entity.Property(e => e.RapApplicReceiptDate)
                .HasMaxLength(20)
                .HasColumnName("rap_applic_receipt_date");
            entity.Property(e => e.RapApplicTargetDate).HasColumnName("rap_applic_target_date");
            entity.Property(e => e.RapApplicationType)
                .HasMaxLength(1)
                .HasColumnName("rap_application_type");
            entity.Property(e => e.RapAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("rap_aud_datetime");
            entity.Property(e => e.RapAudId).HasColumnName("rap_aud_id");
            entity.Property(e => e.RapAudType).HasColumnName("rap_aud_type");
            entity.Property(e => e.RapBirthDate)
                .HasMaxLength(20)
                .HasColumnName("rap_birth_date");
            entity.Property(e => e.RapBreed)
                .HasMaxLength(20)
                .HasColumnName("rap_breed");
            entity.Property(e => e.RapChrCorrectionType)
                .HasMaxLength(1)
                .HasColumnName("rap_chr_correction_type");
            entity.Property(e => e.RapChrLocationInd)
                .HasMaxLength(1)
                .HasColumnName("rap_chr_location_ind");
            entity.Property(e => e.RapCountryOfOrigin)
                .HasMaxLength(2)
                .HasColumnName("rap_country_of_origin");
            entity.Property(e => e.RapCreatedDate).HasColumnName("rap_created_date");
            entity.Property(e => e.RapCtsIndicator)
                .HasMaxLength(1)
                .HasColumnName("rap_cts_indicator");
            entity.Property(e => e.RapCurrentModifiedDate).HasColumnName("rap_current_modified_date");
            entity.Property(e => e.RapCurrentPid)
                .HasPrecision(3)
                .HasColumnName("rap_current_pid");
            entity.Property(e => e.RapCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("rap_current_status");
            entity.Property(e => e.RapCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("rap_current_user");
            entity.Property(e => e.RapEartag)
                .HasMaxLength(30)
                .HasColumnName("rap_eartag");
            entity.Property(e => e.RapEartagType)
                .HasMaxLength(20)
                .HasColumnName("rap_eartag_type");
            entity.Property(e => e.RapElectronicIdentifier)
                .HasMaxLength(20)
                .HasColumnName("rap_electronic_identifier");
            entity.Property(e => e.RapGeneticDamEartag)
                .HasMaxLength(30)
                .HasColumnName("rap_genetic_dam_eartag");
            entity.Property(e => e.RapGeneticDamEtType)
                .HasMaxLength(20)
                .HasColumnName("rap_genetic_dam_et_type");
            entity.Property(e => e.RapHealthCertificateNo)
                .HasMaxLength(30)
                .HasColumnName("rap_health_certificate_no");
            entity.Property(e => e.RapId)
                .HasPrecision(12)
                .HasColumnName("rap_id");
            entity.Property(e => e.RapImportIdentifier)
                .HasMaxLength(20)
                .HasColumnName("rap_import_identifier");
            entity.Property(e => e.RapInitialLocIdentifier)
                .HasMaxLength(30)
                .HasColumnName("rap_initial_loc_identifier");
            entity.Property(e => e.RapInitialLocType)
                .HasMaxLength(2)
                .HasColumnName("rap_initial_loc_type");
            entity.Property(e => e.RapInitialSublocIdentifier)
                .HasMaxLength(30)
                .HasColumnName("rap_initial_subloc_identifier");
            entity.Property(e => e.RapIntendedAction)
                .HasMaxLength(2)
                .HasColumnName("rap_intended_action");
            entity.Property(e => e.RapInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("rap_interface_file_name");
            entity.Property(e => e.RapInterfaceFileTxn)
                .HasPrecision(4)
                .HasColumnName("rap_interface_file_txn");
            entity.Property(e => e.RapNewEartag)
                .HasMaxLength(30)
                .HasColumnName("rap_new_eartag");
            entity.Property(e => e.RapNewEartagType)
                .HasMaxLength(20)
                .HasColumnName("rap_new_eartag_type");
            entity.Property(e => e.RapNumberCalfMovts)
                .HasPrecision(1)
                .HasColumnName("rap_number_calf_movts");
            entity.Property(e => e.RapOrigIfFileName)
                .HasMaxLength(25)
                .HasColumnName("rap_orig_if_file_name");
            entity.Property(e => e.RapOrigIfFileTxn)
                .HasPrecision(4)
                .HasColumnName("rap_orig_if_file_txn");
            entity.Property(e => e.RapOriginator)
                .HasMaxLength(20)
                .HasColumnName("rap_originator");
            entity.Property(e => e.RapPlacementDate)
                .HasMaxLength(20)
                .HasColumnName("rap_placement_date");
            entity.Property(e => e.RapRanIdReserved)
                .HasPrecision(12)
                .HasColumnName("rap_ran_id_reserved");
            entity.Property(e => e.RapRefusedLetter).HasColumnName("rap_refused_letter");
            entity.Property(e => e.RapReminderLetter).HasColumnName("rap_reminder_letter");
            entity.Property(e => e.RapRequestLetter).HasColumnName("rap_request_letter");
            entity.Property(e => e.RapRequestLocIdentifier)
                .HasMaxLength(30)
                .HasColumnName("rap_request_loc_identifier");
            entity.Property(e => e.RapRequestLocType)
                .HasMaxLength(2)
                .HasColumnName("rap_request_loc_type");
            entity.Property(e => e.RapRequestSublocIdentifier)
                .HasMaxLength(30)
                .HasColumnName("rap_request_subloc_identifier");
            entity.Property(e => e.RapSex)
                .HasMaxLength(20)
                .HasColumnName("rap_sex");
            entity.Property(e => e.RapSireEartag)
                .HasMaxLength(30)
                .HasColumnName("rap_sire_eartag");
            entity.Property(e => e.RapSireEtType)
                .HasMaxLength(20)
                .HasColumnName("rap_sire_et_type");
            entity.Property(e => e.RapSourceReference)
                .HasMaxLength(20)
                .HasColumnName("rap_source_reference");
            entity.Property(e => e.RapSourceType)
                .HasMaxLength(3)
                .HasColumnName("rap_source_type");
            entity.Property(e => e.RapSubmitDatetime).HasColumnName("rap_submit_datetime");
            entity.Property(e => e.RapSurrDamEartag)
                .HasMaxLength(30)
                .HasColumnName("rap_surr_dam_eartag");
            entity.Property(e => e.RapSurrDamEtType)
                .HasMaxLength(20)
                .HasColumnName("rap_surr_dam_et_type");
            entity.Property(e => e.RapVersion)
                .HasPrecision(6)
                .HasColumnName("rap_version");
            entity.Property(e => e.RapWgpId)
                .HasPrecision(12)
                .HasColumnName("rap_wgp_id");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtReceivedMovement>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_received_movements_pkey");

            entity.ToTable("ct_received_movements", "cts_transactions");

            entity.HasIndex(e => e.RmoAudDatetime, "ct_received_movements_aud_datetime_idx");

            entity.HasIndex(e => e.RmoAudId, "ct_received_movements_aud_id_idx");

            entity.HasIndex(e => new { e.RmoAudType, e.RmoAudDatetime }, "ct_received_movements_aud_type_datetime_idx");

            entity.HasIndex(e => e.RmoAudType, "ct_received_movements_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_received_movements_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_received_movements_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_received_movements_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_received_movements_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_received_movements_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_received_movements_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_received_movements_record_type_idx");

            entity.HasIndex(e => e.RmoId, "ct_received_movements_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_received_movements_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RmoAmendedBy)
                .HasMaxLength(10)
                .HasColumnName("rmo_amended_by");
            entity.Property(e => e.RmoAmendedDatetime).HasColumnName("rmo_amended_datetime");
            entity.Property(e => e.RmoAmendmentReason)
                .HasMaxLength(2)
                .HasColumnName("rmo_amendment_reason");
            entity.Property(e => e.RmoAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("rmo_aud_datetime");
            entity.Property(e => e.RmoAudId).HasColumnName("rmo_aud_id");
            entity.Property(e => e.RmoAudType).HasColumnName("rmo_aud_type");
            entity.Property(e => e.RmoCreatedDate).HasColumnName("rmo_created_date");
            entity.Property(e => e.RmoCurrentModifiedDate).HasColumnName("rmo_current_modified_date");
            entity.Property(e => e.RmoCurrentPid)
                .HasPrecision(3)
                .HasColumnName("rmo_current_pid");
            entity.Property(e => e.RmoCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("rmo_current_status");
            entity.Property(e => e.RmoCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("rmo_current_user");
            entity.Property(e => e.RmoDirection)
                .HasMaxLength(1)
                .HasColumnName("rmo_direction");
            entity.Property(e => e.RmoEartag)
                .HasMaxLength(20)
                .HasColumnName("rmo_eartag");
            entity.Property(e => e.RmoEidReported)
                .HasMaxLength(20)
                .HasColumnName("rmo_eid_reported");
            entity.Property(e => e.RmoId)
                .HasPrecision(12)
                .HasColumnName("rmo_id");
            entity.Property(e => e.RmoInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("rmo_interface_file_name");
            entity.Property(e => e.RmoInterfaceFileTxn)
                .HasPrecision(4)
                .HasColumnName("rmo_interface_file_txn");
            entity.Property(e => e.RmoKillNumber)
                .HasMaxLength(20)
                .HasColumnName("rmo_kill_number");
            entity.Property(e => e.RmoLocFullIdentifier)
                .HasMaxLength(20)
                .HasColumnName("rmo_loc_full_identifier");
            entity.Property(e => e.RmoMovementDate)
                .HasMaxLength(20)
                .HasColumnName("rmo_movement_date");
            entity.Property(e => e.RmoMovementLocIdentifier)
                .HasMaxLength(30)
                .HasColumnName("rmo_movement_loc_identifier");
            entity.Property(e => e.RmoMovementLocType)
                .HasMaxLength(20)
                .HasColumnName("rmo_movement_loc_type");
            entity.Property(e => e.RmoMovementReceivedDate)
                .HasMaxLength(20)
                .HasColumnName("rmo_movement_received_date");
            entity.Property(e => e.RmoMovementSublocIdentifier)
                .HasMaxLength(30)
                .HasColumnName("rmo_movement_subloc_identifier");
            entity.Property(e => e.RmoMovementType)
                .HasMaxLength(20)
                .HasColumnName("rmo_movement_type");
            entity.Property(e => e.RmoMovtWorkgroup)
                .HasMaxLength(10)
                .HasColumnName("rmo_movt_workgroup");
            entity.Property(e => e.RmoOrigInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("rmo_orig_interface_file_name");
            entity.Property(e => e.RmoOrigInterfaceFileTxn)
                .HasPrecision(4)
                .HasColumnName("rmo_orig_interface_file_txn");
            entity.Property(e => e.RmoOriginator)
                .HasMaxLength(20)
                .HasColumnName("rmo_originator");
            entity.Property(e => e.RmoOriginatorsReference)
                .HasMaxLength(20)
                .HasColumnName("rmo_originators_reference");
            entity.Property(e => e.RmoSourceType)
                .HasMaxLength(3)
                .HasColumnName("rmo_source_type");
            entity.Property(e => e.RmoSubmitDatetime).HasColumnName("rmo_submit_datetime");
            entity.Property(e => e.RmoSuspenseReason)
                .HasMaxLength(2)
                .HasColumnName("rmo_suspense_reason");
            entity.Property(e => e.RmoVersion)
                .HasPrecision(6)
                .HasColumnName("rmo_version");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtRegisteredAnimal>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_registered_animals_pkey");

            entity.ToTable("ct_registered_animals", "cts_transactions");

            entity.HasIndex(e => e.RanAudDatetime, "ct_registered_animals_aud_datetime_idx");

            entity.HasIndex(e => e.RanAudId, "ct_registered_animals_aud_id_idx");

            entity.HasIndex(e => new { e.RanAudType, e.RanAudDatetime }, "ct_registered_animals_aud_type_datetime_idx");

            entity.HasIndex(e => e.RanAudType, "ct_registered_animals_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_registered_animals_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_registered_animals_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_registered_animals_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_registered_animals_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_registered_animals_imported_date_idx");

            entity.HasIndex(e => e.RanBrdId, "ct_registered_animals_ran_brd_id_idx");

            entity.HasIndex(e => e.RanCryIdChrOrigin, "ct_registered_animals_ran_cry_id_chr_origin_idx");

            entity.HasIndex(e => e.RanLocIdPassport, "ct_registered_animals_ran_loc_id_passport_idx");

            entity.HasIndex(e => e.RanVapId, "ct_registered_animals_ran_vap_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_registered_animals_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_registered_animals_record_type_idx");

            entity.HasIndex(e => e.RanId, "ct_registered_animals_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_registered_animals_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.FakeData)
                .HasPrecision(1)
                .HasColumnName("fake_data");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RanApplicLine)
                .HasPrecision(2)
                .HasColumnName("ran_applic_line");
            entity.Property(e => e.RanAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("ran_aud_datetime");
            entity.Property(e => e.RanAudId).HasColumnName("ran_aud_id");
            entity.Property(e => e.RanAudType).HasColumnName("ran_aud_type");
            entity.Property(e => e.RanBirthDate).HasColumnName("ran_birth_date");
            entity.Property(e => e.RanBrdId)
                .HasPrecision(12)
                .HasColumnName("ran_brd_id");
            entity.Property(e => e.RanCryIdChrOrigin)
                .HasPrecision(12)
                .HasColumnName("ran_cry_id_chr_origin");
            entity.Property(e => e.RanCtsIndicator)
                .HasMaxLength(1)
                .HasColumnName("ran_cts_indicator");
            entity.Property(e => e.RanCurrentAddMoves)
                .HasPrecision(4)
                .HasColumnName("ran_current_add_moves");
            entity.Property(e => e.RanCurrentChangeRcvdDate).HasColumnName("ran_current_change_rcvd_date");
            entity.Property(e => e.RanCurrentIntendedAction)
                .HasMaxLength(2)
                .HasColumnName("ran_current_intended_action");
            entity.Property(e => e.RanCurrentModifiedDate).HasColumnName("ran_current_modified_date");
            entity.Property(e => e.RanCurrentPid)
                .HasPrecision(3)
                .HasColumnName("ran_current_pid");
            entity.Property(e => e.RanCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("ran_current_status");
            entity.Property(e => e.RanCurrentTracedMoves)
                .HasPrecision(4)
                .HasColumnName("ran_current_traced_moves");
            entity.Property(e => e.RanCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("ran_current_user");
            entity.Property(e => e.RanId)
                .HasPrecision(12)
                .HasColumnName("ran_id");
            entity.Property(e => e.RanLocIdPassport)
                .HasPrecision(12)
                .HasColumnName("ran_loc_id_passport");
            entity.Property(e => e.RanMovIdDeath)
                .HasPrecision(12)
                .HasColumnName("ran_mov_id_death");
            entity.Property(e => e.RanMovIdRegistration)
                .HasPrecision(12)
                .HasColumnName("ran_mov_id_registration");
            entity.Property(e => e.RanPassportLocationRepd)
                .HasMaxLength(17)
                .HasColumnName("ran_passport_location_repd");
            entity.Property(e => e.RanPassportModFlag)
                .HasMaxLength(1)
                .HasColumnName("ran_passport_mod_flag");
            entity.Property(e => e.RanPassportOrLicence)
                .HasMaxLength(1)
                .HasColumnName("ran_passport_or_licence");
            entity.Property(e => e.RanPassportVersionNumber)
                .HasMaxLength(3)
                .HasColumnName("ran_passport_version_number");
            entity.Property(e => e.RanSex)
                .HasMaxLength(1)
                .HasColumnName("ran_sex");
            entity.Property(e => e.RanVapId)
                .HasPrecision(12)
                .HasColumnName("ran_vap_id");
            entity.Property(e => e.RanVersion)
                .HasPrecision(6)
                .HasColumnName("ran_version");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtRegisteredMovement>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_registered_movements_pkey");

            entity.ToTable("ct_registered_movements", "cts_transactions");

            entity.HasIndex(e => e.MovAudDatetime, "ct_registered_movements_aud_datetime_idx");

            entity.HasIndex(e => e.MovAudId, "ct_registered_movements_aud_id_idx");

            entity.HasIndex(e => new { e.MovAudType, e.MovAudDatetime }, "ct_registered_movements_aud_type_datetime_idx");

            entity.HasIndex(e => e.MovAudType, "ct_registered_movements_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_registered_movements_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_registered_movements_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_registered_movements_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_registered_movements_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_registered_movements_imported_date_idx");

            entity.HasIndex(e => e.MovCryIdImport, "ct_registered_movements_mov_cry_id_import_idx");

            entity.HasIndex(e => e.MovLocId, "ct_registered_movements_mov_loc_id_idx");

            entity.HasIndex(e => e.MovRanId, "ct_registered_movements_mov_ran_id_idx");

            entity.HasIndex(e => e.RecordCount, "ct_registered_movements_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_registered_movements_record_type_idx");

            entity.HasIndex(e => e.MovId, "ct_registered_movements_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_registered_movements_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.FakeData)
                .HasPrecision(1)
                .HasColumnName("fake_data");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.MovAmendedBy)
                .HasMaxLength(10)
                .HasColumnName("mov_amended_by");
            entity.Property(e => e.MovAmendmentReason)
                .HasMaxLength(2)
                .HasColumnName("mov_amendment_reason");
            entity.Property(e => e.MovAnomalyCheckDate).HasColumnName("mov_anomaly_check_date");
            entity.Property(e => e.MovAnomalyCode)
                .HasMaxLength(4)
                .HasColumnName("mov_anomaly_code");
            entity.Property(e => e.MovAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("mov_aud_datetime");
            entity.Property(e => e.MovAudId).HasColumnName("mov_aud_id");
            entity.Property(e => e.MovAudType).HasColumnName("mov_aud_type");
            entity.Property(e => e.MovCryIdImport)
                .HasPrecision(12)
                .HasColumnName("mov_cry_id_import");
            entity.Property(e => e.MovCurrentModifiedDate).HasColumnName("mov_current_modified_date");
            entity.Property(e => e.MovCurrentPid)
                .HasPrecision(12)
                .HasColumnName("mov_current_pid");
            entity.Property(e => e.MovCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("mov_current_status");
            entity.Property(e => e.MovCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("mov_current_user");
            entity.Property(e => e.MovDirection)
                .HasMaxLength(1)
                .HasColumnName("mov_direction");
            entity.Property(e => e.MovEidReported)
                .HasMaxLength(20)
                .HasColumnName("mov_eid_reported");
            entity.Property(e => e.MovHealthCertificateNo)
                .HasMaxLength(30)
                .HasColumnName("mov_health_certificate_no");
            entity.Property(e => e.MovId)
                .HasPrecision(12)
                .HasColumnName("mov_id");
            entity.Property(e => e.MovInferMovementRule)
                .HasMaxLength(4)
                .HasColumnName("mov_infer_movement_rule");
            entity.Property(e => e.MovInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("mov_interface_file_name");
            entity.Property(e => e.MovInterfaceFileTxn)
                .HasPrecision(4)
                .HasColumnName("mov_interface_file_txn");
            entity.Property(e => e.MovKillNumber)
                .HasMaxLength(20)
                .HasColumnName("mov_kill_number");
            entity.Property(e => e.MovLocId)
                .HasPrecision(12)
                .HasColumnName("mov_loc_id");
            entity.Property(e => e.MovLocationRepd)
                .HasMaxLength(17)
                .HasColumnName("mov_location_repd");
            entity.Property(e => e.MovMovementDate).HasColumnName("mov_movement_date");
            entity.Property(e => e.MovMovementReceivedDate).HasColumnName("mov_movement_received_date");
            entity.Property(e => e.MovMovementType)
                .HasMaxLength(2)
                .HasColumnName("mov_movement_type");
            entity.Property(e => e.MovOrigInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("mov_orig_interface_file_name");
            entity.Property(e => e.MovOrigInterfaceFileTxn)
                .HasPrecision(4)
                .HasColumnName("mov_orig_interface_file_txn");
            entity.Property(e => e.MovOriginator)
                .HasPrecision(12)
                .HasColumnName("mov_originator");
            entity.Property(e => e.MovOriginatorsReference)
                .HasMaxLength(20)
                .HasColumnName("mov_originators_reference");
            entity.Property(e => e.MovProbityReportDate).HasColumnName("mov_probity_report_date");
            entity.Property(e => e.MovRanId)
                .HasPrecision(12)
                .HasColumnName("mov_ran_id");
            entity.Property(e => e.MovReportedEartag)
                .HasMaxLength(50)
                .HasColumnName("mov_reported_eartag");
            entity.Property(e => e.MovSourceType)
                .HasMaxLength(3)
                .HasColumnName("mov_source_type");
            entity.Property(e => e.MovSuspenseDate).HasColumnName("mov_suspense_date");
            entity.Property(e => e.MovVersion)
                .HasPrecision(6)
                .HasColumnName("mov_version");
            entity.Property(e => e.MovVersionCreationDate).HasColumnName("mov_version_creation_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtResetToExtract>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_reset_to_extract_pkey");

            entity.ToTable("ct_reset_to_extract", "cts_transactions");

            entity.HasIndex(e => e.RteAudDatetime, "ct_reset_to_extract_aud_datetime_idx");

            entity.HasIndex(e => e.RteAudId, "ct_reset_to_extract_aud_id_idx");

            entity.HasIndex(e => new { e.RteAudType, e.RteAudDatetime }, "ct_reset_to_extract_aud_type_datetime_idx");

            entity.HasIndex(e => e.RteAudType, "ct_reset_to_extract_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_reset_to_extract_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_reset_to_extract_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_reset_to_extract_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_reset_to_extract_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_reset_to_extract_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_reset_to_extract_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_reset_to_extract_record_type_idx");

            entity.HasIndex(e => e.RteId, "ct_reset_to_extract_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_reset_to_extract_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.RteAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("rte_aud_datetime");
            entity.Property(e => e.RteAudId).HasColumnName("rte_aud_id");
            entity.Property(e => e.RteAudType).HasColumnName("rte_aud_type");
            entity.Property(e => e.RteBatch)
                .HasPrecision(5)
                .HasColumnName("rte_batch");
            entity.Property(e => e.RteId).HasColumnName("rte_id");
            entity.Property(e => e.RteStatus)
                .HasMaxLength(1)
                .HasColumnName("rte_status");
            entity.Property(e => e.RteTableName)
                .HasMaxLength(50)
                .HasColumnName("rte_table_name");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtSbcsExt>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_sbcs_ext_pkey");

            entity.ToTable("ct_sbcs_ext", "cts_transactions");

            entity.HasIndex(e => e.SxtAudDatetime, "ct_sbcs_ext_aud_datetime_idx");

            entity.HasIndex(e => e.SxtAudId, "ct_sbcs_ext_aud_id_idx");

            entity.HasIndex(e => new { e.SxtAudType, e.SxtAudDatetime }, "ct_sbcs_ext_aud_type_datetime_idx");

            entity.HasIndex(e => e.SxtAudType, "ct_sbcs_ext_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_sbcs_ext_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_sbcs_ext_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_sbcs_ext_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_sbcs_ext_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_sbcs_ext_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_sbcs_ext_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_sbcs_ext_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_sbcs_ext_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SxtAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("sxt_aud_datetime");
            entity.Property(e => e.SxtAudId).HasColumnName("sxt_aud_id");
            entity.Property(e => e.SxtAudType).HasColumnName("sxt_aud_type");
            entity.Property(e => e.SxtId)
                .HasMaxLength(20)
                .HasColumnName("sxt_id");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtScheme>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_schemes_transactions_pkey");

            entity.ToTable("ct_schemes", "cts_transactions");

            entity.HasIndex(e => e.SchAudDatetime, "ct_schemes_aud_datetime_idx");

            entity.HasIndex(e => e.SchAudId, "ct_schemes_aud_id_idx");

            entity.HasIndex(e => new { e.SchAudType, e.SchAudDatetime }, "ct_schemes_aud_type_datetime_idx");

            entity.HasIndex(e => e.SchAudType, "ct_schemes_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_schemes_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_schemes_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_schemes_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_schemes_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_schemes_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_schemes_record_type_idx");

            entity.HasIndex(e => e.SchId, "ct_schemes_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_schemes_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SchAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("sch_aud_datetime");
            entity.Property(e => e.SchAudId).HasColumnName("sch_aud_id");
            entity.Property(e => e.SchAudType).HasColumnName("sch_aud_type");
            entity.Property(e => e.SchCurrentModifiedDate).HasColumnName("sch_current_modified_date");
            entity.Property(e => e.SchCurrentPid)
                .HasPrecision(3)
                .HasColumnName("sch_current_pid");
            entity.Property(e => e.SchCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("sch_current_status");
            entity.Property(e => e.SchCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("sch_current_user");
            entity.Property(e => e.SchExpiryDate).HasColumnName("sch_expiry_date");
            entity.Property(e => e.SchId)
                .HasPrecision(12)
                .HasColumnName("sch_id");
            entity.Property(e => e.SchLongDescription)
                .HasMaxLength(100)
                .HasColumnName("sch_long_description");
            entity.Property(e => e.SchScheme)
                .HasMaxLength(10)
                .HasColumnName("sch_scheme");
            entity.Property(e => e.SchShortDescription)
                .HasMaxLength(30)
                .HasColumnName("sch_short_description");
            entity.Property(e => e.SchVersion)
                .HasPrecision(6)
                .HasColumnName("sch_version");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtStageFile>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_stage_files_pkey");

            entity.ToTable("ct_stage_files", "cts_transactions");

            entity.HasIndex(e => e.StfAudDatetime, "ct_stage_files_aud_datetime_idx");

            entity.HasIndex(e => e.StfAudId, "ct_stage_files_aud_id_idx");

            entity.HasIndex(e => new { e.StfAudType, e.StfAudDatetime }, "ct_stage_files_aud_type_datetime_idx");

            entity.HasIndex(e => e.StfAudType, "ct_stage_files_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_stage_files_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_stage_files_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_stage_files_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_stage_files_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_stage_files_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_stage_files_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_stage_files_record_type_idx");

            entity.HasIndex(e => e.StfId, "ct_stage_files_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_stage_files_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.StfAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("stf_aud_datetime");
            entity.Property(e => e.StfAudId).HasColumnName("stf_aud_id");
            entity.Property(e => e.StfAudType).HasColumnName("stf_aud_type");
            entity.Property(e => e.StfFileName)
                .HasMaxLength(2000)
                .HasColumnName("stf_file_name");
            entity.Property(e => e.StfFileType)
                .HasMaxLength(100)
                .HasColumnName("stf_file_type");
            entity.Property(e => e.StfId)
                .HasPrecision(12)
                .HasColumnName("stf_id");
            entity.Property(e => e.StfLineNumber)
                .HasPrecision(12)
                .HasColumnName("stf_line_number");
            entity.Property(e => e.StfRecord)
                .HasMaxLength(2000)
                .HasColumnName("stf_record");
            entity.Property(e => e.StfTimestamp).HasColumnName("stf_timestamp");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtStageLock>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_stage_locks_pkey");

            entity.ToTable("ct_stage_locks", "cts_transactions");

            entity.HasIndex(e => e.StlAudDatetime, "ct_stage_locks_aud_datetime_idx");

            entity.HasIndex(e => e.StlAudId, "ct_stage_locks_aud_id_idx");

            entity.HasIndex(e => new { e.StlAudType, e.StlAudDatetime }, "ct_stage_locks_aud_type_datetime_idx");

            entity.HasIndex(e => e.StlAudType, "ct_stage_locks_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_stage_locks_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_stage_locks_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_stage_locks_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_stage_locks_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_stage_locks_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_stage_locks_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_stage_locks_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_stage_locks_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.StlAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("stl_aud_datetime");
            entity.Property(e => e.StlAudId).HasColumnName("stl_aud_id");
            entity.Property(e => e.StlAudType).HasColumnName("stl_aud_type");
            entity.Property(e => e.StlFileName)
                .HasMaxLength(2000)
                .HasColumnName("stl_file_name");
            entity.Property(e => e.StlFileType)
                .HasMaxLength(100)
                .HasColumnName("stl_file_type");
            entity.Property(e => e.StlProcessed)
                .HasMaxLength(1)
                .HasColumnName("stl_processed");
            entity.Property(e => e.StlTimestamp).HasColumnName("stl_timestamp");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtStageMessage>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_stage_messages_pkey");

            entity.ToTable("ct_stage_messages", "cts_transactions");

            entity.HasIndex(e => e.StmAudDatetime, "ct_stage_messages_aud_datetime_idx");

            entity.HasIndex(e => e.StmAudId, "ct_stage_messages_aud_id_idx");

            entity.HasIndex(e => new { e.StmAudType, e.StmAudDatetime }, "ct_stage_messages_aud_type_datetime_idx");

            entity.HasIndex(e => e.StmAudType, "ct_stage_messages_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_stage_messages_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_stage_messages_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_stage_messages_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_stage_messages_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_stage_messages_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_stage_messages_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_stage_messages_record_type_idx");

            entity.HasIndex(e => e.TransType, "ct_stage_messages_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.StmAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("stm_aud_datetime");
            entity.Property(e => e.StmAudId).HasColumnName("stm_aud_id");
            entity.Property(e => e.StmAudType).HasColumnName("stm_aud_type");
            entity.Property(e => e.StmDirectoryKey)
                .HasMaxLength(100)
                .HasColumnName("stm_directory_key");
            entity.Property(e => e.StmFilePrefix)
                .HasMaxLength(100)
                .HasColumnName("stm_file_prefix");
            entity.Property(e => e.StmFileSuffix)
                .HasMaxLength(100)
                .HasColumnName("stm_file_suffix");
            entity.Property(e => e.StmFileType)
                .HasMaxLength(100)
                .HasColumnName("stm_file_type");
            entity.Property(e => e.StmSleepPeriod)
                .HasPrecision(10)
                .HasColumnName("stm_sleep_period");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtSublocationType>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_sublocation_types_transactions_pkey");

            entity.ToTable("ct_sublocation_types", "cts_transactions");

            entity.HasIndex(e => e.SltAudDatetime, "ct_sublocation_types_aud_datetime_idx");

            entity.HasIndex(e => e.SltAudId, "ct_sublocation_types_aud_id_idx");

            entity.HasIndex(e => new { e.SltAudType, e.SltAudDatetime }, "ct_sublocation_types_aud_type_datetime_idx");

            entity.HasIndex(e => e.SltAudType, "ct_sublocation_types_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_sublocation_types_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_sublocation_types_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_sublocation_types_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_sublocation_types_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_sublocation_types_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_sublocation_types_record_type_idx");

            entity.HasIndex(e => e.SltId, "ct_sublocation_types_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_sublocation_types_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SltAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("slt_aud_datetime");
            entity.Property(e => e.SltAudId).HasColumnName("slt_aud_id");
            entity.Property(e => e.SltAudType).HasColumnName("slt_aud_type");
            entity.Property(e => e.SltCurrentModifiedDate).HasColumnName("slt_current_modified_date");
            entity.Property(e => e.SltCurrentPid)
                .HasPrecision(3)
                .HasColumnName("slt_current_pid");
            entity.Property(e => e.SltCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("slt_current_status");
            entity.Property(e => e.SltCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("slt_current_user");
            entity.Property(e => e.SltHierLinkPermitted)
                .HasMaxLength(1)
                .HasColumnName("slt_hier_link_permitted");
            entity.Property(e => e.SltId)
                .HasPrecision(12)
                .HasColumnName("slt_id");
            entity.Property(e => e.SltLongDescription)
                .HasMaxLength(60)
                .HasColumnName("slt_long_description");
            entity.Property(e => e.SltMovementSublocInd)
                .HasMaxLength(1)
                .HasColumnName("slt_movement_subloc_ind");
            entity.Property(e => e.SltPeerLinkPermitted)
                .HasMaxLength(1)
                .HasColumnName("slt_peer_link_permitted");
            entity.Property(e => e.SltShortDescription)
                .HasMaxLength(20)
                .HasColumnName("slt_short_description");
            entity.Property(e => e.SltSublocType)
                .HasMaxLength(2)
                .HasColumnName("slt_subloc_type");
            entity.Property(e => e.SltUseSublocAddress)
                .HasMaxLength(1)
                .HasColumnName("slt_use_subloc_address");
            entity.Property(e => e.SltVersion)
                .HasPrecision(6)
                .HasColumnName("slt_version");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtSuspAnimalError>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_susp_animal_errors_pkey");

            entity.ToTable("ct_susp_animal_errors", "cts_transactions");

            entity.HasIndex(e => e.SaeAudDatetime, "ct_susp_animal_errors_aud_datetime_idx");

            entity.HasIndex(e => e.SaeAudId, "ct_susp_animal_errors_aud_id_idx");

            entity.HasIndex(e => new { e.SaeAudType, e.SaeAudDatetime }, "ct_susp_animal_errors_aud_type_datetime_idx");

            entity.HasIndex(e => e.SaeAudType, "ct_susp_animal_errors_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_susp_animal_errors_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_susp_animal_errors_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_susp_animal_errors_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_susp_animal_errors_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_susp_animal_errors_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_susp_animal_errors_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_susp_animal_errors_record_type_idx");

            entity.HasIndex(e => e.SaeSanId, "ct_susp_animal_errors_sae_san_id_idx");

            entity.HasIndex(e => e.SaeId, "ct_susp_animal_errors_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_susp_animal_errors_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SaeAttributeName)
                .HasMaxLength(30)
                .HasColumnName("sae_attribute_name");
            entity.Property(e => e.SaeAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("sae_aud_datetime");
            entity.Property(e => e.SaeAudId).HasColumnName("sae_aud_id");
            entity.Property(e => e.SaeAudType).HasColumnName("sae_aud_type");
            entity.Property(e => e.SaeCurrentModifiedDate).HasColumnName("sae_current_modified_date");
            entity.Property(e => e.SaeCurrentPid)
                .HasPrecision(3)
                .HasColumnName("sae_current_pid");
            entity.Property(e => e.SaeCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("sae_current_status");
            entity.Property(e => e.SaeCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("sae_current_user");
            entity.Property(e => e.SaeErrorCode)
                .HasMaxLength(4)
                .HasColumnName("sae_error_code");
            entity.Property(e => e.SaeId)
                .HasPrecision(12)
                .HasColumnName("sae_id");
            entity.Property(e => e.SaeSanId)
                .HasPrecision(12)
                .HasColumnName("sae_san_id");
            entity.Property(e => e.SaeVersion)
                .HasPrecision(6)
                .HasColumnName("sae_version");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtSuspCmMeasureResult>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_susp_cm_measure_results_pkey");

            entity.ToTable("ct_susp_cm_measure_results", "cts_transactions");

            entity.HasIndex(e => e.SmrAudDatetime, "ct_susp_cm_measure_results_aud_datetime_idx");

            entity.HasIndex(e => e.SmrAudId, "ct_susp_cm_measure_results_aud_id_idx");

            entity.HasIndex(e => new { e.SmrAudType, e.SmrAudDatetime }, "ct_susp_cm_measure_results_aud_type_datetime_idx");

            entity.HasIndex(e => e.SmrAudType, "ct_susp_cm_measure_results_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_susp_cm_measure_results_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_susp_cm_measure_results_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_susp_cm_measure_results_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_susp_cm_measure_results_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_susp_cm_measure_results_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_susp_cm_measure_results_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_susp_cm_measure_results_record_type_idx");

            entity.HasIndex(e => e.SmrScmId, "ct_susp_cm_measure_results_smr_scm_id_idx");

            entity.HasIndex(e => e.SmrId, "ct_susp_cm_measure_results_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_susp_cm_measure_results_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SmrAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("smr_aud_datetime");
            entity.Property(e => e.SmrAudId).HasColumnName("smr_aud_id");
            entity.Property(e => e.SmrAudType).HasColumnName("smr_aud_type");
            entity.Property(e => e.SmrCurrentModifiedDate).HasColumnName("smr_current_modified_date");
            entity.Property(e => e.SmrCurrentPid)
                .HasPrecision(3)
                .HasColumnName("smr_current_pid");
            entity.Property(e => e.SmrCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("smr_current_status");
            entity.Property(e => e.SmrCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("smr_current_user");
            entity.Property(e => e.SmrId)
                .HasPrecision(12)
                .HasColumnName("smr_id");
            entity.Property(e => e.SmrMeasureChar)
                .HasMaxLength(10)
                .HasColumnName("smr_measure_char");
            entity.Property(e => e.SmrMeasureNum)
                .HasPrecision(9)
                .HasColumnName("smr_measure_num");
            entity.Property(e => e.SmrResultChar)
                .HasMaxLength(10)
                .HasColumnName("smr_result_char");
            entity.Property(e => e.SmrResultNum)
                .HasPrecision(9)
                .HasColumnName("smr_result_num");
            entity.Property(e => e.SmrScmId)
                .HasPrecision(12)
                .HasColumnName("smr_scm_id");
            entity.Property(e => e.SmrVersion)
                .HasPrecision(6)
                .HasColumnName("smr_version");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtSuspConditionMarker>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_susp_condition_markers_pkey");

            entity.ToTable("ct_susp_condition_markers", "cts_transactions");

            entity.HasIndex(e => e.ScmAudDatetime, "ct_susp_condition_markers_aud_datetime_idx");

            entity.HasIndex(e => e.ScmAudId, "ct_susp_condition_markers_aud_id_idx");

            entity.HasIndex(e => new { e.ScmAudType, e.ScmAudDatetime }, "ct_susp_condition_markers_aud_type_datetime_idx");

            entity.HasIndex(e => e.ScmAudType, "ct_susp_condition_markers_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_susp_condition_markers_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_susp_condition_markers_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_susp_condition_markers_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_susp_condition_markers_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_susp_condition_markers_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_susp_condition_markers_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_susp_condition_markers_record_type_idx");

            entity.HasIndex(e => e.ScmLocId, "ct_susp_condition_markers_scm_loc_id_idx");

            entity.HasIndex(e => e.ScmRanId, "ct_susp_condition_markers_scm_ran_id_idx");

            entity.HasIndex(e => e.ScmId, "ct_susp_condition_markers_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_susp_condition_markers_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.ScmAddMatchFlag)
                .HasMaxLength(3)
                .HasColumnName("scm_add_match_flag");
            entity.Property(e => e.ScmAmendedBy)
                .HasMaxLength(30)
                .HasColumnName("scm_amended_by");
            entity.Property(e => e.ScmAmendmentDatetime).HasColumnName("scm_amendment_datetime");
            entity.Property(e => e.ScmAmendmentReason)
                .HasMaxLength(2)
                .HasColumnName("scm_amendment_reason");
            entity.Property(e => e.ScmAmendmentReasonText)
                .HasMaxLength(80)
                .HasColumnName("scm_amendment_reason_text");
            entity.Property(e => e.ScmAmendmentStatus)
                .HasMaxLength(10)
                .HasColumnName("scm_amendment_status");
            entity.Property(e => e.ScmAnimalIdentifier)
                .HasMaxLength(14)
                .HasColumnName("scm_animal_identifier");
            entity.Property(e => e.ScmAnimalIdentifierType)
                .HasMaxLength(2)
                .HasColumnName("scm_animal_identifier_type");
            entity.Property(e => e.ScmAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("scm_aud_datetime");
            entity.Property(e => e.ScmAudId).HasColumnName("scm_aud_id");
            entity.Property(e => e.ScmAudType).HasColumnName("scm_aud_type");
            entity.Property(e => e.ScmCancellationDate).HasColumnName("scm_cancellation_date");
            entity.Property(e => e.ScmComments)
                .HasMaxLength(500)
                .HasColumnName("scm_comments");
            entity.Property(e => e.ScmConditionActivity)
                .HasMaxLength(20)
                .HasColumnName("scm_condition_activity");
            entity.Property(e => e.ScmConditionAuthority)
                .HasMaxLength(20)
                .HasColumnName("scm_condition_authority");
            entity.Property(e => e.ScmConditionCode)
                .HasMaxLength(20)
                .HasColumnName("scm_condition_code");
            entity.Property(e => e.ScmConditionType)
                .HasMaxLength(10)
                .HasColumnName("scm_condition_type");
            entity.Property(e => e.ScmConditionVariant)
                .HasMaxLength(20)
                .HasColumnName("scm_condition_variant");
            entity.Property(e => e.ScmCurrentModifiedDate).HasColumnName("scm_current_modified_date");
            entity.Property(e => e.ScmCurrentPid)
                .HasPrecision(3)
                .HasColumnName("scm_current_pid");
            entity.Property(e => e.ScmCurrentPurposeCode)
                .HasMaxLength(10)
                .HasColumnName("scm_current_purpose_code");
            entity.Property(e => e.ScmCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("scm_current_status");
            entity.Property(e => e.ScmCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("scm_current_user");
            entity.Property(e => e.ScmDocumentRefs)
                .HasMaxLength(60)
                .HasColumnName("scm_document_refs");
            entity.Property(e => e.ScmEffectiveFromDate).HasColumnName("scm_effective_from_date");
            entity.Property(e => e.ScmEffectiveToDate).HasColumnName("scm_effective_to_date");
            entity.Property(e => e.ScmGroupingReference)
                .HasMaxLength(16)
                .HasColumnName("scm_grouping_reference");
            entity.Property(e => e.ScmId)
                .HasPrecision(12)
                .HasColumnName("scm_id");
            entity.Property(e => e.ScmInterfaceFilename)
                .HasMaxLength(25)
                .HasColumnName("scm_interface_filename");
            entity.Property(e => e.ScmInterfaceTxnNumber)
                .HasPrecision(4)
                .HasColumnName("scm_interface_txn_number");
            entity.Property(e => e.ScmLocId)
                .HasPrecision(12)
                .HasColumnName("scm_loc_id");
            entity.Property(e => e.ScmLocationIdentifier)
                .HasMaxLength(15)
                .HasColumnName("scm_location_identifier");
            entity.Property(e => e.ScmLocationType)
                .HasMaxLength(2)
                .HasColumnName("scm_location_type");
            entity.Property(e => e.ScmOriginalInterfaceFile)
                .HasMaxLength(25)
                .HasColumnName("scm_original_interface_file");
            entity.Property(e => e.ScmOriginalInterfaceTxn)
                .HasPrecision(4)
                .HasColumnName("scm_original_interface_txn");
            entity.Property(e => e.ScmOriginator)
                .HasMaxLength(30)
                .HasColumnName("scm_originator");
            entity.Property(e => e.ScmOwner)
                .HasMaxLength(3)
                .HasColumnName("scm_owner");
            entity.Property(e => e.ScmRanId)
                .HasPrecision(12)
                .HasColumnName("scm_ran_id");
            entity.Property(e => e.ScmSource)
                .HasMaxLength(2)
                .HasColumnName("scm_source");
            entity.Property(e => e.ScmSublocationIdentifier)
                .HasMaxLength(2)
                .HasColumnName("scm_sublocation_identifier");
            entity.Property(e => e.ScmSubmitDate).HasColumnName("scm_submit_date");
            entity.Property(e => e.ScmSuspenseReason)
                .HasMaxLength(2)
                .HasColumnName("scm_suspense_reason");
            entity.Property(e => e.ScmSystemError)
                .HasMaxLength(10)
                .HasColumnName("scm_system_error");
            entity.Property(e => e.ScmUseType)
                .HasMaxLength(1)
                .HasColumnName("scm_use_type");
            entity.Property(e => e.ScmVersion)
                .HasPrecision(6)
                .HasColumnName("scm_version");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtSuspMovementError>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_susp_movement_errors_pkey");

            entity.ToTable("ct_susp_movement_errors", "cts_transactions");

            entity.HasIndex(e => e.SmeAudDatetime, "ct_susp_movement_errors_aud_datetime_idx");

            entity.HasIndex(e => e.SmeAudId, "ct_susp_movement_errors_aud_id_idx");

            entity.HasIndex(e => new { e.SmeAudType, e.SmeAudDatetime }, "ct_susp_movement_errors_aud_type_datetime_idx");

            entity.HasIndex(e => e.SmeAudType, "ct_susp_movement_errors_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_susp_movement_errors_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_susp_movement_errors_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_susp_movement_errors_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_susp_movement_errors_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_susp_movement_errors_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_susp_movement_errors_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_susp_movement_errors_record_type_idx");

            entity.HasIndex(e => e.SmeSmoId, "ct_susp_movement_errors_sme_smo_id_idx");

            entity.HasIndex(e => e.SmeId, "ct_susp_movement_errors_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_susp_movement_errors_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SmeAttributeName)
                .HasMaxLength(30)
                .HasColumnName("sme_attribute_name");
            entity.Property(e => e.SmeAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("sme_aud_datetime");
            entity.Property(e => e.SmeAudId).HasColumnName("sme_aud_id");
            entity.Property(e => e.SmeAudType).HasColumnName("sme_aud_type");
            entity.Property(e => e.SmeCurrentModifiedDate).HasColumnName("sme_current_modified_date");
            entity.Property(e => e.SmeCurrentPid)
                .HasPrecision(3)
                .HasColumnName("sme_current_pid");
            entity.Property(e => e.SmeCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("sme_current_status");
            entity.Property(e => e.SmeCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("sme_current_user");
            entity.Property(e => e.SmeErrorCode)
                .HasMaxLength(4)
                .HasColumnName("sme_error_code");
            entity.Property(e => e.SmeId)
                .HasPrecision(12)
                .HasColumnName("sme_id");
            entity.Property(e => e.SmeSmoId)
                .HasPrecision(12)
                .HasColumnName("sme_smo_id");
            entity.Property(e => e.SmeVersion)
                .HasPrecision(6)
                .HasColumnName("sme_version");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtSuspendedAnimal>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_suspended_animals_pkey");

            entity.ToTable("ct_suspended_animals", "cts_transactions");

            entity.HasIndex(e => e.SanAudDatetime, "ct_suspended_animals_aud_datetime_idx");

            entity.HasIndex(e => e.SanAudId, "ct_suspended_animals_aud_id_idx");

            entity.HasIndex(e => new { e.SanAudType, e.SanAudDatetime }, "ct_suspended_animals_aud_type_datetime_idx");

            entity.HasIndex(e => e.SanAudType, "ct_suspended_animals_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_suspended_animals_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_suspended_animals_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_suspended_animals_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_suspended_animals_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_suspended_animals_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_suspended_animals_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_suspended_animals_record_type_idx");

            entity.HasIndex(e => e.SanLocIdInitial, "ct_suspended_animals_san_loc_id_initial_idx");

            entity.HasIndex(e => e.SanLocIdRequest, "ct_suspended_animals_san_loc_id_request_idx");

            entity.HasIndex(e => e.SanRanId, "ct_suspended_animals_san_ran_id_idx");

            entity.HasIndex(e => e.SanVapId, "ct_suspended_animals_san_vap_id_idx");

            entity.HasIndex(e => e.SanWgpId, "ct_suspended_animals_san_wgp_id_idx");

            entity.HasIndex(e => e.SanId, "ct_suspended_animals_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_suspended_animals_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SanAmendReason)
                .HasMaxLength(2)
                .HasColumnName("san_amend_reason");
            entity.Property(e => e.SanAmendRetagInd)
                .HasMaxLength(1)
                .HasColumnName("san_amend_retag_ind");
            entity.Property(e => e.SanAmendedBy)
                .HasMaxLength(10)
                .HasColumnName("san_amended_by");
            entity.Property(e => e.SanAmendedDatetime).HasColumnName("san_amended_datetime");
            entity.Property(e => e.SanApplicReceiptDate).HasColumnName("san_applic_receipt_date");
            entity.Property(e => e.SanApplicTargetDate).HasColumnName("san_applic_target_date");
            entity.Property(e => e.SanApplicationType)
                .HasMaxLength(1)
                .HasColumnName("san_application_type");
            entity.Property(e => e.SanAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("san_aud_datetime");
            entity.Property(e => e.SanAudId).HasColumnName("san_aud_id");
            entity.Property(e => e.SanAudType).HasColumnName("san_aud_type");
            entity.Property(e => e.SanBirthDate).HasColumnName("san_birth_date");
            entity.Property(e => e.SanBreed)
                .HasMaxLength(20)
                .HasColumnName("san_breed");
            entity.Property(e => e.SanChangeReceivedDate).HasColumnName("san_change_received_date");
            entity.Property(e => e.SanChrCorrectionType)
                .HasMaxLength(1)
                .HasColumnName("san_chr_correction_type");
            entity.Property(e => e.SanChrLocationInd)
                .HasMaxLength(1)
                .HasColumnName("san_chr_location_ind");
            entity.Property(e => e.SanCountryOfOrigin)
                .HasMaxLength(2)
                .HasColumnName("san_country_of_origin");
            entity.Property(e => e.SanCtsIndicator)
                .HasMaxLength(1)
                .HasColumnName("san_cts_indicator");
            entity.Property(e => e.SanCurrentModifiedDate).HasColumnName("san_current_modified_date");
            entity.Property(e => e.SanCurrentPid)
                .HasPrecision(3)
                .HasColumnName("san_current_pid");
            entity.Property(e => e.SanCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("san_current_status");
            entity.Property(e => e.SanCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("san_current_user");
            entity.Property(e => e.SanEartag)
                .HasMaxLength(30)
                .HasColumnName("san_eartag");
            entity.Property(e => e.SanEartagType)
                .HasMaxLength(20)
                .HasColumnName("san_eartag_type");
            entity.Property(e => e.SanElectronicIdentifier)
                .HasMaxLength(30)
                .HasColumnName("san_electronic_identifier");
            entity.Property(e => e.SanGeneticDamEartag)
                .HasMaxLength(30)
                .HasColumnName("san_genetic_dam_eartag");
            entity.Property(e => e.SanGeneticDamEtType)
                .HasMaxLength(20)
                .HasColumnName("san_genetic_dam_et_type");
            entity.Property(e => e.SanHealthCertificateNo)
                .HasMaxLength(30)
                .HasColumnName("san_health_certificate_no");
            entity.Property(e => e.SanId)
                .HasPrecision(12)
                .HasColumnName("san_id");
            entity.Property(e => e.SanImportIdentifier)
                .HasMaxLength(20)
                .HasColumnName("san_import_identifier");
            entity.Property(e => e.SanInitialLocationRepd)
                .HasMaxLength(17)
                .HasColumnName("san_initial_location_repd");
            entity.Property(e => e.SanIntendedAction)
                .HasMaxLength(2)
                .HasColumnName("san_intended_action");
            entity.Property(e => e.SanInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("san_interface_file_name");
            entity.Property(e => e.SanInterfaceFileTxn)
                .HasPrecision(4)
                .HasColumnName("san_interface_file_txn");
            entity.Property(e => e.SanLateAppLetter).HasColumnName("san_late_app_letter");
            entity.Property(e => e.SanLocIdInitial)
                .HasPrecision(12)
                .HasColumnName("san_loc_id_initial");
            entity.Property(e => e.SanLocIdRequest)
                .HasPrecision(12)
                .HasColumnName("san_loc_id_request");
            entity.Property(e => e.SanNewEartag)
                .HasMaxLength(30)
                .HasColumnName("san_new_eartag");
            entity.Property(e => e.SanNewEartagType)
                .HasMaxLength(20)
                .HasColumnName("san_new_eartag_type");
            entity.Property(e => e.SanNumberCalfMovts)
                .HasPrecision(1)
                .HasColumnName("san_number_calf_movts");
            entity.Property(e => e.SanOrigIfFileName)
                .HasMaxLength(25)
                .HasColumnName("san_orig_if_file_name");
            entity.Property(e => e.SanOrigIfFileTxn)
                .HasPrecision(4)
                .HasColumnName("san_orig_if_file_txn");
            entity.Property(e => e.SanOriginator)
                .HasMaxLength(20)
                .HasColumnName("san_originator");
            entity.Property(e => e.SanPassportVersionNumber)
                .HasMaxLength(3)
                .HasColumnName("san_passport_version_number");
            entity.Property(e => e.SanPlacementDate).HasColumnName("san_placement_date");
            entity.Property(e => e.SanRanId)
                .HasPrecision(12)
                .HasColumnName("san_ran_id");
            entity.Property(e => e.SanRequestLocationRepd)
                .HasMaxLength(17)
                .HasColumnName("san_request_location_repd");
            entity.Property(e => e.SanSex)
                .HasMaxLength(1)
                .HasColumnName("san_sex");
            entity.Property(e => e.SanSireEartag)
                .HasMaxLength(30)
                .HasColumnName("san_sire_eartag");
            entity.Property(e => e.SanSireEtType)
                .HasMaxLength(20)
                .HasColumnName("san_sire_et_type");
            entity.Property(e => e.SanSourceReference)
                .HasMaxLength(20)
                .HasColumnName("san_source_reference");
            entity.Property(e => e.SanSourceType)
                .HasMaxLength(3)
                .HasColumnName("san_source_type");
            entity.Property(e => e.SanSubmitDatetime).HasColumnName("san_submit_datetime");
            entity.Property(e => e.SanSurrDamEartag)
                .HasMaxLength(30)
                .HasColumnName("san_surr_dam_eartag");
            entity.Property(e => e.SanSurrDamEtType)
                .HasMaxLength(20)
                .HasColumnName("san_surr_dam_et_type");
            entity.Property(e => e.SanSuspenseDate).HasColumnName("san_suspense_date");
            entity.Property(e => e.SanVapId)
                .HasPrecision(12)
                .HasColumnName("san_vap_id");
            entity.Property(e => e.SanVersion)
                .HasPrecision(6)
                .HasColumnName("san_version");
            entity.Property(e => e.SanWgpId)
                .HasPrecision(12)
                .HasColumnName("san_wgp_id");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtSuspendedMovement>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_suspended_movements_pkey");

            entity.ToTable("ct_suspended_movements", "cts_transactions");

            entity.HasIndex(e => e.SmoAudDatetime, "ct_suspended_movements_aud_datetime_idx");

            entity.HasIndex(e => e.SmoAudId, "ct_suspended_movements_aud_id_idx");

            entity.HasIndex(e => new { e.SmoAudType, e.SmoAudDatetime }, "ct_suspended_movements_aud_type_datetime_idx");

            entity.HasIndex(e => e.SmoAudType, "ct_suspended_movements_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_suspended_movements_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_suspended_movements_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_suspended_movements_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_suspended_movements_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_suspended_movements_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_suspended_movements_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_suspended_movements_record_type_idx");

            entity.HasIndex(e => e.SmoId, "ct_suspended_movements_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_suspended_movements_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SmoAmendedBy)
                .HasMaxLength(10)
                .HasColumnName("smo_amended_by");
            entity.Property(e => e.SmoAmendedDatetime).HasColumnName("smo_amended_datetime");
            entity.Property(e => e.SmoAmendmentReason)
                .HasMaxLength(2)
                .HasColumnName("smo_amendment_reason");
            entity.Property(e => e.SmoAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("smo_aud_datetime");
            entity.Property(e => e.SmoAudId).HasColumnName("smo_aud_id");
            entity.Property(e => e.SmoAudType).HasColumnName("smo_aud_type");
            entity.Property(e => e.SmoCurrentModifiedDate).HasColumnName("smo_current_modified_date");
            entity.Property(e => e.SmoCurrentPid)
                .HasPrecision(3)
                .HasColumnName("smo_current_pid");
            entity.Property(e => e.SmoCurrentPurposeCode)
                .HasMaxLength(1)
                .HasColumnName("smo_current_purpose_code");
            entity.Property(e => e.SmoCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("smo_current_status");
            entity.Property(e => e.SmoCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("smo_current_user");
            entity.Property(e => e.SmoDirection)
                .HasMaxLength(1)
                .HasColumnName("smo_direction");
            entity.Property(e => e.SmoEartag)
                .HasMaxLength(14)
                .HasColumnName("smo_eartag");
            entity.Property(e => e.SmoEidReported)
                .HasMaxLength(20)
                .HasColumnName("smo_eid_reported");
            entity.Property(e => e.SmoId)
                .HasPrecision(12)
                .HasColumnName("smo_id");
            entity.Property(e => e.SmoInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("smo_interface_file_name");
            entity.Property(e => e.SmoInterfaceFileTxn)
                .HasPrecision(4)
                .HasColumnName("smo_interface_file_txn");
            entity.Property(e => e.SmoKillNumber)
                .HasMaxLength(20)
                .HasColumnName("smo_kill_number");
            entity.Property(e => e.SmoMovementDate).HasColumnName("smo_movement_date");
            entity.Property(e => e.SmoMovementLocIdentifier)
                .HasMaxLength(30)
                .HasColumnName("smo_movement_loc_identifier");
            entity.Property(e => e.SmoMovementLocType)
                .HasMaxLength(2)
                .HasColumnName("smo_movement_loc_type");
            entity.Property(e => e.SmoMovementReceivedDate).HasColumnName("smo_movement_received_date");
            entity.Property(e => e.SmoMovementSublocIdentifier)
                .HasMaxLength(30)
                .HasColumnName("smo_movement_subloc_identifier");
            entity.Property(e => e.SmoMovementType)
                .HasPrecision(2)
                .HasColumnName("smo_movement_type");
            entity.Property(e => e.SmoMovtWorkgroup)
                .HasMaxLength(10)
                .HasColumnName("smo_movt_workgroup");
            entity.Property(e => e.SmoOrigInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("smo_orig_interface_file_name");
            entity.Property(e => e.SmoOrigInterfaceFileTxn)
                .HasPrecision(4)
                .HasColumnName("smo_orig_interface_file_txn");
            entity.Property(e => e.SmoOriginator)
                .HasMaxLength(7)
                .HasColumnName("smo_originator");
            entity.Property(e => e.SmoOriginatorsReference)
                .HasMaxLength(12)
                .HasColumnName("smo_originators_reference");
            entity.Property(e => e.SmoSourceType)
                .HasMaxLength(3)
                .HasColumnName("smo_source_type");
            entity.Property(e => e.SmoSubmitDatetime).HasColumnName("smo_submit_datetime");
            entity.Property(e => e.SmoSuspenseDate).HasColumnName("smo_suspense_date");
            entity.Property(e => e.SmoSuspenseReason)
                .HasMaxLength(2)
                .HasColumnName("smo_suspense_reason");
            entity.Property(e => e.SmoVersion)
                .HasPrecision(6)
                .HasColumnName("smo_version");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtSuspenseCharAllocRule>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_suspense_char_alloc_rules_transactions_pkey");

            entity.ToTable("ct_suspense_char_alloc_rules", "cts_transactions");

            entity.HasIndex(e => e.ScaAudDatetime, "ct_suspense_char_alloc_rules_aud_datetime_idx");

            entity.HasIndex(e => e.ScaAudId, "ct_suspense_char_alloc_rules_aud_id_idx");

            entity.HasIndex(e => new { e.ScaAudType, e.ScaAudDatetime }, "ct_suspense_char_alloc_rules_aud_type_datetime_idx");

            entity.HasIndex(e => e.ScaAudType, "ct_suspense_char_alloc_rules_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_suspense_char_alloc_rules_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_suspense_char_alloc_rules_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_suspense_char_alloc_rules_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_suspense_char_alloc_rules_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_suspense_char_alloc_rules_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_suspense_char_alloc_rules_record_type_idx");

            entity.HasIndex(e => e.ScaRouId, "ct_suspense_char_alloc_rules_sca_rou_id_idx");

            entity.HasIndex(e => e.ScaId, "ct_suspense_char_alloc_rules_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_suspense_char_alloc_rules_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.ScaAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("sca_aud_datetime");
            entity.Property(e => e.ScaAudId).HasColumnName("sca_aud_id");
            entity.Property(e => e.ScaAudType).HasColumnName("sca_aud_type");
            entity.Property(e => e.ScaCurrentModifiedDate).HasColumnName("sca_current_modified_date");
            entity.Property(e => e.ScaCurrentPid)
                .HasPrecision(3)
                .HasColumnName("sca_current_pid");
            entity.Property(e => e.ScaCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("sca_current_status");
            entity.Property(e => e.ScaCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("sca_current_user");
            entity.Property(e => e.ScaId)
                .HasPrecision(12)
                .HasColumnName("sca_id");
            entity.Property(e => e.ScaRouId)
                .HasPrecision(12)
                .HasColumnName("sca_rou_id");
            entity.Property(e => e.ScaSubroutine)
                .HasMaxLength(3)
                .HasColumnName("sca_subroutine");
            entity.Property(e => e.ScaSuspenseChar)
                .HasMaxLength(3)
                .HasColumnName("sca_suspense_char");
            entity.Property(e => e.ScaTestValue)
                .HasMaxLength(10)
                .HasColumnName("sca_test_value");
            entity.Property(e => e.ScaVersion)
                .HasPrecision(6)
                .HasColumnName("sca_version");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtSuspenseWgAllocRule>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_suspense_wg_alloc_rules_transactions_pkey");

            entity.ToTable("ct_suspense_wg_alloc_rules", "cts_transactions");

            entity.HasIndex(e => e.SwaAudDatetime, "ct_suspense_wg_alloc_rules_aud_datetime_idx");

            entity.HasIndex(e => e.SwaAudId, "ct_suspense_wg_alloc_rules_aud_id_idx");

            entity.HasIndex(e => new { e.SwaAudType, e.SwaAudDatetime }, "ct_suspense_wg_alloc_rules_aud_type_datetime_idx");

            entity.HasIndex(e => e.SwaAudType, "ct_suspense_wg_alloc_rules_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_suspense_wg_alloc_rules_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_suspense_wg_alloc_rules_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_suspense_wg_alloc_rules_file_trans_type_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_suspense_wg_alloc_rules_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_suspense_wg_alloc_rules_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_suspense_wg_alloc_rules_record_type_idx");

            entity.HasIndex(e => e.SwaId, "ct_suspense_wg_alloc_rules_source_key_idx");

            entity.HasIndex(e => e.SwaRouId, "ct_suspense_wg_alloc_rules_swa_rou_id_idx");

            entity.HasIndex(e => e.TransType, "ct_suspense_wg_alloc_rules_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SwaAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("swa_aud_datetime");
            entity.Property(e => e.SwaAudId).HasColumnName("swa_aud_id");
            entity.Property(e => e.SwaAudType).HasColumnName("swa_aud_type");
            entity.Property(e => e.SwaCurrentModifiedDate).HasColumnName("swa_current_modified_date");
            entity.Property(e => e.SwaCurrentPid)
                .HasPrecision(3)
                .HasColumnName("swa_current_pid");
            entity.Property(e => e.SwaCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("swa_current_status");
            entity.Property(e => e.SwaCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("swa_current_user");
            entity.Property(e => e.SwaId)
                .HasPrecision(12)
                .HasColumnName("swa_id");
            entity.Property(e => e.SwaPriority)
                .HasPrecision(3)
                .HasColumnName("swa_priority");
            entity.Property(e => e.SwaReportedBadDate).HasColumnName("swa_reported_bad_date");
            entity.Property(e => e.SwaRouId)
                .HasPrecision(12)
                .HasColumnName("swa_rou_id");
            entity.Property(e => e.SwaRule)
                .HasMaxLength(100)
                .HasColumnName("swa_rule");
            entity.Property(e => e.SwaRuleFormula)
                .HasMaxLength(100)
                .HasColumnName("swa_rule_formula");
            entity.Property(e => e.SwaVersion)
                .HasPrecision(6)
                .HasColumnName("swa_version");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
        });

        modelBuilder.Entity<CtValidApplication>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_valid_applications_pkey");

            entity.ToTable("ct_valid_applications", "cts_transactions");

            entity.HasIndex(e => e.VapAudDatetime, "ct_valid_applications_aud_datetime_idx");

            entity.HasIndex(e => e.VapAudId, "ct_valid_applications_aud_id_idx");

            entity.HasIndex(e => new { e.VapAudType, e.VapAudDatetime }, "ct_valid_applications_aud_type_datetime_idx");

            entity.HasIndex(e => e.VapAudType, "ct_valid_applications_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_valid_applications_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_valid_applications_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_valid_applications_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_valid_applications_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_valid_applications_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_valid_applications_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_valid_applications_record_type_idx");

            entity.HasIndex(e => e.VapId, "ct_valid_applications_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_valid_applications_trans_type_idx");

            entity.HasIndex(e => e.VapLocIdRequester, "ct_valid_applications_vap_loc_id_requester_idx");

            entity.HasIndex(e => e.VapWurId, "ct_valid_applications_vap_wur_id_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.FakeData)
                .HasPrecision(1)
                .HasColumnName("fake_data");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
            entity.Property(e => e.VapApplicationType)
                .HasMaxLength(1)
                .HasColumnName("vap_application_type");
            entity.Property(e => e.VapAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("vap_aud_datetime");
            entity.Property(e => e.VapAudId).HasColumnName("vap_aud_id");
            entity.Property(e => e.VapAudType).HasColumnName("vap_aud_type");
            entity.Property(e => e.VapCountyRequester)
                .HasMaxLength(2)
                .HasColumnName("vap_county_requester");
            entity.Property(e => e.VapCtsIndicator)
                .HasMaxLength(1)
                .HasColumnName("vap_cts_indicator");
            entity.Property(e => e.VapCurrentIntendedAction)
                .HasMaxLength(2)
                .HasColumnName("vap_current_intended_action");
            entity.Property(e => e.VapCurrentModifiedDate).HasColumnName("vap_current_modified_date");
            entity.Property(e => e.VapCurrentPid)
                .HasPrecision(3)
                .HasColumnName("vap_current_pid");
            entity.Property(e => e.VapCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("vap_current_status");
            entity.Property(e => e.VapCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("vap_current_user");
            entity.Property(e => e.VapId)
                .HasPrecision(12)
                .HasColumnName("vap_id");
            entity.Property(e => e.VapInterfaceFileName)
                .HasMaxLength(25)
                .HasColumnName("vap_interface_file_name");
            entity.Property(e => e.VapInterfaceFileTxn)
                .HasPrecision(4)
                .HasColumnName("vap_interface_file_txn");
            entity.Property(e => e.VapLocIdRequester)
                .HasPrecision(12)
                .HasColumnName("vap_loc_id_requester");
            entity.Property(e => e.VapNoOfAnimals)
                .HasPrecision(3)
                .HasColumnName("vap_no_of_animals");
            entity.Property(e => e.VapNoOfAnimalsNotCanc)
                .HasPrecision(3)
                .HasColumnName("vap_no_of_animals_not_canc");
            entity.Property(e => e.VapNumberCalfMovts)
                .HasPrecision(2)
                .HasColumnName("vap_number_calf_movts");
            entity.Property(e => e.VapReceiptDate).HasColumnName("vap_receipt_date");
            entity.Property(e => e.VapRequesterDate).HasColumnName("vap_requester_date");
            entity.Property(e => e.VapRequesterLocationRepd)
                .HasMaxLength(17)
                .HasColumnName("vap_requester_location_repd");
            entity.Property(e => e.VapSourceReference)
                .HasMaxLength(20)
                .HasColumnName("vap_source_reference");
            entity.Property(e => e.VapSourceType)
                .HasMaxLength(3)
                .HasColumnName("vap_source_type");
            entity.Property(e => e.VapTargetDate).HasColumnName("vap_target_date");
            entity.Property(e => e.VapVersion)
                .HasPrecision(6)
                .HasColumnName("vap_version");
            entity.Property(e => e.VapWurId)
                .HasPrecision(12)
                .HasColumnName("vap_wur_id");
        });

        modelBuilder.Entity<CtWebUser>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_web_users_pkey");

            entity.ToTable("ct_web_users", "cts_transactions");

            entity.HasIndex(e => e.WurAudDatetime, "ct_web_users_aud_datetime_idx");

            entity.HasIndex(e => e.WurAudId, "ct_web_users_aud_id_idx");

            entity.HasIndex(e => new { e.WurAudType, e.WurAudDatetime }, "ct_web_users_aud_type_datetime_idx");

            entity.HasIndex(e => e.WurAudType, "ct_web_users_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_web_users_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_web_users_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_web_users_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_web_users_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_web_users_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_web_users_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_web_users_record_type_idx");

            entity.HasIndex(e => e.WurId, "ct_web_users_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_web_users_trans_type_idx");

            entity.HasIndex(e => e.WurLprIdKeeper, "ct_web_users_wur_lpr_id_keeper_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
            entity.Property(e => e.WurAccessNumber)
                .HasMaxLength(12)
                .HasColumnName("wur_access_number");
            entity.Property(e => e.WurAddress2)
                .HasMaxLength(35)
                .HasColumnName("wur_address_2");
            entity.Property(e => e.WurAddress3)
                .HasMaxLength(35)
                .HasColumnName("wur_address_3");
            entity.Property(e => e.WurAddress4)
                .HasMaxLength(35)
                .HasColumnName("wur_address_4");
            entity.Property(e => e.WurAddress5)
                .HasMaxLength(35)
                .HasColumnName("wur_address_5");
            entity.Property(e => e.WurAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("wur_aud_datetime");
            entity.Property(e => e.WurAudId).HasColumnName("wur_aud_id");
            entity.Property(e => e.WurAudType).HasColumnName("wur_aud_type");
            entity.Property(e => e.WurBadLoginPerDayCount)
                .HasPrecision(3)
                .HasColumnName("wur_bad_login_per_day_count");
            entity.Property(e => e.WurBadLoginResetCount)
                .HasPrecision(3)
                .HasColumnName("wur_bad_login_reset_count");
            entity.Property(e => e.WurCurrentModifiedDate).HasColumnName("wur_current_modified_date");
            entity.Property(e => e.WurCurrentPid)
                .HasPrecision(3)
                .HasColumnName("wur_current_pid");
            entity.Property(e => e.WurCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("wur_current_status");
            entity.Property(e => e.WurCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("wur_current_user");
            entity.Property(e => e.WurEmailAddress)
                .HasMaxLength(100)
                .HasColumnName("wur_email_address");
            entity.Property(e => e.WurEncryptedPassword)
                .HasMaxLength(30)
                .HasColumnName("wur_encrypted_password");
            entity.Property(e => e.WurExpiryDate).HasColumnName("wur_expiry_date");
            entity.Property(e => e.WurId)
                .HasPrecision(12)
                .HasColumnName("wur_id");
            entity.Property(e => e.WurIssuedToIdentifier)
                .HasMaxLength(30)
                .HasColumnName("wur_issued_to_identifier");
            entity.Property(e => e.WurLprIdKeeper)
                .HasPrecision(12)
                .HasColumnName("wur_lpr_id_keeper");
            entity.Property(e => e.WurMobileNumber)
                .HasMaxLength(30)
                .HasColumnName("wur_mobile_number");
            entity.Property(e => e.WurPasswordFilename)
                .HasMaxLength(20)
                .HasColumnName("wur_password_filename");
            entity.Property(e => e.WurPasswordIssueFlag)
                .HasMaxLength(1)
                .HasColumnName("wur_password_issue_flag");
            entity.Property(e => e.WurPostCode)
                .HasMaxLength(10)
                .HasColumnName("wur_post_code");
            entity.Property(e => e.WurSecurityFilename)
                .HasMaxLength(20)
                .HasColumnName("wur_security_filename");
            entity.Property(e => e.WurStaffNumber)
                .HasMaxLength(7)
                .HasColumnName("wur_staff_number");
            entity.Property(e => e.WurTelephoneNumber)
                .HasMaxLength(30)
                .HasColumnName("wur_telephone_number");
            entity.Property(e => e.WurUserLocation)
                .HasMaxLength(35)
                .HasColumnName("wur_user_location");
            entity.Property(e => e.WurUserName)
                .HasMaxLength(60)
                .HasColumnName("wur_user_name");
            entity.Property(e => e.WurUserType)
                .HasMaxLength(2)
                .HasColumnName("wur_user_type");
            entity.Property(e => e.WurVersion)
                .HasPrecision(6)
                .HasColumnName("wur_version");
            entity.Property(e => e.WurWelshIndicator)
                .HasMaxLength(1)
                .HasColumnName("wur_welsh_indicator");
        });

        modelBuilder.Entity<CtWgAutoallocation>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_wg_autoallocations_pkey");

            entity.ToTable("ct_wg_autoallocations", "cts_transactions");

            entity.HasIndex(e => e.WgaAudDatetime, "ct_wg_autoallocations_aud_datetime_idx");

            entity.HasIndex(e => e.WgaAudId, "ct_wg_autoallocations_aud_id_idx");

            entity.HasIndex(e => new { e.WgaAudType, e.WgaAudDatetime }, "ct_wg_autoallocations_aud_type_datetime_idx");

            entity.HasIndex(e => e.WgaAudType, "ct_wg_autoallocations_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_wg_autoallocations_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_wg_autoallocations_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_wg_autoallocations_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_wg_autoallocations_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_wg_autoallocations_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_wg_autoallocations_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_wg_autoallocations_record_type_idx");

            entity.HasIndex(e => e.WgaId, "ct_wg_autoallocations_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_wg_autoallocations_trans_type_idx");

            entity.HasIndex(e => e.WgaRouId, "ct_wg_autoallocations_wga_rou_id_idx");

            entity.HasIndex(e => e.WgaWgpId, "ct_wg_autoallocations_wga_wgp_id_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
            entity.Property(e => e.WgaAllocation)
                .HasMaxLength(10)
                .HasColumnName("wga_allocation");
            entity.Property(e => e.WgaAssignment)
                .HasMaxLength(10)
                .HasColumnName("wga_assignment");
            entity.Property(e => e.WgaAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("wga_aud_datetime");
            entity.Property(e => e.WgaAudId).HasColumnName("wga_aud_id");
            entity.Property(e => e.WgaAudType).HasColumnName("wga_aud_type");
            entity.Property(e => e.WgaCurrentModifiedDate).HasColumnName("wga_current_modified_date");
            entity.Property(e => e.WgaCurrentPid)
                .HasPrecision(3)
                .HasColumnName("wga_current_pid");
            entity.Property(e => e.WgaCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("wga_current_status");
            entity.Property(e => e.WgaCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("wga_current_user");
            entity.Property(e => e.WgaId)
                .HasPrecision(12)
                .HasColumnName("wga_id");
            entity.Property(e => e.WgaRouId)
                .HasPrecision(12)
                .HasColumnName("wga_rou_id");
            entity.Property(e => e.WgaVersion)
                .HasPrecision(6)
                .HasColumnName("wga_version");
            entity.Property(e => e.WgaWgpId)
                .HasPrecision(12)
                .HasColumnName("wga_wgp_id");
        });

        modelBuilder.Entity<CtWgSuperAssignment>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_wg_super_assignments_pkey");

            entity.ToTable("ct_wg_super_assignments", "cts_transactions");

            entity.HasIndex(e => e.WsaAudDatetime, "ct_wg_super_assignments_aud_datetime_idx");

            entity.HasIndex(e => e.WsaAudId, "ct_wg_super_assignments_aud_id_idx");

            entity.HasIndex(e => new { e.WsaAudType, e.WsaAudDatetime }, "ct_wg_super_assignments_aud_type_datetime_idx");

            entity.HasIndex(e => e.WsaAudType, "ct_wg_super_assignments_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_wg_super_assignments_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_wg_super_assignments_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_wg_super_assignments_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_wg_super_assignments_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_wg_super_assignments_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_wg_super_assignments_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_wg_super_assignments_record_type_idx");

            entity.HasIndex(e => e.WsaId, "ct_wg_super_assignments_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_wg_super_assignments_trans_type_idx");

            entity.HasIndex(e => e.WsaRouId, "ct_wg_super_assignments_wsa_rou_id_idx");

            entity.HasIndex(e => e.WsaWgpIdAssigned, "ct_wg_super_assignments_wsa_wgp_id_assigned_idx");

            entity.HasIndex(e => e.WsaWgpIdCurrent, "ct_wg_super_assignments_wsa_wgp_id_current_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
            entity.Property(e => e.WsaAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("wsa_aud_datetime");
            entity.Property(e => e.WsaAudId).HasColumnName("wsa_aud_id");
            entity.Property(e => e.WsaAudType).HasColumnName("wsa_aud_type");
            entity.Property(e => e.WsaCurrentModifiedDate).HasColumnName("wsa_current_modified_date");
            entity.Property(e => e.WsaCurrentPid)
                .HasPrecision(3)
                .HasColumnName("wsa_current_pid");
            entity.Property(e => e.WsaCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("wsa_current_status");
            entity.Property(e => e.WsaCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("wsa_current_user");
            entity.Property(e => e.WsaId)
                .HasPrecision(12)
                .HasColumnName("wsa_id");
            entity.Property(e => e.WsaRouId)
                .HasPrecision(12)
                .HasColumnName("wsa_rou_id");
            entity.Property(e => e.WsaVersion)
                .HasPrecision(6)
                .HasColumnName("wsa_version");
            entity.Property(e => e.WsaWgpIdAssigned)
                .HasPrecision(12)
                .HasColumnName("wsa_wgp_id_assigned");
            entity.Property(e => e.WsaWgpIdCurrent)
                .HasPrecision(12)
                .HasColumnName("wsa_wgp_id_current");
        });

        modelBuilder.Entity<CtWgUserAssignment>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_wg_user_assignments_pkey");

            entity.ToTable("ct_wg_user_assignments", "cts_transactions");

            entity.HasIndex(e => e.WuaAudDatetime, "ct_wg_user_assignments_aud_datetime_idx");

            entity.HasIndex(e => e.WuaAudId, "ct_wg_user_assignments_aud_id_idx");

            entity.HasIndex(e => new { e.WuaAudType, e.WuaAudDatetime }, "ct_wg_user_assignments_aud_type_datetime_idx");

            entity.HasIndex(e => e.WuaAudType, "ct_wg_user_assignments_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_wg_user_assignments_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_wg_user_assignments_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_wg_user_assignments_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_wg_user_assignments_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_wg_user_assignments_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_wg_user_assignments_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_wg_user_assignments_record_type_idx");

            entity.HasIndex(e => e.WuaId, "ct_wg_user_assignments_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_wg_user_assignments_trans_type_idx");

            entity.HasIndex(e => e.WuaCusId, "ct_wg_user_assignments_wua_cus_id_idx");

            entity.HasIndex(e => e.WuaWgpId, "ct_wg_user_assignments_wua_wgp_id_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
            entity.Property(e => e.WuaAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("wua_aud_datetime");
            entity.Property(e => e.WuaAudId).HasColumnName("wua_aud_id");
            entity.Property(e => e.WuaAudType).HasColumnName("wua_aud_type");
            entity.Property(e => e.WuaCurrentModifiedDate).HasColumnName("wua_current_modified_date");
            entity.Property(e => e.WuaCurrentPid)
                .HasPrecision(3)
                .HasColumnName("wua_current_pid");
            entity.Property(e => e.WuaCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("wua_current_status");
            entity.Property(e => e.WuaCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("wua_current_user");
            entity.Property(e => e.WuaCusId)
                .HasPrecision(12)
                .HasColumnName("wua_cus_id");
            entity.Property(e => e.WuaFavouredWgInd)
                .HasMaxLength(1)
                .HasColumnName("wua_favoured_wg_ind");
            entity.Property(e => e.WuaId)
                .HasPrecision(12)
                .HasColumnName("wua_id");
            entity.Property(e => e.WuaVersion)
                .HasPrecision(6)
                .HasColumnName("wua_version");
            entity.Property(e => e.WuaWgContactInd)
                .HasMaxLength(1)
                .HasColumnName("wua_wg_contact_ind");
            entity.Property(e => e.WuaWgpId)
                .HasPrecision(12)
                .HasColumnName("wua_wgp_id");
        });

        modelBuilder.Entity<CtWorkgroup>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("ct_workgroups_pkey");

            entity.ToTable("ct_workgroups", "cts_transactions");

            entity.HasIndex(e => e.WgpAudDatetime, "ct_workgroups_aud_datetime_idx");

            entity.HasIndex(e => e.WgpAudId, "ct_workgroups_aud_id_idx");

            entity.HasIndex(e => new { e.WgpAudType, e.WgpAudDatetime }, "ct_workgroups_aud_type_datetime_idx");

            entity.HasIndex(e => e.WgpAudType, "ct_workgroups_aud_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RecordCount }, "ct_workgroups_file_record_count_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_workgroups_file_row_number_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.TransType }, "ct_workgroups_file_trans_type_idx");

            entity.HasIndex(e => new { e.CtsFileImportId, e.RowNumber }, "ct_workgroups_import_row_idx");

            entity.HasIndex(e => e.ImportedDate, "ct_workgroups_imported_date_idx");

            entity.HasIndex(e => e.RecordCount, "ct_workgroups_record_count_idx");

            entity.HasIndex(e => e.RecordType, "ct_workgroups_record_type_idx");

            entity.HasIndex(e => e.WgpId, "ct_workgroups_source_key_idx");

            entity.HasIndex(e => e.TransType, "ct_workgroups_trans_type_idx");

            entity.Property(e => e.TransId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trans_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.FakeData)
                .HasPrecision(1)
                .HasColumnName("fake_data");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransType).HasColumnName("trans_type");
            entity.Property(e => e.WgpActiveIndicator)
                .HasMaxLength(1)
                .HasColumnName("wgp_active_indicator");
            entity.Property(e => e.WgpAudDatetime)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("wgp_aud_datetime");
            entity.Property(e => e.WgpAudId).HasColumnName("wgp_aud_id");
            entity.Property(e => e.WgpAudType).HasColumnName("wgp_aud_type");
            entity.Property(e => e.WgpCurrentModifiedDate).HasColumnName("wgp_current_modified_date");
            entity.Property(e => e.WgpCurrentPid)
                .HasPrecision(3)
                .HasColumnName("wgp_current_pid");
            entity.Property(e => e.WgpCurrentStatus)
                .HasMaxLength(2)
                .HasColumnName("wgp_current_status");
            entity.Property(e => e.WgpCurrentUser)
                .HasMaxLength(10)
                .HasColumnName("wgp_current_user");
            entity.Property(e => e.WgpId)
                .HasPrecision(12)
                .HasColumnName("wgp_id");
            entity.Property(e => e.WgpLongName)
                .HasMaxLength(60)
                .HasColumnName("wgp_long_name");
            entity.Property(e => e.WgpPrinter)
                .HasMaxLength(50)
                .HasColumnName("wgp_printer");
            entity.Property(e => e.WgpReassignLock)
                .HasMaxLength(1)
                .HasColumnName("wgp_reassign_lock");
            entity.Property(e => e.WgpShortName)
                .HasMaxLength(20)
                .HasColumnName("wgp_short_name");
            entity.Property(e => e.WgpSummaryType)
                .HasMaxLength(2)
                .HasColumnName("wgp_summary_type");
            entity.Property(e => e.WgpVersion)
                .HasPrecision(6)
                .HasColumnName("wgp_version");
            entity.Property(e => e.WgpWorkgroup)
                .HasMaxLength(6)
                .HasColumnName("wgp_workgroup");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}