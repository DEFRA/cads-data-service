-- liquibase formatted sql

-- changeset codex:0180-168

create table if not exists _ct_animal_corr_summ_errors
(
    ase_id                    numeric(12) not null
        primary key,
    ase_acs_id                numeric(12)
        constraint fk_ct_animal_corr_summ_errors_ase_acs_id
            references _ct_animal_correct_summaries,
    ase_current_user          varchar(10),
    ase_current_status        varchar(2),
    ase_current_modified_date date,
    ase_current_pid           numeric(12),
    ase_attribute_name        varchar(30),
    ase_error_code            varchar(10),
    ase_version               numeric(6),
    row_number                numeric
);
