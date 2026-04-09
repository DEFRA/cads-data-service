-- liquibase formatted sql

-- changeset codex:0180-001

create table if not exists party_location
(
    identifier varchar(50) not null
        primary key
);
