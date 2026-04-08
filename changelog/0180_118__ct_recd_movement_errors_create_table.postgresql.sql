-- liquibase formatted sql

-- changeset codex:0180-118

create table if not exists _ct_recd_movement_errors
(
    rme_current_status        varchar(2),
    rme_current_user          varchar(10),
    rme_current_modified_date date,
    rme_current_pid           numeric(3),
    rme_version               numeric(6),
    rme_rmo_id                numeric(12)
        constraint fk_ct_recd_movement_errors_rme_rmo_id
            references _ct_received_movements,
    rme_error_code            varchar(4),
    rme_attribute_name        varchar(30),
    rme_id                    numeric(12) not null
        primary key,
    row_number                numeric
);
