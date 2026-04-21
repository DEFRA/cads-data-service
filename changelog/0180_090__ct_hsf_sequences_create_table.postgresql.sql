-- liquibase formatted sql

-- changeset codex:0180-090

create table if not exists _ct_hsf_sequences
(
    hss_sequence_key varchar(20),
    hss_sequence     numeric(12),
    row_number       numeric
);
