-- liquibase formatted sql

-- changeset codex:0180-139

create table if not exists _ct_susp_movement_errors
(
    sme_smo_id                numeric(12)
        constraint fk_ct_susp_movement_errors_sme_smo_id
            references _ct_suspended_movements,
    sme_id                    numeric(12) not null
        primary key,
    sme_attribute_name        varchar(30),
    sme_error_code            varchar(4),
    sme_current_user          varchar(10),
    sme_current_modified_date date,
    sme_current_status        varchar(2),
    sme_current_pid           numeric(3),
    sme_version               numeric(6),
    row_number                numeric
);
