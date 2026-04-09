-- liquibase formatted sql

-- changeset codex:0180-070

create table if not exists _ct_cm_authorities
(
    cma_id                    numeric(12) not null
        primary key,
    cma_cot_id                numeric(12)
        constraint fk_ct_cm_authorities_cma_cot_id
            references _ct_condition_types,
    cma_authority_code        varchar(10),
    cma_short_name            varchar(30),
    cma_long_name             varchar(60),
    cma_current_pid           numeric(3),
    cma_current_status        varchar(2),
    cma_current_modified_date date,
    cma_current_user          varchar(10),
    cma_version               numeric(6),
    row_number                numeric
);
