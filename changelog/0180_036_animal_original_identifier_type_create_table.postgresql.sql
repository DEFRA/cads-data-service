-- liquibase formatted sql

-- changeset codex:0180-036

create table if not exists animal_original_identifier_type
(
    type text not null
        primary key
);
