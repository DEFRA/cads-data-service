-- liquibase formatted sql

-- changeset codex:0180-067

create table if not exists _ct_claim_statuses
(
    cls_id                    numeric(12) not null
        primary key,
    cls_current_pid           numeric(3),
    cls_current_status        varchar(2),
    cls_current_user          varchar(10),
    cls_current_modified_date date,
    cls_claim_status          varchar(2),
    cls_description           varchar(240),
    cls_version               numeric(6),
    row_number                numeric
);
