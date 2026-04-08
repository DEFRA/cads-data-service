-- liquibase formatted sql

-- changeset codex:0180-119

create table if not exists _ct_reset_to_extract
(
    rte_id         numeric not null
        primary key,
    rte_table_name varchar(50),
    rte_status     varchar(1),
    rte_batch      numeric(5),
    row_number     numeric
);
