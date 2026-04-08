-- liquibase formatted sql

-- changeset codex:0180-164

create table if not exists _ct_received_applications
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
            references _ct_workgroups,
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
            references _ct_registered_animals,
    rap_version                   numeric(6),
    rap_request_letter            date,
    rap_reminder_letter           date,
    rap_refused_letter            date,
    row_number                    numeric
);
