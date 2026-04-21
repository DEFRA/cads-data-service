-- liquibase formatted sql

-- changeset codex:0180-101

create table if not exists _ct_mhs_to_cph
(
    cph        varchar(14),
    mhs_number numeric(4),
    row_number numeric
);
