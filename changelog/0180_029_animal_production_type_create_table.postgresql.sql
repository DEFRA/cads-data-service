-- liquibase formatted sql

-- changeset codex:0180-029

create table if not exists animal_production_type
(
    type text not null
        primary key
);
