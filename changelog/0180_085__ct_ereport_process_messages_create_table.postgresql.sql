-- liquibase formatted sql

-- changeset codex:0180-085

create table if not exists _ct_ereport_process_messages
(
    erq_file_type    varchar(3),
    erq_sleep_period numeric(10),
    erq_delay_period numeric(10),
    row_number       numeric
);
