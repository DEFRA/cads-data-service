-- liquibase formatted sql

-- changeset codex:0180-077

create table if not exists _ct_eartag_formats
(
    etf_id                    numeric(12) not null
        primary key,
    etf_description           varchar(60),
    etf_format_pattern        varchar(30),
    etf_max_input_length      numeric(3),
    etf_extra_chars_allowed   varchar(30),
    etf_current_user          varchar(10),
    etf_current_status        varchar(2),
    etf_current_modified_date date,
    etf_current_pid           numeric(3),
    etf_version               numeric(6),
    row_number                numeric
);
