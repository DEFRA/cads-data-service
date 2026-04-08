-- liquibase formatted sql

-- changeset codex:0180-140

create table if not exists _ct_suspense_char_alloc_rules
(
    sca_id                    numeric(12) not null
        primary key,
    sca_suspense_char         varchar(3),
    sca_rou_id                numeric(12)
        constraint fk_ct_suspense_char_alloc_rules_sca_rou_id
            references _ct_alloc_routines,
    sca_subroutine            varchar(3),
    sca_test_value            varchar(10),
    sca_current_user          varchar(10),
    sca_current_status        varchar(2),
    sca_current_modified_date date,
    sca_current_pid           numeric(3),
    sca_version               numeric(6),
    row_number                numeric
);
