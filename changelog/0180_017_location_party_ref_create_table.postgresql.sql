-- liquibase formatted sql

-- changeset codex:0180-017

create table if not exists location_party_ref
(
    party_identifier bigint not null
        primary key
);
