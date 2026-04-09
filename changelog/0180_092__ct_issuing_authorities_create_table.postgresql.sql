-- liquibase formatted sql

-- changeset codex:0180-092

create table if not exists _ct_issuing_authorities
(
    isa_id                    numeric(12) not null
        primary key,
    isa_country_name          varchar(240),
    isa_manufacturers_name    varchar(240),
    isa_type                  varchar(10),
    isa_current_status        varchar(2),
    isa_current_user          varchar(10),
    isa_current_modified_date date,
    isa_current_pid           numeric(3),
    isa_version               numeric(6),
    row_number                numeric
);
