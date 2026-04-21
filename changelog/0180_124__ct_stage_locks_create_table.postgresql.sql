-- liquibase formatted sql

-- changeset codex:0180-124

create table if not exists _ct_stage_locks
(
    stl_file_type varchar(100),
    stl_file_name varchar(2000),
    stl_processed varchar(1),
    stl_timestamp date,
    row_number    numeric
);
