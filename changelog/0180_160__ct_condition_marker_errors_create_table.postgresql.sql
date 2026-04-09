-- liquibase formatted sql

-- changeset codex:0180-160

create table if not exists _ct_condition_marker_errors
(
    cme_id                    numeric(12) not null
        primary key,
    cme_scm_id                numeric(12)
        constraint fk_ct_condition_marker_errors_cme_scm_id
            references _ct_susp_condition_markers,
    cme_attribute_name        varchar(30),
    cme_error_code            varchar(5),
    cme_current_status        varchar(2),
    cme_current_user          varchar(10),
    cme_current_modified_date date,
    cme_current_pid           numeric(3),
    cme_version               numeric(6),
    row_number                numeric
);
