-- liquibase formatted sql

-- changeset codex:0180-141

create table if not exists _ct_suspense_wg_alloc_rules
(
    swa_id                    numeric(12) not null
        primary key,
    swa_rou_id                numeric(12)
        constraint fk_ct_suspense_wg_alloc_rules_swa_rou_id
            references _ct_alloc_routines,
    swa_priority              numeric(3),
    swa_rule                  varchar(100),
    swa_reported_bad_date     date,
    swa_rule_formula          varchar(100),
    swa_current_user          varchar(10),
    swa_current_status        varchar(2),
    swa_current_modified_date date,
    swa_current_pid           numeric(3),
    swa_version               numeric(6),
    row_number                numeric
);
