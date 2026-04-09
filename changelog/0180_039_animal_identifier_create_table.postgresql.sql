-- liquibase formatted sql

-- changeset codex:0180-039

create table if not exists animal_identifier
(
    identifier        text not null
        primary key,
    animal_identifier text not null
        unique
);
