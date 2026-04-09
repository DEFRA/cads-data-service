-- liquibase formatted sql

-- changeset codex:0180-032

create table if not exists animal_breed_state
(
    state text not null
        primary key
);
