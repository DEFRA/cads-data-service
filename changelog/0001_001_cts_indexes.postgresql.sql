-- liquibase formatted sql

-- changeset schema:0001-001-cts-indexes splitStatements:false
CREATE INDEX IF NOT EXISTS ct_animal_changes_ach_loc_id_doc_issued_idx
    ON cts.ct_animal_changes (ach_loc_id_doc_issued);
CREATE INDEX IF NOT EXISTS ct_animal_changes_ach_mov_id_death_cancel_idx
    ON cts.ct_animal_changes (ach_mov_id_death_cancel);
CREATE INDEX IF NOT EXISTS ct_animal_changes_ach_ran_id_doc_issued_idx
    ON cts.ct_animal_changes (ach_ran_id_doc_issued);
CREATE INDEX IF NOT EXISTS ct_animal_claims_anc_cls_id_idx
    ON cts.ct_animal_claims (anc_cls_id);
CREATE INDEX IF NOT EXISTS ct_animal_claims_anc_clt_id_idx
    ON cts.ct_animal_claims (anc_clt_id);
CREATE INDEX IF NOT EXISTS ct_animal_claims_anc_ran_id_idx
    ON cts.ct_animal_claims (anc_ran_id);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_ase_acs_id_idx
    ON cts.ct_animal_corr_summ_errors (ase_acs_id);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_acs_ran_id_idx
    ON cts.ct_animal_correct_summaries (acs_ran_id);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_acs_rap_id_idx
    ON cts.ct_animal_correct_summaries (acs_rap_id);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_acs_san_id_idx
    ON cts.ct_animal_correct_summaries (acs_san_id);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_acs_vap_id_idx
    ON cts.ct_animal_correct_summaries (acs_vap_id);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_aid_id_original_idx
    ON cts.ct_animal_identifiers (aid_aid_id_original);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_aid_id_previous_idx
    ON cts.ct_animal_identifiers (aid_aid_id_previous);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_eid_id_idx
    ON cts.ct_animal_identifiers (aid_eid_id);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_etg_id_idx
    ON cts.ct_animal_identifiers (aid_etg_id);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_loc_id_assigned_idx
    ON cts.ct_animal_identifiers (aid_loc_id_assigned);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_ran_id_idx
    ON cts.ct_animal_identifiers (aid_ran_id);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_aar_loc_id_idx
    ON cts.ct_animal_relationships (aar_loc_id);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_aar_ran_id_child_idx
    ON cts.ct_animal_relationships (aar_ran_id_child);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_aar_ran_id_parent_idx
    ON cts.ct_animal_relationships (aar_ran_id_parent);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_ast_ran_id_idx
    ON cts.ct_animal_statuses (ast_ran_id);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_aps_vap_id_idx
    ON cts.ct_applic_statuses (aps_vap_id);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_cld_cle_id_idx
    ON cts.ct_cla_extract_detail (cld_cle_id);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_cld_cle_id_idx
    ON cts.ct_cla_mini_detail (cld_cle_id);
CREATE INDEX IF NOT EXISTS ct_claim_types_clt_sch_id_idx
    ON cts.ct_claim_types (clt_sch_id);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_cma_cot_id_idx
    ON cts.ct_cm_authorities (cma_cot_id);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_cmr_com_id_idx
    ON cts.ct_cm_measures_results (cmr_com_id);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_cvg_cov_id_idx
    ON cts.ct_cond_variant_groupings (cvg_cov_id);
CREATE INDEX IF NOT EXISTS ct_condition_activities_cac_con_id_idx
    ON cts.ct_condition_activities (cac_con_id);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_cme_scm_id_idx
    ON cts.ct_condition_marker_errors (cme_scm_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_com_cac_id_idx
    ON cts.ct_condition_markers (com_cac_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_com_cma_id_idx
    ON cts.ct_condition_markers (com_cma_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_com_cov_id_idx
    ON cts.ct_condition_markers (com_cov_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_com_loc_id_idx
    ON cts.ct_condition_markers (com_loc_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_com_mov_id_idx
    ON cts.ct_condition_markers (com_mov_id);
CREATE INDEX IF NOT EXISTS ct_condition_variants_cov_con_id_idx
    ON cts.ct_condition_variants (cov_con_id);
CREATE INDEX IF NOT EXISTS ct_conditions_con_cot_id_idx
    ON cts.ct_conditions (con_cot_id);
CREATE INDEX IF NOT EXISTS ct_conditions_con_pch_id_idx
    ON cts.ct_conditions (con_pch_id);
CREATE INDEX IF NOT EXISTS ct_countries_cry_cry_id_main_eu_idx
    ON cts.ct_countries (cry_cry_id_main_eu);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_erf_etr_id_idx
    ON cts.ct_eartag_reason_flags (erf_etr_id);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_est_erf_id_idx
    ON cts.ct_eartag_staging (est_erf_id);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_est_loc_id_order_idx
    ON cts.ct_eartag_staging (est_loc_id_order);
CREATE INDEX IF NOT EXISTS ct_eartag_types_ett_etf_id_idx
    ON cts.ct_eartag_types (ett_etf_id);
CREATE INDEX IF NOT EXISTS ct_eartags_etg_erf_id_idx
    ON cts.ct_eartags (etg_erf_id);
CREATE INDEX IF NOT EXISTS ct_eartags_etg_ett_id_idx
    ON cts.ct_eartags (etg_ett_id);
CREATE INDEX IF NOT EXISTS ct_eartags_etg_loc_id_order_idx
    ON cts.ct_eartags (etg_loc_id_order);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_eid_isa_id_idx
    ON cts.ct_electronic_identifiers (eid_isa_id);
CREATE INDEX IF NOT EXISTS ct_issued_documents_ido_loc_id_idx
    ON cts.ct_issued_documents (ido_loc_id);
CREATE INDEX IF NOT EXISTS ct_issued_documents_ido_ran_id_idx
    ON cts.ct_issued_documents (ido_ran_id);
CREATE INDEX IF NOT EXISTS ct_label_requests_lar_las_id_idx
    ON cts.ct_label_requests (lar_las_id);
CREATE INDEX IF NOT EXISTS ct_label_summaries_las_loc_id_identifying_idx
    ON cts.ct_label_summaries (las_loc_id_identifying);
CREATE INDEX IF NOT EXISTS ct_label_summaries_las_loc_id_labels_idx
    ON cts.ct_label_summaries (las_loc_id_labels);
CREATE INDEX IF NOT EXISTS ct_letters_let_wgp_id_idx
    ON cts.ct_letters (let_wgp_id);
CREATE INDEX IF NOT EXISTS ct_letters_let_wgp_id_sent_idx
    ON cts.ct_letters (let_wgp_id_sent);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_lrc_lrt_id_idx
    ON cts.ct_loc_type_rel_combs (lrc_lrt_id);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_lrc_lty_id_1_idx
    ON cts.ct_loc_type_rel_combs (lrc_lty_id_1);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_lrc_lty_id_2_idx
    ON cts.ct_loc_type_rel_combs (lrc_lty_id_2);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_lid_loc_id_idx
    ON cts.ct_location_identifiers (lid_loc_id);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_lpr_lpt_id_idx
    ON cts.ct_location_party_rels (lpr_lpt_id);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_lpr_par_id_idx
    ON cts.ct_location_party_rels (lpr_par_id);
CREATE INDEX IF NOT EXISTS ct_location_relationships_llr_loc_id_child_idx
    ON cts.ct_location_relationships (llr_loc_id_child);
CREATE INDEX IF NOT EXISTS ct_location_relationships_llr_loc_id_parent_idx
    ON cts.ct_location_relationships (llr_loc_id_parent);
CREATE INDEX IF NOT EXISTS ct_location_relationships_llr_lrt_id_idx
    ON cts.ct_location_relationships (llr_lrt_id);
CREATE INDEX IF NOT EXISTS ct_location_types_lty_lif_id_idx
    ON cts.ct_location_types (lty_lif_id);
CREATE INDEX IF NOT EXISTS ct_locations_loc_cty_id_idx
    ON cts.ct_locations (loc_cty_id);
CREATE INDEX IF NOT EXISTS ct_locations_loc_lty_id_idx
    ON cts.ct_locations (loc_lty_id);
CREATE INDEX IF NOT EXISTS ct_locations_loc_slt_id_idx
    ON cts.ct_locations (loc_slt_id);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_lra_com_id_idx
    ON cts.ct_locrestrictionstoanimals (lra_com_id);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_lra_loc_id_idx
    ON cts.ct_locrestrictionstoanimals (lra_loc_id);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_lra_ran_id_idx
    ON cts.ct_locrestrictionstoanimals (lra_ran_id);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_mce_ran_id_idx
    ON cts.ct_mgt_control_errors (mce_ran_id);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_war_rou_id_idx
    ON cts.ct_mgt_wg_allocation_rules (war_rou_id);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_mse_mcs_id_idx
    ON cts.ct_movt_corr_summ_errors (mse_mcs_id);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_mcs_mov_id_idx
    ON cts.ct_movt_correct_summaries (mcs_mov_id);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_mcs_rmo_id_idx
    ON cts.ct_movt_correct_summaries (mcs_rmo_id);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_mcs_smo_id_idx
    ON cts.ct_movt_correct_summaries (mcs_smo_id);
CREATE INDEX IF NOT EXISTS ct_param_group_pgp_phd_id_idx
    ON cts.ct_param_group (pgp_phd_id);
CREATE INDEX IF NOT EXISTS ct_param_value_pvl_phd_id_idx
    ON cts.ct_param_value (pvl_phd_id);
CREATE INDEX IF NOT EXISTS ct_param_value_group_pvg_pgp_id_idx
    ON cts.ct_param_value_group (pvg_pgp_id);
CREATE INDEX IF NOT EXISTS ct_param_value_group_pvg_pvl_id_idx
    ON cts.ct_param_value_group (pvg_pvl_id);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_ppg_loc_id_birth_idx
    ON cts.ct_ppaf_groupings (ppg_loc_id_birth);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_ppg_loc_id_corres_idx
    ON cts.ct_ppaf_groupings (ppg_loc_id_corres);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_paf_etg_id_idx
    ON cts.ct_preprinted_appn_forms (paf_etg_id);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_paf_ppg_id_idx
    ON cts.ct_preprinted_appn_forms (paf_ppg_id);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_rae_rap_id_idx
    ON cts.ct_recd_application_errors (rae_rap_id);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_rme_rmo_id_idx
    ON cts.ct_recd_movement_errors (rme_rmo_id);
CREATE INDEX IF NOT EXISTS ct_received_applications_rap_ran_id_reserved_idx
    ON cts.ct_received_applications (rap_ran_id_reserved);
CREATE INDEX IF NOT EXISTS ct_received_applications_rap_wgp_id_idx
    ON cts.ct_received_applications (rap_wgp_id);
CREATE INDEX IF NOT EXISTS ct_registered_animals_ran_brd_id_idx
    ON cts.ct_registered_animals (ran_brd_id);
CREATE INDEX IF NOT EXISTS ct_registered_animals_ran_cry_id_chr_origin_idx
    ON cts.ct_registered_animals (ran_cry_id_chr_origin);
CREATE INDEX IF NOT EXISTS ct_registered_animals_ran_loc_id_passport_idx
    ON cts.ct_registered_animals (ran_loc_id_passport);
CREATE INDEX IF NOT EXISTS ct_registered_animals_ran_vap_id_idx
    ON cts.ct_registered_animals (ran_vap_id);
CREATE INDEX IF NOT EXISTS ct_registered_movements_mov_cry_id_import_idx
    ON cts.ct_registered_movements (mov_cry_id_import);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_sae_san_id_idx
    ON cts.ct_susp_animal_errors (sae_san_id);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_smr_scm_id_idx
    ON cts.ct_susp_cm_measure_results (smr_scm_id);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_scm_loc_id_idx
    ON cts.ct_susp_condition_markers (scm_loc_id);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_scm_ran_id_idx
    ON cts.ct_susp_condition_markers (scm_ran_id);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_sme_smo_id_idx
    ON cts.ct_susp_movement_errors (sme_smo_id);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_san_loc_id_initial_idx
    ON cts.ct_suspended_animals (san_loc_id_initial);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_san_loc_id_request_idx
    ON cts.ct_suspended_animals (san_loc_id_request);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_san_ran_id_idx
    ON cts.ct_suspended_animals (san_ran_id);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_san_vap_id_idx
    ON cts.ct_suspended_animals (san_vap_id);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_san_wgp_id_idx
    ON cts.ct_suspended_animals (san_wgp_id);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_sca_rou_id_idx
    ON cts.ct_suspense_char_alloc_rules (sca_rou_id);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_swa_rou_id_idx
    ON cts.ct_suspense_wg_alloc_rules (swa_rou_id);
CREATE INDEX IF NOT EXISTS ct_valid_applications_vap_loc_id_requester_idx
    ON cts.ct_valid_applications (vap_loc_id_requester);
CREATE INDEX IF NOT EXISTS ct_valid_applications_vap_wur_id_idx
    ON cts.ct_valid_applications (vap_wur_id);
CREATE INDEX IF NOT EXISTS ct_web_users_wur_lpr_id_keeper_idx
    ON cts.ct_web_users (wur_lpr_id_keeper);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_wga_rou_id_idx
    ON cts.ct_wg_autoallocations (wga_rou_id);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_wga_wgp_id_idx
    ON cts.ct_wg_autoallocations (wga_wgp_id);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_wsa_rou_id_idx
    ON cts.ct_wg_super_assignments (wsa_rou_id);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_wsa_wgp_id_assigned_idx
    ON cts.ct_wg_super_assignments (wsa_wgp_id_assigned);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_wsa_wgp_id_current_idx
    ON cts.ct_wg_super_assignments (wsa_wgp_id_current);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_wua_cus_id_idx
    ON cts.ct_wg_user_assignments (wua_cus_id);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_wua_wgp_id_idx
    ON cts.ct_wg_user_assignments (wua_wgp_id);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_trans_id_idx
    ON cts.ct_alloc_routines (trans_id);
CREATE INDEX IF NOT EXISTS ct_application_late_days_trans_id_idx
    ON cts.ct_application_late_days (trans_id);
CREATE INDEX IF NOT EXISTS ct_batch_retention_conf_trans_id_idx
    ON cts.ct_batch_retention_conf (trans_id);
CREATE INDEX IF NOT EXISTS ct_breeds_trans_id_idx
    ON cts.ct_breeds (trans_id);
CREATE INDEX IF NOT EXISTS ct_cla_extract_trans_id_idx
    ON cts.ct_cla_extract (trans_id);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_trans_id_idx
    ON cts.ct_cla_extract_detail (trans_id);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_trans_id_idx
    ON cts.ct_cla_extract_dm (trans_id);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_trans_id_idx
    ON cts.ct_cla_mini_detail (trans_id);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_trans_id_idx
    ON cts.ct_cla_mini_extract (trans_id);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_trans_id_idx
    ON cts.ct_claim_statuses (trans_id);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_trans_id_idx
    ON cts.ct_comms_addresses (trans_id);
CREATE INDEX IF NOT EXISTS ct_condition_types_trans_id_idx
    ON cts.ct_condition_types (trans_id);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_trans_id_idx
    ON cts.ct_cm_authorities (trans_id);
CREATE INDEX IF NOT EXISTS ct_counties_trans_id_idx
    ON cts.ct_counties (trans_id);
CREATE INDEX IF NOT EXISTS ct_counties_migration_trans_id_idx
    ON cts.ct_counties_migration (trans_id);
CREATE INDEX IF NOT EXISTS ct_countries_trans_id_idx
    ON cts.ct_countries (trans_id);
CREATE INDEX IF NOT EXISTS ct_cps167_report_trans_id_idx
    ON cts.ct_cps167_report (trans_id);
CREATE INDEX IF NOT EXISTS ct_cts164_handshake_file_keys_trans_id_idx
    ON cts.ct_cts164_handshake_file_keys (trans_id);
CREATE INDEX IF NOT EXISTS ct_cts_users_trans_id_idx
    ON cts.ct_cts_users (trans_id);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_trans_id_idx
    ON cts.ct_eartag_formats (trans_id);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_trans_id_idx
    ON cts.ct_eartag_reasons (trans_id);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_trans_id_idx
    ON cts.ct_eartag_reason_flags (trans_id);
CREATE INDEX IF NOT EXISTS ct_eartag_types_trans_id_idx
    ON cts.ct_eartag_types (trans_id);
CREATE INDEX IF NOT EXISTS ct_email_log_trans_id_idx
    ON cts.ct_email_log (trans_id);
CREATE INDEX IF NOT EXISTS ct_ereport_files_trans_id_idx
    ON cts.ct_ereport_files (trans_id);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_trans_id_idx
    ON cts.ct_ereport_load_messages (trans_id);
CREATE INDEX IF NOT EXISTS ct_ereport_locks_trans_id_idx
    ON cts.ct_ereport_locks (trans_id);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_trans_id_idx
    ON cts.ct_ereport_process_messages (trans_id);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_trans_id_idx
    ON cts.ct_ext_cetd_eartag (trans_id);
CREATE INDEX IF NOT EXISTS ct_ext_ni_district_trans_id_idx
    ON cts.ct_ext_ni_district (trans_id);
CREATE INDEX IF NOT EXISTS ct_ext_special_herd_trans_id_idx
    ON cts.ct_ext_special_herd (trans_id);
CREATE INDEX IF NOT EXISTS ct_file_layouts_trans_id_idx
    ON cts.ct_file_layouts (trans_id);
CREATE INDEX IF NOT EXISTS ct_hsf_sequences_trans_id_idx
    ON cts.ct_hsf_sequences (trans_id);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_trans_id_idx
    ON cts.ct_insert_update_log (trans_id);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_trans_id_idx
    ON cts.ct_issuing_authorities (trans_id);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_trans_id_idx
    ON cts.ct_electronic_identifiers (trans_id);
CREATE INDEX IF NOT EXISTS ct_late_days_trans_id_idx
    ON cts.ct_late_days (trans_id);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_trans_id_idx
    ON cts.ct_location_id_formats (trans_id);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_trans_id_idx
    ON cts.ct_location_party_rel_types (trans_id);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_trans_id_idx
    ON cts.ct_location_rel_types (trans_id);
CREATE INDEX IF NOT EXISTS ct_location_types_trans_id_idx
    ON cts.ct_location_types (trans_id);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_trans_id_idx
    ON cts.ct_loc_type_rel_combs (trans_id);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_trans_id_idx
    ON cts.ct_mgt_wg_allocation_rules (trans_id);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_trans_id_idx
    ON cts.ct_mhs_to_cph (trans_id);
CREATE INDEX IF NOT EXISTS ct_mov_hst_trans_id_idx
    ON cts.ct_mov_hst (trans_id);
CREATE INDEX IF NOT EXISTS ct_msgtxt_trans_id_idx
    ON cts.ct_msgtxt (trans_id);
CREATE INDEX IF NOT EXISTS ct_non_working_days_trans_id_idx
    ON cts.ct_non_working_days (trans_id);
CREATE INDEX IF NOT EXISTS ct_param_header_trans_id_idx
    ON cts.ct_param_header (trans_id);
CREATE INDEX IF NOT EXISTS ct_param_group_trans_id_idx
    ON cts.ct_param_group (trans_id);
CREATE INDEX IF NOT EXISTS ct_param_value_trans_id_idx
    ON cts.ct_param_value (trans_id);
CREATE INDEX IF NOT EXISTS ct_param_value_group_trans_id_idx
    ON cts.ct_param_value_group (trans_id);
CREATE INDEX IF NOT EXISTS ct_parties_trans_id_idx
    ON cts.ct_parties (trans_id);
CREATE INDEX IF NOT EXISTS ct_probity_checks_trans_id_idx
    ON cts.ct_probity_checks (trans_id);
CREATE INDEX IF NOT EXISTS ct_conditions_trans_id_idx
    ON cts.ct_conditions (trans_id);
CREATE INDEX IF NOT EXISTS ct_condition_activities_trans_id_idx
    ON cts.ct_condition_activities (trans_id);
CREATE INDEX IF NOT EXISTS ct_condition_variants_trans_id_idx
    ON cts.ct_condition_variants (trans_id);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_trans_id_idx
    ON cts.ct_cond_variant_groupings (trans_id);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_trans_id_idx
    ON cts.ct_ps9999_ahdb_data (trans_id);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_trans_id_idx
    ON cts.ct_ps9999_ahdb_mov_history (trans_id);
CREATE INDEX IF NOT EXISTS ct_received_movements_trans_id_idx
    ON cts.ct_received_movements (trans_id);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_trans_id_idx
    ON cts.ct_recd_movement_errors (trans_id);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_trans_id_idx
    ON cts.ct_reset_to_extract (trans_id);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_trans_id_idx
    ON cts.ct_sbcs_ext (trans_id);
CREATE INDEX IF NOT EXISTS ct_schemes_trans_id_idx
    ON cts.ct_schemes (trans_id);
CREATE INDEX IF NOT EXISTS ct_claim_types_trans_id_idx
    ON cts.ct_claim_types (trans_id);
CREATE INDEX IF NOT EXISTS ct_stage_files_trans_id_idx
    ON cts.ct_stage_files (trans_id);
CREATE INDEX IF NOT EXISTS ct_stage_locks_trans_id_idx
    ON cts.ct_stage_locks (trans_id);
CREATE INDEX IF NOT EXISTS ct_stage_messages_trans_id_idx
    ON cts.ct_stage_messages (trans_id);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_trans_id_idx
    ON cts.ct_sublocation_types (trans_id);
CREATE INDEX IF NOT EXISTS ct_locations_trans_id_idx
    ON cts.ct_locations (trans_id);
CREATE INDEX IF NOT EXISTS ct_addresses_trans_id_idx
    ON cts.ct_addresses (trans_id);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_trans_id_idx
    ON cts.ct_eartag_staging (trans_id);
CREATE INDEX IF NOT EXISTS ct_eartags_trans_id_idx
    ON cts.ct_eartags (trans_id);
CREATE INDEX IF NOT EXISTS ct_label_summaries_trans_id_idx
    ON cts.ct_label_summaries (trans_id);
CREATE INDEX IF NOT EXISTS ct_label_requests_trans_id_idx
    ON cts.ct_label_requests (trans_id);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_trans_id_idx
    ON cts.ct_location_identifiers (trans_id);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_trans_id_idx
    ON cts.ct_location_party_rels (trans_id);
CREATE INDEX IF NOT EXISTS ct_location_relationships_trans_id_idx
    ON cts.ct_location_relationships (trans_id);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_trans_id_idx
    ON cts.ct_ppaf_groupings (trans_id);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_trans_id_idx
    ON cts.ct_preprinted_appn_forms (trans_id);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_trans_id_idx
    ON cts.ct_suspended_movements (trans_id);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_trans_id_idx
    ON cts.ct_susp_movement_errors (trans_id);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_trans_id_idx
    ON cts.ct_suspense_char_alloc_rules (trans_id);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_trans_id_idx
    ON cts.ct_suspense_wg_alloc_rules (trans_id);
CREATE INDEX IF NOT EXISTS ct_web_users_trans_id_idx
    ON cts.ct_web_users (trans_id);
CREATE INDEX IF NOT EXISTS ct_valid_applications_trans_id_idx
    ON cts.ct_valid_applications (trans_id);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_trans_id_idx
    ON cts.ct_applic_statuses (trans_id);
CREATE INDEX IF NOT EXISTS ct_registered_animals_trans_id_idx
    ON cts.ct_registered_animals (trans_id);
CREATE INDEX IF NOT EXISTS ct_animal_claims_trans_id_idx
    ON cts.ct_animal_claims (trans_id);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_trans_id_idx
    ON cts.ct_animal_identifiers (trans_id);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_trans_id_idx
    ON cts.ct_animal_relationships (trans_id);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_trans_id_idx
    ON cts.ct_animal_statuses (trans_id);
CREATE INDEX IF NOT EXISTS ct_issued_documents_trans_id_idx
    ON cts.ct_issued_documents (trans_id);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_trans_id_idx
    ON cts.ct_mgt_control_errors (trans_id);
CREATE INDEX IF NOT EXISTS ct_registered_movements_trans_id_idx
    ON cts.ct_registered_movements (trans_id);
CREATE INDEX IF NOT EXISTS ct_animal_changes_trans_id_idx
    ON cts.ct_animal_changes (trans_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_trans_id_idx
    ON cts.ct_condition_markers (trans_id);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_trans_id_idx
    ON cts.ct_cm_measures_results (trans_id);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_trans_id_idx
    ON cts.ct_locrestrictionstoanimals (trans_id);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_trans_id_idx
    ON cts.ct_movt_correct_summaries (trans_id);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_trans_id_idx
    ON cts.ct_movt_corr_summ_errors (trans_id);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_trans_id_idx
    ON cts.ct_susp_condition_markers (trans_id);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_trans_id_idx
    ON cts.ct_condition_marker_errors (trans_id);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_trans_id_idx
    ON cts.ct_susp_cm_measure_results (trans_id);
CREATE INDEX IF NOT EXISTS ct_workgroups_trans_id_idx
    ON cts.ct_workgroups (trans_id);
CREATE INDEX IF NOT EXISTS ct_letters_trans_id_idx
    ON cts.ct_letters (trans_id);
CREATE INDEX IF NOT EXISTS ct_received_applications_trans_id_idx
    ON cts.ct_received_applications (trans_id);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_trans_id_idx
    ON cts.ct_recd_application_errors (trans_id);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_trans_id_idx
    ON cts.ct_suspended_animals (trans_id);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_trans_id_idx
    ON cts.ct_animal_correct_summaries (trans_id);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_trans_id_idx
    ON cts.ct_animal_corr_summ_errors (trans_id);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_trans_id_idx
    ON cts.ct_susp_animal_errors (trans_id);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_trans_id_idx
    ON cts.ct_wg_autoallocations (trans_id);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_trans_id_idx
    ON cts.ct_wg_super_assignments (trans_id);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_trans_id_idx
    ON cts.ct_wg_user_assignments (trans_id);
CREATE INDEX IF NOT EXISTS ct_parties_faker_trans_id_idx
    ON cts.ct_parties_faker (trans_id);
CREATE INDEX IF NOT EXISTS ct_locations_faker_trans_id_idx
    ON cts.ct_locations_faker (trans_id);
