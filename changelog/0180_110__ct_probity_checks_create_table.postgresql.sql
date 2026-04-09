-- liquibase formatted sql

-- changeset codex:0180-110

create table if not exists _ct_probity_checks
(
    pch_id                    numeric(12) not null
        primary key,
    pch_long_description      varchar(60),
    pch_short_description     varchar(20),
    pch_checked_to_date       date,
    pch_check_period          numeric(3),
    pch_next_check_date       date,
    pch_current_user          varchar(10),
    pch_current_status        varchar(2),
    pch_current_modified_date date,
    pch_current_pid           numeric(3),
    pch_version               numeric(6),
    row_number                numeric
);
