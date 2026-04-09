-- liquibase formatted sql

-- changeset codex:0180-104

create table if not exists _ct_non_working_days
(
    nwd_id                    numeric(12) not null
        primary key,
    nwd_date                  date,
    nwd_description           varchar(50),
    nwd_year                  numeric(4),
    nwd_current_user          varchar(10),
    nwd_current_status        varchar(2),
    nwd_current_modified_date date,
    nwd_pid                   numeric(3),
    nwd_version               numeric(6),
    row_number                numeric
);
