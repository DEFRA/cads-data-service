-- liquibase formatted sql

-- changeset codex:0180-038

create table if not exists animal_resolution_type
(
    resolution text not null
        primary key
);
