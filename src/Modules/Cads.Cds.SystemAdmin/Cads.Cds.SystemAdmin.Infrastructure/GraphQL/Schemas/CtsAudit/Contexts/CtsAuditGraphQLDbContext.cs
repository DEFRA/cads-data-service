using System;
using System.Collections.Generic;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsAudit.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsAudit.Contexts;

public partial class CtsAuditGraphQLDbContext : DbContext
{
    public CtsAuditGraphQLDbContext(DbContextOptions<CtsAuditGraphQLDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CtAddress> CtAddresses { get; set; }

    public virtual DbSet<CtAnimalChange> CtAnimalChanges { get; set; }

    public virtual DbSet<CtAnimalClaim> CtAnimalClaims { get; set; }

    public virtual DbSet<CtAnimalCorrSummError> CtAnimalCorrSummErrors { get; set; }

    public virtual DbSet<CtAnimalCorrectSummary> CtAnimalCorrectSummaries { get; set; }

    public virtual DbSet<CtAnimalIdentifier> CtAnimalIdentifiers { get; set; }

    public virtual DbSet<CtAnimalRelationship> CtAnimalRelationships { get; set; }

    public virtual DbSet<CtAnimalStatus> CtAnimalStatuses { get; set; }

    public virtual DbSet<CtApplicStatus> CtApplicStatuses { get; set; }

    public virtual DbSet<CtApplicationLateDay> CtApplicationLateDays { get; set; }

    public virtual DbSet<CtClaExtract> CtClaExtracts { get; set; }

    public virtual DbSet<CtClaExtractDetail> CtClaExtractDetails { get; set; }

    public virtual DbSet<CtClaExtractDm> CtClaExtractDms { get; set; }

    public virtual DbSet<CtClaMiniDetail> CtClaMiniDetails { get; set; }

    public virtual DbSet<CtClaMiniExtract> CtClaMiniExtracts { get; set; }

    public virtual DbSet<CtCmMeasuresResult> CtCmMeasuresResults { get; set; }

    public virtual DbSet<CtCommsAddress> CtCommsAddresses { get; set; }

    public virtual DbSet<CtConditionMarker> CtConditionMarkers { get; set; }

    public virtual DbSet<CtConditionMarkerError> CtConditionMarkerErrors { get; set; }

    public virtual DbSet<CtCps167Report> CtCps167Reports { get; set; }

    public virtual DbSet<CtCtsUser> CtCtsUsers { get; set; }

    public virtual DbSet<CtEartag> CtEartags { get; set; }

    public virtual DbSet<CtEartagStaging> CtEartagStagings { get; set; }

    public virtual DbSet<CtElectronicIdentifier> CtElectronicIdentifiers { get; set; }

    public virtual DbSet<CtEmailLog> CtEmailLogs { get; set; }

    public virtual DbSet<CtEreportFile> CtEreportFiles { get; set; }

    public virtual DbSet<CtEreportLoadMessage> CtEreportLoadMessages { get; set; }

    public virtual DbSet<CtEreportLock> CtEreportLocks { get; set; }

    public virtual DbSet<CtEreportProcessMessage> CtEreportProcessMessages { get; set; }

    public virtual DbSet<CtExtCetdEartag> CtExtCetdEartags { get; set; }

    public virtual DbSet<CtInsertUpdateLog> CtInsertUpdateLogs { get; set; }

    public virtual DbSet<CtIssuedDocument> CtIssuedDocuments { get; set; }

    public virtual DbSet<CtLabelRequest> CtLabelRequests { get; set; }

    public virtual DbSet<CtLabelSummary> CtLabelSummaries { get; set; }

    public virtual DbSet<CtLetter> CtLetters { get; set; }

    public virtual DbSet<CtLocation> CtLocations { get; set; }

    public virtual DbSet<CtLocationIdentifier> CtLocationIdentifiers { get; set; }

    public virtual DbSet<CtLocationPartyRel> CtLocationPartyRels { get; set; }

    public virtual DbSet<CtLocationRelationship> CtLocationRelationships { get; set; }

    public virtual DbSet<CtLocationsFaker> CtLocationsFakers { get; set; }

    public virtual DbSet<CtLocrestrictionstoanimal> CtLocrestrictionstoanimals { get; set; }

    public virtual DbSet<CtMgtControlError> CtMgtControlErrors { get; set; }

    public virtual DbSet<CtMhsToCph> CtMhsToCphs { get; set; }

    public virtual DbSet<CtMovHst> CtMovHsts { get; set; }

    public virtual DbSet<CtMovtCorrSummError> CtMovtCorrSummErrors { get; set; }

    public virtual DbSet<CtMovtCorrectSummary> CtMovtCorrectSummaries { get; set; }

    public virtual DbSet<CtPartiesFaker> CtPartiesFakers { get; set; }

    public virtual DbSet<CtParty> CtParties { get; set; }

    public virtual DbSet<CtPpafGrouping> CtPpafGroupings { get; set; }

    public virtual DbSet<CtPreprintedAppnForm> CtPreprintedAppnForms { get; set; }

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

    public virtual DbSet<CtStageFile> CtStageFiles { get; set; }

    public virtual DbSet<CtStageLock> CtStageLocks { get; set; }

    public virtual DbSet<CtStageMessage> CtStageMessages { get; set; }

    public virtual DbSet<CtSuspAnimalError> CtSuspAnimalErrors { get; set; }

    public virtual DbSet<CtSuspCmMeasureResult> CtSuspCmMeasureResults { get; set; }

    public virtual DbSet<CtSuspConditionMarker> CtSuspConditionMarkers { get; set; }

    public virtual DbSet<CtSuspMovementError> CtSuspMovementErrors { get; set; }

    public virtual DbSet<CtSuspendedAnimal> CtSuspendedAnimals { get; set; }

    public virtual DbSet<CtSuspendedMovement> CtSuspendedMovements { get; set; }

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
            entity.HasKey(e => e.AuditId).HasName("ct_addresses_pkey");

            entity.ToTable("ct_addresses", "cts_audit");

            entity.HasIndex(e => e.AdrId, "ct_addresses_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
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
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtAnimalChange>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_animal_changes_pkey");

            entity.ToTable("ct_animal_changes", "cts_audit");

            entity.HasIndex(e => e.AchId, "ct_animal_changes_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
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
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtAnimalClaim>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_animal_claims_pkey");

            entity.ToTable("ct_animal_claims", "cts_audit");

            entity.HasIndex(e => e.AncId, "ct_animal_claims_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
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
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtAnimalCorrSummError>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_animal_corr_summ_errors_pkey");

            entity.ToTable("ct_animal_corr_summ_errors", "cts_audit");

            entity.HasIndex(e => e.AseId, "ct_animal_corr_summ_errors_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AseAcsId)
                .HasPrecision(12)
                .HasColumnName("ase_acs_id");
            entity.Property(e => e.AseAttributeName)
                .HasMaxLength(30)
                .HasColumnName("ase_attribute_name");
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
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtAnimalCorrectSummary>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_animal_correct_summaries_pkey");

            entity.ToTable("ct_animal_correct_summaries", "cts_audit");

            entity.HasIndex(e => e.AcsId, "ct_animal_correct_summaries_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AcsAmendRetagInd)
                .HasMaxLength(1)
                .HasColumnName("acs_amend_retag_ind");
            entity.Property(e => e.AcsApplicationType)
                .HasMaxLength(1)
                .HasColumnName("acs_application_type");
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
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtAnimalIdentifier>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_animal_identifiers_pkey");

            entity.ToTable("ct_animal_identifiers", "cts_audit");

            entity.HasIndex(e => e.AidId, "ct_animal_identifiers_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AidAidIdOriginal)
                .HasPrecision(12)
                .HasColumnName("aid_aid_id_original");
            entity.Property(e => e.AidAidIdPrevious)
                .HasPrecision(12)
                .HasColumnName("aid_aid_id_previous");
            entity.Property(e => e.AidAssignedLocationRepd)
                .HasMaxLength(17)
                .HasColumnName("aid_assigned_location_repd");
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
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtAnimalRelationship>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_animal_relationships_pkey");

            entity.ToTable("ct_animal_relationships", "cts_audit");

            entity.HasIndex(e => e.AarId, "ct_animal_relationships_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
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
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtAnimalStatus>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_animal_statuses_pkey");

            entity.ToTable("ct_animal_statuses", "cts_audit");

            entity.HasIndex(e => e.AstId, "ct_animal_statuses_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AstAddMoves)
                .HasPrecision(4)
                .HasColumnName("ast_add_moves");
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
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtApplicStatus>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_applic_statuses_pkey");

            entity.ToTable("ct_applic_statuses", "cts_audit");

            entity.HasIndex(e => e.ApsId, "ct_applic_statuses_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
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
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtApplicationLateDay>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_application_late_days_pkey");

            entity.ToTable("ct_application_late_days", "cts_audit");

            entity.HasIndex(e => e.AldId, "ct_application_late_days_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AldAdditionalDaysLate)
                .HasPrecision(3)
                .HasColumnName("ald_additional_days_late");
            entity.Property(e => e.AldApplicationType)
                .HasMaxLength(2)
                .HasColumnName("ald_application_type");
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
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtClaExtract>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_cla_extract_pkey");

            entity.ToTable("ct_cla_extract", "cts_audit");

            entity.HasIndex(e => e.CleId, "ct_cla_extract_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtClaExtractDetail>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_cla_extract_detail_pkey");

            entity.ToTable("ct_cla_extract_detail", "cts_audit");

            entity.HasIndex(e => e.CldId, "ct_cla_extract_detail_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.CldRecordCount)
                .HasPrecision(12)
                .HasColumnName("cld_record_count");
            entity.Property(e => e.CldRunEnd).HasColumnName("cld_run_end");
            entity.Property(e => e.CldRunStart).HasColumnName("cld_run_start");
            entity.Property(e => e.CldTableName)
                .HasMaxLength(30)
                .HasColumnName("cld_table_name");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtClaExtractDm>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_cla_extract_dm_pkey");

            entity.ToTable("ct_cla_extract_dm", "cts_audit");

            entity.HasIndex(e => e.CleId, "ct_cla_extract_dm_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtClaMiniDetail>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_cla_mini_detail_pkey");

            entity.ToTable("ct_cla_mini_detail", "cts_audit");

            entity.HasIndex(e => e.CldId, "ct_cla_mini_detail_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.CldRecordCount)
                .HasPrecision(12)
                .HasColumnName("cld_record_count");
            entity.Property(e => e.CldRunEnd).HasColumnName("cld_run_end");
            entity.Property(e => e.CldRunStart).HasColumnName("cld_run_start");
            entity.Property(e => e.CldTableName)
                .HasMaxLength(30)
                .HasColumnName("cld_table_name");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtClaMiniExtract>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_cla_mini_extract_pkey");

            entity.ToTable("ct_cla_mini_extract", "cts_audit");

            entity.HasIndex(e => e.CleId, "ct_cla_mini_extract_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtCmMeasuresResult>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_cm_measures_results_pkey");

            entity.ToTable("ct_cm_measures_results", "cts_audit");

            entity.HasIndex(e => e.CmrId, "ct_cm_measures_results_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
            entity.Property(e => e.RecordCount).HasColumnName("record_count");
            entity.Property(e => e.RecordType)
                .HasMaxLength(1)
                .HasColumnName("record_type");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtCommsAddress>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_comms_addresses_pkey");

            entity.ToTable("ct_comms_addresses", "cts_audit");

            entity.HasIndex(e => e.CoaId, "ct_comms_addresses_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.CoaAttachment)
                .HasMaxLength(1)
                .HasColumnName("coa_attachment");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtConditionMarker>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_condition_markers_pkey");

            entity.ToTable("ct_condition_markers", "cts_audit");

            entity.HasIndex(e => e.ComId, "ct_condition_markers_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.ComAmendmentReasonCode)
                .HasMaxLength(3)
                .HasColumnName("com_amendment_reason_code");
            entity.Property(e => e.ComAmendmentReasonText)
                .HasMaxLength(60)
                .HasColumnName("com_amendment_reason_text");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtConditionMarkerError>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_condition_marker_errors_pkey");

            entity.ToTable("ct_condition_marker_errors", "cts_audit");

            entity.HasIndex(e => e.CmeId, "ct_condition_marker_errors_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.CmeAttributeName)
                .HasMaxLength(30)
                .HasColumnName("cme_attribute_name");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtCps167Report>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_cps167_report_pkey");

            entity.ToTable("ct_cps167_report", "cts_audit");

            entity.HasIndex(e => e.KnsId, "ct_cps167_report_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.KnsActionType)
                .HasMaxLength(5)
                .HasColumnName("kns_action_type");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtCtsUser>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_cts_users_pkey");

            entity.ToTable("ct_cts_users", "cts_audit");

            entity.HasIndex(e => e.CusId, "ct_cts_users_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.CusAccessGroup)
                .HasMaxLength(3)
                .HasColumnName("cus_access_group");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtEartag>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_eartags_pkey");

            entity.ToTable("ct_eartags", "cts_audit");

            entity.HasIndex(e => e.EtgId, "ct_eartags_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtEartagStaging>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_eartag_staging_pkey");

            entity.ToTable("ct_eartag_staging", "cts_audit");

            entity.HasIndex(e => e.EstId, "ct_eartag_staging_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtElectronicIdentifier>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_electronic_identifiers_pkey");

            entity.ToTable("ct_electronic_identifiers", "cts_audit");

            entity.HasIndex(e => e.EidId, "ct_electronic_identifiers_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtEmailLog>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_email_log_pkey");

            entity.ToTable("ct_email_log", "cts_audit");

            entity.HasIndex(e => e.EmlId, "ct_email_log_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtEreportFile>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_ereport_files_pkey");

            entity.ToTable("ct_ereport_files", "cts_audit");

            entity.HasIndex(e => e.EreId, "ct_ereport_files_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtEreportLoadMessage>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_ereport_load_messages_pkey");

            entity.ToTable("ct_ereport_load_messages", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtEreportLock>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_ereport_locks_pkey");

            entity.ToTable("ct_ereport_locks", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtEreportProcessMessage>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_ereport_process_messages_pkey");

            entity.ToTable("ct_ereport_process_messages", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.ErqDelayPeriod)
                .HasPrecision(10)
                .HasColumnName("erq_delay_period");
            entity.Property(e => e.ErqFileType)
                .HasMaxLength(3)
                .HasColumnName("erq_file_type");
            entity.Property(e => e.ErqSleepPeriod)
                .HasPrecision(10)
                .HasColumnName("erq_sleep_period");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtExtCetdEartag>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_ext_cetd_eartag_pkey");

            entity.ToTable("ct_ext_cetd_eartag", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtInsertUpdateLog>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_insert_update_log_pkey");

            entity.ToTable("ct_insert_update_log", "cts_audit");

            entity.HasIndex(e => e.IulId, "ct_insert_update_log_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtIssuedDocument>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_issued_documents_pkey");

            entity.ToTable("ct_issued_documents", "cts_audit");

            entity.HasIndex(e => e.IdoId, "ct_issued_documents_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtLabelRequest>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_label_requests_pkey");

            entity.ToTable("ct_label_requests", "cts_audit");

            entity.HasIndex(e => e.LarId, "ct_label_requests_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtLabelSummary>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_label_summaries_pkey");

            entity.ToTable("ct_label_summaries", "cts_audit");

            entity.HasIndex(e => e.LasId, "ct_label_summaries_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtLetter>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_letters_pkey");

            entity.ToTable("ct_letters", "cts_audit");

            entity.HasIndex(e => e.LetId, "ct_letters_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtLocation>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_locations_pkey");

            entity.ToTable("ct_locations", "cts_audit");

            entity.HasIndex(e => e.LocId, "ct_locations_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.FakeData)
                .HasPrecision(1)
                .HasColumnName("fake_data");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtLocationIdentifier>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_location_identifiers_pkey");

            entity.ToTable("ct_location_identifiers", "cts_audit");

            entity.HasIndex(e => e.LidId, "ct_location_identifiers_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtLocationPartyRel>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_location_party_rels_pkey");

            entity.ToTable("ct_location_party_rels", "cts_audit");

            entity.HasIndex(e => e.LprId, "ct_location_party_rels_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtLocationRelationship>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_location_relationships_pkey");

            entity.ToTable("ct_location_relationships", "cts_audit");

            entity.HasIndex(e => e.LlrId, "ct_location_relationships_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtLocationsFaker>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_locations_faker_pkey");

            entity.ToTable("ct_locations_faker", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.LocComments).HasColumnName("loc_comments");
            entity.Property(e => e.LocEmailAddress).HasColumnName("loc_email_address");
            entity.Property(e => e.LocFaxNumber).HasColumnName("loc_fax_number");
            entity.Property(e => e.LocMobileNumber).HasColumnName("loc_mobile_number");
            entity.Property(e => e.LocSourceReference).HasColumnName("loc_source_reference");
            entity.Property(e => e.LocTelNumber).HasColumnName("loc_tel_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtLocrestrictionstoanimal>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_locrestrictionstoanimals_pkey");

            entity.ToTable("ct_locrestrictionstoanimals", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtMgtControlError>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_mgt_control_errors_pkey");

            entity.ToTable("ct_mgt_control_errors", "cts_audit");

            entity.HasIndex(e => e.MceId, "ct_mgt_control_errors_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtMhsToCph>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_mhs_to_cph_pkey");

            entity.ToTable("ct_mhs_to_cph", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.Cph)
                .HasMaxLength(14)
                .HasColumnName("cph");
            entity.Property(e => e.MhsNumber)
                .HasPrecision(4)
                .HasColumnName("mhs_number");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtMovHst>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_mov_hst_pkey");

            entity.ToTable("ct_mov_hst", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtMovtCorrSummError>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_movt_corr_summ_errors_pkey");

            entity.ToTable("ct_movt_corr_summ_errors", "cts_audit");

            entity.HasIndex(e => e.MseId, "ct_movt_corr_summ_errors_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.MseAttributeName)
                .HasMaxLength(30)
                .HasColumnName("mse_attribute_name");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtMovtCorrectSummary>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_movt_correct_summaries_pkey");

            entity.ToTable("ct_movt_correct_summaries", "cts_audit");

            entity.HasIndex(e => e.McsId, "ct_movt_correct_summaries_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtPartiesFaker>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_parties_faker_pkey");

            entity.ToTable("ct_parties_faker", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.ParEmailAddress).HasColumnName("par_email_address");
            entity.Property(e => e.ParInitials).HasColumnName("par_initials");
            entity.Property(e => e.ParMobileNumber).HasColumnName("par_mobile_number");
            entity.Property(e => e.ParSurname).HasColumnName("par_surname");
            entity.Property(e => e.ParTelNumber).HasColumnName("par_tel_number");
            entity.Property(e => e.ParTitle).HasColumnName("par_title");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtParty>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_parties_pkey");

            entity.ToTable("ct_parties", "cts_audit");

            entity.HasIndex(e => e.ParId, "ct_parties_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.ImportedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("imported_date");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtPpafGrouping>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_ppaf_groupings_pkey");

            entity.ToTable("ct_ppaf_groupings", "cts_audit");

            entity.HasIndex(e => e.PpgId, "ct_ppaf_groupings_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtPreprintedAppnForm>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_preprinted_appn_forms_pkey");

            entity.ToTable("ct_preprinted_appn_forms", "cts_audit");

            entity.HasIndex(e => e.PafId, "ct_preprinted_appn_forms_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtPs9999AhdbDatum>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_ps9999_ahdb_data_pkey");

            entity.ToTable("ct_ps9999_ahdb_data", "cts_audit");

            entity.HasIndex(e => e.RanId, "ct_ps9999_ahdb_data_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AnimalEartag)
                .HasMaxLength(50)
                .HasColumnName("animal_eartag");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.BreedCode)
                .HasMaxLength(5)
                .HasColumnName("breed_code");
            entity.Property(e => e.CurrentCph)
                .HasMaxLength(14)
                .HasColumnName("current_cph");
            entity.Property(e => e.RanId).HasColumnName("ran_id");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SexOfAnimal)
                .HasMaxLength(1)
                .HasColumnName("sex_of_animal");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtPs9999AhdbMovHistory>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_ps9999_ahdb_mov_history_pkey");

            entity.ToTable("ct_ps9999_ahdb_mov_history", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.LocFullIdentifier)
                .HasMaxLength(14)
                .HasColumnName("loc_full_identifier");
            entity.Property(e => e.LocId).HasColumnName("loc_id");
            entity.Property(e => e.OffDate).HasColumnName("off_date");
            entity.Property(e => e.OnDate).HasColumnName("on_date");
            entity.Property(e => e.RanId).HasColumnName("ran_id");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtRecdApplicationError>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_recd_application_errors_pkey");

            entity.ToTable("ct_recd_application_errors", "cts_audit");

            entity.HasIndex(e => e.RaeId, "ct_recd_application_errors_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RaeAttributeName)
                .HasMaxLength(30)
                .HasColumnName("rae_attribute_name");
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
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtRecdMovementError>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_recd_movement_errors_pkey");

            entity.ToTable("ct_recd_movement_errors", "cts_audit");

            entity.HasIndex(e => e.RmeId, "ct_recd_movement_errors_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RmeAttributeName)
                .HasMaxLength(30)
                .HasColumnName("rme_attribute_name");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtReceivedApplication>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_received_applications_pkey");

            entity.ToTable("ct_received_applications", "cts_audit");

            entity.HasIndex(e => e.RapId, "ct_received_applications_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtReceivedMovement>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_received_movements_pkey");

            entity.ToTable("ct_received_movements", "cts_audit");

            entity.HasIndex(e => e.RmoId, "ct_received_movements_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtRegisteredAnimal>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_registered_animals_pkey");

            entity.ToTable("ct_registered_animals", "cts_audit");

            entity.HasIndex(e => e.RanId, "ct_registered_animals_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtRegisteredMovement>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_registered_movements_pkey");

            entity.ToTable("ct_registered_movements", "cts_audit");

            entity.HasIndex(e => e.MovId, "ct_registered_movements_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtResetToExtract>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_reset_to_extract_pkey");

            entity.ToTable("ct_reset_to_extract", "cts_audit");

            entity.HasIndex(e => e.RteId, "ct_reset_to_extract_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtSbcsExt>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_sbcs_ext_pkey");

            entity.ToTable("ct_sbcs_ext", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SxtId)
                .HasMaxLength(20)
                .HasColumnName("sxt_id");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtStageFile>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_stage_files_pkey");

            entity.ToTable("ct_stage_files", "cts_audit");

            entity.HasIndex(e => e.StfId, "ct_stage_files_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtStageLock>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_stage_locks_pkey");

            entity.ToTable("ct_stage_locks", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtStageMessage>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_stage_messages_pkey");

            entity.ToTable("ct_stage_messages", "cts_audit");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtSuspAnimalError>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_susp_animal_errors_pkey");

            entity.ToTable("ct_susp_animal_errors", "cts_audit");

            entity.HasIndex(e => e.SaeId, "ct_susp_animal_errors_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SaeAttributeName)
                .HasMaxLength(30)
                .HasColumnName("sae_attribute_name");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtSuspCmMeasureResult>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_susp_cm_measure_results_pkey");

            entity.ToTable("ct_susp_cm_measure_results", "cts_audit");

            entity.HasIndex(e => e.SmrId, "ct_susp_cm_measure_results_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtSuspConditionMarker>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_susp_condition_markers_pkey");

            entity.ToTable("ct_susp_condition_markers", "cts_audit");

            entity.HasIndex(e => e.ScmId, "ct_susp_condition_markers_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtSuspMovementError>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_susp_movement_errors_pkey");

            entity.ToTable("ct_susp_movement_errors", "cts_audit");

            entity.HasIndex(e => e.SmeId, "ct_susp_movement_errors_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SmeAttributeName)
                .HasMaxLength(30)
                .HasColumnName("sme_attribute_name");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtSuspendedAnimal>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_suspended_animals_pkey");

            entity.ToTable("ct_suspended_animals", "cts_audit");

            entity.HasIndex(e => e.SanId, "ct_suspended_animals_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtSuspendedMovement>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_suspended_movements_pkey");

            entity.ToTable("ct_suspended_movements", "cts_audit");

            entity.HasIndex(e => e.SmoId, "ct_suspended_movements_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
        });

        modelBuilder.Entity<CtValidApplication>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("ct_valid_applications_pkey");

            entity.ToTable("ct_valid_applications", "cts_audit");

            entity.HasIndex(e => e.VapId, "ct_valid_applications_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
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
            entity.Property(e => e.TransId).HasColumnName("trans_id");
            entity.Property(e => e.VapApplicationType)
                .HasMaxLength(1)
                .HasColumnName("vap_application_type");
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
            entity.HasKey(e => e.AuditId).HasName("ct_web_users_pkey");

            entity.ToTable("ct_web_users", "cts_audit");

            entity.HasIndex(e => e.WurId, "ct_web_users_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
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
            entity.HasKey(e => e.AuditId).HasName("ct_wg_autoallocations_pkey");

            entity.ToTable("ct_wg_autoallocations", "cts_audit");

            entity.HasIndex(e => e.WgaId, "ct_wg_autoallocations_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
            entity.Property(e => e.WgaAllocation)
                .HasMaxLength(10)
                .HasColumnName("wga_allocation");
            entity.Property(e => e.WgaAssignment)
                .HasMaxLength(10)
                .HasColumnName("wga_assignment");
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
            entity.HasKey(e => e.AuditId).HasName("ct_wg_super_assignments_pkey");

            entity.ToTable("ct_wg_super_assignments", "cts_audit");

            entity.HasIndex(e => e.WsaId, "ct_wg_super_assignments_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
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
            entity.HasKey(e => e.AuditId).HasName("ct_wg_user_assignments_pkey");

            entity.ToTable("ct_wg_user_assignments", "cts_audit");

            entity.HasIndex(e => e.WuaId, "ct_wg_user_assignments_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
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
            entity.HasKey(e => e.AuditId).HasName("ct_workgroups_pkey");

            entity.ToTable("ct_workgroups", "cts_audit");

            entity.HasIndex(e => e.WgpId, "ct_workgroups_source_key_idx");

            entity.Property(e => e.AuditId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_id");
            entity.Property(e => e.AuditAction).HasColumnName("audit_action");
            entity.Property(e => e.AuditTransId).HasColumnName("audit_trans_id");
            entity.Property(e => e.AuditedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("audited_at");
            entity.Property(e => e.FakeData)
                .HasPrecision(1)
                .HasColumnName("fake_data");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.TransId).HasColumnName("trans_id");
            entity.Property(e => e.WgpActiveIndicator)
                .HasMaxLength(1)
                .HasColumnName("wgp_active_indicator");
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
