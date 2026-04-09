-- liquibase formatted sql

-- changeset codex:0180-167

create table if not exists _ct_animal_correct_summaries
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
            references _ct_suspended_animals,
    acs_rap_id                     numeric(12)
        constraint fk_ct_animal_correct_summaries_acs_rap_id
            references _ct_received_applications,
    acs_ran_id                     numeric(12)
        constraint fk_ct_animal_correct_summaries_acs_ran_id
            references _ct_registered_animals,
    acs_vap_id                     numeric(12)
        constraint fk_ct_animal_correct_summaries_acs_vap_id
            references _ct_valid_applications,
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
