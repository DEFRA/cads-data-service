-- liquibase formatted sql

-- changeset codex:0180-114

create table if not exists _ct_cond_variant_groupings
(
    cvg_id                    numeric(12) not null
        primary key,
    cvg_cov_id                numeric(12)
        constraint fk_ct_cond_variant_groupings_cvg_cov_id
            references _ct_condition_variants,
    cvg_grouping_code         varchar(10),
    cvg_current_modified_date date,
    cvg_current_status        varchar(2),
    cvg_current_user          varchar(10),
    cvg_current_pid           numeric(3),
    cvg_version               numeric(6),
    row_number                numeric
);
