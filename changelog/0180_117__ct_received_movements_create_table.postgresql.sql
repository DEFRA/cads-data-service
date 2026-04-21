-- liquibase formatted sql

-- changeset codex:0180-117

create table if not exists _ct_received_movements
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
    row_number                     numeric
);
