-- liquibase formatted sql

-- changeset codex:0180-035

create table if not exists animal_lost_or_stolen_state
(
    state text not null
        primary key
);
