using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Core.Attributes;

namespace Cads.Cds.StorageBridge.Core.Domain.Enums;

public enum ImportDataType
{
    None,

    #region Address Tables
    [TableInfo("ct_addresses", SchemaName.Cts, "adr_id")]
    [TableInfo("ct_addresses", SchemaName.CtsTransactions, "trans_id")]
    CtAddresses,
    #endregion

    #region Animal Changes
    [TableInfo("ct_animal_changes", SchemaName.Cts, "ach_id")]
    [TableInfo("ct_animal_changes", SchemaName.CtsTransactions, "trans_id")]
    CtAnimalChanges,
    #endregion

    #region Animal Claims
    [TableInfo("ct_animal_claims", SchemaName.Cts, "anc_id")]
    [TableInfo("ct_animal_claims", SchemaName.CtsTransactions, "trans_id")]
    CtAnimalClaims,
    #endregion

    #region Animal Correction Summary Errors
    [TableInfo("ct_animal_corr_summ_errors", SchemaName.Cts, "ase_id")]
    [TableInfo("ct_animal_corr_summ_errors", SchemaName.CtsTransactions, "trans_id")]
    CtAnimalCorrSummErrors,
    #endregion

    #region Animal Correct Summaries
    [TableInfo("ct_animal_correct_summaries", SchemaName.Cts, "acs_id")]
    [TableInfo("ct_animal_correct_summaries", SchemaName.CtsTransactions, "trans_id")]
    CtAnimalCorrectSummaries,
    #endregion

    #region Animal Identifiers
    [TableInfo("ct_animal_identifiers", SchemaName.Cts, "aid_id")]
    [TableInfo("ct_animal_identifiers", SchemaName.CtsTransactions, "trans_id")]
    CtAnimalIdentifiers,
    #endregion

    #region Animal Relationships
    [TableInfo("ct_animal_relationships", SchemaName.Cts, "aar_id")]
    [TableInfo("ct_animal_relationships", SchemaName.CtsTransactions, "trans_id")]
    CtAnimalRelationships,
    #endregion

    #region Animal Statuses
    [TableInfo("ct_animal_statuses", SchemaName.Cts, "ast_id")]
    [TableInfo("ct_animal_statuses", SchemaName.CtsTransactions, "trans_id")]
    CtAnimalStatuses,
    #endregion

    #region Application Status
    [TableInfo("ct_applic_statuses", SchemaName.Cts, "aps_id")]
    [TableInfo("ct_applic_statuses", SchemaName.CtsTransactions, "trans_id")]
    CtApplicStatuses,
    #endregion

    #region Application Late Days
    [TableInfo("ct_application_late_days", SchemaName.Cts, "ald_id")]
    [TableInfo("ct_application_late_days", SchemaName.CtsTransactions, "trans_id")]
    CtApplicationLateDays,
    #endregion

    #region Batch Retention Configuration (Cts only)
    [TableInfo("ct_batch_retention_conf", SchemaName.Cts, "id")]
    CtBatchRetentionConf,
    #endregion

    #region Breeds (Cts only)
    [TableInfo("ct_breeds", SchemaName.Cts, "brd_id")]
    CtBreeds,
    #endregion

    #region CLA Extract
    [TableInfo("ct_cla_extract", SchemaName.Cts, "cle_id")]
    [TableInfo("ct_cla_extract", SchemaName.CtsTransactions, "trans_id")]
    CtClaExtract,
    #endregion

    #region CLA Extract Detail
    [TableInfo("ct_cla_extract_detail", SchemaName.Cts, "cld_id")]
    [TableInfo("ct_cla_extract_detail", SchemaName.CtsTransactions, "trans_id")]
    CtClaExtractDetail,
    #endregion

    #region CLA Extract DM
    [TableInfo("ct_cla_extract_dm", SchemaName.Cts, "cle_id")]
    [TableInfo("ct_cla_extract_dm", SchemaName.CtsTransactions, "trans_id")]
    CtClaExtractDm,
    #endregion

    #region CLA Mini Detail
    [TableInfo("ct_cla_mini_detail", SchemaName.Cts, "cld_id")]
    [TableInfo("ct_cla_mini_detail", SchemaName.CtsTransactions, "trans_id")]
    CtClaMiniDetail,
    #endregion

    #region CLA Mini Extract
    [TableInfo("ct_cla_mini_extract", SchemaName.Cts, "cle_id")]
    [TableInfo("ct_cla_mini_extract", SchemaName.CtsTransactions, "trans_id")]
    CtClaMiniExtract,
    #endregion

    #region Claim Statuses (Cts only)
    [TableInfo("ct_claim_statuses", SchemaName.Cts, "cls_id")]
    CtClaimStatuses,
    #endregion

    #region Claim Types (Cts only)
    [TableInfo("ct_claim_types", SchemaName.Cts, "clt_id")]
    CtClaimTypes,
    #endregion

    #region CM Authorities (Cts only)
    [TableInfo("ct_cm_authorities", SchemaName.Cts, "cma_id")]
    CtCmAuthorities,
    #endregion

    #region CM Measures Results
    [TableInfo("ct_cm_measures_results", SchemaName.Cts, "cmr_id")]
    [TableInfo("ct_cm_measures_results", SchemaName.CtsTransactions, "trans_id")]
    CtCmMeasuresResults,
    #endregion

    #region Communications Addresses
    [TableInfo("ct_comms_addresses", SchemaName.Cts, "coa_id")]
    [TableInfo("ct_comms_addresses", SchemaName.CtsTransactions, "trans_id")]
    CtCommsAddresses,
    #endregion

    #region Condition Variant Groupings (Cts only)
    [TableInfo("ct_cond_variant_groupings", SchemaName.Cts, "cvg_id")]
    CtCondVariantGroupings,
    #endregion

    #region Condition Activities (Cts only)
    [TableInfo("ct_condition_activities", SchemaName.Cts, "cac_id")]
    CtConditionActivities,
    #endregion

    #region Condition Marker Errors
    [TableInfo("ct_condition_marker_errors", SchemaName.Cts, "cme_id")]
    [TableInfo("ct_condition_marker_errors", SchemaName.CtsTransactions, "trans_id")]
    CtConditionMarkerErrors,
    #endregion

    #region Condition Markers
    [TableInfo("ct_condition_markers", SchemaName.Cts, "com_id")]
    [TableInfo("ct_condition_markers", SchemaName.CtsTransactions, "trans_id")]
    CtConditionMarkers,
    #endregion

    #region Condition Types (Cts only)
    [TableInfo("ct_condition_types", SchemaName.Cts, "cot_id")]
    CtConditionTypes,
    #endregion

    #region Condition Variants (Cts only)
    [TableInfo("ct_condition_variants", SchemaName.Cts, "cov_id")]
    CtConditionVariants,
    #endregion

    #region Conditions (Cts only)
    [TableInfo("ct_conditions", SchemaName.Cts, "con_id")]
    CtConditions,
    #endregion

    #region Counties (Cts only)
    [TableInfo("ct_counties", SchemaName.Cts, "cty_id")]
    CtCounties,
    #endregion

    #region Counties Migration (Cts only)
    [TableInfo("ct_counties_migration", SchemaName.Cts, "cty_id")]
    CtCountiesMigration,
    #endregion

    #region Countries (Cts only)
    [TableInfo("ct_countries", SchemaName.Cts, "cry_id")]
    CtCountries,
    #endregion

    #region CPS167 Report
    [TableInfo("ct_cps167_report", SchemaName.Cts, "kns_id")]
    [TableInfo("ct_cps167_report", SchemaName.CtsTransactions, "trans_id")]
    CtCps167Report,
    #endregion

    #region CTS164 Handshake File Keys (Cts only)
    [TableInfo("ct_cts164_handshake_file_keys", SchemaName.Cts, "id")]
    CtCts164HandshakeFileKeys,
    #endregion

    #region CTS Users
    [TableInfo("ct_cts_users", SchemaName.Cts, "cus_id")]
    [TableInfo("ct_cts_users", SchemaName.CtsTransactions, "trans_id")]
    CtCtsUsers,
    #endregion

    #region Eartag Formats (Cts only)
    [TableInfo("ct_eartag_formats", SchemaName.Cts, "etf_id")]
    CtEartagFormats,
    #endregion

    #region Eartag Reason Flags (Cts only)
    [TableInfo("ct_eartag_reason_flags", SchemaName.Cts, "erf_id")]
    CtEartagReasonFlags,
    #endregion

    #region Eartag Reasons (Cts only)
    [TableInfo("ct_eartag_reasons", SchemaName.Cts, "etr_id")]
    CtEartagReasons,
    #endregion

    #region Eartag Staging
    [TableInfo("ct_eartag_staging", SchemaName.Cts, "est_id")]
    [TableInfo("ct_eartag_staging", SchemaName.CtsTransactions, "trans_id")]
    CtEartagStaging,
    #endregion

    #region Eartag Types (Cts only)
    [TableInfo("ct_eartag_types", SchemaName.Cts, "ett_id")]
    CtEartagTypes,
    #endregion

    #region Eartags
    [TableInfo("ct_eartags", SchemaName.Cts, "etg_id")]
    [TableInfo("ct_eartags", SchemaName.CtsTransactions, "trans_id")]
    CtEartags,
    #endregion

    #region Electronic Identifiers
    [TableInfo("ct_electronic_identifiers", SchemaName.Cts, "eid_id")]
    [TableInfo("ct_electronic_identifiers", SchemaName.CtsTransactions, "trans_id")]
    CtElectronicIdentifiers,
    #endregion

    #region Email Log
    [TableInfo("ct_email_log", SchemaName.Cts, "eml_id")]
    [TableInfo("ct_email_log", SchemaName.CtsTransactions, "trans_id")]
    CtEmailLog,
    #endregion

    #region EReport Files
    [TableInfo("ct_ereport_files", SchemaName.Cts, "ere_id")]
    [TableInfo("ct_ereport_files", SchemaName.CtsTransactions, "trans_id")]
    CtEreportFiles,
    #endregion

    #region EReport Load Messages
    [TableInfo("ct_ereport_load_messages", SchemaName.Cts, "id")]
    [TableInfo("ct_ereport_load_messages", SchemaName.CtsTransactions, "trans_id")]
    CtEreportLoadMessages,
    #endregion

    #region EReport Locks
    [TableInfo("ct_ereport_locks", SchemaName.Cts, "id")]
    [TableInfo("ct_ereport_locks", SchemaName.CtsTransactions, "trans_id")]
    CtEreportLocks,
    #endregion

    #region EReport Process Messages
    [TableInfo("ct_ereport_process_messages", SchemaName.Cts, "id")]
    [TableInfo("ct_ereport_process_messages", SchemaName.CtsTransactions, "trans_id")]
    CtEreportProcessMessages,
    #endregion

    #region Ext CETD Eartag
    [TableInfo("ct_ext_cetd_eartag", SchemaName.Cts, "id")]
    [TableInfo("ct_ext_cetd_eartag", SchemaName.CtsTransactions, "trans_id")]
    CtExtCetdEartag,
    #endregion

    #region Ext NI District (Cts only)
    [TableInfo("ct_ext_ni_district", SchemaName.Cts, "id")]
    CtExtNiDistrict,
    #endregion

    #region Ext Special Herd (Cts only)
    [TableInfo("ct_ext_special_herd", SchemaName.Cts, "id")]
    CtExtSpecialHerd,
    #endregion

    #region File Layouts (Cts only)
    [TableInfo("ct_file_layouts", SchemaName.Cts, "id")]
    CtFileLayouts,
    #endregion

    #region HSF Sequences (Cts only)
    [TableInfo("ct_hsf_sequences", SchemaName.Cts, "id")]
    CtHsfSequences,
    #endregion

    #region Insert Update Log
    [TableInfo("ct_insert_update_log", SchemaName.Cts, "iul_id")]
    [TableInfo("ct_insert_update_log", SchemaName.CtsTransactions, "trans_id")]
    CtInsertUpdateLog,
    #endregion

    #region Issued Documents
    [TableInfo("ct_issued_documents", SchemaName.Cts, "ido_id")]
    [TableInfo("ct_issued_documents", SchemaName.CtsTransactions, "trans_id")]
    CtIssuedDocuments,
    #endregion

    #region Issuing Authorities (Cts only)
    [TableInfo("ct_issuing_authorities", SchemaName.Cts, "isa_id")]
    CtIssuingAuthorities,
    #endregion

    #region Label Requests
    [TableInfo("ct_label_requests", SchemaName.Cts, "lar_id")]
    [TableInfo("ct_label_requests", SchemaName.CtsTransactions, "trans_id")]
    CtLabelRequests,
    #endregion

    #region Label Summaries
    [TableInfo("ct_label_summaries", SchemaName.Cts, "las_id")]
    [TableInfo("ct_label_summaries", SchemaName.CtsTransactions, "trans_id")]
    CtLabelSummaries,
    #endregion

    #region Late Days (Cts only)
    [TableInfo("ct_late_days", SchemaName.Cts, "lda_id")]
    CtLateDays,
    #endregion

    #region Letters
    [TableInfo("ct_letters", SchemaName.Cts, "let_id")]
    [TableInfo("ct_letters", SchemaName.CtsTransactions, "trans_id")]
    CtLetters,
    #endregion

    #region Location Type Relationship Combinations (Cts only)
    [TableInfo("ct_loc_type_rel_combs", SchemaName.Cts, "lrc_id")]
    CtLocTypeRelCombs,
    #endregion

    #region Location ID Formats (Cts only)
    [TableInfo("ct_location_id_formats", SchemaName.Cts, "lif_id")]
    CtLocationIdFormats,
    #endregion

    #region Location Identifiers
    [TableInfo("ct_location_identifiers", SchemaName.Cts, "lid_id")]
    [TableInfo("ct_location_identifiers", SchemaName.CtsTransactions, "trans_id")]
    CtLocationIdentifiers,
    #endregion

    #region Location Party Relationship Types (Cts only)
    [TableInfo("ct_location_party_rel_types", SchemaName.Cts, "lpt_id")]
    CtLocationPartyRelTypes,
    #endregion

    #region Location Party Relationships
    [TableInfo("ct_location_party_rels", SchemaName.Cts, "lpr_id")]
    [TableInfo("ct_location_party_rels", SchemaName.CtsTransactions, "trans_id")]
    CtLocationPartyRels,
    #endregion

    #region Location Relationship Types (Cts only)
    [TableInfo("ct_location_rel_types", SchemaName.Cts, "lrt_id")]
    CtLocationRelTypes,
    #endregion

    #region Location Relationships
    [TableInfo("ct_location_relationships", SchemaName.Cts, "llr_id")]
    [TableInfo("ct_location_relationships", SchemaName.CtsTransactions, "trans_id")]
    CtLocationRelationships,
    #endregion

    #region Location Types (Cts only)
    [TableInfo("ct_location_types", SchemaName.Cts, "lty_id")]
    CtLocationTypes,
    #endregion

    #region Locations
    [TableInfo("ct_locations", SchemaName.Cts, "loc_id")]
    [TableInfo("ct_locations", SchemaName.CtsTransactions, "trans_id")]
    CtLocations,
    #endregion

    #region Locations Faker
    [TableInfo("ct_locations_faker", SchemaName.Cts, "id")]
    [TableInfo("ct_locations_faker", SchemaName.CtsTransactions, "trans_id")]
    CtLocationsFaker,
    #endregion

    #region Location Restrictions to Animals
    [TableInfo("ct_locrestrictionstoanimals", SchemaName.Cts, "id")]
    [TableInfo("ct_locrestrictionstoanimals", SchemaName.CtsTransactions, "trans_id")]
    CtLocrestrictionstoanimals,
    #endregion

    #region Management Control Errors
    [TableInfo("ct_mgt_control_errors", SchemaName.Cts, "mce_id")]
    [TableInfo("ct_mgt_control_errors", SchemaName.CtsTransactions, "trans_id")]
    CtMgtControlErrors,
    #endregion

    #region Management Working Group Allocation Rules (Cts only)
    [TableInfo("ct_mgt_wg_allocation_rules", SchemaName.Cts, "war_id")]
    CtMgtWgAllocationRules,
    #endregion

    #region MHS to CPH
    [TableInfo("ct_mhs_to_cph", SchemaName.Cts, "id")]
    [TableInfo("ct_mhs_to_cph", SchemaName.CtsTransactions, "trans_id")]
    CtMhsToCph,
    #endregion

    #region Movement History
    [TableInfo("ct_mov_hst", SchemaName.Cts, "id")]
    [TableInfo("ct_mov_hst", SchemaName.CtsTransactions, "trans_id")]
    CtMovHst,
    #endregion

    #region Movement Correction Summary Errors
    [TableInfo("ct_movt_corr_summ_errors", SchemaName.Cts, "mse_id")]
    [TableInfo("ct_movt_corr_summ_errors", SchemaName.CtsTransactions, "trans_id")]
    CtMovtCorrSummErrors,
    #endregion

    #region Movement Correct Summaries
    [TableInfo("ct_movt_correct_summaries", SchemaName.Cts, "mcs_id")]
    [TableInfo("ct_movt_correct_summaries", SchemaName.CtsTransactions, "trans_id")]
    CtMovtCorrectSummaries,
    #endregion

    #region Message Text (Cts only)
    [TableInfo("ct_msgtxt", SchemaName.Cts, "id")]
    CtMsgtxt,
    #endregion

    #region Non Working Days (Cts only)
    [TableInfo("ct_non_working_days", SchemaName.Cts, "nwd_id")]
    CtNonWorkingDays,
    #endregion

    #region Parameter Group (Cts only)
    [TableInfo("ct_param_group", SchemaName.Cts, "pgp_id")]
    CtParamGroup,
    #endregion

    #region Parameter Header (Cts only)
    [TableInfo("ct_param_header", SchemaName.Cts, "phd_id")]
    CtParamHeader,
    #endregion

    #region Parameter Value (Cts only)
    [TableInfo("ct_param_value", SchemaName.Cts, "pvl_id")]
    CtParamValue,
    #endregion

    #region Parameter Value Group (Cts only)
    [TableInfo("ct_param_value_group", SchemaName.Cts, "pvg_id")]
    CtParamValueGroup,
    #endregion

    #region Parties
    [TableInfo("ct_parties", SchemaName.Cts, "par_id")]
    [TableInfo("ct_parties", SchemaName.CtsTransactions, "trans_id")]
    CtParties,
    #endregion

    #region Parties Faker
    [TableInfo("ct_parties_faker", SchemaName.Cts, "id")]
    [TableInfo("ct_parties_faker", SchemaName.CtsTransactions, "trans_id")]
    CtPartiesFaker,
    #endregion

    #region PPAF Groupings
    [TableInfo("ct_ppaf_groupings", SchemaName.Cts, "ppg_id")]
    [TableInfo("ct_ppaf_groupings", SchemaName.CtsTransactions, "trans_id")]
    CtPpafGroupings,
    #endregion

    #region Preprinted Application Forms
    [TableInfo("ct_preprinted_appn_forms", SchemaName.Cts, "paf_id")]
    [TableInfo("ct_preprinted_appn_forms", SchemaName.CtsTransactions, "trans_id")]
    CtPreprintedAppnForms,
    #endregion

    #region Probity Checks (Cts only)
    [TableInfo("ct_probity_checks", SchemaName.Cts, "pch_id")]
    CtProbityChecks,
    #endregion

    #region PS9999 AHDB Data
    [TableInfo("ct_ps9999_ahdb_data", SchemaName.Cts, "ran_id")]
    [TableInfo("ct_ps9999_ahdb_data", SchemaName.CtsTransactions, "trans_id")]
    CtPs9999AhdbData,
    #endregion

    #region PS9999 AHDB Movement History
    [TableInfo("ct_ps9999_ahdb_mov_history", SchemaName.Cts, "id")]
    [TableInfo("ct_ps9999_ahdb_mov_history", SchemaName.CtsTransactions, "trans_id")]
    CtPs9999AhdbMovHistory,
    #endregion

    #region Received Application Errors
    [TableInfo("ct_recd_application_errors", SchemaName.Cts, "rae_id")]
    [TableInfo("ct_recd_application_errors", SchemaName.CtsTransactions, "trans_id")]
    CtRecdApplicationErrors,
    #endregion

    #region Received Movement Errors
    [TableInfo("ct_recd_movement_errors", SchemaName.Cts, "rme_id")]
    [TableInfo("ct_recd_movement_errors", SchemaName.CtsTransactions, "trans_id")]
    CtRecdMovementErrors,
    #endregion

    #region Received Applications
    [TableInfo("ct_received_applications", SchemaName.Cts, "rap_id")]
    [TableInfo("ct_received_applications", SchemaName.CtsTransactions, "trans_id")]
    CtReceivedApplications,
    #endregion

    #region Received Movements
    [TableInfo("ct_received_movements", SchemaName.Cts, "rmo_id")]
    [TableInfo("ct_received_movements", SchemaName.CtsTransactions, "trans_id")]
    CtReceivedMovements,
    #endregion

    #region Registered Animals
    [TableInfo("ct_registered_animals", SchemaName.Cts, "ran_id")]
    [TableInfo("ct_registered_animals", SchemaName.CtsTransactions, "trans_id")]
    CtRegisteredAnimals,
    #endregion

    #region Registered Movements
    [TableInfo("ct_registered_movements", SchemaName.Cts, "mov_id")]
    [TableInfo("ct_registered_movements", SchemaName.CtsTransactions, "trans_id")]
    CtRegisteredMovements,
    #endregion

    #region Reset to Extract
    [TableInfo("ct_reset_to_extract", SchemaName.Cts, "rte_id")]
    [TableInfo("ct_reset_to_extract", SchemaName.CtsTransactions, "trans_id")]
    CtResetToExtract,
    #endregion

    #region SBCS Extension
    [TableInfo("ct_sbcs_ext", SchemaName.Cts, "id")]
    [TableInfo("ct_sbcs_ext", SchemaName.CtsTransactions, "trans_id")]
    CtSbcsExt,
    #endregion

    #region Schemes (Cts only)
    [TableInfo("ct_schemes", SchemaName.Cts, "sch_id")]
    CtSchemes,
    #endregion

    #region Stage Files
    [TableInfo("ct_stage_files", SchemaName.Cts, "stf_id")]
    [TableInfo("ct_stage_files", SchemaName.CtsTransactions, "trans_id")]
    CtStageFiles,
    #endregion

    #region Stage Locks
    [TableInfo("ct_stage_locks", SchemaName.Cts, "id")]
    [TableInfo("ct_stage_locks", SchemaName.CtsTransactions, "trans_id")]
    CtStageLocks,
    #endregion

    #region Stage Messages
    [TableInfo("ct_stage_messages", SchemaName.Cts, "id")]
    [TableInfo("ct_stage_messages", SchemaName.CtsTransactions, "trans_id")]
    CtStageMessages,
    #endregion

    #region Sublocation Types (Cts only)
    [TableInfo("ct_sublocation_types", SchemaName.Cts, "slt_id")]
    CtSublocationTypes,
    #endregion

    #region Suspended Animal Errors
    [TableInfo("ct_susp_animal_errors", SchemaName.Cts, "sae_id")]
    [TableInfo("ct_susp_animal_errors", SchemaName.CtsTransactions, "trans_id")]
    CtSuspAnimalErrors,
    #endregion

    #region Suspended CM Measure Results
    [TableInfo("ct_susp_cm_measure_results", SchemaName.Cts, "smr_id")]
    [TableInfo("ct_susp_cm_measure_results", SchemaName.CtsTransactions, "trans_id")]
    CtSuspCmMeasureResults,
    #endregion

    #region Suspended Condition Markers
    [TableInfo("ct_susp_condition_markers", SchemaName.Cts, "scm_id")]
    [TableInfo("ct_susp_condition_markers", SchemaName.CtsTransactions, "trans_id")]
    CtSuspConditionMarkers,
    #endregion

    #region Suspended Movement Errors
    [TableInfo("ct_susp_movement_errors", SchemaName.Cts, "sme_id")]
    [TableInfo("ct_susp_movement_errors", SchemaName.CtsTransactions, "trans_id")]
    CtSuspMovementErrors,
    #endregion

    #region Suspended Animals
    [TableInfo("ct_suspended_animals", SchemaName.Cts, "san_id")]
    [TableInfo("ct_suspended_animals", SchemaName.CtsTransactions, "trans_id")]
    CtSuspendedAnimals,
    #endregion

    #region Suspended Movements
    [TableInfo("ct_suspended_movements", SchemaName.Cts, "smo_id")]
    [TableInfo("ct_suspended_movements", SchemaName.CtsTransactions, "trans_id")]
    CtSuspendedMovements,
    #endregion

    #region Suspense Character Allocation Rules (Cts only)
    [TableInfo("ct_suspense_char_alloc_rules", SchemaName.Cts, "sca_id")]
    CtSuspenseCharAllocRules,
    #endregion

    #region Suspense Working Group Allocation Rules (Cts only)
    [TableInfo("ct_suspense_wg_alloc_rules", SchemaName.Cts, "swa_id")]
    CtSuspenseWgAllocRules,
    #endregion

    #region Valid Applications
    [TableInfo("ct_valid_applications", SchemaName.Cts, "vap_id")]
    [TableInfo("ct_valid_applications", SchemaName.CtsTransactions, "trans_id")]
    CtValidApplications,
    #endregion

    #region Web Users
    [TableInfo("ct_web_users", SchemaName.Cts, "wur_id")]
    [TableInfo("ct_web_users", SchemaName.CtsTransactions, "trans_id")]
    CtWebUsers,
    #endregion

    #region Working Group Autoallocations
    [TableInfo("ct_wg_autoallocations", SchemaName.Cts, "wga_id")]
    [TableInfo("ct_wg_autoallocations", SchemaName.CtsTransactions, "trans_id")]
    CtWgAutoallocations,
    #endregion

    #region Working Group Super Assignments
    [TableInfo("ct_wg_super_assignments", SchemaName.Cts, "wsa_id")]
    [TableInfo("ct_wg_super_assignments", SchemaName.CtsTransactions, "trans_id")]
    CtWgSuperAssignments,
    #endregion

    #region Working Group User Assignments
    [TableInfo("ct_wg_user_assignments", SchemaName.Cts, "wua_id")]
    [TableInfo("ct_wg_user_assignments", SchemaName.CtsTransactions, "trans_id")]
    CtWgUserAssignments,
    #endregion

    #region Workgroups
    [TableInfo("ct_workgroups", SchemaName.Cts, "wgp_id")]
    [TableInfo("ct_workgroups", SchemaName.CtsTransactions, "trans_id")]
    CtWorkgroups
    #endregion
}