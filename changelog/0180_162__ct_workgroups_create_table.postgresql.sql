-- liquibase formatted sql

-- changeset codex:0180-162

create table if not exists _ct_workgroups
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
    row_number                numeric
);
