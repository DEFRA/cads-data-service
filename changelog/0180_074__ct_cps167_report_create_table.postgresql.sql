-- liquibase formatted sql

-- changeset codex:0180-074

create table if not exists _ct_cps167_report
(
    kns_id                    numeric(12) not null
        primary key,
    kns_run_date_time         date,
    kns_filename              varchar(25),
    kns_action_type           varchar(5),
    kns_source_directory      varchar(200),
    kns_destination_directory varchar(200),
    kns_message               varchar(200),
    row_number                numeric
);
