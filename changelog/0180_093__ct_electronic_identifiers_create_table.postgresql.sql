-- liquibase formatted sql

-- changeset codex:0180-093

create table if not exists _ct_electronic_identifiers
(
    eid_id                    numeric(12) not null
        primary key,
    eid_electronic_identifier numeric(16),
    eid_isa_id                numeric(12)
        constraint fk_ct_electronic_identifiers_eid_isa_id
            references _ct_issuing_authorities,
    eid_unique_number         varchar(12),
    eid_current_status        varchar(2),
    eid_current_user          varchar(10),
    eid_current_pid           numeric(3),
    eid_current_modified_date date,
    eid_version               numeric(6),
    row_number                numeric
);
