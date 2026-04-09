-- liquibase formatted sql

-- changeset codex:0180-103

create table if not exists _ct_msgtxt
(
    msg_id     varchar(5),
    msg_text   varchar(1000),
    row_number numeric
);
