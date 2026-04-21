-- liquibase formatted sql

-- changeset codex:0180-002

create table if not exists party_type
(
    type varchar(30) not null
        primary key
);
