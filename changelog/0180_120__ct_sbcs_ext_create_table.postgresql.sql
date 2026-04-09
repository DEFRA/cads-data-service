-- liquibase formatted sql

-- changeset codex:0180-120

create table if not exists _ct_sbcs_ext
(
    sxt_id     varchar(20),
    row_number numeric
);
