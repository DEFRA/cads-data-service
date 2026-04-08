-- liquibase formatted sql

-- changeset codex:0180-105

create table if not exists _ct_param_header
(
    phd_id                    numeric(12) not null
        primary key,
    phd_param                 varchar(30),
    phd_short_desc            varchar(20),
    phd_long_desc             varchar(60),
    phd_dont_cache            char,
    phd_use_short             char,
    phd_current_user          varchar(10),
    phd_current_status        varchar(2),
    phd_current_modified_date date,
    phd_current_pid           numeric(3),
    phd_version               numeric(6),
    row_number                numeric
);
