-- liquibase formatted sql

-- changeset codex:0180-004

create table if not exists party_species
(
    species varchar(50) not null
        primary key
);
