-- liquibase formatted sql

-- changeset schema:0001-002-cts_transactions-indexes splitStatements:false
CREATE INDEX ct_addresses_source_key_idx ON cts_transactions.ct_addresses USING btree (adr_id);
CREATE INDEX ct_animal_changes_source_key_idx ON cts_transactions.ct_animal_changes USING btree (ach_id);
CREATE INDEX ct_animal_claims_source_key_idx ON cts_transactions.ct_animal_claims USING btree (anc_id);
CREATE INDEX ct_animal_corr_summ_errors_source_key_idx ON cts_transactions.ct_animal_corr_summ_errors USING btree (ase_id);
CREATE INDEX ct_animal_correct_summaries_source_key_idx ON cts_transactions.ct_animal_correct_summaries USING btree (acs_id);
CREATE INDEX ct_animal_identifiers_source_key_idx ON cts_transactions.ct_animal_identifiers USING btree (aid_id);
CREATE INDEX ct_animal_relationships_source_key_idx ON cts_transactions.ct_animal_relationships USING btree (aar_id);
CREATE INDEX ct_animal_statuses_source_key_idx ON cts_transactions.ct_animal_statuses USING btree (ast_id);
CREATE INDEX ct_applic_statuses_source_key_idx ON cts_transactions.ct_applic_statuses USING btree (aps_id);
CREATE INDEX ct_application_late_days_source_key_idx ON cts_transactions.ct_application_late_days USING btree (ald_id);
CREATE INDEX ct_cla_extract_source_key_idx ON cts_transactions.ct_cla_extract USING btree (cle_id);
CREATE INDEX ct_cla_extract_detail_source_key_idx ON cts_transactions.ct_cla_extract_detail USING btree (cld_id);
CREATE INDEX ct_cla_extract_dm_source_key_idx ON cts_transactions.ct_cla_extract_dm USING btree (cle_id);
CREATE INDEX ct_cla_mini_detail_source_key_idx ON cts_transactions.ct_cla_mini_detail USING btree (cld_id);
CREATE INDEX ct_cla_mini_extract_source_key_idx ON cts_transactions.ct_cla_mini_extract USING btree (cle_id);
CREATE INDEX ct_cm_measures_results_source_key_idx ON cts_transactions.ct_cm_measures_results USING btree (cmr_id);
CREATE INDEX ct_comms_addresses_source_key_idx ON cts_transactions.ct_comms_addresses USING btree (coa_id);
CREATE INDEX ct_condition_marker_errors_source_key_idx ON cts_transactions.ct_condition_marker_errors USING btree (cme_id);
CREATE INDEX ct_condition_markers_source_key_idx ON cts_transactions.ct_condition_markers USING btree (com_id);
CREATE INDEX ct_cps167_report_source_key_idx ON cts_transactions.ct_cps167_report USING btree (kns_id);
CREATE INDEX ct_cts_users_source_key_idx ON cts_transactions.ct_cts_users USING btree (cus_id);
CREATE INDEX ct_eartag_staging_source_key_idx ON cts_transactions.ct_eartag_staging USING btree (est_id);
CREATE INDEX ct_eartags_source_key_idx ON cts_transactions.ct_eartags USING btree (etg_id);
CREATE INDEX ct_electronic_identifiers_source_key_idx ON cts_transactions.ct_electronic_identifiers USING btree (eid_id);
CREATE INDEX ct_email_log_source_key_idx ON cts_transactions.ct_email_log USING btree (eml_id);
CREATE INDEX ct_ereport_files_source_key_idx ON cts_transactions.ct_ereport_files USING btree (ere_id);
CREATE INDEX ct_insert_update_log_source_key_idx ON cts_transactions.ct_insert_update_log USING btree (iul_id);
CREATE INDEX ct_issued_documents_source_key_idx ON cts_transactions.ct_issued_documents USING btree (ido_id);
CREATE INDEX ct_label_requests_source_key_idx ON cts_transactions.ct_label_requests USING btree (lar_id);
CREATE INDEX ct_label_summaries_source_key_idx ON cts_transactions.ct_label_summaries USING btree (las_id);
CREATE INDEX ct_letters_source_key_idx ON cts_transactions.ct_letters USING btree (let_id);
CREATE INDEX ct_location_identifiers_source_key_idx ON cts_transactions.ct_location_identifiers USING btree (lid_id);
CREATE INDEX ct_location_party_rels_source_key_idx ON cts_transactions.ct_location_party_rels USING btree (lpr_id);
CREATE INDEX ct_location_relationships_source_key_idx ON cts_transactions.ct_location_relationships USING btree (llr_id);
CREATE INDEX ct_locations_source_key_idx ON cts_transactions.ct_locations USING btree (loc_id);
CREATE INDEX ct_mgt_control_errors_source_key_idx ON cts_transactions.ct_mgt_control_errors USING btree (mce_id);
CREATE INDEX ct_movt_corr_summ_errors_source_key_idx ON cts_transactions.ct_movt_corr_summ_errors USING btree (mse_id);
CREATE INDEX ct_movt_correct_summaries_source_key_idx ON cts_transactions.ct_movt_correct_summaries USING btree (mcs_id);
CREATE INDEX ct_parties_source_key_idx ON cts_transactions.ct_parties USING btree (par_id);
CREATE INDEX ct_ppaf_groupings_source_key_idx ON cts_transactions.ct_ppaf_groupings USING btree (ppg_id);
CREATE INDEX ct_preprinted_appn_forms_source_key_idx ON cts_transactions.ct_preprinted_appn_forms USING btree (paf_id);
CREATE INDEX ct_ps9999_ahdb_data_source_key_idx ON cts_transactions.ct_ps9999_ahdb_data USING btree (ran_id);
CREATE INDEX ct_recd_application_errors_source_key_idx ON cts_transactions.ct_recd_application_errors USING btree (rae_id);
CREATE INDEX ct_recd_movement_errors_source_key_idx ON cts_transactions.ct_recd_movement_errors USING btree (rme_id);
CREATE INDEX ct_received_applications_source_key_idx ON cts_transactions.ct_received_applications USING btree (rap_id);
CREATE INDEX ct_received_movements_source_key_idx ON cts_transactions.ct_received_movements USING btree (rmo_id);
CREATE INDEX ct_registered_animals_source_key_idx ON cts_transactions.ct_registered_animals USING btree (ran_id);
CREATE INDEX ct_registered_movements_source_key_idx ON cts_transactions.ct_registered_movements USING btree (mov_id);
CREATE INDEX ct_reset_to_extract_source_key_idx ON cts_transactions.ct_reset_to_extract USING btree (rte_id);
CREATE INDEX ct_stage_files_source_key_idx ON cts_transactions.ct_stage_files USING btree (stf_id);
CREATE INDEX ct_susp_animal_errors_source_key_idx ON cts_transactions.ct_susp_animal_errors USING btree (sae_id);
CREATE INDEX ct_susp_cm_measure_results_source_key_idx ON cts_transactions.ct_susp_cm_measure_results USING btree (smr_id);
CREATE INDEX ct_susp_condition_markers_source_key_idx ON cts_transactions.ct_susp_condition_markers USING btree (scm_id);
CREATE INDEX ct_susp_movement_errors_source_key_idx ON cts_transactions.ct_susp_movement_errors USING btree (sme_id);
CREATE INDEX ct_suspended_animals_source_key_idx ON cts_transactions.ct_suspended_animals USING btree (san_id);
CREATE INDEX ct_suspended_movements_source_key_idx ON cts_transactions.ct_suspended_movements USING btree (smo_id);
CREATE INDEX ct_valid_applications_source_key_idx ON cts_transactions.ct_valid_applications USING btree (vap_id);
CREATE INDEX ct_web_users_source_key_idx ON cts_transactions.ct_web_users USING btree (wur_id);
CREATE INDEX ct_wg_autoallocations_source_key_idx ON cts_transactions.ct_wg_autoallocations USING btree (wga_id);
CREATE INDEX ct_wg_super_assignments_source_key_idx ON cts_transactions.ct_wg_super_assignments USING btree (wsa_id);
CREATE INDEX ct_wg_user_assignments_source_key_idx ON cts_transactions.ct_wg_user_assignments USING btree (wua_id);
CREATE INDEX ct_workgroups_source_key_idx ON cts_transactions.ct_workgroups USING btree (wgp_id);
CREATE INDEX IF NOT EXISTS ct_addresses_trans_type_idx
    ON cts_transactions.ct_addresses (trans_type);
CREATE INDEX IF NOT EXISTS ct_addresses_aud_id_idx
    ON cts_transactions.ct_addresses (adr_aud_id);
CREATE INDEX IF NOT EXISTS ct_addresses_aud_type_idx
    ON cts_transactions.ct_addresses (adr_aud_type);
CREATE INDEX IF NOT EXISTS ct_addresses_aud_datetime_idx
    ON cts_transactions.ct_addresses (adr_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_changes_trans_type_idx
    ON cts_transactions.ct_animal_changes (trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_changes_aud_id_idx
    ON cts_transactions.ct_animal_changes (ach_aud_id);
CREATE INDEX IF NOT EXISTS ct_animal_changes_aud_type_idx
    ON cts_transactions.ct_animal_changes (ach_aud_type);
CREATE INDEX IF NOT EXISTS ct_animal_changes_aud_datetime_idx
    ON cts_transactions.ct_animal_changes (ach_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_claims_trans_type_idx
    ON cts_transactions.ct_animal_claims (trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_claims_aud_id_idx
    ON cts_transactions.ct_animal_claims (anc_aud_id);
CREATE INDEX IF NOT EXISTS ct_animal_claims_aud_type_idx
    ON cts_transactions.ct_animal_claims (anc_aud_type);
CREATE INDEX IF NOT EXISTS ct_animal_claims_aud_datetime_idx
    ON cts_transactions.ct_animal_claims (anc_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_trans_type_idx
    ON cts_transactions.ct_animal_corr_summ_errors (trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_aud_id_idx
    ON cts_transactions.ct_animal_corr_summ_errors (ase_aud_id);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_aud_type_idx
    ON cts_transactions.ct_animal_corr_summ_errors (ase_aud_type);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_aud_datetime_idx
    ON cts_transactions.ct_animal_corr_summ_errors (ase_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_trans_type_idx
    ON cts_transactions.ct_animal_correct_summaries (trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_aud_id_idx
    ON cts_transactions.ct_animal_correct_summaries (acs_aud_id);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_aud_type_idx
    ON cts_transactions.ct_animal_correct_summaries (acs_aud_type);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_aud_datetime_idx
    ON cts_transactions.ct_animal_correct_summaries (acs_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_trans_type_idx
    ON cts_transactions.ct_animal_identifiers (trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aud_id_idx
    ON cts_transactions.ct_animal_identifiers (aid_aud_id);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aud_type_idx
    ON cts_transactions.ct_animal_identifiers (aid_aud_type);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aud_datetime_idx
    ON cts_transactions.ct_animal_identifiers (aid_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_trans_type_idx
    ON cts_transactions.ct_animal_relationships (trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_aud_id_idx
    ON cts_transactions.ct_animal_relationships (aar_aud_id);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_aud_type_idx
    ON cts_transactions.ct_animal_relationships (aar_aud_type);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_aud_datetime_idx
    ON cts_transactions.ct_animal_relationships (aar_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_trans_type_idx
    ON cts_transactions.ct_animal_statuses (trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_aud_id_idx
    ON cts_transactions.ct_animal_statuses (ast_aud_id);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_aud_type_idx
    ON cts_transactions.ct_animal_statuses (ast_aud_type);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_aud_datetime_idx
    ON cts_transactions.ct_animal_statuses (ast_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_trans_type_idx
    ON cts_transactions.ct_applic_statuses (trans_type);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_aud_id_idx
    ON cts_transactions.ct_applic_statuses (aps_aud_id);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_aud_type_idx
    ON cts_transactions.ct_applic_statuses (aps_aud_type);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_aud_datetime_idx
    ON cts_transactions.ct_applic_statuses (aps_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_application_late_days_trans_type_idx
    ON cts_transactions.ct_application_late_days (trans_type);
CREATE INDEX IF NOT EXISTS ct_application_late_days_aud_id_idx
    ON cts_transactions.ct_application_late_days (ald_aud_id);
CREATE INDEX IF NOT EXISTS ct_application_late_days_aud_type_idx
    ON cts_transactions.ct_application_late_days (ald_aud_type);
CREATE INDEX IF NOT EXISTS ct_application_late_days_aud_datetime_idx
    ON cts_transactions.ct_application_late_days (ald_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cla_extract_trans_type_idx
    ON cts_transactions.ct_cla_extract (trans_type);
CREATE INDEX IF NOT EXISTS ct_cla_extract_aud_id_idx
    ON cts_transactions.ct_cla_extract (cle_aud_id);
CREATE INDEX IF NOT EXISTS ct_cla_extract_aud_type_idx
    ON cts_transactions.ct_cla_extract (cle_aud_type);
CREATE INDEX IF NOT EXISTS ct_cla_extract_aud_datetime_idx
    ON cts_transactions.ct_cla_extract (cle_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_trans_type_idx
    ON cts_transactions.ct_cla_extract_detail (trans_type);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_aud_id_idx
    ON cts_transactions.ct_cla_extract_detail (cld_aud_id);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_aud_type_idx
    ON cts_transactions.ct_cla_extract_detail (cld_aud_type);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_aud_datetime_idx
    ON cts_transactions.ct_cla_extract_detail (cld_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_trans_type_idx
    ON cts_transactions.ct_cla_extract_dm (trans_type);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_aud_id_idx
    ON cts_transactions.ct_cla_extract_dm (cle_aud_id);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_aud_type_idx
    ON cts_transactions.ct_cla_extract_dm (cle_aud_type);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_aud_datetime_idx
    ON cts_transactions.ct_cla_extract_dm (cle_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_trans_type_idx
    ON cts_transactions.ct_cla_mini_detail (trans_type);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_aud_id_idx
    ON cts_transactions.ct_cla_mini_detail (cld_aud_id);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_aud_type_idx
    ON cts_transactions.ct_cla_mini_detail (cld_aud_type);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_aud_datetime_idx
    ON cts_transactions.ct_cla_mini_detail (cld_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_trans_type_idx
    ON cts_transactions.ct_cla_mini_extract (trans_type);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_aud_id_idx
    ON cts_transactions.ct_cla_mini_extract (cle_aud_id);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_aud_type_idx
    ON cts_transactions.ct_cla_mini_extract (cle_aud_type);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_aud_datetime_idx
    ON cts_transactions.ct_cla_mini_extract (cle_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_trans_type_idx
    ON cts_transactions.ct_cm_measures_results (trans_type);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_aud_id_idx
    ON cts_transactions.ct_cm_measures_results (cmr_aud_id);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_aud_type_idx
    ON cts_transactions.ct_cm_measures_results (cmr_aud_type);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_aud_datetime_idx
    ON cts_transactions.ct_cm_measures_results (cmr_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_trans_type_idx
    ON cts_transactions.ct_comms_addresses (trans_type);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_aud_id_idx
    ON cts_transactions.ct_comms_addresses (coa_aud_id);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_aud_type_idx
    ON cts_transactions.ct_comms_addresses (coa_aud_type);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_aud_datetime_idx
    ON cts_transactions.ct_comms_addresses (coa_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_trans_type_idx
    ON cts_transactions.ct_condition_marker_errors (trans_type);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_aud_id_idx
    ON cts_transactions.ct_condition_marker_errors (cme_aud_id);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_aud_type_idx
    ON cts_transactions.ct_condition_marker_errors (cme_aud_type);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_aud_datetime_idx
    ON cts_transactions.ct_condition_marker_errors (cme_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_condition_markers_trans_type_idx
    ON cts_transactions.ct_condition_markers (trans_type);
CREATE INDEX IF NOT EXISTS ct_condition_markers_aud_id_idx
    ON cts_transactions.ct_condition_markers (com_aud_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_aud_type_idx
    ON cts_transactions.ct_condition_markers (com_aud_type);
CREATE INDEX IF NOT EXISTS ct_condition_markers_aud_datetime_idx
    ON cts_transactions.ct_condition_markers (com_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cps167_report_trans_type_idx
    ON cts_transactions.ct_cps167_report (trans_type);
CREATE INDEX IF NOT EXISTS ct_cps167_report_aud_id_idx
    ON cts_transactions.ct_cps167_report (kns_aud_id);
CREATE INDEX IF NOT EXISTS ct_cps167_report_aud_type_idx
    ON cts_transactions.ct_cps167_report (kns_aud_type);
CREATE INDEX IF NOT EXISTS ct_cps167_report_aud_datetime_idx
    ON cts_transactions.ct_cps167_report (kns_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cts_users_trans_type_idx
    ON cts_transactions.ct_cts_users (trans_type);
CREATE INDEX IF NOT EXISTS ct_cts_users_aud_id_idx
    ON cts_transactions.ct_cts_users (cus_aud_id);
CREATE INDEX IF NOT EXISTS ct_cts_users_aud_type_idx
    ON cts_transactions.ct_cts_users (cus_aud_type);
CREATE INDEX IF NOT EXISTS ct_cts_users_aud_datetime_idx
    ON cts_transactions.ct_cts_users (cus_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_trans_type_idx
    ON cts_transactions.ct_eartag_staging (trans_type);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_aud_id_idx
    ON cts_transactions.ct_eartag_staging (est_aud_id);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_aud_type_idx
    ON cts_transactions.ct_eartag_staging (est_aud_type);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_aud_datetime_idx
    ON cts_transactions.ct_eartag_staging (est_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartags_trans_type_idx
    ON cts_transactions.ct_eartags (trans_type);
CREATE INDEX IF NOT EXISTS ct_eartags_aud_id_idx
    ON cts_transactions.ct_eartags (etg_aud_id);
CREATE INDEX IF NOT EXISTS ct_eartags_aud_type_idx
    ON cts_transactions.ct_eartags (etg_aud_type);
CREATE INDEX IF NOT EXISTS ct_eartags_aud_datetime_idx
    ON cts_transactions.ct_eartags (etg_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_trans_type_idx
    ON cts_transactions.ct_electronic_identifiers (trans_type);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_aud_id_idx
    ON cts_transactions.ct_electronic_identifiers (eid_aud_id);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_aud_type_idx
    ON cts_transactions.ct_electronic_identifiers (eid_aud_type);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_aud_datetime_idx
    ON cts_transactions.ct_electronic_identifiers (eid_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_email_log_trans_type_idx
    ON cts_transactions.ct_email_log (trans_type);
CREATE INDEX IF NOT EXISTS ct_email_log_aud_id_idx
    ON cts_transactions.ct_email_log (eml_aud_id);
CREATE INDEX IF NOT EXISTS ct_email_log_aud_type_idx
    ON cts_transactions.ct_email_log (eml_aud_type);
CREATE INDEX IF NOT EXISTS ct_email_log_aud_datetime_idx
    ON cts_transactions.ct_email_log (eml_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ereport_files_trans_type_idx
    ON cts_transactions.ct_ereport_files (trans_type);
CREATE INDEX IF NOT EXISTS ct_ereport_files_aud_id_idx
    ON cts_transactions.ct_ereport_files (ere_aud_id);
CREATE INDEX IF NOT EXISTS ct_ereport_files_aud_type_idx
    ON cts_transactions.ct_ereport_files (ere_aud_type);
CREATE INDEX IF NOT EXISTS ct_ereport_files_aud_datetime_idx
    ON cts_transactions.ct_ereport_files (ere_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_trans_type_idx
    ON cts_transactions.ct_ereport_load_messages (trans_type);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_aud_id_idx
    ON cts_transactions.ct_ereport_load_messages (erm_aud_id);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_aud_type_idx
    ON cts_transactions.ct_ereport_load_messages (erm_aud_type);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_aud_datetime_idx
    ON cts_transactions.ct_ereport_load_messages (erm_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ereport_locks_trans_type_idx
    ON cts_transactions.ct_ereport_locks (trans_type);
CREATE INDEX IF NOT EXISTS ct_ereport_locks_aud_id_idx
    ON cts_transactions.ct_ereport_locks (erl_aud_id);
CREATE INDEX IF NOT EXISTS ct_ereport_locks_aud_type_idx
    ON cts_transactions.ct_ereport_locks (erl_aud_type);
CREATE INDEX IF NOT EXISTS ct_ereport_locks_aud_datetime_idx
    ON cts_transactions.ct_ereport_locks (erl_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_trans_type_idx
    ON cts_transactions.ct_ereport_process_messages (trans_type);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_aud_id_idx
    ON cts_transactions.ct_ereport_process_messages (erq_aud_id);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_aud_type_idx
    ON cts_transactions.ct_ereport_process_messages (erq_aud_type);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_aud_datetime_idx
    ON cts_transactions.ct_ereport_process_messages (erq_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_trans_type_idx
    ON cts_transactions.ct_ext_cetd_eartag (trans_type);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_aud_id_idx
    ON cts_transactions.ct_ext_cetd_eartag (cet_aud_id);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_aud_type_idx
    ON cts_transactions.ct_ext_cetd_eartag (cet_aud_type);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_aud_datetime_idx
    ON cts_transactions.ct_ext_cetd_eartag (cet_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_trans_type_idx
    ON cts_transactions.ct_insert_update_log (trans_type);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_aud_id_idx
    ON cts_transactions.ct_insert_update_log (iul_aud_id);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_aud_type_idx
    ON cts_transactions.ct_insert_update_log (iul_aud_type);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_aud_datetime_idx
    ON cts_transactions.ct_insert_update_log (iul_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_issued_documents_trans_type_idx
    ON cts_transactions.ct_issued_documents (trans_type);
CREATE INDEX IF NOT EXISTS ct_issued_documents_aud_id_idx
    ON cts_transactions.ct_issued_documents (ido_aud_id);
CREATE INDEX IF NOT EXISTS ct_issued_documents_aud_type_idx
    ON cts_transactions.ct_issued_documents (ido_aud_type);
CREATE INDEX IF NOT EXISTS ct_issued_documents_aud_datetime_idx
    ON cts_transactions.ct_issued_documents (ido_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_label_requests_trans_type_idx
    ON cts_transactions.ct_label_requests (trans_type);
CREATE INDEX IF NOT EXISTS ct_label_requests_aud_id_idx
    ON cts_transactions.ct_label_requests (lar_aud_id);
CREATE INDEX IF NOT EXISTS ct_label_requests_aud_type_idx
    ON cts_transactions.ct_label_requests (lar_aud_type);
CREATE INDEX IF NOT EXISTS ct_label_requests_aud_datetime_idx
    ON cts_transactions.ct_label_requests (lar_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_label_summaries_trans_type_idx
    ON cts_transactions.ct_label_summaries (trans_type);
CREATE INDEX IF NOT EXISTS ct_label_summaries_aud_id_idx
    ON cts_transactions.ct_label_summaries (las_aud_id);
CREATE INDEX IF NOT EXISTS ct_label_summaries_aud_type_idx
    ON cts_transactions.ct_label_summaries (las_aud_type);
CREATE INDEX IF NOT EXISTS ct_label_summaries_aud_datetime_idx
    ON cts_transactions.ct_label_summaries (las_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_letters_trans_type_idx
    ON cts_transactions.ct_letters (trans_type);
CREATE INDEX IF NOT EXISTS ct_letters_aud_id_idx
    ON cts_transactions.ct_letters (let_aud_id);
CREATE INDEX IF NOT EXISTS ct_letters_aud_type_idx
    ON cts_transactions.ct_letters (let_aud_type);
CREATE INDEX IF NOT EXISTS ct_letters_aud_datetime_idx
    ON cts_transactions.ct_letters (let_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_trans_type_idx
    ON cts_transactions.ct_location_identifiers (trans_type);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_aud_id_idx
    ON cts_transactions.ct_location_identifiers (lid_aud_id);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_aud_type_idx
    ON cts_transactions.ct_location_identifiers (lid_aud_type);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_aud_datetime_idx
    ON cts_transactions.ct_location_identifiers (lid_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_trans_type_idx
    ON cts_transactions.ct_location_party_rels (trans_type);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_aud_id_idx
    ON cts_transactions.ct_location_party_rels (lpr_aud_id);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_aud_type_idx
    ON cts_transactions.ct_location_party_rels (lpr_aud_type);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_aud_datetime_idx
    ON cts_transactions.ct_location_party_rels (lpr_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_relationships_trans_type_idx
    ON cts_transactions.ct_location_relationships (trans_type);
CREATE INDEX IF NOT EXISTS ct_location_relationships_aud_id_idx
    ON cts_transactions.ct_location_relationships (llr_aud_id);
CREATE INDEX IF NOT EXISTS ct_location_relationships_aud_type_idx
    ON cts_transactions.ct_location_relationships (llr_aud_type);
CREATE INDEX IF NOT EXISTS ct_location_relationships_aud_datetime_idx
    ON cts_transactions.ct_location_relationships (llr_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_locations_trans_type_idx
    ON cts_transactions.ct_locations (trans_type);
CREATE INDEX IF NOT EXISTS ct_locations_aud_id_idx
    ON cts_transactions.ct_locations (loc_aud_id);
CREATE INDEX IF NOT EXISTS ct_locations_aud_type_idx
    ON cts_transactions.ct_locations (loc_aud_type);
CREATE INDEX IF NOT EXISTS ct_locations_aud_datetime_idx
    ON cts_transactions.ct_locations (loc_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_locations_faker_trans_type_idx
    ON cts_transactions.ct_locations_faker (trans_type);
CREATE INDEX IF NOT EXISTS ct_locations_faker_aud_id_idx
    ON cts_transactions.ct_locations_faker (loc_aud_id);
CREATE INDEX IF NOT EXISTS ct_locations_faker_aud_type_idx
    ON cts_transactions.ct_locations_faker (loc_aud_type);
CREATE INDEX IF NOT EXISTS ct_locations_faker_aud_datetime_idx
    ON cts_transactions.ct_locations_faker (loc_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_trans_type_idx
    ON cts_transactions.ct_locrestrictionstoanimals (trans_type);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_aud_id_idx
    ON cts_transactions.ct_locrestrictionstoanimals (lra_aud_id);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_aud_type_idx
    ON cts_transactions.ct_locrestrictionstoanimals (lra_aud_type);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_aud_datetime_idx
    ON cts_transactions.ct_locrestrictionstoanimals (lra_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_trans_type_idx
    ON cts_transactions.ct_mgt_control_errors (trans_type);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_aud_id_idx
    ON cts_transactions.ct_mgt_control_errors (mce_aud_id);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_aud_type_idx
    ON cts_transactions.ct_mgt_control_errors (mce_aud_type);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_aud_datetime_idx
    ON cts_transactions.ct_mgt_control_errors (mce_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_trans_type_idx
    ON cts_transactions.ct_mhs_to_cph (trans_type);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_aud_id_idx
    ON cts_transactions.ct_mhs_to_cph (cph_aud_id);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_aud_type_idx
    ON cts_transactions.ct_mhs_to_cph (cph_aud_type);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_aud_datetime_idx
    ON cts_transactions.ct_mhs_to_cph (cph_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_mov_hst_trans_type_idx
    ON cts_transactions.ct_mov_hst (trans_type);
CREATE INDEX IF NOT EXISTS ct_mov_hst_aud_id_idx
    ON cts_transactions.ct_mov_hst (hst_aud_id);
CREATE INDEX IF NOT EXISTS ct_mov_hst_aud_type_idx
    ON cts_transactions.ct_mov_hst (hst_aud_type);
CREATE INDEX IF NOT EXISTS ct_mov_hst_aud_datetime_idx
    ON cts_transactions.ct_mov_hst (hst_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_trans_type_idx
    ON cts_transactions.ct_movt_corr_summ_errors (trans_type);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_aud_id_idx
    ON cts_transactions.ct_movt_corr_summ_errors (mse_aud_id);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_aud_type_idx
    ON cts_transactions.ct_movt_corr_summ_errors (mse_aud_type);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_aud_datetime_idx
    ON cts_transactions.ct_movt_corr_summ_errors (mse_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_trans_type_idx
    ON cts_transactions.ct_movt_correct_summaries (trans_type);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_aud_id_idx
    ON cts_transactions.ct_movt_correct_summaries (mcs_aud_id);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_aud_type_idx
    ON cts_transactions.ct_movt_correct_summaries (mcs_aud_type);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_aud_datetime_idx
    ON cts_transactions.ct_movt_correct_summaries (mcs_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_parties_trans_type_idx
    ON cts_transactions.ct_parties (trans_type);
CREATE INDEX IF NOT EXISTS ct_parties_aud_id_idx
    ON cts_transactions.ct_parties (par_aud_id);
CREATE INDEX IF NOT EXISTS ct_parties_aud_type_idx
    ON cts_transactions.ct_parties (par_aud_type);
CREATE INDEX IF NOT EXISTS ct_parties_aud_datetime_idx
    ON cts_transactions.ct_parties (par_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_parties_faker_trans_type_idx
    ON cts_transactions.ct_parties_faker (trans_type);
CREATE INDEX IF NOT EXISTS ct_parties_faker_aud_id_idx
    ON cts_transactions.ct_parties_faker (par_aud_id);
CREATE INDEX IF NOT EXISTS ct_parties_faker_aud_type_idx
    ON cts_transactions.ct_parties_faker (par_aud_type);
CREATE INDEX IF NOT EXISTS ct_parties_faker_aud_datetime_idx
    ON cts_transactions.ct_parties_faker (par_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_trans_type_idx
    ON cts_transactions.ct_ppaf_groupings (trans_type);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_aud_id_idx
    ON cts_transactions.ct_ppaf_groupings (ppg_aud_id);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_aud_type_idx
    ON cts_transactions.ct_ppaf_groupings (ppg_aud_type);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_aud_datetime_idx
    ON cts_transactions.ct_ppaf_groupings (ppg_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_trans_type_idx
    ON cts_transactions.ct_preprinted_appn_forms (trans_type);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_aud_id_idx
    ON cts_transactions.ct_preprinted_appn_forms (paf_aud_id);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_aud_type_idx
    ON cts_transactions.ct_preprinted_appn_forms (paf_aud_type);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_aud_datetime_idx
    ON cts_transactions.ct_preprinted_appn_forms (paf_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_trans_type_idx
    ON cts_transactions.ct_ps9999_ahdb_data (trans_type);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_aud_id_idx
    ON cts_transactions.ct_ps9999_ahdb_data (ran_aud_id);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_aud_type_idx
    ON cts_transactions.ct_ps9999_ahdb_data (ran_aud_type);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_aud_datetime_idx
    ON cts_transactions.ct_ps9999_ahdb_data (ran_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_trans_type_idx
    ON cts_transactions.ct_ps9999_ahdb_mov_history (trans_type);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_aud_id_idx
    ON cts_transactions.ct_ps9999_ahdb_mov_history (loc_aud_id);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_aud_type_idx
    ON cts_transactions.ct_ps9999_ahdb_mov_history (loc_aud_type);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_aud_datetime_idx
    ON cts_transactions.ct_ps9999_ahdb_mov_history (loc_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_trans_type_idx
    ON cts_transactions.ct_recd_application_errors (trans_type);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_aud_id_idx
    ON cts_transactions.ct_recd_application_errors (rae_aud_id);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_aud_type_idx
    ON cts_transactions.ct_recd_application_errors (rae_aud_type);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_aud_datetime_idx
    ON cts_transactions.ct_recd_application_errors (rae_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_trans_type_idx
    ON cts_transactions.ct_recd_movement_errors (trans_type);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_aud_id_idx
    ON cts_transactions.ct_recd_movement_errors (rme_aud_id);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_aud_type_idx
    ON cts_transactions.ct_recd_movement_errors (rme_aud_type);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_aud_datetime_idx
    ON cts_transactions.ct_recd_movement_errors (rme_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_received_applications_trans_type_idx
    ON cts_transactions.ct_received_applications (trans_type);
CREATE INDEX IF NOT EXISTS ct_received_applications_aud_id_idx
    ON cts_transactions.ct_received_applications (rap_aud_id);
CREATE INDEX IF NOT EXISTS ct_received_applications_aud_type_idx
    ON cts_transactions.ct_received_applications (rap_aud_type);
CREATE INDEX IF NOT EXISTS ct_received_applications_aud_datetime_idx
    ON cts_transactions.ct_received_applications (rap_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_received_movements_trans_type_idx
    ON cts_transactions.ct_received_movements (trans_type);
CREATE INDEX IF NOT EXISTS ct_received_movements_aud_id_idx
    ON cts_transactions.ct_received_movements (rmo_aud_id);
CREATE INDEX IF NOT EXISTS ct_received_movements_aud_type_idx
    ON cts_transactions.ct_received_movements (rmo_aud_type);
CREATE INDEX IF NOT EXISTS ct_received_movements_aud_datetime_idx
    ON cts_transactions.ct_received_movements (rmo_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_registered_animals_trans_type_idx
    ON cts_transactions.ct_registered_animals (trans_type);
CREATE INDEX IF NOT EXISTS ct_registered_animals_aud_id_idx
    ON cts_transactions.ct_registered_animals (ran_aud_id);
CREATE INDEX IF NOT EXISTS ct_registered_animals_aud_type_idx
    ON cts_transactions.ct_registered_animals (ran_aud_type);
CREATE INDEX IF NOT EXISTS ct_registered_animals_aud_datetime_idx
    ON cts_transactions.ct_registered_animals (ran_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_registered_movements_trans_type_idx
    ON cts_transactions.ct_registered_movements (trans_type);
CREATE INDEX IF NOT EXISTS ct_registered_movements_aud_id_idx
    ON cts_transactions.ct_registered_movements (mov_aud_id);
CREATE INDEX IF NOT EXISTS ct_registered_movements_aud_type_idx
    ON cts_transactions.ct_registered_movements (mov_aud_type);
CREATE INDEX IF NOT EXISTS ct_registered_movements_aud_datetime_idx
    ON cts_transactions.ct_registered_movements (mov_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_trans_type_idx
    ON cts_transactions.ct_reset_to_extract (trans_type);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_aud_id_idx
    ON cts_transactions.ct_reset_to_extract (rte_aud_id);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_aud_type_idx
    ON cts_transactions.ct_reset_to_extract (rte_aud_type);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_aud_datetime_idx
    ON cts_transactions.ct_reset_to_extract (rte_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_trans_type_idx
    ON cts_transactions.ct_sbcs_ext (trans_type);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_aud_id_idx
    ON cts_transactions.ct_sbcs_ext (sxt_aud_id);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_aud_type_idx
    ON cts_transactions.ct_sbcs_ext (sxt_aud_type);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_aud_datetime_idx
    ON cts_transactions.ct_sbcs_ext (sxt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_stage_files_trans_type_idx
    ON cts_transactions.ct_stage_files (trans_type);
CREATE INDEX IF NOT EXISTS ct_stage_files_aud_id_idx
    ON cts_transactions.ct_stage_files (stf_aud_id);
CREATE INDEX IF NOT EXISTS ct_stage_files_aud_type_idx
    ON cts_transactions.ct_stage_files (stf_aud_type);
CREATE INDEX IF NOT EXISTS ct_stage_files_aud_datetime_idx
    ON cts_transactions.ct_stage_files (stf_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_stage_locks_trans_type_idx
    ON cts_transactions.ct_stage_locks (trans_type);
CREATE INDEX IF NOT EXISTS ct_stage_locks_aud_id_idx
    ON cts_transactions.ct_stage_locks (stl_aud_id);
CREATE INDEX IF NOT EXISTS ct_stage_locks_aud_type_idx
    ON cts_transactions.ct_stage_locks (stl_aud_type);
CREATE INDEX IF NOT EXISTS ct_stage_locks_aud_datetime_idx
    ON cts_transactions.ct_stage_locks (stl_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_stage_messages_trans_type_idx
    ON cts_transactions.ct_stage_messages (trans_type);
CREATE INDEX IF NOT EXISTS ct_stage_messages_aud_id_idx
    ON cts_transactions.ct_stage_messages (stm_aud_id);
CREATE INDEX IF NOT EXISTS ct_stage_messages_aud_type_idx
    ON cts_transactions.ct_stage_messages (stm_aud_type);
CREATE INDEX IF NOT EXISTS ct_stage_messages_aud_datetime_idx
    ON cts_transactions.ct_stage_messages (stm_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_trans_type_idx
    ON cts_transactions.ct_susp_animal_errors (trans_type);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_aud_id_idx
    ON cts_transactions.ct_susp_animal_errors (sae_aud_id);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_aud_type_idx
    ON cts_transactions.ct_susp_animal_errors (sae_aud_type);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_aud_datetime_idx
    ON cts_transactions.ct_susp_animal_errors (sae_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_trans_type_idx
    ON cts_transactions.ct_susp_cm_measure_results (trans_type);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_aud_id_idx
    ON cts_transactions.ct_susp_cm_measure_results (smr_aud_id);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_aud_type_idx
    ON cts_transactions.ct_susp_cm_measure_results (smr_aud_type);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_aud_datetime_idx
    ON cts_transactions.ct_susp_cm_measure_results (smr_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_trans_type_idx
    ON cts_transactions.ct_susp_condition_markers (trans_type);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_aud_id_idx
    ON cts_transactions.ct_susp_condition_markers (scm_aud_id);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_aud_type_idx
    ON cts_transactions.ct_susp_condition_markers (scm_aud_type);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_aud_datetime_idx
    ON cts_transactions.ct_susp_condition_markers (scm_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_trans_type_idx
    ON cts_transactions.ct_susp_movement_errors (trans_type);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_aud_id_idx
    ON cts_transactions.ct_susp_movement_errors (sme_aud_id);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_aud_type_idx
    ON cts_transactions.ct_susp_movement_errors (sme_aud_type);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_aud_datetime_idx
    ON cts_transactions.ct_susp_movement_errors (sme_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_trans_type_idx
    ON cts_transactions.ct_suspended_animals (trans_type);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_aud_id_idx
    ON cts_transactions.ct_suspended_animals (san_aud_id);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_aud_type_idx
    ON cts_transactions.ct_suspended_animals (san_aud_type);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_aud_datetime_idx
    ON cts_transactions.ct_suspended_animals (san_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_trans_type_idx
    ON cts_transactions.ct_suspended_movements (trans_type);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_aud_id_idx
    ON cts_transactions.ct_suspended_movements (smo_aud_id);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_aud_type_idx
    ON cts_transactions.ct_suspended_movements (smo_aud_type);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_aud_datetime_idx
    ON cts_transactions.ct_suspended_movements (smo_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_valid_applications_trans_type_idx
    ON cts_transactions.ct_valid_applications (trans_type);
CREATE INDEX IF NOT EXISTS ct_valid_applications_aud_id_idx
    ON cts_transactions.ct_valid_applications (vap_aud_id);
CREATE INDEX IF NOT EXISTS ct_valid_applications_aud_type_idx
    ON cts_transactions.ct_valid_applications (vap_aud_type);
CREATE INDEX IF NOT EXISTS ct_valid_applications_aud_datetime_idx
    ON cts_transactions.ct_valid_applications (vap_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_web_users_trans_type_idx
    ON cts_transactions.ct_web_users (trans_type);
CREATE INDEX IF NOT EXISTS ct_web_users_aud_id_idx
    ON cts_transactions.ct_web_users (wur_aud_id);
CREATE INDEX IF NOT EXISTS ct_web_users_aud_type_idx
    ON cts_transactions.ct_web_users (wur_aud_type);
CREATE INDEX IF NOT EXISTS ct_web_users_aud_datetime_idx
    ON cts_transactions.ct_web_users (wur_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_trans_type_idx
    ON cts_transactions.ct_wg_autoallocations (trans_type);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_aud_id_idx
    ON cts_transactions.ct_wg_autoallocations (wga_aud_id);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_aud_type_idx
    ON cts_transactions.ct_wg_autoallocations (wga_aud_type);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_aud_datetime_idx
    ON cts_transactions.ct_wg_autoallocations (wga_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_trans_type_idx
    ON cts_transactions.ct_wg_super_assignments (trans_type);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_aud_id_idx
    ON cts_transactions.ct_wg_super_assignments (wsa_aud_id);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_aud_type_idx
    ON cts_transactions.ct_wg_super_assignments (wsa_aud_type);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_aud_datetime_idx
    ON cts_transactions.ct_wg_super_assignments (wsa_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_trans_type_idx
    ON cts_transactions.ct_wg_user_assignments (trans_type);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_aud_id_idx
    ON cts_transactions.ct_wg_user_assignments (wua_aud_id);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_aud_type_idx
    ON cts_transactions.ct_wg_user_assignments (wua_aud_type);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_aud_datetime_idx
    ON cts_transactions.ct_wg_user_assignments (wua_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_workgroups_trans_type_idx
    ON cts_transactions.ct_workgroups (trans_type);
CREATE INDEX IF NOT EXISTS ct_workgroups_aud_id_idx
    ON cts_transactions.ct_workgroups (wgp_aud_id);
CREATE INDEX IF NOT EXISTS ct_workgroups_aud_type_idx
    ON cts_transactions.ct_workgroups (wgp_aud_type);
CREATE INDEX IF NOT EXISTS ct_workgroups_aud_datetime_idx
    ON cts_transactions.ct_workgroups (wgp_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_trans_type_idx
    ON cts_transactions.ct_alloc_routines (trans_type);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_aud_id_idx
    ON cts_transactions.ct_alloc_routines (rou_aud_id);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_aud_type_idx
    ON cts_transactions.ct_alloc_routines (rou_aud_type);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_aud_datetime_idx
    ON cts_transactions.ct_alloc_routines (rou_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_batch_retention_conf_trans_type_idx
    ON cts_transactions.ct_batch_retention_conf (trans_type);
CREATE INDEX IF NOT EXISTS ct_batch_retention_conf_aud_id_idx
    ON cts_transactions.ct_batch_retention_conf (brt_aud_id);
CREATE INDEX IF NOT EXISTS ct_batch_retention_conf_aud_type_idx
    ON cts_transactions.ct_batch_retention_conf (brt_aud_type);
CREATE INDEX IF NOT EXISTS ct_batch_retention_conf_aud_datetime_idx
    ON cts_transactions.ct_batch_retention_conf (brt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_breeds_trans_type_idx
    ON cts_transactions.ct_breeds (trans_type);
CREATE INDEX IF NOT EXISTS ct_breeds_aud_id_idx
    ON cts_transactions.ct_breeds (brd_aud_id);
CREATE INDEX IF NOT EXISTS ct_breeds_aud_type_idx
    ON cts_transactions.ct_breeds (brd_aud_type);
CREATE INDEX IF NOT EXISTS ct_breeds_aud_datetime_idx
    ON cts_transactions.ct_breeds (brd_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_trans_type_idx
    ON cts_transactions.ct_claim_statuses (trans_type);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_aud_id_idx
    ON cts_transactions.ct_claim_statuses (cls_aud_id);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_aud_type_idx
    ON cts_transactions.ct_claim_statuses (cls_aud_type);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_aud_datetime_idx
    ON cts_transactions.ct_claim_statuses (cls_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_claim_types_trans_type_idx
    ON cts_transactions.ct_claim_types (trans_type);
CREATE INDEX IF NOT EXISTS ct_claim_types_aud_id_idx
    ON cts_transactions.ct_claim_types (clt_aud_id);
CREATE INDEX IF NOT EXISTS ct_claim_types_aud_type_idx
    ON cts_transactions.ct_claim_types (clt_aud_type);
CREATE INDEX IF NOT EXISTS ct_claim_types_aud_datetime_idx
    ON cts_transactions.ct_claim_types (clt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_trans_type_idx
    ON cts_transactions.ct_cm_authorities (trans_type);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_aud_id_idx
    ON cts_transactions.ct_cm_authorities (cma_aud_id);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_aud_type_idx
    ON cts_transactions.ct_cm_authorities (cma_aud_type);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_aud_datetime_idx
    ON cts_transactions.ct_cm_authorities (cma_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_trans_type_idx
    ON cts_transactions.ct_cond_variant_groupings (trans_type);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_aud_id_idx
    ON cts_transactions.ct_cond_variant_groupings (cvg_aud_id);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_aud_type_idx
    ON cts_transactions.ct_cond_variant_groupings (cvg_aud_type);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_aud_datetime_idx
    ON cts_transactions.ct_cond_variant_groupings (cvg_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_condition_activities_trans_type_idx
    ON cts_transactions.ct_condition_activities (trans_type);
CREATE INDEX IF NOT EXISTS ct_condition_activities_aud_id_idx
    ON cts_transactions.ct_condition_activities (cac_aud_id);
CREATE INDEX IF NOT EXISTS ct_condition_activities_aud_type_idx
    ON cts_transactions.ct_condition_activities (cac_aud_type);
CREATE INDEX IF NOT EXISTS ct_condition_activities_aud_datetime_idx
    ON cts_transactions.ct_condition_activities (cac_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_condition_types_trans_type_idx
    ON cts_transactions.ct_condition_types (trans_type);
CREATE INDEX IF NOT EXISTS ct_condition_types_aud_id_idx
    ON cts_transactions.ct_condition_types (cot_aud_id);
CREATE INDEX IF NOT EXISTS ct_condition_types_aud_type_idx
    ON cts_transactions.ct_condition_types (cot_aud_type);
CREATE INDEX IF NOT EXISTS ct_condition_types_aud_datetime_idx
    ON cts_transactions.ct_condition_types (cot_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_condition_variants_trans_type_idx
    ON cts_transactions.ct_condition_variants (trans_type);
CREATE INDEX IF NOT EXISTS ct_condition_variants_aud_id_idx
    ON cts_transactions.ct_condition_variants (cov_aud_id);
CREATE INDEX IF NOT EXISTS ct_condition_variants_aud_type_idx
    ON cts_transactions.ct_condition_variants (cov_aud_type);
CREATE INDEX IF NOT EXISTS ct_condition_variants_aud_datetime_idx
    ON cts_transactions.ct_condition_variants (cov_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_conditions_trans_type_idx
    ON cts_transactions.ct_conditions (trans_type);
CREATE INDEX IF NOT EXISTS ct_conditions_aud_id_idx
    ON cts_transactions.ct_conditions (con_aud_id);
CREATE INDEX IF NOT EXISTS ct_conditions_aud_type_idx
    ON cts_transactions.ct_conditions (con_aud_type);
CREATE INDEX IF NOT EXISTS ct_conditions_aud_datetime_idx
    ON cts_transactions.ct_conditions (con_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_counties_trans_type_idx
    ON cts_transactions.ct_counties (trans_type);
CREATE INDEX IF NOT EXISTS ct_counties_aud_id_idx
    ON cts_transactions.ct_counties (cty_aud_id);
CREATE INDEX IF NOT EXISTS ct_counties_aud_type_idx
    ON cts_transactions.ct_counties (cty_aud_type);
CREATE INDEX IF NOT EXISTS ct_counties_aud_datetime_idx
    ON cts_transactions.ct_counties (cty_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_counties_migration_trans_type_idx
    ON cts_transactions.ct_counties_migration (trans_type);
CREATE INDEX IF NOT EXISTS ct_counties_migration_aud_id_idx
    ON cts_transactions.ct_counties_migration (cty_aud_id);
CREATE INDEX IF NOT EXISTS ct_counties_migration_aud_type_idx
    ON cts_transactions.ct_counties_migration (cty_aud_type);
CREATE INDEX IF NOT EXISTS ct_counties_migration_aud_datetime_idx
    ON cts_transactions.ct_counties_migration (cty_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_countries_trans_type_idx
    ON cts_transactions.ct_countries (trans_type);
CREATE INDEX IF NOT EXISTS ct_countries_aud_id_idx
    ON cts_transactions.ct_countries (cry_aud_id);
CREATE INDEX IF NOT EXISTS ct_countries_aud_type_idx
    ON cts_transactions.ct_countries (cry_aud_type);
CREATE INDEX IF NOT EXISTS ct_countries_aud_datetime_idx
    ON cts_transactions.ct_countries (cry_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cts164_handshake_file_keys_trans_type_idx
    ON cts_transactions.ct_cts164_handshake_file_keys (trans_type);
CREATE INDEX IF NOT EXISTS ct_cts164_handshake_file_keys_aud_id_idx
    ON cts_transactions.ct_cts164_handshake_file_keys (bjk_aud_id);
CREATE INDEX IF NOT EXISTS ct_cts164_handshake_file_keys_aud_type_idx
    ON cts_transactions.ct_cts164_handshake_file_keys (bjk_aud_type);
CREATE INDEX IF NOT EXISTS ct_cts164_handshake_file_keys_aud_datetime_idx
    ON cts_transactions.ct_cts164_handshake_file_keys (bjk_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_trans_type_idx
    ON cts_transactions.ct_eartag_formats (trans_type);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_aud_id_idx
    ON cts_transactions.ct_eartag_formats (etf_aud_id);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_aud_type_idx
    ON cts_transactions.ct_eartag_formats (etf_aud_type);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_aud_datetime_idx
    ON cts_transactions.ct_eartag_formats (etf_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_trans_type_idx
    ON cts_transactions.ct_eartag_reason_flags (trans_type);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_aud_id_idx
    ON cts_transactions.ct_eartag_reason_flags (erf_aud_id);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_aud_type_idx
    ON cts_transactions.ct_eartag_reason_flags (erf_aud_type);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_aud_datetime_idx
    ON cts_transactions.ct_eartag_reason_flags (erf_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_trans_type_idx
    ON cts_transactions.ct_eartag_reasons (trans_type);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_aud_id_idx
    ON cts_transactions.ct_eartag_reasons (etr_aud_id);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_aud_type_idx
    ON cts_transactions.ct_eartag_reasons (etr_aud_type);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_aud_datetime_idx
    ON cts_transactions.ct_eartag_reasons (etr_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartag_types_trans_type_idx
    ON cts_transactions.ct_eartag_types (trans_type);
CREATE INDEX IF NOT EXISTS ct_eartag_types_aud_id_idx
    ON cts_transactions.ct_eartag_types (ett_aud_id);
CREATE INDEX IF NOT EXISTS ct_eartag_types_aud_type_idx
    ON cts_transactions.ct_eartag_types (ett_aud_type);
CREATE INDEX IF NOT EXISTS ct_eartag_types_aud_datetime_idx
    ON cts_transactions.ct_eartag_types (ett_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ext_ni_district_trans_type_idx
    ON cts_transactions.ct_ext_ni_district (trans_type);
CREATE INDEX IF NOT EXISTS ct_ext_ni_district_aud_id_idx
    ON cts_transactions.ct_ext_ni_district (nid_aud_id);
CREATE INDEX IF NOT EXISTS ct_ext_ni_district_aud_type_idx
    ON cts_transactions.ct_ext_ni_district (nid_aud_type);
CREATE INDEX IF NOT EXISTS ct_ext_ni_district_aud_datetime_idx
    ON cts_transactions.ct_ext_ni_district (nid_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ext_special_herd_trans_type_idx
    ON cts_transactions.ct_ext_special_herd (trans_type);
CREATE INDEX IF NOT EXISTS ct_ext_special_herd_aud_id_idx
    ON cts_transactions.ct_ext_special_herd (sph_aud_id);
CREATE INDEX IF NOT EXISTS ct_ext_special_herd_aud_type_idx
    ON cts_transactions.ct_ext_special_herd (sph_aud_type);
CREATE INDEX IF NOT EXISTS ct_ext_special_herd_aud_datetime_idx
    ON cts_transactions.ct_ext_special_herd (sph_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_file_layouts_trans_type_idx
    ON cts_transactions.ct_file_layouts (trans_type);
CREATE INDEX IF NOT EXISTS ct_file_layouts_aud_id_idx
    ON cts_transactions.ct_file_layouts (flt_aud_id);
CREATE INDEX IF NOT EXISTS ct_file_layouts_aud_type_idx
    ON cts_transactions.ct_file_layouts (flt_aud_type);
CREATE INDEX IF NOT EXISTS ct_file_layouts_aud_datetime_idx
    ON cts_transactions.ct_file_layouts (flt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_hsf_sequences_trans_type_idx
    ON cts_transactions.ct_hsf_sequences (trans_type);
CREATE INDEX IF NOT EXISTS ct_hsf_sequences_aud_id_idx
    ON cts_transactions.ct_hsf_sequences (hss_aud_id);
CREATE INDEX IF NOT EXISTS ct_hsf_sequences_aud_type_idx
    ON cts_transactions.ct_hsf_sequences (hss_aud_type);
CREATE INDEX IF NOT EXISTS ct_hsf_sequences_aud_datetime_idx
    ON cts_transactions.ct_hsf_sequences (hss_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_trans_type_idx
    ON cts_transactions.ct_issuing_authorities (trans_type);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_aud_id_idx
    ON cts_transactions.ct_issuing_authorities (isa_aud_id);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_aud_type_idx
    ON cts_transactions.ct_issuing_authorities (isa_aud_type);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_aud_datetime_idx
    ON cts_transactions.ct_issuing_authorities (isa_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_late_days_trans_type_idx
    ON cts_transactions.ct_late_days (trans_type);
CREATE INDEX IF NOT EXISTS ct_late_days_aud_id_idx
    ON cts_transactions.ct_late_days (lda_aud_id);
CREATE INDEX IF NOT EXISTS ct_late_days_aud_type_idx
    ON cts_transactions.ct_late_days (lda_aud_type);
CREATE INDEX IF NOT EXISTS ct_late_days_aud_datetime_idx
    ON cts_transactions.ct_late_days (lda_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_trans_type_idx
    ON cts_transactions.ct_loc_type_rel_combs (trans_type);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_aud_id_idx
    ON cts_transactions.ct_loc_type_rel_combs (lrc_aud_id);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_aud_type_idx
    ON cts_transactions.ct_loc_type_rel_combs (lrc_aud_type);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_aud_datetime_idx
    ON cts_transactions.ct_loc_type_rel_combs (lrc_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_trans_type_idx
    ON cts_transactions.ct_location_id_formats (trans_type);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_aud_id_idx
    ON cts_transactions.ct_location_id_formats (lif_aud_id);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_aud_type_idx
    ON cts_transactions.ct_location_id_formats (lif_aud_type);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_aud_datetime_idx
    ON cts_transactions.ct_location_id_formats (lif_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_trans_type_idx
    ON cts_transactions.ct_location_party_rel_types (trans_type);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_aud_id_idx
    ON cts_transactions.ct_location_party_rel_types (lpt_aud_id);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_aud_type_idx
    ON cts_transactions.ct_location_party_rel_types (lpt_aud_type);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_aud_datetime_idx
    ON cts_transactions.ct_location_party_rel_types (lpt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_trans_type_idx
    ON cts_transactions.ct_location_rel_types (trans_type);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_aud_id_idx
    ON cts_transactions.ct_location_rel_types (lrt_aud_id);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_aud_type_idx
    ON cts_transactions.ct_location_rel_types (lrt_aud_type);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_aud_datetime_idx
    ON cts_transactions.ct_location_rel_types (lrt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_types_trans_type_idx
    ON cts_transactions.ct_location_types (trans_type);
CREATE INDEX IF NOT EXISTS ct_location_types_aud_id_idx
    ON cts_transactions.ct_location_types (lty_aud_id);
CREATE INDEX IF NOT EXISTS ct_location_types_aud_type_idx
    ON cts_transactions.ct_location_types (lty_aud_type);
CREATE INDEX IF NOT EXISTS ct_location_types_aud_datetime_idx
    ON cts_transactions.ct_location_types (lty_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_trans_type_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (trans_type);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_aud_id_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (war_aud_id);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_aud_type_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (war_aud_type);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_aud_datetime_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (war_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_msgtxt_trans_type_idx
    ON cts_transactions.ct_msgtxt (trans_type);
CREATE INDEX IF NOT EXISTS ct_msgtxt_aud_id_idx
    ON cts_transactions.ct_msgtxt (msg_aud_id);
CREATE INDEX IF NOT EXISTS ct_msgtxt_aud_type_idx
    ON cts_transactions.ct_msgtxt (msg_aud_type);
CREATE INDEX IF NOT EXISTS ct_msgtxt_aud_datetime_idx
    ON cts_transactions.ct_msgtxt (msg_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_non_working_days_trans_type_idx
    ON cts_transactions.ct_non_working_days (trans_type);
CREATE INDEX IF NOT EXISTS ct_non_working_days_aud_id_idx
    ON cts_transactions.ct_non_working_days (nwd_aud_id);
CREATE INDEX IF NOT EXISTS ct_non_working_days_aud_type_idx
    ON cts_transactions.ct_non_working_days (nwd_aud_type);
CREATE INDEX IF NOT EXISTS ct_non_working_days_aud_datetime_idx
    ON cts_transactions.ct_non_working_days (nwd_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_param_group_trans_type_idx
    ON cts_transactions.ct_param_group (trans_type);
CREATE INDEX IF NOT EXISTS ct_param_group_aud_id_idx
    ON cts_transactions.ct_param_group (pgp_aud_id);
CREATE INDEX IF NOT EXISTS ct_param_group_aud_type_idx
    ON cts_transactions.ct_param_group (pgp_aud_type);
CREATE INDEX IF NOT EXISTS ct_param_group_aud_datetime_idx
    ON cts_transactions.ct_param_group (pgp_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_param_header_trans_type_idx
    ON cts_transactions.ct_param_header (trans_type);
CREATE INDEX IF NOT EXISTS ct_param_header_aud_id_idx
    ON cts_transactions.ct_param_header (phd_aud_id);
CREATE INDEX IF NOT EXISTS ct_param_header_aud_type_idx
    ON cts_transactions.ct_param_header (phd_aud_type);
CREATE INDEX IF NOT EXISTS ct_param_header_aud_datetime_idx
    ON cts_transactions.ct_param_header (phd_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_param_value_trans_type_idx
    ON cts_transactions.ct_param_value (trans_type);
CREATE INDEX IF NOT EXISTS ct_param_value_aud_id_idx
    ON cts_transactions.ct_param_value (pvl_aud_id);
CREATE INDEX IF NOT EXISTS ct_param_value_aud_type_idx
    ON cts_transactions.ct_param_value (pvl_aud_type);
CREATE INDEX IF NOT EXISTS ct_param_value_aud_datetime_idx
    ON cts_transactions.ct_param_value (pvl_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_param_value_group_trans_type_idx
    ON cts_transactions.ct_param_value_group (trans_type);
CREATE INDEX IF NOT EXISTS ct_param_value_group_aud_id_idx
    ON cts_transactions.ct_param_value_group (pvg_aud_id);
CREATE INDEX IF NOT EXISTS ct_param_value_group_aud_type_idx
    ON cts_transactions.ct_param_value_group (pvg_aud_type);
CREATE INDEX IF NOT EXISTS ct_param_value_group_aud_datetime_idx
    ON cts_transactions.ct_param_value_group (pvg_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_probity_checks_trans_type_idx
    ON cts_transactions.ct_probity_checks (trans_type);
CREATE INDEX IF NOT EXISTS ct_probity_checks_aud_id_idx
    ON cts_transactions.ct_probity_checks (pch_aud_id);
CREATE INDEX IF NOT EXISTS ct_probity_checks_aud_type_idx
    ON cts_transactions.ct_probity_checks (pch_aud_type);
CREATE INDEX IF NOT EXISTS ct_probity_checks_aud_datetime_idx
    ON cts_transactions.ct_probity_checks (pch_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_schemes_trans_type_idx
    ON cts_transactions.ct_schemes (trans_type);
CREATE INDEX IF NOT EXISTS ct_schemes_aud_id_idx
    ON cts_transactions.ct_schemes (sch_aud_id);
CREATE INDEX IF NOT EXISTS ct_schemes_aud_type_idx
    ON cts_transactions.ct_schemes (sch_aud_type);
CREATE INDEX IF NOT EXISTS ct_schemes_aud_datetime_idx
    ON cts_transactions.ct_schemes (sch_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_trans_type_idx
    ON cts_transactions.ct_sublocation_types (trans_type);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_aud_id_idx
    ON cts_transactions.ct_sublocation_types (slt_aud_id);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_aud_type_idx
    ON cts_transactions.ct_sublocation_types (slt_aud_type);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_aud_datetime_idx
    ON cts_transactions.ct_sublocation_types (slt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_trans_type_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (trans_type);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_aud_id_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (sca_aud_id);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_aud_type_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (sca_aud_type);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_aud_datetime_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (sca_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_trans_type_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (trans_type);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_aud_id_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (swa_aud_id);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_aud_type_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (swa_aud_type);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_aud_datetime_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (swa_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_source_key_idx
    ON cts_transactions.ct_alloc_routines (rou_id);
CREATE INDEX IF NOT EXISTS ct_breeds_source_key_idx
    ON cts_transactions.ct_breeds (brd_id);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_source_key_idx
    ON cts_transactions.ct_claim_statuses (cls_id);
CREATE INDEX IF NOT EXISTS ct_claim_types_source_key_idx
    ON cts_transactions.ct_claim_types (clt_id);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_source_key_idx
    ON cts_transactions.ct_cm_authorities (cma_id);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_source_key_idx
    ON cts_transactions.ct_cond_variant_groupings (cvg_id);
CREATE INDEX IF NOT EXISTS ct_condition_activities_source_key_idx
    ON cts_transactions.ct_condition_activities (cac_id);
CREATE INDEX IF NOT EXISTS ct_condition_types_source_key_idx
    ON cts_transactions.ct_condition_types (cot_id);
CREATE INDEX IF NOT EXISTS ct_condition_variants_source_key_idx
    ON cts_transactions.ct_condition_variants (cov_id);
CREATE INDEX IF NOT EXISTS ct_conditions_source_key_idx
    ON cts_transactions.ct_conditions (con_id);
CREATE INDEX IF NOT EXISTS ct_counties_source_key_idx
    ON cts_transactions.ct_counties (cty_id);
CREATE INDEX IF NOT EXISTS ct_counties_migration_source_key_idx
    ON cts_transactions.ct_counties_migration (cty_id);
CREATE INDEX IF NOT EXISTS ct_countries_source_key_idx
    ON cts_transactions.ct_countries (cry_id);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_source_key_idx
    ON cts_transactions.ct_eartag_formats (etf_id);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_source_key_idx
    ON cts_transactions.ct_eartag_reason_flags (erf_id);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_source_key_idx
    ON cts_transactions.ct_eartag_reasons (etr_id);
CREATE INDEX IF NOT EXISTS ct_eartag_types_source_key_idx
    ON cts_transactions.ct_eartag_types (ett_id);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_source_key_idx
    ON cts_transactions.ct_issuing_authorities (isa_id);
CREATE INDEX IF NOT EXISTS ct_late_days_source_key_idx
    ON cts_transactions.ct_late_days (lda_id);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_source_key_idx
    ON cts_transactions.ct_loc_type_rel_combs (lrc_id);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_source_key_idx
    ON cts_transactions.ct_location_id_formats (lif_id);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_source_key_idx
    ON cts_transactions.ct_location_party_rel_types (lpt_id);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_source_key_idx
    ON cts_transactions.ct_location_rel_types (lrt_id);
CREATE INDEX IF NOT EXISTS ct_location_types_source_key_idx
    ON cts_transactions.ct_location_types (lty_id);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_source_key_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (war_id);
CREATE INDEX IF NOT EXISTS ct_non_working_days_source_key_idx
    ON cts_transactions.ct_non_working_days (nwd_id);
CREATE INDEX IF NOT EXISTS ct_param_group_source_key_idx
    ON cts_transactions.ct_param_group (pgp_id);
CREATE INDEX IF NOT EXISTS ct_param_header_source_key_idx
    ON cts_transactions.ct_param_header (phd_id);
CREATE INDEX IF NOT EXISTS ct_param_value_source_key_idx
    ON cts_transactions.ct_param_value (pvl_id);
CREATE INDEX IF NOT EXISTS ct_param_value_group_source_key_idx
    ON cts_transactions.ct_param_value_group (pvg_id);
CREATE INDEX IF NOT EXISTS ct_probity_checks_source_key_idx
    ON cts_transactions.ct_probity_checks (pch_id);
CREATE INDEX IF NOT EXISTS ct_schemes_source_key_idx
    ON cts_transactions.ct_schemes (sch_id);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_source_key_idx
    ON cts_transactions.ct_sublocation_types (slt_id);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_source_key_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (sca_id);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_source_key_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (swa_id);
CREATE INDEX IF NOT EXISTS ct_addresses_import_row_idx
    ON cts_transactions.ct_addresses (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_animal_changes_import_row_idx
    ON cts_transactions.ct_animal_changes (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_animal_claims_import_row_idx
    ON cts_transactions.ct_animal_claims (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_import_row_idx
    ON cts_transactions.ct_animal_corr_summ_errors (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_import_row_idx
    ON cts_transactions.ct_animal_correct_summaries (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_import_row_idx
    ON cts_transactions.ct_animal_identifiers (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_import_row_idx
    ON cts_transactions.ct_animal_relationships (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_import_row_idx
    ON cts_transactions.ct_animal_statuses (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_import_row_idx
    ON cts_transactions.ct_applic_statuses (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_application_late_days_import_row_idx
    ON cts_transactions.ct_application_late_days (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_cla_extract_import_row_idx
    ON cts_transactions.ct_cla_extract (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_import_row_idx
    ON cts_transactions.ct_cla_extract_detail (cts_file_import_id);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_import_row_idx
    ON cts_transactions.ct_cla_extract_dm (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_import_row_idx
    ON cts_transactions.ct_cla_mini_detail (cts_file_import_id);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_import_row_idx
    ON cts_transactions.ct_cla_mini_extract (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_import_row_idx
    ON cts_transactions.ct_cm_measures_results (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_import_row_idx
    ON cts_transactions.ct_comms_addresses (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_import_row_idx
    ON cts_transactions.ct_condition_marker_errors (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_condition_markers_import_row_idx
    ON cts_transactions.ct_condition_markers (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_cps167_report_import_row_idx
    ON cts_transactions.ct_cps167_report (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_cts_users_import_row_idx
    ON cts_transactions.ct_cts_users (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_import_row_idx
    ON cts_transactions.ct_eartag_staging (cts_file_import_id);
CREATE INDEX IF NOT EXISTS ct_eartags_import_row_idx
    ON cts_transactions.ct_eartags (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_import_row_idx
    ON cts_transactions.ct_electronic_identifiers (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_email_log_import_row_idx
    ON cts_transactions.ct_email_log (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_ereport_files_import_row_idx
    ON cts_transactions.ct_ereport_files (cts_file_import_id);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_import_row_idx
    ON cts_transactions.ct_ereport_load_messages (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_ereport_locks_import_row_idx
    ON cts_transactions.ct_ereport_locks (cts_file_import_id);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_import_row_idx
    ON cts_transactions.ct_ereport_process_messages (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_import_row_idx
    ON cts_transactions.ct_ext_cetd_eartag (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_import_row_idx
    ON cts_transactions.ct_insert_update_log (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_issued_documents_import_row_idx
    ON cts_transactions.ct_issued_documents (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_label_requests_import_row_idx
    ON cts_transactions.ct_label_requests (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_label_summaries_import_row_idx
    ON cts_transactions.ct_label_summaries (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_letters_import_row_idx
    ON cts_transactions.ct_letters (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_import_row_idx
    ON cts_transactions.ct_location_identifiers (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_import_row_idx
    ON cts_transactions.ct_location_party_rels (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_location_relationships_import_row_idx
    ON cts_transactions.ct_location_relationships (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_locations_import_row_idx
    ON cts_transactions.ct_locations (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_locations_faker_import_row_idx
    ON cts_transactions.ct_locations_faker (cts_file_import_id);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_import_row_idx
    ON cts_transactions.ct_locrestrictionstoanimals (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_import_row_idx
    ON cts_transactions.ct_mgt_control_errors (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_import_row_idx
    ON cts_transactions.ct_mhs_to_cph (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_mov_hst_import_row_idx
    ON cts_transactions.ct_mov_hst (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_import_row_idx
    ON cts_transactions.ct_movt_corr_summ_errors (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_import_row_idx
    ON cts_transactions.ct_movt_correct_summaries (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_parties_import_row_idx
    ON cts_transactions.ct_parties (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_parties_faker_import_row_idx
    ON cts_transactions.ct_parties_faker (cts_file_import_id);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_import_row_idx
    ON cts_transactions.ct_ppaf_groupings (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_import_row_idx
    ON cts_transactions.ct_preprinted_appn_forms (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_import_row_idx
    ON cts_transactions.ct_ps9999_ahdb_data (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_import_row_idx
    ON cts_transactions.ct_ps9999_ahdb_mov_history (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_import_row_idx
    ON cts_transactions.ct_recd_application_errors (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_import_row_idx
    ON cts_transactions.ct_recd_movement_errors (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_received_applications_import_row_idx
    ON cts_transactions.ct_received_applications (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_received_movements_import_row_idx
    ON cts_transactions.ct_received_movements (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_registered_animals_import_row_idx
    ON cts_transactions.ct_registered_animals (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_registered_movements_import_row_idx
    ON cts_transactions.ct_registered_movements (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_import_row_idx
    ON cts_transactions.ct_reset_to_extract (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_import_row_idx
    ON cts_transactions.ct_sbcs_ext (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_stage_files_import_row_idx
    ON cts_transactions.ct_stage_files (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_stage_locks_import_row_idx
    ON cts_transactions.ct_stage_locks (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_stage_messages_import_row_idx
    ON cts_transactions.ct_stage_messages (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_import_row_idx
    ON cts_transactions.ct_susp_animal_errors (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_import_row_idx
    ON cts_transactions.ct_susp_cm_measure_results (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_import_row_idx
    ON cts_transactions.ct_susp_condition_markers (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_import_row_idx
    ON cts_transactions.ct_susp_movement_errors (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_import_row_idx
    ON cts_transactions.ct_suspended_animals (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_import_row_idx
    ON cts_transactions.ct_suspended_movements (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_valid_applications_import_row_idx
    ON cts_transactions.ct_valid_applications (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_web_users_import_row_idx
    ON cts_transactions.ct_web_users (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_import_row_idx
    ON cts_transactions.ct_wg_autoallocations (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_import_row_idx
    ON cts_transactions.ct_wg_super_assignments (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_import_row_idx
    ON cts_transactions.ct_wg_user_assignments (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_workgroups_import_row_idx
    ON cts_transactions.ct_workgroups (cts_file_import_id,row_number);
CREATE INDEX IF NOT EXISTS ct_addresses_adr_loc_id_idx
    ON cts_transactions.ct_addresses (adr_loc_id);
CREATE INDEX IF NOT EXISTS ct_addresses_adr_par_id_idx
    ON cts_transactions.ct_addresses (adr_par_id);
CREATE INDEX IF NOT EXISTS ct_animal_changes_ach_loc_id_doc_issued_idx
    ON cts_transactions.ct_animal_changes (ach_loc_id_doc_issued);
CREATE INDEX IF NOT EXISTS ct_animal_changes_ach_mov_id_death_cancel_idx
    ON cts_transactions.ct_animal_changes (ach_mov_id_death_cancel);
CREATE INDEX IF NOT EXISTS ct_animal_changes_ach_ran_id_doc_issued_idx
    ON cts_transactions.ct_animal_changes (ach_ran_id_doc_issued);
CREATE INDEX IF NOT EXISTS ct_animal_claims_anc_cls_id_idx
    ON cts_transactions.ct_animal_claims (anc_cls_id);
CREATE INDEX IF NOT EXISTS ct_animal_claims_anc_clt_id_idx
    ON cts_transactions.ct_animal_claims (anc_clt_id);
CREATE INDEX IF NOT EXISTS ct_animal_claims_anc_ran_id_idx
    ON cts_transactions.ct_animal_claims (anc_ran_id);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_ase_acs_id_idx
    ON cts_transactions.ct_animal_corr_summ_errors (ase_acs_id);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_acs_ran_id_idx
    ON cts_transactions.ct_animal_correct_summaries (acs_ran_id);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_acs_rap_id_idx
    ON cts_transactions.ct_animal_correct_summaries (acs_rap_id);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_acs_san_id_idx
    ON cts_transactions.ct_animal_correct_summaries (acs_san_id);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_acs_vap_id_idx
    ON cts_transactions.ct_animal_correct_summaries (acs_vap_id);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_aid_id_original_idx
    ON cts_transactions.ct_animal_identifiers (aid_aid_id_original);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_aid_id_previous_idx
    ON cts_transactions.ct_animal_identifiers (aid_aid_id_previous);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_eid_id_idx
    ON cts_transactions.ct_animal_identifiers (aid_eid_id);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_etg_id_idx
    ON cts_transactions.ct_animal_identifiers (aid_etg_id);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_loc_id_assigned_idx
    ON cts_transactions.ct_animal_identifiers (aid_loc_id_assigned);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_ran_id_idx
    ON cts_transactions.ct_animal_identifiers (aid_ran_id);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_aar_loc_id_idx
    ON cts_transactions.ct_animal_relationships (aar_loc_id);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_aar_ran_id_child_idx
    ON cts_transactions.ct_animal_relationships (aar_ran_id_child);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_aar_ran_id_parent_idx
    ON cts_transactions.ct_animal_relationships (aar_ran_id_parent);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_ast_ran_id_idx
    ON cts_transactions.ct_animal_statuses (ast_ran_id);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_aps_vap_id_idx
    ON cts_transactions.ct_applic_statuses (aps_vap_id);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_cld_cle_id_idx
    ON cts_transactions.ct_cla_extract_detail (cld_cle_id);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_cld_cle_id_idx
    ON cts_transactions.ct_cla_mini_detail (cld_cle_id);
CREATE INDEX IF NOT EXISTS ct_claim_types_clt_sch_id_idx
    ON cts_transactions.ct_claim_types (clt_sch_id);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_cma_cot_id_idx
    ON cts_transactions.ct_cm_authorities (cma_cot_id);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_cmr_com_id_idx
    ON cts_transactions.ct_cm_measures_results (cmr_com_id);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_cvg_cov_id_idx
    ON cts_transactions.ct_cond_variant_groupings (cvg_cov_id);
CREATE INDEX IF NOT EXISTS ct_condition_activities_cac_con_id_idx
    ON cts_transactions.ct_condition_activities (cac_con_id);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_cme_scm_id_idx
    ON cts_transactions.ct_condition_marker_errors (cme_scm_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_com_cac_id_idx
    ON cts_transactions.ct_condition_markers (com_cac_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_com_cma_id_idx
    ON cts_transactions.ct_condition_markers (com_cma_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_com_cov_id_idx
    ON cts_transactions.ct_condition_markers (com_cov_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_com_loc_id_idx
    ON cts_transactions.ct_condition_markers (com_loc_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_com_mov_id_idx
    ON cts_transactions.ct_condition_markers (com_mov_id);
CREATE INDEX IF NOT EXISTS ct_condition_markers_com_ran_id_idx
    ON cts_transactions.ct_condition_markers (com_ran_id);
CREATE INDEX IF NOT EXISTS ct_condition_variants_cov_con_id_idx
    ON cts_transactions.ct_condition_variants (cov_con_id);
CREATE INDEX IF NOT EXISTS ct_conditions_con_cot_id_idx
    ON cts_transactions.ct_conditions (con_cot_id);
CREATE INDEX IF NOT EXISTS ct_conditions_con_pch_id_idx
    ON cts_transactions.ct_conditions (con_pch_id);
CREATE INDEX IF NOT EXISTS ct_countries_cry_cry_id_main_eu_idx
    ON cts_transactions.ct_countries (cry_cry_id_main_eu);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_erf_etr_id_idx
    ON cts_transactions.ct_eartag_reason_flags (erf_etr_id);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_est_erf_id_idx
    ON cts_transactions.ct_eartag_staging (est_erf_id);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_est_loc_id_order_idx
    ON cts_transactions.ct_eartag_staging (est_loc_id_order);
CREATE INDEX IF NOT EXISTS ct_eartag_types_ett_etf_id_idx
    ON cts_transactions.ct_eartag_types (ett_etf_id);
CREATE INDEX IF NOT EXISTS ct_eartags_etg_erf_id_idx
    ON cts_transactions.ct_eartags (etg_erf_id);
CREATE INDEX IF NOT EXISTS ct_eartags_etg_ett_id_idx
    ON cts_transactions.ct_eartags (etg_ett_id);
CREATE INDEX IF NOT EXISTS ct_eartags_etg_loc_id_order_idx
    ON cts_transactions.ct_eartags (etg_loc_id_order);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_eid_isa_id_idx
    ON cts_transactions.ct_electronic_identifiers (eid_isa_id);
CREATE INDEX IF NOT EXISTS ct_issued_documents_ido_loc_id_idx
    ON cts_transactions.ct_issued_documents (ido_loc_id);
CREATE INDEX IF NOT EXISTS ct_issued_documents_ido_ran_id_idx
    ON cts_transactions.ct_issued_documents (ido_ran_id);
CREATE INDEX IF NOT EXISTS ct_label_requests_lar_las_id_idx
    ON cts_transactions.ct_label_requests (lar_las_id);
CREATE INDEX IF NOT EXISTS ct_label_summaries_las_loc_id_identifying_idx
    ON cts_transactions.ct_label_summaries (las_loc_id_identifying);
CREATE INDEX IF NOT EXISTS ct_label_summaries_las_loc_id_labels_idx
    ON cts_transactions.ct_label_summaries (las_loc_id_labels);
CREATE INDEX IF NOT EXISTS ct_letters_let_wgp_id_idx
    ON cts_transactions.ct_letters (let_wgp_id);
CREATE INDEX IF NOT EXISTS ct_letters_let_wgp_id_sent_idx
    ON cts_transactions.ct_letters (let_wgp_id_sent);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_lrc_lrt_id_idx
    ON cts_transactions.ct_loc_type_rel_combs (lrc_lrt_id);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_lrc_lty_id_1_idx
    ON cts_transactions.ct_loc_type_rel_combs (lrc_lty_id_1);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_lrc_lty_id_2_idx
    ON cts_transactions.ct_loc_type_rel_combs (lrc_lty_id_2);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_lid_loc_id_idx
    ON cts_transactions.ct_location_identifiers (lid_loc_id);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_lpr_loc_id_idx
    ON cts_transactions.ct_location_party_rels (lpr_loc_id);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_lpr_lpt_id_idx
    ON cts_transactions.ct_location_party_rels (lpr_lpt_id);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_lpr_par_id_idx
    ON cts_transactions.ct_location_party_rels (lpr_par_id);
CREATE INDEX IF NOT EXISTS ct_location_relationships_llr_loc_id_child_idx
    ON cts_transactions.ct_location_relationships (llr_loc_id_child);
CREATE INDEX IF NOT EXISTS ct_location_relationships_llr_loc_id_parent_idx
    ON cts_transactions.ct_location_relationships (llr_loc_id_parent);
CREATE INDEX IF NOT EXISTS ct_location_relationships_llr_lrt_id_idx
    ON cts_transactions.ct_location_relationships (llr_lrt_id);
CREATE INDEX IF NOT EXISTS ct_location_types_lty_lif_id_idx
    ON cts_transactions.ct_location_types (lty_lif_id);
CREATE INDEX IF NOT EXISTS ct_locations_loc_cty_id_idx
    ON cts_transactions.ct_locations (loc_cty_id);
CREATE INDEX IF NOT EXISTS ct_locations_loc_lty_id_idx
    ON cts_transactions.ct_locations (loc_lty_id);
CREATE INDEX IF NOT EXISTS ct_locations_loc_slt_id_idx
    ON cts_transactions.ct_locations (loc_slt_id);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_lra_com_id_idx
    ON cts_transactions.ct_locrestrictionstoanimals (lra_com_id);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_lra_loc_id_idx
    ON cts_transactions.ct_locrestrictionstoanimals (lra_loc_id);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_lra_ran_id_idx
    ON cts_transactions.ct_locrestrictionstoanimals (lra_ran_id);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_mce_ran_id_idx
    ON cts_transactions.ct_mgt_control_errors (mce_ran_id);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_war_rou_id_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (war_rou_id);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_mse_mcs_id_idx
    ON cts_transactions.ct_movt_corr_summ_errors (mse_mcs_id);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_mcs_mov_id_idx
    ON cts_transactions.ct_movt_correct_summaries (mcs_mov_id);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_mcs_rmo_id_idx
    ON cts_transactions.ct_movt_correct_summaries (mcs_rmo_id);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_mcs_smo_id_idx
    ON cts_transactions.ct_movt_correct_summaries (mcs_smo_id);
CREATE INDEX IF NOT EXISTS ct_param_group_pgp_phd_id_idx
    ON cts_transactions.ct_param_group (pgp_phd_id);
CREATE INDEX IF NOT EXISTS ct_param_value_pvl_phd_id_idx
    ON cts_transactions.ct_param_value (pvl_phd_id);
CREATE INDEX IF NOT EXISTS ct_param_value_group_pvg_pgp_id_idx
    ON cts_transactions.ct_param_value_group (pvg_pgp_id);
CREATE INDEX IF NOT EXISTS ct_param_value_group_pvg_pvl_id_idx
    ON cts_transactions.ct_param_value_group (pvg_pvl_id);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_ppg_loc_id_birth_idx
    ON cts_transactions.ct_ppaf_groupings (ppg_loc_id_birth);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_ppg_loc_id_corres_idx
    ON cts_transactions.ct_ppaf_groupings (ppg_loc_id_corres);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_paf_etg_id_idx
    ON cts_transactions.ct_preprinted_appn_forms (paf_etg_id);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_paf_ppg_id_idx
    ON cts_transactions.ct_preprinted_appn_forms (paf_ppg_id);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_rae_rap_id_idx
    ON cts_transactions.ct_recd_application_errors (rae_rap_id);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_rme_rmo_id_idx
    ON cts_transactions.ct_recd_movement_errors (rme_rmo_id);
CREATE INDEX IF NOT EXISTS ct_received_applications_rap_ran_id_reserved_idx
    ON cts_transactions.ct_received_applications (rap_ran_id_reserved);
CREATE INDEX IF NOT EXISTS ct_received_applications_rap_wgp_id_idx
    ON cts_transactions.ct_received_applications (rap_wgp_id);
CREATE INDEX IF NOT EXISTS ct_registered_animals_ran_brd_id_idx
    ON cts_transactions.ct_registered_animals (ran_brd_id);
CREATE INDEX IF NOT EXISTS ct_registered_animals_ran_cry_id_chr_origin_idx
    ON cts_transactions.ct_registered_animals (ran_cry_id_chr_origin);
CREATE INDEX IF NOT EXISTS ct_registered_animals_ran_loc_id_passport_idx
    ON cts_transactions.ct_registered_animals (ran_loc_id_passport);
CREATE INDEX IF NOT EXISTS ct_registered_animals_ran_vap_id_idx
    ON cts_transactions.ct_registered_animals (ran_vap_id);
CREATE INDEX IF NOT EXISTS ct_registered_movements_mov_cry_id_import_idx
    ON cts_transactions.ct_registered_movements (mov_cry_id_import);
CREATE INDEX IF NOT EXISTS ct_registered_movements_mov_loc_id_idx
    ON cts_transactions.ct_registered_movements (mov_loc_id);
CREATE INDEX IF NOT EXISTS ct_registered_movements_mov_ran_id_idx
    ON cts_transactions.ct_registered_movements (mov_ran_id);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_sae_san_id_idx
    ON cts_transactions.ct_susp_animal_errors (sae_san_id);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_smr_scm_id_idx
    ON cts_transactions.ct_susp_cm_measure_results (smr_scm_id);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_scm_loc_id_idx
    ON cts_transactions.ct_susp_condition_markers (scm_loc_id);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_scm_ran_id_idx
    ON cts_transactions.ct_susp_condition_markers (scm_ran_id);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_sme_smo_id_idx
    ON cts_transactions.ct_susp_movement_errors (sme_smo_id);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_san_loc_id_initial_idx
    ON cts_transactions.ct_suspended_animals (san_loc_id_initial);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_san_loc_id_request_idx
    ON cts_transactions.ct_suspended_animals (san_loc_id_request);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_san_ran_id_idx
    ON cts_transactions.ct_suspended_animals (san_ran_id);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_san_vap_id_idx
    ON cts_transactions.ct_suspended_animals (san_vap_id);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_san_wgp_id_idx
    ON cts_transactions.ct_suspended_animals (san_wgp_id);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_sca_rou_id_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (sca_rou_id);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_swa_rou_id_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (swa_rou_id);
CREATE INDEX IF NOT EXISTS ct_valid_applications_vap_loc_id_requester_idx
    ON cts_transactions.ct_valid_applications (vap_loc_id_requester);
CREATE INDEX IF NOT EXISTS ct_valid_applications_vap_wur_id_idx
    ON cts_transactions.ct_valid_applications (vap_wur_id);
CREATE INDEX IF NOT EXISTS ct_web_users_wur_lpr_id_keeper_idx
    ON cts_transactions.ct_web_users (wur_lpr_id_keeper);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_wga_rou_id_idx
    ON cts_transactions.ct_wg_autoallocations (wga_rou_id);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_wga_wgp_id_idx
    ON cts_transactions.ct_wg_autoallocations (wga_wgp_id);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_wsa_rou_id_idx
    ON cts_transactions.ct_wg_super_assignments (wsa_rou_id);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_wsa_wgp_id_assigned_idx
    ON cts_transactions.ct_wg_super_assignments (wsa_wgp_id_assigned);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_wsa_wgp_id_current_idx
    ON cts_transactions.ct_wg_super_assignments (wsa_wgp_id_current);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_wua_cus_id_idx
    ON cts_transactions.ct_wg_user_assignments (wua_cus_id);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_wua_wgp_id_idx
    ON cts_transactions.ct_wg_user_assignments (wua_wgp_id);
CREATE INDEX IF NOT EXISTS ct_addresses_record_type_idx
    ON cts_transactions.ct_addresses (record_type);
CREATE INDEX IF NOT EXISTS ct_addresses_record_count_idx
    ON cts_transactions.ct_addresses (record_count);
CREATE INDEX IF NOT EXISTS ct_addresses_imported_date_idx
    ON cts_transactions.ct_addresses (imported_date);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_record_type_idx
    ON cts_transactions.ct_alloc_routines (record_type);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_record_count_idx
    ON cts_transactions.ct_alloc_routines (record_count);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_imported_date_idx
    ON cts_transactions.ct_alloc_routines (imported_date);
CREATE INDEX IF NOT EXISTS ct_animal_changes_record_type_idx
    ON cts_transactions.ct_animal_changes (record_type);
CREATE INDEX IF NOT EXISTS ct_animal_changes_record_count_idx
    ON cts_transactions.ct_animal_changes (record_count);
CREATE INDEX IF NOT EXISTS ct_animal_changes_imported_date_idx
    ON cts_transactions.ct_animal_changes (imported_date);
CREATE INDEX IF NOT EXISTS ct_animal_claims_record_type_idx
    ON cts_transactions.ct_animal_claims (record_type);
CREATE INDEX IF NOT EXISTS ct_animal_claims_record_count_idx
    ON cts_transactions.ct_animal_claims (record_count);
CREATE INDEX IF NOT EXISTS ct_animal_claims_imported_date_idx
    ON cts_transactions.ct_animal_claims (imported_date);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_record_type_idx
    ON cts_transactions.ct_animal_corr_summ_errors (record_type);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_record_count_idx
    ON cts_transactions.ct_animal_corr_summ_errors (record_count);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_imported_date_idx
    ON cts_transactions.ct_animal_corr_summ_errors (imported_date);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_record_type_idx
    ON cts_transactions.ct_animal_correct_summaries (record_type);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_record_count_idx
    ON cts_transactions.ct_animal_correct_summaries (record_count);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_imported_date_idx
    ON cts_transactions.ct_animal_correct_summaries (imported_date);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_record_type_idx
    ON cts_transactions.ct_animal_identifiers (record_type);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_record_count_idx
    ON cts_transactions.ct_animal_identifiers (record_count);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_imported_date_idx
    ON cts_transactions.ct_animal_identifiers (imported_date);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_record_type_idx
    ON cts_transactions.ct_animal_relationships (record_type);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_record_count_idx
    ON cts_transactions.ct_animal_relationships (record_count);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_imported_date_idx
    ON cts_transactions.ct_animal_relationships (imported_date);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_record_type_idx
    ON cts_transactions.ct_animal_statuses (record_type);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_record_count_idx
    ON cts_transactions.ct_animal_statuses (record_count);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_imported_date_idx
    ON cts_transactions.ct_animal_statuses (imported_date);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_record_type_idx
    ON cts_transactions.ct_applic_statuses (record_type);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_record_count_idx
    ON cts_transactions.ct_applic_statuses (record_count);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_imported_date_idx
    ON cts_transactions.ct_applic_statuses (imported_date);
CREATE INDEX IF NOT EXISTS ct_application_late_days_record_type_idx
    ON cts_transactions.ct_application_late_days (record_type);
CREATE INDEX IF NOT EXISTS ct_application_late_days_record_count_idx
    ON cts_transactions.ct_application_late_days (record_count);
CREATE INDEX IF NOT EXISTS ct_application_late_days_imported_date_idx
    ON cts_transactions.ct_application_late_days (imported_date);
CREATE INDEX IF NOT EXISTS ct_batch_retention_conf_record_type_idx
    ON cts_transactions.ct_batch_retention_conf (record_type);
CREATE INDEX IF NOT EXISTS ct_batch_retention_conf_record_count_idx
    ON cts_transactions.ct_batch_retention_conf (record_count);
CREATE INDEX IF NOT EXISTS ct_batch_retention_conf_imported_date_idx
    ON cts_transactions.ct_batch_retention_conf (imported_date);
CREATE INDEX IF NOT EXISTS ct_breeds_record_type_idx
    ON cts_transactions.ct_breeds (record_type);
CREATE INDEX IF NOT EXISTS ct_breeds_record_count_idx
    ON cts_transactions.ct_breeds (record_count);
CREATE INDEX IF NOT EXISTS ct_breeds_imported_date_idx
    ON cts_transactions.ct_breeds (imported_date);
CREATE INDEX IF NOT EXISTS ct_cla_extract_record_type_idx
    ON cts_transactions.ct_cla_extract (record_type);
CREATE INDEX IF NOT EXISTS ct_cla_extract_record_count_idx
    ON cts_transactions.ct_cla_extract (record_count);
CREATE INDEX IF NOT EXISTS ct_cla_extract_imported_date_idx
    ON cts_transactions.ct_cla_extract (imported_date);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_record_type_idx
    ON cts_transactions.ct_cla_extract_detail (record_type);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_record_count_idx
    ON cts_transactions.ct_cla_extract_detail (record_count);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_imported_date_idx
    ON cts_transactions.ct_cla_extract_detail (imported_date);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_record_type_idx
    ON cts_transactions.ct_cla_extract_dm (record_type);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_record_count_idx
    ON cts_transactions.ct_cla_extract_dm (record_count);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_imported_date_idx
    ON cts_transactions.ct_cla_extract_dm (imported_date);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_record_type_idx
    ON cts_transactions.ct_cla_mini_detail (record_type);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_record_count_idx
    ON cts_transactions.ct_cla_mini_detail (record_count);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_imported_date_idx
    ON cts_transactions.ct_cla_mini_detail (imported_date);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_record_type_idx
    ON cts_transactions.ct_cla_mini_extract (record_type);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_record_count_idx
    ON cts_transactions.ct_cla_mini_extract (record_count);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_imported_date_idx
    ON cts_transactions.ct_cla_mini_extract (imported_date);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_record_type_idx
    ON cts_transactions.ct_claim_statuses (record_type);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_record_count_idx
    ON cts_transactions.ct_claim_statuses (record_count);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_imported_date_idx
    ON cts_transactions.ct_claim_statuses (imported_date);
CREATE INDEX IF NOT EXISTS ct_claim_types_record_type_idx
    ON cts_transactions.ct_claim_types (record_type);
CREATE INDEX IF NOT EXISTS ct_claim_types_record_count_idx
    ON cts_transactions.ct_claim_types (record_count);
CREATE INDEX IF NOT EXISTS ct_claim_types_imported_date_idx
    ON cts_transactions.ct_claim_types (imported_date);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_record_type_idx
    ON cts_transactions.ct_cm_authorities (record_type);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_record_count_idx
    ON cts_transactions.ct_cm_authorities (record_count);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_imported_date_idx
    ON cts_transactions.ct_cm_authorities (imported_date);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_record_type_idx
    ON cts_transactions.ct_cm_measures_results (record_type);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_record_count_idx
    ON cts_transactions.ct_cm_measures_results (record_count);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_imported_date_idx
    ON cts_transactions.ct_cm_measures_results (imported_date);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_record_type_idx
    ON cts_transactions.ct_comms_addresses (record_type);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_record_count_idx
    ON cts_transactions.ct_comms_addresses (record_count);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_imported_date_idx
    ON cts_transactions.ct_comms_addresses (imported_date);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_record_type_idx
    ON cts_transactions.ct_cond_variant_groupings (record_type);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_record_count_idx
    ON cts_transactions.ct_cond_variant_groupings (record_count);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_imported_date_idx
    ON cts_transactions.ct_cond_variant_groupings (imported_date);
CREATE INDEX IF NOT EXISTS ct_condition_activities_record_type_idx
    ON cts_transactions.ct_condition_activities (record_type);
CREATE INDEX IF NOT EXISTS ct_condition_activities_record_count_idx
    ON cts_transactions.ct_condition_activities (record_count);
CREATE INDEX IF NOT EXISTS ct_condition_activities_imported_date_idx
    ON cts_transactions.ct_condition_activities (imported_date);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_record_type_idx
    ON cts_transactions.ct_condition_marker_errors (record_type);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_record_count_idx
    ON cts_transactions.ct_condition_marker_errors (record_count);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_imported_date_idx
    ON cts_transactions.ct_condition_marker_errors (imported_date);
CREATE INDEX IF NOT EXISTS ct_condition_markers_record_type_idx
    ON cts_transactions.ct_condition_markers (record_type);
CREATE INDEX IF NOT EXISTS ct_condition_markers_record_count_idx
    ON cts_transactions.ct_condition_markers (record_count);
CREATE INDEX IF NOT EXISTS ct_condition_markers_imported_date_idx
    ON cts_transactions.ct_condition_markers (imported_date);
CREATE INDEX IF NOT EXISTS ct_condition_types_record_type_idx
    ON cts_transactions.ct_condition_types (record_type);
CREATE INDEX IF NOT EXISTS ct_condition_types_record_count_idx
    ON cts_transactions.ct_condition_types (record_count);
CREATE INDEX IF NOT EXISTS ct_condition_types_imported_date_idx
    ON cts_transactions.ct_condition_types (imported_date);
CREATE INDEX IF NOT EXISTS ct_condition_variants_record_type_idx
    ON cts_transactions.ct_condition_variants (record_type);
CREATE INDEX IF NOT EXISTS ct_condition_variants_record_count_idx
    ON cts_transactions.ct_condition_variants (record_count);
CREATE INDEX IF NOT EXISTS ct_condition_variants_imported_date_idx
    ON cts_transactions.ct_condition_variants (imported_date);
CREATE INDEX IF NOT EXISTS ct_conditions_record_type_idx
    ON cts_transactions.ct_conditions (record_type);
CREATE INDEX IF NOT EXISTS ct_conditions_record_count_idx
    ON cts_transactions.ct_conditions (record_count);
CREATE INDEX IF NOT EXISTS ct_conditions_imported_date_idx
    ON cts_transactions.ct_conditions (imported_date);
CREATE INDEX IF NOT EXISTS ct_counties_record_type_idx
    ON cts_transactions.ct_counties (record_type);
CREATE INDEX IF NOT EXISTS ct_counties_record_count_idx
    ON cts_transactions.ct_counties (record_count);
CREATE INDEX IF NOT EXISTS ct_counties_imported_date_idx
    ON cts_transactions.ct_counties (imported_date);
CREATE INDEX IF NOT EXISTS ct_counties_migration_record_type_idx
    ON cts_transactions.ct_counties_migration (record_type);
CREATE INDEX IF NOT EXISTS ct_counties_migration_record_count_idx
    ON cts_transactions.ct_counties_migration (record_count);
CREATE INDEX IF NOT EXISTS ct_counties_migration_imported_date_idx
    ON cts_transactions.ct_counties_migration (imported_date);
CREATE INDEX IF NOT EXISTS ct_countries_record_type_idx
    ON cts_transactions.ct_countries (record_type);
CREATE INDEX IF NOT EXISTS ct_countries_record_count_idx
    ON cts_transactions.ct_countries (record_count);
CREATE INDEX IF NOT EXISTS ct_countries_imported_date_idx
    ON cts_transactions.ct_countries (imported_date);
CREATE INDEX IF NOT EXISTS ct_cps167_report_record_type_idx
    ON cts_transactions.ct_cps167_report (record_type);
CREATE INDEX IF NOT EXISTS ct_cps167_report_record_count_idx
    ON cts_transactions.ct_cps167_report (record_count);
CREATE INDEX IF NOT EXISTS ct_cps167_report_imported_date_idx
    ON cts_transactions.ct_cps167_report (imported_date);
CREATE INDEX IF NOT EXISTS ct_cts164_handshake_file_keys_record_type_idx
    ON cts_transactions.ct_cts164_handshake_file_keys (record_type);
CREATE INDEX IF NOT EXISTS ct_cts164_handshake_file_keys_record_count_idx
    ON cts_transactions.ct_cts164_handshake_file_keys (record_count);
CREATE INDEX IF NOT EXISTS ct_cts164_handshake_file_keys_imported_date_idx
    ON cts_transactions.ct_cts164_handshake_file_keys (imported_date);
CREATE INDEX IF NOT EXISTS ct_cts_users_record_type_idx
    ON cts_transactions.ct_cts_users (record_type);
CREATE INDEX IF NOT EXISTS ct_cts_users_record_count_idx
    ON cts_transactions.ct_cts_users (record_count);
CREATE INDEX IF NOT EXISTS ct_cts_users_imported_date_idx
    ON cts_transactions.ct_cts_users (imported_date);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_record_type_idx
    ON cts_transactions.ct_eartag_formats (record_type);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_record_count_idx
    ON cts_transactions.ct_eartag_formats (record_count);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_imported_date_idx
    ON cts_transactions.ct_eartag_formats (imported_date);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_record_type_idx
    ON cts_transactions.ct_eartag_reason_flags (record_type);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_record_count_idx
    ON cts_transactions.ct_eartag_reason_flags (record_count);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_imported_date_idx
    ON cts_transactions.ct_eartag_reason_flags (imported_date);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_record_type_idx
    ON cts_transactions.ct_eartag_reasons (record_type);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_record_count_idx
    ON cts_transactions.ct_eartag_reasons (record_count);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_imported_date_idx
    ON cts_transactions.ct_eartag_reasons (imported_date);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_record_type_idx
    ON cts_transactions.ct_eartag_staging (record_type);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_record_count_idx
    ON cts_transactions.ct_eartag_staging (record_count);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_imported_date_idx
    ON cts_transactions.ct_eartag_staging (imported_date);
CREATE INDEX IF NOT EXISTS ct_eartag_types_record_type_idx
    ON cts_transactions.ct_eartag_types (record_type);
CREATE INDEX IF NOT EXISTS ct_eartag_types_record_count_idx
    ON cts_transactions.ct_eartag_types (record_count);
CREATE INDEX IF NOT EXISTS ct_eartag_types_imported_date_idx
    ON cts_transactions.ct_eartag_types (imported_date);
CREATE INDEX IF NOT EXISTS ct_eartags_record_type_idx
    ON cts_transactions.ct_eartags (record_type);
CREATE INDEX IF NOT EXISTS ct_eartags_record_count_idx
    ON cts_transactions.ct_eartags (record_count);
CREATE INDEX IF NOT EXISTS ct_eartags_imported_date_idx
    ON cts_transactions.ct_eartags (imported_date);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_record_type_idx
    ON cts_transactions.ct_electronic_identifiers (record_type);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_record_count_idx
    ON cts_transactions.ct_electronic_identifiers (record_count);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_imported_date_idx
    ON cts_transactions.ct_electronic_identifiers (imported_date);
CREATE INDEX IF NOT EXISTS ct_email_log_record_type_idx
    ON cts_transactions.ct_email_log (record_type);
CREATE INDEX IF NOT EXISTS ct_email_log_record_count_idx
    ON cts_transactions.ct_email_log (record_count);
CREATE INDEX IF NOT EXISTS ct_email_log_imported_date_idx
    ON cts_transactions.ct_email_log (imported_date);
CREATE INDEX IF NOT EXISTS ct_ereport_files_record_type_idx
    ON cts_transactions.ct_ereport_files (record_type);
CREATE INDEX IF NOT EXISTS ct_ereport_files_record_count_idx
    ON cts_transactions.ct_ereport_files (record_count);
CREATE INDEX IF NOT EXISTS ct_ereport_files_imported_date_idx
    ON cts_transactions.ct_ereport_files (imported_date);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_record_type_idx
    ON cts_transactions.ct_ereport_load_messages (record_type);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_record_count_idx
    ON cts_transactions.ct_ereport_load_messages (record_count);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_imported_date_idx
    ON cts_transactions.ct_ereport_load_messages (imported_date);
CREATE INDEX IF NOT EXISTS ct_ereport_locks_record_type_idx
    ON cts_transactions.ct_ereport_locks (record_type);
CREATE INDEX IF NOT EXISTS ct_ereport_locks_record_count_idx
    ON cts_transactions.ct_ereport_locks (record_count);
CREATE INDEX IF NOT EXISTS ct_ereport_locks_imported_date_idx
    ON cts_transactions.ct_ereport_locks (imported_date);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_record_type_idx
    ON cts_transactions.ct_ereport_process_messages (record_type);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_record_count_idx
    ON cts_transactions.ct_ereport_process_messages (record_count);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_imported_date_idx
    ON cts_transactions.ct_ereport_process_messages (imported_date);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_record_type_idx
    ON cts_transactions.ct_ext_cetd_eartag (record_type);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_record_count_idx
    ON cts_transactions.ct_ext_cetd_eartag (record_count);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_imported_date_idx
    ON cts_transactions.ct_ext_cetd_eartag (imported_date);
CREATE INDEX IF NOT EXISTS ct_ext_ni_district_record_type_idx
    ON cts_transactions.ct_ext_ni_district (record_type);
CREATE INDEX IF NOT EXISTS ct_ext_ni_district_record_count_idx
    ON cts_transactions.ct_ext_ni_district (record_count);
CREATE INDEX IF NOT EXISTS ct_ext_ni_district_imported_date_idx
    ON cts_transactions.ct_ext_ni_district (imported_date);
CREATE INDEX IF NOT EXISTS ct_ext_special_herd_record_type_idx
    ON cts_transactions.ct_ext_special_herd (record_type);
CREATE INDEX IF NOT EXISTS ct_ext_special_herd_record_count_idx
    ON cts_transactions.ct_ext_special_herd (record_count);
CREATE INDEX IF NOT EXISTS ct_ext_special_herd_imported_date_idx
    ON cts_transactions.ct_ext_special_herd (imported_date);
CREATE INDEX IF NOT EXISTS ct_file_layouts_record_type_idx
    ON cts_transactions.ct_file_layouts (record_type);
CREATE INDEX IF NOT EXISTS ct_file_layouts_record_count_idx
    ON cts_transactions.ct_file_layouts (record_count);
CREATE INDEX IF NOT EXISTS ct_file_layouts_imported_date_idx
    ON cts_transactions.ct_file_layouts (imported_date);
CREATE INDEX IF NOT EXISTS ct_hsf_sequences_record_type_idx
    ON cts_transactions.ct_hsf_sequences (record_type);
CREATE INDEX IF NOT EXISTS ct_hsf_sequences_record_count_idx
    ON cts_transactions.ct_hsf_sequences (record_count);
CREATE INDEX IF NOT EXISTS ct_hsf_sequences_imported_date_idx
    ON cts_transactions.ct_hsf_sequences (imported_date);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_record_type_idx
    ON cts_transactions.ct_insert_update_log (record_type);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_record_count_idx
    ON cts_transactions.ct_insert_update_log (record_count);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_imported_date_idx
    ON cts_transactions.ct_insert_update_log (imported_date);
CREATE INDEX IF NOT EXISTS ct_issued_documents_record_type_idx
    ON cts_transactions.ct_issued_documents (record_type);
CREATE INDEX IF NOT EXISTS ct_issued_documents_record_count_idx
    ON cts_transactions.ct_issued_documents (record_count);
CREATE INDEX IF NOT EXISTS ct_issued_documents_imported_date_idx
    ON cts_transactions.ct_issued_documents (imported_date);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_record_type_idx
    ON cts_transactions.ct_issuing_authorities (record_type);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_record_count_idx
    ON cts_transactions.ct_issuing_authorities (record_count);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_imported_date_idx
    ON cts_transactions.ct_issuing_authorities (imported_date);
CREATE INDEX IF NOT EXISTS ct_label_requests_record_type_idx
    ON cts_transactions.ct_label_requests (record_type);
CREATE INDEX IF NOT EXISTS ct_label_requests_record_count_idx
    ON cts_transactions.ct_label_requests (record_count);
CREATE INDEX IF NOT EXISTS ct_label_requests_imported_date_idx
    ON cts_transactions.ct_label_requests (imported_date);
CREATE INDEX IF NOT EXISTS ct_label_summaries_record_type_idx
    ON cts_transactions.ct_label_summaries (record_type);
CREATE INDEX IF NOT EXISTS ct_label_summaries_record_count_idx
    ON cts_transactions.ct_label_summaries (record_count);
CREATE INDEX IF NOT EXISTS ct_label_summaries_imported_date_idx
    ON cts_transactions.ct_label_summaries (imported_date);
CREATE INDEX IF NOT EXISTS ct_late_days_record_type_idx
    ON cts_transactions.ct_late_days (record_type);
CREATE INDEX IF NOT EXISTS ct_late_days_record_count_idx
    ON cts_transactions.ct_late_days (record_count);
CREATE INDEX IF NOT EXISTS ct_late_days_imported_date_idx
    ON cts_transactions.ct_late_days (imported_date);
CREATE INDEX IF NOT EXISTS ct_letters_record_type_idx
    ON cts_transactions.ct_letters (record_type);
CREATE INDEX IF NOT EXISTS ct_letters_record_count_idx
    ON cts_transactions.ct_letters (record_count);
CREATE INDEX IF NOT EXISTS ct_letters_imported_date_idx
    ON cts_transactions.ct_letters (imported_date);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_record_type_idx
    ON cts_transactions.ct_loc_type_rel_combs (record_type);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_record_count_idx
    ON cts_transactions.ct_loc_type_rel_combs (record_count);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_imported_date_idx
    ON cts_transactions.ct_loc_type_rel_combs (imported_date);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_record_type_idx
    ON cts_transactions.ct_location_id_formats (record_type);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_record_count_idx
    ON cts_transactions.ct_location_id_formats (record_count);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_imported_date_idx
    ON cts_transactions.ct_location_id_formats (imported_date);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_record_type_idx
    ON cts_transactions.ct_location_identifiers (record_type);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_record_count_idx
    ON cts_transactions.ct_location_identifiers (record_count);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_imported_date_idx
    ON cts_transactions.ct_location_identifiers (imported_date);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_record_type_idx
    ON cts_transactions.ct_location_party_rel_types (record_type);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_record_count_idx
    ON cts_transactions.ct_location_party_rel_types (record_count);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_imported_date_idx
    ON cts_transactions.ct_location_party_rel_types (imported_date);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_record_type_idx
    ON cts_transactions.ct_location_party_rels (record_type);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_record_count_idx
    ON cts_transactions.ct_location_party_rels (record_count);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_imported_date_idx
    ON cts_transactions.ct_location_party_rels (imported_date);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_record_type_idx
    ON cts_transactions.ct_location_rel_types (record_type);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_record_count_idx
    ON cts_transactions.ct_location_rel_types (record_count);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_imported_date_idx
    ON cts_transactions.ct_location_rel_types (imported_date);
CREATE INDEX IF NOT EXISTS ct_location_relationships_record_type_idx
    ON cts_transactions.ct_location_relationships (record_type);
CREATE INDEX IF NOT EXISTS ct_location_relationships_record_count_idx
    ON cts_transactions.ct_location_relationships (record_count);
CREATE INDEX IF NOT EXISTS ct_location_relationships_imported_date_idx
    ON cts_transactions.ct_location_relationships (imported_date);
CREATE INDEX IF NOT EXISTS ct_location_types_record_type_idx
    ON cts_transactions.ct_location_types (record_type);
CREATE INDEX IF NOT EXISTS ct_location_types_record_count_idx
    ON cts_transactions.ct_location_types (record_count);
CREATE INDEX IF NOT EXISTS ct_location_types_imported_date_idx
    ON cts_transactions.ct_location_types (imported_date);
CREATE INDEX IF NOT EXISTS ct_locations_record_type_idx
    ON cts_transactions.ct_locations (record_type);
CREATE INDEX IF NOT EXISTS ct_locations_record_count_idx
    ON cts_transactions.ct_locations (record_count);
CREATE INDEX IF NOT EXISTS ct_locations_imported_date_idx
    ON cts_transactions.ct_locations (imported_date);
CREATE INDEX IF NOT EXISTS ct_locations_faker_record_type_idx
    ON cts_transactions.ct_locations_faker (record_type);
CREATE INDEX IF NOT EXISTS ct_locations_faker_record_count_idx
    ON cts_transactions.ct_locations_faker (record_count);
CREATE INDEX IF NOT EXISTS ct_locations_faker_imported_date_idx
    ON cts_transactions.ct_locations_faker (imported_date);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_record_type_idx
    ON cts_transactions.ct_locrestrictionstoanimals (record_type);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_record_count_idx
    ON cts_transactions.ct_locrestrictionstoanimals (record_count);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_imported_date_idx
    ON cts_transactions.ct_locrestrictionstoanimals (imported_date);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_record_type_idx
    ON cts_transactions.ct_mgt_control_errors (record_type);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_record_count_idx
    ON cts_transactions.ct_mgt_control_errors (record_count);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_imported_date_idx
    ON cts_transactions.ct_mgt_control_errors (imported_date);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_record_type_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (record_type);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_record_count_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (record_count);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_imported_date_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (imported_date);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_record_type_idx
    ON cts_transactions.ct_mhs_to_cph (record_type);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_record_count_idx
    ON cts_transactions.ct_mhs_to_cph (record_count);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_imported_date_idx
    ON cts_transactions.ct_mhs_to_cph (imported_date);
CREATE INDEX IF NOT EXISTS ct_mov_hst_record_type_idx
    ON cts_transactions.ct_mov_hst (record_type);
CREATE INDEX IF NOT EXISTS ct_mov_hst_record_count_idx
    ON cts_transactions.ct_mov_hst (record_count);
CREATE INDEX IF NOT EXISTS ct_mov_hst_imported_date_idx
    ON cts_transactions.ct_mov_hst (imported_date);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_record_type_idx
    ON cts_transactions.ct_movt_corr_summ_errors (record_type);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_record_count_idx
    ON cts_transactions.ct_movt_corr_summ_errors (record_count);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_imported_date_idx
    ON cts_transactions.ct_movt_corr_summ_errors (imported_date);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_record_type_idx
    ON cts_transactions.ct_movt_correct_summaries (record_type);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_record_count_idx
    ON cts_transactions.ct_movt_correct_summaries (record_count);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_imported_date_idx
    ON cts_transactions.ct_movt_correct_summaries (imported_date);
CREATE INDEX IF NOT EXISTS ct_msgtxt_record_type_idx
    ON cts_transactions.ct_msgtxt (record_type);
CREATE INDEX IF NOT EXISTS ct_msgtxt_record_count_idx
    ON cts_transactions.ct_msgtxt (record_count);
CREATE INDEX IF NOT EXISTS ct_msgtxt_imported_date_idx
    ON cts_transactions.ct_msgtxt (imported_date);
CREATE INDEX IF NOT EXISTS ct_non_working_days_record_type_idx
    ON cts_transactions.ct_non_working_days (record_type);
CREATE INDEX IF NOT EXISTS ct_non_working_days_record_count_idx
    ON cts_transactions.ct_non_working_days (record_count);
CREATE INDEX IF NOT EXISTS ct_non_working_days_imported_date_idx
    ON cts_transactions.ct_non_working_days (imported_date);
CREATE INDEX IF NOT EXISTS ct_param_group_record_type_idx
    ON cts_transactions.ct_param_group (record_type);
CREATE INDEX IF NOT EXISTS ct_param_group_record_count_idx
    ON cts_transactions.ct_param_group (record_count);
CREATE INDEX IF NOT EXISTS ct_param_group_imported_date_idx
    ON cts_transactions.ct_param_group (imported_date);
CREATE INDEX IF NOT EXISTS ct_param_header_record_type_idx
    ON cts_transactions.ct_param_header (record_type);
CREATE INDEX IF NOT EXISTS ct_param_header_record_count_idx
    ON cts_transactions.ct_param_header (record_count);
CREATE INDEX IF NOT EXISTS ct_param_header_imported_date_idx
    ON cts_transactions.ct_param_header (imported_date);
CREATE INDEX IF NOT EXISTS ct_param_value_record_type_idx
    ON cts_transactions.ct_param_value (record_type);
CREATE INDEX IF NOT EXISTS ct_param_value_record_count_idx
    ON cts_transactions.ct_param_value (record_count);
CREATE INDEX IF NOT EXISTS ct_param_value_imported_date_idx
    ON cts_transactions.ct_param_value (imported_date);
CREATE INDEX IF NOT EXISTS ct_param_value_group_record_type_idx
    ON cts_transactions.ct_param_value_group (record_type);
CREATE INDEX IF NOT EXISTS ct_param_value_group_record_count_idx
    ON cts_transactions.ct_param_value_group (record_count);
CREATE INDEX IF NOT EXISTS ct_param_value_group_imported_date_idx
    ON cts_transactions.ct_param_value_group (imported_date);
CREATE INDEX IF NOT EXISTS ct_parties_record_type_idx
    ON cts_transactions.ct_parties (record_type);
CREATE INDEX IF NOT EXISTS ct_parties_record_count_idx
    ON cts_transactions.ct_parties (record_count);
CREATE INDEX IF NOT EXISTS ct_parties_imported_date_idx
    ON cts_transactions.ct_parties (imported_date);
CREATE INDEX IF NOT EXISTS ct_parties_faker_record_type_idx
    ON cts_transactions.ct_parties_faker (record_type);
CREATE INDEX IF NOT EXISTS ct_parties_faker_record_count_idx
    ON cts_transactions.ct_parties_faker (record_count);
CREATE INDEX IF NOT EXISTS ct_parties_faker_imported_date_idx
    ON cts_transactions.ct_parties_faker (imported_date);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_record_type_idx
    ON cts_transactions.ct_ppaf_groupings (record_type);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_record_count_idx
    ON cts_transactions.ct_ppaf_groupings (record_count);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_imported_date_idx
    ON cts_transactions.ct_ppaf_groupings (imported_date);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_record_type_idx
    ON cts_transactions.ct_preprinted_appn_forms (record_type);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_record_count_idx
    ON cts_transactions.ct_preprinted_appn_forms (record_count);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_imported_date_idx
    ON cts_transactions.ct_preprinted_appn_forms (imported_date);
CREATE INDEX IF NOT EXISTS ct_probity_checks_record_type_idx
    ON cts_transactions.ct_probity_checks (record_type);
CREATE INDEX IF NOT EXISTS ct_probity_checks_record_count_idx
    ON cts_transactions.ct_probity_checks (record_count);
CREATE INDEX IF NOT EXISTS ct_probity_checks_imported_date_idx
    ON cts_transactions.ct_probity_checks (imported_date);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_record_type_idx
    ON cts_transactions.ct_ps9999_ahdb_data (record_type);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_record_count_idx
    ON cts_transactions.ct_ps9999_ahdb_data (record_count);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_imported_date_idx
    ON cts_transactions.ct_ps9999_ahdb_data (imported_date);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_record_type_idx
    ON cts_transactions.ct_ps9999_ahdb_mov_history (record_type);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_record_count_idx
    ON cts_transactions.ct_ps9999_ahdb_mov_history (record_count);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_imported_date_idx
    ON cts_transactions.ct_ps9999_ahdb_mov_history (imported_date);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_record_type_idx
    ON cts_transactions.ct_recd_application_errors (record_type);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_record_count_idx
    ON cts_transactions.ct_recd_application_errors (record_count);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_imported_date_idx
    ON cts_transactions.ct_recd_application_errors (imported_date);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_record_type_idx
    ON cts_transactions.ct_recd_movement_errors (record_type);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_record_count_idx
    ON cts_transactions.ct_recd_movement_errors (record_count);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_imported_date_idx
    ON cts_transactions.ct_recd_movement_errors (imported_date);
CREATE INDEX IF NOT EXISTS ct_received_applications_record_type_idx
    ON cts_transactions.ct_received_applications (record_type);
CREATE INDEX IF NOT EXISTS ct_received_applications_record_count_idx
    ON cts_transactions.ct_received_applications (record_count);
CREATE INDEX IF NOT EXISTS ct_received_applications_imported_date_idx
    ON cts_transactions.ct_received_applications (imported_date);
CREATE INDEX IF NOT EXISTS ct_received_movements_record_type_idx
    ON cts_transactions.ct_received_movements (record_type);
CREATE INDEX IF NOT EXISTS ct_received_movements_record_count_idx
    ON cts_transactions.ct_received_movements (record_count);
CREATE INDEX IF NOT EXISTS ct_received_movements_imported_date_idx
    ON cts_transactions.ct_received_movements (imported_date);
CREATE INDEX IF NOT EXISTS ct_registered_animals_record_type_idx
    ON cts_transactions.ct_registered_animals (record_type);
CREATE INDEX IF NOT EXISTS ct_registered_animals_record_count_idx
    ON cts_transactions.ct_registered_animals (record_count);
CREATE INDEX IF NOT EXISTS ct_registered_animals_imported_date_idx
    ON cts_transactions.ct_registered_animals (imported_date);
CREATE INDEX IF NOT EXISTS ct_registered_movements_record_type_idx
    ON cts_transactions.ct_registered_movements (record_type);
CREATE INDEX IF NOT EXISTS ct_registered_movements_record_count_idx
    ON cts_transactions.ct_registered_movements (record_count);
CREATE INDEX IF NOT EXISTS ct_registered_movements_imported_date_idx
    ON cts_transactions.ct_registered_movements (imported_date);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_record_type_idx
    ON cts_transactions.ct_reset_to_extract (record_type);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_record_count_idx
    ON cts_transactions.ct_reset_to_extract (record_count);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_imported_date_idx
    ON cts_transactions.ct_reset_to_extract (imported_date);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_record_type_idx
    ON cts_transactions.ct_sbcs_ext (record_type);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_record_count_idx
    ON cts_transactions.ct_sbcs_ext (record_count);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_imported_date_idx
    ON cts_transactions.ct_sbcs_ext (imported_date);
CREATE INDEX IF NOT EXISTS ct_schemes_record_type_idx
    ON cts_transactions.ct_schemes (record_type);
CREATE INDEX IF NOT EXISTS ct_schemes_record_count_idx
    ON cts_transactions.ct_schemes (record_count);
CREATE INDEX IF NOT EXISTS ct_schemes_imported_date_idx
    ON cts_transactions.ct_schemes (imported_date);
CREATE INDEX IF NOT EXISTS ct_stage_files_record_type_idx
    ON cts_transactions.ct_stage_files (record_type);
CREATE INDEX IF NOT EXISTS ct_stage_files_record_count_idx
    ON cts_transactions.ct_stage_files (record_count);
CREATE INDEX IF NOT EXISTS ct_stage_files_imported_date_idx
    ON cts_transactions.ct_stage_files (imported_date);
CREATE INDEX IF NOT EXISTS ct_stage_locks_record_type_idx
    ON cts_transactions.ct_stage_locks (record_type);
CREATE INDEX IF NOT EXISTS ct_stage_locks_record_count_idx
    ON cts_transactions.ct_stage_locks (record_count);
CREATE INDEX IF NOT EXISTS ct_stage_locks_imported_date_idx
    ON cts_transactions.ct_stage_locks (imported_date);
CREATE INDEX IF NOT EXISTS ct_stage_messages_record_type_idx
    ON cts_transactions.ct_stage_messages (record_type);
CREATE INDEX IF NOT EXISTS ct_stage_messages_record_count_idx
    ON cts_transactions.ct_stage_messages (record_count);
CREATE INDEX IF NOT EXISTS ct_stage_messages_imported_date_idx
    ON cts_transactions.ct_stage_messages (imported_date);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_record_type_idx
    ON cts_transactions.ct_sublocation_types (record_type);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_record_count_idx
    ON cts_transactions.ct_sublocation_types (record_count);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_imported_date_idx
    ON cts_transactions.ct_sublocation_types (imported_date);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_record_type_idx
    ON cts_transactions.ct_susp_animal_errors (record_type);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_record_count_idx
    ON cts_transactions.ct_susp_animal_errors (record_count);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_imported_date_idx
    ON cts_transactions.ct_susp_animal_errors (imported_date);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_record_type_idx
    ON cts_transactions.ct_susp_cm_measure_results (record_type);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_record_count_idx
    ON cts_transactions.ct_susp_cm_measure_results (record_count);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_imported_date_idx
    ON cts_transactions.ct_susp_cm_measure_results (imported_date);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_record_type_idx
    ON cts_transactions.ct_susp_condition_markers (record_type);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_record_count_idx
    ON cts_transactions.ct_susp_condition_markers (record_count);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_imported_date_idx
    ON cts_transactions.ct_susp_condition_markers (imported_date);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_record_type_idx
    ON cts_transactions.ct_susp_movement_errors (record_type);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_record_count_idx
    ON cts_transactions.ct_susp_movement_errors (record_count);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_imported_date_idx
    ON cts_transactions.ct_susp_movement_errors (imported_date);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_record_type_idx
    ON cts_transactions.ct_suspended_animals (record_type);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_record_count_idx
    ON cts_transactions.ct_suspended_animals (record_count);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_imported_date_idx
    ON cts_transactions.ct_suspended_animals (imported_date);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_record_type_idx
    ON cts_transactions.ct_suspended_movements (record_type);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_record_count_idx
    ON cts_transactions.ct_suspended_movements (record_count);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_imported_date_idx
    ON cts_transactions.ct_suspended_movements (imported_date);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_record_type_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (record_type);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_record_count_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (record_count);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_imported_date_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (imported_date);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_record_type_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (record_type);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_record_count_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (record_count);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_imported_date_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (imported_date);
CREATE INDEX IF NOT EXISTS ct_valid_applications_record_type_idx
    ON cts_transactions.ct_valid_applications (record_type);
CREATE INDEX IF NOT EXISTS ct_valid_applications_record_count_idx
    ON cts_transactions.ct_valid_applications (record_count);
CREATE INDEX IF NOT EXISTS ct_valid_applications_imported_date_idx
    ON cts_transactions.ct_valid_applications (imported_date);
CREATE INDEX IF NOT EXISTS ct_web_users_record_type_idx
    ON cts_transactions.ct_web_users (record_type);
CREATE INDEX IF NOT EXISTS ct_web_users_record_count_idx
    ON cts_transactions.ct_web_users (record_count);
CREATE INDEX IF NOT EXISTS ct_web_users_imported_date_idx
    ON cts_transactions.ct_web_users (imported_date);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_record_type_idx
    ON cts_transactions.ct_wg_autoallocations (record_type);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_record_count_idx
    ON cts_transactions.ct_wg_autoallocations (record_count);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_imported_date_idx
    ON cts_transactions.ct_wg_autoallocations (imported_date);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_record_type_idx
    ON cts_transactions.ct_wg_super_assignments (record_type);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_record_count_idx
    ON cts_transactions.ct_wg_super_assignments (record_count);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_imported_date_idx
    ON cts_transactions.ct_wg_super_assignments (imported_date);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_record_type_idx
    ON cts_transactions.ct_wg_user_assignments (record_type);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_record_count_idx
    ON cts_transactions.ct_wg_user_assignments (record_count);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_imported_date_idx
    ON cts_transactions.ct_wg_user_assignments (imported_date);
CREATE INDEX IF NOT EXISTS ct_workgroups_record_type_idx
    ON cts_transactions.ct_workgroups (record_type);
CREATE INDEX IF NOT EXISTS ct_workgroups_record_count_idx
    ON cts_transactions.ct_workgroups (record_count);
CREATE INDEX IF NOT EXISTS ct_workgroups_imported_date_idx
    ON cts_transactions.ct_workgroups (imported_date);
CREATE INDEX IF NOT EXISTS ct_addresses_file_trans_type_idx
    ON cts_transactions.ct_addresses (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_addresses_file_record_count_idx
    ON cts_transactions.ct_addresses (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_addresses_aud_type_datetime_idx
    ON cts_transactions.ct_addresses (adr_aud_type, adr_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_addresses_file_row_number_idx
    ON cts_transactions.ct_addresses (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_animal_changes_file_trans_type_idx
    ON cts_transactions.ct_animal_changes (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_changes_file_record_count_idx
    ON cts_transactions.ct_animal_changes (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_animal_changes_aud_type_datetime_idx
    ON cts_transactions.ct_animal_changes (ach_aud_type, ach_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_changes_file_row_number_idx
    ON cts_transactions.ct_animal_changes (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_animal_claims_file_trans_type_idx
    ON cts_transactions.ct_animal_claims (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_claims_file_record_count_idx
    ON cts_transactions.ct_animal_claims (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_animal_claims_aud_type_datetime_idx
    ON cts_transactions.ct_animal_claims (anc_aud_type, anc_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_claims_file_row_number_idx
    ON cts_transactions.ct_animal_claims (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_file_trans_type_idx
    ON cts_transactions.ct_animal_corr_summ_errors (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_file_record_count_idx
    ON cts_transactions.ct_animal_corr_summ_errors (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_aud_type_datetime_idx
    ON cts_transactions.ct_animal_corr_summ_errors (ase_aud_type, ase_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_corr_summ_errors_file_row_number_idx
    ON cts_transactions.ct_animal_corr_summ_errors (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_file_trans_type_idx
    ON cts_transactions.ct_animal_correct_summaries (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_file_record_count_idx
    ON cts_transactions.ct_animal_correct_summaries (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_aud_type_datetime_idx
    ON cts_transactions.ct_animal_correct_summaries (acs_aud_type, acs_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_correct_summaries_file_row_number_idx
    ON cts_transactions.ct_animal_correct_summaries (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_file_trans_type_idx
    ON cts_transactions.ct_animal_identifiers (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_file_record_count_idx
    ON cts_transactions.ct_animal_identifiers (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aud_type_datetime_idx
    ON cts_transactions.ct_animal_identifiers (aid_aud_type, aid_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_file_row_number_idx
    ON cts_transactions.ct_animal_identifiers (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_file_trans_type_idx
    ON cts_transactions.ct_animal_relationships (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_file_record_count_idx
    ON cts_transactions.ct_animal_relationships (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_aud_type_datetime_idx
    ON cts_transactions.ct_animal_relationships (aar_aud_type, aar_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_relationships_file_row_number_idx
    ON cts_transactions.ct_animal_relationships (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_file_trans_type_idx
    ON cts_transactions.ct_animal_statuses (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_file_record_count_idx
    ON cts_transactions.ct_animal_statuses (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_aud_type_datetime_idx
    ON cts_transactions.ct_animal_statuses (ast_aud_type, ast_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_animal_statuses_file_row_number_idx
    ON cts_transactions.ct_animal_statuses (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_file_trans_type_idx
    ON cts_transactions.ct_applic_statuses (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_file_record_count_idx
    ON cts_transactions.ct_applic_statuses (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_aud_type_datetime_idx
    ON cts_transactions.ct_applic_statuses (aps_aud_type, aps_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_applic_statuses_file_row_number_idx
    ON cts_transactions.ct_applic_statuses (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_application_late_days_file_trans_type_idx
    ON cts_transactions.ct_application_late_days (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_application_late_days_file_record_count_idx
    ON cts_transactions.ct_application_late_days (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_application_late_days_aud_type_datetime_idx
    ON cts_transactions.ct_application_late_days (ald_aud_type, ald_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_application_late_days_file_row_number_idx
    ON cts_transactions.ct_application_late_days (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_cla_extract_file_trans_type_idx
    ON cts_transactions.ct_cla_extract (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_cla_extract_file_record_count_idx
    ON cts_transactions.ct_cla_extract (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_cla_extract_aud_type_datetime_idx
    ON cts_transactions.ct_cla_extract (cle_aud_type, cle_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cla_extract_file_row_number_idx
    ON cts_transactions.ct_cla_extract (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_file_trans_type_idx
    ON cts_transactions.ct_cla_extract_detail (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_file_record_count_idx
    ON cts_transactions.ct_cla_extract_detail (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_cla_extract_detail_aud_type_datetime_idx
    ON cts_transactions.ct_cla_extract_detail (cld_aud_type, cld_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_file_trans_type_idx
    ON cts_transactions.ct_cla_extract_dm (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_file_record_count_idx
    ON cts_transactions.ct_cla_extract_dm (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_aud_type_datetime_idx
    ON cts_transactions.ct_cla_extract_dm (cle_aud_type, cle_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cla_extract_dm_file_row_number_idx
    ON cts_transactions.ct_cla_extract_dm (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_file_trans_type_idx
    ON cts_transactions.ct_cla_mini_detail (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_file_record_count_idx
    ON cts_transactions.ct_cla_mini_detail (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_cla_mini_detail_aud_type_datetime_idx
    ON cts_transactions.ct_cla_mini_detail (cld_aud_type, cld_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_file_trans_type_idx
    ON cts_transactions.ct_cla_mini_extract (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_file_record_count_idx
    ON cts_transactions.ct_cla_mini_extract (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_aud_type_datetime_idx
    ON cts_transactions.ct_cla_mini_extract (cle_aud_type, cle_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cla_mini_extract_file_row_number_idx
    ON cts_transactions.ct_cla_mini_extract (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_file_trans_type_idx
    ON cts_transactions.ct_cm_measures_results (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_file_record_count_idx
    ON cts_transactions.ct_cm_measures_results (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_aud_type_datetime_idx
    ON cts_transactions.ct_cm_measures_results (cmr_aud_type, cmr_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cm_measures_results_file_row_number_idx
    ON cts_transactions.ct_cm_measures_results (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_file_trans_type_idx
    ON cts_transactions.ct_comms_addresses (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_file_record_count_idx
    ON cts_transactions.ct_comms_addresses (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_aud_type_datetime_idx
    ON cts_transactions.ct_comms_addresses (coa_aud_type, coa_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_comms_addresses_file_row_number_idx
    ON cts_transactions.ct_comms_addresses (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_file_trans_type_idx
    ON cts_transactions.ct_condition_marker_errors (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_file_record_count_idx
    ON cts_transactions.ct_condition_marker_errors (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_aud_type_datetime_idx
    ON cts_transactions.ct_condition_marker_errors (cme_aud_type, cme_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_condition_marker_errors_file_row_number_idx
    ON cts_transactions.ct_condition_marker_errors (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_condition_markers_file_trans_type_idx
    ON cts_transactions.ct_condition_markers (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_condition_markers_file_record_count_idx
    ON cts_transactions.ct_condition_markers (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_condition_markers_aud_type_datetime_idx
    ON cts_transactions.ct_condition_markers (com_aud_type, com_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_condition_markers_file_row_number_idx
    ON cts_transactions.ct_condition_markers (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_cps167_report_file_trans_type_idx
    ON cts_transactions.ct_cps167_report (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_cps167_report_file_record_count_idx
    ON cts_transactions.ct_cps167_report (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_cps167_report_aud_type_datetime_idx
    ON cts_transactions.ct_cps167_report (kns_aud_type, kns_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cps167_report_file_row_number_idx
    ON cts_transactions.ct_cps167_report (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_cts_users_file_trans_type_idx
    ON cts_transactions.ct_cts_users (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_cts_users_file_record_count_idx
    ON cts_transactions.ct_cts_users (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_cts_users_aud_type_datetime_idx
    ON cts_transactions.ct_cts_users (cus_aud_type, cus_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cts_users_file_row_number_idx
    ON cts_transactions.ct_cts_users (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_file_trans_type_idx
    ON cts_transactions.ct_eartag_staging (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_file_record_count_idx
    ON cts_transactions.ct_eartag_staging (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_eartag_staging_aud_type_datetime_idx
    ON cts_transactions.ct_eartag_staging (est_aud_type, est_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartags_file_trans_type_idx
    ON cts_transactions.ct_eartags (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_eartags_file_record_count_idx
    ON cts_transactions.ct_eartags (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_eartags_aud_type_datetime_idx
    ON cts_transactions.ct_eartags (etg_aud_type, etg_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartags_file_row_number_idx
    ON cts_transactions.ct_eartags (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_file_trans_type_idx
    ON cts_transactions.ct_electronic_identifiers (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_file_record_count_idx
    ON cts_transactions.ct_electronic_identifiers (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_aud_type_datetime_idx
    ON cts_transactions.ct_electronic_identifiers (eid_aud_type, eid_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_electronic_identifiers_file_row_number_idx
    ON cts_transactions.ct_electronic_identifiers (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_email_log_file_trans_type_idx
    ON cts_transactions.ct_email_log (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_email_log_file_record_count_idx
    ON cts_transactions.ct_email_log (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_email_log_aud_type_datetime_idx
    ON cts_transactions.ct_email_log (eml_aud_type, eml_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_email_log_file_row_number_idx
    ON cts_transactions.ct_email_log (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_ereport_files_file_trans_type_idx
    ON cts_transactions.ct_ereport_files (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_ereport_files_file_record_count_idx
    ON cts_transactions.ct_ereport_files (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_ereport_files_aud_type_datetime_idx
    ON cts_transactions.ct_ereport_files (ere_aud_type, ere_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_file_trans_type_idx
    ON cts_transactions.ct_ereport_load_messages (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_file_record_count_idx
    ON cts_transactions.ct_ereport_load_messages (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_aud_type_datetime_idx
    ON cts_transactions.ct_ereport_load_messages (erm_aud_type, erm_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ereport_load_messages_file_row_number_idx
    ON cts_transactions.ct_ereport_load_messages (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_ereport_locks_file_trans_type_idx
    ON cts_transactions.ct_ereport_locks (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_ereport_locks_file_record_count_idx
    ON cts_transactions.ct_ereport_locks (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_ereport_locks_aud_type_datetime_idx
    ON cts_transactions.ct_ereport_locks (erl_aud_type, erl_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_file_trans_type_idx
    ON cts_transactions.ct_ereport_process_messages (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_file_record_count_idx
    ON cts_transactions.ct_ereport_process_messages (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_aud_type_datetime_idx
    ON cts_transactions.ct_ereport_process_messages (erq_aud_type, erq_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ereport_process_messages_file_row_number_idx
    ON cts_transactions.ct_ereport_process_messages (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_file_trans_type_idx
    ON cts_transactions.ct_ext_cetd_eartag (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_file_record_count_idx
    ON cts_transactions.ct_ext_cetd_eartag (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_aud_type_datetime_idx
    ON cts_transactions.ct_ext_cetd_eartag (cet_aud_type, cet_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ext_cetd_eartag_file_row_number_idx
    ON cts_transactions.ct_ext_cetd_eartag (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_file_trans_type_idx
    ON cts_transactions.ct_insert_update_log (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_file_record_count_idx
    ON cts_transactions.ct_insert_update_log (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_aud_type_datetime_idx
    ON cts_transactions.ct_insert_update_log (iul_aud_type, iul_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_insert_update_log_file_row_number_idx
    ON cts_transactions.ct_insert_update_log (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_issued_documents_file_trans_type_idx
    ON cts_transactions.ct_issued_documents (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_issued_documents_file_record_count_idx
    ON cts_transactions.ct_issued_documents (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_issued_documents_aud_type_datetime_idx
    ON cts_transactions.ct_issued_documents (ido_aud_type, ido_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_issued_documents_file_row_number_idx
    ON cts_transactions.ct_issued_documents (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_label_requests_file_trans_type_idx
    ON cts_transactions.ct_label_requests (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_label_requests_file_record_count_idx
    ON cts_transactions.ct_label_requests (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_label_requests_aud_type_datetime_idx
    ON cts_transactions.ct_label_requests (lar_aud_type, lar_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_label_requests_file_row_number_idx
    ON cts_transactions.ct_label_requests (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_label_summaries_file_trans_type_idx
    ON cts_transactions.ct_label_summaries (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_label_summaries_file_record_count_idx
    ON cts_transactions.ct_label_summaries (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_label_summaries_aud_type_datetime_idx
    ON cts_transactions.ct_label_summaries (las_aud_type, las_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_label_summaries_file_row_number_idx
    ON cts_transactions.ct_label_summaries (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_letters_file_trans_type_idx
    ON cts_transactions.ct_letters (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_letters_file_record_count_idx
    ON cts_transactions.ct_letters (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_letters_aud_type_datetime_idx
    ON cts_transactions.ct_letters (let_aud_type, let_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_letters_file_row_number_idx
    ON cts_transactions.ct_letters (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_letters_file_let_type_idx
    ON cts_transactions.ct_letters (cts_file_import_id, let_type);
CREATE INDEX IF NOT EXISTS ct_letters_file_let_type_aud_type_idx
    ON cts_transactions.ct_letters (cts_file_import_id, let_type, let_aud_type);
CREATE INDEX IF NOT EXISTS ct_letters_file_let_type_aud_datetime_idx
    ON cts_transactions.ct_letters (cts_file_import_id, let_type, let_aud_type, let_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_file_trans_type_idx
    ON cts_transactions.ct_location_identifiers (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_file_record_count_idx
    ON cts_transactions.ct_location_identifiers (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_aud_type_datetime_idx
    ON cts_transactions.ct_location_identifiers (lid_aud_type, lid_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_identifiers_file_row_number_idx
    ON cts_transactions.ct_location_identifiers (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_file_trans_type_idx
    ON cts_transactions.ct_location_party_rels (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_file_record_count_idx
    ON cts_transactions.ct_location_party_rels (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_aud_type_datetime_idx
    ON cts_transactions.ct_location_party_rels (lpr_aud_type, lpr_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_party_rels_file_row_number_idx
    ON cts_transactions.ct_location_party_rels (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_location_relationships_file_trans_type_idx
    ON cts_transactions.ct_location_relationships (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_location_relationships_file_record_count_idx
    ON cts_transactions.ct_location_relationships (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_location_relationships_aud_type_datetime_idx
    ON cts_transactions.ct_location_relationships (llr_aud_type, llr_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_relationships_file_row_number_idx
    ON cts_transactions.ct_location_relationships (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_locations_file_trans_type_idx
    ON cts_transactions.ct_locations (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_locations_file_record_count_idx
    ON cts_transactions.ct_locations (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_locations_aud_type_datetime_idx
    ON cts_transactions.ct_locations (loc_aud_type, loc_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_locations_file_row_number_idx
    ON cts_transactions.ct_locations (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_locations_faker_file_trans_type_idx
    ON cts_transactions.ct_locations_faker (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_locations_faker_file_record_count_idx
    ON cts_transactions.ct_locations_faker (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_locations_faker_aud_type_datetime_idx
    ON cts_transactions.ct_locations_faker (loc_aud_type, loc_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_file_trans_type_idx
    ON cts_transactions.ct_locrestrictionstoanimals (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_file_record_count_idx
    ON cts_transactions.ct_locrestrictionstoanimals (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_aud_type_datetime_idx
    ON cts_transactions.ct_locrestrictionstoanimals (lra_aud_type, lra_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_locrestrictionstoanimals_file_row_number_idx
    ON cts_transactions.ct_locrestrictionstoanimals (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_file_trans_type_idx
    ON cts_transactions.ct_mgt_control_errors (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_file_record_count_idx
    ON cts_transactions.ct_mgt_control_errors (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_aud_type_datetime_idx
    ON cts_transactions.ct_mgt_control_errors (mce_aud_type, mce_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_mgt_control_errors_file_row_number_idx
    ON cts_transactions.ct_mgt_control_errors (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_file_trans_type_idx
    ON cts_transactions.ct_mhs_to_cph (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_file_record_count_idx
    ON cts_transactions.ct_mhs_to_cph (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_aud_type_datetime_idx
    ON cts_transactions.ct_mhs_to_cph (cph_aud_type, cph_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_mhs_to_cph_file_row_number_idx
    ON cts_transactions.ct_mhs_to_cph (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_mov_hst_file_trans_type_idx
    ON cts_transactions.ct_mov_hst (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_mov_hst_file_record_count_idx
    ON cts_transactions.ct_mov_hst (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_mov_hst_aud_type_datetime_idx
    ON cts_transactions.ct_mov_hst (hst_aud_type, hst_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_mov_hst_file_row_number_idx
    ON cts_transactions.ct_mov_hst (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_file_trans_type_idx
    ON cts_transactions.ct_movt_corr_summ_errors (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_file_record_count_idx
    ON cts_transactions.ct_movt_corr_summ_errors (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_aud_type_datetime_idx
    ON cts_transactions.ct_movt_corr_summ_errors (mse_aud_type, mse_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_movt_corr_summ_errors_file_row_number_idx
    ON cts_transactions.ct_movt_corr_summ_errors (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_file_trans_type_idx
    ON cts_transactions.ct_movt_correct_summaries (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_file_record_count_idx
    ON cts_transactions.ct_movt_correct_summaries (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_aud_type_datetime_idx
    ON cts_transactions.ct_movt_correct_summaries (mcs_aud_type, mcs_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_movt_correct_summaries_file_row_number_idx
    ON cts_transactions.ct_movt_correct_summaries (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_parties_file_trans_type_idx
    ON cts_transactions.ct_parties (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_parties_file_record_count_idx
    ON cts_transactions.ct_parties (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_parties_aud_type_datetime_idx
    ON cts_transactions.ct_parties (par_aud_type, par_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_parties_file_row_number_idx
    ON cts_transactions.ct_parties (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_parties_faker_file_trans_type_idx
    ON cts_transactions.ct_parties_faker (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_parties_faker_file_record_count_idx
    ON cts_transactions.ct_parties_faker (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_parties_faker_aud_type_datetime_idx
    ON cts_transactions.ct_parties_faker (par_aud_type, par_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_file_trans_type_idx
    ON cts_transactions.ct_ppaf_groupings (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_file_record_count_idx
    ON cts_transactions.ct_ppaf_groupings (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_aud_type_datetime_idx
    ON cts_transactions.ct_ppaf_groupings (ppg_aud_type, ppg_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ppaf_groupings_file_row_number_idx
    ON cts_transactions.ct_ppaf_groupings (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_file_trans_type_idx
    ON cts_transactions.ct_preprinted_appn_forms (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_file_record_count_idx
    ON cts_transactions.ct_preprinted_appn_forms (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_aud_type_datetime_idx
    ON cts_transactions.ct_preprinted_appn_forms (paf_aud_type, paf_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_preprinted_appn_forms_file_row_number_idx
    ON cts_transactions.ct_preprinted_appn_forms (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_file_trans_type_idx
    ON cts_transactions.ct_ps9999_ahdb_data (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_file_record_count_idx
    ON cts_transactions.ct_ps9999_ahdb_data (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_aud_type_datetime_idx
    ON cts_transactions.ct_ps9999_ahdb_data (ran_aud_type, ran_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_data_file_row_number_idx
    ON cts_transactions.ct_ps9999_ahdb_data (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_file_trans_type_idx
    ON cts_transactions.ct_ps9999_ahdb_mov_history (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_file_record_count_idx
    ON cts_transactions.ct_ps9999_ahdb_mov_history (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_aud_type_datetime_idx
    ON cts_transactions.ct_ps9999_ahdb_mov_history (loc_aud_type, loc_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ps9999_ahdb_mov_history_file_row_number_idx
    ON cts_transactions.ct_ps9999_ahdb_mov_history (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_file_trans_type_idx
    ON cts_transactions.ct_recd_application_errors (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_file_record_count_idx
    ON cts_transactions.ct_recd_application_errors (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_aud_type_datetime_idx
    ON cts_transactions.ct_recd_application_errors (rae_aud_type, rae_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_recd_application_errors_file_row_number_idx
    ON cts_transactions.ct_recd_application_errors (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_file_trans_type_idx
    ON cts_transactions.ct_recd_movement_errors (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_file_record_count_idx
    ON cts_transactions.ct_recd_movement_errors (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_aud_type_datetime_idx
    ON cts_transactions.ct_recd_movement_errors (rme_aud_type, rme_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_recd_movement_errors_file_row_number_idx
    ON cts_transactions.ct_recd_movement_errors (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_received_applications_file_trans_type_idx
    ON cts_transactions.ct_received_applications (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_received_applications_file_record_count_idx
    ON cts_transactions.ct_received_applications (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_received_applications_aud_type_datetime_idx
    ON cts_transactions.ct_received_applications (rap_aud_type, rap_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_received_applications_file_row_number_idx
    ON cts_transactions.ct_received_applications (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_received_movements_file_trans_type_idx
    ON cts_transactions.ct_received_movements (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_received_movements_file_record_count_idx
    ON cts_transactions.ct_received_movements (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_received_movements_aud_type_datetime_idx
    ON cts_transactions.ct_received_movements (rmo_aud_type, rmo_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_received_movements_file_row_number_idx
    ON cts_transactions.ct_received_movements (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_registered_animals_file_trans_type_idx
    ON cts_transactions.ct_registered_animals (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_registered_animals_file_record_count_idx
    ON cts_transactions.ct_registered_animals (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_registered_animals_aud_type_datetime_idx
    ON cts_transactions.ct_registered_animals (ran_aud_type, ran_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_registered_animals_file_row_number_idx
    ON cts_transactions.ct_registered_animals (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_registered_movements_file_trans_type_idx
    ON cts_transactions.ct_registered_movements (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_registered_movements_file_record_count_idx
    ON cts_transactions.ct_registered_movements (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_registered_movements_aud_type_datetime_idx
    ON cts_transactions.ct_registered_movements (mov_aud_type, mov_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_registered_movements_file_row_number_idx
    ON cts_transactions.ct_registered_movements (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_file_trans_type_idx
    ON cts_transactions.ct_reset_to_extract (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_file_record_count_idx
    ON cts_transactions.ct_reset_to_extract (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_aud_type_datetime_idx
    ON cts_transactions.ct_reset_to_extract (rte_aud_type, rte_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_reset_to_extract_file_row_number_idx
    ON cts_transactions.ct_reset_to_extract (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_file_trans_type_idx
    ON cts_transactions.ct_sbcs_ext (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_file_record_count_idx
    ON cts_transactions.ct_sbcs_ext (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_aud_type_datetime_idx
    ON cts_transactions.ct_sbcs_ext (sxt_aud_type, sxt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_sbcs_ext_file_row_number_idx
    ON cts_transactions.ct_sbcs_ext (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_stage_files_file_trans_type_idx
    ON cts_transactions.ct_stage_files (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_stage_files_file_record_count_idx
    ON cts_transactions.ct_stage_files (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_stage_files_aud_type_datetime_idx
    ON cts_transactions.ct_stage_files (stf_aud_type, stf_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_stage_files_file_row_number_idx
    ON cts_transactions.ct_stage_files (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_stage_locks_file_trans_type_idx
    ON cts_transactions.ct_stage_locks (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_stage_locks_file_record_count_idx
    ON cts_transactions.ct_stage_locks (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_stage_locks_aud_type_datetime_idx
    ON cts_transactions.ct_stage_locks (stl_aud_type, stl_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_stage_locks_file_row_number_idx
    ON cts_transactions.ct_stage_locks (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_stage_messages_file_trans_type_idx
    ON cts_transactions.ct_stage_messages (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_stage_messages_file_record_count_idx
    ON cts_transactions.ct_stage_messages (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_stage_messages_aud_type_datetime_idx
    ON cts_transactions.ct_stage_messages (stm_aud_type, stm_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_stage_messages_file_row_number_idx
    ON cts_transactions.ct_stage_messages (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_file_trans_type_idx
    ON cts_transactions.ct_susp_animal_errors (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_file_record_count_idx
    ON cts_transactions.ct_susp_animal_errors (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_aud_type_datetime_idx
    ON cts_transactions.ct_susp_animal_errors (sae_aud_type, sae_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_susp_animal_errors_file_row_number_idx
    ON cts_transactions.ct_susp_animal_errors (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_file_trans_type_idx
    ON cts_transactions.ct_susp_cm_measure_results (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_file_record_count_idx
    ON cts_transactions.ct_susp_cm_measure_results (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_aud_type_datetime_idx
    ON cts_transactions.ct_susp_cm_measure_results (smr_aud_type, smr_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_susp_cm_measure_results_file_row_number_idx
    ON cts_transactions.ct_susp_cm_measure_results (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_file_trans_type_idx
    ON cts_transactions.ct_susp_condition_markers (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_file_record_count_idx
    ON cts_transactions.ct_susp_condition_markers (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_aud_type_datetime_idx
    ON cts_transactions.ct_susp_condition_markers (scm_aud_type, scm_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_susp_condition_markers_file_row_number_idx
    ON cts_transactions.ct_susp_condition_markers (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_file_trans_type_idx
    ON cts_transactions.ct_susp_movement_errors (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_file_record_count_idx
    ON cts_transactions.ct_susp_movement_errors (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_aud_type_datetime_idx
    ON cts_transactions.ct_susp_movement_errors (sme_aud_type, sme_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_susp_movement_errors_file_row_number_idx
    ON cts_transactions.ct_susp_movement_errors (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_file_trans_type_idx
    ON cts_transactions.ct_suspended_animals (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_file_record_count_idx
    ON cts_transactions.ct_suspended_animals (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_aud_type_datetime_idx
    ON cts_transactions.ct_suspended_animals (san_aud_type, san_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_suspended_animals_file_row_number_idx
    ON cts_transactions.ct_suspended_animals (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_file_trans_type_idx
    ON cts_transactions.ct_suspended_movements (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_file_record_count_idx
    ON cts_transactions.ct_suspended_movements (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_aud_type_datetime_idx
    ON cts_transactions.ct_suspended_movements (smo_aud_type, smo_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_suspended_movements_file_row_number_idx
    ON cts_transactions.ct_suspended_movements (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_valid_applications_file_trans_type_idx
    ON cts_transactions.ct_valid_applications (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_valid_applications_file_record_count_idx
    ON cts_transactions.ct_valid_applications (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_valid_applications_aud_type_datetime_idx
    ON cts_transactions.ct_valid_applications (vap_aud_type, vap_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_valid_applications_file_row_number_idx
    ON cts_transactions.ct_valid_applications (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_web_users_file_trans_type_idx
    ON cts_transactions.ct_web_users (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_web_users_file_record_count_idx
    ON cts_transactions.ct_web_users (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_web_users_aud_type_datetime_idx
    ON cts_transactions.ct_web_users (wur_aud_type, wur_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_web_users_file_row_number_idx
    ON cts_transactions.ct_web_users (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_file_trans_type_idx
    ON cts_transactions.ct_wg_autoallocations (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_file_record_count_idx
    ON cts_transactions.ct_wg_autoallocations (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_aud_type_datetime_idx
    ON cts_transactions.ct_wg_autoallocations (wga_aud_type, wga_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_wg_autoallocations_file_row_number_idx
    ON cts_transactions.ct_wg_autoallocations (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_file_trans_type_idx
    ON cts_transactions.ct_wg_super_assignments (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_file_record_count_idx
    ON cts_transactions.ct_wg_super_assignments (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_aud_type_datetime_idx
    ON cts_transactions.ct_wg_super_assignments (wsa_aud_type, wsa_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_wg_super_assignments_file_row_number_idx
    ON cts_transactions.ct_wg_super_assignments (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_file_trans_type_idx
    ON cts_transactions.ct_wg_user_assignments (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_file_record_count_idx
    ON cts_transactions.ct_wg_user_assignments (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_aud_type_datetime_idx
    ON cts_transactions.ct_wg_user_assignments (wua_aud_type, wua_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_wg_user_assignments_file_row_number_idx
    ON cts_transactions.ct_wg_user_assignments (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_workgroups_file_trans_type_idx
    ON cts_transactions.ct_workgroups (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_workgroups_file_record_count_idx
    ON cts_transactions.ct_workgroups (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_workgroups_aud_type_datetime_idx
    ON cts_transactions.ct_workgroups (wgp_aud_type, wgp_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_workgroups_file_row_number_idx
    ON cts_transactions.ct_workgroups (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_file_trans_type_idx
    ON cts_transactions.ct_alloc_routines (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_file_record_count_idx
    ON cts_transactions.ct_alloc_routines (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_aud_type_datetime_idx
    ON cts_transactions.ct_alloc_routines (rou_aud_type, rou_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_alloc_routines_file_row_number_idx
    ON cts_transactions.ct_alloc_routines (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_batch_retention_conf_file_trans_type_idx
    ON cts_transactions.ct_batch_retention_conf (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_batch_retention_conf_file_record_count_idx
    ON cts_transactions.ct_batch_retention_conf (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_batch_retention_conf_aud_type_datetime_idx
    ON cts_transactions.ct_batch_retention_conf (brt_aud_type, brt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_batch_retention_conf_file_row_number_idx
    ON cts_transactions.ct_batch_retention_conf (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_breeds_file_trans_type_idx
    ON cts_transactions.ct_breeds (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_breeds_file_record_count_idx
    ON cts_transactions.ct_breeds (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_breeds_aud_type_datetime_idx
    ON cts_transactions.ct_breeds (brd_aud_type, brd_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_breeds_file_row_number_idx
    ON cts_transactions.ct_breeds (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_breeds_file_brd_type_idx
    ON cts_transactions.ct_breeds (cts_file_import_id, brd_type);
CREATE INDEX IF NOT EXISTS ct_breeds_file_brd_type_aud_type_idx
    ON cts_transactions.ct_breeds (cts_file_import_id, brd_type, brd_aud_type);
CREATE INDEX IF NOT EXISTS ct_breeds_file_brd_type_aud_datetime_idx
    ON cts_transactions.ct_breeds (cts_file_import_id, brd_type, brd_aud_type, brd_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_file_trans_type_idx
    ON cts_transactions.ct_claim_statuses (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_file_record_count_idx
    ON cts_transactions.ct_claim_statuses (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_aud_type_datetime_idx
    ON cts_transactions.ct_claim_statuses (cls_aud_type, cls_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_claim_statuses_file_row_number_idx
    ON cts_transactions.ct_claim_statuses (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_claim_types_file_trans_type_idx
    ON cts_transactions.ct_claim_types (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_claim_types_file_record_count_idx
    ON cts_transactions.ct_claim_types (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_claim_types_aud_type_datetime_idx
    ON cts_transactions.ct_claim_types (clt_aud_type, clt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_claim_types_file_row_number_idx
    ON cts_transactions.ct_claim_types (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_file_trans_type_idx
    ON cts_transactions.ct_cm_authorities (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_file_record_count_idx
    ON cts_transactions.ct_cm_authorities (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_aud_type_datetime_idx
    ON cts_transactions.ct_cm_authorities (cma_aud_type, cma_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cm_authorities_file_row_number_idx
    ON cts_transactions.ct_cm_authorities (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_file_trans_type_idx
    ON cts_transactions.ct_cond_variant_groupings (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_file_record_count_idx
    ON cts_transactions.ct_cond_variant_groupings (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_aud_type_datetime_idx
    ON cts_transactions.ct_cond_variant_groupings (cvg_aud_type, cvg_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_cond_variant_groupings_file_row_number_idx
    ON cts_transactions.ct_cond_variant_groupings (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_condition_activities_file_trans_type_idx
    ON cts_transactions.ct_condition_activities (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_condition_activities_file_record_count_idx
    ON cts_transactions.ct_condition_activities (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_condition_activities_aud_type_datetime_idx
    ON cts_transactions.ct_condition_activities (cac_aud_type, cac_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_condition_activities_file_row_number_idx
    ON cts_transactions.ct_condition_activities (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_condition_types_file_trans_type_idx
    ON cts_transactions.ct_condition_types (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_condition_types_file_record_count_idx
    ON cts_transactions.ct_condition_types (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_condition_types_aud_type_datetime_idx
    ON cts_transactions.ct_condition_types (cot_aud_type, cot_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_condition_types_file_row_number_idx
    ON cts_transactions.ct_condition_types (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_condition_variants_file_trans_type_idx
    ON cts_transactions.ct_condition_variants (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_condition_variants_file_record_count_idx
    ON cts_transactions.ct_condition_variants (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_condition_variants_aud_type_datetime_idx
    ON cts_transactions.ct_condition_variants (cov_aud_type, cov_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_condition_variants_file_row_number_idx
    ON cts_transactions.ct_condition_variants (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_conditions_file_trans_type_idx
    ON cts_transactions.ct_conditions (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_conditions_file_record_count_idx
    ON cts_transactions.ct_conditions (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_conditions_aud_type_datetime_idx
    ON cts_transactions.ct_conditions (con_aud_type, con_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_conditions_file_row_number_idx
    ON cts_transactions.ct_conditions (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_counties_file_trans_type_idx
    ON cts_transactions.ct_counties (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_counties_file_record_count_idx
    ON cts_transactions.ct_counties (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_counties_aud_type_datetime_idx
    ON cts_transactions.ct_counties (cty_aud_type, cty_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_counties_file_row_number_idx
    ON cts_transactions.ct_counties (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_counties_migration_file_trans_type_idx
    ON cts_transactions.ct_counties_migration (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_counties_migration_file_record_count_idx
    ON cts_transactions.ct_counties_migration (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_counties_migration_aud_type_datetime_idx
    ON cts_transactions.ct_counties_migration (cty_aud_type, cty_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_counties_migration_file_row_number_idx
    ON cts_transactions.ct_counties_migration (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_countries_file_trans_type_idx
    ON cts_transactions.ct_countries (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_countries_file_record_count_idx
    ON cts_transactions.ct_countries (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_countries_aud_type_datetime_idx
    ON cts_transactions.ct_countries (cry_aud_type, cry_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_countries_file_row_number_idx
    ON cts_transactions.ct_countries (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_cts164_handshake_file_keys_file_trans_type_idx
    ON cts_transactions.ct_cts164_handshake_file_keys (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_cts164_handshake_file_keys_file_record_count_idx
    ON cts_transactions.ct_cts164_handshake_file_keys (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_cts164_handshake_file_keys_aud_type_datetime_idx
    ON cts_transactions.ct_cts164_handshake_file_keys (bjk_aud_type, bjk_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_file_trans_type_idx
    ON cts_transactions.ct_eartag_formats (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_file_record_count_idx
    ON cts_transactions.ct_eartag_formats (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_aud_type_datetime_idx
    ON cts_transactions.ct_eartag_formats (etf_aud_type, etf_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartag_formats_file_row_number_idx
    ON cts_transactions.ct_eartag_formats (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_file_trans_type_idx
    ON cts_transactions.ct_eartag_reason_flags (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_file_record_count_idx
    ON cts_transactions.ct_eartag_reason_flags (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_aud_type_datetime_idx
    ON cts_transactions.ct_eartag_reason_flags (erf_aud_type, erf_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartag_reason_flags_file_row_number_idx
    ON cts_transactions.ct_eartag_reason_flags (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_file_trans_type_idx
    ON cts_transactions.ct_eartag_reasons (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_file_record_count_idx
    ON cts_transactions.ct_eartag_reasons (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_aud_type_datetime_idx
    ON cts_transactions.ct_eartag_reasons (etr_aud_type, etr_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartag_reasons_file_row_number_idx
    ON cts_transactions.ct_eartag_reasons (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_eartag_types_file_trans_type_idx
    ON cts_transactions.ct_eartag_types (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_eartag_types_file_record_count_idx
    ON cts_transactions.ct_eartag_types (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_eartag_types_aud_type_datetime_idx
    ON cts_transactions.ct_eartag_types (ett_aud_type, ett_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_eartag_types_file_row_number_idx
    ON cts_transactions.ct_eartag_types (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_ext_ni_district_file_trans_type_idx
    ON cts_transactions.ct_ext_ni_district (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_ext_ni_district_file_record_count_idx
    ON cts_transactions.ct_ext_ni_district (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_ext_ni_district_aud_type_datetime_idx
    ON cts_transactions.ct_ext_ni_district (nid_aud_type, nid_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ext_ni_district_file_row_number_idx
    ON cts_transactions.ct_ext_ni_district (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_ext_special_herd_file_trans_type_idx
    ON cts_transactions.ct_ext_special_herd (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_ext_special_herd_file_record_count_idx
    ON cts_transactions.ct_ext_special_herd (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_ext_special_herd_aud_type_datetime_idx
    ON cts_transactions.ct_ext_special_herd (sph_aud_type, sph_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_ext_special_herd_file_row_number_idx
    ON cts_transactions.ct_ext_special_herd (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_file_layouts_file_trans_type_idx
    ON cts_transactions.ct_file_layouts (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_file_layouts_file_record_count_idx
    ON cts_transactions.ct_file_layouts (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_file_layouts_aud_type_datetime_idx
    ON cts_transactions.ct_file_layouts (flt_aud_type, flt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_file_layouts_file_row_number_idx
    ON cts_transactions.ct_file_layouts (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_hsf_sequences_file_trans_type_idx
    ON cts_transactions.ct_hsf_sequences (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_hsf_sequences_file_record_count_idx
    ON cts_transactions.ct_hsf_sequences (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_hsf_sequences_aud_type_datetime_idx
    ON cts_transactions.ct_hsf_sequences (hss_aud_type, hss_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_hsf_sequences_file_row_number_idx
    ON cts_transactions.ct_hsf_sequences (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_file_trans_type_idx
    ON cts_transactions.ct_issuing_authorities (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_file_record_count_idx
    ON cts_transactions.ct_issuing_authorities (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_aud_type_datetime_idx
    ON cts_transactions.ct_issuing_authorities (isa_aud_type, isa_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_file_row_number_idx
    ON cts_transactions.ct_issuing_authorities (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_file_isa_type_idx
    ON cts_transactions.ct_issuing_authorities (cts_file_import_id, isa_type);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_file_isa_type_aud_type_idx
    ON cts_transactions.ct_issuing_authorities (cts_file_import_id, isa_type, isa_aud_type);
CREATE INDEX IF NOT EXISTS ct_issuing_authorities_file_isa_type_aud_datetime_idx
    ON cts_transactions.ct_issuing_authorities (cts_file_import_id, isa_type, isa_aud_type, isa_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_late_days_file_trans_type_idx
    ON cts_transactions.ct_late_days (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_late_days_file_record_count_idx
    ON cts_transactions.ct_late_days (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_late_days_aud_type_datetime_idx
    ON cts_transactions.ct_late_days (lda_aud_type, lda_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_late_days_file_row_number_idx
    ON cts_transactions.ct_late_days (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_file_trans_type_idx
    ON cts_transactions.ct_loc_type_rel_combs (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_file_record_count_idx
    ON cts_transactions.ct_loc_type_rel_combs (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_aud_type_datetime_idx
    ON cts_transactions.ct_loc_type_rel_combs (lrc_aud_type, lrc_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_loc_type_rel_combs_file_row_number_idx
    ON cts_transactions.ct_loc_type_rel_combs (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_file_trans_type_idx
    ON cts_transactions.ct_location_id_formats (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_file_record_count_idx
    ON cts_transactions.ct_location_id_formats (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_aud_type_datetime_idx
    ON cts_transactions.ct_location_id_formats (lif_aud_type, lif_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_id_formats_file_row_number_idx
    ON cts_transactions.ct_location_id_formats (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_file_trans_type_idx
    ON cts_transactions.ct_location_party_rel_types (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_file_record_count_idx
    ON cts_transactions.ct_location_party_rel_types (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_aud_type_datetime_idx
    ON cts_transactions.ct_location_party_rel_types (lpt_aud_type, lpt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_party_rel_types_file_row_number_idx
    ON cts_transactions.ct_location_party_rel_types (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_file_trans_type_idx
    ON cts_transactions.ct_location_rel_types (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_file_record_count_idx
    ON cts_transactions.ct_location_rel_types (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_aud_type_datetime_idx
    ON cts_transactions.ct_location_rel_types (lrt_aud_type, lrt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_rel_types_file_row_number_idx
    ON cts_transactions.ct_location_rel_types (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_location_types_file_trans_type_idx
    ON cts_transactions.ct_location_types (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_location_types_file_record_count_idx
    ON cts_transactions.ct_location_types (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_location_types_aud_type_datetime_idx
    ON cts_transactions.ct_location_types (lty_aud_type, lty_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_location_types_file_row_number_idx
    ON cts_transactions.ct_location_types (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_file_trans_type_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_file_record_count_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_aud_type_datetime_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (war_aud_type, war_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_mgt_wg_allocation_rules_file_row_number_idx
    ON cts_transactions.ct_mgt_wg_allocation_rules (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_msgtxt_file_trans_type_idx
    ON cts_transactions.ct_msgtxt (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_msgtxt_file_record_count_idx
    ON cts_transactions.ct_msgtxt (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_msgtxt_aud_type_datetime_idx
    ON cts_transactions.ct_msgtxt (msg_aud_type, msg_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_msgtxt_file_row_number_idx
    ON cts_transactions.ct_msgtxt (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_non_working_days_file_trans_type_idx
    ON cts_transactions.ct_non_working_days (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_non_working_days_file_record_count_idx
    ON cts_transactions.ct_non_working_days (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_non_working_days_aud_type_datetime_idx
    ON cts_transactions.ct_non_working_days (nwd_aud_type, nwd_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_non_working_days_file_row_number_idx
    ON cts_transactions.ct_non_working_days (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_param_group_file_trans_type_idx
    ON cts_transactions.ct_param_group (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_param_group_file_record_count_idx
    ON cts_transactions.ct_param_group (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_param_group_aud_type_datetime_idx
    ON cts_transactions.ct_param_group (pgp_aud_type, pgp_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_param_group_file_row_number_idx
    ON cts_transactions.ct_param_group (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_param_header_file_trans_type_idx
    ON cts_transactions.ct_param_header (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_param_header_file_record_count_idx
    ON cts_transactions.ct_param_header (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_param_header_aud_type_datetime_idx
    ON cts_transactions.ct_param_header (phd_aud_type, phd_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_param_header_file_row_number_idx
    ON cts_transactions.ct_param_header (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_param_value_file_trans_type_idx
    ON cts_transactions.ct_param_value (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_param_value_file_record_count_idx
    ON cts_transactions.ct_param_value (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_param_value_aud_type_datetime_idx
    ON cts_transactions.ct_param_value (pvl_aud_type, pvl_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_param_value_file_row_number_idx
    ON cts_transactions.ct_param_value (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_param_value_group_file_trans_type_idx
    ON cts_transactions.ct_param_value_group (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_param_value_group_file_record_count_idx
    ON cts_transactions.ct_param_value_group (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_param_value_group_aud_type_datetime_idx
    ON cts_transactions.ct_param_value_group (pvg_aud_type, pvg_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_param_value_group_file_row_number_idx
    ON cts_transactions.ct_param_value_group (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_probity_checks_file_trans_type_idx
    ON cts_transactions.ct_probity_checks (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_probity_checks_file_record_count_idx
    ON cts_transactions.ct_probity_checks (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_probity_checks_aud_type_datetime_idx
    ON cts_transactions.ct_probity_checks (pch_aud_type, pch_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_probity_checks_file_row_number_idx
    ON cts_transactions.ct_probity_checks (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_schemes_file_trans_type_idx
    ON cts_transactions.ct_schemes (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_schemes_file_record_count_idx
    ON cts_transactions.ct_schemes (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_schemes_aud_type_datetime_idx
    ON cts_transactions.ct_schemes (sch_aud_type, sch_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_schemes_file_row_number_idx
    ON cts_transactions.ct_schemes (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_file_trans_type_idx
    ON cts_transactions.ct_sublocation_types (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_file_record_count_idx
    ON cts_transactions.ct_sublocation_types (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_aud_type_datetime_idx
    ON cts_transactions.ct_sublocation_types (slt_aud_type, slt_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_sublocation_types_file_row_number_idx
    ON cts_transactions.ct_sublocation_types (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_file_trans_type_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_file_record_count_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_aud_type_datetime_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (sca_aud_type, sca_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_suspense_char_alloc_rules_file_row_number_idx
    ON cts_transactions.ct_suspense_char_alloc_rules (cts_file_import_id, row_number);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_file_trans_type_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (cts_file_import_id, trans_type);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_file_record_count_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (cts_file_import_id, record_count);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_aud_type_datetime_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (swa_aud_type, swa_aud_datetime);
CREATE INDEX IF NOT EXISTS ct_suspense_wg_alloc_rules_file_row_number_idx
    ON cts_transactions.ct_suspense_wg_alloc_rules (cts_file_import_id, row_number);
