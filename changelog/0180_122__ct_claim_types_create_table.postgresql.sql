-- liquibase formatted sql

-- changeset codex:0180-122

create table if not exists _ct_claim_types
(
    clt_id                    numeric(12) not null
        primary key,
    clt_current_pid           numeric(3),
    clt_current_status        varchar(2),
    clt_current_user          varchar(10),
    clt_current_modified_date date,
    clt_sch_id                numeric(12)
        constraint fk_ct_claim_types_clt_sch_id
            references _ct_schemes,
    clt_claim_type            varchar(1),
    clt_description           varchar(240),
    clt_version               numeric(6),
    row_number                numeric
);
