-- liquibase formatted sql

-- changeset codex:0180-159

create table if not exists _ct_susp_condition_markers
(
    scm_id                      numeric(12) not null
        primary key,
    scm_ran_id                  numeric(12)
        constraint fk_ct_susp_condition_markers_scm_ran_id
            references _ct_registered_animals,
    scm_loc_id                  numeric(12)
        constraint fk_ct_susp_condition_markers_scm_loc_id
            references _ct_locations,
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
    row_number                  numeric
);
