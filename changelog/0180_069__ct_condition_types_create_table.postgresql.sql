-- liquibase formatted sql

-- changeset codex:0180-069

create table if not exists _ct_condition_types
(
    cot_id                    numeric(12) not null
        primary key,
    cot_condition_type        varchar(5),
    cot_short_description     varchar(20),
    cot_effective_from_date   date,
    cot_long_description      varchar(60),
    cot_effective_to_date     date,
    cot_cessation_reason      varchar(3),
    cot_access_group          varchar(10),
    cot_current_user          varchar(10),
    cot_current_status        varchar(2),
    cot_current_modified_date date,
    cot_current_pid           numeric(3),
    cot_version               numeric(6),
    row_number                numeric
);
