-- liquibase formatted sql

-- changeset codex:0180-152

create table if not exists _ct_registered_movements
(
    mov_id                       numeric(12) not null
        primary key,
    mov_current_user             varchar(10),
    mov_current_status           varchar(2),
    mov_current_modified_date    date,
    mov_current_pid              numeric(12),
    mov_ran_id                   numeric(12),
    mov_loc_id                   numeric(12),
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
    mov_cry_id_import            numeric(12),
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
    row_number                   numeric
);