-- liquibase formatted sql

-- changeset codex:0180-064

create table if not exists _ct_cla_extract_dm
(
    cle_id                    numeric(12) not null
        primary key,
    cle_batch_id              numeric(12),
    cle_run_start             date,
    cle_run_end               date,
    cle_data_read_start       date,
    cle_data_read_end         date,
    cle_run_status            varchar(1000),
    cle_current_modified_date date,
    cle_bulk_run_stop         varchar(1),
    row_number                numeric
);
