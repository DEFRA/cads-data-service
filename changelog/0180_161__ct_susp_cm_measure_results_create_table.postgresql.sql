-- liquibase formatted sql

-- changeset codex:0180-161

create table if not exists _ct_susp_cm_measure_results
(
    smr_id                    numeric(12) not null
        primary key,
    smr_scm_id                numeric(12)
        constraint fk_ct_susp_cm_measure_results_smr_scm_id
            references _ct_susp_condition_markers,
    smr_measure_char          varchar(10),
    smr_result_num            numeric(9),
    smr_measure_num           numeric(9),
    smr_result_char           varchar(10),
    smr_current_status        varchar(2),
    smr_current_modified_date date,
    smr_current_user          varchar(10),
    smr_current_pid           numeric(3),
    smr_version               numeric(6),
    row_number                numeric
);
