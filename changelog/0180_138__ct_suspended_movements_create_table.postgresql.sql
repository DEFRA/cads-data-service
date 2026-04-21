-- liquibase formatted sql

-- changeset codex:0180-138

create table if not exists _ct_suspended_movements
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
    row_number                     numeric
);
