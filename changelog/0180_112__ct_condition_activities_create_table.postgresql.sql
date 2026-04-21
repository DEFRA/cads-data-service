-- liquibase formatted sql

-- changeset codex:0180-112

create table if not exists _ct_condition_activities
(
    cac_id                    numeric(12) not null
        primary key,
    cac_con_id                numeric(12)
        constraint fk_ct_condition_activities_cac_con_id
            references _ct_conditions,
    cac_short_description     varchar(20),
    cac_activity_code         varchar(8),
    cac_long_description      varchar(60),
    cac_current_user          varchar(10),
    cac_current_status        varchar(2),
    cac_current_pid           numeric(3),
    cac_current_modified_date date,
    cac_version               numeric(6),
    row_number                numeric
);
