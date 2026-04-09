-- liquibase formatted sql

-- changeset codex:0180-080

create table if not exists _ct_eartag_types
(
    ett_id                    numeric(12) not null
        primary key,
    ett_eartag_type           varchar(2),
    ett_cr_export             varchar(1),
    ett_description           varchar(60),
    ett_etf_id                numeric(12)
        constraint fk_ct_eartag_types_ett_etf_id
            references _ct_eartag_formats,
    ett_short_description     varchar(20),
    ett_current_user          varchar(10),
    ett_current_status        varchar(2),
    ett_current_modified_date date,
    ett_current_pid           numeric(3),
    ett_version               numeric(6),
    row_number                numeric
);
