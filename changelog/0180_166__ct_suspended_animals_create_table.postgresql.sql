-- liquibase formatted sql

-- changeset codex:0180-166

create table if not exists _ct_suspended_animals
(
    san_vap_id                  numeric(12)
        constraint fk_ct_suspended_animals_san_vap_id
            references _ct_valid_applications,
    san_wgp_id                  numeric(12)
        constraint fk_ct_suspended_animals_san_wgp_id
            references _ct_workgroups,
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
            references _ct_locations,
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
            references _ct_locations,
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
            references _ct_registered_animals,
    row_number                  numeric
);
