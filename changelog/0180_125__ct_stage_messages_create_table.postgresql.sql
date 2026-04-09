-- liquibase formatted sql

-- changeset codex:0180-125

create table if not exists _ct_stage_messages
(
    stm_directory_key varchar(100),
    stm_file_type     varchar(100),
    stm_file_prefix   varchar(100),
    stm_file_suffix   varchar(100),
    stm_sleep_period  numeric(10),
    row_number        numeric
);
