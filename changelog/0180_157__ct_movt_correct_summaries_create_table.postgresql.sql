-- liquibase formatted sql

-- changeset codex:0180-157

create table if not exists _ct_movt_correct_summaries
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
            references _ct_suspended_movements,
    mcs_rmo_id                     numeric(12)
        constraint fk_ct_movt_correct_summaries_mcs_rmo_id
            references _ct_received_movements,
    mcs_mov_id                     numeric(12)
        constraint fk_ct_movt_correct_summaries_mcs_mov_id
            references _ct_registered_movements,
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
