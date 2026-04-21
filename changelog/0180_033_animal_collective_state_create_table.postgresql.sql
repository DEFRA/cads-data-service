-- liquibase formatted sql

-- changeset codex:0180-033

create table if not exists animal_collective_state
(
    state text not null
        primary key
);
