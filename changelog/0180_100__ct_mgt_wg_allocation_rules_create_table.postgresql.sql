-- liquibase formatted sql

-- changeset codex:0180-100

create table if not exists _ct_mgt_wg_allocation_rules
(
    war_id                    numeric(12) not null
        primary key,
    war_rou_id                numeric(12)
        constraint fk_ct_mgt_wg_allocation_rules_war_rou_id
            references _ct_alloc_routines,
    war_priority              numeric(3),
    war_suspense_type         varchar(1),
    war_rule                  varchar(100),
    war_rule_formula          varchar(100),
    war_current_user          varchar(10),
    war_current_status        varchar(2),
    war_current_modified_date date,
    war_current_pid           numeric(3),
    war_version               numeric(6),
    row_number                numeric
);
