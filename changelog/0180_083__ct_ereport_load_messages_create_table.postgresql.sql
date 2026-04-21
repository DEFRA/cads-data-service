-- liquibase formatted sql

-- changeset codex:0180-083

create table if not exists _ct_ereport_load_messages
(
    erm_directory_key varchar(100),
    erm_file_type     varchar(100),
    erm_file_prefix   varchar(100),
    erm_file_suffix   varchar(100),
    erm_sleep_period  numeric(10),
    row_number        numeric
);
