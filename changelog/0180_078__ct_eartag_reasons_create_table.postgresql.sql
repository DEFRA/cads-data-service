-- liquibase formatted sql

-- changeset codex:0180-078

create table if not exists _ct_eartag_reasons
(
    etr_id                    numeric(12) not null
        primary key,
    etr_eartag_reason_code    varchar(2),
    etr_reason_code_type      varchar(1),
    etr_short_description     varchar(20),
    etr_long_description      varchar(60),
    etr_current_status        varchar(2),
    etr_current_user          varchar(10),
    etr_current_modified_date date,
    etr_current_pid           numeric(3),
    etr_version               numeric(6),
    row_number                numeric
);
