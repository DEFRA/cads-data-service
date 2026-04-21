-- liquibase formatted sql

-- changeset codex:0180-091

create table if not exists _ct_insert_update_log
(
    iul_id                    numeric(12) not null
        primary key,
    iul_system                varchar(240),
    iul_table_name            varchar(240),
    iul_record_key            varchar(240),
    iul_name                  varchar(25),
    iul_date_processed        date,
    iul_date_processed_mis    date,
    iul_insert_delete_flag    varchar(1),
    iul_current_user          varchar(10),
    iul_current_status        varchar(2),
    iul_current_modified_date date,
    iul_current_pid           numeric(3),
    iul_version               numeric(6),
    row_number                numeric
);
