-- liquibase formatted sql

-- changeset codex:0180-026

create table if not exists animal_party_ref
(
    identifier integer not null
        primary key
);
