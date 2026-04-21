-- liquibase formatted sql

-- changeset codex:0180-123

create table if not exists _ct_stage_files
(
    stf_id          numeric(12) not null
        primary key,
    stf_file_name   varchar(2000),
    stf_file_type   varchar(100),
    stf_line_number numeric(12),
    stf_record      varchar(2000),
    stf_timestamp   date,
    row_number      numeric
);
