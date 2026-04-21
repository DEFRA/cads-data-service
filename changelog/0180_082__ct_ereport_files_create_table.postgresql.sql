-- liquibase formatted sql

-- changeset codex:0180-082

create table if not exists _ct_ereport_files
(
    ere_id          numeric(12) not null
        primary key,
    ere_file_name   varchar(2000),
    ere_file_type   varchar(100),
    ere_line_number numeric(12),
    ere_record      varchar(2000),
    ere_timestamp   date
);
