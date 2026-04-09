-- liquibase formatted sql

-- changeset codex:0180-111

create table if not exists _ct_conditions
(
    con_id                    numeric(12) not null
        primary key,
    con_pch_id                numeric(12)
        constraint fk_ct_conditions_con_pch_id
            references _ct_probity_checks,
    con_cot_id                numeric(12)
        constraint fk_ct_conditions_con_cot_id
            references _ct_condition_types,
    con_current_pid           numeric(3),
    con_report_recipient      varchar(2),
    con_long_description      varchar(60),
    con_allocation_process    varchar(10),
    con_short_description     varchar(20),
    con_scope                 varchar(1),
    con_current_user          varchar(10),
    con_current_status        varchar(2),
    con_current_modified_date date,
    con_condition_code        varchar(12),
    con_version               numeric(6),
    row_number                numeric
);
