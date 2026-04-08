-- liquibase formatted sql

-- changeset codex:0180-121

create table if not exists _ct_schemes
(
    sch_expiry_date           date,
    sch_short_description     varchar(30),
    sch_id                    numeric(12) not null
        primary key,
    sch_current_status        varchar(2),
    sch_current_user          varchar(10),
    sch_current_modified_date date,
    sch_current_pid           numeric(3),
    sch_scheme                varchar(10),
    sch_long_description      varchar(100),
    sch_version               numeric(6),
    row_number                numeric
);
