-- liquibase formatted sql

-- changeset codex:0180-059

create table if not exists _ct_application_late_days
(
    ald_id                    numeric(12) not null
        primary key,
    ald_valid_days            numeric(3),
    ald_effective_from_date   date,
    ald_application_type      varchar(2),
    ald_additional_days_late  numeric(3),
    ald_current_user          varchar(10),
    ald_current_status        varchar(2),
    ald_current_pid           numeric(3),
    ald_current_modified_date date,
    ald_version               numeric(6),
    row_number                numeric
);
