-- liquibase formatted sql

-- changeset codex:0180-113

create table if not exists _ct_condition_variants
(
    cov_condition_variant     varchar(20),
    cov_id                    numeric(12) not null
        primary key,
    cov_con_id                numeric(12)
        constraint fk_ct_condition_variants_cov_con_id
            references _ct_conditions,
    cov_current_pid           numeric(3),
    cov_alert_movement        varchar(1),
    cov_report_movement       varchar(1),
    cov_short_description     varchar(20),
    cov_long_description      varchar(60),
    cov_default_period        numeric(3),
    cov_effective_from_date   date,
    cov_access_restricted     varchar(1),
    cov_scope                 varchar(1),
    cov_auto_snaffle          varchar(1),
    cov_alert_marker_creation varchar(1),
    cov_effective_to_date     date,
    cov_letter_type           varchar(3),
    cov_letter_data_source    varchar(1),
    cov_live_indicator        varchar(1),
    cov_letter_max_animals    numeric(3),
    cov_multiple_usage        varchar(1),
    cov_movt_restrict_type    varchar(2),
    cov_cessation_reason      varchar(2),
    cov_current_status        varchar(2),
    cov_current_user          varchar(10),
    cov_current_modified_date date,
    cov_version               numeric(6),
    row_number                numeric
);
