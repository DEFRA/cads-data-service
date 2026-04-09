-- liquibase formatted sql

-- changeset codex:0180-094

create table if not exists _ct_late_days
(
    lda_version               numeric(6),
    lda_id                    numeric(12) not null
        primary key,
    lda_applic_type           char,
    lda_start_date            date,
    lda_valid_days            numeric(4),
    lda_current_user          varchar(10),
    lda_current_modified_date date,
    lda_current_pid           numeric(3),
    lda_current_status        varchar(2),
    row_number                numeric
);
