-- liquibase formatted sql

-- changeset codex:0180-158

create table if not exists _ct_movt_corr_summ_errors
(
    mse_id                    numeric(12) not null
        primary key,
    mse_mcs_id                numeric(12)
        constraint fk_ct_movt_corr_summ_errors_mse_mcs_id
            references _ct_movt_correct_summaries,
    mse_current_user          varchar(10),
    mse_current_status        varchar(2),
    mse_current_modified_date date,
    mse_current_pid           numeric(12),
    mse_attribute_name        varchar(30),
    mse_error_code            varchar(10),
    mse_version               numeric(6),
    row_number                numeric
);