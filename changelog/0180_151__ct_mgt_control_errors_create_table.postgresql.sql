-- liquibase formatted sql

-- changeset codex:0180-151

create table if not exists _ct_mgt_control_errors
(
    mce_id                      numeric(12) not null
        primary key,
    mce_ran_id                  numeric(12)
        constraint fk_ct_mgt_control_errors_mce_ran_id
            references _ct_registered_animals,
    mce_error_code              varchar(5),
    mce_passport_version_issued numeric(4),
    mce_number_of_days_late     numeric(4),
    mce_current_user            varchar(10),
    mce_current_status          varchar(2),
    mce_current_modified_date   date,
    mce_current_pid             numeric(3),
    mce_version                 numeric(6),
    row_number                  numeric
);
