-- liquibase formatted sql

-- changeset codex:0180-068

create table if not exists _ct_comms_addresses
(
    coa_id                    numeric(12) not null
        primary key,
    coa_current_status        varchar(2),
    coa_current_user          varchar(10),
    coa_current_modified_date date,
    coa_pid                   numeric(3),
    coa_email_address         varchar(200),
    coa_attachment            char,
    row_number                numeric
);
