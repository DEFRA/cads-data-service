-- liquibase formatted sql

-- changeset codex:0180-060

create table if not exists _ct_batch_retention_conf
(
    brt_item_id        varchar(50),
    brt_retention_days numeric(4),
    brt_description    varchar(200),
    row_number         numeric
);
