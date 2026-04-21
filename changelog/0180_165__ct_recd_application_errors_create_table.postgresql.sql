-- liquibase formatted sql

-- changeset codex:0180-165

create table if not exists _ct_recd_application_errors
(
    rae_id                    numeric(12) not null
        primary key,
    rae_rap_id                numeric(12)
        constraint fk_ct_recd_application_errors_rae_rap_id
            references _ct_received_applications,
    rae_attribute_name        varchar(30),
    rae_error_code            varchar(4),
    rae_current_status        varchar(2),
    rae_current_user          varchar(10),
    rae_current_modified_date date,
    rae_current_pid           numeric(3),
    rae_version               numeric(6),
    row_number                numeric
);
