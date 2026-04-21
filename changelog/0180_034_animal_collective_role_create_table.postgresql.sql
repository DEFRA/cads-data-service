-- liquibase formatted sql

-- changeset codex:0180-034

create table if not exists animal_collective_role
(
    role text not null
        primary key
);
