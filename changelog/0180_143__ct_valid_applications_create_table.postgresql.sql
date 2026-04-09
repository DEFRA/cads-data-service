-- liquibase formatted sql

-- changeset codex:0180-143

create table if not exists _ct_valid_applications
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
            references _ct_locations,
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
            references _ct_web_users,
    vap_version                 numeric(6),
    vap_requester_location_repd varchar(17),
    row_number                  numeric
);
