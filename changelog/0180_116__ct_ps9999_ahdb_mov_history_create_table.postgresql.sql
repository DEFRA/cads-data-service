-- liquibase formatted sql

-- changeset codex:0180-116

create table if not exists _ct_ps9999_ahdb_mov_history
(
    ran_id              numeric,
    on_date             date,
    off_date            date,
    loc_id              numeric,
    loc_full_identifier varchar(14),
    row_number          numeric
);
