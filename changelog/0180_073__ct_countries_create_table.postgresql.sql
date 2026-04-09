-- liquibase formatted sql

-- changeset codex:0180-073

create table if not exists _ct_countries
(
    cry_id                    numeric(12) not null
        primary key,
    cry_code                  varchar(2),
    cry_name                  varchar(25),
    cry_eu_member             varchar(1),
    cry_import_export         varchar(1),
    cry_cry_id_main_eu        numeric(12)
        constraint fk_ct_countries_cry_cry_id_main_eu
            references _ct_countries,
    cry_back_capture          varchar(1),
    cry_current_user          varchar(10),
    cry_current_status        varchar(2),
    cry_current_modified_date date,
    cry_current_pid           numeric(3),
    cry_version               numeric(6),
    row_number                numeric
);
