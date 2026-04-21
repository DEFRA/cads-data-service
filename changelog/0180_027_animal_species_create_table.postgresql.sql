-- liquibase formatted sql

-- changeset codex:0180-027

create table if not exists animal_species
(
    species text not null
        primary key
);
