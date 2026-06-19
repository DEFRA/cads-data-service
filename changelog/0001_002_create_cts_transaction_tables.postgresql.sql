-- liquibase formatted sql

-- changeset schema:0001-002-cts-transactions splitStatements:false

create table if not exists cts_transactions.ct_addresses
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    adr_id numeric(12,0) NOT NULL,
    adr_loc_id numeric(12,0),
    adr_par_id numeric(12,0),
    adr_name character varying(35),
    adr_address_2 character varying(35),
    adr_address_3 character varying(35),
    adr_address_4 character varying(35),
    adr_address_5 character varying(35),
    adr_post_code character varying(8),
    adr_current_modified_date date,
    adr_current_status character varying(2),
    adr_current_user character varying(10),
    adr_current_pid numeric(3,0),
    adr_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_addresses_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_animal_changes
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    ach_id numeric(12,0) NOT NULL,
    ach_current_status character varying(2),
    ach_current_user character varying(10),
    ach_current_modified_date date,
    ach_current_pid numeric(3,0),
    ach_ran_id_doc_issued numeric(12,0),
    ach_loc_id_doc_issued numeric(12,0),
    ach_doc_issued_date date,
    ach_passport_version_number character varying(3),
    ach_mov_id_death_cancel numeric(12,0),
    ach_breed_original character varying(5),
    ach_breed_new character varying(5),
    ach_sex_original character(1),
    ach_sex_new character(1),
    ach_birth_date_original date,
    ach_birth_date_new date,
    ach_eartag_original character varying(14),
    ach_eartag_new character varying(14),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_animal_changes_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_animal_claims
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    anc_id numeric(12,0) NOT NULL,
    anc_ran_id numeric(12,0),
    anc_claim_sequence numeric(3,0),
    anc_current_modified_date date,
    anc_current_pid numeric(3,0),
    anc_current_user character varying(10),
    anc_cls_id numeric(12,0),
    anc_clt_id numeric(12,0),
    anc_claim_reference character varying(20),
    anc_retention_start_date date,
    anc_retention_end_date date,
    anc_office character varying(2),
    anc_scheme_year numeric(4,0),
    anc_scheme_modified_datetime date,
    anc_version numeric(6,0),
    anc_current_status character varying(2),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_animal_claims_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_animal_corr_summ_errors
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    ase_id numeric(12,0) NOT NULL,
    ase_acs_id numeric(12,0),
    ase_current_user character varying(10),
    ase_current_status character varying(2),
    ase_current_modified_date date,
    ase_current_pid numeric(12,0),
    ase_attribute_name character varying(30),
    ase_error_code character varying(10),
    ase_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_animal_corr_summ_errors_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_animal_correct_summaries
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    acs_init_initial_loc_ident character varying(30),
    acs_init_initial_subloc_ident character varying(2),
    acs_init_placement_date character varying(30),
    acs_init_previous_eartag character varying(30),
    acs_init_country_of_origin character varying(30),
    acs_init_health_certificate_no character varying(30),
    acs_init_electronic_identifier character varying(30),
    acs_init_import_identifier character varying(50),
    acs_init_number_calf_movts numeric(2,0),
    acs_init_intended_action character varying(60),
    acs_submit_intended_action character varying(60),
    acs_submit_amend_reason character varying(60),
    acs_submit_status character varying(20),
    acs_submit_user character varying(10),
    acs_submit_workgroup character varying(6),
    acs_submit_date date,
    acs_change_received_date date,
    acs_suspense_datetime date,
    acs_amend_retag_ind character varying(1),
    acs_new_eartag_type character varying(20),
    acs_new_eartag character varying(30),
    acs_chr_correction_type character(1),
    acs_chr_location_ind character(1),
    acs_interface_file_name character varying(25),
    acs_interface_file_txn numeric(4,0),
    acs_version numeric(6,0),
    acs_migrated_appsus_key numeric(12,0),
    acs_late_app_letter date,
    acs_request_letter date,
    acs_reminder_letter date,
    acs_refused_letter date,
    acs_id numeric(12,0) NOT NULL,
    acs_current_user character varying(10),
    acs_current_status character varying(2),
    acs_current_modified_date date,
    acs_current_pid numeric(12,0),
    acs_san_or_rap_ind character varying(3),
    acs_san_id numeric(12,0),
    acs_rap_id numeric(12,0),
    acs_ran_id numeric(12,0),
    acs_vap_id numeric(12,0),
    acs_application_type character varying(1),
    acs_source_type character varying(3),
    acs_source_reference character varying(20),
    acs_cts_indicator character varying(1),
    acs_passport_version_no character varying(2),
    acs_init_applic_receipt_date character varying(20),
    acs_init_applic_target_date date,
    acs_init_request_loc_type character varying(2),
    acs_init_request_loc_ident character varying(30),
    acs_init_request_subloc_ident character varying(30),
    acs_init_eartag_type character varying(20),
    acs_init_eartag character varying(30),
    acs_init_breed character varying(20),
    acs_init_birth_date character varying(20),
    acs_init_sex character varying(20),
    acs_init_genetic_dam_et_type character varying(20),
    acs_init_genetic_dam_eartag character varying(30),
    acs_init_surr_dam_et_type character varying(20),
    acs_init_surr_dam_eartag character varying(30),
    acs_init_sire_et_type character varying(20),
    acs_init_sire_eartag character varying(30),
    acs_init_initial_loc_type character varying(2),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_animal_correct_summaries_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_animal_identifiers
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    aid_id numeric(12,0) NOT NULL,
    aid_identifier character varying(50),
    aid_identifier_type character varying(2),
    aid_effective_from_date date,
    aid_effective_to_date date,
    aid_loc_id_assigned numeric(12,0),
    aid_current_flag character varying(1),
    aid_ran_id numeric(12,0),
    aid_etg_id numeric(12,0),
    aid_eid_id numeric(12,0),
    aid_current_user character varying(10),
    aid_current_status character varying(2),
    aid_current_modified_date date,
    aid_current_pid numeric(3,0),
    aid_aid_id_original numeric(12,0),
    aid_aid_id_previous numeric(12,0),
    aid_version numeric(6,0),
    aid_assigned_location_repd character varying(17),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_animal_identifiers_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_animal_relationships
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    aar_current_modified_date date,
    aar_current_pid numeric(3,0),
    aar_version numeric(6,0),
    aar_id numeric(12,0) NOT NULL,
    aar_rel_type character varying(3),
    aar_loc_id numeric(12,0),
    aar_confidence_indicator numeric(1,0),
    aar_effective_from_date date,
    aar_effective_to_date date,
    aar_ran_id_child numeric(12,0),
    aar_ran_id_parent numeric(12,0),
    aar_parent_identifier character varying(20),
    aar_parent_identifier_type character varying(2),
    aar_cancelled_reason character varying(3),
    aar_current_user character varying(10),
    aar_current_status character varying(2),
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_animal_relationships_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_animal_statuses
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    ast_id numeric(12,0) NOT NULL,
    ast_ran_id numeric(12,0),
    ast_status character varying(2),
    ast_user character varying(10),
    ast_modified_date date,
    ast_pid numeric(3,0),
    ast_intended_action character varying(2),
    ast_change_received_date date,
    ast_traced_moves numeric(4,0),
    ast_add_moves numeric(4,0),
    ast_version numeric(6,0),
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_animal_statuses_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_applic_statuses
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    aps_id numeric(12,0) NOT NULL,
    aps_vap_id numeric(12,0),
    aps_user character varying(10),
    aps_status character varying(2),
    aps_modified_date date,
    aps_pid numeric(3,0),
    aps_intended_action character varying(2),
    aps_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_applic_statuses_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_application_late_days
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    ald_id numeric(12,0) NOT NULL,
    ald_valid_days numeric(3,0),
    ald_effective_from_date date,
    ald_application_type character varying(2),
    ald_additional_days_late numeric(3,0),
    ald_current_user character varying(10),
    ald_current_status character varying(2),
    ald_current_pid numeric(3,0),
    ald_current_modified_date date,
    ald_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_application_late_days_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_cla_extract
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    cle_id numeric(12,0) NOT NULL,
    cle_batch_id numeric(12,0),
    cle_run_start date,
    cle_run_end date,
    cle_data_read_start date,
    cle_data_read_end date,
    cle_run_status character varying(1000),
    cle_current_modified_date date,
    cle_bulk_run_stop character varying(1),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_cla_extract_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_cla_extract_detail
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    cld_id numeric(12,0) NOT NULL,
    cld_cle_id numeric(12,0),
    cld_batch_id numeric(12,0),
    cld_table_name character varying(30),
    cld_record_count numeric(12,0),
    cld_run_start date,
    cld_run_end date,
    cld_current_modified_date date,
    cts_file_import_id bigint,
    CONSTRAINT ct_cla_extract_detail_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_cla_extract_dm
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    cle_id numeric(12,0) NOT NULL,
    cle_batch_id numeric(12,0),
    cle_run_start date,
    cle_run_end date,
    cle_data_read_start date,
    cle_data_read_end date,
    cle_run_status character varying(1000),
    cle_current_modified_date date,
    cle_bulk_run_stop character varying(1),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_cla_extract_dm_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_cla_mini_detail
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    cld_id numeric(12,0) NOT NULL,
    cld_cle_id numeric(12,0),
    cld_batch_id numeric(12,0),
    cld_table_name character varying(30),
    cld_record_count numeric(12,0),
    cld_run_start date,
    cld_run_end date,
    cld_current_modified_date date,
    cts_file_import_id bigint,
    CONSTRAINT ct_cla_mini_detail_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_cla_mini_extract
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    cle_id numeric(12,0) NOT NULL,
    cle_batch_id numeric(12,0),
    cle_run_start date,
    cle_run_end date,
    cle_data_read_start date,
    cle_data_read_end date,
    cle_run_status character varying(1000),
    cle_current_modified_date date,
    cle_bulk_run_stop character varying(1),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_cla_mini_extract_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_cm_measures_results
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    cmr_com_id numeric(12,0),
    cmr_result_char character varying(10),
    cmr_measure_char character varying(10),
    cmr_result_num numeric(9,0),
    cmr_measure_num numeric(9,0),
    cmr_current_user character varying(10),
    cmr_current_modified_date date,
    cmr_current_status character varying(2),
    cmr_current_pid numeric(3,0),
    cmr_version numeric(6,0),
    cmr_id numeric(12,0) NOT NULL,
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_cm_measures_results_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_comms_addresses
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    coa_id numeric(12,0) NOT NULL,
    coa_current_status character varying(2),
    coa_current_user character varying(10),
    coa_current_modified_date date,
    coa_pid numeric(3,0),
    coa_email_address character varying(200),
    coa_attachment character(1),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_comms_addresses_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_condition_marker_errors
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    cme_id numeric(12,0) NOT NULL,
    cme_scm_id numeric(12,0),
    cme_attribute_name character varying(30),
    cme_error_code character varying(5),
    cme_current_status character varying(2),
    cme_current_user character varying(10),
    cme_current_modified_date date,
    cme_current_pid numeric(3,0),
    cme_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_condition_marker_errors_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_condition_markers
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    com_id numeric(12,0) NOT NULL,
    com_ran_id numeric(12,0),
    com_cma_id numeric(12,0),
    com_cac_id numeric(12,0),
    com_effective_from_date date,
    com_marker_type character varying(1),
    com_amendment_reason_code character varying(3),
    com_last_used_bud_number numeric(12,0),
    com_autotag_wave_number numeric(2,0),
    com_comments character varying(1000),
    com_amendment_reason_text character varying(60),
    com_effective_to_date date,
    com_loc_id numeric(12,0),
    com_cov_id numeric(12,0),
    com_grouping_reference character varying(16),
    com_branch_number numeric(1,0),
    com_mov_id numeric(12,0),
    com_document_refs character varying(60),
    com_last_probity_date date,
    com_source character varying(2),
    com_current_pid numeric(3,0),
    com_current_status character varying(2),
    com_current_user character varying(10),
    com_current_modified_date date,
    com_version numeric(6,0),
    fake_data numeric(1,0) DEFAULT 0 NOT NULL,
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_condition_markers_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_cps167_report
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    kns_id numeric(12,0) NOT NULL,
    kns_run_date_time date,
    kns_filename character varying(25),
    kns_action_type character varying(5),
    kns_source_directory character varying(200),
    kns_destination_directory character varying(200),
    kns_message character varying(200),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_cps167_report_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_cts_users
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    cus_id numeric(12,0) NOT NULL,
    cus_user_identifier character varying(10),
    cus_colon_flag character varying(1),
    cus_grade character varying(3),
    cus_team_reference character varying(4),
    cus_access_group character varying(3),
    cus_room_name character varying(20),
    cus_email_address character varying(200),
    cus_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_cts_users_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_eartag_staging
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    est_id numeric(12,0) NOT NULL,
    est_eartag character varying(20),
    est_usage_code character varying(2),
    est_identifier_availability character varying(2),
    est_order_location_repd character varying(17),
    est_loc_id_order numeric(12,0),
    est_eartag_reason_code character varying(2),
    est_erf_id numeric(12,0),
    est_current_modified_date date,
    cts_file_import_id bigint,
    CONSTRAINT ct_eartag_staging_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_eartags
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    etg_id numeric(12,0) NOT NULL,
    etg_ett_id numeric(12,0),
    etg_erf_id numeric(12,0),
    etg_eartag character varying(20),
    etg_usage_code character varying(2),
    etg_eartag_authority character varying(2),
    etg_source character varying(2),
    etg_identifier_availability character varying(2),
    etg_species character varying(240),
    etg_fuzzy_eartag_1 character varying(20),
    etg_fuzzy_eartag_2 character varying(20),
    etg_eartag_defra_format character varying(20),
    etg_type_defra_format character varying(10),
    etg_current_user character varying(10),
    etg_current_modified_date date,
    etg_current_status character varying(2),
    etg_current_pid numeric(3,0),
    etg_version numeric(6,0),
    etg_loc_id_order numeric(12,0),
    etg_order_location_repd character varying(17),
    etg_ppaf_indicator character(1),
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_eartags_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_electronic_identifiers
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    eid_id numeric(12,0) NOT NULL,
    eid_electronic_identifier numeric(16,0),
    eid_isa_id numeric(12,0),
    eid_unique_number character varying(12),
    eid_current_status character varying(2),
    eid_current_user character varying(10),
    eid_current_pid numeric(3,0),
    eid_current_modified_date date,
    eid_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_electronic_identifiers_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_email_log
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    eml_id numeric(12,0) NOT NULL,
    eml_sent_datetime date,
    eml_email_addr_recd character varying(70),
    eml_file_name character varying(50),
    eml_received_datetime date,
    eml_send_return_code character varying(100),
    eml_email_addr_sent character varying(70),
    eml_current_user character varying(10),
    eml_current_modified_date date,
    eml_current_pid numeric(3,0),
    eml_current_status character varying(2),
    eml_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_email_log_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_ereport_files
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    ere_id numeric(12,0) NOT NULL,
    ere_file_name character varying(2000),
    ere_file_type character varying(100),
    ere_line_number numeric(12,0),
    ere_record character varying(2000),
    ere_timestamp date,
    cts_file_import_id bigint,
    CONSTRAINT ct_ereport_files_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_ereport_load_messages
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    erm_directory_key character varying(100),
    erm_file_type character varying(100),
    erm_file_prefix character varying(100),
    erm_file_suffix character varying(100),
    erm_sleep_period numeric(10,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_ereport_load_messages_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_ereport_locks
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    erl_file_type character varying(100),
    erl_file_name character varying(2000),
    erl_processed character varying(1),
    erl_timestamp date,
    cts_file_import_id bigint,
    CONSTRAINT ct_ereport_locks_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_ereport_process_messages
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    erq_file_type character varying(3),
    erq_sleep_period numeric(10,0),
    erq_delay_period numeric(10,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_ereport_process_messages_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_ext_cetd_eartag
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    cet_key character varying(20),
    cet_herd character varying(10),
    cet_rsc numeric(3,0),
    cet_date date,
    cet_bsps character varying(6),
    cet_cid character varying(14),
    cet_scps character varying(9),
    cet_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_ext_cetd_eartag_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_insert_update_log
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    iul_id numeric(12,0) NOT NULL,
    iul_system character varying(240),
    iul_table_name character varying(240),
    iul_record_key character varying(240),
    iul_name character varying(25),
    iul_date_processed date,
    iul_date_processed_mis date,
    iul_insert_delete_flag character varying(1),
    iul_current_user character varying(10),
    iul_current_status character varying(2),
    iul_current_modified_date date,
    iul_current_pid numeric(3,0),
    iul_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_insert_update_log_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_issued_documents
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    ido_id numeric(12,0) NOT NULL,
    ido_loc_id numeric(12,0),
    ido_creation_date date,
    ido_reason_code character varying(2),
    ido_interface_file_name character varying(25),
    ido_passpt_layout_ver_number character varying(10),
    ido_interface_txn_number numeric(4,0),
    ido_passport_version_number numeric(3,0),
    ido_current_status character varying(2),
    ido_current_modified_date date,
    ido_current_user character varying(10),
    ido_current_pid numeric(3,0),
    ido_ran_id numeric(12,0),
    ido_version numeric(6,0),
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_issued_documents_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_label_requests
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    lar_id numeric(12,0) NOT NULL,
    lar_las_id numeric(12,0),
    lar_sheet_quantity numeric(10,0),
    lar_label_type character varying(2),
    lar_label_version numeric(6,0),
    lar_submitted_date date,
    lar_requested_date date,
    lar_reason_code character varying(2),
    lar_print_method character varying(2),
    lar_labels_interface_file character varying(25),
    lar_keeper_title character varying(10),
    lar_keeper_initials character varying(12),
    lar_keeper_surname character varying(30),
    lar_label_loc_type character varying(2),
    lar_label_loc_identifier character varying(20),
    lar_label_subloc_identifier character varying(2),
    lar_label_loc_name character varying(35),
    lar_label_address_2 character varying(35),
    lar_label_address_3 character varying(35),
    lar_label_address_4 character varying(35),
    lar_label_address_5 character varying(35),
    lar_label_post_code character varying(8),
    lar_corr_loc_type character varying(2),
    lar_corr_loc_identifier character varying(20),
    lar_corr_subloc_identifier character varying(2),
    lar_corr_title character varying(10),
    lar_corr_initials character varying(12),
    lar_corr_surname character varying(30),
    lar_corr_loc_name character varying(35),
    lar_corr_address_2 character varying(35),
    lar_corr_address_3 character varying(35),
    lar_corr_address_4 character varying(35),
    lar_corr_address_5 character varying(35),
    lar_corr_post_code character varying(8),
    lar_current_user character varying(10),
    lar_current_status character varying(2),
    lar_current_modified_date date,
    lar_current_pid numeric(3,0),
    lar_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_label_requests_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_label_summaries
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    las_id numeric(12,0) NOT NULL,
    las_loc_id_identifying numeric(12,0),
    las_loc_id_labels numeric(12,0),
    las_label_version_number numeric(2,0),
    las_last_submitted_date date,
    las_default_label_type character varying(2),
    las_default_sheet_quantity numeric(4,0),
    las_current_user character varying(10),
    las_current_status character varying(2),
    las_current_modified_date date,
    las_current_pid numeric(3,0),
    las_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_label_summaries_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_letters
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    let_id numeric(12,0) NOT NULL,
    let_type character varying(3),
    let_description character varying(30),
    let_wgp_id numeric(12,0),
    let_program_name character varying(10),
    let_wgp_id_sent numeric(12,0),
    let_current_user character varying(10),
    let_current_status character varying(2),
    let_current_modified_date date,
    let_current_pid numeric(3,0),
    let_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_letters_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_location_identifiers
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    lid_id numeric(12,0) NOT NULL,
    lid_loc_id numeric(12,0),
    lid_effective_from_date date,
    lid_identifier character varying(14),
    lid_full_identifier character varying(17),
    lid_sub_identifier character varying(2),
    lid_effective_to_date date,
    lid_current_status character varying(2),
    lid_current_modified_date date,
    lid_current_user character varying(10),
    lid_current_pid numeric(3,0),
    lid_current_amend_reason character varying(2),
    lid_version numeric(6,0),
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_location_identifiers_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_location_party_rels
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    lpr_id numeric(12,0) NOT NULL,
    lpr_loc_id numeric(12,0),
    lpr_lpt_id numeric(12,0),
    lpr_par_id numeric(12,0),
    lpr_effective_from_date date,
    lpr_effective_to_date date,
    lpr_cessation_reason character varying(3),
    lpr_comments character varying(250),
    lpr_current_user character varying(10),
    lpr_current_modified_date date,
    lpr_current_status character varying(2),
    lpr_current_pid numeric(3,0),
    lpr_version numeric(6,0),
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_location_party_rels_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_location_relationships
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    llr_id numeric(12,0) NOT NULL,
    llr_loc_id_parent numeric(12,0),
    llr_loc_id_child numeric(12,0),
    llr_effective_from_date date,
    llr_cessation_reason character varying(2),
    llr_comments character varying(200),
    llr_lrt_id numeric(12,0),
    llr_effective_to_date date,
    llr_current_status character varying(2),
    llr_current_modified_date date,
    llr_current_user character varying(10),
    llr_current_pid numeric(3,0),
    llr_version numeric(6,0),
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_location_relationships_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_locations
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    loc_receive_ppaf_flag character varying(1),
    loc_id numeric(12,0) NOT NULL,
    loc_slt_id numeric(12,0),
    loc_lty_id numeric(12,0),
    loc_cty_id numeric(12,0),
    loc_receive_labels_flag character varying(1),
    loc_effective_from date,
    loc_effective_to date,
    loc_cessation_reason character varying(2),
    loc_premises_type character varying(4),
    loc_comments character varying(400),
    loc_map_reference character varying(12),
    loc_source_identifier character varying(2),
    loc_source_reference character varying(20),
    loc_tel_number character varying(25),
    loc_mobile_number character varying(25),
    loc_fax_number character varying(25),
    loc_email_address character varying(50),
    loc_current_status character varying(2),
    loc_current_user character varying(10),
    loc_current_modified_date date,
    loc_current_pid numeric(3,0),
    loc_reason_code character varying(2),
    loc_version numeric(6,0),
    fake_data numeric(1,0) DEFAULT 0 NOT NULL,
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_locations_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_locations_faker
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    loc_tel_number text,
    loc_mobile_number text,
    loc_fax_number text,
    loc_email_address text,
    loc_source_reference text,
    loc_comments text,
    cts_file_import_id bigint,
    CONSTRAINT ct_locations_faker_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_locrestrictionstoanimals
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    lra_com_id numeric(12,0),
    lra_last_probity_date date,
    lra_com_effective_from date,
    lra_com_effective_to date,
    lra_loc_id numeric(12,0),
    lra_ran_id numeric(12,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_locrestrictionstoanimals_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_mgt_control_errors
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    mce_id numeric(12,0) NOT NULL,
    mce_ran_id numeric(12,0),
    mce_error_code character varying(5),
    mce_passport_version_issued numeric(4,0),
    mce_number_of_days_late numeric(4,0),
    mce_current_user character varying(10),
    mce_current_status character varying(2),
    mce_current_modified_date date,
    mce_current_pid numeric(3,0),
    mce_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_mgt_control_errors_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_mhs_to_cph
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    cph character varying(14),
    mhs_number numeric(4,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_mhs_to_cph_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_mov_hst
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    hst_ondate date,
    hst_ontype numeric,
    hst_onsource character varying(3),
    hst_offkey numeric,
    hst_offdate date,
    hst_offtype numeric,
    hst_offsource character varying(3),
    hst_pairind character varying(1),
    hst_splitflg character varying(1),
    hst_key numeric,
    hst_lkey numeric,
    hst_onkey numeric,
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_mov_hst_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_movt_corr_summ_errors
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    mse_id numeric(12,0) NOT NULL,
    mse_mcs_id numeric(12,0),
    mse_current_user character varying(10),
    mse_current_status character varying(2),
    mse_current_modified_date date,
    mse_current_pid numeric(12,0),
    mse_attribute_name character varying(30),
    mse_error_code character varying(10),
    mse_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_movt_corr_summ_errors_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_movt_correct_summaries
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    mcs_id numeric(12,0) NOT NULL,
    mcs_current_user character varying(10),
    mcs_current_status character varying(2),
    mcs_current_modified_date date,
    mcs_current_pid numeric(12,0),
    mcs_smo_or_rmo_ind character varying(3),
    mcs_smo_id numeric(12,0),
    mcs_rmo_id numeric(12,0),
    mcs_mov_id numeric(12,0),
    mcs_source_type character varying(3),
    mcs_suspense_datetime date,
    mcs_orig_interface_file_name character varying(25),
    mcs_orig_interface_file_txn numeric(4,0),
    mcs_interface_file_name character varying(25),
    mcs_interface_file_txn numeric(4,0),
    mcs_init_eartag character varying(14),
    mcs_init_loc_type character varying(2),
    mcs_init_loc_identifier character varying(30),
    mcs_init_subloc_identifier character varying(30),
    mcs_init_movement_type character varying(2),
    mcs_init_movement_date character varying(20),
    mcs_init_movement_rcvd_date character varying(20),
    mcs_init_originator character varying(7),
    mcs_init_originators_reference character varying(12),
    mcs_init_eid_reported character varying(20),
    mcs_init_kill_number character varying(20),
    mcs_init_workgroup character varying(6),
    mcs_init_suspense_reason character varying(60),
    mcs_init_purpose_code character varying(1),
    mcs_submit_amendment_reason character varying(60),
    mcs_submit_workgroup character varying(6),
    mcs_submit_user character varying(10),
    mcs_submit_date date,
    mcs_submit_status character varying(20),
    mcs_submit_purpose_code character varying(1),
    mcs_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_movt_correct_summaries_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_parties
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    par_id numeric(12,0) NOT NULL,
    par_initials character varying(12),
    par_surname character varying(30),
    par_title character varying(10),
    par_welsh_indicator character varying(1),
    par_email_address character varying(50),
    par_effective_from_date date,
    par_effective_to_date date,
    par_fax_number character varying(25),
    par_cessation_reason character varying(2),
    par_tel_number character varying(25),
    par_mobile_number character varying(25),
    par_comments character varying(400),
    par_current_user character varying(10),
    par_current_status character varying(2),
    par_current_modified_date date,
    par_current_pid numeric(12,0),
    par_version numeric(6,0),
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_parties_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_parties_faker
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    par_surname text,
    par_initials text,
    par_title text,
    par_tel_number text,
    par_mobile_number text,
    par_email_address text,
    cts_file_import_id bigint,
    CONSTRAINT ct_parties_faker_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_ppaf_groupings
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    ppg_id numeric(12,0) NOT NULL,
    ppg_loc_id_birth numeric(12,0),
    ppg_loc_id_corres numeric(12,0),
    ppg_form_identifier character varying(30),
    ppg_welsh_indicator character varying(1),
    ppg_interface_filename character varying(25),
    ppg_interface_txn_number numeric(10,0),
    ppg_printing_date date,
    ppg_ppaf_added_date date,
    ppg_current_status character varying(2),
    ppg_current_user character varying(10),
    ppg_current_modified_date date,
    ppg_current_pid numeric(3,0),
    ppg_version numeric(6,0),
    ppg_corres_location_repd character varying(17),
    ppg_birth_location_repd character varying(17),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_ppaf_groupings_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_preprinted_appn_forms
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    paf_id numeric(12,0) NOT NULL,
    paf_etg_id numeric(12,0),
    paf_ppg_id numeric(12,0),
    paf_reason_for_issue character varying(1),
    paf_interface_txn_number numeric(10,0),
    paf_interface_filename character varying(25),
    paf_date_issued date,
    paf_current_status character varying(2),
    paf_current_modified_date date,
    paf_current_user character varying(10),
    paf_current_pid numeric(3,0),
    paf_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_preprinted_appn_forms_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_ps9999_ahdb_data
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    ran_id numeric NOT NULL,
    current_cph character varying(14),
    animal_eartag character varying(50),
    birth_date date,
    breed_code character varying(5),
    sex_of_animal character varying(1),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_ps9999_ahdb_data_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_ps9999_ahdb_mov_history
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    ran_id numeric,
    on_date date,
    off_date date,
    loc_id numeric,
    loc_full_identifier character varying(14),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_ps9999_ahdb_mov_history_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_recd_application_errors
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    rae_id numeric(12,0) NOT NULL,
    rae_rap_id numeric(12,0),
    rae_attribute_name character varying(30),
    rae_error_code character varying(4),
    rae_current_status character varying(2),
    rae_current_user character varying(10),
    rae_current_modified_date date,
    rae_current_pid numeric(3,0),
    rae_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_recd_application_errors_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_recd_movement_errors
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    rme_current_status character varying(2),
    rme_current_user character varying(10),
    rme_current_modified_date date,
    rme_current_pid numeric(3,0),
    rme_version numeric(6,0),
    rme_rmo_id numeric(12,0),
    rme_error_code character varying(4),
    rme_attribute_name character varying(30),
    rme_id numeric(12,0) NOT NULL,
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_recd_movement_errors_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_received_applications
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    rap_id numeric(12,0) NOT NULL,
    rap_current_user character varying(10),
    rap_current_status character varying(2),
    rap_current_modified_date date,
    rap_current_pid numeric(3,0),
    rap_application_type character(1),
    rap_applic_receipt_date character varying(20),
    rap_applic_target_date date,
    rap_cts_indicator character(1),
    rap_eartag_type character varying(20),
    rap_eartag character varying(30),
    rap_source_type character varying(3),
    rap_source_reference character varying(20),
    rap_request_loc_type character varying(2),
    rap_request_loc_identifier character varying(30),
    rap_request_subloc_identifier character varying(30),
    rap_genetic_dam_et_type character varying(20),
    rap_genetic_dam_eartag character varying(30),
    rap_surr_dam_et_type character varying(20),
    rap_surr_dam_eartag character varying(30),
    rap_sire_et_type character varying(20),
    rap_sire_eartag character varying(30),
    rap_birth_date character varying(20),
    rap_placement_date character varying(20),
    rap_breed character varying(20),
    rap_sex character varying(20),
    rap_initial_loc_type character varying(2),
    rap_initial_loc_identifier character varying(30),
    rap_initial_subloc_identifier character varying(30),
    rap_country_of_origin character varying(2),
    rap_health_certificate_no character varying(30),
    rap_import_identifier character varying(20),
    rap_electronic_identifier character varying(20),
    rap_new_eartag_type character varying(20),
    rap_new_eartag character varying(30),
    rap_number_calf_movts numeric(1,0),
    rap_wgp_id numeric(12,0),
    rap_interface_file_name character varying(25),
    rap_interface_file_txn numeric(4,0),
    rap_orig_if_file_name character varying(25),
    rap_orig_if_file_txn numeric(4,0),
    rap_chr_correction_type character(1),
    rap_chr_location_ind character(1),
    rap_created_date date,
    rap_intended_action character varying(2),
    rap_amended_by character varying(10),
    rap_amended_datetime date,
    rap_submit_datetime date,
    rap_originator character varying(20),
    rap_ran_id_reserved numeric(12,0),
    rap_version numeric(6,0),
    rap_request_letter date,
    rap_reminder_letter date,
    rap_refused_letter date,
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_received_applications_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_received_movements
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    rmo_id numeric(12,0) NOT NULL,
    rmo_current_user character varying(10),
    rmo_current_status character varying(2),
    rmo_current_modified_date date,
    rmo_current_pid numeric(3,0),
    rmo_source_type character varying(3),
    rmo_suspense_reason character varying(2),
    rmo_direction character varying(1),
    rmo_eartag character varying(20),
    rmo_movement_date character varying(20),
    rmo_movement_type character varying(20),
    rmo_movement_received_date character varying(20),
    rmo_movement_loc_type character varying(20),
    rmo_movement_loc_identifier character varying(30),
    rmo_movement_subloc_identifier character varying(30),
    rmo_loc_full_identifier character varying(20),
    rmo_originator character varying(20),
    rmo_originators_reference character varying(20),
    rmo_kill_number character varying(20),
    rmo_eid_reported character varying(20),
    rmo_movt_workgroup character varying(10),
    rmo_interface_file_name character varying(25),
    rmo_interface_file_txn numeric(4,0),
    rmo_orig_interface_file_name character varying(25),
    rmo_orig_interface_file_txn numeric(4,0),
    rmo_created_date date,
    rmo_submit_datetime date,
    rmo_amended_datetime date,
    rmo_amended_by character varying(10),
    rmo_amendment_reason character varying(2),
    rmo_version numeric(6,0),
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_received_movements_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_registered_animals
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    ran_id numeric(12,0) NOT NULL,
    ran_current_user character varying(10),
    ran_current_status character varying(2),
    ran_current_modified_date date,
    ran_current_pid numeric(3,0),
    ran_current_intended_action character varying(2),
    ran_current_change_rcvd_date date,
    ran_current_traced_moves numeric(4,0),
    ran_current_add_moves numeric(4,0),
    ran_cts_indicator character varying(1),
    ran_passport_or_licence character varying(1),
    ran_sex character varying(1),
    ran_birth_date date,
    ran_applic_line numeric(2,0),
    ran_brd_id numeric(12,0),
    ran_loc_id_passport numeric(12,0),
    ran_vap_id numeric(12,0),
    ran_mov_id_registration numeric(12,0),
    ran_passport_mod_flag character(1),
    ran_passport_version_number character varying(3),
    ran_version numeric(6,0),
    ran_mov_id_death numeric(12,0),
    ran_cry_id_chr_origin numeric(12,0),
    ran_passport_location_repd character varying(17),
    fake_data numeric(1,0) DEFAULT 0 NOT NULL,
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_registered_animals_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_registered_movements
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    mov_id numeric(12,0) NOT NULL,
    mov_current_user character varying(10),
    mov_current_status character varying(2),
    mov_current_modified_date date,
    mov_current_pid numeric(12,0),
    mov_ran_id numeric(12,0),
    mov_loc_id numeric(12,0),
    mov_movement_type character varying(2),
    mov_direction character varying(1),
    mov_movement_date date,
    mov_movement_received_date date,
    mov_version_creation_date date,
    mov_reported_eartag character varying(50),
    mov_source_type character varying(3),
    mov_originator numeric(12,0),
    mov_originators_reference character varying(20),
    mov_kill_number character varying(20),
    mov_eid_reported character varying(20),
    mov_cry_id_import numeric(12,0),
    mov_health_certificate_no character varying(30),
    mov_interface_file_name character varying(25),
    mov_interface_file_txn numeric(4,0),
    mov_orig_interface_file_name character varying(25),
    mov_orig_interface_file_txn numeric(4,0),
    mov_amendment_reason character varying(2),
    mov_amended_by character varying(10),
    mov_suspense_date date,
    mov_probity_report_date date,
    mov_anomaly_check_date date,
    mov_anomaly_code character varying(4),
    mov_infer_movement_rule character varying(4),
    mov_version numeric(6,0),
    mov_location_repd character varying(17),
    fake_data numeric(1,0) DEFAULT 0 NOT NULL,
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_registered_movements_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_reset_to_extract
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    rte_id numeric NOT NULL,
    rte_table_name character varying(50),
    rte_status character varying(1),
    rte_batch numeric(5,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_reset_to_extract_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_sbcs_ext
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    sxt_id character varying(20),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_sbcs_ext_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_stage_files
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    stf_id numeric(12,0) NOT NULL,
    stf_file_name character varying(2000),
    stf_file_type character varying(100),
    stf_line_number numeric(12,0),
    stf_record character varying(2000),
    stf_timestamp date,
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_stage_files_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_stage_locks
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    stl_file_type character varying(100),
    stl_file_name character varying(2000),
    stl_processed character varying(1),
    stl_timestamp date,
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_stage_locks_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_stage_messages
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    stm_directory_key character varying(100),
    stm_file_type character varying(100),
    stm_file_prefix character varying(100),
    stm_file_suffix character varying(100),
    stm_sleep_period numeric(10,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_stage_messages_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_susp_animal_errors
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    sae_id numeric(12,0) NOT NULL,
    sae_san_id numeric(12,0),
    sae_error_code character varying(4),
    sae_attribute_name character varying(30),
    sae_current_modified_date date,
    sae_current_user character varying(10),
    sae_current_status character varying(2),
    sae_current_pid numeric(3,0),
    sae_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_susp_animal_errors_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_susp_cm_measure_results
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    smr_id numeric(12,0) NOT NULL,
    smr_scm_id numeric(12,0),
    smr_measure_char character varying(10),
    smr_result_num numeric(9,0),
    smr_measure_num numeric(9,0),
    smr_result_char character varying(10),
    smr_current_status character varying(2),
    smr_current_modified_date date,
    smr_current_user character varying(10),
    smr_current_pid numeric(3,0),
    smr_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_susp_cm_measure_results_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_susp_condition_markers
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    scm_id numeric(12,0) NOT NULL,
    scm_ran_id numeric(12,0),
    scm_loc_id numeric(12,0),
    scm_location_type character varying(2),
    scm_submit_date date,
    scm_amendment_datetime date,
    scm_amendment_reason character varying(2),
    scm_amendment_reason_text character varying(80),
    scm_amendment_status character varying(10),
    scm_original_interface_txn numeric(4,0),
    scm_condition_code character varying(20),
    scm_document_refs character varying(60),
    scm_effective_from_date date,
    scm_location_identifier character varying(15),
    scm_comments character varying(500),
    scm_condition_variant character varying(20),
    scm_effective_to_date date,
    scm_suspense_reason character varying(2),
    scm_source character varying(2),
    scm_originator character varying(30),
    scm_condition_authority character varying(20),
    scm_current_purpose_code character varying(10),
    scm_grouping_reference character varying(16),
    scm_original_interface_file character varying(25),
    scm_cancellation_date date,
    scm_condition_type character varying(10),
    scm_interface_txn_number numeric(4,0),
    scm_interface_filename character varying(25),
    scm_add_match_flag character varying(3),
    scm_owner character varying(3),
    scm_amended_by character varying(30),
    scm_animal_identifier character varying(14),
    scm_animal_identifier_type character varying(2),
    scm_condition_activity character varying(20),
    scm_sublocation_identifier character varying(2),
    scm_use_type character varying(1),
    scm_system_error character varying(10),
    scm_current_status character varying(2),
    scm_current_modified_date date,
    scm_current_user character varying(10),
    scm_current_pid numeric(3,0),
    scm_version numeric(6,0),
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_susp_condition_markers_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_susp_movement_errors
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    sme_smo_id numeric(12,0),
    sme_id numeric(12,0) NOT NULL,
    sme_attribute_name character varying(30),
    sme_error_code character varying(4),
    sme_current_user character varying(10),
    sme_current_modified_date date,
    sme_current_status character varying(2),
    sme_current_pid numeric(3,0),
    sme_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_susp_movement_errors_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_suspended_animals
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    san_vap_id numeric(12,0),
    san_wgp_id numeric(12,0),
    san_application_type character(1),
    san_cts_indicator character(1),
    san_applic_receipt_date date,
    san_suspense_date date,
    san_eartag character varying(30),
    san_intended_action character varying(2),
    san_passport_version_number character varying(3),
    san_amended_by character varying(10),
    san_amended_datetime date,
    san_sex character(1),
    san_breed character varying(20),
    san_birth_date date,
    san_placement_date date,
    san_loc_id_initial numeric(12,0),
    san_eartag_type character varying(20),
    san_genetic_dam_et_type character varying(20),
    san_genetic_dam_eartag character varying(30),
    san_surr_dam_et_type character varying(20),
    san_surr_dam_eartag character varying(30),
    san_sire_et_type character varying(20),
    san_sire_eartag character varying(30),
    san_electronic_identifier character varying(30),
    san_country_of_origin character varying(2),
    san_health_certificate_no character varying(30),
    san_import_identifier character varying(20),
    san_number_calf_movts numeric(1,0),
    san_chr_location_ind character(1),
    san_chr_correction_type character(1),
    san_change_received_date date,
    san_amend_reason character varying(2),
    san_submit_datetime date,
    san_loc_id_request numeric(12,0),
    san_amend_retag_ind character(1),
    san_new_eartag_type character varying(20),
    san_new_eartag character varying(30),
    san_source_type character varying(3),
    san_source_reference character varying(20),
    san_interface_file_name character varying(25),
    san_interface_file_txn numeric(4,0),
    san_orig_if_file_name character varying(25),
    san_orig_if_file_txn numeric(4,0),
    san_applic_target_date date,
    san_originator character varying(20),
    san_version numeric(6,0),
    san_initial_location_repd character varying(17),
    san_request_location_repd character varying(17),
    san_late_app_letter date,
    san_id numeric(12,0) NOT NULL,
    san_current_user character varying(10),
    san_current_status character varying(2),
    san_current_modified_date date,
    san_current_pid numeric(3,0),
    san_ran_id numeric(12,0),
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_suspended_animals_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_suspended_movements
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    smo_id numeric(12,0) NOT NULL,
    smo_current_user character varying(10),
    smo_current_status character varying(2),
    smo_current_modified_date date,
    smo_current_pid numeric(3,0),
    smo_source_type character varying(3),
    smo_movement_type numeric(2,0),
    smo_movement_date date,
    smo_movement_received_date date,
    smo_eartag character varying(14),
    smo_originator character varying(7),
    smo_suspense_date date,
    smo_direction character varying(1),
    smo_movement_loc_type character varying(2),
    smo_movement_loc_identifier character varying(30),
    smo_movement_subloc_identifier character varying(30),
    smo_originators_reference character varying(12),
    smo_kill_number character varying(20),
    smo_eid_reported character varying(20),
    smo_movt_workgroup character varying(10),
    smo_suspense_reason character varying(2),
    smo_current_purpose_code character varying(1),
    smo_interface_file_name character varying(25),
    smo_interface_file_txn numeric(4,0),
    smo_orig_interface_file_name character varying(25),
    smo_orig_interface_file_txn numeric(4,0),
    smo_submit_datetime date,
    smo_amended_by character varying(10),
    smo_amended_datetime date,
    smo_amendment_reason character varying(2),
    smo_version numeric(6,0),
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_suspended_movements_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_valid_applications
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    vap_id numeric(12,0) NOT NULL,
    vap_current_status character varying(2),
    vap_current_user character varying(10),
    vap_current_modified_date date,
    vap_current_pid numeric(3,0),
    vap_current_intended_action character varying(2),
    vap_application_type character(1),
    vap_receipt_date date,
    vap_loc_id_requester numeric(12,0),
    vap_requester_date date,
    vap_county_requester character varying(2),
    vap_source_type character varying(3),
    vap_target_date date,
    vap_source_reference character varying(20),
    vap_cts_indicator character(1),
    vap_no_of_animals numeric(3,0),
    vap_no_of_animals_not_canc numeric(3,0),
    vap_number_calf_movts numeric(2,0),
    vap_interface_file_name character varying(25),
    vap_interface_file_txn numeric(4,0),
    vap_wur_id numeric(12,0),
    vap_version numeric(6,0),
    vap_requester_location_repd character varying(17),
    fake_data numeric(1,0) DEFAULT 0 NOT NULL,
    row_number numeric,
    record_type character varying(1),
    record_count numeric,
    imported_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cts_file_import_id bigint,
    CONSTRAINT ct_valid_applications_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_web_users
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    wur_current_pid numeric(3,0),
    wur_version numeric(6,0),
    wur_id numeric(12,0) NOT NULL,
    wur_access_number character varying(12),
    wur_bad_login_reset_count numeric(3,0),
    wur_bad_login_per_day_count numeric(3,0),
    wur_password_issue_flag character varying(1),
    wur_user_type character varying(2),
    wur_lpr_id_keeper numeric(12,0),
    wur_encrypted_password character varying(30),
    wur_staff_number character varying(7),
    wur_welsh_indicator character varying(1),
    wur_issued_to_identifier character varying(30),
    wur_security_filename character varying(20),
    wur_mobile_number character varying(30),
    wur_telephone_number character varying(30),
    wur_user_name character varying(60),
    wur_user_location character varying(35),
    wur_address_2 character varying(35),
    wur_address_3 character varying(35),
    wur_address_4 character varying(35),
    wur_address_5 character varying(35),
    wur_post_code character varying(10),
    wur_email_address character varying(100),
    wur_expiry_date date,
    wur_password_filename character varying(20),
    wur_current_user character varying(10),
    wur_current_status character varying(2),
    wur_current_modified_date date,
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_web_users_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_wg_autoallocations
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    wga_id numeric(12,0) NOT NULL,
    wga_rou_id numeric(12,0),
    wga_wgp_id numeric(12,0),
    wga_allocation character varying(10),
    wga_assignment character varying(10),
    wga_current_user character varying(10),
    wga_current_pid numeric(3,0),
    wga_current_status character varying(2),
    wga_current_modified_date date,
    wga_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_wg_autoallocations_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_wg_super_assignments
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    wsa_id numeric(12,0) NOT NULL,
    wsa_wgp_id_current numeric(12,0),
    wsa_wgp_id_assigned numeric(12,0),
    wsa_rou_id numeric(12,0),
    wsa_current_user character varying(10),
    wsa_current_status character varying(2),
    wsa_current_modified_date date,
    wsa_current_pid numeric(3,0),
    wsa_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_wg_super_assignments_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_wg_user_assignments
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    wua_id numeric(12,0) NOT NULL,
    wua_cus_id numeric(12,0),
    wua_wgp_id numeric(12,0),
    wua_wg_contact_ind character(1),
    wua_favoured_wg_ind character(1),
    wua_current_user character varying(10),
    wua_current_status character varying(2),
    wua_current_modified_date date,
    wua_current_pid numeric(3,0),
    wua_version numeric(6,0),
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_wg_user_assignments_pkey PRIMARY KEY (trans_id)
);

create table if not exists cts_transactions.ct_workgroups
(
    trans_id bigint GENERATED ALWAYS AS IDENTITY (
        START WITH 1
        INCREMENT BY 1
        NO MINVALUE
        NO MAXVALUE
        CACHE 1
    ) NOT NULL,
    trans_type text NOT NULL,
    wgp_id numeric(12,0) NOT NULL,
    wgp_workgroup character varying(6),
    wgp_short_name character varying(20),
    wgp_long_name character varying(60),
    wgp_active_indicator character(1),
    wgp_printer character varying(50),
    wgp_summary_type character varying(2),
    wgp_reassign_lock character(1),
    wgp_current_status character varying(2),
    wgp_current_modified_date date,
    wgp_current_user character varying(10),
    wgp_current_pid numeric(3,0),
    wgp_version numeric(6,0),
    fake_data numeric(1,0) DEFAULT 0 NOT NULL,
    row_number numeric,
    cts_file_import_id bigint,
    CONSTRAINT ct_workgroups_pkey PRIMARY KEY (trans_id)
);

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
