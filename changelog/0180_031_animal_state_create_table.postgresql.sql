-- liquibase formatted sql

-- changeset codex:0180-031

create table if not exists animal_state
(
    state text not null
        primary key
);
