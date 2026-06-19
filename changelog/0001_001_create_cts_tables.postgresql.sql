-- liquibase formatted sql

-- changeset schema:0001-001-cts splitStatements:false

create table if not exists cts.ct_alloc_routines
(
    rou_id                    numeric(12) not null
        primary key,
    rou_routine               varchar(6),
    rou_allocation_type       varchar(1),
    rou_long_description      varchar(40),
    rou_current_user          varchar(10),
    rou_current_status        varchar(2),
    rou_current_modified_date date,
    rou_current_pid           numeric(3),
    rou_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_application_late_days
(
    ald_id                    numeric(12) not null
        primary key,
    ald_valid_days            numeric(3),
    ald_effective_from_date   date,
    ald_application_type      varchar(2),
    ald_additional_days_late  numeric(3),
    ald_current_user          varchar(10),
    ald_current_status        varchar(2),
    ald_current_pid           numeric(3),
    ald_current_modified_date date,
    ald_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_batch_retention_conf
(
    brt_item_id        varchar(50),
    brt_retention_days numeric(4),
    brt_description    varchar(200),
    row_number         numeric
);

create table if not exists cts.ct_breeds
(
    brd_id                    numeric(12) not null
        primary key,
    brd_code                  varchar(5),
    brd_type                  varchar(2),
    brd_long_description      varchar(60),
    brd_scheme_eligibility    varchar(10),
    brd_short_description     varchar(20),
    brd_current_user          varchar(10),
    brd_current_status        varchar(2),
    brd_current_pid           numeric(3),
    brd_current_modified_date date,
    brd_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_cla_extract
(
    cle_id                    numeric(12) not null
        primary key,
    cle_batch_id              numeric(12),
    cle_run_start             date,
    cle_run_end               date,
    cle_data_read_start       date,
    cle_data_read_end         date,
    cle_run_status            varchar(1000),
    cle_current_modified_date date,
    cle_bulk_run_stop         varchar(1),
    row_number                numeric
);

create table if not exists cts.ct_cla_extract_detail
(
    cld_id                    numeric(12) not null
        primary key,
    cld_cle_id                numeric(12)
        constraint fk_ct_cla_extract_detail_cld_cle_id
            references cts.ct_cla_extract,
    cld_batch_id              numeric(12),
    cld_table_name            varchar(30),
    cld_record_count          numeric(12),
    cld_run_start             date,
    cld_run_end               date,
    cld_current_modified_date date
);

create table if not exists cts.ct_cla_extract_dm
(
    cle_id                    numeric(12) not null
        primary key,
    cle_batch_id              numeric(12),
    cle_run_start             date,
    cle_run_end               date,
    cle_data_read_start       date,
    cle_data_read_end         date,
    cle_run_status            varchar(1000),
    cle_current_modified_date date,
    cle_bulk_run_stop         varchar(1),
    row_number                numeric
);

create table if not exists cts.ct_cla_mini_detail
(
    cld_id                    numeric(12) not null
        primary key,
    cld_cle_id                numeric(12)
        constraint fk_ct_cla_mini_detail_cld_cle_id
            references cts.ct_cla_extract,
    cld_batch_id              numeric(12),
    cld_table_name            varchar(30),
    cld_record_count          numeric(12),
    cld_run_start             date,
    cld_run_end               date,
    cld_current_modified_date date
);

create table if not exists cts.ct_cla_mini_extract
(
    cle_id                    numeric(12) not null
        primary key,
    cle_batch_id              numeric(12),
    cle_run_start             date,
    cle_run_end               date,
    cle_data_read_start       date,
    cle_data_read_end         date,
    cle_run_status            varchar(1000),
    cle_current_modified_date date,
    cle_bulk_run_stop         varchar(1),
    row_number                numeric
);

create table if not exists cts.ct_claim_statuses
(
    cls_id                    numeric(12) not null
        primary key,
    cls_current_pid           numeric(3),
    cls_current_status        varchar(2),
    cls_current_user          varchar(10),
    cls_current_modified_date date,
    cls_claim_status          varchar(2),
    cls_description           varchar(240),
    cls_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_comms_addresses
(
    coa_id                    numeric(12) not null
        primary key,
    coa_current_status        varchar(2),
    coa_current_user          varchar(10),
    coa_current_modified_date date,
    coa_pid                   numeric(3),
    coa_email_address         varchar(200),
    coa_attachment            char,
    row_number                numeric
);

create table if not exists cts.ct_condition_types
(
    cot_id                    numeric(12) not null
        primary key,
    cot_condition_type        varchar(5),
    cot_short_description     varchar(20),
    cot_effective_from_date   date,
    cot_long_description      varchar(60),
    cot_effective_to_date     date,
    cot_cessation_reason      varchar(3),
    cot_access_group          varchar(10),
    cot_current_user          varchar(10),
    cot_current_status        varchar(2),
    cot_current_modified_date date,
    cot_current_pid           numeric(3),
    cot_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_cm_authorities
(
    cma_id                    numeric(12) not null
        primary key,
    cma_cot_id                numeric(12)
        constraint fk_ct_cm_authorities_cma_cot_id
            references cts.ct_condition_types,
    cma_authority_code        varchar(10),
    cma_short_name            varchar(30),
    cma_long_name             varchar(60),
    cma_current_pid           numeric(3),
    cma_current_status        varchar(2),
    cma_current_modified_date date,
    cma_current_user          varchar(10),
    cma_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_counties
(
    cty_current_pid           numeric(3),
    cty_version               numeric(6),
    cty_id                    numeric(12) not null
        primary key,
    cty_code                  varchar(2),
    cty_name                  varchar(25),
    cty_uk_area               varchar(1),
    cty_vet_area              varchar(3),
    cty_passport_area         varchar(3),
    cty_admin_office          varchar(2),
    cty_bcms_team             varchar(10),
    cty_inspection_area       varchar(2),
    cty_data_mgt_area         varchar(3),
    cty_current_user          varchar(10),
    cty_current_status        varchar(2),
    cty_current_modified_date date,
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_counties_migration
(
    cty_id                    numeric(12) not null
        primary key,
    cty_code                  varchar(2),
    cty_name                  varchar(25),
    cty_uk_area               varchar(1),
    cty_vet_area              varchar(3),
    cty_passport_area         varchar(3),
    cty_admin_office          varchar(2),
    cty_bcms_team             varchar(10),
    cty_inspection_area       varchar(2),
    cty_data_mgt_area         varchar(3),
    cty_current_user          varchar(10),
    cty_current_status        varchar(2),
    cty_current_modified_date date,
    cty_current_pid           numeric(3),
    cty_version               numeric(6),
    cty_due_for_migration     varchar(1),
    cty_date_migrated         date,
    cty_print_passports       varchar(1),
    row_number                numeric
);

create table if not exists cts.ct_countries
(
    cry_id                    numeric(12) not null
        primary key,
    cry_code                  varchar(2),
    cry_name                  varchar(25),
    cry_eu_member             varchar(1),
    cry_import_export         varchar(1),
    cry_cry_id_main_eu        numeric(12),
    cry_back_capture          varchar(1),
    cry_current_user          varchar(10),
    cry_current_status        varchar(2),
    cry_current_modified_date date,
    cry_current_pid           numeric(3),
    cry_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

alter table cts.ct_countries
    add constraint fk_ct_countries_cry_cry_id_main_eu
        foreign key (cry_cry_id_main_eu) references cts.ct_countries;

create table if not exists cts.ct_cps167_report
(
    kns_id                    numeric(12) not null
        primary key,
    kns_run_date_time         date,
    kns_filename              varchar(25),
    kns_action_type           varchar(5),
    kns_source_directory      varchar(200),
    kns_destination_directory varchar(200),
    kns_message               varchar(200),
    row_number                numeric
);

create table if not exists cts.ct_cts164_handshake_file_keys
(
    bjk_batch_id      numeric(12),
    bjk_group_id      numeric(12),
    bjk_filetype      varchar(30),
    bjk_key           varchar(30),
    bjk_modified_date date
);

create table if not exists cts.ct_cts_users
(
    cus_id              numeric(12) not null
        primary key,
    cus_user_identifier varchar(10),
    cus_colon_flag      varchar(1),
    cus_grade           varchar(3),
    cus_team_reference  varchar(4),
    cus_access_group    varchar(3),
    cus_room_name       varchar(20),
    cus_email_address   varchar(200),
    cus_version         numeric(6),
    row_number          numeric
);

create table if not exists cts.ct_eartag_formats
(
    etf_id                    numeric(12) not null
        primary key,
    etf_description           varchar(60),
    etf_format_pattern        varchar(30),
    etf_max_input_length      numeric(3),
    etf_extra_chars_allowed   varchar(30),
    etf_current_user          varchar(10),
    etf_current_status        varchar(2),
    etf_current_modified_date date,
    etf_current_pid           numeric(3),
    etf_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_eartag_reasons
(
    etr_id                    numeric(12) not null
        primary key,
    etr_eartag_reason_code    varchar(2),
    etr_reason_code_type      varchar(1),
    etr_short_description     varchar(20),
    etr_long_description      varchar(60),
    etr_current_status        varchar(2),
    etr_current_user          varchar(10),
    etr_current_modified_date date,
    etr_current_pid           numeric(3),
    etr_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_eartag_reason_flags
(
    erf_id                        numeric(12) not null
        primary key,
    erf_eartag_authority          varchar(2),
    erf_etr_id                    numeric(12)
        constraint fk_ct_eartag_reason_flags_erf_etr_id
            references cts.ct_eartag_reasons,
    erf_manual_entry_default_ind  numeric(1),
    erf_manual_deletion_ind       numeric(1),
    erf_batch_update_amend_flag   numeric(1),
    erf_cts_animal_reg_flag       numeric(1),
    erf_manual_override           numeric(1),
    erf_cts_gen_surr_sire_allowed numeric(1),
    erf_manual_entry_ind          numeric(1),
    erf_backcapture_regn_flag     numeric(1),
    erf_manual_update_flag        numeric(1),
    erf_current_status            varchar(2),
    erf_current_user              varchar(10),
    erf_current_modified_date     date,
    erf_current_pid               numeric(3),
    erf_version                   numeric(6),
    fake_data                     numeric(1) default 0 not null,
    row_number                    numeric
);

create table if not exists cts.ct_eartag_types
(
    ett_id                    numeric(12) not null
        primary key,
    ett_eartag_type           varchar(2),
    ett_cr_export             varchar(1),
    ett_description           varchar(60),
    ett_etf_id                numeric(12)
        constraint fk_ct_eartag_types_ett_etf_id
            references cts.ct_eartag_formats,
    ett_short_description     varchar(20),
    ett_current_user          varchar(10),
    ett_current_status        varchar(2),
    ett_current_modified_date date,
    ett_current_pid           numeric(3),
    ett_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_email_log
(
    eml_id                    numeric(12) not null
        primary key,
    eml_sent_datetime         date,
    eml_email_addr_recd       varchar(70),
    eml_file_name             varchar(50),
    eml_received_datetime     date,
    eml_send_return_code      varchar(100),
    eml_email_addr_sent       varchar(70),
    eml_current_user          varchar(10),
    eml_current_modified_date date,
    eml_current_pid           numeric(3),
    eml_current_status        varchar(2),
    eml_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_ereport_files
(
    ere_id          numeric(12) not null
        primary key,
    ere_file_name   varchar(2000),
    ere_file_type   varchar(100),
    ere_line_number numeric(12),
    ere_record      varchar(2000),
    ere_timestamp   date
);

create table if not exists cts.ct_ereport_load_messages
(
    erm_directory_key varchar(100),
    erm_file_type     varchar(100),
    erm_file_prefix   varchar(100),
    erm_file_suffix   varchar(100),
    erm_sleep_period  numeric(10),
    row_number        numeric
);

create table if not exists cts.ct_ereport_locks
(
    erl_file_type varchar(100),
    erl_file_name varchar(2000),
    erl_processed varchar(1),
    erl_timestamp date
);

create table if not exists cts.ct_ereport_process_messages
(
    erq_file_type    varchar(3),
    erq_sleep_period numeric(10),
    erq_delay_period numeric(10),
    row_number       numeric
);

create table if not exists cts.ct_ext_cetd_eartag
(
    cet_key     varchar(20),
    cet_herd    varchar(10),
    cet_rsc     numeric(3),
    cet_date    date,
    cet_bsps    varchar(6),
    cet_cid     varchar(14),
    cet_scps    varchar(9),
    cet_version numeric(6),
    row_number  numeric
);

create table if not exists cts.ct_ext_ni_district
(
    nid_electoral_district varchar(16),
    nid_version            numeric(6),
    nid_herd_code          varchar(2),
    row_number             numeric
);

create table if not exists cts.ct_ext_special_herd
(
    sph_herd_code   varchar(10),
    sph_herd_region varchar(30),
    sph_version     numeric(6),
    row_number      numeric
);

create table if not exists cts.ct_file_layouts
(
    flt_id                numeric,
    flt_process_name      varchar(20),
    flt_record_type       varchar(1),
    flt_element_name      varchar(30),
    flt_element_desc      varchar(50),
    flt_element_index     numeric,
    flt_element_tests     varchar(10),
    flt_data_type         varchar(8),
    flt_data_length       numeric,
    flt_data_precision    numeric,
    flt_file_format       varchar(30),
    flt_conversion_format varchar(30),
    flt_unidata_name      varchar(10),
    row_number            numeric
);

create table if not exists cts.ct_hsf_sequences
(
    hss_sequence_key varchar(20),
    hss_sequence     numeric(12),
    row_number       numeric
);

create table if not exists cts.ct_insert_update_log
(
    iul_id                    numeric(12) not null
        primary key,
    iul_system                varchar(240),
    iul_table_name            varchar(240),
    iul_record_key            varchar(240),
    iul_name                  varchar(25),
    iul_date_processed        date,
    iul_date_processed_mis    date,
    iul_insert_delete_flag    varchar(1),
    iul_current_user          varchar(10),
    iul_current_status        varchar(2),
    iul_current_modified_date date,
    iul_current_pid           numeric(3),
    iul_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_issuing_authorities
(
    isa_id                    numeric(12) not null
        primary key,
    isa_country_name          varchar(240),
    isa_manufacturers_name    varchar(240),
    isa_type                  varchar(10),
    isa_current_status        varchar(2),
    isa_current_user          varchar(10),
    isa_current_modified_date date,
    isa_current_pid           numeric(3),
    isa_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_electronic_identifiers
(
    eid_id                    numeric(12) not null
        primary key,
    eid_electronic_identifier numeric(16),
    eid_isa_id                numeric(12)
        constraint fk_ct_electronic_identifiers_eid_isa_id
            references cts.ct_issuing_authorities,
    eid_unique_number         varchar(12),
    eid_current_status        varchar(2),
    eid_current_user          varchar(10),
    eid_current_pid           numeric(3),
    eid_current_modified_date date,
    eid_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_late_days
(
    lda_version               numeric(6),
    lda_id                    numeric(12) not null
        primary key,
    lda_applic_type           char,
    lda_start_date            date,
    lda_valid_days            numeric(4),
    lda_current_user          varchar(10),
    lda_current_modified_date date,
    lda_current_pid           numeric(3),
    lda_current_status        varchar(2),
    row_number                numeric
);

create table if not exists cts.ct_location_id_formats
(
    lif_version               numeric(6),
    lif_id                    numeric(12) not null
        primary key,
    lif_subloc_type_reqd      varchar(1),
    lif_description           varchar(30),
    lif_loc_type_reqd         varchar(1),
    lif_format_pattern        varchar(15),
    lif_current_user          varchar(10),
    lif_current_status        varchar(2),
    lif_current_modified_date date,
    lif_current_pid           numeric(3),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_location_party_rel_types
(
    lpt_id                    numeric(12) not null
        primary key,
    lpt_code                  varchar(2),
    lpt_description           varchar(60),
    lpt_gaps_allowed          char,
    lpt_mandatory             char,
    lpt_primary_single_link   char,
    lpt_second_single_link    char,
    lpt_hierarchical_link     char,
    lpt_relship_text_down     varchar(30),
    lpt_relship_text_up       varchar(30),
    lpt_current_user          varchar(10),
    lpt_current_status        varchar(2),
    lpt_current_modified_date date,
    lpt_current_pid           numeric(3),
    lpt_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_location_rel_types
(
    lrt_id                    numeric(12) not null
        primary key,
    lrt_code                  varchar(2),
    lrt_description           varchar(30),
    lrt_second_single_link    varchar(1),
    lrt_mandatory             varchar(1),
    lrt_gaps_allowed          varchar(1),
    lrt_primary_single_link   varchar(1),
    lrt_hierarchical_link     varchar(1),
    lrt_relship_text_down     varchar(30),
    lrt_relship_text_up       varchar(30),
    lrt_current_modified_date date,
    lrt_current_status        varchar(2),
    lrt_current_user          varchar(10),
    lrt_current_pid           numeric(3),
    lrt_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_location_types
(
    lty_cii_location_type     varchar(1),
    lty_ownership             varchar(2),
    lty_current_status        varchar(2),
    lty_current_modified_date date,
    lty_current_user          varchar(10),
    lty_current_pid           numeric(3),
    lty_version               numeric(6),
    lty_id                    numeric(12) not null
        primary key,
    lty_loc_type              varchar(2),
    lty_lif_id                numeric(12)
        constraint fk_ct_location_types_lty_lif_id
            references cts.ct_location_id_formats,
    lty_location_type_reqd    numeric(1),
    lty_short_description     varchar(20),
    lty_subloc_type_reqd      numeric(1),
    lty_long_description      varchar(60),
    lty_premises_group        varchar(2),
    lty_hier_link_permitted   varchar(1),
    lty_movement_loc_ind      varchar(1),
    lty_peer_link_permitted   varchar(1),
    lty_perform_anomaly_check varchar(1),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_loc_type_rel_combs
(
    lrc_id                    numeric(12) not null
        primary key,
    lrc_lty_id_1              numeric(12)
        constraint fk_ct_loc_type_rel_combs_lrc_lty_id_1
            references cts.ct_location_types,
    lrc_lty_id_2              numeric(12)
        constraint fk_ct_loc_type_rel_combs_lrc_lty_id_2
            references cts.ct_location_types,
    lrc_lrt_id                numeric(12)
        constraint fk_ct_loc_type_rel_combs_lrc_lrt_id
            references cts.ct_location_rel_types,
    lrc_current_user          varchar(10),
    lrc_current_modified_date date,
    lrc_current_status        varchar(2),
    lrc_current_pid           numeric(3),
    lrc_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_mgt_wg_allocation_rules
(
    war_id                    numeric(12) not null
        primary key,
    war_rou_id                numeric(12)
        constraint fk_ct_mgt_wg_allocation_rules_war_rou_id
            references cts.ct_alloc_routines,
    war_priority              numeric(3),
    war_suspense_type         varchar(1),
    war_rule                  varchar(100),
    war_rule_formula          varchar(100),
    war_current_user          varchar(10),
    war_current_status        varchar(2),
    war_current_modified_date date,
    war_current_pid           numeric(3),
    war_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_mhs_to_cph
(
    cph        varchar(14),
    mhs_number numeric(4),
    row_number numeric
);

create table if not exists cts.ct_mov_hst
(
    hst_ondate    date,
    hst_ontype    numeric,
    hst_onsource  varchar(3),
    hst_offkey    numeric,
    hst_offdate   date,
    hst_offtype   numeric,
    hst_offsource varchar(3),
    hst_pairind   varchar(1),
    hst_splitflg  varchar(1),
    hst_key       numeric,
    hst_lkey      numeric,
    hst_onkey     numeric,
    row_number    numeric
);

create table if not exists cts.ct_msgtxt
(
    msg_id     varchar(5),
    msg_text   varchar(1000),
    row_number numeric
);

create table if not exists cts.ct_non_working_days
(
    nwd_id                    numeric(12) not null
        primary key,
    nwd_date                  date,
    nwd_description           varchar(50),
    nwd_year                  numeric(4),
    nwd_current_user          varchar(10),
    nwd_current_status        varchar(2),
    nwd_current_modified_date date,
    nwd_pid                   numeric(3),
    nwd_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_param_header
(
    phd_id                    numeric(12) not null
        primary key,
    phd_param                 varchar(30),
    phd_short_desc            varchar(20),
    phd_long_desc             varchar(60),
    phd_dont_cache            char,
    phd_use_short             char,
    phd_current_user          varchar(10),
    phd_current_status        varchar(2),
    phd_current_modified_date date,
    phd_current_pid           numeric(3),
    phd_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_param_group
(
    pgp_version               numeric(6),
    pgp_id                    numeric(12) not null
        primary key,
    pgp_param                 varchar(30),
    pgp_group_value           varchar(15),
    pgp_phd_id                numeric(12)
        constraint fk_ct_param_group_pgp_phd_id
            references cts.ct_param_header,
    pgp_short_desc            varchar(30),
    pgp_long_desc             varchar(100),
    pgp_current_user          varchar(10),
    pgp_current_status        varchar(2),
    pgp_current_modified_date date,
    pgp_current_pid           numeric(3),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_param_value
(
    pvl_id                    numeric(12) not null
        primary key,
    pvl_param                 varchar(30),
    pvl_phd_id                numeric(12)
        constraint fk_ct_param_value_pvl_phd_id
            references cts.ct_param_header,
    pvl_param_value           varchar(30),
    pvl_param_short_desc      varchar(20),
    pvl_param_long_desc       varchar(100),
    pvl_sequence              numeric(4),
    pvl_current_user          varchar(10),
    pvl_current_status        varchar(2),
    pvl_current_modified_date date,
    pvl_current_pid           numeric(3),
    pvl_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_param_value_group
(
    pvg_id                    numeric(12) not null
        primary key,
    pvg_pgp_id                numeric(12)
        constraint fk_ct_param_value_group_pvg_pgp_id
            references cts.ct_param_group,
    pvg_pvl_id                numeric(12)
        constraint fk_ct_param_value_group_pvg_pvl_id
            references cts.ct_param_value,
    pvg_group_value           varchar(15),
    pvg_param                 varchar(30),
    pvg_param_value           varchar(30),
    pvg_current_user          varchar(10),
    pvg_current_status        varchar(2),
    pvg_current_modified_date date,
    pvg_current_pid           numeric(3),
    pvg_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_parties
(
    par_id                    numeric(12) not null
        primary key,
    par_initials              varchar(12),
    par_surname               varchar(30),
    par_title                 varchar(10),
    par_welsh_indicator       varchar(1),
    par_email_address         varchar(50),
    par_effective_from_date   date,
    par_effective_to_date     date,
    par_fax_number            varchar(25),
    par_cessation_reason      varchar(2),
    par_tel_number            varchar(25),
    par_mobile_number         varchar(25),
    par_comments              varchar(400),
    par_current_user          varchar(10),
    par_current_status        varchar(2),
    par_current_modified_date date,
    par_current_pid           numeric(12),
    par_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_probity_checks
(
    pch_id                    numeric(12) not null
        primary key,
    pch_long_description      varchar(60),
    pch_short_description     varchar(20),
    pch_checked_to_date       date,
    pch_check_period          numeric(3),
    pch_next_check_date       date,
    pch_current_user          varchar(10),
    pch_current_status        varchar(2),
    pch_current_modified_date date,
    pch_current_pid           numeric(3),
    pch_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_conditions
(
    con_id                    numeric(12) not null
        primary key,
    con_pch_id                numeric(12)
        constraint fk_ct_conditions_con_pch_id
            references cts.ct_probity_checks,
    con_cot_id                numeric(12)
        constraint fk_ct_conditions_con_cot_id
            references cts.ct_condition_types,
    con_current_pid           numeric(3),
    con_report_recipient      varchar(2),
    con_long_description      varchar(60),
    con_allocation_process    varchar(10),
    con_short_description     varchar(20),
    con_scope                 varchar(1),
    con_current_user          varchar(10),
    con_current_status        varchar(2),
    con_current_modified_date date,
    con_condition_code        varchar(12),
    con_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_condition_activities
(
    cac_id                    numeric(12) not null
        primary key,
    cac_con_id                numeric(12)
        constraint fk_ct_condition_activities_cac_con_id
            references cts.ct_conditions,
    cac_short_description     varchar(20),
    cac_activity_code         varchar(8),
    cac_long_description      varchar(60),
    cac_current_user          varchar(10),
    cac_current_status        varchar(2),
    cac_current_pid           numeric(3),
    cac_current_modified_date date,
    cac_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_condition_variants
(
    cov_condition_variant     varchar(20),
    cov_id                    numeric(12) not null
        primary key,
    cov_con_id                numeric(12)
        constraint fk_ct_condition_variants_cov_con_id
            references cts.ct_conditions,
    cov_current_pid           numeric(3),
    cov_alert_movement        varchar(1),
    cov_report_movement       varchar(1),
    cov_short_description     varchar(20),
    cov_long_description      varchar(60),
    cov_default_period        numeric(3),
    cov_effective_from_date   date,
    cov_access_restricted     varchar(1),
    cov_scope                 varchar(1),
    cov_auto_snaffle          varchar(1),
    cov_alert_marker_creation varchar(1),
    cov_effective_to_date     date,
    cov_letter_type           varchar(3),
    cov_letter_data_source    varchar(1),
    cov_live_indicator        varchar(1),
    cov_letter_max_animals    numeric(3),
    cov_multiple_usage        varchar(1),
    cov_movt_restrict_type    varchar(2),
    cov_cessation_reason      varchar(2),
    cov_current_status        varchar(2),
    cov_current_user          varchar(10),
    cov_current_modified_date date,
    cov_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_cond_variant_groupings
(
    cvg_id                    numeric(12) not null
        primary key,
    cvg_cov_id                numeric(12)
        constraint fk_ct_cond_variant_groupings_cvg_cov_id
            references cts.ct_condition_variants,
    cvg_grouping_code         varchar(10),
    cvg_current_modified_date date,
    cvg_current_status        varchar(2),
    cvg_current_user          varchar(10),
    cvg_current_pid           numeric(3),
    cvg_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_ps9999_ahdb_data
(
    ran_id        numeric not null
        primary key,
    current_cph   varchar(14),
    animal_eartag varchar(50),
    birth_date    date,
    breed_code    varchar(5),
    sex_of_animal varchar(1),
    row_number    numeric
);

create table if not exists cts.ct_ps9999_ahdb_mov_history
(
    ran_id              numeric,
    on_date             date,
    off_date            date,
    loc_id              numeric,
    loc_full_identifier varchar(14),
    row_number          numeric
);

create table if not exists cts.ct_received_movements
(
    rmo_id                         numeric(12) not null
        primary key,
    rmo_current_user               varchar(10),
    rmo_current_status             varchar(2),
    rmo_current_modified_date      date,
    rmo_current_pid                numeric(3),
    rmo_source_type                varchar(3),
    rmo_suspense_reason            varchar(2),
    rmo_direction                  varchar(1),
    rmo_eartag                     varchar(20),
    rmo_movement_date              varchar(20),
    rmo_movement_type              varchar(20),
    rmo_movement_received_date     varchar(20),
    rmo_movement_loc_type          varchar(20),
    rmo_movement_loc_identifier    varchar(30),
    rmo_movement_subloc_identifier varchar(30),
    rmo_loc_full_identifier        varchar(20),
    rmo_originator                 varchar(20),
    rmo_originators_reference      varchar(20),
    rmo_kill_number                varchar(20),
    rmo_eid_reported               varchar(20),
    rmo_movt_workgroup             varchar(10),
    rmo_interface_file_name        varchar(25),
    rmo_interface_file_txn         numeric(4),
    rmo_orig_interface_file_name   varchar(25),
    rmo_orig_interface_file_txn    numeric(4),
    rmo_created_date               date,
    rmo_submit_datetime            date,
    rmo_amended_datetime           date,
    rmo_amended_by                 varchar(10),
    rmo_amendment_reason           varchar(2),
    rmo_version                    numeric(6),
    row_number                     numeric,
    record_type                    varchar(1),
    record_count                   numeric,
    imported_date                  timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_recd_movement_errors
(
    rme_current_status        varchar(2),
    rme_current_user          varchar(10),
    rme_current_modified_date date,
    rme_current_pid           numeric(3),
    rme_version               numeric(6),
    rme_rmo_id                numeric(12)
        constraint fk_ct_recd_movement_errors_rme_rmo_id
            references cts.ct_received_movements,
    rme_error_code            varchar(4),
    rme_attribute_name        varchar(30),
    rme_id                    numeric(12) not null
        primary key,
    row_number                numeric
);

create table if not exists cts.ct_reset_to_extract
(
    rte_id         numeric not null
        primary key,
    rte_table_name varchar(50),
    rte_status     varchar(1),
    rte_batch      numeric(5),
    row_number     numeric
);

create table if not exists cts.ct_sbcs_ext
(
    sxt_id     varchar(20),
    row_number numeric
);

create table if not exists cts.ct_schemes
(
    sch_expiry_date           date,
    sch_short_description     varchar(30),
    sch_id                    numeric(12) not null
        primary key,
    sch_current_status        varchar(2),
    sch_current_user          varchar(10),
    sch_current_modified_date date,
    sch_current_pid           numeric(3),
    sch_scheme                varchar(10),
    sch_long_description      varchar(100),
    sch_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_claim_types
(
    clt_id                    numeric(12) not null
        primary key,
    clt_current_pid           numeric(3),
    clt_current_status        varchar(2),
    clt_current_user          varchar(10),
    clt_current_modified_date date,
    clt_sch_id                numeric(12)
        constraint fk_ct_claim_types_clt_sch_id
            references cts.ct_schemes,
    clt_claim_type            varchar(1),
    clt_description           varchar(240),
    clt_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_stage_files
(
    stf_id          numeric(12) not null
        primary key,
    stf_file_name   varchar(2000),
    stf_file_type   varchar(100),
    stf_line_number numeric(12),
    stf_record      varchar(2000),
    stf_timestamp   date,
    row_number      numeric
);

create table if not exists cts.ct_stage_locks
(
    stl_file_type varchar(100),
    stl_file_name varchar(2000),
    stl_processed varchar(1),
    stl_timestamp date,
    row_number    numeric
);

create table if not exists cts.ct_stage_messages
(
    stm_directory_key varchar(100),
    stm_file_type     varchar(100),
    stm_file_prefix   varchar(100),
    stm_file_suffix   varchar(100),
    stm_sleep_period  numeric(10),
    row_number        numeric
);

create table if not exists cts.ct_sublocation_types
(
    slt_id                    numeric(12) not null
        primary key,
    slt_subloc_type           varchar(2),
    slt_short_description     varchar(20),
    slt_long_description      varchar(60),
    slt_peer_link_permitted   varchar(1),
    slt_hier_link_permitted   varchar(1),
    slt_movement_subloc_ind   varchar(1),
    slt_use_subloc_address    varchar(1),
    slt_current_user          varchar(10),
    slt_current_status        varchar(2),
    slt_current_modified_date date,
    slt_current_pid           numeric(3),
    slt_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_locations
(
    loc_receive_ppaf_flag     varchar(1),
    loc_id                    numeric(12) not null
        primary key,
    loc_slt_id                numeric(12)
        constraint fk_ct_locations_loc_slt_id
            references cts.ct_sublocation_types,
    loc_lty_id                numeric(12)
        constraint fk_ct_locations_loc_lty_id
            references cts.ct_location_types,
    loc_cty_id                numeric(12)
        constraint fk_ct_locations_loc_cty_id
            references cts.ct_counties,
    loc_receive_labels_flag   varchar(1),
    loc_effective_from        date,
    loc_effective_to          date,
    loc_cessation_reason      varchar(2),
    loc_premises_type         varchar(4),
    loc_comments              varchar(400),
    loc_map_reference         varchar(12),
    loc_source_identifier     varchar(2),
    loc_source_reference      varchar(20),
    loc_tel_number            varchar(25),
    loc_mobile_number         varchar(25),
    loc_fax_number            varchar(25),
    loc_email_address         varchar(50),
    loc_current_status        varchar(2),
    loc_current_user          varchar(10),
    loc_current_modified_date date,
    loc_current_pid           numeric(3),
    loc_reason_code           varchar(2),
    loc_version               numeric(6),
    fake_data                 numeric(1) default 0 not null,
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_addresses
(
    adr_id                    numeric(12) not null
        primary key,
    adr_loc_id                numeric(12)
        constraint fk_ct_addresses_adr_loc_id
            references cts.ct_locations,
    adr_par_id                numeric(12)
        constraint fk_ct_addresses_adr_par_id
            references cts.ct_parties,
    adr_name                  varchar(35),
    adr_address_2             varchar(35),
    adr_address_3             varchar(35),
    adr_address_4             varchar(35),
    adr_address_5             varchar(35),
    adr_post_code             varchar(8),
    adr_current_modified_date date,
    adr_current_status        varchar(2),
    adr_current_user          varchar(10),
    adr_current_pid           numeric(3),
    adr_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_eartag_staging
(
    est_id                      numeric(12) not null
        primary key,
    est_eartag                  varchar(20),
    est_usage_code              varchar(2),
    est_identifier_availability varchar(2),
    est_order_location_repd     varchar(17),
    est_loc_id_order            numeric(12)
        constraint fk_ct_eartag_staging_est_loc_id_order
            references cts.ct_locations,
    est_eartag_reason_code      varchar(2),
    est_erf_id                  numeric(12)
        constraint fk_ct_eartag_staging_est_erf_id
            references cts.ct_eartag_reason_flags,
    est_current_modified_date   date
);

create table if not exists cts.ct_eartags
(
    etg_id                      numeric(12) not null
        primary key,
    etg_ett_id                  numeric(12)
        constraint fk_ct_eartags_etg_ett_id
            references cts.ct_eartag_types,
    etg_erf_id                  numeric(12)
        constraint fk_ct_eartags_etg_erf_id
            references cts.ct_eartag_reason_flags,
    etg_eartag                  varchar(20),
    etg_usage_code              varchar(2),
    etg_eartag_authority        varchar(2),
    etg_source                  varchar(2),
    etg_identifier_availability varchar(2),
    etg_species                 varchar(240),
    etg_fuzzy_eartag_1          varchar(20),
    etg_fuzzy_eartag_2          varchar(20),
    etg_eartag_defra_format     varchar(20),
    etg_type_defra_format       varchar(10),
    etg_current_user            varchar(10),
    etg_current_modified_date   date,
    etg_current_status          varchar(2),
    etg_current_pid             numeric(3),
    etg_version                 numeric(6),
    etg_loc_id_order            numeric(12)
        constraint fk_ct_eartags_etg_loc_id_order
            references cts.ct_locations,
    etg_order_location_repd     varchar(17),
    etg_ppaf_indicator          char,
    row_number                  numeric,
    record_type                 varchar(1),
    record_count                numeric,
    imported_date               timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_label_summaries
(
    las_id                     numeric(12) not null
        primary key,
    las_loc_id_identifying     numeric(12)
        constraint fk_ct_label_summaries_las_loc_id_identifying
            references cts.ct_locations,
    las_loc_id_labels          numeric(12)
        constraint fk_ct_label_summaries_las_loc_id_labels
            references cts.ct_locations,
    las_label_version_number   numeric(2),
    las_last_submitted_date    date,
    las_default_label_type     varchar(2),
    las_default_sheet_quantity numeric(4),
    las_current_user           varchar(10),
    las_current_status         varchar(2),
    las_current_modified_date  date,
    las_current_pid            numeric(3),
    las_version                numeric(6),
    row_number                 numeric
);

create table if not exists cts.ct_label_requests
(
    lar_id                      numeric(12) not null
        primary key,
    lar_las_id                  numeric(12)
        constraint fk_ct_label_requests_lar_las_id
            references cts.ct_label_summaries,
    lar_sheet_quantity          numeric(10),
    lar_label_type              varchar(2),
    lar_label_version           numeric(6),
    lar_submitted_date          date,
    lar_requested_date          date,
    lar_reason_code             varchar(2),
    lar_print_method            varchar(2),
    lar_labels_interface_file   varchar(25),
    lar_keeper_title            varchar(10),
    lar_keeper_initials         varchar(12),
    lar_keeper_surname          varchar(30),
    lar_label_loc_type          varchar(2),
    lar_label_loc_identifier    varchar(20),
    lar_label_subloc_identifier varchar(2),
    lar_label_loc_name          varchar(35),
    lar_label_address_2         varchar(35),
    lar_label_address_3         varchar(35),
    lar_label_address_4         varchar(35),
    lar_label_address_5         varchar(35),
    lar_label_post_code         varchar(8),
    lar_corr_loc_type           varchar(2),
    lar_corr_loc_identifier     varchar(20),
    lar_corr_subloc_identifier  varchar(2),
    lar_corr_title              varchar(10),
    lar_corr_initials           varchar(12),
    lar_corr_surname            varchar(30),
    lar_corr_loc_name           varchar(35),
    lar_corr_address_2          varchar(35),
    lar_corr_address_3          varchar(35),
    lar_corr_address_4          varchar(35),
    lar_corr_address_5          varchar(35),
    lar_corr_post_code          varchar(8),
    lar_current_user            varchar(10),
    lar_current_status          varchar(2),
    lar_current_modified_date   date,
    lar_current_pid             numeric(3),
    lar_version                 numeric(6),
    row_number                  numeric
);

create table if not exists cts.ct_location_identifiers
(
    lid_id                    numeric(12) not null
        primary key,
    lid_loc_id                numeric(12)
        constraint fk_ct_location_identifiers_lid_loc_id
            references cts.ct_locations,
    lid_effective_from_date   date,
    lid_identifier            varchar(14),
    lid_full_identifier       varchar(17),
    lid_sub_identifier        varchar(2),
    lid_effective_to_date     date,
    lid_current_status        varchar(2),
    lid_current_modified_date date,
    lid_current_user          varchar(10),
    lid_current_pid           numeric(3),
    lid_current_amend_reason  varchar(2),
    lid_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_location_party_rels
(
    lpr_id                    numeric(12) not null
        primary key,
    lpr_loc_id                numeric(12)
        constraint fk_ct_location_party_rels_lpr_loc_id
            references cts.ct_locations,
    lpr_lpt_id                numeric(12)
        constraint fk_ct_location_party_rels_lpr_lpt_id
            references cts.ct_location_party_rel_types,
    lpr_par_id                numeric(12)
        constraint fk_ct_location_party_rels_lpr_par_id
            references cts.ct_parties,
    lpr_effective_from_date   date,
    lpr_effective_to_date     date,
    lpr_cessation_reason      varchar(3),
    lpr_comments              varchar(250),
    lpr_current_user          varchar(10),
    lpr_current_modified_date date,
    lpr_current_status        varchar(2),
    lpr_current_pid           numeric(3),
    lpr_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_location_relationships
(
    llr_id                    numeric(12) not null
        primary key,
    llr_loc_id_parent         numeric(12)
        constraint fk_ct_location_relationships_llr_loc_id_parent
            references cts.ct_locations,
    llr_loc_id_child          numeric(12)
        constraint fk_ct_location_relationships_llr_loc_id_child
            references cts.ct_locations,
    llr_effective_from_date   date,
    llr_cessation_reason      varchar(2),
    llr_comments              varchar(200),
    llr_lrt_id                numeric(12)
        constraint fk_ct_location_relationships_llr_lrt_id
            references cts.ct_location_rel_types,
    llr_effective_to_date     date,
    llr_current_status        varchar(2),
    llr_current_modified_date date,
    llr_current_user          varchar(10),
    llr_current_pid           numeric(3),
    llr_version               numeric(6),
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_ppaf_groupings
(
    ppg_id                    numeric(12) not null
        primary key,
    ppg_loc_id_birth          numeric(12)
        constraint fk_ct_ppaf_groupings_ppg_loc_id_birth
            references cts.ct_locations,
    ppg_loc_id_corres         numeric(12)
        constraint fk_ct_ppaf_groupings_ppg_loc_id_corres
            references cts.ct_locations,
    ppg_form_identifier       varchar(30),
    ppg_welsh_indicator       varchar(1),
    ppg_interface_filename    varchar(25),
    ppg_interface_txn_number  numeric(10),
    ppg_printing_date         date,
    ppg_ppaf_added_date       date,
    ppg_current_status        varchar(2),
    ppg_current_user          varchar(10),
    ppg_current_modified_date date,
    ppg_current_pid           numeric(3),
    ppg_version               numeric(6),
    ppg_corres_location_repd  varchar(17),
    ppg_birth_location_repd   varchar(17),
    row_number                numeric
);

create table if not exists cts.ct_preprinted_appn_forms
(
    paf_id                    numeric(12) not null
        primary key,
    paf_etg_id                numeric(12)
        constraint fk_ct_preprinted_appn_forms_paf_etg_id
            references cts.ct_eartags,
    paf_ppg_id                numeric(12)
        constraint fk_ct_preprinted_appn_forms_paf_ppg_id
            references cts.ct_ppaf_groupings,
    paf_reason_for_issue      varchar(1),
    paf_interface_txn_number  numeric(10),
    paf_interface_filename    varchar(25),
    paf_date_issued           date,
    paf_current_status        varchar(2),
    paf_current_modified_date date,
    paf_current_user          varchar(10),
    paf_current_pid           numeric(3),
    paf_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_suspended_movements
(
    smo_id                         numeric(12) not null
        primary key,
    smo_current_user               varchar(10),
    smo_current_status             varchar(2),
    smo_current_modified_date      date,
    smo_current_pid                numeric(3),
    smo_source_type                varchar(3),
    smo_movement_type              numeric(2),
    smo_movement_date              date,
    smo_movement_received_date     date,
    smo_eartag                     varchar(14),
    smo_originator                 varchar(7),
    smo_suspense_date              date,
    smo_direction                  varchar(1),
    smo_movement_loc_type          varchar(2),
    smo_movement_loc_identifier    varchar(30),
    smo_movement_subloc_identifier varchar(30),
    smo_originators_reference      varchar(12),
    smo_kill_number                varchar(20),
    smo_eid_reported               varchar(20),
    smo_movt_workgroup             varchar(10),
    smo_suspense_reason            varchar(2),
    smo_current_purpose_code       varchar(1),
    smo_interface_file_name        varchar(25),
    smo_interface_file_txn         numeric(4),
    smo_orig_interface_file_name   varchar(25),
    smo_orig_interface_file_txn    numeric(4),
    smo_submit_datetime            date,
    smo_amended_by                 varchar(10),
    smo_amended_datetime           date,
    smo_amendment_reason           varchar(2),
    smo_version                    numeric(6),
    row_number                     numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_susp_movement_errors
(
    sme_smo_id                numeric(12)
        constraint fk_ct_susp_movement_errors_sme_smo_id
            references cts.ct_suspended_movements,
    sme_id                    numeric(12) not null
        primary key,
    sme_attribute_name        varchar(30),
    sme_error_code            varchar(4),
    sme_current_user          varchar(10),
    sme_current_modified_date date,
    sme_current_status        varchar(2),
    sme_current_pid           numeric(3),
    sme_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_suspense_char_alloc_rules
(
    sca_id                    numeric(12) not null
        primary key,
    sca_suspense_char         varchar(3),
    sca_rou_id                numeric(12)
        constraint fk_ct_suspense_char_alloc_rules_sca_rou_id
            references cts.ct_alloc_routines,
    sca_subroutine            varchar(3),
    sca_test_value            varchar(10),
    sca_current_user          varchar(10),
    sca_current_status        varchar(2),
    sca_current_modified_date date,
    sca_current_pid           numeric(3),
    sca_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_suspense_wg_alloc_rules
(
    swa_id                    numeric(12) not null
        primary key,
    swa_rou_id                numeric(12)
        constraint fk_ct_suspense_wg_alloc_rules_swa_rou_id
            references cts.ct_alloc_routines,
    swa_priority              numeric(3),
    swa_rule                  varchar(100),
    swa_reported_bad_date     date,
    swa_rule_formula          varchar(100),
    swa_current_user          varchar(10),
    swa_current_status        varchar(2),
    swa_current_modified_date date,
    swa_current_pid           numeric(3),
    swa_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_web_users
(
    wur_current_pid             numeric(3),
    wur_version                 numeric(6),
    wur_id                      numeric(12) not null
        primary key,
    wur_access_number           varchar(12),
    wur_bad_login_reset_count   numeric(3),
    wur_bad_login_per_day_count numeric(3),
    wur_password_issue_flag     varchar(1),
    wur_user_type               varchar(2),
    wur_lpr_id_keeper           numeric(12)
        constraint fk_ct_web_users_wur_lpr_id_keeper
            references cts.ct_location_party_rels,
    wur_encrypted_password      varchar(30),
    wur_staff_number            varchar(7),
    wur_welsh_indicator         varchar(1),
    wur_issued_to_identifier    varchar(30),
    wur_security_filename       varchar(20),
    wur_mobile_number           varchar(30),
    wur_telephone_number        varchar(30),
    wur_user_name               varchar(60),
    wur_user_location           varchar(35),
    wur_address_2               varchar(35),
    wur_address_3               varchar(35),
    wur_address_4               varchar(35),
    wur_address_5               varchar(35),
    wur_post_code               varchar(10),
    wur_email_address           varchar(100),
    wur_expiry_date             date,
    wur_password_filename       varchar(20),
    wur_current_user            varchar(10),
    wur_current_status          varchar(2),
    wur_current_modified_date   date,
    row_number                  numeric
);

create table if not exists cts.ct_valid_applications
(
    vap_id                      numeric(12) not null
        primary key,
    vap_current_status          varchar(2),
    vap_current_user            varchar(10),
    vap_current_modified_date   date,
    vap_current_pid             numeric(3),
    vap_current_intended_action varchar(2),
    vap_application_type        char,
    vap_receipt_date            date,
    vap_loc_id_requester        numeric(12)
        constraint fk_ct_valid_applications_vap_loc_id_requester
            references cts.ct_locations,
    vap_requester_date          date,
    vap_county_requester        varchar(2),
    vap_source_type             varchar(3),
    vap_target_date             date,
    vap_source_reference        varchar(20),
    vap_cts_indicator           char,
    vap_no_of_animals           numeric(3),
    vap_no_of_animals_not_canc  numeric(3),
    vap_number_calf_movts       numeric(2),
    vap_interface_file_name     varchar(25),
    vap_interface_file_txn      numeric(4),
    vap_wur_id                  numeric(12)
        constraint fk_ct_valid_applications_vap_wur_id
            references cts.ct_web_users,
    vap_version                 numeric(6),
    vap_requester_location_repd varchar(17),
    fake_data                   numeric(1) default 0 not null,
    row_number                  numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_applic_statuses
(
    aps_id              numeric(12) not null
        primary key,
    aps_vap_id          numeric(12)
        constraint fk_ct_applic_statuses_aps_vap_id
            references cts.ct_valid_applications,
    aps_user            varchar(10),
    aps_status          varchar(2),
    aps_modified_date   date,
    aps_pid             numeric(3),
    aps_intended_action varchar(2),
    aps_version         numeric(6),
    row_number          numeric
);

create table if not exists cts.ct_registered_animals
(
    ran_id                       numeric(12) not null
        primary key,
    ran_current_user             varchar(10),
    ran_current_status           varchar(2),
    ran_current_modified_date    date,
    ran_current_pid              numeric(3),
    ran_current_intended_action  varchar(2),
    ran_current_change_rcvd_date date,
    ran_current_traced_moves     numeric(4),
    ran_current_add_moves        numeric(4),
    ran_cts_indicator            varchar(1),
    ran_passport_or_licence      varchar(1),
    ran_sex                      varchar(1),
    ran_birth_date               date,
    ran_applic_line              numeric(2),
    ran_brd_id                   numeric(12)
        references cts.ct_breeds,
    ran_loc_id_passport          numeric(12)
        references cts.ct_locations,
    ran_vap_id                   numeric(12)
        references cts.ct_valid_applications,
    ran_mov_id_registration      numeric(12),
    ran_passport_mod_flag        char,
    ran_passport_version_number  varchar(3),
    ran_version                  numeric(6),
    ran_mov_id_death             numeric(12),
    ran_cry_id_chr_origin        numeric(12)
        references cts.ct_countries,
    ran_passport_location_repd   varchar(17),
    fake_data                    numeric(1) default 0 not null,
    row_number                   numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_animal_claims
(
    anc_id                       numeric(12) not null
        primary key,
    anc_ran_id                   numeric(12)
        constraint fk_ct_animal_claims_anc_ran_id
            references cts.ct_registered_animals,
    anc_claim_sequence           numeric(3),
    anc_current_modified_date    date,
    anc_current_pid              numeric(3),
    anc_current_user             varchar(10),
    anc_cls_id                   numeric(12)
        constraint fk_ct_animal_claims_anc_cls_id
            references cts.ct_claim_statuses,
    anc_clt_id                   numeric(12)
        constraint fk_ct_animal_claims_anc_clt_id
            references cts.ct_claim_types,
    anc_claim_reference          varchar(20),
    anc_retention_start_date     date,
    anc_retention_end_date       date,
    anc_office                   varchar(2),
    anc_scheme_year              numeric(4),
    anc_scheme_modified_datetime date,
    anc_version                  numeric(6),
    anc_current_status           varchar(2),
    row_number                   numeric
);

create table if not exists cts.ct_animal_identifiers
(
    aid_id                     numeric(12) not null
        primary key,
    aid_identifier             varchar(50),
    aid_identifier_type        varchar(2),
    aid_effective_from_date    date,
    aid_effective_to_date      date,
    aid_loc_id_assigned        numeric(12)
        constraint fk_ct_animal_identifiers_aid_loc_id_assigned
            references cts.ct_locations,
    aid_current_flag           varchar(1),
    aid_ran_id                 numeric(12)
        constraint fk_ct_animal_identifiers_aid_ran_id
            references cts.ct_registered_animals,
    aid_etg_id                 numeric(12)
        constraint fk_ct_animal_identifiers_aid_etg_id
            references cts.ct_eartags,
    aid_eid_id                 numeric(12)
        constraint fk_ct_animal_identifiers_aid_eid_id
            references cts.ct_electronic_identifiers,
    aid_current_user           varchar(10),
    aid_current_status         varchar(2),
    aid_current_modified_date  date,
    aid_current_pid            numeric(3),
    aid_aid_id_original        numeric(12),
    aid_aid_id_previous        numeric(12),
    aid_version                numeric(6),
    aid_assigned_location_repd varchar(17),
    row_number                 numeric
);

alter table cts.ct_animal_identifiers
    add constraint fk_ct_animal_identifiers_aid_aid_id_original
        foreign key (aid_aid_id_original) references cts.ct_animal_identifiers;

alter table cts.ct_animal_identifiers
    add constraint fk_ct_animal_identifiers_aid_aid_id_previous
        foreign key (aid_aid_id_previous) references cts.ct_animal_identifiers;

create table if not exists cts.ct_animal_relationships
(
    aar_current_modified_date  date,
    aar_current_pid            numeric(3),
    aar_version                numeric(6),
    aar_id                     numeric(12) not null
        primary key,
    aar_rel_type               varchar(3),
    aar_loc_id                 numeric(12)
        constraint fk_ct_animal_relationships_aar_loc_id
            references cts.ct_locations,
    aar_confidence_indicator   numeric(1),
    aar_effective_from_date    date,
    aar_effective_to_date      date,
    aar_ran_id_child           numeric(12)
        constraint fk_ct_animal_relationships_aar_ran_id_child
            references cts.ct_registered_animals,
    aar_ran_id_parent          numeric(12)
        constraint fk_ct_animal_relationships_aar_ran_id_parent
            references cts.ct_registered_animals,
    aar_parent_identifier      varchar(20),
    aar_parent_identifier_type varchar(2),
    aar_cancelled_reason       varchar(3),
    aar_current_user           varchar(10),
    aar_current_status         varchar(2),
    row_number                 numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_animal_statuses
(
    ast_id                   numeric(12) not null
        primary key,
    ast_ran_id               numeric(12)
        constraint fk_ct_animal_statuses_ast_ran_id
            references cts.ct_registered_animals,
    ast_status               varchar(2),
    ast_user                 varchar(10),
    ast_modified_date        date,
    ast_pid                  numeric(3),
    ast_intended_action      varchar(2),
    ast_change_received_date date,
    ast_traced_moves         numeric(4),
    ast_add_moves            numeric(4),
    ast_version              numeric(6),
    row_number               numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_issued_documents
(
    ido_id                       numeric(12) not null
        primary key,
    ido_loc_id                   numeric(12)
        constraint fk_ct_issued_documents_ido_loc_id
            references cts.ct_locations,
    ido_creation_date            date,
    ido_reason_code              varchar(2),
    ido_interface_file_name      varchar(25),
    ido_passpt_layout_ver_number varchar(10),
    ido_interface_txn_number     numeric(4),
    ido_passport_version_number  numeric(3),
    ido_current_status           varchar(2),
    ido_current_modified_date    date,
    ido_current_user             varchar(10),
    ido_current_pid              numeric(3),
    ido_ran_id                   numeric(12)
        constraint fk_ct_issued_documents_ido_ran_id
            references cts.ct_registered_animals,
    ido_version                  numeric(6),
    row_number                   numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_mgt_control_errors
(
    mce_id                      numeric(12) not null
        primary key,
    mce_ran_id                  numeric(12)
        constraint fk_ct_mgt_control_errors_mce_ran_id
            references cts.ct_registered_animals,
    mce_error_code              varchar(5),
    mce_passport_version_issued numeric(4),
    mce_number_of_days_late     numeric(4),
    mce_current_user            varchar(10),
    mce_current_status          varchar(2),
    mce_current_modified_date   date,
    mce_current_pid             numeric(3),
    mce_version                 numeric(6),
    row_number                  numeric
);

create table if not exists cts.ct_registered_movements
(
    mov_id                       numeric(12) not null
        primary key,
    mov_current_user             varchar(10),
    mov_current_status           varchar(2),
    mov_current_modified_date    date,
    mov_current_pid              numeric(12),
    mov_ran_id                   numeric(12),
    mov_loc_id                   numeric(12)
        constraint fk_ct_registered_movements_mov_loc_id
            references cts.ct_locations,
    mov_movement_type            varchar(2),
    mov_direction                varchar(1),
    mov_movement_date            date,
    mov_movement_received_date   date,
    mov_version_creation_date    date,
    mov_reported_eartag          varchar(50),
    mov_source_type              varchar(3),
    mov_originator               numeric(12),
    mov_originators_reference    varchar(20),
    mov_kill_number              varchar(20),
    mov_eid_reported             varchar(20),
    mov_cry_id_import            numeric(12)
        constraint fk_ct_registered_movements_mov_cry_id_import
            references cts.ct_countries,
    mov_health_certificate_no    varchar(30),
    mov_interface_file_name      varchar(25),
    mov_interface_file_txn       numeric(4),
    mov_orig_interface_file_name varchar(25),
    mov_orig_interface_file_txn  numeric(4),
    mov_amendment_reason         varchar(2),
    mov_amended_by               varchar(10),
    mov_suspense_date            date,
    mov_probity_report_date      date,
    mov_anomaly_check_date       date,
    mov_anomaly_code             varchar(4),
    mov_infer_movement_rule      varchar(4),
    mov_version                  numeric(6),
    mov_location_repd            varchar(17),
    fake_data                    numeric(1) default 0 not null,
    row_number                   numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

alter table cts.ct_registered_movements
    add constraint fk_ct_registered_movements_mov_ran_id
        foreign key (mov_ran_id) references cts.ct_registered_animals;

create table if not exists cts.ct_animal_changes
(
    ach_id                      numeric(12) not null
        primary key,
    ach_current_status          varchar(2),
    ach_current_user            varchar(10),
    ach_current_modified_date   date,
    ach_current_pid             numeric(3),
    ach_ran_id_doc_issued       numeric(12)
        constraint fk_ct_animal_changes_ach_ran_id_doc_issued
            references cts.ct_registered_animals,
    ach_loc_id_doc_issued       numeric(12)
        constraint fk_ct_animal_changes_ach_loc_id_doc_issued
            references cts.ct_locations,
    ach_doc_issued_date         date,
    ach_passport_version_number varchar(3),
    ach_mov_id_death_cancel     numeric(12)
        constraint fk_ct_animal_changes_ach_mov_id_death_cancel
            references cts.ct_registered_movements,
    ach_breed_original          varchar(5),
    ach_breed_new               varchar(5),
    ach_sex_original            char,
    ach_sex_new                 char,
    ach_birth_date_original     date,
    ach_birth_date_new          date,
    ach_eartag_original         varchar(14),
    ach_eartag_new              varchar(14),
    row_number                  numeric
);

create table if not exists cts.ct_condition_markers
(
    com_id                    numeric(12) not null
        primary key,
    com_ran_id                numeric(12)
        references cts.ct_registered_animals,
    com_cma_id                numeric(12)
        references cts.ct_cm_authorities,
    com_cac_id                numeric(12)
        references cts.ct_condition_activities,
    com_effective_from_date   date,
    com_marker_type           varchar(1),
    com_amendment_reason_code varchar(3),
    com_last_used_bud_number  numeric(12),
    com_autotag_wave_number   numeric(2),
    com_comments              varchar(1000),
    com_amendment_reason_text varchar(60),
    com_effective_to_date     date,
    com_loc_id                numeric(12)
        references cts.ct_locations,
    com_cov_id                numeric(12)
        references cts.ct_condition_variants,
    com_grouping_reference    varchar(16),
    com_branch_number         numeric(1),
    com_mov_id                numeric(12)
        references cts.ct_registered_movements,
    com_document_refs         varchar(60),
    com_last_probity_date     date,
    com_source                varchar(2),
    com_current_pid           numeric(3),
    com_current_status        varchar(2),
    com_current_user          varchar(10),
    com_current_modified_date date,
    com_version               numeric(6),
    fake_data                 numeric(1) default 0 not null,
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_cm_measures_results
(
    cmr_com_id                numeric(12)
        constraint fk_ct_cm_measures_results_cmr_com_id
            references cts.ct_condition_markers,
    cmr_result_char           varchar(10),
    cmr_measure_char          varchar(10),
    cmr_result_num            numeric(9),
    cmr_measure_num           numeric(9),
    cmr_current_user          varchar(10),
    cmr_current_modified_date date,
    cmr_current_status        varchar(2),
    cmr_current_pid           numeric(3),
    cmr_version               numeric(6),
    cmr_id                    numeric(12) not null
        primary key,
    row_number                numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_locrestrictionstoanimals
(
    lra_com_id             numeric(12)
        constraint fk_ct_locrestrictionstoanimals_lra_com_id
            references cts.ct_condition_markers,
    lra_last_probity_date  date,
    lra_com_effective_from date,
    lra_com_effective_to   date,
    lra_loc_id             numeric(12)
        constraint fk_ct_locrestrictionstoanimals_lra_loc_id
            references cts.ct_locations,
    lra_ran_id             numeric(12)
        constraint fk_ct_locrestrictionstoanimals_lra_ran_id
            references cts.ct_registered_animals,
    row_number             numeric
);

create table if not exists cts.ct_movt_correct_summaries
(
    mcs_id                         numeric(12) not null
        primary key,
    mcs_current_user               varchar(10),
    mcs_current_status             varchar(2),
    mcs_current_modified_date      date,
    mcs_current_pid                numeric(12),
    mcs_smo_or_rmo_ind             varchar(3),
    mcs_smo_id                     numeric(12)
        constraint fk_ct_movt_correct_summaries_mcs_smo_id
            references cts.ct_suspended_movements,
    mcs_rmo_id                     numeric(12)
        constraint fk_ct_movt_correct_summaries_mcs_rmo_id
            references cts.ct_received_movements,
    mcs_mov_id                     numeric(12)
        constraint fk_ct_movt_correct_summaries_mcs_mov_id
            references cts.ct_registered_movements,
    mcs_source_type                varchar(3),
    mcs_suspense_datetime          date,
    mcs_orig_interface_file_name   varchar(25),
    mcs_orig_interface_file_txn    numeric(4),
    mcs_interface_file_name        varchar(25),
    mcs_interface_file_txn         numeric(4),
    mcs_init_eartag                varchar(14),
    mcs_init_loc_type              varchar(2),
    mcs_init_loc_identifier        varchar(30),
    mcs_init_subloc_identifier     varchar(30),
    mcs_init_movement_type         varchar(2),
    mcs_init_movement_date         varchar(20),
    mcs_init_movement_rcvd_date    varchar(20),
    mcs_init_originator            varchar(7),
    mcs_init_originators_reference varchar(12),
    mcs_init_eid_reported          varchar(20),
    mcs_init_kill_number           varchar(20),
    mcs_init_workgroup             varchar(6),
    mcs_init_suspense_reason       varchar(60),
    mcs_init_purpose_code          varchar(1),
    mcs_submit_amendment_reason    varchar(60),
    mcs_submit_workgroup           varchar(6),
    mcs_submit_user                varchar(10),
    mcs_submit_date                date,
    mcs_submit_status              varchar(20),
    mcs_submit_purpose_code        varchar(1),
    mcs_version                    numeric(6),
    row_number                     numeric
);

create table if not exists cts.ct_movt_corr_summ_errors
(
    mse_id                    numeric(12) not null
        primary key,
    mse_mcs_id                numeric(12)
        constraint fk_ct_movt_corr_summ_errors_mse_mcs_id
            references cts.ct_movt_correct_summaries,
    mse_current_user          varchar(10),
    mse_current_status        varchar(2),
    mse_current_modified_date date,
    mse_current_pid           numeric(12),
    mse_attribute_name        varchar(30),
    mse_error_code            varchar(10),
    mse_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_susp_condition_markers
(
    scm_id                      numeric(12) not null
        primary key,
    scm_ran_id                  numeric(12)
        constraint fk_ct_susp_condition_markers_scm_ran_id
            references cts.ct_registered_animals,
    scm_loc_id                  numeric(12)
        constraint fk_ct_susp_condition_markers_scm_loc_id
            references cts.ct_locations,
    scm_location_type           varchar(2),
    scm_submit_date             date,
    scm_amendment_datetime      date,
    scm_amendment_reason        varchar(2),
    scm_amendment_reason_text   varchar(80),
    scm_amendment_status        varchar(10),
    scm_original_interface_txn  numeric(4),
    scm_condition_code          varchar(20),
    scm_document_refs           varchar(60),
    scm_effective_from_date     date,
    scm_location_identifier     varchar(15),
    scm_comments                varchar(500),
    scm_condition_variant       varchar(20),
    scm_effective_to_date       date,
    scm_suspense_reason         varchar(2),
    scm_source                  varchar(2),
    scm_originator              varchar(30),
    scm_condition_authority     varchar(20),
    scm_current_purpose_code    varchar(10),
    scm_grouping_reference      varchar(16),
    scm_original_interface_file varchar(25),
    scm_cancellation_date       date,
    scm_condition_type          varchar(10),
    scm_interface_txn_number    numeric(4),
    scm_interface_filename      varchar(25),
    scm_add_match_flag          varchar(3),
    scm_owner                   varchar(3),
    scm_amended_by              varchar(30),
    scm_animal_identifier       varchar(14),
    scm_animal_identifier_type  varchar(2),
    scm_condition_activity      varchar(20),
    scm_sublocation_identifier  varchar(2),
    scm_use_type                varchar(1),
    scm_system_error            varchar(10),
    scm_current_status          varchar(2),
    scm_current_modified_date   date,
    scm_current_user            varchar(10),
    scm_current_pid             numeric(3),
    scm_version                 numeric(6),
    row_number                  numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_condition_marker_errors
(
    cme_id                    numeric(12) not null
        primary key,
    cme_scm_id                numeric(12)
        constraint fk_ct_condition_marker_errors_cme_scm_id
            references cts.ct_susp_condition_markers,
    cme_attribute_name        varchar(30),
    cme_error_code            varchar(5),
    cme_current_status        varchar(2),
    cme_current_user          varchar(10),
    cme_current_modified_date date,
    cme_current_pid           numeric(3),
    cme_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_susp_cm_measure_results
(
    smr_id                    numeric(12) not null
        primary key,
    smr_scm_id                numeric(12)
        constraint fk_ct_susp_cm_measure_results_smr_scm_id
            references cts.ct_susp_condition_markers,
    smr_measure_char          varchar(10),
    smr_result_num            numeric(9),
    smr_measure_num           numeric(9),
    smr_result_char           varchar(10),
    smr_current_status        varchar(2),
    smr_current_modified_date date,
    smr_current_user          varchar(10),
    smr_current_pid           numeric(3),
    smr_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_workgroups
(
    wgp_id                    numeric(12) not null
        primary key,
    wgp_workgroup             varchar(6),
    wgp_short_name            varchar(20),
    wgp_long_name             varchar(60),
    wgp_active_indicator      char,
    wgp_printer               varchar(50),
    wgp_summary_type          varchar(2),
    wgp_reassign_lock         char,
    wgp_current_status        varchar(2),
    wgp_current_modified_date date,
    wgp_current_user          varchar(10),
    wgp_current_pid           numeric(3),
    wgp_version               numeric(6),
    fake_data                 numeric(1) default 0 not null,
    row_number                numeric
);

create table if not exists cts.ct_letters
(
    let_id                    numeric(12) not null
        primary key,
    let_type                  varchar(3),
    let_description           varchar(30),
    let_wgp_id                numeric(12)
        constraint fk_ct_letters_let_wgp_id
            references cts.ct_workgroups,
    let_program_name          varchar(10),
    let_wgp_id_sent           numeric(12)
        constraint fk_ct_letters_let_wgp_id_sent
            references cts.ct_workgroups,
    let_current_user          varchar(10),
    let_current_status        varchar(2),
    let_current_modified_date date,
    let_current_pid           numeric(3),
    let_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_received_applications
(
    rap_id                        numeric(12) not null
        primary key,
    rap_current_user              varchar(10),
    rap_current_status            varchar(2),
    rap_current_modified_date     date,
    rap_current_pid               numeric(3),
    rap_application_type          char,
    rap_applic_receipt_date       varchar(20),
    rap_applic_target_date        date,
    rap_cts_indicator             char,
    rap_eartag_type               varchar(20),
    rap_eartag                    varchar(30),
    rap_source_type               varchar(3),
    rap_source_reference          varchar(20),
    rap_request_loc_type          varchar(2),
    rap_request_loc_identifier    varchar(30),
    rap_request_subloc_identifier varchar(30),
    rap_genetic_dam_et_type       varchar(20),
    rap_genetic_dam_eartag        varchar(30),
    rap_surr_dam_et_type          varchar(20),
    rap_surr_dam_eartag           varchar(30),
    rap_sire_et_type              varchar(20),
    rap_sire_eartag               varchar(30),
    rap_birth_date                varchar(20),
    rap_placement_date            varchar(20),
    rap_breed                     varchar(20),
    rap_sex                       varchar(20),
    rap_initial_loc_type          varchar(2),
    rap_initial_loc_identifier    varchar(30),
    rap_initial_subloc_identifier varchar(30),
    rap_country_of_origin         varchar(2),
    rap_health_certificate_no     varchar(30),
    rap_import_identifier         varchar(20),
    rap_electronic_identifier     varchar(20),
    rap_new_eartag_type           varchar(20),
    rap_new_eartag                varchar(30),
    rap_number_calf_movts         numeric(1),
    rap_wgp_id                    numeric(12)
        constraint fk_ct_received_applications_rap_wgp_id
            references cts.ct_workgroups,
    rap_interface_file_name       varchar(25),
    rap_interface_file_txn        numeric(4),
    rap_orig_if_file_name         varchar(25),
    rap_orig_if_file_txn          numeric(4),
    rap_chr_correction_type       char,
    rap_chr_location_ind          char,
    rap_created_date              date,
    rap_intended_action           varchar(2),
    rap_amended_by                varchar(10),
    rap_amended_datetime          date,
    rap_submit_datetime           date,
    rap_originator                varchar(20),
    rap_ran_id_reserved           numeric(12)
        constraint fk_ct_received_applications_rap_ran_id_reserved
            references cts.ct_registered_animals,
    rap_version                   numeric(6),
    rap_request_letter            date,
    rap_reminder_letter           date,
    rap_refused_letter            date,
    row_number                    numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_recd_application_errors
(
    rae_id                    numeric(12) not null
        primary key,
    rae_rap_id                numeric(12)
        constraint fk_ct_recd_application_errors_rae_rap_id
            references cts.ct_received_applications,
    rae_attribute_name        varchar(30),
    rae_error_code            varchar(4),
    rae_current_status        varchar(2),
    rae_current_user          varchar(10),
    rae_current_modified_date date,
    rae_current_pid           numeric(3),
    rae_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_suspended_animals
(
    san_vap_id                  numeric(12)
        constraint fk_ct_suspended_animals_san_vap_id
            references cts.ct_valid_applications,
    san_wgp_id                  numeric(12)
        constraint fk_ct_suspended_animals_san_wgp_id
            references cts.ct_workgroups,
    san_application_type        char,
    san_cts_indicator           char,
    san_applic_receipt_date     date,
    san_suspense_date           date,
    san_eartag                  varchar(30),
    san_intended_action         varchar(2),
    san_passport_version_number varchar(3),
    san_amended_by              varchar(10),
    san_amended_datetime        date,
    san_sex                     char,
    san_breed                   varchar(20),
    san_birth_date              date,
    san_placement_date          date,
    san_loc_id_initial          numeric(12)
        constraint fk_ct_suspended_animals_san_loc_id_initial
            references cts.ct_locations,
    san_eartag_type             varchar(20),
    san_genetic_dam_et_type     varchar(20),
    san_genetic_dam_eartag      varchar(30),
    san_surr_dam_et_type        varchar(20),
    san_surr_dam_eartag         varchar(30),
    san_sire_et_type            varchar(20),
    san_sire_eartag             varchar(30),
    san_electronic_identifier   varchar(30),
    san_country_of_origin       varchar(2),
    san_health_certificate_no   varchar(30),
    san_import_identifier       varchar(20),
    san_number_calf_movts       numeric(1),
    san_chr_location_ind        char,
    san_chr_correction_type     char,
    san_change_received_date    date,
    san_amend_reason            varchar(2),
    san_submit_datetime         date,
    san_loc_id_request          numeric(12)
        constraint fk_ct_suspended_animals_san_loc_id_request
            references cts.ct_locations,
    san_amend_retag_ind         char,
    san_new_eartag_type         varchar(20),
    san_new_eartag              varchar(30),
    san_source_type             varchar(3),
    san_source_reference        varchar(20),
    san_interface_file_name     varchar(25),
    san_interface_file_txn      numeric(4),
    san_orig_if_file_name       varchar(25),
    san_orig_if_file_txn        numeric(4),
    san_applic_target_date      date,
    san_originator              varchar(20),
    san_version                 numeric(6),
    san_initial_location_repd   varchar(17),
    san_request_location_repd   varchar(17),
    san_late_app_letter         date,
    san_id                      numeric(12) not null
        primary key,
    san_current_user            varchar(10),
    san_current_status          varchar(2),
    san_current_modified_date   date,
    san_current_pid             numeric(3),
    san_ran_id                  numeric(12)
        constraint fk_ct_suspended_animals_san_ran_id
            references cts.ct_registered_animals,
    row_number                  numeric,
    record_type               varchar(1),
    record_count              numeric,
    imported_date             timestamp without time zone default current_timestamp
);

create table if not exists cts.ct_animal_correct_summaries
(
    acs_init_initial_loc_ident     varchar(30),
    acs_init_initial_subloc_ident  varchar(2),
    acs_init_placement_date        varchar(30),
    acs_init_previous_eartag       varchar(30),
    acs_init_country_of_origin     varchar(30),
    acs_init_health_certificate_no varchar(30),
    acs_init_electronic_identifier varchar(30),
    acs_init_import_identifier     varchar(50),
    acs_init_number_calf_movts     numeric(2),
    acs_init_intended_action       varchar(60),
    acs_submit_intended_action     varchar(60),
    acs_submit_amend_reason        varchar(60),
    acs_submit_status              varchar(20),
    acs_submit_user                varchar(10),
    acs_submit_workgroup           varchar(6),
    acs_submit_date                date,
    acs_change_received_date       date,
    acs_suspense_datetime          date,
    acs_amend_retag_ind            varchar(1),
    acs_new_eartag_type            varchar(20),
    acs_new_eartag                 varchar(30),
    acs_chr_correction_type        char,
    acs_chr_location_ind           char,
    acs_interface_file_name        varchar(25),
    acs_interface_file_txn         numeric(4),
    acs_version                    numeric(6),
    acs_migrated_appsus_key        numeric(12),
    acs_late_app_letter            date,
    acs_request_letter             date,
    acs_reminder_letter            date,
    acs_refused_letter             date,
    acs_id                         numeric(12) not null
        primary key,
    acs_current_user               varchar(10),
    acs_current_status             varchar(2),
    acs_current_modified_date      date,
    acs_current_pid                numeric(12),
    acs_san_or_rap_ind             varchar(3),
    acs_san_id                     numeric(12)
        constraint fk_ct_animal_correct_summaries_acs_san_id
            references cts.ct_suspended_animals,
    acs_rap_id                     numeric(12)
        constraint fk_ct_animal_correct_summaries_acs_rap_id
            references cts.ct_received_applications,
    acs_ran_id                     numeric(12)
        constraint fk_ct_animal_correct_summaries_acs_ran_id
            references cts.ct_registered_animals,
    acs_vap_id                     numeric(12)
        constraint fk_ct_animal_correct_summaries_acs_vap_id
            references cts.ct_valid_applications,
    acs_application_type           varchar(1),
    acs_source_type                varchar(3),
    acs_source_reference           varchar(20),
    acs_cts_indicator              varchar(1),
    acs_passport_version_no        varchar(2),
    acs_init_applic_receipt_date   varchar(20),
    acs_init_applic_target_date    date,
    acs_init_request_loc_type      varchar(2),
    acs_init_request_loc_ident     varchar(30),
    acs_init_request_subloc_ident  varchar(30),
    acs_init_eartag_type           varchar(20),
    acs_init_eartag                varchar(30),
    acs_init_breed                 varchar(20),
    acs_init_birth_date            varchar(20),
    acs_init_sex                   varchar(20),
    acs_init_genetic_dam_et_type   varchar(20),
    acs_init_genetic_dam_eartag    varchar(30),
    acs_init_surr_dam_et_type      varchar(20),
    acs_init_surr_dam_eartag       varchar(30),
    acs_init_sire_et_type          varchar(20),
    acs_init_sire_eartag           varchar(30),
    acs_init_initial_loc_type      varchar(2),
    row_number                     numeric
);

create table if not exists cts.ct_animal_corr_summ_errors
(
    ase_id                    numeric(12) not null
        primary key,
    ase_acs_id                numeric(12)
        constraint fk_ct_animal_corr_summ_errors_ase_acs_id
            references cts.ct_animal_correct_summaries,
    ase_current_user          varchar(10),
    ase_current_status        varchar(2),
    ase_current_modified_date date,
    ase_current_pid           numeric(12),
    ase_attribute_name        varchar(30),
    ase_error_code            varchar(10),
    ase_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_susp_animal_errors
(
    sae_id                    numeric(12) not null
        primary key,
    sae_san_id                numeric(12)
        constraint fk_ct_susp_animal_errors_sae_san_id
            references cts.ct_suspended_animals,
    sae_error_code            varchar(4),
    sae_attribute_name        varchar(30),
    sae_current_modified_date date,
    sae_current_user          varchar(10),
    sae_current_status        varchar(2),
    sae_current_pid           numeric(3),
    sae_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_wg_autoallocations
(
    wga_id                    numeric(12) not null
        primary key,
    wga_rou_id                numeric(12)
        constraint fk_ct_wg_autoallocations_wga_rou_id
            references cts.ct_alloc_routines,
    wga_wgp_id                numeric(12)
        constraint fk_ct_wg_autoallocations_wga_wgp_id
            references cts.ct_workgroups,
    wga_allocation            varchar(10),
    wga_assignment            varchar(10),
    wga_current_user          varchar(10),
    wga_current_pid           numeric(3),
    wga_current_status        varchar(2),
    wga_current_modified_date date,
    wga_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_wg_super_assignments
(
    wsa_id                    numeric(12) not null
        primary key,
    wsa_wgp_id_current        numeric(12)
        constraint fk_ct_wg_super_assignments_wsa_wgp_id_current
            references cts.ct_workgroups,
    wsa_wgp_id_assigned       numeric(12)
        constraint fk_ct_wg_super_assignments_wsa_wgp_id_assigned
            references cts.ct_workgroups,
    wsa_rou_id                numeric(12)
        constraint fk_ct_wg_super_assignments_wsa_rou_id
            references cts.ct_alloc_routines,
    wsa_current_user          varchar(10),
    wsa_current_status        varchar(2),
    wsa_current_modified_date date,
    wsa_current_pid           numeric(3),
    wsa_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_wg_user_assignments
(
    wua_id                    numeric(12) not null
        primary key,
    wua_cus_id                numeric(12)
        constraint fk_ct_wg_user_assignments_wua_cus_id
            references cts.ct_cts_users,
    wua_wgp_id                numeric(12)
        constraint fk_ct_wg_user_assignments_wua_wgp_id
            references cts.ct_workgroups,
    wua_wg_contact_ind        char,
    wua_favoured_wg_ind       char,
    wua_current_user          varchar(10),
    wua_current_status        varchar(2),
    wua_current_modified_date date,
    wua_current_pid           numeric(3),
    wua_version               numeric(6),
    row_number                numeric
);

create table if not exists cts.ct_parties_faker
(
    par_surname       text,
    par_initials      text,
    par_title         text,
    par_tel_number    text,
    par_mobile_number text,
    par_email_address text
);

create table if not exists cts.ct_locations_faker
(
    loc_tel_number       text,
    loc_mobile_number    text,
    loc_fax_number       text,
    loc_email_address    text,
    loc_source_reference text,
    loc_comments         text
);
