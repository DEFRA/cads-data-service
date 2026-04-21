-- liquibase formatted sql

-- changeset codex:0180-155

create table if not exists _ct_cm_measures_results
(
    cmr_com_id                numeric(12)
        constraint fk_ct_cm_measures_results_cmr_com_id
            references _ct_condition_markers,
    cmr_result_char           varchar(10),
    cmr_measure_char          varchar(10),
    cmr_result_num            numeric(9),
    cmr_measure_num           numeric(9),
    cmr_current_user          varchar(10),
    cmr_current_modified_date date,
    cmr_current_status        varchar(2),
    cmr_current_pid           numeric(3),
    cmr_version               numeric(6),
    cmr_id                    numeric(12) not null
        primary key
);
