-- liquibase formatted sql

-- changeset codex:0180-072

create table if not exists _ct_counties_migration
(
    cty_id                    numeric(12) not null
        primary key,
    cty_code                  varchar(2),
    cty_name                  varchar(25),
    cty_uk_area               varchar(1),
    cty_vet_area              varchar(3),
    cty_passport_area         varchar(3),
    cty_admin_office          varchar(2),
    cty_bcms_team             varchar(10),
    cty_inspection_area       varchar(2),
    cty_data_mgt_area         varchar(3),
    cty_current_user          varchar(10),
    cty_current_status        varchar(2),
    cty_current_modified_date date,
    cty_current_pid           numeric(3),
    cty_version               numeric(6),
    cty_due_for_migration     varchar(1),
    cty_date_migrated         date,
    cty_print_passports       varchar(1),
    row_number                numeric
);
