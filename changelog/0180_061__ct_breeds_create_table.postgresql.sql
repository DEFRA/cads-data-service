-- liquibase formatted sql

-- changeset codex:0180-061

create table if not exists _ct_breeds
(
    brd_id                    numeric(12) not null
        primary key,
    brd_code                  varchar(5),
    brd_type                  varchar(2),
    brd_long_description      varchar(60),
    brd_scheme_eligibility    varchar(10),
    brd_short_description     varchar(20),
    brd_current_user          varchar(10),
    brd_current_status        varchar(2),
    brd_current_pid           numeric(3),
    brd_current_modified_date date,
    brd_version               numeric(6),
    row_number                numeric
);
