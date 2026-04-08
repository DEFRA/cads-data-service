-- liquibase formatted sql

-- changeset codex:0180-075

create table if not exists _ct_cts164_handshake_file_keys
(
    bjk_batch_id      numeric(12),
    bjk_group_id      numeric(12),
    bjk_filetype      varchar(30),
    bjk_key           varchar(30),
    bjk_modified_date date
);
