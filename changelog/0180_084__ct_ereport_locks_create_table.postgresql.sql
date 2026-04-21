-- liquibase formatted sql

-- changeset codex:0180-084

create table if not exists _ct_ereport_locks
(
    erl_file_type varchar(100),
    erl_file_name varchar(2000),
    erl_processed varchar(1),
    erl_timestamp date
);
